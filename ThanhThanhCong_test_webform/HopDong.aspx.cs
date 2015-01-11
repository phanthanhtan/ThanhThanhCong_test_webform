using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ThanhThanhCong_test_webform
{
    public partial class HopDong1 : System.Web.UI.Page
    {
        private TTC_HopDongThueDatEntities entity = new TTC_HopDongThueDatEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.QueryString["action"];
            if (action == "Them")
            {
                string them = Them();
                switch (them)
                {
                    case "ThemOk":
                        Response.Write("<script>alert('Thêm thành công!');</script>)");
                        break;
                    case "ThemError":
                        Response.Write("<script>alert('Có lỗi xảy ra trong quá trình thêm (có thể do trùng mã vùng). Vui lòng thao tác lại!');</script>");
                        break;
                    case "ThemErrorDienTich":
                        Response.Write("<script>alert('Có lỗi xảy ra trong quá trình thêm (chưa chọn vùng/diện tích nhập không đúng theo yêu cầu). Vui lòng thao tác lại!');</script>");
                        break;
                    default:
                        break;
                }
            }
            if (action == "Sua")
            {
                string luuIn = Request.Form["luu"];
                //int maHopDong = int.Parse(Request.QueryString["maHopDong"]);
                //string sua = Sua(maHopDong);
                //switch (sua)
                //{
                //    case "SuaOk":
                //        Response.Write("<script>alert('Sửa thành công!');</script>)");
                //        break;
                //    case "SuaError":
                //        Response.Write("<script>alert('Có lỗi xảy ra trong quá trình sửa. Vui lòng thao tác lại!');</script>");
                //        break;
                //    case "SuaErrorDienTich":
                //        Response.Write("<script>alert('Có lỗi xảy ra trong quá trình sửa (chưa chọn vùng/diện tích nhập không đúng theo yêu cầu). Vui lòng thao tác lại!');</script>");
                //        break;
                //    default:
                //        break;
                //}
            }
            if (action == "Xoa")
            {
                string maHopDong = Request.QueryString["maHopDong"];
                string xoa = Xoa(maHopDong);
                switch (xoa)
                {
                    case "XoaOk":
                        Response.Write("<script>alert('Xóa thành công!');</script>");
                        break;
                    case "XoaError":
                        Response.Write("<script>alert('Có lỗi xảy ra trong quá trình xóa. Vui lòng thao tác lại!');</script>");
                        break;
                    case "XoaNull":
                        Response.Write("<script>alert('Mã hợp đồng không có trong cơ sở dữ liệu. Vui lòng kiểm tra lại!');</script>");
                        break;
                    default:
                        break;
                }
            }
        }

        public string Them()
        {
            try
            {
                //kiểm tra xem có nhập diện tích chưa
                int checkDienTich = 1;
                float dienTich = 0;
                for (int i = 0; i < 10; i++)
                {
                    if (Request.Form["cb" + i] == "on")
                    {
                        float _dienTich = 0;
                        try
                        {
                            _dienTich = float.Parse(Request.Form["txtDienTich" + i]);
                            dienTich = dienTich + _dienTich;
                        }
                        catch
                        {
                            checkDienTich = 0;//có diện tích chưa nhập/nhập không đúng yêu cầu
                        }
                    }
                }
                if(checkDienTich == 1 && dienTich > 0)
                {
                    HopDong hd = new HopDong();
                    hd.HoTen_A1 = Request.Form["txtHoTen_A1"];
                    hd.HoTen_A2 = Request.Form["txtHoTen_A2"];
                    hd.HoTen_B1 = Request.Form["txtHoTen_B1"];
                    hd.HoTen_B2 = Request.Form["txtHoTen_B2"];

                    hd.CMND_A1 = Request.Form["txtCMND_A1"];
                    hd.CMND_A2 = Request.Form["txtCMND_A2"];
                    hd.CMND_B1 = Request.Form["txtCMND_B1"];
                    hd.CMND_B2 = Request.Form["txtCMND_B2"];

                    hd.DiaChi_A1 = Request.Form["txtDiaChi_A1"];
                    hd.DiaChi_A2 = Request.Form["txtDiaChi_A2"];
                    hd.DiaChi_B1 = Request.Form["txtDiaChi_B1"];
                    hd.DiaChi_B2 = Request.Form["txtDiaChi_B2"];

                    hd.SDT_A1 = Request.Form["txtSDT_A1"];
                    hd.SDT_A2 = Request.Form["txtSDT_A2"];
                    hd.SDT_B1 = Request.Form["txtSDT_B1"];
                    hd.SDT_B2 = Request.Form["txtSDT_B2"];

                    hd.MoiQuanHeA = Request.Form["txtMoiQuanHeA"];
                    hd.MoiQuanHeB = Request.Form["txtMoiQuanHeB"];

                    hd.KiemSoatVien = Request.Form["txtKiemSoatVien"];
                    int soVu = int.Parse(Request.Form["txtSoVu"]);
                    hd.SoVu = soVu;
                    hd.TuVu = Request.Form["txtTuVu"];
                    float donGiaThue = float.Parse(Request.Form["txtDonGiaThue"]);
                    hd.DonGiaThue = donGiaThue;
                    hd.UngTruoc = float.Parse(Request.Form["txtUngTruoc"]);

                    hd.TongTien = dienTich * donGiaThue * soVu;

                    entity.HopDong.Add(hd);
                    entity.SaveChanges();
                    HopDong hd_ma = entity.HopDong.Where(item => item.HoTen_A1 == hd.HoTen_A1
                        && item.HoTen_A2 == hd.HoTen_A2
                        && item.HoTen_B1 == hd.HoTen_B1
                        && item.HoTen_B2 == hd.HoTen_B2
                        && item.KiemSoatVien == hd.KiemSoatVien).OrderByDescending(item => item.MaHopDong).FirstOrDefault();
                    if (hd_ma != null)//lưu HopDong_ChiTiet
                    {
                        HopDong_ChiTiet hd_ct = new HopDong_ChiTiet();
                        for (int i = 0; i < 10; i++)
                        {
                            if (Request.Form["cb" + i] == "on")
                            {
                                hd_ct.MaHopDong = hd_ma.MaHopDong;
                                hd_ct.MaVung = Request.Form["txtMaVung" + i];
                                hd_ct.SoThua = Request.Form["txtSoThua" + i];
                                hd_ct.DienTich = float.Parse(Request.Form["txtDienTich" + i]);
                                hd_ct.ViTriDat = Request.Form["txtViTriDat" + i];
                                hd_ct.LoaiDat = Request.Form["txtLoaiDat" + i];
                                hd_ct.TinhTrangDat = Request.Form["txtTinhTrangDat" + i];
                                entity.HopDong_ChiTiet.Add(hd_ct);
                                entity.SaveChanges();
                            }
                        }
                        return "ThemOk";
                    }
                    else
                        return "ThemError";
                }
                else
                    return "ThemErrorDienTich";
            }
            catch
            {
                return "ThemError";
            }
        }

        public string Sua(int maHopDong)
        {
            try
            {
                //kiểm tra xem có nhập diện tích chưa
                int checkDienTich = 1;
                float dienTich = 0;
                for (int i = 0; i < 10; i++)
                {
                    if (Request.Form["cb" + i] == "on")
                    {
                        float _dienTich = 0;
                        try
                        {
                            _dienTich = float.Parse(Request.Form["txtDienTich" + i]);
                            dienTich = dienTich + _dienTich;
                        }
                        catch
                        {
                            checkDienTich = 0;//có diện tích chưa nhập/nhập không đúng yêu cầu
                        }
                    }
                }
                if (checkDienTich == 1 && dienTich > 0)
                {
                    HopDong hd = new HopDong();
                    hd.MaHopDong = maHopDong;
                    hd.HoTen_A1 = Request.Form["txtHoTen_A1"];
                    hd.HoTen_A2 = Request.Form["txtHoTen_A2"];
                    hd.HoTen_B1 = Request.Form["txtHoTen_B1"];
                    hd.HoTen_B2 = Request.Form["txtHoTen_B2"];

                    hd.CMND_A1 = Request.Form["txtCMND_A1"];
                    hd.CMND_A2 = Request.Form["txtCMND_A2"];
                    hd.CMND_B1 = Request.Form["txtCMND_B1"];
                    hd.CMND_B2 = Request.Form["txtCMND_B2"];

                    hd.DiaChi_A1 = Request.Form["txtDiaChi_A1"];
                    hd.DiaChi_A2 = Request.Form["txtDiaChi_A2"];
                    hd.DiaChi_B1 = Request.Form["txtDiaChi_B1"];
                    hd.DiaChi_B2 = Request.Form["txtDiaChi_B2"];

                    hd.SDT_A1 = Request.Form["txtSDT_A1"];
                    hd.SDT_A2 = Request.Form["txtSDT_A2"];
                    hd.SDT_B1 = Request.Form["txtSDT_B1"];
                    hd.SDT_B2 = Request.Form["txtSDT_B2"];

                    hd.MoiQuanHeA = Request.Form["txtMoiQuanHeA"];
                    hd.MoiQuanHeB = Request.Form["txtMoiQuanHeB"];

                    hd.KiemSoatVien = Request.Form["txtKiemSoatVien"];
                    int soVu = int.Parse(Request.Form["txtSoVu"]);
                    hd.SoVu = soVu;
                    hd.TuVu = Request.Form["txtTuVu"];
                    float donGiaThue = float.Parse(Request.Form["txtDonGiaThue"]);
                    hd.DonGiaThue = donGiaThue;
                    hd.UngTruoc = float.Parse(Request.Form["txtUngTruoc"]);

                    hd.TongTien = dienTich * donGiaThue * soVu;

                    entity.HopDong.Attach(hd);
                    entity.Entry(hd).State = EntityState.Modified;
                    entity.SaveChanges();

                    //xóa HopDong_ChiTiet cũ
                    List<HopDong_ChiTiet> listHD_CT = entity.HopDong_ChiTiet.Where(item => item.MaHopDong == maHopDong).ToList();
                    foreach (HopDong_ChiTiet HD_CT in listHD_CT)
                    {
                        HopDong_ChiTiet _hd_ct = entity.HopDong_ChiTiet.Single(item => item.MaHopDong_ChiTiet == HD_CT.MaHopDong_ChiTiet);
                        entity.HopDong_ChiTiet.Remove(_hd_ct);
                        entity.SaveChanges();
                    }

                    //thêm HopDong_ChiTiet mới
                    HopDong_ChiTiet hd_ct = new HopDong_ChiTiet();
                    for (int i = 0; i < 10; i++)
                    {
                        if (Request.Form["cb" + i] == "on")
                        {
                            hd_ct.MaHopDong = maHopDong;
                            hd_ct.MaVung = Request.Form["txtMaVung" + i];
                            hd_ct.SoThua = Request.Form["txtSoThua" + i];
                            hd_ct.DienTich = float.Parse(Request.Form["txtDienTich" + i]);
                            hd_ct.ViTriDat = Request.Form["txtViTriDat" + i];
                            hd_ct.LoaiDat = Request.Form["txtLoaiDat" + i];
                            hd_ct.TinhTrangDat = Request.Form["txtTinhTrangDat" + i];
                            entity.HopDong_ChiTiet.Add(hd_ct);
                            entity.SaveChanges();
                        }
                    }
                    return "SuaOk";
                }
                else
                    return "SuaErrorDienTich";
            }
            catch
            {
                return "SuaError";
            }
        }

        public string Xoa(string maHopDong)
        {
            try
            {
                int _maHopDong = int.Parse(maHopDong);
                HopDong hd = entity.HopDong.Where(item => item.MaHopDong == _maHopDong).FirstOrDefault();
                if (hd != null)
                {
                    List<HopDong_ChiTiet> listHDCT = entity.HopDong_ChiTiet.Where(item => item.MaHopDong == _maHopDong).ToList();
                    foreach (HopDong_ChiTiet hdct in listHDCT)
                    {
                        HopDong_ChiTiet _hdct = entity.HopDong_ChiTiet.Single(item => item.MaHopDong_ChiTiet == hdct.MaHopDong_ChiTiet);
                        entity.HopDong_ChiTiet.Remove(_hdct);
                        entity.SaveChanges();
                    }
                    HopDong HD = entity.HopDong.Single(item => item.MaHopDong == _maHopDong);
                    entity.HopDong.Remove(HD);
                    entity.SaveChanges();
                    return "XoaOk";//xóa ok
                }
                else
                    return "XoaNull";//null
            }
            catch
            {
                return "XoaError";//lỗi
            }
        }
    }
}