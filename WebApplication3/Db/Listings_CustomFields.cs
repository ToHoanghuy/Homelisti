
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
    
public partial class Listings_CustomFields
{

    public int id { get; set; }

    public Nullable<int> listing_id { get; set; }

    public Nullable<int> custom_fields_id { get; set; }



    public virtual CustomField CustomField { get; set; }

    public virtual Listing Listing { get; set; }

}

}
