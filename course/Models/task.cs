namespace course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Дата { get; set; }

        public int Приоритет { get; set; }

        [Required]
        [StringLength(50)]
        public string Название { get; set; }

        [Column("Полное описание")]
        public string Полное_описание { get; set; }

        [StringLength(50)]
        public string Периодичность { get; set; }

        public int? Status { get; set; }

        [StringLength(50)]
        public string Логин { get; set; }

        public virtual Date Date { get; set; }

        public virtual User User { get; set; }
    }
}
