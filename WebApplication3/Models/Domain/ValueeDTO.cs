using System.ComponentModel.DataAnnotations;

namespace homelisti_API.Models.Domain
{
    public class ValueeDTO
    {
        [Required]
        public int id { get; set; }
        public string data { get; set; }
    }
}
