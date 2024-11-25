using homelisti_API.Models.Domain;
using HomelistiAPI.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication3;

namespace HomelistiAPI.Controllers
{
    public class ChoicesController : Controller
    {
        public List<ChoiceDTO> Get()
        {
            var _dbContext = new HomelistiDbEntities();
            List<ChoiceDTO> list = WebApiApplication._mapper.Map<List<ChoiceDTO>>(_dbContext.Choices.ToList());
            return list;
        }
    }
}
