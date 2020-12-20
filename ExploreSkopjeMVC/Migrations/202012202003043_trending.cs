namespace ExploreSkopjeMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trending : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CoffeeBars", "Trending_id1", c => c.Long());
            AddColumn("dbo.Restaurants", "Trending_id1", c => c.Long());
            AddColumn("dbo.Theatres", "Trending_id1", c => c.Long());
            CreateIndex("dbo.CoffeeBars", "Trending_id1");
            CreateIndex("dbo.Restaurants", "Trending_id1");
            CreateIndex("dbo.Theatres", "Trending_id1");
            AddForeignKey("dbo.CoffeeBars", "Trending_id1", "dbo.Trendings", "id");
            AddForeignKey("dbo.Restaurants", "Trending_id1", "dbo.Trendings", "id");
            AddForeignKey("dbo.Theatres", "Trending_id1", "dbo.Trendings", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Theatres", "Trending_id1", "dbo.Trendings");
            DropForeignKey("dbo.Restaurants", "Trending_id1", "dbo.Trendings");
            DropForeignKey("dbo.CoffeeBars", "Trending_id1", "dbo.Trendings");
            DropIndex("dbo.Theatres", new[] { "Trending_id1" });
            DropIndex("dbo.Restaurants", new[] { "Trending_id1" });
            DropIndex("dbo.CoffeeBars", new[] { "Trending_id1" });
            DropColumn("dbo.Theatres", "Trending_id1");
            DropColumn("dbo.Restaurants", "Trending_id1");
            DropColumn("dbo.CoffeeBars", "Trending_id1");
        }
    }
}
