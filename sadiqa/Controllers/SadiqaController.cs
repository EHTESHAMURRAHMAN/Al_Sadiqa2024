using Microsoft.AspNetCore.Mvc;
using sadiqa.Model;
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
        [HttpPost]
        [Route("addCatogrey")]
        public CatogryResponse CatogryAdd([FromBody] Catogrey authenticate)
        {
            Catogrey catogry = new Catogrey();
            var result = catogry.CatogryAdd(authenticate);
            return result;
        }
        [HttpGet]
        [Route("getCatogryList/{userid}")]
        public CatogryProfileResponse? GetCatogryList(int userid)
        {
            CatogryProfileResponse catogryProfileResponse = new CatogryProfileResponse();
            catogryProfileResponse = Catogrey.GetCatogryList(userid);
            return catogryProfileResponse;
        }
        [HttpGet]
        [Route("getCatogryDetail/{userid}")]
        public CatogryDetailResponse? GetCatogryDetails(int userid)
        {
            CatogryDetailResponse catogryDetailResponse = new CatogryDetailResponse();
            catogryDetailResponse = Catogrey.GetCatogryDetails(userid);
            return catogryDetailResponse;
        }
        [HttpPost]
        [Route("addVender")]
        public VenderResponse AddVender([FromBody] VenderList venderList)
        {
            VenderList ven = new VenderList();
            var result = ven.AddVender(venderList);
            return result;
        }
        [HttpGet]
        [Route("getVenderList/{userid}")]
        public VenderProfileResponse? GetVenderList(int userid)
        {
            VenderProfileResponse venderProfileResponse = new VenderProfileResponse();
            venderProfileResponse = VenderList.GetVenderList(userid);
            return venderProfileResponse;
        }
        [HttpGet]
        [Route("getVenderDetail/{userid}")]
        public VenderDetailResponse? GetVenderDetails(int userid)
        {
            VenderDetailResponse venderDetailResponse = new VenderDetailResponse();
            venderDetailResponse = VenderList.GetVenderDetails(userid);
            return venderDetailResponse;
        }

    }
    }

