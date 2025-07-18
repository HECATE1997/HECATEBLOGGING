using IdentityService.Application.Interfaces;
using IdentityService.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public IdentityService(UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<(bool Succeeded, string UserId, IEnumerable<string> Errors)> RegisterUserAsync(string username, string email, string password)
        {
            var user = new ApplicationUser { UserName = username, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded
                ? (true, user.Id, Enumerable.Empty<string>())
                : (false, null, result.Errors.Select(e => e.Description));
        }
        public async Task<(bool Succeeded, string UserId, string Token, IEnumerable<string> Errors)> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return (false, null, null, new[] { "Invalid credentials" });
            }

            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email);
            return (true, user.Id, token, Enumerable.Empty<string>());
        }

    }
}
