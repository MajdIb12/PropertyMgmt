using MediatR;
using PropertyMgmt.Application.Common.Model.IdentityDtos;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        private readonly IIdentityService _identityService;

        public LoginCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // تحويل الـ Command إلى DTO الذي تفهمه خدمة الـ Identity
            var loginDto = new LoginRequestDto
            {
                Email = request.Email,
                Password = request.Password
            };
            
            // استدعاء الخدمة لإتمام عملية تسجيل الدخول وتوليد التوكن
            return await _identityService.LoginAsync(loginDto);
        }
    }