<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="HopDongForm.aspx.cs" Inherits="ThanhThanhCong_test_webform.HopDongForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%  string action = "Them";
    string _action = "Thêm";
    int maHopDong = -1;
    try
    {
        maHopDong = int.Parse(Request.QueryString["maHopDong"]);
    }
    catch
    {
    }
    ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities entity = new ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities();
    List<ThanhThanhCong_test_webform.Vung> listVung = entity.Vung.ToList();
    int check = 0;
    if (listVung.Count > 0)
        check = 1;
    ThanhThanhCong_test_webform.HopDong hd = null;
    if (maHopDong != null)
    {
        hd = entity.HopDong.Where(item => item.MaHopDong == maHopDong).FirstOrDefault();
        if (hd != null)
        {
            action = "Sua";
            _action = "Sửa";
        }
    }
    
    if (check == 1)
    {%>
    <center>
    <h2>Hợp đồng - <%=_action %></h2>
    <form action="/HopDong.aspx?action=<%=action %>" method="post" name="FHopDong" id="FHopDong" enctype ="multipart/form-data" onsubmit ="return CheckForm()">
    <table>
        <tr>
            <td>Mã hợp đồng: </td>
            <td><input id="txtMaHopDong" name="txtMaHopDong" <%if (action == "Sua") {%> value="<%=hd.MaHopDong %>" <%} else {%> value="0" <%} %> readonly="true"/></td>
        </tr>
        <tr>
            <td></td><td>Bên A</td>
            <td></td><td>Bên B</td>
        </tr>
        <tr>
            <td>Bên cho thuê: </td><td><input id="txtHoten_A1" name="txtHoten_A1" maxlength="200" <%if (action == "Sua") {%> value="<%=hd.HoTen_A1 %>" <%} %>/></td>
            <td>Bên thuê đất: </td><td><input id="txtHoten_B1" name="txtHoten_B1" maxlength="200" <%if (action == "Sua") {%> value="<%=hd.HoTen_B1 %>" <%} %>/></td>
            <td>Kiểm soát viên: </td><td><input id="txtKiemSoatVien" name="txtKiemSoatVien" maxlength="200" <%if (action == "Sua") {%> value="<%=hd.KiemSoatVien %>" <%} %>/></td>
        </tr>
        <tr>
            <td>Số CMND: </td><td><input id="txtCMND_A1" name="txtCMND_A1" maxlength="50" <%if (action == "Sua") {%> value="<%=hd.CMND_A1 %>" <%} %>/></td>
            <td>Số CMND: </td><td><input id="txtCMND_B1" name="txtCMND_B1" maxlength="50" <%if (action == "Sua") {%> value="<%=hd.CMND_B1 %>" <%} %>/></td>
            <td>Số vụ: </td><td><input id="txtSoVu" name="txtSoVu" maxlength="2" <%if (action == "Sua") {%> value="<%=hd.SoVu %>" <%} %>/></td>
            <td> > 0</td>
        </tr>
        <tr>
            <td>Địa chỉ: </td><td><input id="txtDiaChi_A1" name="txtDiaChi_A1" maxlength="500" <%if (action == "Sua") {%> value="<%=hd.DiaChi_A1 %>" <%} %>/></td>
            <td>Địa chỉ: </td><td><input id="txtDiaChi_B1" name="txtDiaChi_B1" maxlength="500" <%if (action == "Sua") {%> value="<%=hd.DiaChi_B1 %>" <%} %>/></td>
            <td>Từ vụ: </td><td><input id="txtTuVu" name="txtTuVu" maxlength="4" <%if (action == "Sua") {%> value="<%=hd.TuVu %>" <%} %>/></td>
            <td> >= 2000</td>
        </tr>
        <tr>
            <td>Điện thoại: </td><td><input id="txtSDT_A1" name="txtSDT_A1" maxlength="50" <%if (action == "Sua") {%> value="<%=hd.SDT_A1 %>" <%} %>/></td>
            <td>Điện thoại: </td><td><input id="txtSDT_B1" name="txtSDT_B1" maxlength="50" <%if (action == "Sua") {%> value="<%=hd.SDT_B1 %>" <%} %>/></td>
            <td></td><td colspan="2">ví dụ: 2014-2015 => chỉ ghi 2014</td>
        </tr>
        <tr>
            <td>Họ tên người thừa kế: </td><td><input id="txtHoten_A2" name="txtHoten_A2" maxlength="200" <%if (action == "Sua") {%> value="<%=hd.HoTen_A2 %>" <%} %>/></td>
            <td>Người thừa kế hợp đồng: </td><td><input id="txtHoten_B2" name="txtHoten_B2" maxlength="200" <%if (action == "Sua") {%> value="<%=hd.HoTen_B2 %>" <%} %>/></td>
            <td>Đơn giá thuê thuê: </td><td><input id="txtDonGiaThue" name="txtDonGiaThue" maxlength="200" <%if (action == "Sua") {%> value="<%=hd.DonGiaThue %>" <%} %>/></td>
            <td> đồng/ha/năm > 0</td>
        </tr>
        <tr>
            <td>Số CMND: </td><td><input id="txtCMND_A2" name="txtCMND_A2" maxlength="50" <%if (action == "Sua") {%> value="<%=hd.CMND_A1 %>" <%} %>/></td>
            <td>Số CMND: </td><td><input id="txtCMND_B2" name="txtCMND_B2" maxlength="50" <%if (action == "Sua") {%> value="<%=hd.CMND_B1 %>" <%} %>/></td>
            <td>Tổng tiền: </td><td><input id="txtTongTien" name="txtTongTien" readonly="true" <%if (action == "Sua") {%> value="<%=hd.TongTien %>" <%} %>/></td>
            <td> đồng</td>
        </tr>
        <tr>
            <td>Mối quan hệ: </td><td><input id="txtMoiQuanHeA" name="txtMoiQuanHeA" maxlength="50" <%if (action == "Sua") {%> value="<%=hd.MoiQuanHeA %>" <%} %>/></td>
            <td>Mối quan hệ: </td><td><input id="txtMoiQuanHeB" name="txtMoiQuanHeB" maxlength="50" <%if (action == "Sua") {%> value="<%=hd.MoiQuanHeB %>" <%} %>/></td>
            <td>Ứng trước: </td><td><input id="txtUngTruoc" name="txtUngTruoc" maxlength="200" <%if (action == "Sua") {%> value="<%=hd.UngTruoc %>" <%} %>/></td>
            <td> đồng <= Tổng tiền</td>
        </tr>
        <tr>
            <td>Địa chỉ: </td><td><input id="txtDiaChi_A2" name="txtDiaChi_A2" maxlength="500" <%if (action == "Sua") {%> value="<%=hd.DiaChi_A2 %>" <%} %>/></td>
            <td>Địa chỉ: </td><td><input id="txtDiaChi_B2" name="txtDiaChi_B2" maxlength="500" <%if (action == "Sua") {%> value="<%=hd.DiaChi_B2 %>" <%} %>/></td>
        </tr>
        <tr>
            <td>Điện thoại: </td><td><input id="txtSDT_A2" name="txtSDT_A2" maxlength="50" <%if (action == "Sua") {%> value="<%=hd.SDT_A2 %>" <%} %>/></td>
            <td>Điện thoại: </td><td><input id="txtSDT_B2" name="txtSDT_B2" maxlength="50" <%if (action == "Sua") {%> value="<%=hd.SDT_B2 %>" <%} %>/></td>
        </tr>
        <tr>
            <td colspan="7">
                <asp:gridview ID="Gridview1" runat="server" ShowFooter="True" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Mã vùng">
                            <ItemTemplate>
                                <asp:DropDownList ID="MaVung" runat="server">
                                <%
                                %>
                                    <asp:ListItem Text="User" Value="User"></asp:ListItem>
                                    <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                    <asp:ListItem Text="Super User" Value="Super User"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Header 1">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Header 2">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Header 3">
                            <ItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <FooterTemplate>
                                <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:gridview>



            </td>
        </tr>
        <tr>
            <td></td>
            <td><input type="submit" style="width:50px" <%if (action == "Sua"){%> value="Sửa"<%}else{%>value="Thêm" <%}%>/></td>
        </tr>
    </form>
        <tr>
            <%  string link = "";
                if (action == "Sua")
                    link = "?maHopDong=" + hd.MaHopDong;
            %>
            <td></td><td><a href="/HopDongForm.aspx<%=link %>"><button style="width:50px">Hủy</button></a></td>
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
    <%}
    else
    {%>
        Chưa có danh sách vùng.<br />
        Bấm vào <a href="/Vung.aspx">đây</a> để chuyển đến trang Quản lý vùng.
    <%}%>
</asp:Content>
