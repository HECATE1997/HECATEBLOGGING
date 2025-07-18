using BlogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Domain.IRepository
{
    public interface IPostRepository
    {
        Task<string> CreateAsync(Post post);
        Task<Post> GetByIdAsync(string id);
        Task<List<Post>> GetAllAsync();
    }
}
