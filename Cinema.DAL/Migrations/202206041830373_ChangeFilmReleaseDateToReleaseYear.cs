namespace Cinema.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFilmReleaseDateToReleaseYear : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Film", "FilmReleaseYear", c => c.Int(nullable: false));
            DropColumn("dbo.Film", "FilmReleaseDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Film", "FilmReleaseDate", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.Film", "FilmReleaseYear");
        }
    }
}
