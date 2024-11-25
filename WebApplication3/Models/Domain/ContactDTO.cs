using System.ComponentModel.DataAnnotations;

namespace homelisti_API.Models.Domain
{
    public class ContactDTO
    {
        [Required]
        public int id { get; set; }
        public LocationDTO location { get; set; }
        public string address { get; set; }
        [Required]
        public string phone { get; set; }
        public string whatsapp_number { get; set; }
        public string email { get; set; }
    }
}
