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
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var requestBody = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);

            return requestBody;
        }
    }
}