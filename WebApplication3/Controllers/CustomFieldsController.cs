using homelisti_API.Models.Domain;
using HomelistiAPI.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication3;

namespace HomelistiAPI.Controllers
{
    public class CustomFieldsController : Controller
    {
        // GET: CustomFields
        public List<CustomFieldDTO> Get()
        {
            var _dbContext = new HomelistiDbEntities();
            List<CustomFieldDTO> list = WebApiApplication._mapper.Map<List<CustomFieldDTO>>(_dbContext.CustomFields.ToList());
            
            foreach(var item in list)
            {
                List<ChoiceDTO> choices = WebApiApplication._mapper.Map<List<ChoiceDTO>>(_dbContext.CustomFields_Choices
                .Where(c => c.custom_fields_id == item.id)
                .Select(c => c.Choice)
                .ToList());
                item.choices = choices.ToArray();

                List<ChoiceDTO> values = WebApiApplication._mapper.Map<List<ChoiceDTO>>(_dbContext.CustomFields_Values
                .Where(c => c.custom_fields_id == item.id)
                .Select(c => c.Valuess)
                .ToList());
                item.choices = choices.ToArray();
            }
            return list;
        }
    }
}