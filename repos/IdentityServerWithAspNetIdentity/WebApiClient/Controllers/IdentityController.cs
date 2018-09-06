using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApiClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        // GET api/identity
        [HttpGet]
        public IActionResult Get()
        {
            var userClaims = User
                .Claims
                .Select(c => new {c.Type, c.Value});

            var jsonResult = new JsonResult(userClaims);

            return jsonResult;
        }
    }
}
