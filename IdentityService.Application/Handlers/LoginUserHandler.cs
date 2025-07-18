using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IdentityService.Application.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IIdentityService _identityService;

        public LoginUserHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var (succeeded, userId, token, errors) = await _identityService.LoginAsync(request.Email, request.Password);

            if (!succeeded)
                throw new UnauthorizedAccessException(string.Join(", ", errors));

            return new LoginResponse
            {
                Token = token,
                UserId = userId
            };
        }
    }
}
