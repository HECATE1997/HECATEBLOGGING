using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<(bool Succeeded, string UserId, string Token, IEnumerable<string> Errors)> LoginAsync(string email, string password);
        Task<(bool Succeeded, string UserId, IEnumerable<string> Errors)> RegisterUserAsync(string username, string email, string password);
    }
}
