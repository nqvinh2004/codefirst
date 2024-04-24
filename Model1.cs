using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace codeF
{
    public class NhanVien
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Ten { get; set; }

        public int Tuoi { get; set; }

        [StringLength(10)]
        public string GioiTinh { get; set; }

        public int PhongBanId { get; set; }

        [ForeignKey("PhongBanId")]
        public PhongBan PhongBan { get; set; }
    }

    public class PhongBan
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Ten { get; set; }

        public int CongtyId { get; set; }

        [ForeignKey("CongtyId")]
        public Congty Congty { get; set; }

        public ICollection<NhanVien> NhanViens { get; set; }
    }

    public class Congty
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Ten { get; set; }

        public ICollection<PhongBan> PhongBans { get; set; }
    }

    public partial class Model1 : DbContext
    {
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<PhongBan> PhongBans { get; set; }
        public DbSet<Congty> Congtys { get; set; }

        public Model1()
            : base("name=Model1")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Thiết lập các quan hệ và cấu hình mô hình ở đây
            modelBuilder.Entity<NhanVien>()
                .HasKey(nv => nv.Id);
            modelBuilder.Entity<PhongBan>()
                .HasKey(pb => pb.Id);
            modelBuilder.Entity<Congty>()
                .HasKey(ct => ct.Id);

            // Thiết lập quan hệ giữa các bảng
            modelBuilder.Entity<NhanVien>()
                .HasRequired(nv => nv.PhongBan)
                .WithMany(pb => pb.NhanViens)
                .HasForeignKey(nv => nv.PhongBanId);

            modelBuilder.Entity<PhongBan>()
                .HasRequired(pb => pb.Congty)
                .WithMany(ct => ct.PhongBans)
                .HasForeignKey(pb => pb.CongtyId);
        }
    }
}
