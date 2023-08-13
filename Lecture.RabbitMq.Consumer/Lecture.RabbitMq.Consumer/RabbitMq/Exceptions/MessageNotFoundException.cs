namespace Lecture.RabbitMq.Publisher.RabbitMq.Exceptions;

public class MessageNotFoundException : Exception
{
    private const string DefaultMessage = "None message whas found on Queue ";
    public MessageNotFoundException(Queues queue) : base(DefaultMessage + queue)
    {
    }
}