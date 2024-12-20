
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace HomelistiAPI.Db
{

using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Listing
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Listing()
    {

        this.Images = new HashSet<Image>();

        this.Listings_CustomFields = new HashSet<Listings_CustomFields>();

        this.CustomFields_Values = new HashSet<CustomFields_Values>();

    }

        [Key]
    public int listing_id { get; set; }

    public Nullable<int> author_id { get; set; }

    public string title { get; set; }

    public string price { get; set; }

    //public Nullable<int> Category_term_id { get; set; }

    public string ad_type_id { get; set; }

    public Nullable<int> view_count { get; set; }

    //public Nullable<int> contact_id { get; set; }

    public string description { get; set; }



    public virtual Category Category { get; set; }

    public virtual Contact Contact { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Image> Images { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Listings_CustomFields> Listings_CustomFields { get; set; }

    public virtual ListingType ListingType { get; set; }

    public virtual User User { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<CustomFields_Values> CustomFields_Values { get; set; }

}

}
