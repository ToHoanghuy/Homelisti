using homelisti_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using HomelistiAPI.Db;

namespace WebApplication3.Controllers
{
    public class LocationsController : ApiController
    {
        public List<LocationDTO> Get()
        {
            var _dbContext = new HomelistiDbEntities();
            List<LocationDTO> list = WebApiApplication._mapper.Map<List<LocationDTO>>(_dbContext.Locations.ToList());
            return list;
        }
    }
}
