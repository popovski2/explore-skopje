﻿namespace ExploreSkopjeMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class urlfblinkaddedMonuments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Monuments", "picture_URL", c => c.String());
            AddColumn("dbo.Monuments", "facebook_link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Monuments", "facebook_link");
            DropColumn("dbo.Monuments", "picture_URL");
        }
    }
}
