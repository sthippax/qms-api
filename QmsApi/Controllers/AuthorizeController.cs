using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QmsDAL.DataAccess;
using QmsDAL.Models;

namespace QmsApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AuthorizeController : Controller
    {
        #region [Code Owner : Chenthilkumaran (11-04-2023)]
        [HttpPost("WhoAmI")]

        public async Task<IActionResult> WhoAmI()
        {
            try
            {
                string UserName = "";
                if (User.Identity.Name != null)
                {
                    UserName = new HttpMessage().GetLogin(User.Identity.Name);

                    ContactList result = new AuthorizeDAL().GetUserAccount(UserName);

                    //Logger.Info("[WhoAmI] {** User:" + UserName + "**}{** IP:" + _IPAddress + "**}{** response : " + new HttpMessage().GetSeializer(response) + "**");
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new HttpMessage().GetOops());

                }

            }
            catch (Exception ex)
            {
                return BadRequest(new HttpMessage().GetOops());
            }
        }
        #endregion

    }
}
