using Cinema.DAL.Models;
using System;

namespace Cinema.BLL.DTO
{
    public class TicketDTO
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public int FilmId { get; set; }
        public int CinemaHallId { get; set; }
        public int MovieTheaterId { get; set; }
        public CinemaHallSeat Seat { get; set; }
        public DateTime? TicketDate { get; set; }
    }
}
