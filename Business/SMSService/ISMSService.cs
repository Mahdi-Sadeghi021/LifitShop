using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.SMSService
{
    public interface ISMSService
    {
        Task SendPublicSMS(string phoneNumber, string message);
        Task SendLookupSMS(string phoneNumber, string templateName, string token1, string? token2 = "", string? token3 = "");
    }
}
