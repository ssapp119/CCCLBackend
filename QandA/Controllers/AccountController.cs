using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QandA.Data;
using QandA.Models;

namespace QandA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public AccountController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpPost]
        [Route("Retrieve")]
        public AccountLookupModel RetrieveData(string token)
        {

            var accountObj = _dataRepository.GetAccount(token);

            return accountObj;
        }

        [HttpPost]
        [Route("Google")]
        public int PostGoogle(GoogleModel model)
        {
            int id = 0;

            id = _dataRepository.PostGoogleToken(model);

            return id;
        }

        [HttpPost]
        [Route("Facebook")]
        public int PostFacebook(FacebookModel model)
        {
            int id = 0;

            id = _dataRepository.PostFacebookToken(model);

            return id;
        }


    }
}
