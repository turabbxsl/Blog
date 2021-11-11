namespace Blogsayt.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class databaseblog : DbContext
    {
        public databaseblog()
            : base("name=databaseblog")
        {
        }

        public virtual DbSet<tbl_etiket> tbl_etiket { get; set; }
        public virtual DbSet<tbl_kategoriya> tbl_kategoriya { get; set; }
        public virtual DbSet<tbl_makale> tbl_makale { get; set; }
        public virtual DbSet<tbl_yetki> tbl_yetki { get; set; }
        public virtual DbSet<tbl_yorum> tbl_yorum { get; set; }
        public virtual DbSet<tbl_kullanici> tbl_kullanici { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_etiket>()
                .HasMany(e => e.tbl_makale)
                .WithMany(e => e.tbl_etiket)
                .Map(m => m.ToTable("tbl_etiket-makale").MapLeftKey("etiketid").MapRightKey("makaleid"));

            modelBuilder.Entity<tbl_kategoriya>()
                .HasMany(e => e.tbl_makale)
                .WithRequired(e => e.tbl_kategoriya)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_makale>()
                .HasMany(e => e.tbl_yorum)
                .WithRequired(e => e.tbl_makale)
                .HasForeignKey(e => e.yorummakaleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_yetki>()
                .HasMany(e => e.tbl_kullanici)
                .WithRequired(e => e.tbl_yetki)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_kullanici>()
                .HasMany(e => e.tbl_makale)
                .WithRequired(e => e.tbl_kullanici)
                .HasForeignKey(e => e.kullaniciid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_kullanici>()
                .HasMany(e => e.tbl_yorum)
                .WithRequired(e => e.tbl_kullanici)
                .HasForeignKey(e => e.yorumkullaniciID)
                .WillCascadeOnDelete(false);
        }
    }
}
