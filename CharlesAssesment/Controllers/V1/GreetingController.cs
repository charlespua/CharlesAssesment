using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharlesAssesment.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class GreetingController : ControllerBase
    {
        private readonly ILogger<GreetingController> _logger;

        public GreetingController(ILogger<GreetingController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Saying Hello V1
        /// </summary>
        /// <returns code="200">Returns Hello</returns>
        /// <returns code="500">Something is wrong</returns>
        [AllowAnonymous]
        [HttpGet]
        [MapToApiVersion("1.0")]
        public IActionResult SayHello()
        {
            return Ok($"Hello World!");
        }
    }
}