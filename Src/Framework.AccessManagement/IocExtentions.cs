using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Pishkhan.Domain.Entity;

namespace Pishkhan.UserManagement
{
    public static class IocExtentions
    {
        public static void ConfigAspNetIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();



            //// services.TryAddScoped(typeof(SignInManager));
            // services.TryAddScoped(typeof(UserManager));
            // services.TryAddScoped(typeof(RoleManager));
            //// services.TryAddScoped(typeof(UserManagerProvider));



        }


    }
}