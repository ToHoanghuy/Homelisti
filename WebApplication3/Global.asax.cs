using AutoMapper;
using WebApplication3.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication3
{

    public class WebApiApplication : System.Web.HttpApplication
    {
        internal static MapperConfiguration MapperConfiguration { get; private set; }
        public static IMapper _mapper;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MapperConfiguration = AutoMapperConfig.MapperConfiguration();
            _mapper = AutoMapperConfig.MapperConfiguration().CreateMapper();
        }
    }
}
