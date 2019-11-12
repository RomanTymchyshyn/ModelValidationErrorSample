using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ModelValidationErrorSample.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
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
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var requestBody = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            body.Position = 0;

            return requestBody;
        }
    }
}