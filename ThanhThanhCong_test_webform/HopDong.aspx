<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="HopDong.aspx.cs" Inherits="ThanhThanhCong_test_webform.HopDong1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
    <%  if (Session["user"] != null && Session["per"].ToString() == "1")
        {
    %>
    <h2>Danh sách hợp đồng thuê đất</h2>
    <a href="/HopDongForm.aspx">Tạo mới hợp đồng</a><br /><br />
    <table cellspacing="0" cellpadding="0" border="1" style="text-align:center">
        <tr>
            <td style="width:100px">Mã hợp đồng</td>
            <td style="width:200px">Bên cho thuê</td>
            <td style="width:200px">Bên thuê đất</td>
            <td style="width:50px">Số vụ</td>
            <td style="width:50px">Từ vụ</td>
            <td>Đơn giá thuê</td>
            <td>Tổng tiền</td>
            <td>Ứng trước</td>
            <td>Chức năng</td>
        </tr>
        <%  ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities entity = new ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities();
            List<ThanhThanhCong_test_webform.HopDong> listHopDong = entity.HopDong.OrderByDescending(item => item.MaHopDong).ToList();
            if (listHopDong.Count == 0)
            {%>
                <tr>
                    <td colspan="8">Chưa có dữ liệu về hợp đồng.</td>
                </tr>
            <%}
            else
            {
                foreach (ThanhThanhCong_test_webform.HopDong hd in listHopDong)
                {%>
                    <tr>
                        <td><%=hd.MaHopDong %></td>
                        <td><%=hd.HoTen_A1 %></td>
                        <td><%=hd.HoTen_B1 %></td>
                        <td><%=hd.SoVu %></td>
                        <td><%=hd.TuVu %></td>
                        <td style="text-align:right"><%=hd.DonGiaThue.ToString("0,0") %></td>
                        <td style="text-align:right"><%=hd.TongTien %></td>
                        <td style="text-align:right"><%=hd.UngTruoc.ToString("0,0") %></td>
                        <td><a href="/HopDongForm.aspx?maHopDong=<%=hd.MaHopDong %>" title="Xem chi tiết/Sửa">Chi tiết</a>
                            <a href="/HopDong.aspx?action=Xoa&maHopDong=<%=hd.MaHopDong %>" title="Xóa" onclick="return confirm('Bạn chắc chắn muốn xóa?!?')">Xóa</a>
                        </td>
                    </tr>
                <%}
            }%>
    </table>
    <% }
       else
       {%>
            Chức năng này chỉ dành cho Admin.<br />
            Bấm vào <a href="/DangNhap.aspx">đây</a> để đăng nhập.
       <%} %>
</center>
</asp:Content>
