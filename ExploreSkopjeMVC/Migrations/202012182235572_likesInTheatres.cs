namespace ExploreSkopjeMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class likesInTheatres : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Theatres", "likes_counter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Theatres", "likes_counter");
        }
    }
}
