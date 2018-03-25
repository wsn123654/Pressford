namespace KPMGNews.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<News> NewsArticle { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Likes> Likes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
