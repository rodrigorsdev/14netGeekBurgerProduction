using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.Management.ServiceBus.Fluent;

using GeekBurger.Production.Application.Interfaces;
using Microsoft.Extensions.Configuration;

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

        public OrderService(IConfiguration configuration, ILogService logService,  IServiceProvider serviceProvider)
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

        public void NewOrder()
        {
            throw new NotImplementedException();
        }

        public void OrderChanged()
        {
            throw new NotImplementedException();
        }

        public void ProductionAreaChanged()
        {
            throw new NotImplementedException();
        }
        
        public void WaitOrderChanged()
        {
            
        }

        private void EnsureTopicIsCreated()
        {
            if (!_namespace.Topics.List().Any(topic => topic.Name.Equals(Topic, StringComparison.InvariantCultureIgnoreCase)))
                _namespace.Topics.Define(Topic).WithSizeInMB(1024).Create();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            EnsureTopicIsCreated();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cancelMessages.Cancel();
            return Task.CompletedTask;
        }

        #endregion
    }
}
