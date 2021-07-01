using Microsoft.AspNetCore.Mvc;
using WebApplicationCustomerInvitaion.Classes;

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
            CustomerService customerService = new CustomerService();
            var result = customerService.GetList();

            if (result != null)
                return new JsonResult(result);
            else
                return NotFound();
        }
    }
}
