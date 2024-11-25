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
    public class CategoriesController : ApiController
    {
        public List<CategoryDTO> Get()
        {
            var _dbContext = new HomelistiDbEntities();
            List<CategoryDTO> list = WebApiApplication._mapper.Map<List<CategoryDTO>>(_dbContext.Categories.ToList());
            return list;
        }
    }
}
