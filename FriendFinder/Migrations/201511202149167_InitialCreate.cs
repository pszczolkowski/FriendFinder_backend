namespace FriendFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Friends", "FriendUserName");
            AddColumn("dbo.Friends", "FriendUserName", c => c.String());
           // DropColumn("dbo.Friends", "UserFriendId");
        }
        
        public override void Down()
        {
           // AddColumn("dbo.Friends", "UserFriendId", c => c.String());
            DropColumn("dbo.Friends", "FriendUserName");
        }
    }
}
