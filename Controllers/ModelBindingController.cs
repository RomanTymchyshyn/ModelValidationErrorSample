using Microsoft.AspNetCore.Mvc;

namespace ModelValidationErrorSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelBindingController : ControllerBase
    {
        [HttpPost("post")]
        public IActionResult Post(Model model)
        {
            return Ok(model);
        }
    }
}
