using BlogService.Application.DTOs;
using BlogService.Domain.Entities;
using BlogService.Domain.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hecate.Common.Events;
using MassTransit;

namespace BlogService.Application.Handlers
{
    public class CreatePostHandler : IRequestHandler<CreatePostRequest, string>
    {
        private readonly IPostRepository _postRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public CreatePostHandler(IPostRepository postRepository, IPublishEndpoint publishEndpoint)
        {
            _postRepository = postRepository;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<string> Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                AuthorId = request.AuthorId
            };

            await _postRepository.CreateAsync(post);
            
            await _publishEndpoint.Publish(new BlogPostCreatedEvent
            {
                PostId = post.Id,
                Title = post.Title,
                AuthorId = post.AuthorId,
                CreatedAt = post.CreatedAt
            });

            return post.Id;
        }
    }
}
