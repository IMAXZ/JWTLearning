using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebAPI.Tools;

namespace WebAPI.Filters
{
    public class WebAPIAuthAttribute : Attribute, IAuthorizationFilter
    {
        public bool AllowMultiple {get;}

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (actionContext.Request.Headers.TryGetValues("token",out IEnumerable<string>headers))
            {
                var loginName = JwtTools.Decode(headers.FirstOrDefault(), JwtTools.Key)["loginName"].ToString();
                var userId = JwtTools.Decode(headers.FirstOrDefault(), JwtTools.Key)["userId"].ToString();
                (actionContext.ControllerContext.Controller as ApiController).User = new ApplicationUser(loginName,int.Parse(userId));
                return await continuation();
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
        }
    }
}