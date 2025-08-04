using Hecate.Common.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Tests.Fakes
{
    public class InMemoryBlogConsumer : IConsumer<BlogPostCreatedEvent>
    {
        private readonly ILogger<InMemoryBlogConsumer> _logger;

        public InMemoryBlogConsumer(ILogger<InMemoryBlogConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<BlogPostCreatedEvent> context)
        {
            var message = context.Message;

            _logger.LogInformation($"New blog post published: {message.Title} by {message.AuthorId}");

            // Simulate sending a notification
            Console.WriteLine($"[NotificationService] Notify followers: {message.Title} was posted.");

            return Task.CompletedTask;
        }
    }
}
