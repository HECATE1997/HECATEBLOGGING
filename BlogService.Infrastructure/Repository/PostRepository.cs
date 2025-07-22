using BlogService.Domain.Entities;
using BlogService.Domain.IRepository;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Infrastructure.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IMongoCollection<Post> _posts;

        public PostRepository(IConfiguration configuration)
        {
            var mongoDbConfig = configuration.GetSection("MongoDB");
            var client = new MongoClient(mongoDbConfig["ConnectionString"]);
            var database = client.GetDatabase(mongoDbConfig["Database"]);
            _posts = database.GetCollection<Post>("Posts");
        }

        public async Task<string> CreateAsync(Post post)
        {
            await _posts.InsertOneAsync(post);
            return post.Id;
        }

        public async Task<Post> GetByIdAsync(string id)
        {
            return await _posts.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _posts.Find(_ => true).ToListAsync();
        }
    }
}
