//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ThanhThanhCong_test_webform
{
    using System;
    using System.Collections.Generic;
    
    public partial class HopDong
    {
        public HopDong()
        {
            this.HopDong_ChiTiet = new HashSet<HopDong_ChiTiet>();
            this.HopDong_in = new HashSet<HopDong_in>();
        }
    
        public int MaHopDong { get; set; }
        public string HoTen_A1 { get; set; }
        public string CMND_A1 { get; set; }
        public string DiaChi_A1 { get; set; }
        public string SDT_A1 { get; set; }
        public string HoTen_A2 { get; set; }
        public string CMND_A2 { get; set; }
        public string MoiQuanHeA { get; set; }
        public string DiaChi_A2 { get; set; }
        public string SDT_A2 { get; set; }
        public string HoTen_B1 { get; set; }
        public string CMND_B1 { get; set; }
        public string DiaChi_B1 { get; set; }
        public string SDT_B1 { get; set; }
        public string HoTen_B2 { get; set; }
        public string CMND_B2 { get; set; }
        public string MoiQuanHeB { get; set; }
        public string DiaChi_B2 { get; set; }
        public string SDT_B2 { get; set; }
        public string KiemSoatVien { get; set; }
        public int SoVu { get; set; }
        public string TuVu { get; set; }
        public double DonGiaThue { get; set; }
        public string TongTien { get; set; }
        public double UngTruoc { get; set; }
    
        public virtual ICollection<HopDong_ChiTiet> HopDong_ChiTiet { get; set; }
        public virtual ICollection<HopDong_in> HopDong_in { get; set; }
    }
}
