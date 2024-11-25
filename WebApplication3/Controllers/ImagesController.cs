using homelisti_API.Models.Domain;
using HomelistiAPI.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication3;

namespace HomelistiAPI.Controllers
{
    public class ImagesController : Controller
    {
        // GET: Images
        public List<ImageDTO> Get()
        {
            var _dbContext = new HomelistiDbEntities();
            List<ImageDTO> list = WebApiApplication._mapper.Map<List<ImageDTO>>(_dbContext.Images.ToList());
            return list;
        }
    }
}