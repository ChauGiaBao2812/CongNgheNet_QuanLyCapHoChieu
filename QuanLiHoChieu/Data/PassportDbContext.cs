using Microsoft.EntityFrameworkCore;
using QuanLiHoChieu.Models;
using QuanLiHoChieu.Models.ViewModels;

namespace QuanLiHoChieu.Data
{
    public class PassportDbContext : DbContext
    {
        public PassportDbContext(DbContextOptions<PassportDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<PassportData> PassportDatas { get; set; } = null!;
        public DbSet<XuLy> XuLys { get; set; } = null!;
        public DbSet<ResidentData> ResidentDatas { get; set; } = null!;
        public DbSet<LuuTru> LuuTrus { get; set; } = null!;
        public DbSet<AuditLog> AuditLog { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optional: make table names singular if needed
            modelBuilder.Entity<TaiKhoan>().ToTable("TaiKhoan");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<PassportData>().ToTable("PassportData");
            modelBuilder.Entity<XuLy>().ToTable("XuLy");
            modelBuilder.Entity<ResidentData>().ToTable("ResidentData");
            modelBuilder.Entity<LuuTru>().ToTable("LuuTru");
            modelBuilder.Entity<AuditLog>().ToTable("AuditLog");

            // Unique constraint on FormID in LuuTru (only needed if not using [Index])
            modelBuilder.Entity<LuuTru>()
                .HasIndex(l => l.FormID)
                .IsUnique();

            // Set up composite key or constraints if any (none in your case so far)

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DecryptedUserVM>(e =>
            {
                e.HasNoKey();
                e.ToView(null);
            });
        }
    }
}
