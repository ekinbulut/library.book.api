using System;
using System.Diagnostics.CodeAnalysis;
using Library.Book.Service;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Options;

namespace Library.Book.Api
{
    [ExcludeFromCodeCoverage]
    public class BusConfigurator
    {
//        private readonly IEnumerable<IConsumeObserver> _consumeObservers;
//        private readonly IEnumerable<IPublishObserver> _publishObservers;
        private readonly IOptions<MassTransitConfigConstants> _massTransitConfigOptions;


        public BusConfigurator(IOptions<MassTransitConfigConstants> massTransitConfigOptions)
        {
//            _consumeObservers = consumeObservers;
//            _publishObservers = publishObservers;
            _massTransitConfigOptions = massTransitConfigOptions;
        }

        public IBusControl ConfigureBus(
            Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
        {
            var massTransitConfigConstants = _massTransitConfigOptions.Value;
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(massTransitConfigConstants.HostAddress), hst =>
                {
                    hst.Username(massTransitConfigConstants.Username);
                    hst.Password(massTransitConfigConstants.Password);
                });
                registrationAction?.Invoke(cfg, host);
            });

//            _consumeObservers?.ToList().ForEach(consumeObserver => busControl.ConnectConsumeObserver(consumeObserver));
//            _publishObservers?.ToList().ForEach(publishObserver => busControl.ConnectPublishObserver(publishObserver));
            return busControl;
        }
    }
}