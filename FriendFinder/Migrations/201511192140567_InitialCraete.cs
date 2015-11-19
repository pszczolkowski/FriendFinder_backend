namespace FriendFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCraete : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Longitude = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Positions");
        }
    }
}
