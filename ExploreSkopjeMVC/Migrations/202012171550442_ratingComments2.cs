namespace ExploreSkopjeMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingComments2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RatingComments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Comments = c.String(),
                        ThisDateTime = c.DateTime(),
                        ObjectId = c.Int(nullable: false),
                        Rating = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RatingComments");
        }
    }
}
