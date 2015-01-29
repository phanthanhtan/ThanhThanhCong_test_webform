<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="VungForm.aspx.cs" Inherits="ThanhThanhCong_test_webform.VungForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%  if (Session["user"] != null && Session["per"].ToString() == "1")
        {
    %>
<%  string action = "Them";
    string _action = "Thêm";
    string maVung = Request.QueryString["maVung"];
    ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities entity = new ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities();
    ThanhThanhCong_test_webform.Vung v = null;
    if (maVung != null)
    {
        v = entity.Vung.Where(item => item.MaVung == maVung).FirstOrDefault();
        if (v != null)
        {
            action = "Sua";
            _action = "Sửa";
        }
    }
%>
<center>
    <h2>Quản lý vùng - <%=_action %></h2>
    <table>
    <form action="/Vung.aspx?action=<%=action %>" method="post" name="FVung" id="FVung" enctype ="multipart/form-data" onsubmit ="return CheckForm()">
        <tr>
            <td>Mã vùng: </td>
            <td><input id="txtMaVung" name="txtMaVung" maxlength="50" <%if (action == "Sua") {%> value="<%=v.MaVung %>" readonly ="true" <%} %>/></td>
        </tr>
        <tr>
            <td>Tên vùng: </td>
            <td><input id="txtTenVung" name="txtTenVung" maxlength="50" <%if (action == "Sua") {%> value="<%=v.TenVung %>" <%} %>/></td>
        </tr>
        <tr>
            <td></td>
            <td><input type="submit" style="width:50px" <%if (action == "Sua"){%> value="Sửa"<%}else{%>value="Thêm" <%}%>/></td>
        </tr>
    </form>
        <tr>
            <%  string link = "";
                if (action == "Sua")
                    link = "?maVung=" + v.MaVung;
            %>
            <td></td><td><a href="/VungForm.aspx<%=link %>"><button style="width:50px">Hủy</button></a></td>
        </tr>
    </table>
</center>
<script>
    function CheckForm() {
        if (txtMaVung.value == "" || txtMaVung.value == null) {
            alert("Chưa nhập mã vùng.");
            txtMaVung.focus();
            return false;
        }
        if (txtTenVung.value == "" || txtTenVung.value == null) {
            alert("Chưa nhập tên vùng.");
            txtTenVung.focus();
            return false;
        }
    }
</script>
    <% }
       else
       {%>
            <center>
            Chức năng này chỉ dành cho Admin.<br />
            Bấm vào <a href="/DangNhap.aspx">đây</a> để đăng nhập.
            </center>
       <%} %>
</asp:Content>
