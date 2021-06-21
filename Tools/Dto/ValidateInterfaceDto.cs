using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tools.Dto
{
    public class ValidateInterfaceDto : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var temp = from objType in validationContext.ObjectType.GetInterfaces()
                       from pro in objType.GetProperties()
                       from attr in pro.GetCustomAttributes(true).Cast<ValidationAttribute>()
                       where attr != null
                       select new { pro = pro, attr = attr };

            foreach (var t in temp)
            {
                validationContext.DisplayName = t.pro.Name;
                var error = t.attr.GetValidationResult(t.pro.GetValue(validationContext.ObjectInstance), validationContext);
                if (error != null)
                {
                    yield return error;
                }
            }
        }
    }
}
