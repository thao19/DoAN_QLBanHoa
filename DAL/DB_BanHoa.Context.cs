﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QL_BanHoaEntities1 : DbContext
    {
        public QL_BanHoaEntities1()
            : base("name=QL_BanHoaEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ChiTietHD> ChiTietHDs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<LoaiSP> LoaiSPs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<Quyen> Quyens { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
    }
}
