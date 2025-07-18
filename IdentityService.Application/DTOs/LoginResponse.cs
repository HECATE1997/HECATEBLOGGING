using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.DTOs
{
    public record LoginResponse
    {
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}
