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
            if (Session["user"] != null)//user + admin
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
                        case "inOk":
                            Response.Write("<script>alert('Lưu và in thành công!');</script>");
                            break;
                        default:
                            break;
                    }
                }
                if (Session["per"].ToString() == "1")//admin
                {
                    if (action == "Sua")
                    {
                        string sua = Sua();
                        switch (sua)
                        {
                            case "SuaOk":
                                Response.Write("<script>alert('Sửa thành công!');</script>)");
                                break;
                            case "SuaError":
                                Response.Write("<script>alert('Có lỗi xảy ra trong quá trình sửa. Vui lòng thao tác lại!');</script>");
                                break;
                            case "SuaErrorDienTich":
                                Response.Write("<script>alert('Có lỗi xảy ra trong quá trình sửa (chưa chọn vùng/diện tích nhập không đúng theo yêu cầu). Vui lòng thao tác lại!');</script>");
                                break;
                            case "inOk":
                                Response.Write("<script>alert('Lưu và in thành công!');</script>");
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

                    hd.TongTien = (dienTich * donGiaThue * soVu).ToString();

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
                                hd_ct.DienTich = (float.Parse(Request.Form["txtDienTich" + i])).ToString();
                                hd_ct.ViTriDat = Request.Form["txtViTriDat" + i];
                                hd_ct.LoaiDat = Request.Form["txtLoaiDat" + i];
                                hd_ct.TinhTrangDat = Request.Form["txtTinhTrangDat" + i];
                                entity.HopDong_ChiTiet.Add(hd_ct);
                                entity.SaveChanges();
                            }
                        }
                        String str = "ThemOk";
                        string luuIn = Request.Form["luu"];
                        if (luuIn == "Lưu và in")
                        {
                            InHopDong(hd_ma.MaHopDong);
                            str = "inOk";
                        }
                        return str;
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

        public string Sua()
        {
            try
            {
                int maHopDong = int.Parse(Request.Form["txtMaHopDong"]);
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

                    hd.TongTien = (dienTich * donGiaThue * soVu).ToString();

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
                            hd_ct.DienTich = (float.Parse(Request.Form["txtDienTich" + i])).ToString();
                            hd_ct.ViTriDat = Request.Form["txtViTriDat" + i];
                            hd_ct.LoaiDat = Request.Form["txtLoaiDat" + i];
                            hd_ct.TinhTrangDat = Request.Form["txtTinhTrangDat" + i];
                            entity.HopDong_ChiTiet.Add(hd_ct);
                            entity.SaveChanges();
                        }
                    }
                    String str = "SuaOk";
                    string luuIn = Request.Form["luu"];
                    if (luuIn == "Lưu và in")
                    {
                        InHopDong(maHopDong);
                        str = "inOk";
                    }
                    return str;
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

        public void InHopDong(int maHopDong)
        {
            try
            {
                HopDong hd = entity.HopDong.Where(item => item.MaHopDong == maHopDong).FirstOrDefault();
                List<HopDong_ChiTiet> listhd_ct = entity.HopDong_ChiTiet.Where(item => item.MaHopDong == maHopDong).ToList();
                //Tạo các đối tượng Excel
                Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbooks oBooks;
                Microsoft.Office.Interop.Excel.Sheets oSheets;
                Microsoft.Office.Interop.Excel.Workbook oBook;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;

                //Tạo mới một Excel WorkBook
                oExcel.Visible = true;
                oExcel.DisplayAlerts = false;
                oExcel.Application.SheetsInNewWorkbook = 1;
                oBooks = oExcel.Workbooks;

                oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
                oSheets = oBook.Worksheets;
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
                oSheet.Name = "Hop dong thue dat";

                // Tạo phần đầu nếu muốn
                Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "I1");
                head.MergeCells = true;
                head.Value2 = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
                head.Font.Bold = true;
                head.Font.Name = "Times New Roman";
                head.Font.Size = "12";
                head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                Microsoft.Office.Interop.Excel.Range head1 = oSheet.get_Range("A2", "I2");
                head1.MergeCells = true;
                head1.Value2 = "Độc lập - Tự do - Hạnh phúc";
                head1.Font.Bold = true;
                head1.Font.Name = "Times New Roman";
                head1.Font.Size = "12";
                head1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                Microsoft.Office.Interop.Excel.Range head2 = oSheet.get_Range("A3", "I3");
                head2.MergeCells = true;
                head2.Value2 = "-----";
                head2.Font.Bold = true;
                head2.Font.Name = "Times New Roman";
                head2.Font.Size = "12";
                head2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                Microsoft.Office.Interop.Excel.Range head3 = oSheet.get_Range("A5", "I5");
                head3.MergeCells = true;
                head3.Value2 = "HỢP ĐỒNG THUÊ ĐẤT";
                head3.Font.Bold = true;
                head3.Font.Name = "Times New Roman";
                head3.Font.Size = "14";
                head3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                Microsoft.Office.Interop.Excel.Range canCu = oSheet.get_Range("A7", "I7");
                canCu.MergeCells = true;
                canCu.Value2 = "  - Căn cứ Bộ luật Dân sự số 33/2005/QH11;";

                Microsoft.Office.Interop.Excel.Range canCu1 = oSheet.get_Range("A8", "I8");
                canCu1.MergeCells = true;
                canCu1.Value2 = "  - Căn cứ vào nhu cầu và khả năng của 2 bên.";

                Microsoft.Office.Interop.Excel.Range ngay = oSheet.get_Range("A10", "I10");
                ngay.MergeCells = true;
                ngay.Value2 = "  Hôm nay, ngày ... tháng ... năm ...... tại trạm nông vụ số ...... Chúng tôi gồm có:";
                ngay.Font.Italic = true;

                //Bên A ----------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Range benA = oSheet.get_Range("A12", "A12");
                benA.MergeCells = true;
                benA.Value2 = "Bên A:";
                benA.Font.Bold = true;
                benA.Font.Underline = true;
                // A1 ------------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Range benA1_1 = oSheet.get_Range("B12", "F12");
                benA1_1.MergeCells = true;
                benA1_1.Value2 = "Bên cho thuê: " + hd.HoTen_A1;

                Microsoft.Office.Interop.Excel.Range benA1_2 = oSheet.get_Range("G12", "I12");
                benA1_2.MergeCells = true;
                benA1_2.Value2 = "Số CMND: " + hd.CMND_A1;

                Microsoft.Office.Interop.Excel.Range benA1_3 = oSheet.get_Range("B13", "F14");
                benA1_3.MergeCells = true;
                benA1_3.Value2 = "Địa chỉ: " + hd.DiaChi_A1;
                benA1_3.WrapText = true;

                Microsoft.Office.Interop.Excel.Range benA1_4 = oSheet.get_Range("G13", "I13");
                benA1_4.MergeCells = true;
                benA1_4.Value2 = "Điện thoại: " + hd.SDT_A1;
                // A2--------------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Range benA2_1 = oSheet.get_Range("B15", "F15");
                benA2_1.MergeCells = true;
                benA2_1.Value2 = "Họ tên người thừa kế: " + hd.HoTen_A2;

                Microsoft.Office.Interop.Excel.Range benA2_2 = oSheet.get_Range("G15", "I15");
                benA2_2.MergeCells = true;
                benA2_2.Value2 = "Số CMND: " + hd.CMND_A2;

                Microsoft.Office.Interop.Excel.Range benA2_3 = oSheet.get_Range("B16", "I16");
                benA2_3.MergeCells = true;
                benA2_3.Value2 = "Mối quan hệ: " + hd.MoiQuanHeA;

                Microsoft.Office.Interop.Excel.Range benA2_4 = oSheet.get_Range("B17", "F18");
                benA2_4.MergeCells = true;
                benA2_4.Value2 = "Địa chỉ: " + hd.DiaChi_A2;
                benA2_4.WrapText = true;

                Microsoft.Office.Interop.Excel.Range benA2_5 = oSheet.get_Range("G17", "I17");
                benA2_5.MergeCells = true;
                benA2_5.Value2 = "Điện thoại: " + hd.SDT_A2;
                //Bên B ----------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Range benB = oSheet.get_Range("A19", "A19");
                benB.MergeCells = true;
                benB.Value2 = "Bên B:";
                benB.Font.Bold = true;
                benB.Font.Underline = true;
                // B1 ------------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Range benB1_1 = oSheet.get_Range("B19", "F19");
                benB1_1.MergeCells = true;
                benB1_1.Value2 = "Bên thuê đất: " + hd.HoTen_B1;

                Microsoft.Office.Interop.Excel.Range benB1_2 = oSheet.get_Range("G19", "I19");
                benB1_2.MergeCells = true;
                benB1_2.Value2 = "Số CMND: " + hd.CMND_B1;

                Microsoft.Office.Interop.Excel.Range benB1_3 = oSheet.get_Range("B20", "F21");
                benB1_3.MergeCells = true;
                benB1_3.Value2 = "Địa chỉ: " + hd.DiaChi_B1;
                benB1_3.WrapText = true;

                Microsoft.Office.Interop.Excel.Range benB1_4 = oSheet.get_Range("G20", "I20");
                benB1_4.MergeCells = true;
                benB1_4.Value2 = "Điện thoại: " + hd.SDT_B1;
                // B2--------------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Range benB2_1 = oSheet.get_Range("B22", "F22");
                benB2_1.MergeCells = true;
                benB2_1.Value2 = "Người thừa kế hợp đồng: " + hd.HoTen_B2;

                Microsoft.Office.Interop.Excel.Range benB2_2 = oSheet.get_Range("G22", "I22");
                benB2_2.MergeCells = true;
                benB2_2.Value2 = "Số CMND: " + hd.CMND_B2;

                Microsoft.Office.Interop.Excel.Range benB2_3 = oSheet.get_Range("B23", "I23");
                benB2_3.MergeCells = true;
                benB2_3.Value2 = "Mối quan hệ: " + hd.MoiQuanHeB;

                Microsoft.Office.Interop.Excel.Range benB2_4 = oSheet.get_Range("B24", "F25");
                benB2_4.MergeCells = true;
                benB2_4.Value2 = "Địa chỉ: " + hd.DiaChi_B2;
                benB2_4.WrapText = true;

                Microsoft.Office.Interop.Excel.Range benB2_5 = oSheet.get_Range("G24", "I24");
                benB2_5.MergeCells = true;
                benB2_5.Value2 = "Điện thoại: " + hd.SDT_B2;
                //het ben B

                Microsoft.Office.Interop.Excel.Range ksv = oSheet.get_Range("A26", "I27");
                ksv.MergeCells = true;
                ksv.Value2 = "Và sự chứng kiến của Ông (Bà) " + hd.KiemSoatVien + ". Chức vụ: Kiểm soát viên tại Công ty cổ phần mía đường Thành Thành Công Tây Ninh (TTCS).";
                ksv.WrapText = true;

                Microsoft.Office.Interop.Excel.Range haiBen = oSheet.get_Range("B28", "I28");
                haiBen.MergeCells = true;
                haiBen.Value2 = "Hai bên cùng thống nhất các điều khoản sau đây:";
                haiBen.Font.Bold = true;
                haiBen.Font.Italic = true;

                //điều 1-------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Range dieu1_1 = oSheet.get_Range("A29", "I29");
                dieu1_1.MergeCells = true;
                dieu1_1.Value2 = "Điều 1: Bên A đồng ý cho bên B thuê đất với chi tiết như sau:";
                dieu1_1.Font.Bold = true;

                Microsoft.Office.Interop.Excel.Range HAIBEN = oSheet.get_Range("A7", "I29");
                HAIBEN.Font.Name = "Times New Roman";
                HAIBEN.Font.Size = "12";
                HAIBEN.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignJustify;
                HAIBEN.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;

                Microsoft.Office.Interop.Excel.Range dieu1_2 = oSheet.get_Range("B30", "B31");
                dieu1_2.MergeCells = true;
                dieu1_2.Value2 = "Mã vùng";

                Microsoft.Office.Interop.Excel.Range dieu1_3 = oSheet.get_Range("C30", "C31");
                dieu1_3.MergeCells = true;
                dieu1_3.Value2 = "Số thửa";

                Microsoft.Office.Interop.Excel.Range dieu1_4 = oSheet.get_Range("D30", "D31");
                dieu1_4.MergeCells = true;
                dieu1_4.Value2 = "Diện tích (ha)";

                Microsoft.Office.Interop.Excel.Range dieu1_5 = oSheet.get_Range("E30", "F31");
                dieu1_5.MergeCells = true;
                dieu1_5.Value2 = "Ví trí đất";

                Microsoft.Office.Interop.Excel.Range dieu1_6 = oSheet.get_Range("G30", "H31");
                dieu1_6.MergeCells = true;
                dieu1_6.Value2 = "Loại đất (cao/thấp/...)";

                Microsoft.Office.Interop.Excel.Range dieu1_7 = oSheet.get_Range("I30", "I31");
                dieu1_7.MergeCells = true;
                dieu1_7.Value2 = "Tình trạng đất";

                Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("B30", "I31");
                rowHead.WrapText = true;
                rowHead.Font.Name = "Times New Roman";
                rowHead.Font.Size = "12";
                rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                rowHead.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                // Kẻ viền
                rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

                int row = 32;
                foreach (HopDong_ChiTiet hd_ct in listhd_ct)
                {
                    Microsoft.Office.Interop.Excel.Range col1 = oSheet.get_Range("B" + row, "B" + row);
                    col1.MergeCells = true;
                    col1.Value2 = hd_ct.MaVung;

                    Microsoft.Office.Interop.Excel.Range col2 = oSheet.get_Range("C" + row, "C" + row);
                    col2.MergeCells = true;
                    col2.Value2 = hd_ct.SoThua;

                    Microsoft.Office.Interop.Excel.Range col3 = oSheet.get_Range("D" + row, "D" + row);
                    col3.MergeCells = true;
                    col3.Value2 = hd_ct.DienTich;

                    Microsoft.Office.Interop.Excel.Range col4 = oSheet.get_Range("E" + row, "F" + row);
                    col4.MergeCells = true;
                    col4.Value2 = hd_ct.ViTriDat;

                    Microsoft.Office.Interop.Excel.Range col5 = oSheet.get_Range("G" + row, "H" + row);
                    col5.MergeCells = true;
                    col5.Value2 = hd_ct.LoaiDat;

                    Microsoft.Office.Interop.Excel.Range col6 = oSheet.get_Range("I" + row, "I" + row);
                    col6.MergeCells = true;
                    col6.Value2 = hd_ct.TinhTrangDat;

                    row++;
                }
                Microsoft.Office.Interop.Excel.Range data = oSheet.get_Range("B32", "I" + (row - 1));
                data.Font.Name = "Times New Roman";
                data.Font.Size = "12";
                data.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                // Kẻ viền
                data.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

                //điều 2----------------------------------------------------------------------------------
                int dieu2_start = row;
                Microsoft.Office.Interop.Excel.Range dieu2_1 = oSheet.get_Range("A" + row, "I" + row);
                dieu2_1.MergeCells = true;
                dieu2_1.Value2 = "Điều 2: Mục đích sử dụng";
                dieu2_1.Font.Bold = true;
                row++;

                int soVu = hd.SoVu;
                int tuVu = int.Parse(hd.TuVu);
                string _soVu = soVu.ToString();
                if (soVu < 10)
                    _soVu = "0" + soVu.ToString();
                Microsoft.Office.Interop.Excel.Range dieu2_2 = oSheet.get_Range("A" + row, "I" + (row + 1));
                dieu2_2.MergeCells = true;
                dieu2_2.Value2 = "Bên B sẽ sử dụng đất thuê trên để trồng mía cung cấp cho Công ty cổ phần mía đường Thành Thành Công Tây Ninh (TTCS) trong " + _soVu + " vụ thu hoạch liên tiếp, kể từ vụ thu hoạch " + tuVu + "-" + (tuVu + 1) + ".";
                dieu2_2.WrapText = true;
                row += 2;

                //điều 3----------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Range dieu3_1 = oSheet.get_Range("A" + row, "I" + row);
                dieu3_1.MergeCells = true;
                dieu3_1.Value2 = "Điều 3: Thời hạn thuê đất";
                dieu3_1.Font.Bold = true;
                row++;

                Microsoft.Office.Interop.Excel.Range dieu3_2 = oSheet.get_Range("A" + row, "I" + row);
                dieu3_2.MergeCells = true;
                dieu3_2.Value2 = _soVu + " vụ, kể từ ngày ký hợp đồng này cho đến khi kết thúc vụ thu hoạch " + (tuVu + soVu - 1) + "-" + (tuVu + soVu) + ".";
                row++;

                //điều 4----------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Range dieu4_1 = oSheet.get_Range("A" + row, "I" + row);
                dieu4_1.MergeCells = true;
                dieu4_1.Value2 = "Điều 4: Đơn giá thuê";
                dieu4_1.Font.Bold = true;
                row++;

                Microsoft.Office.Interop.Excel.Range dieu4_2 = oSheet.get_Range("A" + row, "I" + row);
                dieu4_2.MergeCells = true;
                dieu4_2.Value2 = hd.DonGiaThue + " đồng/ha/năm";
                row++;

                Microsoft.Office.Interop.Excel.Range dieu4_3 = oSheet.get_Range("A" + row, "I" + row);
                dieu4_3.MergeCells = true;
                dieu4_3.Value2 = "Tổng số tiền thuê đất: " + hd.TongTien + " đồng.";
                row++;

                //điều 5----------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Range dieu5_1 = oSheet.get_Range("A" + row, "I" + row);
                dieu5_1.MergeCells = true;
                dieu5_1.Value2 = "Điều 5: Phương thức, thời hạn thanh toán";
                dieu5_1.Font.Bold = true;
                row++;

                Microsoft.Office.Interop.Excel.Range dieu5_2 = oSheet.get_Range("A" + row, "I" + (row + 1));
                dieu5_2.MergeCells = true;
                dieu5_2.Value2 = "Bên B thanh toán đủ một lần tiền thuê đất cho toàn bộ thời gian thuê đã nêu ở trên ngay vụ trồng đầu tiên. Cụ thể như sau:";
                dieu5_2.WrapText = true;
                row += 2;

                Microsoft.Office.Interop.Excel.Range dieu5_3 = oSheet.get_Range("A" + row, "I" + row);
                dieu5_3.MergeCells = true;
                dieu5_3.Value2 = "1. Bên B sẽ ứng trước cho bên A số tiền là " + hd.UngTruoc + " đồng sau khi ký Hợp đồng này.";
                row++;

                Microsoft.Office.Interop.Excel.Range dieu5_4 = oSheet.get_Range("A" + row, "I" + (row + 1));
                dieu5_4.MergeCells = true;
                dieu5_4.Value2 = "2. Số tiền còn lại bên B sẽ thanh toán hết cho bên A ngay sau khi nhận tiền từ hợp đồng Ứng vốn về ciệc thuê đất trồng mía từ TTCS hoặc bên A nhận trực tiếp tại TTCS.";
                dieu5_4.WrapText = true;
                row += 2;


                //điều 6----------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Range dieu6_1 = oSheet.get_Range("A" + row, "I" + row);
                dieu6_1.MergeCells = true;
                dieu6_1.Value2 = "Điều 6: Quyền và nghĩa vụ của bên A";
                dieu6_1.Font.Bold = true;
                row++;

                Microsoft.Office.Interop.Excel.Range dieu6_2 = oSheet.get_Range("A" + row, "I" + (row + 1));
                dieu6_2.MergeCells = true;
                dieu6_2.Value2 = "1. Chuyển giao cho bên B đủ diện tích đất thuê, đúng vị trí, đúng tình trạng, như đã thỏa thuận tại Điều 1 Hợp đồng này.";
                dieu6_2.WrapText = true;
                row += 2;

                Microsoft.Office.Interop.Excel.Range dieu6_3 = oSheet.get_Range("A" + row, "I" + (row + 1));
                dieu6_3.MergeCells = true;
                dieu6_3.Value2 = "2. Thực hiện các thủ tục và thanh toán cac khoản thuế, phí, lệ phí cho nhà nước cũng như các phí khác trong quá trình cho thuê.";
                dieu6_3.WrapText = true;
                row += 2;

                Microsoft.Office.Interop.Excel.Range dieu6_4 = oSheet.get_Range("A" + row, "I" + (row + 1));
                dieu6_4.MergeCells = true;
                dieu6_4.Value2 = "3. Đảm bảo quyền sử dụng hợp pháp của mình đối với diện tích đất cho thuê, nếu có tranh chấp bên A sẽ phải bồi thường toàn bộ thiệt hại gây ra cho bên B do việc tranh chấp.";
                dieu6_4.WrapText = true;
                row += 2;

                Microsoft.Office.Interop.Excel.Range dieu6_5 = oSheet.get_Range("A" + row, "I" + (row + 3));
                dieu6_5.MergeCells = true;
                dieu6_5.Value2 = "4. Trong thời hạn cho thuê quyền sử dụng đất, nếu bên A chuyển nhượng quyền sử dụng đất cho người khác, bên A phải báo trước cho bên B và TTCS biết. Việc chuyển nhượng này phải được sự nhất trí của bên B và TTCS bằng văn bản. Khi đó các bên cùng thống nhất các nội dung:";
                dieu6_5.WrapText = true;
                row += 4;

                Microsoft.Office.Interop.Excel.Range dieu6_6 = oSheet.get_Range("B" + row, "I" + (row + 2));
                dieu6_6.MergeCells = true;
                dieu6_6.Value2 = "a. Nếu bên nhận chuyển nhượng tiếp tục cho thuê, thì Hợp đồng này vẫn tiếp tục thực hiện và bên nhận chuyển nhượng thực hiện tiếp nghĩa vụ của bên A theo hợp đồng này cho đến hết thời hạn hợp đồng.";
                dieu6_6.WrapText = true;
                row += 3;

                Microsoft.Office.Interop.Excel.Range dieu6_7 = oSheet.get_Range("B" + row, "I" + (row + 2));
                dieu6_7.MergeCells = true;
                dieu6_7.Value2 = "b. Nếu bên nhận chuyển nhượng không thể tiếp tục cho thuê, xem như bên A vi phạm hợp đồng. Khi đó bên A phải thanh toán cho bên B các khoản theo như xác định tại khoản 5, Điều này.";
                dieu6_7.WrapText = true;
                row += 3;

                Microsoft.Office.Interop.Excel.Range dieu6_8 = oSheet.get_Range("A" + row, "I" + (row + 5));
                dieu6_8.MergeCells = true;
                dieu6_8.Value2 = "5. Bên A không được quyền đơn phương chấm dứt hợp đồng. Nếu đơn phương chấm dứt hợp đồng, bên A phải hoàn trả lại cho bên B số tiền thuê đất theo giá thỏa thuận giữa hai bên tùy tình trạng thực tế của đất mía nhưng tối thiểu phải bằng giá thuê theo Hợp đồng này tính trên số năm đã thuê nhưng khôgn thực hiện. Ngoài ra, bên A còn phải bồi thường toàn bộ thiệt hại cho bên B do việc đơn phương chấm dứt hợp đồng của bên A gây ra và chịu phạt vi phạm hợp đồng bằng 50% giá trị hợp đồng.";
                dieu6_8.WrapText = true;
                row += 6;

                Microsoft.Office.Interop.Excel.Range dieu6_9 = oSheet.get_Range("A" + row, "I" + (row + 2));
                dieu6_9.MergeCells = true;
                dieu6_9.Value2 = "Số tiền hoàn lại, tiền phạt vi phạm hợp đồng và tiền bồi thường phải được chi trả thông qua TTCS để bên B hoàn trả số tiền ứng trước và tiền lãi tương ứng trên diện tích vi phạm cho TTCS.";
                dieu6_9.WrapText = true;
                row += 3;

                //điều 7----------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Range dieu7_1 = oSheet.get_Range("A" + row, "I" + row);
                dieu7_1.MergeCells = true;
                dieu7_1.Value2 = "Điều 7: Quyền và nghĩa vụ của bên B";
                dieu7_1.Font.Bold = true;
                row++;

                Microsoft.Office.Interop.Excel.Range dieu7_2 = oSheet.get_Range("A" + row, "I" + row);
                dieu7_2.MergeCells = true;
                dieu7_2.Value2 = "1. Sử dụng đất thuê đúng mục đích, đúng ranh giới và đúng thời hạn thuê.";
                row++;

                Microsoft.Office.Interop.Excel.Range dieu7_3 = oSheet.get_Range("A" + row, "I" + (row + 5));
                dieu7_3.MergeCells = true;
                dieu7_3.Value2 = "2. Diện tích đất cho thuê trong Hợp đồng này được thuê bởi bên B do TTCS đầu tư một phần tiền thuê, do đó nếu bên B không thực hiện đúng trách nhiệm của mình đối với bên cho thuê hoặc vi phạm những thỏa thuận trong các hợp đồng với TTCS thì TTCS có quyền chỉ định người khác tiếp tục thực hiện Hợp đồng này. Khi đó bên A có trách nhiệm ký hợp đồng mới với người được TTCS chỉ định và thực hiện tiếp các nghĩa vụ theo đúng các điều khoản đã ký của Hợp đồng này.";
                dieu7_3.WrapText = true;
                row += 6;

                Microsoft.Office.Interop.Excel.Range dieu7_4 = oSheet.get_Range("A" + row, "I" + row);
                dieu7_4.MergeCells = true;
                dieu7_4.Value2 = "3. Thanh toán đúng hạn và đầy đủ tiền thuê đất cho bên A theo quy định của Hợp đồng.";
                row++;

                Microsoft.Office.Interop.Excel.Range dieu7_5 = oSheet.get_Range("A" + row, "I" + (row + 1));
                dieu7_5.MergeCells = true;
                dieu7_5.Value2 = "4. Không cho người khác thuê lại hoặc sử dụng đất vào mục đích khác theo quy định của Hợp đồng trừ trường hợp được bên A và TTCS đồng ý.";
                dieu7_5.WrapText = true;
                row += 2;

                Microsoft.Office.Interop.Excel.Range dieu7_6 = oSheet.get_Range("A" + row, "I" + row);
                dieu7_6.MergeCells = true;
                dieu7_6.Value2 = "5. Không được hủy hoại, làm giảm sút giá trị sử dụng đất, tài sản gắn liền với đất.";
                row++;

                Microsoft.Office.Interop.Excel.Range dieu7_7 = oSheet.get_Range("A" + row, "I" + (row + 1));
                dieu7_7.MergeCells = true;
                dieu7_7.Value2 = "6. Không được quyền đơn phương chấm dứt hợp đồng, trừ trường hợp nêu tại khoản 2 điều này.";
                dieu7_7.WrapText = true;
                row += 2;

                //điều 8----------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Range dieu8_1 = oSheet.get_Range("A" + row, "I" + row);
                dieu8_1.MergeCells = true;
                dieu8_1.Value2 = "Điều 8: Cam kết chung";
                dieu8_1.Font.Bold = true;
                row++;

                Microsoft.Office.Interop.Excel.Range dieu8_2 = oSheet.get_Range("A" + row, "I" + (row + 2));
                dieu8_2.MergeCells = true;
                dieu8_2.Value2 = "Hai bên cam kết thực hiện theo đúng những điều khoản đã ghi trong Hợp đồng. Trong thời gian cho thuê nếu xảy ra tranh chấp hai bên tích cực bàn bạc tìm cách giải quyết. Trường hợp không giải quyết được mới đua ra Tòa án có thẩm quyền giải quyết.";
                dieu8_2.WrapText = true;
                row += 3;

                Microsoft.Office.Interop.Excel.Range dieu8_3 = oSheet.get_Range("A" + row, "I" + (row + 1));
                dieu8_3.MergeCells = true;
                dieu8_3.Value2 = "Hợp đồng này lập thành 04 (bốn) bản có giá trị pháp lý như nhau, bên A giữ 01 (một) bản và bên B giữ 03 (ba) bản.";
                dieu8_3.Font.Italic = true;
                dieu8_3.WrapText = true;
                row += 2;

                Microsoft.Office.Interop.Excel.Range dieu2dieu8 = oSheet.get_Range("A" + dieu2_start, "I" + (row - 1));
                dieu2dieu8.Font.Name = "Times New Roman";
                dieu2dieu8.Font.Size = "12";
                dieu2dieu8.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignJustify;
                dieu2dieu8.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
                //---------------------------------------------------------------------------------
                Microsoft.Office.Interop.Excel.Range UBND = oSheet.get_Range("A" + row, "I" + row);
                UBND.MergeCells = true;
                UBND.Value2 = "Chứng thực của UBND xã .....................................";
                UBND.Font.Bold = true;
                UBND.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                UBND.Font.Name = "Times New Roman";
                UBND.Font.Size = "12";
                row += 7;

                Microsoft.Office.Interop.Excel.Range final1_1 = oSheet.get_Range("A" + row, "D" + row);
                final1_1.MergeCells = true;
                final1_1.Value2 = "BÊN A";

                Microsoft.Office.Interop.Excel.Range final1_2 = oSheet.get_Range("E" + row, "F" + row);
                final1_2.MergeCells = true;
                final1_2.Value2 = "ĐẠI DIỆN SBT";

                Microsoft.Office.Interop.Excel.Range final1_3 = oSheet.get_Range("G" + row, "I" + row);
                final1_3.MergeCells = true;
                final1_3.Value2 = "BÊN B";

                Microsoft.Office.Interop.Excel.Range final1 = oSheet.get_Range("A" + row, "I" + row);
                final1.Font.Bold = true;
                final1.Font.Underline = true;
                final1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                final1.Font.Name = "Times New Roman";
                final1.Font.Size = "12";
                row++;

                Microsoft.Office.Interop.Excel.Range final2_1 = oSheet.get_Range("A" + row, "D" + row);
                final2_1.MergeCells = true;
                final2_1.Value2 = "Người cho thuê      Người thừa kế";

                Microsoft.Office.Interop.Excel.Range final2_2 = oSheet.get_Range("E" + row, "F" + row);
                final2_2.MergeCells = true;
                final2_2.Value2 = "Kiếm soát viên";

                Microsoft.Office.Interop.Excel.Range final2_3 = oSheet.get_Range("G" + row, "I" + row);
                final2_3.MergeCells = true;
                final2_3.Value2 = "Người thuê     Người thừa kế";

                Microsoft.Office.Interop.Excel.Range final2 = oSheet.get_Range("A" + row, "I" + row);
                final2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                final2.Font.Name = "Times New Roman";
                final2.Font.Size = "12";

            //quản lý in-------------------------------------------------------------------------
                try
                {
                    HopDong_in hd_in = new HopDong_in();
                    hd_in.MaHopDong = hd.MaHopDong;
                    hd_in.UserID = Session["user"].ToString();
                    hd_in.Time = DateTime.Now.ToString(); ;
                    entity.HopDong_in.Add(hd_in);
                    entity.SaveChanges();
                }
                catch
                {
                }
            }
            catch
            {
            }
        }

    }
}