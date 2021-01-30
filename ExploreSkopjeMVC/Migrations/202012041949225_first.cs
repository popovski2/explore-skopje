namespace ExploreSkopjeMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CoffeeBars",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(),
                        longitude = c.Double(nullable: false),
                        latitude = c.Double(nullable: false),
                        Trending_id = c.Long(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Trendings", t => t.Trending_id)
                .Index(t => t.Trending_id);
            
            CreateTable(
                "dbo.Monuments",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(),
                        longitude = c.Double(nullable: false),
                        latitude = c.Double(nullable: false),
                        Trending_id = c.Long(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Trendings", t => t.Trending_id)
                .Index(t => t.Trending_id);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(),
                        longitude = c.Double(nullable: false),
                        latitude = c.Double(nullable: false),
                        Trending_id = c.Long(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Trendings", t => t.Trending_id)
                .Index(t => t.Trending_id);
            
            CreateTable(
                "dbo.Theatres",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(),
                        longitude = c.Double(nullable: false),
                        latitude = c.Double(nullable: false),
                        Trending_id = c.Long(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Trendings", t => t.Trending_id)
                .Index(t => t.Trending_id);
            
            CreateTable(
                "dbo.Trendings",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Theatres", "Trending_id", "dbo.Trendings");
            DropForeignKey("dbo.Restaurants", "Trending_id", "dbo.Trendings");
            DropForeignKey("dbo.Monuments", "Trending_id", "dbo.Trendings");
            DropForeignKey("dbo.CoffeeBars", "Trending_id", "dbo.Trendings");
            DropIndex("dbo.Theatres", new[] { "Trending_id" });
            DropIndex("dbo.Restaurants", new[] { "Trending_id" });
            DropIndex("dbo.Monuments", new[] { "Trending_id" });
            DropIndex("dbo.CoffeeBars", new[] { "Trending_id" });
            DropTable("dbo.Trendings");
            DropTable("dbo.Theatres");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Monuments");
            DropTable("dbo.CoffeeBars");
        }
    }
}
