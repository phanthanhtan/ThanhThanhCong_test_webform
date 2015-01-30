<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="HopDongin.aspx.cs" Inherits="ThanhThanhCong_test_webform.HopDongin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
    <%  if (Session["user"] != null && Session["per"].ToString() == "1")
        {
    %>
    <%  int maHopDong = 0;
        try
        {
            maHopDong = int.Parse(Request.QueryString["maHopDong"]);
        }
        catch { }
        int count = 0;
        List<ThanhThanhCong_test_webform.HopDong_in> list = null;
        ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities entity_in = new ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities();
        if (maHopDong != 0)
        {
            list = entity_in.HopDong_in.Where(item => item.MaHopDong == maHopDong).ToList();
            count = list.Count;
        }
        else
        {
            list = entity_in.HopDong_in.OrderByDescending(item => item.MaHopDong).ToList();
            count = list.Count;
        }
         %>
    <h2>Hợp đồng - Số lần in</h2>
    <table>
        <tr>
            <td>Mã hợp đồng: </td>
            <td><input id="txtMaHopDong" name="txtMaHopDong" maxlength="50" onkeypress="return CheckNumber(event)" /></td>
            <td><input type="button" style="width:50px" value="Xem" onclick="return CheckForm()"/></td>
        </tr>
    </table>
    <script>
        function CheckNumber(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function CheckForm() {
            if (txtMaHopDong.value == "" || txtMaHopDong.value == null) {
                alert("Chưa nhập mã hợp đồng.");
                txtMaHopDong.focus();
                return false;
            }
            else {
                //alert(window.location.host + window.location.pathname + '?maHopDong=' + txtMaHopDong.value);
                var str = 'http://' + window.location.host + window.location.pathname + '?maHopDong=' + txtMaHopDong.value;
                window.location = str;
            }
        }
    </script>
    <br />
    Mã hợp đồng: <% if (maHopDong != 0) {%><%=maHopDong %><%} else {%> Tất cả hợp đồng<%} %>
    <table cellspacing="0" cellpadding="0" border="1" style="text-align:center">
        <tr>
            <td>Mã HĐ - Tên đăng nhập</td>
            <td>Thời gian in</td>
        </tr>
        <%  if (count > 0)
            {
                foreach (ThanhThanhCong_test_webform.HopDong_in hd_in in list)
                {%>
        <tr>
            <td><%=hd_in.MaHopDong %> - <%=hd_in.UserID %></td>
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
