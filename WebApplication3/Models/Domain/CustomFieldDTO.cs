
using System.ComponentModel.DataAnnotations;

namespace homelisti_API.Models.Domain
{
    public class CustomFieldDTO
    {
        [Required]
        public int id { get; set; }
        public string label { get; set; }
        public ValueeDTO[] value { get; set; }
        public ChoiceDTO[] choices { get; set; }
    }
}
