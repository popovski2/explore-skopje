namespace ExploreSkopjeMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class likesInRestaurants : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "likes_counter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "likes_counter");
        }
    }
}
