using System.ComponentModel.DataAnnotations;

namespace homelisti_API.Models.Domain
{
    public class AccountDTO
    {
        [Required]
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
