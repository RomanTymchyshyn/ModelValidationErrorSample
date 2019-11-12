using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ModelValidationErrorSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManualDeserializationController : ControllerBase
    {
        [HttpPost("post")]
        public async Task<IActionResult> Post()
        {
            var model = await DeserializeInputStream<Model>(Request.Body);
            return Ok(model);
        }

        private static async Task<T> DeserializeInputStream<T>(Stream body)
        {
            var jsonRequest = await new StreamReader(body).ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(jsonRequest);
        }
    }
}