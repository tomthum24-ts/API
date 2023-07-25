using API.APPLICATION.ViewModels.Media;
using BaseCommon.Common.MethodResult;
using MediatR;
using System.Collections.Generic;

namespace API.APPLICATION.Commands.Media
{
    public class CreateAttachmentFileCommand : IRequest<MethodResult<CreateAttachmentFileResponse>>
    {
        public List<FileDTO> Files { get; set; }
    }

    public class FileDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public string Path { get; set; }
    }

    public class CreateAttachmentFileResponse
    {
        public List<MediaResponse> Files { get; set; }

        public CreateAttachmentFileResponse(List<MediaResponse> files)
        {
            Files = files;
        }
    }
}