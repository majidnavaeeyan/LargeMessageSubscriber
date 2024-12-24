using LargeMessageSubscriber.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LargeMessageSubscriber.Domain.Authorization
{
  public class LargeMessageAuthorizeAttribute : Attribute, IAuthorizationFilter
  {
    public void OnAuthorization(AuthorizationFilterContext context)
    {
      var _configurationService = context.HttpContext.RequestServices.GetService(typeof(IConfigurationService)) as IConfigurationService;
      var token = context.HttpContext.Request.Headers["Authorization"].ToString();


      var (validationResult, errors, warnings) = _configurationService.ValidateToken(token);
      if (!validationResult)
        context.Result = new UnauthorizedResult();
    }
  }
}
