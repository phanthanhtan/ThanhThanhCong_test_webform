using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ThanhThanhCong_test_webform
{
    public partial class Vung1 : System.Web.UI.Page
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
            if(action == "Xoa")
            {
                string maVung = Request.QueryString["maVung"];
                string xoa = Xoa(maVung);
                switch (xoa)
                {
                    case "Xoa_Exist":
                        Response.Write("<script>alert('Mã vùng được sử dụng trong hợp đồng. Vui lòng kiểm tra lại!');</script>)");
                        break;
                    case "XoaOk":
                        Response.Write("<script>alert('Xóa thành công!');</script>");
                        break;
                    case "XoaError":
                        Response.Write("<script>alert('Có lỗi xảy ra trong quá trình xóa. Vui lòng thao tác lại!');</script>");
                        break;
                    case "XoaNull":
                        Response.Write("<script>alert('Mã vùng không có trong cơ sở dữ liệu. Vui lòng kiểm tra lại!');</script>");
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

        public string Xoa(string maVung)
        {
            try
            {
                Vung vung = entity.Vung.Where(item => item.MaVung == maVung).FirstOrDefault();
                if (vung != null)
                {
                    HopDong_ChiTiet hd_ct = entity.HopDong_ChiTiet.Where(item => item.MaVung == maVung).FirstOrDefault();
                    if (hd_ct != null)
                        return "Xoa_Exist";//maVung có trong hợp đồng
                    else
                    {
                        try
                        {
                            Vung v = entity.Vung.Single(item => item.MaVung == maVung);
                            entity.Vung.Remove(v);
                            entity.SaveChanges();
                            return "XoaOk";//xóa ok
                        }
                        catch
                        {
                            return "XoaError";//lỗi
                        }
                    }
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