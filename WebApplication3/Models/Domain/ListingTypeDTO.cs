using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace homelisti_API.Models.Domain
{
    public class ListingTypeDTO
    {
        [Required]
        public string id { get; set; }
        public string name { get; set; }
    }
}
