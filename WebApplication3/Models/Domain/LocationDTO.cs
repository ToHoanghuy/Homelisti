using System.ComponentModel.DataAnnotations;

namespace homelisti_API.Models.Domain
{
    public class LocationDTO
    {
        [Required]
        public int term_id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public int count { get; set; }
    }
}
