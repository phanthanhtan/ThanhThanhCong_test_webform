<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="HopDongin.aspx.cs" Inherits="ThanhThanhCong_test_webform.HopDongin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<center>
    <%  if (Session["user"] != null && Session["per"].ToString() == "1")
        {
    %>
    <%  int maHopDong = -1;
        try
        {
            maHopDong = int.Parse(Request.QueryString["maHopDong"]);
        }
        catch { }
        int count = 0;
        List<ThanhThanhCong_test_webform.HopDong_in> list = null;
        if (maHopDong != -1)
        {
            ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities entity_in = new ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities();
            list = entity_in.HopDong_in.Where(item => item.MaHopDong == maHopDong).ToList();
            count = list.Count;
        }
         %>
    <h2>Hợp đồng - Quản lý in</h2>
    Mã hợp đồng: <%=maHopDong %>
    <table cellspacing="0" cellpadding="0" border="1" style="text-align:center">
        <tr>
            <td>Tên đăng nhập</td>
            <td>Thời gian in</td>
        </tr>
        <%  if (count > 0)
            {
                foreach (ThanhThanhCong_test_webform.HopDong_in hd_in in list)
                {%>
        <tr>
            <td><%=hd_in.UserID %></td>
            <td><%=hd_in.Time %></td>
        </tr>
                <%}
            }
            else//không có
            {%>
        <tr>
            <td colspan="2">Mã hợp đồng này chưa được in hoặc không có trong cơ sở dữ liệu.</td>
        </tr>
            <%}
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
