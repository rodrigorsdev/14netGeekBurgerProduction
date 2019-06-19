using GeekBurger.Production.Application.Interfaces;
using GeekBurger.Production.Contract;
using GeekBurger.Production.Models;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Production.Application.Service
{
    /// <summary>
    /// Order service
    /// </summary>
    public class NewOrderService : INewOrderService
    {
        private const string Topic = "NewOrder";
        private readonly IConfiguration _configuration;
        private readonly List<Message> _messages;
        private Task _lastTask;
        private readonly IServiceBusNamespace _namespace;
        private readonly ILogService _logService;
        private CancellationTokenSource _cancelMessages;
        private IServiceProvider _serviceProvider { get; }

        public void SendMessagesAsync()
        {
            throw new NotImplementedException();
        }

        public void AddToMessageList(IEnumerable<EntityEntry<Models.Order>> changes)
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
    }
}
