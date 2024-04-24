using System;
using System.Linq;

namespace codeF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new Model1())
            {
                // Thêm dữ liệu vào bảng Congty
                var congTy = new Congty { Ten = "Công ty A" };
                db.Congtys.Add(congTy);
                db.SaveChanges();

                // Thêm dữ liệu vào bảng PhongBan
                var phongBan = new PhongBan { Ten = "Marketing", CongtyId = congTy.Id };
                db.PhongBans.Add(phongBan);
                db.SaveChanges();

                // Thêm dữ liệu vào bảng NhanVien
                var nhanVien = new NhanVien { Ten = "Nguyen Van A", Tuoi = 35, GioiTinh = "Nam", PhongBanId = phongBan.Id };
                db.NhanViens.Add(nhanVien);
                db.SaveChanges();

                // Truy vấn và hiển thị thông tin
                var nhanViens = (from nv in db.NhanViens
                                 join pb in db.PhongBans on nv.PhongBanId equals pb.Id
                                 join ct in db.Congtys on pb.CongtyId equals ct.Id
                                 where nv.GioiTinh == "Nam" &&
                                       pb.Ten == "Marketing" &&
                                       nv.Tuoi >= 30 && nv.Tuoi <= 40
                                 select new
                                 {
                                     TenNhanVien = nv.Ten,
                                     Tuoi = nv.Tuoi,
                                     TenPhongBan = pb.Ten,
                                     TenCongTy = ct.Ten
                                 }).ToList();

                Console.WriteLine("Danh sách nhân viên nam thuộc phòng Marketing và có tuổi từ 30 đến 40:");
                foreach (var nv in nhanViens)
                {
                    Console.WriteLine($"Tên: {nv.TenNhanVien}, Tuổi: {nv.Tuoi}, Phòng ban: {nv.TenPhongBan}, Công ty: {nv.TenCongTy}");
                }
            }

            Console.ReadLine();
        }
    }
}
