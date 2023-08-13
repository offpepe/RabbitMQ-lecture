using System.Text;
using System.Text.Json;
using Lecture.RabbitMq.Publisher.RabbitMq.Messages;

namespace Lecture.RabbitMq.Publisher.RabbitMq;

public static class Extensions
{
    public static byte[] ToBytes<TMessage>(this TMessage message) where TMessage : Message
    => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
}