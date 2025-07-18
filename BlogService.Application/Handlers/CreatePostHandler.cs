using BlogService.Application.DTOs;
using BlogService.Domain.Entities;
using BlogService.Domain.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Application.Handlers
{
    public class CreatePostHandler : IRequestHandler<CreatePostRequest, string>
    {
        private readonly IPostRepository _postRepository;
        public CreatePostHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<string> Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                AuthorId = request.AuthorId
            };

            return await _postRepository.CreateAsync(post);
        }
    }
}
