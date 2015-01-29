<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="HopDongForm.aspx.cs" Inherits="ThanhThanhCong_test_webform.HopDongForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%  if (Session["user"] != null)
        {
    %>
    <%
    string action = "Them";
    string _action = "Thêm";
    int maHopDong = -1;
    try
    {
        if (Session["per"].ToString() == "1")
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
    List<ThanhThanhCong_test_webform.HopDong_ChiTiet> listhd_ct = new List<ThanhThanhCong_test_webform.HopDong_ChiTiet>();
    if (maHopDong != null)
    {
        hd = entity.HopDong.Where(item => item.MaHopDong == maHopDong).FirstOrDefault();
        if (hd != null)
        {
            action = "Sua";
            _action = "Sửa";
            listhd_ct = entity.HopDong_ChiTiet.Where(item => item.MaHopDong == maHopDong).ToList();
        }
    }
    
    if (check == 1)
    {%>
        <center>
    <h2>Hợp đồng - <%=_action %></h2>
    <table style="text-align:right">
    <form action="/HopDong.aspx?action=<%=action %>" method="post" name="FHopDong" id="FHopDong" enctype ="multipart/form-data" onsubmit ="return CheckForm()">
        <tr>
            <td>Mã hợp đồng: </td>
            <td><input id="txtMaHopDong" name="txtMaHopDong" <%if (action == "Sua") {%> value="<%=hd.MaHopDong %>" <%} else {%> value="0" <%} %> readonly="true"/></td>
            <td colspan="5" style="color:red; text-align:left"> (mã hợp đồng không nhập)</td>
        </tr>
        <tr style="text-align:center; font-weight:bold">
            <td></td><td>Bên A</td>
            <td></td><td>Bên B</td>
        </tr>
        <tr>
            <td>Bên cho thuê: </td><td><input id="txtHoten_A1" name="txtHoten_A1" maxlength="200" <%if (action == "Sua") {%> value="<%=hd.HoTen_A1 %>" <%} %>/></td>
            <td>Bên thuê đất: </td><td><input id="txtHoten_B1" name="txtHoten_B1" maxlength="200" <%if (action == "Sua") {%> value="<%=hd.HoTen_B1 %>" <%} %>/></td>
            <td>Kiểm soát viên: </td><td><input id="txtKiemSoatVien" name="txtKiemSoatVien" maxlength="200" <%if (action == "Sua") {%> value="<%=hd.KiemSoatVien %>" <%} %>/></td>
        </tr>
        <tr>
            <td>Số CMND: </td><td><input id="txtCMND_A1" name="txtCMND_A1" onkeypress="return CheckNumber(event)" maxlength="50" <%if (action == "Sua") {%> value="<%=hd.CMND_A1 %>" <%} %>/></td>
            <td>Số CMND: </td><td><input id="txtCMND_B1" name="txtCMND_B1" onkeypress="return CheckNumber(event)" maxlength="50" <%if (action == "Sua") {%> value="<%=hd.CMND_B1 %>" <%} %>/></td>
            <td>Số vụ: </td><td><input id="txtSoVu" name="txtSoVu" maxlength="2" onkeypress="return CheckNumber(event)" <%if (action == "Sua") {%> value="<%=hd.SoVu %>" <%} %>/></td>
            <td style="text-align:left"> 1-99</td>
        </tr>
        <tr>
            <td>Địa chỉ: </td><td><input id="txtDiaChi_A1" name="txtDiaChi_A1" maxlength="500" <%if (action == "Sua") {%> value="<%=hd.DiaChi_A1 %>" <%} %>/></td>
            <td>Địa chỉ: </td><td><input id="txtDiaChi_B1" name="txtDiaChi_B1" maxlength="500" <%if (action == "Sua") {%> value="<%=hd.DiaChi_B1 %>" <%} %>/></td>
            <td>Từ vụ: </td><td><input id="txtTuVu" name="txtTuVu" maxlength="4" onkeypress="return CheckNumber(event)" <%if (action == "Sua") {%> value="<%=hd.TuVu %>" <%} %>/></td>
            <td style="text-align:left"> >= 2000</td>
        </tr>
        <tr>
            <td>Điện thoại: </td><td><input id="txtSDT_A1" name="txtSDT_A1" maxlength="50" onkeypress="return CheckNumber(event)" <%if (action == "Sua") {%> value="<%=hd.SDT_A1 %>" <%} %>/></td>
            <td>Điện thoại: </td><td><input id="txtSDT_B1" name="txtSDT_B1" maxlength="50" onkeypress="return CheckNumber(event)" <%if (action == "Sua") {%> value="<%=hd.SDT_B1 %>" <%} %>/></td>
            <td></td><td colspan="2"  style="text-align:left">ví dụ: 2014-2015 => chỉ ghi 2014</td>
        </tr>
        <tr>
            <td>Họ tên người thừa kế: </td><td><input id="txtHoten_A2" name="txtHoten_A2" maxlength="200" <%if (action == "Sua") {%> value="<%=hd.HoTen_A2 %>" <%} %>/></td>
            <td>Người thừa kế hợp đồng: </td><td><input id="txtHoten_B2" name="txtHoten_B2" maxlength="200" <%if (action == "Sua") {%> value="<%=hd.HoTen_B2 %>" <%} %>/></td>
            <td>Đơn giá thuê thuê: </td><td><input id="txtDonGiaThue" name="txtDonGiaThue" maxlength="200" style="text-align:right" onkeypress="return CheckNumber(event)" <%if (action == "Sua") {%> value="<%=hd.DonGiaThue %>" <%} %>/></td>
            <td style="text-align:left"> đồng/ha/năm > 0</td>
        </tr>
        <tr>
            <td>Số CMND: </td><td><input id="txtCMND_A2" name="txtCMND_A2" maxlength="50" onkeypress="return CheckNumber(event)" <%if (action == "Sua") {%> value="<%=hd.CMND_A1 %>" <%} %>/></td>
            <td>Số CMND: </td><td><input id="txtCMND_B2" name="txtCMND_B2" maxlength="50" onkeypress="return CheckNumber(event)" <%if (action == "Sua") {%> value="<%=hd.CMND_B1 %>" <%} %>/></td>
            <td>Ứng trước: </td><td><input id="txtUngTruoc" name="txtUngTruoc" maxlength="200" style="text-align:right" onkeypress="return CheckNumber(event)" <%if (action == "Sua") {%> value="<%=hd.UngTruoc %>" <%} %>/></td>
            <td style="text-align:left"> đồng</td>
            <%--<td>Tổng tiền: </td><td><input id="txtTongTien" name="txtTongTien" readonly="true" <%if (action == "Sua") {%> value="<%=hd.TongTien %>" <%} %>/></td>
            <td> đồng</td>--%>
        </tr>
        <tr>
            <td>Mối quan hệ: </td><td><input id="txtMoiQuanHeA" name="txtMoiQuanHeA" maxlength="50" <%if (action == "Sua") {%> value="<%=hd.MoiQuanHeA %>" <%} %>/></td>
            <td>Mối quan hệ: </td><td><input id="txtMoiQuanHeB" name="txtMoiQuanHeB" maxlength="50" <%if (action == "Sua") {%> value="<%=hd.MoiQuanHeB %>" <%} %>/></td>
            <%--<td>Ứng trước: </td><td><input id="txtUngTruoc" name="txtUngTruoc" maxlength="200" onkeypress="return CheckNumber(event)" <%if (action == "Sua") {%> value="<%=hd.UngTruoc %>" <%} %>/></td>
            <td> đồng <= Tổng tiền</td>--%>
        </tr>
        <tr>
            <td>Địa chỉ: </td><td><input id="txtDiaChi_A2" name="txtDiaChi_A2" maxlength="500" <%if (action == "Sua") {%> value="<%=hd.DiaChi_A2 %>" <%} %>/></td>
            <td>Địa chỉ: </td><td><input id="txtDiaChi_B2" name="txtDiaChi_B2" maxlength="500" <%if (action == "Sua") {%> value="<%=hd.DiaChi_B2 %>" <%} %>/></td>
            <%  if (action == "Sua")
                {
                    ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities entity_in = new ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities();
                    List<ThanhThanhCong_test_webform.HopDong_in> list = entity_in.HopDong_in.Where(item => item.MaHopDong == maHopDong).ToList();
                    int count = list.Count;
                %>
            <td>Số lần in:</td>
            <td>
                <%=count %>
            </td>
            <td>
                <%  if (count > 0)
                    {%>
                        <a href="HopDongin.aspx?maHopDong=<%=maHopDong %>">Chi tiết</a>
                    <%} %>                
            </td>
            <%} %>
        </tr>
        <tr>
            <td>Điện thoại: </td><td><input id="txtSDT_A2" name="txtSDT_A2" maxlength="50" onkeypress="return CheckNumber(event)" <%if (action == "Sua") {%> value="<%=hd.SDT_A2 %>" <%} %>/></td>
            <td>Điện thoại: </td><td><input id="txtSDT_B2" name="txtSDT_B2" maxlength="50" onkeypress="return CheckNumber(event)" <%if (action == "Sua") {%> value="<%=hd.SDT_B2 %>" <%} %>/></td>
        </tr>
        <tr>
            <td colspan="7">
                <table cellspacing="0" cellpadding="0" border="1" style="text-align:center; margin-top:20px;">
                    <tr style="text-align:left">
                        <td colspan="7" style="color:red">Mặc định có 10 dòng, chọn dòng nào tick vào dòng đó.<br />
                            Diện tích/Số thửa: chỉ được nhập 0-9 và dấu ","
                        </td>
                    </tr>
                    <tr style="font-weight:bold">
                        <td></td>
                        <td>Mã vùng</td><td>Số thửa</td><td>Diện tích (ha)</td>
                        <td>Ví trí đất</td><td>Loại đất (cao/thấp/...)</td><td>Tình trạng đất</td>
                    </tr>
                <%  int i = 0;
                    foreach(ThanhThanhCong_test_webform.HopDong_ChiTiet hd_ct in listhd_ct)
                    {%>
                    <tr>
                        <td><input id="cb<%=i %>" name="cb<%=i %>" type="checkbox" checked="true" /></td>
                        <td>
                            <select id="txtMaVung<%=i %>" name="txtMaVung<%=i %>">
                        <% foreach (ThanhThanhCong_test_webform.Vung vung in listVung)
                            {%>
                                <option value="<%=vung.MaVung %>" <%if(hd_ct.MaVung == vung.MaVung) {%>checked="true"<%} %>><%=vung.TenVung %></option>
                            <%}
                        %>
                            </select>
                        </td>
                        <td><input id="txtSoThua<%=i %>" name="txtSoThua<%=i %>" maxlength="10" value="<%=hd_ct.SoThua %>" onkeypress="return float(event)"/></td>
                        <td><input id="txtDienTich<%=i %>" name="txtDienTich<%=i %>" maxlength="50" value="<%=hd_ct.DienTich %>" onkeypress="return float(event)"/></td>
                        <td><input id="txtViTriDat<%=i %>" name="txtViTriDat<%=i %>" maxlength="200" value="<%=hd_ct.ViTriDat %>" /></td>
                        <td><input id="txtLoaiDat<%=i %>" name="txtLoaiDat<%=i %>" maxlength="50" value="<%=hd_ct.LoaiDat %>" /></td>
                        <td><input id="txtTinhTrangDat<%=i %>" name="txtTinhTrangDat<%=i %>" maxlength="200" value="<%=hd_ct.TinhTrangDat %>" /></td>
                    </tr> 
                    <%i++;
                    }
                    for (int j = i; j < 10; j++)
                    {%>
                    <tr>
                        <td><input id="cb<%=j %>" name="cb<%=j %>" type="checkbox" /></td>
                        <td>
                            <select id="txtMaVung<%=j %>" name="txtMaVung<%=j %>">
                        <% foreach (ThanhThanhCong_test_webform.Vung vung in listVung)
                            {%>
                                <option value="<%=vung.MaVung %>"><%=vung.TenVung %></option>
                            <%}
                        %>
                            </select>
                        </td>
                        <td><input id="txtSoThua<%=j %>" name="txtSoThua<%=j %>" maxlength="10" onkeypress="return float(event)" /></td>
                        <td><input id="txtDienTich<%=j %>" name="txtDienTich<%=j %>" maxlength="100" onkeypress="return float(event)" /></td>
                        <td><input id="txtViTriDat<%=j %>" name="txtViTriDat<%=j %>" maxlength="200" /></td>
                        <td><input id="txtLoaiDat<%=j %>" name="txtLoaiDat<%=j %>" maxlength="50" /></td>
                        <td><input id="txtTinhTrangDat<%=j %>" name="txtTinhTrangDat<%=j %>" maxlength="200" /></td>
                    </tr> 
                    <%}
                %>
                </table>



                <%--<asp:gridview ID="Gridview1" runat="server" ShowFooter="true" AutoGenerateColumns="false">
                        <Columns>
                        <asp:BoundField DataField="STT" HeaderText="STT" />
                        <asp:TemplateField HeaderText="Mã vùng">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMaVung" runat="server"></asp:TextBox>
                                <%--<select id="txtMaVung">
                                    <%  ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities entity1 = new ThanhThanhCong_test_webform.TTC_HopDongThueDatEntities();
                                        List<ThanhThanhCong_test_webform.Vung> listVung = entity1.Vung.ToList();
                                        foreach (ThanhThanhCong_test_webform.Vung v in listVung)
                                        {%>
                                    <option value="<%=v.MaVung %>"><%=v.TenVung %></option>
                                        <%}
                                        %>
                                </select>--%>
                                <%--<asp:DropDownList ID="ddMaVung" runat="server" DataSourceID="SqlDataSource1" DataTextField="TenVung" DataValueField="MaVung">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TTC_HopDongThueDatConnectionString5 %>" SelectCommand="SELECT * FROM [Vung]"></asp:SqlDataSource>--%>
                            <%--</ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Số thửa">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSoThua" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Diện tích">
                            <ItemTemplate>
                                 <asp:TextBox ID="txtDienTich" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <FooterTemplate>
                             <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click"/>
                            </FooterTemplate>
                        </asp:TemplateField>
                        </Columns>
                </asp:gridview>--%>
                <%--<asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="MaHopDong_ChiTiet" GridLines="Vertical" OnRowDataBound="grd_RowDataBound">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Mã vùng">
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="TenVung" DataValueField="MaVung" SelectedValue='<%# Bind("MaVung") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TTC_HopDongThueDatConnectionString4 %>" SelectCommand="SELECT * FROM [Vung]"></asp:SqlDataSource>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SoThua" HeaderText="Số thửa" SortExpression="SoThua" />
                        <asp:BoundField DataField="DienTich" HeaderText="Diện tích (ha)" SortExpression="DienTich" />
                        <asp:BoundField DataField="ViTriDat" HeaderText="Vị trí đất" SortExpression="ViTriDat" />
                        <asp:BoundField DataField="LoaiDat" HeaderText="Loại đất (cao/thấp/...)" SortExpression="LoaiDat" />
                        <asp:BoundField DataField="TinhTrangDat" HeaderText="Tình trạng đất" SortExpression="TinhTrangDat" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
                <asp:Button ID="btnAddRow" runat="server" OnClick="btnAddRow_Click" Text="Add Row" />--%>
            </td>
        </tr>
        <tr>
            <td style="text-align:left; padding-top:10px"><input type="submit" id="Luu" name="Luu" style="width:100px" <%if (action == "Sua"){%> value="Sửa"<%}else{%>value="Thêm" <%}%>/></td>
            <td style="text-align:left; padding-top:10px"><input type="submit" id="in" name="Luu" style="width:100px" value="Lưu và in"/></td>
        </tr>
    </form>
        <tr>
            <%  string link = "";
                if (action == "Sua")
                    link = "?maHopDong=" + hd.MaHopDong;
            %>
            <td style="text-align:left"><a href="/HopDongForm.aspx<%=link %>"><button style="width:100px">Hủy</button></a></td>
        </tr>
    </table>
    </center>
    <script>
        function CheckNumber(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function float(evt) {   //number and ,
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode > 31 && charCode < 44) || (charCode > 44 && charCode < 48) || charCode > 57)//, => 44
                return false;
            return true;
        }
        //function _tongtien(evt) {
        //    var sum = 0;
        //    var cb = "cb";
        //    var txtDienTich = "txtDienTich";
        //    for (var i = 0; i < 10; i++) {
        //        cb = cb + i;
        //        if (FHopDong.cb == checked) {
        //            txtDienTich = txtDienTich + i;
        //            sum = sum + parseFloat(FHopDong.txtDonGiaThue.value) * parseFloat(FHopDong.txtDienTich.value);
        //        }
        //    }
        //    FHopDong.txtHoten_A1.value = "123";
        //}
        function CheckForm() {
            if (txtHoten_A1.value == "" || txtHoten_A1.value == null) {
                alert("Chưa nhập Bên cho thuê.");
                txtHoten_A1.focus();
                return false;
            }
            if (txtHoten_A2.value == "" || txtHoten_A2.value == null) {
                alert("Chưa nhập Họ tên người thừa kế.");
                txtHoten_A2.focus();
                return false;
            }
            if (txtHoten_B1.value == "" || txtHoten_B1.value == null) {
                alert("Chưa nhập Bên thuê đất.");
                txtHoten_B1.focus();
                return false;
            }
            if (txtHoten_B2.value == "" || txtHoten_B2.value == null) {
                alert("Chưa nhập Người thừa kế hợp đồng.");
                txtHoten_B2.focus();
                return false;
            }
            if (txtKiemSoatVien.value == "" || txtKiemSoatVien.value == null) {
                alert("Chưa nhập Kiểm soát viên.");
                txtKiemSoatVien.focus();
                return false;
            }
            if (txtCMND_A1.value == "" || txtCMND_A1.value == null) {
                alert("Chưa nhập Số CMND.");
                txtCMND_A1.focus();
                return false;
            }
            if (txtCMND_A2.value == "" || txtCMND_A2.value == null) {
                alert("Chưa nhập Số CMND.");
                txtCMND_A2.focus();
                return false;
            }
            if (txtCMND_B1.value == "" || txtCMND_B1.value == null) {
                alert("Chưa nhập Số CMND.");
                txtCMND_B1.focus();
                return false;
            }
            if (txtCMND_B2.value == "" || txtCMND_B2.value == null) {
                alert("Chưa nhập Số CMND.");
                txtCMND_B2.focus();
                return false;
            }
            if (txtDiaChi_A1.value == "" || txtDiaChi_A1.value == null) {
                alert("Chưa nhập Địa chỉ.");
                txtDiaChi_A1.focus();
                return false;
            }
            if (txtDiaChi_A2.value == "" || txtDiaChi_A2.value == null) {
                alert("Chưa nhập Địa chỉ.");
                txtDiaChi_A2.focus();
                return false;
            }
            if (txtDiaChi_B1.value == "" || txtDiaChi_B1.value == null) {
                alert("Chưa nhập Địa chỉ.");
                txtDiaChi_B1.focus();
                return false;
            }
            if (txtDiaChi_B2.value == "" || txtDiaChi_B2.value == null) {
                alert("Chưa nhập Địa chỉ.");
                txtDiaChi_B2.focus();
                return false;
            }
            if (txtSDT_A1.value == "" || txtSDT_A1.value == null) {
                alert("Chưa nhập Điện thoại.");
                txtSDT_A1.focus();
                return false;
            }
            if (txtSDT_A2.value == "" || txtSDT_A2.value == null) {
                alert("Chưa nhập Điện thoại.");
                txtSDT_A2.focus();
                return false;
            }
            if (txtSDT_B1.value == "" || txtSDT_B1.value == null) {
                alert("Chưa nhập Điện thoại.");
                txtSDT_B1.focus();
                return false;
            }
            if (txtSDT_B2.value == "" || txtSDT_B2.value == null) {
                alert("Chưa nhập Điện thoại.");
                txtSDT_B2.focus();
                return false;
            }
            if (txtMoiQuanHeA.value == "" || txtMoiQuanHeA.value == null) {
                alert("Chưa nhập Mối quan hệ bên A.");
                txtMoiQuanHeA.focus();
                return false;
            }
            if (txtMoiQuanHeB.value == "" || txtMoiQuanHeB.value == null) {
                alert("Chưa nhập Mối quan hệ bên B.");
                txtMoiQuanHeB.focus();
                return false;
            }
            if (txtSoVu.value == "" || txtSoVu.value == null || parseInt(txtSoVu.value) <= 0) {
                alert("Chưa nhập Số vụ/Số vụ <= 0.");
                txtSoVu.focus();
                return false;
            }
            if (txtTuVu.value == "" || txtTuVu.value == null || parseInt(txtTuVu.value) <2000) {
                alert("Chưa nhập Từ vụ/Từ vụ < 2000.");
                txtTuVu.focus();
                return false;
            }
            if (txtDonGiaThue.value == "" || txtDonGiaThue.value == null || parseInt(txtDonGiaThue.value) <= 0) {
                alert("Chưa nhập Đơn giá thuê/Đơn giá thuê phải > 0.");
                txtDonGiaThue.focus();
                return false;
            }
            if (txtUngTruoc.value == "" || txtUngTruoc.value == null) {
                alert("Chưa nhập Ứng trước.");
                txtUngTruoc.focus();
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

    <%  }
        else
        {%>
            <center>
            Bạn chưa đăng nhập.<br />
            Bấm vào <a href="/DangNhap.aspx">đây</a> để đăng nhập.
            </center>
        <%}
         %>
</asp:Content>
