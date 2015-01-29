<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="DangNhap.aspx.cs" Inherits="ThanhThanhCong_test_webform.DangNhap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
    <%  if (Session["user"] == null)
        {%>
            <form action="/DangNhap.aspx" method="post" id="FDangNhap" name="FDangNhap" onsubmit ="return CheckForm()">
            <table>
                <tr>
                    <td>Tên đăng nhập: </td><td><input type="text" id="txtID" name="txtID" /></td>
                </tr>
                <tr>
                    <td>Mật khẩu: </td><td><input type="password" id="txtPass" name="txtPass" /></td>
                </tr>
                <tr>
                    <td></td><td><input type="submit" value="Đăng nhập" /></td>
                </tr>
            </table> 
            </form>
            <br />
        <script>
            function CheckForm() {
                if (txtID.value == "" || txtID.value == null) {
                    alert("Chưa nhập tên đăng nhập.");
                    txtID.focus();
                    return false;
                }
                if (txtPass.value == "" || txtPass.value == null) {
                    alert("Chưa nhập mật khẩu.");
                    txtPass.focus();
                    return false;
                }
            }
        </script>
        <%}
        else
        {%>
            Bạn đã đăng nhập với tên đăng nhập: <b style="color:red"><%=Session["user"] %></b>
            <br />Nếu bạn muốn <b>đăng xuất</b>, thì bấm vào <a href="/DangNhap.aspx?action=out">đây</a>.
                
        <%}%>
    </center>
</asp:Content>
