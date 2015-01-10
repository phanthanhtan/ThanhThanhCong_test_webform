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
                    default:
                        break;
                }
            }
            if (action == "Sua")
            {
                string maVung = Request.QueryString["maVung"];
                string sua = Sua(maVung);
                switch (sua)
                {
                    case "SuaOk":
                        Response.Write("<script>alert('Sửa thành công!');</script>)");
                        break;
                    case "SuaError":
                        Response.Write("<script>alert('Có lỗi xảy ra trong quá trình sửa. Vui lòng thao tác lại!');</script>");
                        break;
                    default:
                        break;
                }
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
                Vung v = new Vung();
                v.MaVung = Request.Form["txtMaVung"];
                v.TenVung = Request.Form["txtTenVung"];
                entity.Vung.Add(v);
                entity.SaveChanges();
                return "ThemOk";
            }
            catch
            {
                return "ThemError";
            }
        }

        public string Sua(string maVung)
        {
            try
            {
                Vung v = new Vung();
                v.MaVung = Request.Form["txtMaVung"];
                v.TenVung = Request.Form["txtTenVung"];
                entity.Vung.Attach(v);
                entity.Entry(v).State = EntityState.Modified;
                entity.SaveChanges();
                return "SuaOk";
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