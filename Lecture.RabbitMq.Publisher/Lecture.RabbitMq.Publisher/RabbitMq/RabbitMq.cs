using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
using Lecture.RabbitMq.Publisher.RabbitMq.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Lecture.RabbitMq.Publisher.RabbitMq;

public class RabbitMq
{
    private readonly IConnectionFactory _factory;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly EventingBasicConsumer _consumer;

    public RabbitMq()
    {
        _factory = new ConnectionFactory()
        {
            HostName = "localhost",
        };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(
            Queues.LectureQueue.ToString(),
            true,
            false,
            false
        );
        _consumer = new EventingBasicConsumer(_channel);
    }

    public void PublishMessage<TMessage>(TMessage message) where TMessage : Message
    {
        try
        {
            Console.WriteLine($"[RABBIT-MQ] PUBLISHING MESSAGE ON QUEUE {Queues.LectureQueue}");
            _channel.BasicPublish(
                "",
                Queues.LectureQueue.ToString(),
                null,
                message.ToBytes()
            );
        }
        catch (Exception e)
        {
            Console.WriteLine("[RABBIT-MQ] ERROR WHILE PUBLISHING MESSAGE:\n {Message} |\n INNER: {InnerMessage}",
                e.Message, e.InnerException!.Message);
        }
        finally
        {
            Console.WriteLine("[RABBIT-MQ] MESSAGE DELIVERED TO QUEUE WITH SUCCESS!");
        }
    }

    public async Task<TMessage> ConsumeMessage<TMessage>() where TMessage : Message
    {
        var messageTask = new TaskCompletionSource<TMessage>();
        _consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = JsonSerializer.Deserialize<TMessage>(Encoding.UTF8.GetString(body));
            if (message == null)
            {
                messageTask.SetException(new Exception("None message was found on queue"));
                return;
            }
            messageTask.SetResult(message);
        };
        _channel.BasicConsume(Queues.LectureQueue.ToString(), true, _consumer);
        return await messageTask.Task;
    }
    
    
    
}


public enum Queues
{
    LectureQueue
}