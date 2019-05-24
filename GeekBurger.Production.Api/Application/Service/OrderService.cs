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
    public class OrderService : IOrderService
    {
        #region| Properties |

        private const string CONST_TOPIC = "OrderChanged";
        private readonly List<Message> _messages;
        private readonly IServiceBusNamespace _namespace;
        private readonly CancellationTokenSource _cancelMessages;
        private readonly IConfiguration _configuration;

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

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void WaitOrderChanged()
        {
            throw new NotImplementedException();
        }

        private void EnsureTopicIsCreated()
        {
            if (!_namespace.Topics.List().Any(topic => topic.Name.Equals(CONST_TOPIC, StringComparison.InvariantCultureIgnoreCase)))
            {
                _namespace.Topics.Define(CONST_TOPIC).WithSizeInMB(1024).Create();
            }
        }

        #endregion

        #region| Constructor | 

        public OrderService(IConfiguration configuration)
        {
            this._messages = new List<Message>();
            this._cancelMessages = new CancellationTokenSource();
            this._configuration = configuration;

            this._namespace = _configuration.GetServiceBusNamespace();
        } 
        #endregion
    }
}
