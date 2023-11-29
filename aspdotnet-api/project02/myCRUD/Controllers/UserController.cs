using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using myCRUD.Models;

namespace myCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<UserModel>> getAllUsers()
        {
            return Ok();
        }
    }
}