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
    public class RegisterUserHandler : IRequestHandler<RegisterRequest, RegisterResponse>
    {
        private readonly IIdentityService _identityService;

        public RegisterUserHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var (succeeded, userId, errors) = await _identityService.RegisterUserAsync(request.UserName, request.Email, request.Password);

            if (!succeeded)
            {
                throw new ApplicationException(string.Join(", ", errors));
            }

            return new RegisterResponse
            {
                UserId = userId,
                Message = "User registered successfully."
            };
        }
    }
}
