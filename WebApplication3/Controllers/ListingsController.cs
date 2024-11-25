using homelisti_API.Models.Domain;
using HomelistiAPI.Db;
using Microsoft.IdentityModel.Tokens;
using Ninject.Activation;
using Ninject.Planning.Targets;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;
using WebApplication3;

namespace HomelistiAPI.Controllers
{
    public class ListingsController : ApiController
    {
        [HttpGet]
        public List<ListingDTO> Get()
        {
            var _dbContext = new HomelistiDbEntities();
            List<ListingDTO> list = WebApiApplication._mapper.Map<List<ListingDTO>>(_dbContext.Listings.ToList());
            foreach (var item in list)
            {
                List<CustomFieldDTO> custom_fields = WebApiApplication._mapper.Map<List<CustomFieldDTO>>(_dbContext.Listings_CustomFields
                .Where(c => c.listing_id == item.listing_id)
                .Select(c => c.CustomField)
                .ToList());

                foreach (var temp in custom_fields)
                {
                    //List<ChoiceDTO> choices = WebApiApplication._mapper.Map<List<ChoiceDTO>>(_dbContext.CustomFields_Choices
                    //.Where(c => c.custom_fields_id == temp.id)
                    //.Select(c => c.Choice)
                    //.ToList());
                    //temp.choices = choices.ToArray();

                    //List<ValueeDTO> values = WebApiApplication._mapper.Map<List<ValueeDTO>>(_dbContext.CustomFields_Values
                    //.Where(c => c.custom_fields_id == temp.id && c.listing_id == item.listing_id)
                    //.Select(c => c.Valuess)
                    //.ToList());
                    //temp.value = values.ToArray();

                    if (temp == null) continue;

                    // Lấy danh sách choices
                    var customFieldChoices = _dbContext.CustomFields_Choices
                        .Where(c => c.custom_fields_id == temp.id)
                        .Select(c => c.Choice)
                        .ToList();

                    List<ChoiceDTO> choices = customFieldChoices != null
                        ? WebApiApplication._mapper.Map<List<ChoiceDTO>>(customFieldChoices)
                        : new List<ChoiceDTO>();

                    temp.choices = choices.ToArray();

                    // Lấy danh sách values
                    var customFieldValues = _dbContext.CustomFields_Values
                        .Where(c => c.custom_fields_id == temp.id && c.listing_id == item.listing_id)
                        .Select(c => c.Valuess)
                        .ToList();

                    List<ValueeDTO> values = customFieldValues != null
                        ? WebApiApplication._mapper.Map<List<ValueeDTO>>(customFieldValues)
                        : new List<ValueeDTO>();

                    temp.value = values.ToArray();
                }

                item.custom_fields = custom_fields.ToArray();

            }
            return list;
        }

