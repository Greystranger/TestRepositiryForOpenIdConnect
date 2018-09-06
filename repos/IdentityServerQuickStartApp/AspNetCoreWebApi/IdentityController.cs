using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApi
{
    [Route("identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        public IActionResult Get()
        {
            var jsonResult = new JsonResult(
                User
                    .Claims
                    .Select(c => 
                        new
                        {
                            c.Type,
                            c.Value
                        }));

            return jsonResult;
        }
    }
}
