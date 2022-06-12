namespace Cinema.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserUniqueConstraints : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.User", "UserLogin", unique: true);
            CreateIndex("dbo.User", "UserPhone", unique: true);
            CreateIndex("dbo.User", "UserEmail", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "UserEmail" });
            DropIndex("dbo.User", new[] { "UserPhone" });
            DropIndex("dbo.User", new[] { "UserLogin" });
        }
    }
}
