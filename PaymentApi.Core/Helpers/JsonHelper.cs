using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PaymentApi.Core.Helpers
{
    public static class JsonHelper
    {
        public static ByteArrayContent ConvertToByteContent(object obj)
        {
            try
            {
                var myContent = JsonConvert.SerializeObject(obj);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return byteContent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
