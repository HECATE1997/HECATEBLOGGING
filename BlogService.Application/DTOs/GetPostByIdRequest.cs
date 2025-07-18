using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Application.DTOs
{
    public class GetPostByIdRequest : IRequest<PostDto>
    {
        public string Id { get; set; }
    }
}
