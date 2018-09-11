using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCoreWithIdentity.Services
{
    public class ErrorHandlerService
    {
        public static void AddErrorsToModelState(ModelStateDictionary modelState, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.Code, error.Description);
            }
        }
    }
}
