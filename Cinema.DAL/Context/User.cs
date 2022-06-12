namespace Cinema.DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Admins = new HashSet<Admin>();
        }

        public int UserId { get; set; }

        [Required]
        [Index("IX_UserLogin", 1, IsUnique=true)]
        [StringLength(100)]
        public string UserLogin { get; set; }

        [Required]
        [StringLength(100)]
        public string UserPassword { get; set; }

        [Required]
        [StringLength(50)]
        public string UserFirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string UserLastName { get; set; }

        [StringLength(30)]
        [Index("IX_UserPhone", 1, IsUnique = true)]
        public string UserPhone { get; set; }

        [StringLength(100)]
        [Index("IX_UserEmail", 1, IsUnique = true)]
        public string UserEmail { get; set; }

        public bool IsActive { get; set; }

        public DateTime UserLastLoginDateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admin> Admins { get; set; }
    }
}
