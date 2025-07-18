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
    public class GetPostByIdHandler : IRequestHandler<GetPostByIdRequest, PostDto>
    {
        private readonly IPostRepository _postRepository;

        public GetPostByIdHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<PostDto> Handle(GetPostByIdRequest request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.Id);

            if (post == null)
                return null;

            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                AuthorId = post.AuthorId,
                CreatedAt = post.CreatedAt
            };
        }
    }
}
