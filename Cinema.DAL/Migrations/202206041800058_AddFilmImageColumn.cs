namespace Cinema.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilmImageColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Film", "FilmImage", c => c.String());
        }
        
        public override void Down()
        {
        }
    }
}
