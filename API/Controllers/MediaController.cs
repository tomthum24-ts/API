using API.APPLICATION.Commands.Media;
using API.APPLICATION.Queries.Media;
using API.APPLICATION.ViewModels;
using API.APPLICATION.ViewModels.Media;
using BaseCommon.Attributes;
using BaseCommon.Common.MethodResult;
using BaseCommon.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MediaController : ControllerBase
    {
        private const string UploadFile = nameof(UploadFile);
        private const string UploadFileV2 = nameof(UploadFileV2);

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMediaService _mediaService;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        public MediaController(IWebHostEnvironment webHostEnvironment, IMediaService mediaService, IConfiguration configuration, IMediator mediator)
        {
            _webHostEnvironment = webHostEnvironment;
            _mediaService = mediaService;
            _configuration = configuration;
            _mediator = mediator;
        }


        /// <summary>
        /// Get token - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route(UploadFile)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public IActionResult UploadFileAsyn(List<IFormFile> files)
        {
            var result = "";
            if (files.Count == 0)
                return BadRequest();
            string dictionaryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Media");
            foreach (var file in files)
            {
                string filepath = Path.Combine(dictionaryPath, file.FileName);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            result = dictionaryPath;
            return Ok(result);
        }
        /// <summary>
        /// Upload files.
        /// </summary>
        /// <param name="formFiles"></param>
        /// <param name="uploadFileViewModel"></param>
        /// <returns></returns>
        [HttpPost(UploadFileV2)]
        [ProducesResponseType(typeof(MethodResult<List<MediaResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> UploadFileAsync(IEnumerable<IFormFile> formFiles, [FromQuery] UploadFileViewModel uploadFileViewModel)
        {
            var uploadResult = await _mediaService.UploadFileAsync(formFiles, uploadFileViewModel).ConfigureAwait(false);
            List<FileDTO> createAttachmentFileCommands = new List<FileDTO>();
            foreach (var item in uploadResult)
            {
                createAttachmentFileCommands.Add(new FileDTO
                {
                    Name = item.Name,
                    Path = item.Path,
                    Size = item.Size
                });
            }
            var createAttachmentFileCommand = new CreateAttachmentFileCommand() { Files = createAttachmentFileCommands };
            var result = await _mediator.Send(createAttachmentFileCommand).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
