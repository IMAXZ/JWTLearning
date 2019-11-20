using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Models;
using WebAPI.Tools;

namespace WebAPI.Controllers
{
    //跨域支持
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        public string Get()
        {
            return "Hello World!";
        }
        [HttpPost]
        [Route("Login")]
        public string Login(UserViewModel model)
        {
            if (model.loginName == "zhangsan" && model.loginPwd == "123456")
            {
               return JwtTools.Encoding(new Dictionary<string, object>() {
                    { "loginName",model.loginName}
                }, JwtTools.Key);
            }
            throw new Exception("账号密码有误！");
        }

        [HttpGet]
        [Route("getInfo")]
        public string GetUserInfo()
        {
            var userName = JwtTools.ValideLogined(ControllerContext.Request.Headers);
            return "用户名称：" + userName;
        }
    }
}
