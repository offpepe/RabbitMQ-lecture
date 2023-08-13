using Bogus;
using Lecture.RabbitMq.Publisher.RabbitMq.Enums;
using Lecture.RabbitMq.Publisher.RabbitMq.Messages;

namespace Lecture.RabbitMq.Publisher;

public static class MessageFaker
{
    public static Faker<MessageLectureHelloWorld> MessageFake
        => new Faker<MessageLectureHelloWorld>("pt_BR")
            .RuleFor(m => m.Label, f => f.Commerce.ProductDescription())
            .RuleFor(m => m.Message, f => f.Lorem.Paragraphs(1))
            .RuleFor(m => m.MessageType, MessageTypes.Memo);
}