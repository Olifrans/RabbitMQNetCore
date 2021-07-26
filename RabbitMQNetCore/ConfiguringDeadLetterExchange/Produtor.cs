using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;


namespace RabbitMQNetCore.ConfiguringDeadLetterExchange
{
    public class Produtor
    {
        private IModel _channel;
        public Produtor(IModel channel)
        {
            _channel = channel;
        }

        public void Publish(object publishModel)
        {
            var message = JsonConvert.SerializeObject(publishModel);
            var body = Encoding.UTF8.GetBytes(message);
            IBasicProperties properties
                = _channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.DeliveryMode = 2;
            _channel.ConfirmSelect();
            _channel.BasicPublish(exchange: "amq.direct",
                routingKey: "demo-queue", mandatory: true,
                basicProperties: properties, body: body);
            _channel.WaitForConfirmsOrDie();
            _channel.ConfirmSelect();
        }
    }
}
