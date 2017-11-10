namespace XDPM_PHATHANHSACH.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QLPhatHanhSachDBContext : DbContext
    {
        public QLPhatHanhSachDBContext()
            : base("name=QLPhatHanhSachDBContext")
        {
        }
        public virtual DbSet<CT_DAILY> CT_DAILY { get; set; }
        public virtual DbSet<CT_NXB> CT_NXB { get; set; }
        public virtual DbSet<CT_PHIEUCHI> CT_PHIEUCHI { get; set; }
        public virtual DbSet<CT_PHIEUNHAP> CT_PHIEUNHAP { get; set; }
        public virtual DbSet<CT_PHIEUTHU> CT_PHIEUTHU { get; set; }
        public virtual DbSet<CT_PHIEUXUAT> CT_PHIEUXUAT { get; set; }
        public virtual DbSet<DAILY> DAILies { get; set; }
        public virtual DbSet<NXB> NXBs { get; set; }
        public virtual DbSet<PHIEUCHI> PHIEUCHIs { get; set; }
        public virtual DbSet<PHIEUNHAP> PHIEUNHAPs { get; set; }
        public virtual DbSet<PHIEUTHU> PHIEUTHUs { get; set; }
        public virtual DbSet<PHIEUXUAT> PHIEUXUATs { get; set; }
        public virtual DbSet<SACH> SACHes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DAILY>()
                .HasMany(e => e.CT_DAILY)
                .WithRequired(e => e.DAILY)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHIEUCHI>()
                .HasMany(e => e.CT_PHIEUCHI)
                .WithRequired(e => e.PHIEUCHI)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHIEUNHAP>()
                .HasMany(e => e.CT_PHIEUNHAP)
                .WithRequired(e => e.PHIEUNHAP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHIEUTHU>()
                .HasMany(e => e.CT_PHIEUTHU)
                .WithRequired(e => e.PHIEUTHU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHIEUXUAT>()
                .HasMany(e => e.CT_PHIEUXUAT)
                .WithRequired(e => e.PHIEUXUAT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SACH>()
                .HasMany(e => e.CT_DAILY)
                .WithRequired(e => e.SACH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SACH>()
                .HasMany(e => e.CT_PHIEUCHI)
                .WithRequired(e => e.SACH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SACH>()
                .HasMany(e => e.CT_PHIEUNHAP)
                .WithRequired(e => e.SACH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SACH>()
                .HasMany(e => e.CT_PHIEUTHU)
                .WithRequired(e => e.SACH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SACH>()
                .HasMany(e => e.CT_PHIEUXUAT)
                .WithRequired(e => e.SACH)
                .WillCascadeOnDelete(false);
        }
    }
}
