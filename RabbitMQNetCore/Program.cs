using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using RabbitMQ.Client;
using System.Threading;
using RabbitMQNetCore.ConfiguringDeadLetterExchange;

namespace RabbitMQNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();



            RabbitMQConnection rabbitMQConnection = new
                RabbitMQConnection();
            IModel channel = rabbitMQConnection.CreateModel();
            Consumidor consumer = new Consumidor(channel);
            consumer.SetConsumidor();
            Produtor producer = new Produtor(channel);
            producer.Publish("teste");
            Thread.Sleep(10000);


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
