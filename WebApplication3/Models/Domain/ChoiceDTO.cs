using System.ComponentModel.DataAnnotations;

namespace homelisti_API.Models.Domain
{
    public class ChoiceDTO
    {
        [Required]
        public string id { get; set; }
        public string name { get; set; }
    }
}
