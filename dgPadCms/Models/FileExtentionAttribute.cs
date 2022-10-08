using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace dgPadCms.Models
{
    public class FileExtentionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var context = (CmsShoppingContext)validationContext.GetService(typeof(CmsShoppingContext));
            // if I need to use the database to validate

            var file = value as IFormFile;

            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);

                string[] extentions = { "jpg", "png" };
                bool result = extentions.Any(x => extention.EndsWith(x));

                if (!result) return new ValidationResult(GetErrorMessage());

            }
                return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "Allowed extentions are jpg and png";
        }
    }
}
