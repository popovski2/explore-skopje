namespace ExploreSkopjeMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingCB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CoffeeBars", "TotalRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CoffeeBars", "TotalRating");
        }
    }
}
