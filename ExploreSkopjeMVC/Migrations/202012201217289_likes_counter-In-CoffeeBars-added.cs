namespace ExploreSkopjeMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class likes_counterInCoffeeBarsadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CoffeeBars", "likes_counter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CoffeeBars", "likes_counter");
        }
    }
}
