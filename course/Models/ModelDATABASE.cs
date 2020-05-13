namespace course.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelDATABASE : DbContext
    {
        public ModelDATABASE()
            : base("name=ModelDATABASE")
        {
        }

        public virtual DbSet<Date> Dates { get; set; }
        public virtual DbSet<task> tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Расписание> Расписание { get; set; }
        public virtual DbSet<Расходы> Расходы { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Date>()
                .HasMany(e => e.tasks)
                .WithRequired(e => e.Date)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Расписание)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Расходы)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Расходы>()
                .Property(e => e.Стоимость)
                .HasPrecision(19, 4);
        }
    }
}
