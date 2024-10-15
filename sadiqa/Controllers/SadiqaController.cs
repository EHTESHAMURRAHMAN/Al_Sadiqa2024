using Microsoft.AspNetCore.Mvc;
using Sadiqa.Model;

namespace Sadiqa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SadiqaController : Controller
    {
        [HttpPost]
        [Route("register")]
        public ResultResponse RegisterAccount([FromBody] Authenticate authenticate)
        {
            Authenticate auth = new Authenticate();
            var result = auth.RegisterAccount(authenticate);
            return result;
        }
        [HttpGet]
        [Route("getProfile/{userid}")]
        public ProfileResponse? GetYourProfile(int userid)
        {
            ProfileResponse profileResponse = new ProfileResponse();
            profileResponse = Authenticate.GetYourProfile(userid);
            return profileResponse;
        }
        [HttpPost]
        [Route("updateProfile")]
        public Result UpdateProfile([FromBody] Authenticate authenticate)
        {
            Authenticate auth = new Authenticate();
            var result = auth.UpdateProfile(authenticate);
            return result;
        }

            [HttpPost]
            [Route("login")]
            public ResultResponse login([FromBody] Authenticate objInsertKey)
            {
                Authenticate obj = new Authenticate();
                var result = obj.login(objInsertKey);
                return result;
            }

        }
    }

