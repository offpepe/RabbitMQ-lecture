

using Lecture.RabbitMq.Publisher;
using Lecture.RabbitMq.Publisher.RabbitMq;


Console.WriteLine("Starting RabbitMQ publisher lecture");
Thread.Sleep(100);
Console.WriteLine("Initializating RabbitMq Connection");
var rabbit = new RabbitMq();
Console.WriteLine("Write limit of messages");
var limitReaded = Console.ReadLine();
var validLimit = int.TryParse(limitReaded, out int limit);
if (!validLimit) throw new ArgumentException("Invalid limit value");
Console.WriteLine("Write delay between every message publish (in milliseconds)");
var delayReaded = Console.ReadLine();
var validDelay = int.TryParse(delayReaded, out int delay);
if (!validDelay)  throw new ArgumentException("Invalid delay value");
Console.WriteLine("Starting massive message sender, with limit of {0}");
Parallel.For(0, limit, (i) =>
{
    try
    {
        var message = MessageFaker.MessageFake.Generate();
        rabbit.PublishMessage(message);
        Thread.Sleep(delay);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return;
    }
    
});