using homelisti_API.Models.Domain;
using Swashbuckle.Swagger;
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
    public class ContactsController : ApiController
    {
        public List<ContactDTO> Get()
        {
            var _dbContext = new HomelistiDbEntities();
            List<ContactDTO> list = WebApiApplication._mapper.Map<List<ContactDTO>>(_dbContext.Contacts.ToList());
            return list;
        }
    }
}
