namespace Cinema.DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CinemaHall")]
    public partial class CinemaHall
    {
        public int CinemaHallId { get; set; }

        public int TotalSeatsNumber { get; set; }

        public int AvailableSeatsNumber { get; set; }

        public int BookedSeatsNumber { get; set; }

        public int CinemaId { get; set; }

        public virtual MovieTheater MovieTheater { get; set; }
    }
}
