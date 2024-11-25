using homelisti_API.Models.Domain;
using HomelistiAPI.Db;
using HomelistiAPI.Models.Domain;
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
    public class CustomField_ChoiceController : Controller
    {
        public List<CustomField_ChoiceDTO> Get()
        {
            var _dbContext = new HomelistiDbEntities();
            List<CustomField_ChoiceDTO> list = WebApiApplication._mapper.Map<List<CustomField_ChoiceDTO>>(_dbContext.CustomFields_Choices.ToList());
            return list;
        }
    }
}
