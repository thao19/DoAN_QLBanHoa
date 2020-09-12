using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class Bus_Nhanvien
    {
        QL_BanHoaEntities1 db = new QL_BanHoaEntities1();

        public List<NhanVien> GetAllNhanvien()
        {
            return db.NhanViens.ToList();


        }

        public bool CheckLogin(string tk, string mk)
        {
            int kt = db.TaiKhoans.Where(c => c.TaiKhoan1 == tk && c.MatKhau == mk).Count();

            if (kt == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<string> getAllQuyen()
        {
            return db.Quyens.Select(c => c.TenQuyen).ToList();
        }

        public NhanVien GetNhanvienByID(int id)
        {

            NhanVien nv = db.NhanViens.Where(c => c.MaNV == id).FirstOrDefault();
            return nv;
        }

        public TaiKhoan GetTaiKhoanNhanVien(int id)
        {
            TaiKhoan tk = db.TaiKhoans.Where(c => c.MaNV == id).FirstOrDefault();
            return tk;
        }

        public bool InsertTaiKhoanNV(int manv, string tk, string mk, int maquyen)
        {
            if (checkTK(tk))
            {
                TaiKhoan tknv = new TaiKhoan();
                tknv.MaNV = manv;
                tknv.TaiKhoan1 = tk;
                tknv.MatKhau = mk;
                tknv.MaQuyen = maquyen;
                db.TaiKhoans.Add(tknv);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateTaiKhoanNV(int manv,string tk,  string mk, int maquyen)
        {
            TaiKhoan tknv = db.TaiKhoans.Where(c=>c.TaiKhoan1 == tk).FirstOrDefault();
            if (tknv != null)
            {
                                
                tknv.MatKhau = mk;
                tknv.MaQuyen = maquyen;
               
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }


        }

        public bool checkTK(string tk)
        {
            int count = db.TaiKhoans.Where(c => c.TaiKhoan1 == tk).Count();

            if (count == 1)
            {
                return false;
            }
            else return true;
        }

        public bool CheckMaNV(int id)
        {
            int ktra = db.NhanViens.Where(c => c.MaNV == id).Count();
            if (ktra == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertNhanvien(string hoten, bool gioitinh, DateTime ngaysinh, string quequan, string diachi, string dienthoai, string email)
        {
            try
            {
                NhanVien nv = new NhanVien();
                nv.HoTen = hoten;
                nv.Gioitinh = gioitinh;
                nv.NgaySinh = ngaysinh;
                nv.QueQuan = quequan;
                nv.DiaChi = diachi;
                nv.SDT = dienthoai;
                nv.Email = email;
                db.NhanViens.Add(nv);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool UpdateNhanvien(int id, string hoten, bool gioitinh, DateTime ngaysinh, string quequan, string diachi, string dienthoai, string email)
        {
            NhanVien nv = db.NhanViens.Where(c => c.MaNV == id).FirstOrDefault();
            if (nv != null)
            {
                nv.HoTen = hoten;
                nv.Gioitinh = gioitinh;
                nv.NgaySinh = ngaysinh;
                nv.QueQuan = quequan;
                nv.DiaChi = diachi;
                nv.SDT = dienthoai;
                nv.Email = email;

                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeteteNhanvien(int id)
        {

            NhanVien nv = db.NhanViens.Where(c => c.MaNV == id).FirstOrDefault();

            if (nv != null)
            {
                db.NhanViens.Remove(nv);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<NhanVien> FindNVWithName(string name)
        {
            return db.NhanViens.Where(c => c.HoTen.Contains(name)).ToList();
        }
    }
}
