using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace _0_Framework.Application
{
    public class MaxFileSizeAttribute : ValidationAttribute , IClientModelValidator
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }


        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null) return true;

            //if (file.Length > _maxFileSize)
            //    return false;
            //return true; ----------------------> This is same as : 

            return file.Length <= _maxFileSize;
        }


        public void AddValidation(ClientModelValidationContext context)  //این متد به ما کمک میکند که ولیدیشن سمت کلاینت رو
                                                                         //داشته باشیم و قابل ذکر است که این متد از
                                                                         //اینترفیسی که در بالا اضافه کردیم پیاده سازی شده است
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-maxFileSize", ErrorMessage);
        }//حالا باید بریم سمت جی کوئری رو هم کانفیگ کنیم
    }
}
