using Microsoft.EntityFrameworkCore;
using QlySukienSV.Models;

namespace QlySukienSV.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CLB> CLBs { get; set; }
        public DbSet<ThanhVienCLB> ThanhVienCLBs { get; set; }
        public DbSet<SuKien> SuKiens { get; set; }
        public DbSet<DangKySuKien> DangKySuKiens { get; set; }
        public DbSet<DiemDanhSuKien> DiemDanhSuKiens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure unique constraints for NguoiDung
            modelBuilder.Entity<NguoiDung>()
                .HasIndex(s => s.Email)
                .IsUnique();

            // Configure Role unique constraint
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.TenRole)
                .IsUnique();

            // Configure CLB relationships
            modelBuilder.Entity<CLB>()
                .HasOne(c => c.ChuNhiem)
                .WithMany()
                .HasForeignKey(c => c.ChuNhiemId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure ThanhVienCLB relationships
            modelBuilder.Entity<ThanhVienCLB>()
                .HasOne(tv => tv.CLB)
                .WithMany(c => c.ThanhViens)
                .HasForeignKey(tv => tv.CLBId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ThanhVienCLB>()
                .HasOne(tv => tv.NguoiDung)
                .WithMany(s => s.ThanhVienCLBs)
                .HasForeignKey(tv => tv.NguoiDungId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure SuKien relationships
            modelBuilder.Entity<SuKien>()
                .HasOne(sk => sk.CLB)
                .WithMany(c => c.SuKiens)
                .HasForeignKey(sk => sk.CLBId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure DangKySuKien relationships
            modelBuilder.Entity<DangKySuKien>()
                .HasOne(dk => dk.SuKien)
                .WithMany(sk => sk.DangKySuKiens)
                .HasForeignKey(dk => dk.SuKienId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DangKySuKien>()
                .HasOne(dk => dk.NguoiDung)
                .WithMany(s => s.DangKySuKiens)
                .HasForeignKey(dk => dk.NguoiDungId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure DiemDanhSuKien relationships
            modelBuilder.Entity<DiemDanhSuKien>()
                .HasOne(dd => dd.SuKien)
                .WithMany(sk => sk.DiemDanhSuKiens)
                .HasForeignKey(dd => dd.SuKienId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DiemDanhSuKien>()
                .HasOne(dd => dd.NguoiDung)
                .WithMany(s => s.DiemDanhSuKiens)
                .HasForeignKey(dd => dd.NguoiDungId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Seed();
        }
    }
}
