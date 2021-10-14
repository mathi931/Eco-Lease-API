using EcoLease_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace EcoLease_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        // GET api/Files/{img1.jpg}
        [HttpGet("{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            try
            {
                var(fileType, fileData) = _fileService.GetFile(fileName);
                return File(fileData, fileType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/Files/upload
        [HttpPost("upload")]
        public IActionResult UploadImage([Required] IFormFile formFile)
        {
            try
            {
                _fileService.UploadFile(formFile);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
