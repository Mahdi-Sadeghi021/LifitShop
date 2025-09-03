using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.SMSService
{
    public static class Generator
    {
        public static string RandomNumber()
        {
            var generator = new Random();
            
            var result = generator.Next(0, 1000000).ToString(format:"D5");

            return result;
        }
    }
}
