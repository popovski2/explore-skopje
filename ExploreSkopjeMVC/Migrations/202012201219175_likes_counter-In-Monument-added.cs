namespace ExploreSkopjeMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class likes_counterInMonumentadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Monuments", "likes_counter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Monuments", "likes_counter");
        }
    }
}
