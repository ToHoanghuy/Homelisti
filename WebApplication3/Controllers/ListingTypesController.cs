using homelisti_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using HomelistiAPI.Db;
using AutoMapper;

namespace WebApplication3.Controllers
{

    public class ListingTypesController : ApiController
    {
        // GET api/values
        public List<ListingTypeDTO> Get()
        {
            var _dbContext = new HomelistiDbEntities();
            List<ListingTypeDTO> list = WebApiApplication._mapper.Map<List<ListingTypeDTO>>(_dbContext.ListingTypes.ToList());
            return list;
        }        
    }
}
