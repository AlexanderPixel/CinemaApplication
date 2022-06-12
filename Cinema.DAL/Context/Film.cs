namespace Cinema.DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Film")]
    public partial class Film
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Film()
        {
            FilmActors = new HashSet<FilmActor>();
        }

        public int FilmId { get; set; }

        [Required]
        [StringLength(100)]
        public string FilmTitle { get; set; }

        public string FilmDescription { get; set; }

        public int FilmReleaseYear { get; set; }

        [Column(TypeName = "date")]
        public DateTime FilmDisplayStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime FilmDisplayEndDate { get; set; }

        public string FilmImage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilmActor> FilmActors { get; set; }
    }
}
