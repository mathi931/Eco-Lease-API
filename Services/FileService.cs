using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EcoLease_API.Services
{
    public class FileService : IFileService
    {
        private IWebHostEnvironment _hostingEnvironment;

        public FileService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public bool UploadFile(IFormFile file)
        {
            bool uploaded = true;

            //declare the main root
            var path = _hostingEnvironment.ContentRootPath;

            if (file.FileName.Contains("jpg") && file.Length > 0)
            {
                //if the file is jpg => goes to images
                path = Path.Combine(path, "App_Data/Images");

            }
            else if (file.FileName.Contains("pdf") && file.Length > 0)
            {
                //if the file is pdf => goes to Agreements
                path = Path.Combine(path, "App_Data/Agreements");

            }
            else
            {
                uploaded = false;
                //if there is no file or its not pdf or jpg, returns
            }

            //if the directory does not exist creates one
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //creates the full path with the filename to the target directory
            var filePath = Path.Combine(path, file.FileName);

            //copies the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return uploaded;
        }

        public (string fileType, byte[] fileData) GetFile(string fileName)
        {
            //Create HTTP Response.
            var response =  new HttpResponseMessage((HttpStatusCode.OK));

            var subPath = "";
            var contentType = "";

            //checks if the file is jpg
            if (fileName.Contains("jpg"))
            {
                subPath = "Images/";
                contentType = "image/jpeg";
            }
            //or pdf
            else if (fileName.Contains("pdf"))
            {
                subPath = "Agreements/";
                contentType = "application/pdf";
            }
            else
            {
                throw new Exception("Wrong file!");
            }

            //creates the path depends on file
            var filePath = $"{Path.Combine(_hostingEnvironment.ContentRootPath, $"App_Data/{subPath}")}{fileName}";

            if (!File.Exists(filePath))
            {
                // Throw 404(Not Found) exception if File not found.
            response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = $"File not found: {fileName} .";
                throw new HttpResponseException(response);
            }

            //Read the File into a Byte Array.
            byte[] bytes = File.ReadAllBytes(filePath);

            //returns the type and the file in byte array
            return (contentType, bytes);
        }

        public bool RemoveFile(string fileName)
        {
            var path = "";

            //if its an image
            if (fileName.Contains("jpg"))
            {
                path = Path.Combine(_hostingEnvironment.ContentRootPath, "App_Data", "Images", fileName);
            }
            //if its a pdf
            else if (fileName.Contains("pdf"))
            {
                path = Path.Combine(_hostingEnvironment.ContentRootPath, "App_Data", "Agreements", fileName);
            }

            //if the file exists delete 
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
