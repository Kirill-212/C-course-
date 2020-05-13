namespace course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            tasks = new HashSet<task>();
            Расписание = new HashSet<Расписание>();
            Расходы = new HashSet<Расходы>();
        }

        [Key]
        [StringLength(50)]
        public string Логин { get; set; }

        public int Пароль { get; set; }

        [Column("e-maeil")]
        [Required]
        [StringLength(50)]
        public string e_maeil { get; set; }

        [StringLength(50)]
        public string Роль { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<task> tasks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Расписание> Расписание { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Расходы> Расходы { get; set; }
    }
}
