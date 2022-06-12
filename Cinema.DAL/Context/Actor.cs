namespace Cinema.DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Actor")]
    public partial class Actor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Actor()
        {
            FilmActors = new HashSet<FilmActor>();
        }

        public int ActorId { get; set; }

        [Required]
        [StringLength(50)]
        public string ActorFirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string ActorLastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ActorBirthDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilmActor> FilmActors { get; set; }
    }
}
