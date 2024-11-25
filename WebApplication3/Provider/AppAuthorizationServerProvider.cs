using HomelistiAPI.Controllers;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using HomelistiAPI.Controllers;

namespace HomelistiAPI.Provider
{
    public class AppAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using(UsersController controller = new UsersController())
            {
                var user = controller.IsValidUser(context.UserName, context.Password);

                if(user == null)
                {
                    context.SetError("Invalid_grant", "Username or password is incorrect!");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user[0].username));

                context.Validated(identity);
            }
        }
    }
}