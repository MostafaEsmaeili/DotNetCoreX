using System;
using System.Linq;
using System.Security.Claims;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Pishkhan.Domain.Api_Request_Response;
using Pishkhan.Domain.Entity;
using Pishkhan.Framework.Infra.Logging;
using RestSharp;

namespace Sahra.MDP.App.Attribute
{
    public class AuditingAttribute : ActionFilterAttribute
    {
        private CustomLogger Logger => new CustomLogger(GetType().FullName);


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).ActionName;
                var controllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).ControllerName;
                var principal = filterContext.HttpContext.User;
                var userName = principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                var remoteIpAddress = filterContext.HttpContext.Connection.RemoteIpAddress;
                //var controllerContext = new ControllerContext(filterContext.RequestContext, filterContext.Controller);
                var audit = new Audit
                {
                    UserName = (!userName.IsNullOrEmpty()) ? userName : "Anonymous",
                    // UserId = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.GetUserId() : Guid.Empty.ToString(),
                    IpAddress = remoteIpAddress.ToString(),
                    Data = JsonConvert.SerializeObject(filterContext.ActionArguments),
                    TimeAccessed = DateTime.UtcNow,
                    Action = actionName,
                    Controller = controllerName, 
                    MachinName = Environment.MachineName,
                    ApplicationId = 1
                };
              //var apiHelper = new ApiHelper();
              //  var request = apiHelper.Request(ApiAddressProvider.CommonApi + "MakeAudit", Method.POST);
              //  request.AddJsonBody(new ApiRequest<Auditing>()
              //  {
              //    Entity = audit
              //  });
              // request.Exec<ApiResponse<object>>(new ClaimsIdentity(principal.Claims));

            }
            catch (Exception ex)
            {
                throw;
            }

            base.OnActionExecuting(filterContext);
        }

     


    }
}