using BlogService.Domain.Entities;
using BlogService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Tests.Fakes
{
    public class InMemoryPostRepository : IPostRepository
    {
        private readonly List<Post> _posts = new();

        public Task<string> CreateAsync(Post post)
        {
            post.Id = Guid.NewGuid().ToString();
            post.CreatedAt = DateTime.UtcNow;
            _posts.Add(post);
            return Task.FromResult(post.Id);
        }

        public Task<List<Post>> GetAllAsync() => Task.FromResult(_posts);
        public Task<Post> GetByIdAsync(string id) => Task.FromResult(_posts.FirstOrDefault(p => p.Id == id));
    }
}
