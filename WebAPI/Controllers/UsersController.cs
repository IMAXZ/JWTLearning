using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Filters;
using WebAPI.Models;
using WebAPI.Models.Auth;
using WebAPI.Tools;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(new ResponseData
                {
                    Data = JwtTools.Encoding(new Dictionary<string, object> {
                        { "loginName", model.loginName } ,
                        { "userId",123456}
                    }, JwtTools.Key)
                });
            }
            else
            {
                return Ok(new ResponseData { Code = 500, ErrorMessage = "用户名或密码错误！" });
            }
        }

        [HttpGet]
        [Route("getuser")]
        [WebAPIAuth]
        public IHttpActionResult GetUserInfo()
        {
            UserIdentity data = (UserIdentity)User.Identity; 
            return Ok(new ResponseData { Data= data });
        }
    }
}
