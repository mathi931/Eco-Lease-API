using EcoLease_API.Services;
using EcoLease_API.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
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

        FileValidator validator = new FileValidator();
        

        // GET api/Files/{img1.jpg}
        [HttpGet("{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            FluentValidation.Results.ValidationResult validation = validator.Validate(fileName);
            if (!validation.IsValid)
            {
                return BadRequest("Wrong Parameters!");
            }

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
        public IActionResult UploadImage(IFormFile file)
        {
            FluentValidation.Results.ValidationResult validation = validator.Validate(file.FileName);
            if (!validation.IsValid)
            {
                return BadRequest("Wrong Parameters!");
            }

            try
            {
                if (_fileService.UploadFile(file))
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("{fileName}")]
        public IActionResult RemoveFile(string fileName)
        {
            FluentValidation.Results.ValidationResult validation = validator.Validate(fileName);
            if (!validation.IsValid)
            {
                return BadRequest("Wrong Parameters!");
            }

            try
            {
                if (_fileService.RemoveFile(fileName))
                {
                    return Ok();
                }
                return BadRequest("File does not exist!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
