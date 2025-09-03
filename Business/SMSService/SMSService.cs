using DataAccess.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.SMSService
{
    public class SMSService : ISMSService
    {
        private readonly KavenegarInfoViewModel _kavenegarInfo;
        public SMSService(IOptions<KavenegarInfoViewModel> kavenegarInfo)
        {
            _kavenegarInfo = kavenegarInfo.Value;
        }

        public async Task SendPublicSMS(string phoneNumber, string message)
        {
            try
            {
                var api = new Kavenegar.KavenegarApi(_kavenegarInfo.ApiKey);

                var result = api.Send(_kavenegarInfo.Sender, receptor: phoneNumber, message);
            }
            catch (Kavenegar.Core.Exceptions.ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                throw new Exception(ex.Message);
            }
            catch (Kavenegar.Core.Exceptions.HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                throw new Exception(ex.Message);
            }

        }
        public async Task SendLookupSMS(string phoneNumber, string templateName, string token1, string? token2 = "", string? token3 = "")
        {

            try
            {
                var api = new Kavenegar.KavenegarApi(_kavenegarInfo.ApiKey);

                var result = api.VerifyLookup(phoneNumber, token1, token2, token3, templateName);
            }
            catch (Kavenegar.Core.Exceptions.ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                throw new Exception(ex.Message);
            }
            catch (Kavenegar.Core.Exceptions.HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                throw new Exception(ex.Message);
            }

        }
    }
}
