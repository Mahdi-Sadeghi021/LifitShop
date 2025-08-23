using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LifitShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly string _storagePath;
        public FileController(IConfiguration configuration)
        {
            _storagePath = configuration["FileUploud:StoragePath"];
        }

        [HttpGet("{GetFile}")]

        public IActionResult Downloadfile(string fileName)
        {
            var fullPath = Path.Combine(_storagePath, fileName);
            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound("File Not Found");
            }
            var FileBytes = System.IO.File.ReadAllBytes(fullPath);
            var connectType = "application/octet-stream";
            return PhysicalFile(fullPath, connectType, fileName);
        }
    }
}
