using HomelistiAPI.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApplication3;

[assembly: OwinStartup(typeof(HomelistiAPI.Startup))]

namespace HomelistiAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var myProvider = new AppAuthorizationServerProvider();

            var options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/login/authorize"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = myProvider
            };

            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

        }
    }
}
