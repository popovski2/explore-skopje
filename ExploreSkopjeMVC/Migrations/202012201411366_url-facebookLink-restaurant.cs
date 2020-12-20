namespace ExploreSkopjeMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class urlfacebookLinkrestaurant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "url_photo", c => c.String());
            AddColumn("dbo.Restaurants", "facebook_link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "facebook_link");
            DropColumn("dbo.Restaurants", "url_photo");
        }
    }
}
