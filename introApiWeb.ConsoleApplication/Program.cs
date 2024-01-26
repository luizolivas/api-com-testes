﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
//Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
var factory = new ConnectionFactory
{
    HostName = "localhost"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// Declare a fila principal
channel.QueueDeclare("product", durable: true, exclusive: false, autoDelete: false);

// Declare a fila de mensagens mortas
channel.QueueDeclare("product-dead-letter", durable: true, exclusive: false, autoDelete: false);

// Declare a troca de mensagens mortas
channel.ExchangeDeclare("product-dead-letter-exchange", ExchangeType.Fanout, durable: true);

// Vincule a fila de mensagens mortas à troca de mensagens mortas
channel.QueueBind("product-dead-letter", "product-dead-letter-exchange", routingKey: "");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine("Product message received: " + message);
};

// Consuma mensagens tanto da fila principal quanto da fila de mensagens mortas
channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);
channel.BasicConsume(queue: "product-dead-letter", autoAck: true, consumer: consumer);

Console.ReadKey();