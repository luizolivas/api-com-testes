using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
//Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
var factory = new ConnectionFactory
{
    HostName = "localhost"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();


var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    // Verifica a fila de origem da mensagem
    var queue = eventArgs.RoutingKey;

    if (queue.Contains("dead"))
    {
        Console.WriteLine("Message received from dead letter queue: " + message);
        // Lógica adicional para mensagens da fila de mensagens mortas
    }
    else
    {
        Console.WriteLine("Message received from main queue: " + message);
    }
};

// Consuma mensagens tanto da fila principal quanto da fila de mensagens mortas
channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);
channel.BasicConsume(queue: "product-dead-letter", autoAck: true, consumer: consumer);

Console.ReadKey();