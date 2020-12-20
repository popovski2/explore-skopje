namespace ExploreSkopjeMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class urlfacebookLinktheatre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Theatres", "url_photo", c => c.String());
            AddColumn("dbo.Theatres", "facebook_link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Theatres", "facebook_link");
            DropColumn("dbo.Theatres", "url_photo");
        }
    }
}
