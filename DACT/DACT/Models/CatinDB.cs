using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DACT.Models
{
    public partial class CatinDB : DbContext
    {
        public CatinDB()
            : base("name=CatinDB1")
        {
        }

        public virtual DbSet<ChiTietVA> ChiTietVAs { get; set; }
        public virtual DbSet<MonAn> MonAns { get; set; }
        public virtual DbSet<QuyenTruyCap> QuyenTruyCaps { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Taikhoan> Taikhoans { get; set; }
        public virtual DbSet<Theloai> Theloais { get; set; }
        public virtual DbSet<VeAn> VeAns { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MonAn>()
                .Property(e => e.GiaMon)
                .HasPrecision(18, 0);

            modelBuilder.Entity<QuyenTruyCap>()
                .Property(e => e.TenQuyenTruyCap)
                .IsFixedLength();

            modelBuilder.Entity<Theloai>()
                .HasMany(e => e.MonAns)
                .WithRequired(e => e.Theloai)
                .WillCascadeOnDelete(false);
        }
    }
}
