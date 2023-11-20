
using System.Globalization;
using System.Text;
using CommandService.EventProcessing;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CommandService.AsyncDataServices;

public class MessageBusSubscriber : BackgroundService
{
    private readonly IConfiguration _config;
    private readonly IEventProcessor _eventProcessor;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName;

    public MessageBusSubscriber(
        IConfiguration config, 
        IEventProcessor eventProcessor)
    {
        _eventProcessor = eventProcessor;
        _config = config;

        InitializeRabbitMQ();
    }
    private void InitializeRabbitMQ(){
            var factory = new ConnectionFactory()
            {
                HostName = _config["RabbitMQHost"],
                Port = int.Parse(_config["RabbitMQPort"])
            };


              try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _queueName = _channel.QueueDeclare().QueueName;
                _channel.QueueBind(queue: _queueName, exchange: "trigger", routingKey: "");



                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("--> Listening on the message bus..");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" --> Could not listen to the message bus: ${ex.Message}");
            }
    }
    public void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        Console.WriteLine("--> Rabbit MQ Connection shuts down.");
      
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (ModuleHandle, ea) => 
        {
            Console.WriteLine("--> Event Received!");
               var body =  ea.Body;
               var notifcationMessage = Encoding.UTF8.GetString(body.ToArray());

               _eventProcessor.ProcessEvent(notifcationMessage);
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;

     


    }

    public override void Dispose()
    {
          Console.WriteLine($"Message bus disposed.");
            if (_connection.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        base.Dispose();
    }
}