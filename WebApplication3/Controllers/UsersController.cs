using homelisti_API.Models.Domain;
using HomelistiAPI.Db;
using Microsoft.IdentityModel.Tokens;
using Ninject.Activation;
using System;
using System.Collections.Generic;
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
using Microsoft.Owin.Security;
using Microsoft.Owin.BuilderProperties;
using System.ComponentModel.DataAnnotations;

namespace HomelistiAPI.Controllers
{
    public class UsersController : ApiController
    {
        [HttpGet]
        public List<UserDTO> Get()
        {
            var dbContext = new HomelistiDbEntities();
            List<UserDTO> list = WebApiApplication._mapper.Map<List<UserDTO>>(dbContext.Users.ToList());
            return list;
        }
        [HttpGet]
        [Route("api/user/id")]
        public UserDTO FindUser(string username)
        {
            var dbContext = new HomelistiDbEntities();
            UserDTO user = WebApiApplication._mapper.Map<UserDTO>(dbContext.Users.Where(x=>x.Account.username==username).FirstOrDefault());
            return user;
        }
        [HttpGet]
        [Route("api/user/userId")]
        public UserDTO FindUserWithID(int id)
        {
            var dbContext = new HomelistiDbEntities();
            UserDTO user = WebApiApplication._mapper.Map<UserDTO>(dbContext.Users.Where(x => x.id == id).FirstOrDefault());
            return user;
        }
        [HttpPost]
        public IHttpActionResult Post(string username, string password)
        {
            if (IsValidUser(username, password) != null)
            {
                var token = GenerateToken(username);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
        [HttpPost]
        public IHttpActionResult Register(string username, string password, string first_name, string last_name, int location_id, string address, string phone, string email)
        {
            var dbContext = new HomelistiDbEntities();
            Account checkAccount = dbContext.Accounts.Where(x=>x.username == username).FirstOrDefault();
            if(checkAccount != null)
            {
                return BadRequest("Existing username");
            }
            Account account = new Account();
            account.id = dbContext.Accounts.ToList().Count() + 1;
            account.username = username;
            account.password = password;
            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();
            dbContext.Database.Log = Console.WriteLine;
            Console.WriteLine("a");
            Contact contact = new Contact();
            contact.id = dbContext.Contacts.ToList().Count() + 1;
            //contact.location_term_id = location_id;
            contact.address = address;
            contact.phone = phone;
            contact.whatsapp_number = phone;
            contact.email = email;
            dbContext.Contacts.Add(contact);
            
            dbContext.SaveChanges();

            User user = new User();
            user.id = dbContext.Users.ToList().Count() + 4;
            user.first_name = first_name;
            user.last_name = last_name;
            user.username = first_name + " " + last_name;
            user.Account = account;
            user.Contact = contact;
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return Ok(WebApiApplication._mapper.Map<UserDTO>(user));
        }
        public List<UserDTO> IsValidUser(string username, string password)
        {
            var _dbContext = new HomelistiDbEntities();
            List<AccountDTO> accounts = WebApiApplication._mapper.Map<List<AccountDTO>>(_dbContext.Accounts.ToList());
            foreach(var item in accounts)
            {
                if (item.username == username && item.password == password)
                {
                    List<UserDTO> users = WebApiApplication._mapper.Map<List<UserDTO>>(_dbContext.Users
                        .Where(c => c.Account.id == item.id)
                        .ToList());

                    return users = WebApiApplication._mapper.Map<List<UserDTO>>(_dbContext.Users
                        .Where(c => c.Account.id == item.id)
                        .ToList());
                }
            }
            return null;
        }
        [HttpPut]
        public IHttpActionResult ResetPassword(string username, string password)
        {
            var dbContext = new HomelistiDbEntities();
            Account checkAccount = dbContext.Accounts.Where(x => x.username == username).FirstOrDefault();
            if (checkAccount == null)
            {
                return BadRequest("Username does not exist");
            }
            else
            {
                checkAccount.password = password;
                dbContext.SaveChanges();
                return Ok(WebApiApplication._mapper.Map<AccountDTO>(checkAccount));
            }
        }
        private string GenerateToken(string username)
        {
            string secretKey = "HGlqKsfuDwfl3DEOpVlMIIUOXZKrWjFosAOTkk+T/Tcn3IaMgRsNOtwcVnpbNLSs";
            
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
            };

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingCredentials
            );
            
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
