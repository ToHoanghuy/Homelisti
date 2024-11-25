using System.ComponentModel.DataAnnotations;

namespace homelisti_API.Models.Domain
{
    public class ImageDTO
    {
        [Required]
        public int ID { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string alt { get; set; }
    }
}
