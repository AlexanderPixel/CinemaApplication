namespace Cinema.DAL.Context
{
    using Cinema.DAL.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ticket")]
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public int FilmId { get; set; }
        public int CinemaHallId { get; set; }
        public int MovieTheaterId { get; set; }
        public CinemaHallSeat Seat { get; set; }
        public DateTime? TicketDate { get; set; }

        public virtual User User { get; set; }
        public virtual Film Film { get; set; }
        public virtual CinemaHall CinemaHall { get; set; }
        public virtual MovieTheater MovieTheater { get; set; }
    }
}
