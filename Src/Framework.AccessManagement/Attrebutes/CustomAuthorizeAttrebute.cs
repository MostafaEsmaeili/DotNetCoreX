using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Pishkhan.Framework.Infra.IoC;
using Pishkhan.UserManagement.AccessControl;
using Pishkhan.UserManagement.ApiResources;

namespace Pishkhan.UserManagement.Attrebutes
{
    public class ApiRequirement : IAuthorizationRequirement
    {




    }

    public class ApiRequirementHandler : AuthorizationHandler<ApiRequirement>
    {
        public IApiResourceRepository ApiResourceRepository =>
            CoreContainer.Container.Resolve<IApiResourceRepository>();

        public IAccessControlRepository AccessControlRepository =>
            CoreContainer.Container.Resolve<IAccessControlRepository>();
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiRequirement requirement)
        {
            if (!(context.Resource is AuthorizationFilterContext mvcContext)) return Task.CompletedTask;

            if (mvcContext.Filters.Any(x => x is IAllowAnonymousFilter))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;

            }
            if(!context.User.Identity.IsAuthenticated)
                return Task.CompletedTask;

            var address=mvcContext.ActionDescriptor.AttributeRouteInfo.Template;
            var method = mvcContext.HttpContext.Request.Method;
            var user = context.User;
                 context.Succeed(requirement);

            //var resource = ApiResourceRepository.GetApiByAddressAndType(address, method);
            //if (resource != null)
            //{
            //    var role = user.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Role)?.Value;
            //    var a = AccessControlRepository.GetAccess(user.Identity.Name, role, resource.Id, 1);
            //    if (a)
            //        context.Succeed(requirement);
            //}

            //TODO: Use the following if targeting a version of
            //.NET Framework older than 4.6:
            //      return Task.FromResult(0);
            return Task.CompletedTask;

        }
    }
}