using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using GeekBurger.Production.Application.Interfaces;

using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Polly;

namespace GeekBurger.Production.Application.Service
{
    /// <summary>
    /// Log service
    /// </summary>
    public class LogService : ILogService
    {
        #region| Fields |

        private const string MicroService = "Production";
        private const string Topic = "Log";
        private IConfiguration _configuration;
        private List<Message> _messages;
        private Task _lastTask;
        private IServiceBusNamespace _namespace;

        #endregion

        #region| Constructor |

        public LogService(IConfiguration configuration)
        {
            _configuration = configuration;
            _messages = new List<Message>();
            _namespace = _configuration.GetServiceBusNamespace();

            EnsureTopicIsCreated();
        }

        #endregion

        #region| Methods |

        private void EnsureTopicIsCreated()
        {
            if (!_namespace.Topics.List()
                .Any(topic => topic.Name
                    .Equals(Topic, StringComparison.InvariantCultureIgnoreCase)))
                _namespace.Topics.Define(Topic)
                    .WithSizeInMB(1024).Create();
        }

        public Message GetMessage(string message)
        {
            var productChangedByteArray = Encoding.UTF8.GetBytes(message);

            return new Message
            {
                Body = productChangedByteArray,
                MessageId = Guid.NewGuid().ToString(),
                Label = MicroService
            };
        }

        public async void SendMessagesAsync(string message)
        {
            _messages.Add(GetMessage(message));

            if (_lastTask != null && !_lastTask.IsCompleted)
                return;

            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var topicClient = new TopicClient(config.ConnectionString, Topic);

            _lastTask = SendAsync(topicClient);

            await _lastTask;

            var closeTask = topicClient.CloseAsync();
            await closeTask;
            HandleException(closeTask);
        }

        /// <summary>
        /// Send message async
        /// </summary>
        /// <param name="topicClient"></param>
        /// <returns></returns>
        public async Task SendAsync(TopicClient topicClient)
        {
            var maxRetryAttempts     = int.Parse(_configuration["maxRetryAttempts"]);
            var pauseBetweenFailures = TimeSpan.FromSeconds(int.Parse(_configuration["pauseBetweenFailures"]));

            Message message = null;

            var sendTask = topicClient.SendAsync(message);

            var retryPolicy = Policy
                .Handle<HttpRequestException>(ex=> HandleException(sendTask))
                .WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures);

            await retryPolicy.ExecuteAsync(async () =>
            {
                lock (_messages)
                {
                    message = _messages.FirstOrDefault();
                }

                sendTask = topicClient.SendAsync(message);

                await sendTask;

                var success = HandleException(sendTask);

                if (success)
                {
                    _messages.Remove(message);
                }
            });
        }

        /// <summary>
        /// Handle task exception
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public bool HandleException(Task task)
        {
            if (task.Exception == null || task.Exception.InnerExceptions.Count == 0) return true;

            task.Exception.InnerExceptions.ToList().ForEach(innerException =>
            {
                Trace.WriteLine($"Error in SendAsync task: {innerException.Message}. Details:{innerException.StackTrace} ");

                if (innerException is ServiceBusCommunicationException)
                {
                    Trace.WriteLine("Connection Problem with Host. Internet Connection can be down");
                }
            });

            return false;
        } 
        #endregion
    }
}