using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

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
            var containerName = _configuration.GetValue<string>("ContainerName");

            List<string> files = new List<string>();

            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(connectionString, out storageAccount))
            {
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);

                long size = inputFiles.Sum(f => f.Length);

                foreach(var file in inputFiles)
                {
                    if (file.Length > 0)
                    {
                        var filePath = Path.GetTempFileName();
                        var memoryStream = new MemoryStream();

                        using (var stream = new FileStream(filePath, FileMode.Create))
                            file.CopyTo(stream);

                        using (var fileStream = new FileStream(filePath, FileMode.Open))
                            fileStream.CopyTo(memoryStream);

                        memoryStream.Position = 0;

                        CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(file.FileName);
                        var requestOption = new BlobRequestOptions()
                        {
                            ParallelOperationThreadCount = 4
                        };

                        await blockBlob.UploadFromStreamAsync(memoryStream, AccessCondition.GenerateIfNotExistsCondition(), requestOption, null);

                        blockBlob.Properties.ContentType = file.ContentType;

                        await blockBlob.SetPropertiesAsync();

                        files.Add($"{file.FileName} (ContentType: {file.ContentType}) -{file.Length} bytes");
                    }
                }
            }
            else
            {
                _logger.LogInformation($"A connection string has not been defined in the system environment variables.");

                throw new Exception("A connection string has not been defined in the system environment variables.");
            }

            ViewBag.Files = files;
            return View();
        }
    }
}