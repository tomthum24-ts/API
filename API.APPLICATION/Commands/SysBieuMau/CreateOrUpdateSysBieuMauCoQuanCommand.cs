using BaseCommon.Common.MethodResult;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace API.APPLICATION.Commands.SysBieuMau
{
    public class CreateOrUpdateSysBieuMauCoQuanCommand : IRequest<MethodResult<CreateOrUpdateSysBieuMauCoQuanCommandResponse>>
    {
        public int? IdBieuMau { get; set; }
        public string MaBieuMau { get; set; }
        public IFormFile FileDinhKem { get; set; }
        public bool? IsExportPDF { get; set; }
    }

    public class CreateOrUpdateSysBieuMauCoQuanCommandResponse
    {
        public int? Id { get; set; }
    }
}