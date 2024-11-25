using System.ComponentModel.DataAnnotations;

namespace homelisti_API.Models.Domain
{
    public class ListingDTO
    {  
        [Required]
        public int listing_id { get; set; }
        public int author_id { get; set; }
        public string title { get; set; }
        public string price { get; set; }
        //public string price_type { get; set; }
        public CategoryDTO category { get; set; }
        public ListingTypeDTO listingtype { get; set; }
        public int view_count { get; set; }
        public ContactDTO contact { get; set; }
        public ImageDTO[] images { get; set; }
        public CustomFieldDTO[] custom_fields { get; set; }
        public string description { get; set; }

    }
}
