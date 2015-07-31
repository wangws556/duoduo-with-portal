using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace YoYoStudio.Common.Web
{
	public class TokenIdentity : GenericIdentity
	{
		public TokenIdentity(string name, string authenticationType, string token)
			: base(name, authenticationType)
		{
			Token = token;
		}
		public string Token { get; private set; }
	}

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CookieAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            var isAuthenticated = base.AuthorizeCore(httpContext);
            if (isAuthenticated)
            {
                string cookieName = FormsAuthentication.FormsCookieName;

                if (!httpContext.User.Identity.IsAuthenticated ||
                    httpContext.Request.Cookies == null ||
                    httpContext.Request.Cookies[cookieName] == null)
                {
                    return false;
                }
                
                var authCookie = httpContext.Request.Cookies[cookieName];
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                // This is where you can read the userData part of the authentication
                // cookie and fetch the token
                string webServiceToken = authTicket.UserData;
				if (string.IsNullOrEmpty(webServiceToken))
				{
					return false;
				}
                GenericIdentity identity = new TokenIdentity(httpContext.User.Identity.Name, httpContext.User.Identity.AuthenticationType, webServiceToken);

                IPrincipal userPrincipal = new GenericPrincipal(identity, null);

                // Inject the custom principal in the httpcontext
                httpContext.User = userPrincipal;
            }
            return isAuthenticated;

        }
    }
}
