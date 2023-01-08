using Microsoft.AspNetCore.Authorization;
using Website.Common.WebFrameworks;
using Website.Services.AuthServices;

namespace Website.Api.Controllers;

[Authorize]
public class SecureBaseController : BaseController
{
    private readonly IAuthService AuthServices;

    public SecureBaseController(IAuthService authService) =>
        AuthServices = authService ?? throw new ArgumentNullException(nameof(authService));
    
}
