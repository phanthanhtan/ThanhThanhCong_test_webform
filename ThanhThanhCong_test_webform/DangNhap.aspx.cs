using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ThanhThanhCong_test_webform
{
    public partial class DangNhap : System.Web.UI.Page
    {
        private TTC_HopDongThueDatEntities entity = new TTC_HopDongThueDatEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string action = Request.QueryString["action"];
                if (action == "out")//đăng xuất
                {
                    Session["user"] = null;
                    Session["per"] = null;
                }
                else//đăng nhập
                {
                    string id = Request.Form["txtID"];
                    string pass = Request.Form["txtPass"];
                    if (id != null && pass != null)
                    {
                        User u = entity.User.Where(item => item.ID.Equals(id) && item.Pass.Equals(pass)).FirstOrDefault();
                        if (u != null)
                        {
                            Session["user"] = u.ID;
                            Session["per"] = u.Permission;
                            Response.Write("<script>alert('Đăng nhập thành công!');</script>");
                        }
                        else
                        {
                            Session["user"] = null;
                            Session["per"] = null;
                            Response.Write("<script>alert('Sai tên đăng nhập hoặc mật khẩu. Vui lòng kiểm tra lại!');</script>");
                        }
                    }
                    else
                    {
                        Session["user"] = null;
                        Session["per"] = null;
                    }
                }
            }
            catch
            {
                Session["user"] = null;
                Session["per"] = null;
                Response.Write("<script>alert('Có lỗi xảy ra. Vui lòng thao tác lại!');</script>");
            }
        }
    }
}