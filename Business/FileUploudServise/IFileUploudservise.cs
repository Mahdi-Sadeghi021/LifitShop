using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Business.FileUploudServise
{
    public interface IFileUploudservise
    {
        Task<string> UploudFileAsync(IFormFile file);
    }
}
