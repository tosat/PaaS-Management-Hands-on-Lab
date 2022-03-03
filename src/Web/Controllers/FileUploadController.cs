using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;

namespace Web.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly ILogger<FileUploadController> _logger;
        private readonly IConfiguration _configuration;

        public FileUploadController(ILogger<FileUploadController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveImages(List<IFormFile> inputFiles)
        {
            var connectionString = _configuration.GetValue<string>("BlobConnectionString");
            var accountName = _configuration.GetValue<string>("StorageAccountName");
            var containerName = _configuration.GetValue<string>("ContainerName");

            List<string> files = new List<string>();

            BlobContainerClient containerClient;

            if (string.IsNullOrEmpty(connectionString))
            {
                var containerEndpoint = string.Format("https://{0}.blob.core.windows.net/{1}", accountName, containerName);

                containerClient = new BlobContainerClient(new Uri(containerEndpoint), new DefaultAzureCredential());
            }
            else
            {
                containerClient = new BlobContainerClient(connectionString, containerName);
            }

            await containerClient.CreateIfNotExistsAsync();

            foreach (var file in inputFiles)
            {
                if (file.Length > 0)
                {
                    BlobClient blobClient = containerClient.GetBlobClient(file.FileName);

                    var filePath = Path.GetTempFileName();
                    var memoryStream = new MemoryStream();

                    using (var stream = new FileStream(filePath, FileMode.Create))
                        file.CopyTo(stream);

                    using (var fileStream = new FileStream(filePath, FileMode.Open))
                        fileStream.CopyTo(memoryStream);
                    
                    memoryStream.Position = 0;

                    await blobClient.UploadAsync(memoryStream, overwrite: false);

                    BlobProperties properties = await blobClient.GetPropertiesAsync();

                    BlobHttpHeaders headers = new BlobHttpHeaders
                    {
                        ContentType = file.ContentType
                    };

                    await blobClient.SetHttpHeadersAsync(headers);

                    files.Add($"{file.FileName} (ContentType: {file.ContentType} - {file.Length} bytes)");
                }
            }

            ViewBag.Files = files;
            return View();
        }
    }
}