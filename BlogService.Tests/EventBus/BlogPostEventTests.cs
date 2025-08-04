using BlogService.Application.DTOs;
using BlogService.Application.Handlers;
using BlogService.Domain.IRepository;
using BlogService.Tests.Fakes;
using Hecate.Common.Events;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Tests.EventBus
{
    public class BlogPostEventTests
    {
        [Fact]
        public async Task Should_Publish_BlogPostCreatedEvent_When_Post_Is_Created()
        {
            var services = new ServiceCollection();

            // Add logging for consumer
            services.AddLogging();

            // Register MassTransit test harness and the consumer
            services.AddMassTransitTestHarness(cfg =>
            {
                cfg.AddConsumer<InMemoryBlogConsumer>();
            });

            services.AddScoped<IPostRepository, InMemoryPostRepository>(); // fake or mock
            services.AddScoped<InMemoryBlogConsumer>();

            // Build the provider and create scope
            await using var provider = services.BuildServiceProvider(true);
            await using var scope = provider.CreateAsyncScope();

            // Start MassTransit test harness
            var harness = scope.ServiceProvider.GetRequiredService<ITestHarness>();


            await harness.Start();
            try
            {
                // Arrange: create a sample event message
                var blogEvent = new BlogPostCreatedEvent
                {
                    PostId = "123",
                    Title = "Test Title",
                    AuthorId = "Bijayash",
                    CreatedAt = DateTime.UtcNow
                };
                var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                // Act: publish the event
                await publishEndpoint.Publish(blogEvent);

                // Assert: the consumer should have consumed the event
                var consumerHarness = harness.GetConsumerHarness<InMemoryBlogConsumer>();
                Assert.True(await consumerHarness.Consumed.Any<BlogPostCreatedEvent>(), "Event was not consumed by InMemoryBlogConsumer");

                // Assert: the bus should have received the event
                Assert.True(await harness.Consumed.Any<BlogPostCreatedEvent>(), "Message was not received by the bus");
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}
