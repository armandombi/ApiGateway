using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PaymentApi.Core.Helpers
{
    /// <summary>
    /// Extension method to handle data conversion
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Converts an object into the appropriate content to be sent by a http client
        /// </summary>
        /// <param name="obj">The data to be transferred</param>
        /// <returns></returns>
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
