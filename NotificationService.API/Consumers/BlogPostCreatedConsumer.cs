using MassTransit;
using Hecate.Common.Events;

namespace NotificationService.API.Consumers
{
    public class BlogPostCreatedConsumer : IConsumer<BlogPostCreatedEvent>
    {
        private readonly ILogger<BlogPostCreatedConsumer> _logger;

        public BlogPostCreatedConsumer(ILogger<BlogPostCreatedConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<BlogPostCreatedEvent> context)
        {
            var message = context.Message;

            _logger.LogInformation($"📨 New blog post published: {message.Title} by {message.AuthorId}");

            // Simulate sending a notification
            Console.WriteLine($"[NotificationService] Notify followers: {message.Title} was posted.");

            return Task.CompletedTask;
        }
    }
}
