using CharlesAssesment.Security;
using CharlesAssesment.Service;
using Microsoft.AspNetCore.Mvc;

namespace CharlesAssesment.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        /// <summary>
        /// Gets product list
        /// </summary>DD
        /// <returns code="200">Returns a list of products</returns>
        /// <returns code="500">Something is wrong</returns>
        [HttpGet]
        [ApiVersion("1.0")]
        [JWTAuthorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _productService.GetProductsAsync());
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message + "-" + ex.StackTrace);
                //return StatusCode(500, "Something is wrong");
            }
        }
    }
}