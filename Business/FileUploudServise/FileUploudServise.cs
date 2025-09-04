using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Business.FileUploudServise
{
    public class FileUploudServise : IfileUploudServise
    {
        private readonly string _storagePath;
        public FileUploudServise(IConfiguration configuration)
        {
            _storagePath = configuration["FileUploud:StoragePath"];
        }
        public async Task<string> UploudFileAsync(IFormFile file)
        {
            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty pr not provided");

            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var fuulPath = Path.Combine(_storagePath, fileName);
            using (var stream = new FileStream(fuulPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
    }
    }

