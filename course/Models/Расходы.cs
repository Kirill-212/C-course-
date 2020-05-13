namespace course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Расходы
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Логин { get; set; }

        [Column("Группы товара")]
        [Required]
        [StringLength(50)]
        public string Группы_товара { get; set; }

        [Required]
        [StringLength(50)]
        public string Периодиность { get; set; }

        [Column(TypeName = "money")]
        public decimal Стоимость { get; set; }

        public virtual User User { get; set; }
    }
}
