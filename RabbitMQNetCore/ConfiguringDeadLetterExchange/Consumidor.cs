using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQNetCore.ConfiguringDeadLetterExchange
{
    public class Consumidor
    {
        public Consumidor(IModel channel)
        {
            _channel = channel;
        }
        private IModel _channel;

        public void SetConsumidor()
        {
            var consumidor = new EventingBasicConsumidor(_channel);
            consumidor.Received += ReceivedEvent;
            _channel.BasicConsumidor(queue: "demo-queue", autoAck:
                false, consumidor: consumidor);
        }

        public async void ReceivedEvent(object sender,
            BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.Span);
            if (e.RoutingKey == "demo-queue")
            {
                var deserializedMessage =
                    JsonConvert.DeserializeObject<object>(message);
                _channel.BasicNack(e.DeliveryTag, false, false);
            }
        }
    }
}
