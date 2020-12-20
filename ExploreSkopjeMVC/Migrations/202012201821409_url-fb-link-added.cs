namespace ExploreSkopjeMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class urlfblinkadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CoffeeBars", "picture_URL", c => c.String());
            AddColumn("dbo.CoffeeBars", "facebook_link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CoffeeBars", "facebook_link");
            DropColumn("dbo.CoffeeBars", "picture_URL");
        }
    }
}
