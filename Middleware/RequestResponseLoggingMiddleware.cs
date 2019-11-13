using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ModelValidationErrorSample.Middleware
{
    public class RequestResponseLoggingMiddleware: IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var request = context.Request;

            request.EnableBuffering();

            var body = await GetRequestBody(request);

            // log body...

            await next(context);
        }

        private async Task<string> GetRequestBody(HttpRequest request)
        {
            var body = request.Body;

            body.Seek(0, SeekOrigin.Begin);

            // this works as expected, reads the whole body
            var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            
            // this does not read the whole buffer
//            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
//
//            // this count is <= buffer.Length
//            var read = await request.Body.ReadAsync(buffer, 0, buffer.Length);
//            var requestBody = Encoding.UTF8.GetString(buffer);
//
//            // try to read more
//            try
//            {
//                // this count is > 0
//                var read = await request.Body.ReadAsync(buffer, 0, buffer.Length);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//            }

            body.Seek(0, SeekOrigin.Begin);

            return requestBody;
        }
    }
}