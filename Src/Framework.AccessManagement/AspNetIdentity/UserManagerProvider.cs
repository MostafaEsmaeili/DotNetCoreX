using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pishkhan.Common;
using Pishkhan.Common.Database;
using Pishkhan.Domain.Entity;
using Pishkhan.Domain.Enum;
using Pishkhan.Framework.Infra.IoC;
using Pishkhan.Framework.Infra.Logging;
using Pishkhan.Framework.Infra.Utility;
using Pishkhan.Framework.Infra.Validation;
using Pishkhan.UserManagement.AspNetUser;

namespace Pishkhan.UserManagement
{
    public class UserManagerProvider : BaseProvider
    {

        public IAspNetUsersRepository AspNetUsersRepository =>
            CoreContainer.Container.Resolve<IAspNetUsersRepository>();

        public IPasswordHasher<ApplicationUser> PasswordHasher => new PasswordHasher<ApplicationUser>(null);
         


        public async Task<int> Register( RegisterDto model)
        {
            try
            {
                RuleExceptionCode.FilterIsNull.Assert(model != null);
                RuleExceptionCodeAccounting.UserNameIsInvalid.Assert(!model.UserName.IsNullOrEmpty());
                RuleExceptionCodeAccounting.UserNameIsInvalid.Assert(!model.Password.IsNullOrEmpty());
                var ifExist = await AspNetUsersRepository.GetUserByName(model.UserName);
                RuleExceptionCodeAccounting.UserNameIsDuplicate.Assert(ifExist==null);
               
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                };
                var applicationUser = new ApplicationUser
                {
                    UserName = model.UserName,
                    
                };
                var result = await AspNetUsersRepository.SaveAsync(applicationUser);
                applicationUser.Id = result;
                applicationUser.PasswordHash = PasswordHasher.HashPassword(applicationUser, model.Password);

               return await AspNetUsersRepository.SaveOrUpdateAsync(applicationUser);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message,ex);
                throw;
            }
        }
        /// <returns></returns>
        public Task<ApplicationUser> FindByUsername(string username)
        {
            return AspNetUsersRepository.GetUserByName(username);
        }

    }
}