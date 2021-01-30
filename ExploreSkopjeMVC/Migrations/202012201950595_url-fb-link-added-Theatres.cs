namespace ExploreSkopjeMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class urlfblinkaddedTheatres : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Theatres", "picture_URL", c => c.String());
            AddColumn("dbo.Theatres", "facebook_link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Theatres", "facebook_link");
            DropColumn("dbo.Theatres", "picture_URL");
        }
    }
}
