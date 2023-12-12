using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MicroService.Web.Utility
{
    public class AllowExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var extenstion = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extenstion.ToLower()))
                {
                    return new ValidationResult("This photo extenstion is not allowed");
                }
            }

            return ValidationResult.Success;
        }
    }
}
