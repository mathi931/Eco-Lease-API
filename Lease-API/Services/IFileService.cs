using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcoLease_API.Services
{
    public interface IFileService
    {
        bool UploadFile(IFormFile file);

        (string fileType, byte[] fileData) GetFile(string fileName);

        bool RemoveFile(string fileName);
    }
}
