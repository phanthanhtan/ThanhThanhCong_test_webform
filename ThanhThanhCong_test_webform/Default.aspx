<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ThanhThanhCong_test_webform.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<center>
    <%  if(Session["user"] != null)
        { %>
    <a href="/HopDongForm.aspx">Tạo mới hợp đồng</a><br />
     <%  if(Session["per"].ToString() == "1")
        { %>
    <a href="/HopDong.aspx">Danh sách hợp đồng</a><br />
    <a href="/HopDongin.aspx"> Xem số lần in hợp đồng</a><br />
    <a href="/Vung.aspx">Quản lý vùng</a><br />
    <%  }
        } %>
</center>
</asp:Content>
