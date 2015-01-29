<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Vung.aspx.cs" Inherits="ThanhThanhCong_test_webform.Vung1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<center>
    <%  if (Session["user"] != null && Session["per"].ToString() == "1")
        {
    %>
    <h2>Quản lý vùng</h2>
    <a href="/VungForm.aspx">Thêm vùng</a><br /><br />
    <table cellspacing="0" cellpadding="0" border="1" style="text-align:center">
        <tr>
            <td>Mã vùng</td>
            <td>Tên vùng</td>
            <td>Chức năng</td>
        </tr>
        <%  ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities entity = new ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities();
            List<ThanhThanhCong_test_webform.Vung> listVung = entity.Vung.ToList();
            if (listVung.Count == 0)
            {%>
        <tr>
            <td colspan="2">Chưa có dữ liệu về vùng.</td>
        </tr>
            <%}
            else
            {
                foreach (ThanhThanhCong_test_webform.Vung v in listVung)
                {%>
                <tr>
                    <td><%=v.MaVung %></td>
                    <td><%=v.TenVung %></td>
                    <td><a href="/VungForm.aspx?maVung=<%=v.MaVung %>" title="Sửa">Sửa</a>
                        <a href="/Vung.aspx?action=Xoa&maVung=<%=v.MaVung %>" title="Xóa" onclick="return confirm('Bạn chắc chắn muốn xóa?!?')">Xóa</a>
                    </td>
                </tr>
                <%}
            }
            %>
    </table>
    <% }
       else
       {%>
            Chức năng này chỉ dành cho Admin.<br />
            Bấm vào <a href="/DangNhap.aspx">đây</a> để đăng nhập.
       <%} %>
</center>
</asp:Content>
