namespace HomelistiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        first_name = c.String(),
                        last_name = c.String(),
                        username = c.String(),
                        //contact_id = c.Int(),
                        //account_id = c.Int(),
                        Account_id = c.Int(),
                        Contact_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Accounts", t => t.Account_id)
                .ForeignKey("dbo.Contacts", t => t.Contact_id)
                .Index(t => t.Account_id)
                .Index(t => t.Contact_id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        //location_term_id = c.Int(),
                        address = c.String(),
                        phone = c.String(),
                        whatsapp_number = c.String(),
                        email = c.String(),
                        location_term_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Locations", t => t.location_term_id)
                .Index(t => t.location_term_id);
            
            CreateTable(
                "dbo.Listings",
                c => new
                    {
                        listing_id = c.Int(nullable: false, identity: true),
                        author_id = c.Int(),
                        title = c.String(),
                        price = c.String(),
                        //category_term_id = c.Int(),
                        ad_type_id = c.String(),
                        view_count = c.Int(),
                        //contact_id = c.Int(),
                        description = c.String(),
                        Category_term_id = c.Int(),
                        Contact_id = c.Int(),
                        ListingType_id = c.String(maxLength: 128),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.listing_id)
                .ForeignKey("dbo.Categories", t => t.Category_term_id)
                .ForeignKey("dbo.Contacts", t => t.Contact_id)
                .ForeignKey("dbo.ListingTypes", t => t.ListingType_id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.Category_term_id)
                .Index(t => t.Contact_id)
                .Index(t => t.ListingType_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        term_id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        slug = c.String(),
                        count = c.Int(),
                    })
                .PrimaryKey(t => t.term_id);
            
            CreateTable(
                "dbo.CustomFields_Values",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        values_id = c.Int(),
                        custom_fields_id = c.Int(),
                        listing_id = c.Int(),
                        CustomField_id = c.Int(),
                        Valuess_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.CustomFields", t => t.CustomField_id)
                .ForeignKey("dbo.Listings", t => t.listing_id)
                .ForeignKey("dbo.Valuesses", t => t.Valuess_id)
                .Index(t => t.listing_id)
                .Index(t => t.CustomField_id)
                .Index(t => t.Valuess_id);
            
            CreateTable(
                "dbo.CustomFields",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        label = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.CustomFields_Choices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        choices_id = c.String(),
                        custom_fields_id = c.Int(),
                        Choice_id = c.String(maxLength: 128),
                        CustomField_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Choices", t => t.Choice_id)
                .ForeignKey("dbo.CustomFields", t => t.CustomField_id)
                .Index(t => t.Choice_id)
                .Index(t => t.CustomField_id);
            
            CreateTable(
                "dbo.Choices",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Listings_CustomFields",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        listing_id = c.Int(),
                        custom_fields_id = c.Int(),
                        CustomField_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.CustomFields", t => t.CustomField_id)
                .ForeignKey("dbo.Listings", t => t.listing_id)
                .Index(t => t.listing_id)
                .Index(t => t.CustomField_id);
            
            CreateTable(
                "dbo.Valuesses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        data = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        listing_id = c.Int(),
                        title = c.String(),
                        url = c.String(),
                        alt = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Listings", t => t.listing_id)
                .Index(t => t.listing_id);
            
            CreateTable(
                "dbo.ListingTypes",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        term_id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        slug = c.String(),
                        count = c.Int(),
                    })
                .PrimaryKey(t => t.term_id);
            
            CreateTable(
                "dbo.C__EFMigrationsHistory",
                c => new
                    {
                        MigrationId = c.String(nullable: false, maxLength: 128),
                        ProductVersion = c.String(),
                    })
                .PrimaryKey(t => t.MigrationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Contact_id", "dbo.Contacts");
            DropForeignKey("dbo.Contacts", "location_term_id", "dbo.Locations");
            DropForeignKey("dbo.Listings", "User_id", "dbo.Users");
            DropForeignKey("dbo.Listings", "ListingType_id", "dbo.ListingTypes");
            DropForeignKey("dbo.Images", "listing_id", "dbo.Listings");
            DropForeignKey("dbo.CustomFields_Values", "Valuess_id", "dbo.Valuesses");
            DropForeignKey("dbo.CustomFields_Values", "listing_id", "dbo.Listings");
            DropForeignKey("dbo.Listings_CustomFields", "listing_id", "dbo.Listings");
            DropForeignKey("dbo.Listings_CustomFields", "CustomField_id", "dbo.CustomFields");
            DropForeignKey("dbo.CustomFields_Values", "CustomField_id", "dbo.CustomFields");
            DropForeignKey("dbo.CustomFields_Choices", "CustomField_id", "dbo.CustomFields");
            DropForeignKey("dbo.CustomFields_Choices", "Choice_id", "dbo.Choices");
            DropForeignKey("dbo.Listings", "Contact_id", "dbo.Contacts");
            DropForeignKey("dbo.Listings", "Category_term_id", "dbo.Categories");
            DropForeignKey("dbo.Users", "Account_id", "dbo.Accounts");
            DropIndex("dbo.Images", new[] { "listing_id" });
            DropIndex("dbo.Listings_CustomFields", new[] { "CustomField_id" });
            DropIndex("dbo.Listings_CustomFields", new[] { "listing_id" });
            DropIndex("dbo.CustomFields_Choices", new[] { "CustomField_id" });
            DropIndex("dbo.CustomFields_Choices", new[] { "Choice_id" });
            DropIndex("dbo.CustomFields_Values", new[] { "Valuess_id" });
            DropIndex("dbo.CustomFields_Values", new[] { "CustomField_id" });
            DropIndex("dbo.CustomFields_Values", new[] { "listing_id" });
            DropIndex("dbo.Listings", new[] { "User_id" });
            DropIndex("dbo.Listings", new[] { "ListingType_id" });
            DropIndex("dbo.Listings", new[] { "Contact_id" });
            DropIndex("dbo.Listings", new[] { "Category_term_id" });

            DropIndex("dbo.Contacts", new[] { "location_term_id" });
            DropIndex("dbo.Users", new[] { "Contact_id" });
            DropIndex("dbo.Users", new[] { "Account_id" });
            DropTable("dbo.C__EFMigrationsHistory");
            DropTable("dbo.Locations");
            DropTable("dbo.ListingTypes");
            DropTable("dbo.Images");
            DropTable("dbo.Valuesses");
            DropTable("dbo.Listings_CustomFields");
            DropTable("dbo.Choices");
            DropTable("dbo.CustomFields_Choices");
            DropTable("dbo.CustomFields");
            DropTable("dbo.CustomFields_Values");
            DropTable("dbo.Categories");
            DropTable("dbo.Listings");
            DropTable("dbo.Contacts");
            DropTable("dbo.Users");
            DropTable("dbo.Accounts");
        }
    }
}
