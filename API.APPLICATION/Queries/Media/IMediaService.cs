using API.APPLICATION.ViewModels;
using API.APPLICATION.ViewModels.Media;
using BaseCommon.Common.AttachFile;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.Media
{
    public interface IMediaService
    {
        Task<MediaResponse> UploadFileAsync(IFormFile file, UploadFileViewModel uploadFileViewModel);
    }

    public class MediaService : IMediaService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _iconfiguration;

        public MediaService(IWebHostEnvironment webHostEnvironment, IConfiguration iconfiguration)
        {
            _webHostEnvironment = webHostEnvironment;
            _iconfiguration = iconfiguration;
        }

        public async Task<MediaResponse> UploadFileAsync(IFormFile file, UploadFileViewModel uploadFileViewModel)
        {
            
            string dayString = string.Format("{0:yyyy/MM/dd}", DateTime.Now);
            string folder = Path.Combine(_webHostEnvironment.ContentRootPath, $"{_iconfiguration["MediaLink:Media"]}/{dayString}/{uploadFileViewModel.FolderFunction}");
            string patch = $"{_iconfiguration["MediaLink:Media"]}/{dayString}/{uploadFileViewModel.FolderFunction}";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var data = new MediaResponse(file.FileName, patch, file.Length);
            string filepath = Path.Combine(folder, file.FileName);
            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return data;
        }
    }
}