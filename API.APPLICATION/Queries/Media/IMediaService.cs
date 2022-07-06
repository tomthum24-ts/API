using API.APPLICATION.ViewModels;
using API.APPLICATION.ViewModels.Media;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.Media
{
    public interface IMediaService
    {
        Task<List<MediaResponse>> UploadFileAsync(IEnumerable<IFormFile> formFiles, UploadFileViewModel uploadFileViewModel);
    }
    public class MediaService : IMediaService
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly MediaOptions _mediaOptions;

        public MediaService(IOptionsSnapshot<MediaOptions> snapshotOptionsAccessor, IHttpClientFactory clientFactory)
        {
            _mediaOptions = snapshotOptionsAccessor.Value;

            _clientFactory = clientFactory;
        }

        public async Task<List<MediaResponse>> UploadFileAsync(IEnumerable<IFormFile> formFiles, UploadFileViewModel uploadFileViewModel)
        {
            if (formFiles == null || !formFiles.Any()) return default;
            var listOfMediaResponse = new List<MediaResponse>();
            var fileType = new FileType();
            var client = _clientFactory.CreateClient();
            int i = 0;

            foreach (var formFile in formFiles)
            {
                var fileTypeVerifyResult = await fileType.ProcessFormFile(formFile, _mediaOptions.PermittedExtensions, _mediaOptions.SizeLimit);

                MultipartFormDataContent content = new MultipartFormDataContent();

                //StringContent strFolderName = new StringContent(_mediaOptions.FolderForWeb);

                StringContent strFolderFunction = new StringContent(uploadFileViewModel.FolderFunction);

                StringContent strFileSize = new StringContent(uploadFileViewModel.FileSize.ToString());

                ByteArrayContent byteArrayContent = new ByteArrayContent(fileTypeVerifyResult.Result);

                //content.Add(strFolderName, "pFolder");

                content.Add(strFolderFunction, "pFolderFunction");

                content.Add(strFileSize, "pFileSize");

                content.Add(byteArrayContent, "File" + i, Uri.EscapeDataString(formFile.FileName));

                var response = await client.PostAsync("Media", content);

                var resultApi = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(resultApi))
                {
                    var lstOfMediaIntegrationResponse = JsonConvert.DeserializeObject<List<MediaIntegrationResponse>>(resultApi);

                    if (lstOfMediaIntegrationResponse != null && lstOfMediaIntegrationResponse.Any())
                        listOfMediaResponse.AddRange(lstOfMediaIntegrationResponse.Select(x => new MediaResponse(x.FileName, x.DuongDan, x.DungLuong)));
                }
                i++;
            }
            return listOfMediaResponse;
        }
    }
}
