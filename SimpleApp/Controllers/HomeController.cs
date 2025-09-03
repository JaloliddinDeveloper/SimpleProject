using Microsoft.AspNetCore.Mvc;

namespace SimpleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Hello Simple App";
        }
    }
}
