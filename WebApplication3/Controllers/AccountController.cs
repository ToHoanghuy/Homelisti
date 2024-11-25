using homelisti_API.Models.Domain;
using HomelistiAPI.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication3;

namespace HomelistiAPI.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        public List<AccountDTO> Get()
        {
            var dbContext = new HomelistiDbEntities();
            List<AccountDTO> list = WebApiApplication._mapper.Map<List<AccountDTO>>(dbContext.Accounts.ToList());
            return list;
        }
    }
}
