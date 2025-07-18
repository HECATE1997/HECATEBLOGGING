using BlogService.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: /api/posts
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
            var id = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        // GET: /api/posts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var post = await _mediator.Send(new GetPostByIdRequest { Id = id });

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        // GET: /api/posts
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _mediator.Send(new GetAllPostsRequest());
            return Ok(posts);
        }
    }
}
