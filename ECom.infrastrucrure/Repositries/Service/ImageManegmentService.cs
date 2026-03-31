using ECom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace ECom.infrastrucrure.Repositries.Service
{
    public class ImageManegmentService : IImageManegmentService
    {
        //private readonly IFileProvider fileProvider;

        public ImageManegmentService()
        {
            //this.fileProvider = fileProvider;
        }

        async Task<List<string>> IImageManegmentService.AddImageAsync(IFormFileCollection files, string src)
        {
            var savedFiles = new List<string>();
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), src);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName); // لمنع تكرار الأسماء
                    var filePath = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    savedFiles.Add(fileName);
                }
            }

            return savedFiles;
        }

        async Task IImageManegmentService.DeleteImageAsync(string src)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), src);

            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}
