using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharlesAssesment.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class GreetingController : ControllerBase
    {
        private readonly ILogger<GreetingController> _logger;

        public GreetingController(ILogger<GreetingController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Saying Hello V2
        /// </summary>
        /// <returns code="200">Returns Hello V2</returns>
        /// <returns code="500">Something is wrong</returns>
        [AllowAnonymous]
        [HttpGet]
        [MapToApiVersion("2.0")]
        public IActionResult SayHello()
        {
            return Ok($"Hello World V2!");
        }
    }
}