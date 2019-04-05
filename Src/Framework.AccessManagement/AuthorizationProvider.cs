using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using NLog;
using Pishkhan.Common;
using Pishkhan.Domain.Api_Request_Response;
using Pishkhan.Domain.Entity;
using Pishkhan.Domain.Enum;
using Pishkhan.Framework.Infra.IoC;
using Pishkhan.UserManagement.ApiResources;

namespace Pishkhan.UserManagement
{
    public class AuthorizationProvider
    {
        private readonly IApiDescriptionGroupCollectionProvider _apiExplorer;

        public IApiResourceRepository ApiResourceRepository =>
            CoreContainer.Container.Resolve<IApiResourceRepository>();
        public AuthorizationProvider(IApiDescriptionGroupCollectionProvider apiExplorer)
        {
            _apiExplorer = apiExplorer;
        }
        public  void GetAllApi()
        {
            try
            {
                var all = _apiExplorer.ApiDescriptionGroups.Items.ToList();
                foreach (var group in all)
                {
                    foreach (var api in group.Items)
                    {
                        var apiResource = new ApiResource
                        {
                            Address = api.RelativePath,
                            MethodType = api.HttpMethod,
                            ApiTitle = api.RelativePath,
                            Created = DateTime.Now,
                            Modified = DateTime.Now,
                            CreatedBy = "System",
                            ModifiedBy = "System",
                            
                        };
                        var exists =
                             ApiResourceRepository.GetApiByAddressAndType(apiResource.Address,
                                apiResource.MethodType);

                        if (exists == null )
                            ApiResourceRepository.Save(
                            
                                apiResource

                            );

                        //var model = api.ParameterDescriptions.FirstOrDefault();
                        //if (model == null) continue;
                        //var apiModel = model.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).FirstOrDefault(x => x.Name == "Entity");
                        //var props = new List<FieldResource>();
                        //var allprop = apiModel.PropertyType
                        //    .GetProperties();
                        //foreach (var propertyInfo in allprop)
                        //{
                        //    if (!_dbContext.FieldResources.Any(x =>
                        //        x.ApiId == apiResource.Id &&
                        //        x.FullAssemblyName == $"{apiModel.PropertyType.Namespace}.{apiModel.PropertyType.Name}" &&
                        //        x.FieldName == propertyInfo.Name))
                        //        props.Add(new FieldResource

                        //        {
                        //            ApiId = apiResource.Id,
                        //            ClassName = apiModel.PropertyType.Name,
                        //            FieldName = propertyInfo.Name,
                        //            FullAssemblyName = $"{apiModel.PropertyType.Namespace}.{apiModel.PropertyType.Name}",
                        //            Type = ResourceType.Field,
                        //            Created = DateTime.Now,
                        //            CreatedBy = "System",
                        //            Modified = DateTime.Now,
                        //            ModifiedBy = "System"
                        //        });
                        //}
                        //_dbContext.FieldResources.AddRange(props);
                        //_dbContext.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }


    }

    public enum Action
    {
        Read = 1,
        Update = 2,
        Save = 3
    }


}
