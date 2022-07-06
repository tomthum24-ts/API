using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.Media
{
    public class FileType
    {
        public FileType()
        {
        }

        protected Dictionary<string, List<byte[]>> _fileSignature = new Dictionary<string, List<byte[]>>
        {
            {".mp3", new List<byte[]>{new byte[] { 0x49, 0x44, 0x33 } }},
            {".pdf", new List<byte[]>{new byte[]{ 0x25, 0x50, 0x44, 0x46 }}},
            {".png", new List<byte[]>{new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }}},
            {".jpg", new List<byte[]>
            {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 }
            }},
            { ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 }}},
            { ".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                }
            },
            { ".zip", new List<byte[]>
                {
                    new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                    new byte[] { 0x50, 0x4B, 0x4C, 0x49, 0x54, 0x45 },
                    new byte[] { 0x50, 0x4B, 0x53, 0x70, 0x58 },
                    new byte[] { 0x50, 0x4B, 0x05, 0x06 },
                    new byte[] { 0x50, 0x4B, 0x07, 0x08 },
                    new byte[] { 0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70 },
                }
            },
            {".ico", new List<byte[]>{new byte[]{ 0x00, 0x00, 0x01, 0x00 }}},
        };

        public void AddSignatures(string key, List<byte[]> fileSignature)
        {
            _fileSignature.TryAdd(key, fileSignature);
        }

        public FileTypeVerifyResult VerifyFileExtensionAndSignature(string fileName, Stream data, string[] permittedExtensions)
        {
            var fileTypeVerifyResult = new FileTypeVerifyResult();
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(fileName) || data == null || data.Length == 0)
            {
                fileTypeVerifyResult.IsSucceed = false;
                fileTypeVerifyResult.Error = EFileError.InValid;
                return fileTypeVerifyResult;
            }
            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                fileTypeVerifyResult.IsSucceed = false;
                fileTypeVerifyResult.Error = EFileError.InValidExtension;
                return fileTypeVerifyResult;
            }

            //data.Position = 0;
            //using var reader = new BinaryReader(data);
            //var signatures = _fileSignature[ext];
            //var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));
            //if (!signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature)))
            //{
            //    fileTypeVerifyResult.IsSucceed = false;
            //    fileTypeVerifyResult.Error = EFileError.InValidSignature;
            //    return fileTypeVerifyResult;
            //}
            return fileTypeVerifyResult;
        }

        public async Task<FileTypeVerifyResult> ProcessFormFile(IFormFile formFile, string[] permittedExtensions, long sizeLimit)
        {
            var fileTypeVerifyResult = new FileTypeVerifyResult();
            if (formFile.Length == 0)
            {
                fileTypeVerifyResult.IsSucceed = false;
                fileTypeVerifyResult.Error = EFileError.InValid;
                return fileTypeVerifyResult;
            }
            if (formFile.Length > sizeLimit)
            {
                fileTypeVerifyResult.IsSucceed = false;
                fileTypeVerifyResult.Error = EFileError.InValidSize;
                return fileTypeVerifyResult;
            }
            try
            {
                await using var memoryStream = new MemoryStream();
                await formFile.CopyToAsync(memoryStream);
                if (memoryStream.Length == 0) { return fileTypeVerifyResult; }
                fileTypeVerifyResult = VerifyFileExtensionAndSignature(formFile.FileName, memoryStream, permittedExtensions);
                if (fileTypeVerifyResult.IsSucceed) fileTypeVerifyResult.Result = memoryStream.ToArray();
                return fileTypeVerifyResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public async Task<FileTypeVerifyResult> ProcessStreamedFile(MultipartSection section, ContentDispositionHeaderValue contentDisposition, string[] permittedExtensions, long sizeLimit)
        //{
        //    var fileTypeVerifyResult = new FileTypeVerifyResult();
        //    try
        //    {
        //        await using var memoryStream = new MemoryStream();
        //        await section.Body.CopyToAsync(memoryStream);
        //        if (memoryStream.Length == 0)
        //        {
        //            fileTypeVerifyResult.IsSucceed = false;
        //            fileTypeVerifyResult.Error = EFileError.InValid;
        //            return fileTypeVerifyResult;
        //        }
        //        if (memoryStream.Length > sizeLimit)
        //        {
        //            fileTypeVerifyResult.IsSucceed = false;
        //            fileTypeVerifyResult.Error = EFileError.InValid;
        //            return fileTypeVerifyResult;
        //        }
        //        fileTypeVerifyResult = VerifyFileExtensionAndSignature(contentDisposition.FileName.Value, memoryStream, permittedExtensions);
        //        if (fileTypeVerifyResult.IsSucceed) fileTypeVerifyResult.Result = memoryStream.ToArray();
        //        return fileTypeVerifyResult;
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //}
    }

    public class FileTypeVerifyResult
    {
        public FileTypeVerifyResult()
        {
            Result = new byte[0];
            IsSucceed = true;
            Error = null;
        }

        public bool IsSucceed { get; set; }
        public byte[] Result { get; set; }
        public EFileError? Error { get; set; }
    }

    public enum EFileError
    {
        InValidSignature,
        InValidExtension,
        InValidSize,
        InValid
    }
}
