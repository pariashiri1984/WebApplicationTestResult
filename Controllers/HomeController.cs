using Microsoft.AspNetCore.Mvc;


namespace WebApplicationTestResult.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
     

        public HomeController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
          
        }
    }
}
