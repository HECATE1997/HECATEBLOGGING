using BlogService.Application.DTOs;
using BlogService.Domain.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Application.Handlers
{
    public class GetAllPostsHandler : IRequestHandler<GetAllPostsRequest, List<PostDto>>
    {
        private readonly IPostRepository _postRepository;

        public GetAllPostsHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<PostDto>> Handle(GetAllPostsRequest request, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetAllAsync();

            return posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                AuthorId = p.AuthorId,
                CreatedAt = p.CreatedAt
            }).ToList();
        }
    }
}
