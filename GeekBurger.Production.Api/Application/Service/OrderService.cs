using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

using GeekBurger.Production.Application.Interfaces;
using GeekBurger.Production.Contract;
using Polly;
using System.Net.Http;

namespace GeekBurger.Production.Application.Service
{
    /// <summary>
    /// Order service
    /// </summary>
    public class OrderService : IOrderService
    {
        #region| Properties |

        private const string Topic = "OrderChanged";
        private readonly IConfiguration _configuration;
        private readonly List<Message> _messages;
        private Task _lastTask;
        private readonly IServiceBusNamespace _namespace;
        private readonly ILogService _logService;
        private CancellationTokenSource _cancelMessages;
        private IServiceProvider _serviceProvider { get; }

        #endregion

        #region| Constructor | 

        public OrderService(IConfiguration configuration, ILogService logService, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _logService = logService;
            _messages = new List<Message>();
            _namespace = _configuration.GetServiceBusNamespace();
            _cancelMessages = new CancellationTokenSource();
            _serviceProvider = serviceProvider;
        }
        #endregion

        #region| Methods |

        /// <summary>
        /// Check whether a topic exists. If don't, it creates a new one.
        /// </summary>
        private void EnsureTopicIsCreated()
        {
            if (!_namespace.Topics.List().Any(topic => topic.Name.Equals(Topic, StringComparison.InvariantCultureIgnoreCase)))
                _namespace.Topics.Define(Topic).WithSizeInMB(1024).Create();
        }

        /// <summary>
        /// Start
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            EnsureTopicIsCreated();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stop
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cancelMessages.Cancel();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Send messages asynchronously
        /// </summary>
        public async void SendMessagesAsync()
        {
            if (_lastTask != null && !_lastTask.IsCompleted)
                return;

            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var topicClient = new TopicClient(config.ConnectionString, Topic);

            _logService.SendMessagesAsync("Order was changed");

            _lastTask = SendAsync(topicClient, _cancelMessages.Token);

            await _lastTask;

            var closeTask = topicClient.CloseAsync();
            await closeTask;
            HandleException(closeTask);
        }

        /// <summary>
        /// Add to message list
        /// </summary>
        /// <param name="changes">IEnumerable of EntityEntry<![CDATA[T]]></param>
        public void AddToMessageList(IEnumerable<EntityEntry<Order>> changes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send a topic asynchronously
        /// </summary>
        /// <param name="topicClient">TopicClient</param>
        /// <param name="cancellationToken">CancellationToken</param>
        private async Task SendAsync(TopicClient topicClient, CancellationToken cancellationToken)
        {
            if(_messages.Count<=0)
            {
                return;
            }

            var maxRetryAttempts = int.Parse(_configuration["maxRetryAttempts"]);
            var pauseBetweenFailures = TimeSpan.FromSeconds(int.Parse(_configuration["pauseBetweenFailures"]));

            Message message = null;

            var sendTask = topicClient.SendAsync(message);

            var retryPolicy = Policy
                .Handle<HttpRequestException>(ex => HandleException(sendTask))
                .WaitAndRetryAsync(maxRetryAttempts,i => pauseBetweenFailures);

            //TODO: Veryfy how to pass the cancelation token
            await retryPolicy.ExecuteAsync(async () =>
            {
                lock (_messages)
                {
                    message = _messages.FirstOrDefault();
                }

                await sendTask;
                var success = HandleException(sendTask);

                if (success)
                {
                    if (message != null)
                    {
                        _messages.Remove(message);
                    }
                }
            });
        }

        /// <summary>
        /// Exception error handler
        /// </summary>
        /// <param name="task">Task</param>
        /// <returns>bool</returns>
        private bool HandleException(Task task)
        {
            if (task.Exception == null || task.Exception.InnerExceptions.Count == 0) return true;

            task.Exception.InnerExceptions.ToList().ForEach(innerException =>
            {
                Console.WriteLine($"Error in SendAsync task: {innerException.Message}. Details:{innerException.StackTrace} ");

                if (innerException is ServiceBusCommunicationException)
                    Console.WriteLine("Connection Problem with Host. Internet Connection can be down");
            });

            return false;
        }

        #endregion
    }
}
