using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyMgmt.Application.Features.Auth.Commands.Login;

namespace PropertyMgmt.Api.Controllers;

public class AuthController : BaseApiController
    {
        [HttpPost("login")]
        [AllowAnonymous] // هام جداً: يسمح بالدخول بدون توكن لأن هذا هو مسار الحصول على التوكن
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            // توجيه الطلب عبر MediatR
            var response = await Mediator.Send(command);
            
            // إرجاع التوكن والبيانات بـ HTTP Status 200
            return Ok(response);
        }
    }
