using System.Security.Claims;
using KSFramework;
using KSFramework.Enums;
using KSFramework.Responses;
using Microsoft.AspNetCore.Mvc;
using Website.Common.WebFrameworks.Routing;

namespace Website.Common.WebFrameworks;

[ApiController]
[Route(Routes.BaseRootAddress)]
public class BaseController : ControllerBase
{
    [NonAction]
    protected ActionResult CustomPagedOk(object data, int? pageIndex, int? totalPages, int? totalItems,
        bool? showPagination, string message = "")
    {
        return Ok(new ResultMessage<object>(true, data, ApiResultStatusCode.Success, message, pageIndex, totalPages, totalItems, showPagination));
    }

    [NonAction]
    protected ActionResult CustomOk(object data, string message = "")
    {
        return Ok(new ResultMessage<object>(true, data, ApiResultStatusCode.Success, message));
    }

    [NonAction]
    protected ActionResult CustomOk<T>(T data)
    {
        return Ok(new OkResponse<T>(data));
    }

    [NonAction]
    protected ActionResult CustomError(ApiResultStatusCode status, string message = "")
    {
        return BadRequest(new ResultMessage(false, status, message));
    }
    
    protected string UserId => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
    protected string UserEmail => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;
    protected List<string> UserRoles => User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
    protected Dictionary<string, string> UserClaims => User.Claims.ToDictionary(x => x.Type, x => x.Value);
    protected bool IsAdmin => UserRoles.Contains("SysAdmin");

    protected bool IsInRole(string role)
    {
        return UserRoles.Contains(role);
    }

    protected bool HasPermission(string resourceId, bool adminPermitted = true)
    {
        return (IsAdmin && adminPermitted) || UserId == resourceId;
    }
    
    protected string GetUserIp()
    {
        return Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()!;
    }
}