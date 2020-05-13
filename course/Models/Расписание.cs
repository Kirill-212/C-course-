namespace course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Расписание
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column("День недели")]
        [Required]
        [StringLength(50)]
        public string День_недели { get; set; }

        [Required]
        [StringLength(50)]
        public string Логин { get; set; }

        public int? Неделя { get; set; }

        [Required]
        [StringLength(50)]
        public string Предмет { get; set; }

        [Required]
        [StringLength(50)]
        public string Время { get; set; }

        public virtual User User { get; set; }
    }
}