        [HttpGet]
        public List<ListingDTO> Get(int id)
        {
            var _dbContext = new HomelistiDbEntities();
            List<ListingDTO> list = WebApiApplication._mapper.Map<List<ListingDTO>>(_dbContext.Listings
                .Where(c => c.listing_id == id)
                .ToList());
            foreach (var item in list)
            {
                List<CustomFieldDTO> custom_fields = WebApiApplication._mapper.Map<List<CustomFieldDTO>>(_dbContext.Listings_CustomFields
                .Where(c => c.listing_id == item.listing_id)
                .Select(c => c.CustomField)
                .ToList());

                foreach (var temp in custom_fields)
                {
                    List<ChoiceDTO> choices = WebApiApplication._mapper.Map<List<ChoiceDTO>>(_dbContext.CustomFields_Choices
                    .Where(c => c.custom_fields_id == temp.id)
                    .Select(c => c.Choice)
                    .ToList());
                    temp.choices = choices.ToArray();

                    List<ValueeDTO> values = WebApiApplication._mapper.Map<List<ValueeDTO>>(_dbContext.CustomFields_Values
                    .Where(c => c.custom_fields_id == temp.id && c.listing_id == item.listing_id)
                    .Select(c => c.Valuess)
                    .ToList());
                    temp.value = values.ToArray();
                }

                item.custom_fields = custom_fields.ToArray();

            }
            return list;
        }
        [HttpGet]
        [Route("api/Listings/filter")]
        public List<ListingDTO> Search(string title, string type, int category_id, int location_id, int min_price, int max_price, string sort_by)
        {
            var _dbContext = new HomelistiDbEntities();
            List<ListingDTO> list = WebApiApplication._mapper.Map<List<ListingDTO>>(_dbContext.Listings.ToList());

            if (title != "null" && title != "" && title != null)
            {
                list = list.FindAll(x => x.title.Contains(title));
            }
            if(type != "null" && (type == "sell" || type == "buy" || type == "rent"))
            {
                list = list.FindAll(x => x.listingtype != null && x.listingtype.id == type);
            }
            if (category_id != 0 && list.Find(x => x.category.term_id == category_id) != null)
            {
                list = list.FindAll( x=> x.category.term_id == category_id);
            }
            if(location_id != 0)
            {
                list = list.FindAll(x => x.contact.location.term_id == location_id);
            }
            if (min_price != 0)
            {
                list = list.FindAll(x => Int64.Parse(x.price.Replace(",","")) >= min_price);
            }
            if (min_price != 10000000)
            {
                list = list.FindAll(x => Int64.Parse(x.price.Replace(",", "")) <= max_price);
            }

            if(sort_by != "date-desc")
            {
                switch(sort_by)
                {
                    case "title-asc":
                        list = list.OrderBy(x => x.title).ToList();
                        return list;
                    case "title-desc":
                        list = list.OrderBy(x => x.title).ToList();
                        list.Reverse();
                        return list;
                    case "views-desc":
                        list = list.OrderBy(x => x.view_count).ToList();
                        list.Reverse();
                        return list;
                    case "views-asc":
                        list = list.OrderBy(x => x.view_count).ToList();
                        return list;
                    case "price-asc":
                        list = list.OrderBy(x => Int64.Parse(x.price.Replace(",", ""))).ToList();
                        return list;
                    case "price-desc":
                        list = list.OrderBy(x => Int64.Parse(x.price.Replace(",", ""))).ToList();
                        list.Reverse();
                        return list;
                    default: return list;
                }
            }

            return list;
        }
        [HttpPost]
        [Authorize]
        public IHttpActionResult Post( int author_id, string title, string price, int category_id, string listing_type, int tv, int air, int barbeque, int gym, int swim, int laundry, int microwave, int outdoor, int lawn, int refrigerator, int sauna, int washer, int parking, int bed, int bath)
        {
            var _dbContext = new HomelistiDbEntities();

            List<User> user = _dbContext.Users.Where(x => x.id == author_id).ToList();
            user.ToArray();

            Listing listing = new Listing();
            int listing_id = _dbContext.Listings.ToList().Count() + 1;

            listing.listing_id = listing_id;
            listing.author_id = author_id;
            listing.title = title;
            listing.price = price;
            var category = _dbContext.Categories.FirstOrDefault(c => c.term_id == category_id);
            if (category == null)
            {
                listing.Category = category;
                //return BadRequest("Category not found");
            }
            listing.Category = category;
            //listing.category_term_id = category_id;
            listing.ad_type_id = listing_type;
            listing.view_count = 0;
            //listing.contact_id = user[0].contact_id;
            _dbContext.Listings.Add(listing);
            _dbContext.SaveChanges();

            int listings_cus_id = _dbContext.Listings_CustomFields.ToList().Count() + 1;
            Listings_CustomFields listings_CustomFields = new Listings_CustomFields();
            listings_CustomFields.id = listings_cus_id;
            listings_CustomFields.listing_id = listing_id;
            listings_CustomFields.custom_fields_id = 4216;
            _dbContext.Listings_CustomFields.Add(listings_CustomFields);
            _dbContext.SaveChanges();

            listings_CustomFields = new Listings_CustomFields();
            listings_CustomFields.id = listings_cus_id + 1;
            listings_CustomFields.listing_id = listing_id;
            listings_CustomFields.custom_fields_id = 4323;
            _dbContext.Listings_CustomFields.Add(listings_CustomFields);
            _dbContext.SaveChanges();

            listings_CustomFields = new Listings_CustomFields();
            listings_CustomFields.id = listings_cus_id + 2;
            listings_CustomFields.listing_id = listing_id;
            listings_CustomFields.custom_fields_id = 4322;
            _dbContext.Listings_CustomFields.Add(listings_CustomFields);
            _dbContext.SaveChanges();

            listings_CustomFields = new Listings_CustomFields();
            listings_CustomFields.id = listings_cus_id + 3;
            listings_CustomFields.listing_id = listing_id;
            listings_CustomFields.custom_fields_id = 4316;
            _dbContext.Listings_CustomFields.Add(listings_CustomFields);
            _dbContext.SaveChanges();

            listings_CustomFields = new Listings_CustomFields();
            listings_CustomFields.id = listings_cus_id + 4;
            listings_CustomFields.listing_id = listing_id;
            listings_CustomFields.custom_fields_id = 4321;
            _dbContext.Listings_CustomFields.Add(listings_CustomFields);
            _dbContext.SaveChanges();

            listings_CustomFields = new Listings_CustomFields();
            listings_CustomFields.id = listings_cus_id + 5;
            listings_CustomFields.listing_id = listing_id;
            listings_CustomFields.custom_fields_id = 4692;
            _dbContext.Listings_CustomFields.Add(listings_CustomFields);
            _dbContext.SaveChanges();

            int customFields_val_id = _dbContext.CustomFields_Values.ToList().Count() + 5;
            CustomFields_Values customFields_Values = new CustomFields_Values();
            if(tv == 1 || tv != -1)
            {
                customFields_Values = new CustomFields_Values();
                customFields_val_id += 1;
                customFields_Values.id = customFields_val_id;
                customFields_Values.values_id = 1;
                customFields_Values.custom_fields_id = 4216;
                customFields_Values.listing_id = listing_id;
                _dbContext.CustomFields_Values.Add(customFields_Values);
                _dbContext.SaveChanges();
            }
            if (air == 2 || air != -1)
            {
                customFields_Values = new CustomFields_Values();
                customFields_val_id += 1;
                customFields_Values.id = customFields_val_id;
                customFields_Values.values_id = 2;
                customFields_Values.custom_fields_id = 4216;
                customFields_Values.listing_id = listing_id;
                _dbContext.CustomFields_Values.Add(customFields_Values);
                _dbContext.SaveChanges();
            }
            if (barbeque == 3 || barbeque != -1)
            {
                customFields_Values = new CustomFields_Values();
                customFields_val_id += 1;
                customFields_Values.id = customFields_val_id;
                customFields_Values.values_id = 3;
                customFields_Values.custom_fields_id = 4216;
                customFields_Values.listing_id = listing_id;
                _dbContext.CustomFields_Values.Add(customFields_Values);
                _dbContext.SaveChanges();
            }
            if (gym == 4 || gym != -1)
            {
                customFields_Values = new CustomFields_Values();
                customFields_val_id += 1;
                customFields_Values.id = customFields_val_id;
                customFields_Values.values_id = 4;
                customFields_Values.custom_fields_id = 4216;
                customFields_Values.listing_id = listing_id;
                _dbContext.CustomFields_Values.Add(customFields_Values);
                _dbContext.SaveChanges();
            }
            if (swim == 5 || swim != -1)
            {
                customFields_Values = new CustomFields_Values();
                customFields_val_id += 1;
                customFields_Values.id = customFields_val_id;
                customFields_Values.values_id = 5;
                customFields_Values.custom_fields_id = 4216;
                customFields_Values.listing_id = listing_id;
                _dbContext.CustomFields_Values.Add(customFields_Values);
                _dbContext.SaveChanges();
            }
            if (laundry == 6 || laundry != -1)
            {
                customFields_Values = new CustomFields_Values();
                customFields_val_id += 1;
                customFields_Values.id = customFields_val_id;
                customFields_Values.values_id = 6;
                customFields_Values.custom_fields_id = 4216;
                customFields_Values.listing_id = listing_id;
                _dbContext.CustomFields_Values.Add(customFields_Values);
                _dbContext.SaveChanges();
            }
            if (microwave == 7 || microwave != -1)
            {
                customFields_Values = new CustomFields_Values();
                customFields_val_id += 1;
                customFields_Values.id = customFields_val_id;
                customFields_Values.values_id = 7;
                customFields_Values.custom_fields_id = 4216;
                customFields_Values.listing_id = listing_id;
                _dbContext.CustomFields_Values.Add(customFields_Values);
                _dbContext.SaveChanges();
            }
            if (outdoor == 8 || outdoor != -1)
            {
                customFields_Values = new CustomFields_Values();
                customFields_val_id += 1;
                customFields_Values.id = customFields_val_id;
                customFields_Values.values_id = 8;
                customFields_Values.custom_fields_id = 4216;
                customFields_Values.listing_id = listing_id;
                _dbContext.CustomFields_Values.Add(customFields_Values);
                _dbContext.SaveChanges();
            }
            if (lawn == 9 || lawn != -1)
            {
                customFields_Values = new CustomFields_Values();
                customFields_val_id += 1;
                customFields_Values.id = customFields_val_id;
                customFields_Values.values_id = 9;
                customFields_Values.custom_fields_id = 4216;
                customFields_Values.listing_id = listing_id;
                _dbContext.CustomFields_Values.Add(customFields_Values);
                _dbContext.SaveChanges();
            }
            if (refrigerator == 10 || refrigerator != -1)
            {
                customFields_Values = new CustomFields_Values();
                customFields_val_id += 1;
                customFields_Values.id = customFields_val_id;
                customFields_Values.values_id = 10;
                customFields_Values.custom_fields_id = 4216;
                customFields_Values.listing_id = listing_id;
                _dbContext.CustomFields_Values.Add(customFields_Values);
                _dbContext.SaveChanges();
            }
            if (sauna == 11 || sauna != -1)
            {
                customFields_Values = new CustomFields_Values();
                customFields_val_id += 1;
                customFields_Values.id = customFields_val_id;
                customFields_Values.values_id = 11;
                customFields_Values.custom_fields_id = 4216;
                customFields_Values.listing_id = listing_id;
                _dbContext.CustomFields_Values.Add(customFields_Values);
                _dbContext.SaveChanges();
            }
            if (washer == 12 || washer != -1)
            {
                customFields_Values = new CustomFields_Values();
                customFields_val_id += 1;
                customFields_Values.id = customFields_val_id;
                customFields_Values.values_id = 12;
                customFields_Values.custom_fields_id = 4216;
                customFields_Values.listing_id = listing_id;
                _dbContext.CustomFields_Values.Add(customFields_Values);
                _dbContext.SaveChanges();
            }

            customFields_Values = new CustomFields_Values();
            customFields_val_id += 1;
            customFields_Values.id = customFields_val_id;
            switch (category_id)
            {
                case 112:
                    customFields_Values.values_id = 13;
                    break;
                case 162:
                    customFields_Values.values_id = 14;
                    break;
                case 204:
                    customFields_Values.values_id = 15;
                    break;
                default:
                    customFields_Values.values_id = 13;
                    break;
            }
            customFields_Values.custom_fields_id = 4323;
            customFields_Values.listing_id = listing_id;
            _dbContext.CustomFields_Values.Add(customFields_Values);
            _dbContext.SaveChanges();

            customFields_Values = new CustomFields_Values();
            customFields_val_id += 1;
            customFields_Values.id = customFields_val_id;
            customFields_Values.values_id = parking;
            customFields_Values.custom_fields_id = 4322;
            customFields_Values.listing_id = listing_id;
            _dbContext.CustomFields_Values.Add(customFields_Values);
            _dbContext.SaveChanges();

            customFields_Values = new CustomFields_Values();
            customFields_val_id += 1;
            customFields_Values.id = customFields_val_id;
            customFields_Values.values_id = bed;
            customFields_Values.custom_fields_id = 4316;
            customFields_Values.listing_id = listing_id;
            _dbContext.CustomFields_Values.Add(customFields_Values);
            _dbContext.SaveChanges();

            customFields_Values = new CustomFields_Values();
            customFields_val_id += 1;
            customFields_Values.id = customFields_val_id;
            customFields_Values.values_id = bath;
            customFields_Values.custom_fields_id = 4321;
            customFields_Values.listing_id = listing_id;
            _dbContext.CustomFields_Values.Add(customFields_Values);
            _dbContext.SaveChanges();

            customFields_Values = new CustomFields_Values();
            customFields_val_id += 1;
            customFields_Values.id = customFields_val_id;
            switch (listing_type)
            {
                case "sell":
                    customFields_Values.values_id = 28;
                    break;
                case "buy":
                    customFields_Values.values_id = 29;
                    break;
                case "rent":
                    customFields_Values.values_id = 30;
                    break;
                default:
                    break;
            }
            customFields_Values.custom_fields_id = 4692;
            customFields_Values.listing_id = listing_id;
            _dbContext.CustomFields_Values.Add(customFields_Values);
            _dbContext.SaveChanges();

            return Ok(listing);
        }
    }
}
