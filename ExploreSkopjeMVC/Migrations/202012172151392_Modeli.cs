namespace ExploreSkopjeMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modeli : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RatingComments", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RatingComments", "Type");
        }
    }
}
