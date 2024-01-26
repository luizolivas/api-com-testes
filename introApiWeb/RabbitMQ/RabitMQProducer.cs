﻿using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace introApiWeb.RabbitMQ
{
    // Classe que implementa a interface IRabitMQProducer
    public class RabitMQProducer : IRabitMQProducer
    {
        // Método para enviar mensagens para a fila RabbitMQ
        public void SendProductMessage<T>(T message)
        {
            // Aqui especificamos o servidor Rabbit MQ. Estamos usando a imagem Docker do rabbitmq e a utilizando.
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            // Criação da conexão RabbitMQ usando os detalhes fornecidos pela ConnectionFactory
            var connection = factory.CreateConnection();

            // Criação de um canal (channel) para a sessão e o modelo (model)
            using var channel = connection.CreateModel();

            // Declaração da fila após fornecer o nome e algumas propriedades relacionadas
            //channel.QueueDeclare("productt", exclusive: false);


            //channel.QueueDeclare("product", durable: true, exclusive: false, autoDelete: false);

            // Declaração da fila de mensagens mortas
            channel.QueueDeclare("product-dead-letter", durable: true, exclusive: false, autoDelete: false);

            // Declare a troca de mensagens mortas
            channel.ExchangeDeclare("product-dead-letter-exchange", ExchangeType.Fanout, durable: true);

            // Vincule a fila de mensagens mortas à troca de mensagens mortas
            channel.QueueBind("product-dead-letter", "product-dead-letter-exchange", routingKey: "product-dead");


            // Configuração da fila principal com suporte a mensagens mortas
            var args = new Dictionary<string, object>
            {
                {"x-dead-letter-exchange", "product-dead-letter-exchange"},
            };




            //channel.QueueDeclare("product", durable: true, exclusive: false, autoDelete: false, arguments: args);

            // Serializa a mensagem para JSON
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            // Coloca os dados na fila 'product'
            channel.BasicPublish(exchange: "", routingKey: "product", body: body);

        }
    }
}
