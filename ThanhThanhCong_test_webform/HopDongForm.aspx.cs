using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ThanhThanhCong_test_webform
{
    public partial class HopDongForm : System.Web.UI.Page
    {
        private TTC_HopDongThueDatEntities entity = new TTC_HopDongThueDatEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            int maHopDong = -1;
            try
            {
                maHopDong = int.Parse(Request.QueryString["maHopDong"]);
            }
            catch
            {
            }
            if (!IsPostBack)
            {
                List<HopDong_ChiTiet> listHD_CT = entity.HopDong_ChiTiet.Where(item => item.MaHopDong == maHopDong).ToList();
                grd.DataSource = listHD_CT; // get first initial data
                grd.DataBind();
            }
        }
        public DataTable GetTableWithInitialData(int maHopDong) // this might be your sp for select
        {
            DataTable table = new DataTable();
            table.Columns.Add("MaVung", typeof(string));
            table.Columns.Add("SoThua", typeof(string));
            table.Columns.Add("DienTich", typeof(string));
            table.Columns.Add("ViTriDat", typeof(string));
            table.Columns.Add("LoaiDat", typeof(string));
            table.Columns.Add("TinhTrangDat", typeof(string));

            List<HopDong_ChiTiet> listHD_CT = entity.HopDong_ChiTiet.Where(item => item.MaHopDong == maHopDong).ToList();
            foreach (HopDong_ChiTiet hd_ct in listHD_CT)
            {
                table.Rows.Add(hd_ct.MaVung, hd_ct.SoThua, hd_ct.DienTich, hd_ct.ViTriDat, hd_ct.LoaiDat, hd_ct.TinhTrangDat);
            }
            return table;
        }

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            DataTable dt = GetTableWithNoData(); // get select column header only records not required
            DataRow dr;

            foreach (GridViewRow gvr in grd.Rows)
            {
                dr = dt.NewRow();

                //DropDownList MaVung = gvr.FindControl("MaVung") as DropDownList;
                //TextBox SoThua = gvr.FindControl("SoThua") as TextBox;
                //TextBox DienTich = gvr.FindControl("DienTich") as TextBox;
                //TextBox ViTriDat = gvr.FindControl("ViTriDat") as TextBox;
                //TextBox LoaiDat = gvr.FindControl("LoaiDat") as TextBox;
                //TextBox TinhTrangDat = gvr.FindControl("TinhTrangDat") as TextBox;

                //dr[0] = MaVung.SelectedIndex;
                //dr[1] = SoThua.Text;
                //dr[2] = DienTich.Text;
                //dr[3] = ViTriDat.Text;
                //dr[4] = LoaiDat.Text;
                //dr[5] = TinhTrangDat.Text;
                dr[0] = "HT";
                dr[1] = "1";
                dr[2] = "2";
                dr[3] = "3";
                dr[4] = "4";
                dr[5] = "5";

                dt.Rows.Add(dr); // add grid values in to row and add row to the blank table
            }

            dr = dt.NewRow(); // add last empty row
            dt.Rows.Add(dr);

            grd.DataSource = dt; // bind new datatable to grid
            grd.DataBind();
        }

        public DataTable GetTableWithNoData() // returns only structure if the select columns
        {
            DataTable table = new DataTable();
            table.Columns.Add("MaVung", typeof(string));
            table.Columns.Add("SoThua", typeof(string));
            table.Columns.Add("DienTich", typeof(string));
            table.Columns.Add("ViTriDat", typeof(string));
            table.Columns.Add("LoaiDat", typeof(string));
            table.Columns.Add("TinhTrangDat", typeof(string));
            return table;
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=TTC_HopDongThueDat;Integrated Security=True");//"Data Source=.;Initial Catalog=TTC_HopDongThueDat;Integrated Security=True;Database=TTC_HopDongThueDat;");
            //    DropDownList dd1 = (DropDownList)e.Row.FindControl("DropDownList1");
            //    SqlCommand cmd = new SqlCommand("select * from Vung",con);
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    da.Fill(ds);
            //    dd1.DataSource = ds;
            //    dd1.DataTextField = "TenVung";
            //    dd1.DataValueField = "MaVung";
            //    dd1.DataBind();
            //}
        }
    }
}