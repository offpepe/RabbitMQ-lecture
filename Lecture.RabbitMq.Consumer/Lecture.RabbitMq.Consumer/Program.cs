using Lecture.RabbitMq.Publisher.RabbitMq;
using Lecture.RabbitMq.Publisher.RabbitMq.Exceptions;
using Lecture.RabbitMq.Publisher.RabbitMq.Messages;

public class Program
{

    public static async Task GetMessage()
    {
        try
        {
            var bus = new EventBusHandler();
            Console.WriteLine("Trying to reach message on queue");
            var message = await bus.ConsumeMessage<MessageLectureHelloWorld>();
            Console.WriteLine("New message was received!\n Label: {0}\n Type: {1}\n Message: {2}", message.Label,
                message.MessageType, message.Message);  
        }
        catch (MessageNotFoundException e)
        {
            Console.WriteLine(e.Message);
            Thread.Sleep(1000);
        }
        catch (Exception e)
        {
            Console.WriteLine("an unexpected exception was found!\n message: {0}\n stacktrace: {1} ", e.Message, e.StackTrace);
            Console.WriteLine("program exit with code -1");
            throw;
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to bus consuming test");
        Task.Delay(100);
        Console.WriteLine("This exercise was created because of Macoratti's Lecture");
        Task.Delay(100);
        Console.WriteLine("Start to listen queue until any message be found");
        while (true)
        {
            try
            {
                GetMessage().Wait();
            }
            catch (Exception e)
            {
                break;
            }
        }

    }
}