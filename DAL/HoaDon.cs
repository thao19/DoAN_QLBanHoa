//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class HoaDon
    {
        public HoaDon()
        {
            this.ChiTietHDs = new HashSet<ChiTietHD>();
        }
    
        public int SoHD { get; set; }
        public System.DateTime Ngay { get; set; }
        public int MaKH { get; set; }
        public int MaNV { get; set; }
        public double ThanhTien { get; set; }
    
        public virtual ICollection<ChiTietHD> ChiTietHDs { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}