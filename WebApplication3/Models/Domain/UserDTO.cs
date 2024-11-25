using System;
using System.ComponentModel.DataAnnotations;

namespace homelisti_API.Models.Domain
{
    public class UserDTO
    {
        [Required]
        public int id { get; set; }
        public string first_name { get; set; }
        public string  last_name { get; set; }
        public string username { get; set; }
        public ContactDTO contact { get; set; }
        public AccountDTO account { get; set; }
    }
}
