using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ECom.Core.Services
{
    public  interface IImageManegmentService
    {
        Task <List<string>> AddImageAsync(IFormFileCollection file , string src);
        //Task AddImageAsync(IFormFileCollection images);
        Task DeleteImageAsync(string src);
    }
}
