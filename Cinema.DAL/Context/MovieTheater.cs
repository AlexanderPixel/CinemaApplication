namespace Cinema.DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MovieTheater")]
    public partial class MovieTheater
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MovieTheater()
        {
            CinemaHalls = new HashSet<CinemaHall>();
        }

        [Key]
        public int CinemaId { get; set; }

        [Required]
        [StringLength(100)]
        public string CinemaLocation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CinemaHall> CinemaHalls { get; set; }
    }
}
