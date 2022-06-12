namespace Cinema.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTicketTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FilmId = c.Int(nullable: false),
                        CinemaHallId = c.Int(nullable: false),
                        MovieTheaterId = c.Int(nullable: false),
                        Seat_Row = c.Int(nullable: false),
                        Seat_Column = c.Int(nullable: false),
                        TicketDate = c.DateTime(),
                        MovieTheater_CinemaId = c.Int(),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.CinemaHall", t => t.CinemaHallId, cascadeDelete: true)
                .ForeignKey("dbo.Film", t => t.FilmId, cascadeDelete: true)
                .ForeignKey("dbo.MovieTheater", t => t.MovieTheater_CinemaId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.FilmId)
                .Index(t => t.CinemaHallId)
                .Index(t => t.MovieTheater_CinemaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ticket", "UserId", "dbo.User");
            DropForeignKey("dbo.Ticket", "MovieTheater_CinemaId", "dbo.MovieTheater");
            DropForeignKey("dbo.Ticket", "FilmId", "dbo.Film");
            DropForeignKey("dbo.Ticket", "CinemaHallId", "dbo.CinemaHall");
            DropIndex("dbo.Ticket", new[] { "MovieTheater_CinemaId" });
            DropIndex("dbo.Ticket", new[] { "CinemaHallId" });
            DropIndex("dbo.Ticket", new[] { "FilmId" });
            DropIndex("dbo.Ticket", new[] { "UserId" });
            DropTable("dbo.Ticket");
        }
    }
}
