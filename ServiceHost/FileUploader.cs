using _0_Framework.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file , string path)
        {
            if (file == null) return "";

            var directoryPath = $"{_webHostEnvironment.WebRootPath}//ProductPictures//{path}";

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            //if(file.Length >= 2 * 1024 * 1024)
            //{
            //    throw new Exception();
            //} ------------------------------>چون ما میخواهیم از این کلاس همه جا و برای آپلود هر چیزی استفاده کنیم
            //-------------------------------->یعنی این کلاس یک کلاس جنریک باشد پس این روش مناسب نیست

            var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
            var filePath = $"{directoryPath}//{fileName}";

            //using (var output = System.IO.File.Create(path))
            //{
            //    file.CopyToAsync(output);
            //}  ---------------------------------------> or the same :
            using var output = File.Create(filePath);
            file.CopyTo(output);
            return $"{path}/{fileName}";
        }
    }
}
