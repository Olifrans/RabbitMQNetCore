using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;


namespace RabbitMQNetCore.ConfiguringDeadLetterExchange
{
    public class RabbitMQConnection
    {


        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        public RabbitMQConnection()
        {
            _connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(GetConnectionString())
            };
            _connection = _connectionFactory.CreateConnection();
        }
        public IModel CreateModel()
        {
            return _connection.CreateModel();
        }
        private string GetConnectionString()
        {
            string user = "john123";
            string password = "123456";
            string host = "localhost:5672";
            string virtualHost = "demo-vhost";
            string connectionString = "amqp://{0}:{1}@{2}/{3}";
            return string.Format(connectionString, user, password,
                host, virtualHost);
        }


    }
}



//https://medium.com/nerd-for-tech/dead-letter-exchanges-at-rabbitmq-net-core-b6348122460d