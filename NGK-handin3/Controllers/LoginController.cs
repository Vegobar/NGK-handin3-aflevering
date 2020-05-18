using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NGK_handin3.Model;
using NGK_handin3.Token;

namespace NGK_handin3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        [HttpPost]
        public IActionResult login(User myUser)
        {
            if (myUser.username == myUser.password)
            {
                TokenManager myToken = new TokenManager();
                return new ObjectResult(myToken.GenerateToken(myUser.username));
            }

            return BadRequest();
        }
    }
}