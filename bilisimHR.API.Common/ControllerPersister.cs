using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Helper;
using bilisimHR.Infrastructure.Dependency.CastleWindsor;
using bilisimHR.Services.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace bilisimHR.API.Common
{
    public class ControllerPersister
    {
        public void PersistControllerActions(Type controllerType)
        {
            try
            {
                Assembly assembly = Assembly.GetAssembly(controllerType);
                var controlleractionlist = assembly.GetTypes()
                .Where(type => typeof(ApiController).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Select(x => new
                {
                    Controller = x.DeclaringType.Name,
                    Action = x.Name,
                    CustomAttributes = x.GetCustomAttributes(false)
                });

                var controllerActionsService = WebApiInstaller.Resolve<IControllerActionsService>();
                
                List<ControllerActionsModel> listOfControllerActions = controllerActionsService.GetAllAsync().Result.ToList();


                foreach (var controller in controlleractionlist)
                {
                    string description = controller.CustomAttributes.OfType<DescriptionAttribute>().FirstOrDefault() != null 
                            ? controller.CustomAttributes.OfType<DescriptionAttribute>().FirstOrDefault().Description : string.Empty;

                    OperationType type;

                    if (controller.CustomAttributes.OfType<HttpGetAttribute>().FirstOrDefault() != null)
                        type = OperationType.Read;
                    else if (controller.CustomAttributes.OfType<HttpPostAttribute>().FirstOrDefault() != null)
                        type = OperationType.Create;
                    else if (controller.CustomAttributes.OfType<HttpPatchAttribute>().FirstOrDefault() != null)
                        type = OperationType.Update;
                    else if (controller.CustomAttributes.OfType<HttpDeleteAttribute>().FirstOrDefault() != null)
                        type = OperationType.Delete;
                    else
                        type = OperationType.Operation;


                    List<ControllerActionsModel> filteredList = listOfControllerActions.Where(s => s.Controller == controller.Controller && s.Action == controller.Action).ToList();
                    
                    if (filteredList.Count == 0)
                    {
                        ControllerActionsModel model = new ControllerActionsModel()
                        {
                            Controller = controller.Controller,
                            Action = controller.Action,
                            Description = description,
                            InsertedBy = 1,
                            InsertedDate = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedDate = DateTime.Now,
                            OperationType = type
                        };
                        controllerActionsService.InsertAsync(model);
                    }    
                    else if (filteredList.Count == 1)
                    {
                        filteredList.FirstOrDefault().UpdatedDate = DateTime.Now;
                        controllerActionsService.UpdateAsync(filteredList.FirstOrDefault());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
