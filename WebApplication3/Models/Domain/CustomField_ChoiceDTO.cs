using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomelistiAPI.Models.Domain
{
    public class CustomField_ChoiceDTO
    {
        [Required]
        public int id { get; set; }
        public string choice_id { get; set; }
        public int custom_field_id { get; set; }

    }
}