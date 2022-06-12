namespace Cinema.DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FilmActor")]
    public partial class FilmActor
    {
        public int FilmActorId { get; set; }

        public int FilmId { get; set; }

        public int ActorId { get; set; }

        public virtual Actor Actor { get; set; }

        public virtual Film Film { get; set; }
    }
}
