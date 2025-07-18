using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Application.DTOs
{
    public class CreatePostRequest : IRequest<string>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
    }
}
