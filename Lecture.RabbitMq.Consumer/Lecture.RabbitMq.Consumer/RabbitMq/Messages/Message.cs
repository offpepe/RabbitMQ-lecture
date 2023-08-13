using System.Text.Json;
using Lecture.RabbitMq.Publisher.RabbitMq.Enums;

namespace Lecture.RabbitMq.Publisher.RabbitMq.Messages;

public class Message
{
    public MessageTypes MessageType { get; set; }
    public string Label { get; set; }

    
}