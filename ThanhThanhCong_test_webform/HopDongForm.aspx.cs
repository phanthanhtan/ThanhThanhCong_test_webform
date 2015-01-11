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
            
            //if (!Page.IsPostBack)
            //{
            //    SetInitialRow();
            //}

            //int maHopDong = -1;
            //try
            //{
            //    maHopDong = int.Parse(Request.QueryString["maHopDong"]);
            //}
            //catch
            //{
            //}
            //if (!IsPostBack)
            //{
            //    List<Select_HD_CT> listHD_CT = entity.HopDong_ChiTiet.Where(item => item.MaHopDong == maHopDong).Select(item => new Select_HD_CT { MaVung = item.MaVung, SoThua = item.SoThua, DienTich = item.DienTich, ViTriDat = item.ViTriDat, LoaiDat = item.LoaiDat, TinhTrangDat = item.TinhTrangDat }).ToList();
            //    grd.DataSource = listHD_CT; // get first initial data
            //    grd.DataBind();
            //}
        }

        //private void SetInitialRow()
        //{
        //    DataTable dt = new DataTable();
        //    DataRow dr = null;
        //    dt.Columns.Add(new DataColumn("STT", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        //    dr = dt.NewRow();
        //    dr["STT"] = 1;

        //    //TTC_HopDongThueDatEntities entity = new TTC_HopDongThueDatEntities();
        //    //List<Vung> listVung = entity.Vung.ToList();
        //    //DropDownList drColumn1 = new DropDownList();
        //    //drColumn1.DataSource = listVung;

        //    dr["Column1"] = string.Empty;
        //    dr["Column2"] = string.Empty;
        //    dr["Column3"] = string.Empty;
        //    dt.Rows.Add(dr);
        //    //dr = dt.NewRow();

        //    //Store the DataTable in ViewState
        //    ViewState["CurrentTable"] = dt;

        //    DataTable dtForm = new DataTable();
        //    dtForm.Columns.Add(new DataColumn("col1", typeof(string)));
        //    dtForm.Columns.Add(new DataColumn("col2", typeof(string)));
        //    DataRow drForm = null;
        //    drForm = dtForm.NewRow();
        //    drForm["col1"] = Request.Form["txtHoTen_A1"];
        //    drForm["col2"] = Request.Form["txtHoTen_A2"];
        //    dtForm.Rows.Clear();
        //    dtForm.Rows.Add(drForm);
        //    ViewState["form"] = dtForm;



        //    Gridview1.DataSource = dt;
        //    Gridview1.DataBind();
        //}

        //private void AddNewRowToGrid()
        //{
        //    int rowIndex = 0;

        //    if (ViewState["CurrentTable"] != null)
        //    {
        //        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        //        DataRow drCurrentRow = null;
        //        if (dtCurrentTable.Rows.Count > 0)
        //        {
        //            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
        //            {
        //                //extract the TextBox values
        //                TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtMaVung");
        //                TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtSoThua");
        //                TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtDienTich");

        //                drCurrentRow = dtCurrentTable.NewRow();
        //                drCurrentRow["STT"] = i + 1;

        //                dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
        //                dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
        //                dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;

        //                rowIndex++;
        //            }
        //            dtCurrentTable.Rows.Add(drCurrentRow);
        //            ViewState["CurrentTable"] = dtCurrentTable;

        //            Gridview1.DataSource = dtCurrentTable;
        //            Gridview1.DataBind();
        //        }
        //    }
        //    else
        //    {
        //        Response.Write("ViewState is null");
        //    }

        //    //Set Previous Data on Postbacks
        //    SetPreviousData();
        //}
        //private void SetPreviousData()
        //{
        //    int rowIndex = 0;
        //    if (ViewState["CurrentTable"] != null)
        //    {
        //        DataTable dt = (DataTable)ViewState["CurrentTable"];
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtMaVung");
        //                TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtSoThua");
        //                TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtDienTich");

        //                box1.Text = dt.Rows[i]["Column1"].ToString();
        //                box2.Text = dt.Rows[i]["Column2"].ToString();
        //                box3.Text = dt.Rows[i]["Column3"].ToString();

        //                rowIndex++;
        //            }
        //        }
        //    }

        //}
        //protected void ButtonAdd_Click(object sender, EventArgs e)
        //{
        //    AddNewRowToGrid();
        //}




        //public DataTable GetTableWithInitialData(int maHopDong) // this might be your sp for select
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add("MaVung", typeof(string));
        //    table.Columns.Add("SoThua", typeof(string));
        //    table.Columns.Add("DienTich", typeof(string));
        //    table.Columns.Add("ViTriDat", typeof(string));
        //    table.Columns.Add("LoaiDat", typeof(string));
        //    table.Columns.Add("TinhTrangDat", typeof(string));

        //    List<HopDong_ChiTiet> listHD_CT = entity.HopDong_ChiTiet.Where(item => item.MaHopDong == maHopDong).ToList();
        //    foreach (HopDong_ChiTiet hd_ct in listHD_CT)
        //    {
        //        table.Rows.Add(hd_ct.MaVung, hd_ct.SoThua, hd_ct.DienTich, hd_ct.ViTriDat, hd_ct.LoaiDat, hd_ct.TinhTrangDat);
        //    }
        //    return table;
        //}

        //protected void btnAddRow_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = GetTableWithNoData(); // get select column header only records not required
        //    DataRow dr;

        //    foreach (GridViewRow gvr in grd.Rows)
        //    {
        //        dr = dt.NewRow();

                ////DropDownList MaVung = gvr.FindControl("MaVung") as DropDownList;
                ////TextBox SoThua = gvr.FindControl("SoThua") as TextBox;
                ////TextBox DienTich = gvr.FindControl("DienTich") as TextBox;
                ////TextBox ViTriDat = gvr.FindControl("ViTriDat") as TextBox;
                ////TextBox LoaiDat = gvr.FindControl("LoaiDat") as TextBox;
                ////TextBox TinhTrangDat = gvr.FindControl("TinhTrangDat") as TextBox;

                ////dr[0] = MaVung.SelectedIndex;
                ////dr[1] = SoThua.Text;
                ////dr[2] = DienTich.Text;
                ////dr[3] = ViTriDat.Text;
                ////dr[4] = LoaiDat.Text;
                ////dr[5] = TinhTrangDat.Text;
        //        dr[0] = "HT";
        //        dr[1] = "1";
        //        dr[2] = "2";
        //        dr[3] = "3";
        //        dr[4] = "4";
        //        dr[5] = "5";

        //        dt.Rows.Add(dr); // add grid values in to row and add row to the blank table
        //    }

        //    dr = dt.NewRow(); // add last empty row
        //    dt.Rows.Add(dr);

        //    grd.DataSource = dt; // bind new datatable to grid
        //    grd.DataBind();
        //}

        //public DataTable GetTableWithNoData() // returns only structure if the select columns
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add("MaVung", typeof(string));
        //    table.Columns.Add("SoThua", typeof(string));
        //    table.Columns.Add("DienTich", typeof(string));
        //    table.Columns.Add("ViTriDat", typeof(string));
        //    table.Columns.Add("LoaiDat", typeof(string));
        //    table.Columns.Add("TinhTrangDat", typeof(string));
        //    return table;
        //}

        //protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    //if (e.Row.RowType == DataControlRowType.DataRow)
        //    //{
        //    //    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=TTC_HopDongThueDat;Integrated Security=True");//"Data Source=.;Initial Catalog=TTC_HopDongThueDat;Integrated Security=True;Database=TTC_HopDongThueDat;");
        //    //    DropDownList dd1 = (DropDownList)e.Row.FindControl("DropDownList1");
        //    //    SqlCommand cmd = new SqlCommand("select * from Vung",con);
        //    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    //    DataSet ds = new DataSet();
        //    //    da.Fill(ds);
        //    //    dd1.DataSource = ds;
        //    //    dd1.DataTextField = "TenVung";
        //    //    dd1.DataValueField = "MaVung";
        //    //    dd1.DataBind();
        //    //}
        //}
    }
}