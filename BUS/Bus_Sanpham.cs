using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class Bus_Sanpham
    {
        QL_BanHoaEntities1 db = new QL_BanHoaEntities1();

        //public List<SanPham> GetAllSanPham()
        //{
        //    //var result = from c in db.SanPhams
        //    //             select new
        //    //             {
        //    //                 MaSP = c.MaSP,
        //    //                 MaLoai = c.MaLoai,
        //    //                 TenSP = c.TenSP,
        //    //                 DVT = c.DVT
        //    //             };
        //    //return result.ToList(); 
        //}

        public void InsertSanPham(int loaisp, string tensp,string dvt,string noisx,float gianhap,float giaban,DateTime ngaynhap,byte[] img)
        {
            SanPham sp = new SanPham();
            sp.MaLoai = loaisp;
            sp.TenSP = tensp;
            sp.DVT = dvt;
            sp.NoiSanXuat = noisx;
            sp.GiaNhap = gianhap;
            sp.GiaBan = giaban;
            sp.NgayNhap = ngaynhap;
            sp.HinhAnh = img;
            db.SanPhams.Add(sp);
            db.SaveChanges();
        }

        public bool UpdateSanPham(int id,int loaisp, string tensp, string dvt , string noisx , float gianhap, float giaban, DateTime ngaynhap, byte[] img)
        {
            SanPham sp = db.SanPhams.Where(c => c.MaSP == id).FirstOrDefault();
            if (sp != null)
            {
                sp.MaLoai = loaisp;
                sp.TenSP = tensp;
                sp.DVT = dvt;
                sp.NoiSanXuat = noisx;
                sp.GiaNhap = gianhap;
                sp.GiaBan = giaban;
                sp.NgayNhap = ngaynhap;
                sp.HinhAnh = img;
              
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public SanPham GetSanPhamByID(int id)
        {
            SanPham sp = db.SanPhams.Where(c => c.MaSP == id).FirstOrDefault();
            return sp;
        }


        public bool DeteteSanPham(int id)
        {

            SanPham sp = db.SanPhams.Where(c => c.MaSP == id).FirstOrDefault();

            if (sp != null)
            {
                db.SanPhams.Remove(sp);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
