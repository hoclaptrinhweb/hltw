<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucContact.ascx.cs" Inherits="Admin_usercontrols_ucContact" %>
<input type="hidden" id="notselect" value="<%=msg.GetMessage("ERR-000007")%>" />
<input type="hidden" id="deleteconfirm" value="<%=msg.GetMessage("ERR-000008")%>" />
<input type="hidden" id="hdLanguage" value="<%= CurrentPage.Language%>" />
<input type="hidden" id="DateError" value="Ngày không hợp lệ" />
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="Liên hệ"></asp:Label>
        </h1>
    </div>
    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <asp:ValidationSummary ID="valError" runat="server" EnableClientScript="False" />
                    <asp:CustomValidator ID="SaveValidate" runat="server" Display="None" CssClass="FormStyle"
                        ErrorMessage="CustomValidator" />
                </div>
                <div class="box-header">
                    <div style="text-align: left">
                        <asp:Label ID="lbTotal" runat="server"></asp:Label>
                        <asp:Button ID="btnEdit" CssClass="button" runat="server" UseSubmitBehavior="False"
                            Text="Xem" CausesValidation="False" OnClientClick="if(!IsEdit('chckSelect','notselect')) return; showClose=true;isEdit=true;"
                            OnClick="BtnEditClick" />
                        <asp:Button ID="btnDelete" CssClass="button" runat="server" UseSubmitBehavior="False"
                            Text="Xóa" OnClientClick="if(! IsDelete('chckSelect','notselect','deleteconfirm')) return;"
                            CausesValidation="False" OnClick="BtnDeleteClick" />
                        <asp:DropDownList ID="dropContactType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropContactTypeSelectedIndexChanged">
                            <asp:ListItem Value="-1">Tất cả</asp:ListItem>
                            <asp:ListItem Value="1">Liên hệ</asp:ListItem>
                            <asp:ListItem Value="2">Phản hồi</asp:ListItem>
                            <asp:ListItem Value="3">Khách hàng</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="box table">
                    <asp:GridView CellPadding="0" Width="100%" CssClass="GridStyle" AutoGenerateColumns="False"
                        AllowPaging="True" DataKeyNames="ContactID" DataSourceID="ObjData" ID="gvData"
                        runat="server" OnDataBound="GvDataDataBound" OnPageIndexChanging="GvDataPageIndexChanging">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle Width="5%"></HeaderStyle>
                                <ItemStyle CssClass="cbxCheck" HorizontalAlign="Center"></ItemStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chckAll" onclick="ChangeAllCheckBoxStates('chckSelect',this.checked);ShowHideBtnEdit();"
                                        runat="server" ToolTip="Bỏ / Chọn hết" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chckSelect" onclick="ChangeHeaderAsNeeded('chckSelect','chckAll',$(this));ShowHideBtnEdit();"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tên">
                                <HeaderStyle Width="20%"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdContactID" Value='<%# Eval("ContactID") %>' runat="server" />
                                    <asp:HyperLink ID="hpusername" runat="server" Text='<%# Eval("Username") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tiêu đề">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ngày gửi">
                                <HeaderStyle Width="150px" />
                                <ItemStyle HorizontalAlign="left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbCreatedDate" runat="server" Text='<%# Eval("CreatedDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridTitle" />
                        <RowStyle CssClass="RowStyle" />
                        <AlternatingRowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoItem" runat="server" Text="Updating data"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjData" runat="server" TypeName="HocLapTrinhWeb.BLL.vnn_ContactBLL"
                        EnablePaging="True" SelectMethod="GetAllContactForGridView" SelectCountMethod="GetAllContactRowCount"
                        OnObjectCreating="ObjDataObjectCreating" OnSelected="ObjDataSelected" OnSelecting="ObjDataSelecting">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="dropContactType" Name="Type" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="btnEdit" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="divAdd" class="popup" style="display: none">
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdIsAddSuccessful" Value="0" />
                    <asp:HiddenField runat="server" ID="hdEdit" Value="0" />
                    <asp:HiddenField runat="server" ID="hdContactID" Value="0" />
                    <table cellpadding="3">
                        <tr>
                            <td class="midleft">
                            </td>
                            <td class="miditem">
                                <div style="padding-left: 20px">
                                    <asp:ValidationSummary ID="valError1" ValidationGroup="vSave" runat="server" EnableClientScript="False">
                                    </asp:ValidationSummary>
                                    <asp:CustomValidator ID="SaveValidate1" ValidationGroup="vSave" runat="server" Display="None"
                                        ErrorMessage="CustomValidator" /><div class="Spacer">
                                        </div>
                                </div>
                                <table cellpadding="0" cellspacing="0">
                                    <tr valign="top">
                                        <td style="width: 100px">
                                            <asp:Label ID="lblContactName" CssClass="Caption" runat="server" Text="Tên người gửi"></asp:Label>
                                        </td>
                                        <td class="">
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUsername" runat="server" Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr valign="top">
                                        <td style="width: 35px">
                                            <asp:Label ID="Label4" CssClass="Caption" runat="server" Text="Tên công ty"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCompany" Width="500px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr valign="top">
                                        <td style="width: 35px">
                                            <asp:Label ID="Label1" runat="server" CssClass="Caption" Text="Email"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmail" runat="server" Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr valign="top">
                                        <td style="width: 35px">
                                            <asp:Label ID="Label2" runat="server" CssClass="Caption" Text="Điện thoại"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPhone" runat="server" Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr valign="top">
                                        <td style="width: 35px">
                                            <asp:Label ID="Label3" runat="server" CssClass="Caption" Text="Địa chỉ"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAddress" runat="server" Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr valign="top">
                                        <td style="width: 35px">
                                            <asp:Label ID="Label5" runat="server" CssClass="Caption" Text="Tiêu đề"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTitle" runat="server" Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr valign="top">
                                        <td style="width: 35px">
                                            <asp:Label ID="Label6" runat="server" CssClass="Caption" Text="Nội dung"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtContent" runat="server" Height="300px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="right">
                                            &nbsp;<asp:Button ID="btnCancelAdd" UseSubmitBehavior="False" runat="server" Text="Đóng"
                                                OnClientClick="hidePopup();" CausesValidation="False" CssClass="button" OnClick="BtnCancelAddClick" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="midright">
                            </td>
                        </tr>
                        <tr>
                            <td class="footerleft">
                            </td>
                            <td class="footeritem">
                            </td>
                            <td class="footerright">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<script type="text/javascript">
    var isAdd = false, isEdit = false;
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);
    function BeginRequestHandler(sender, args)//begin ajax
    { }
    function EndRequestHandler(sender, args)//ajax return value
    {
        if (isAdd) {
            showPopupDiv('divAdd', 'Thêm Contact', null, null, true, null, null, null); //absolute
            isAdd = false;
        }
        if (isEdit) {
            showPopupDiv('divAdd', 'Xem thông tin Liên hệ', null, null, true, null, null, null);
            isEdit = false;
        }
        var hdIsSuccessful = document.getElementById('<%= hdIsAddSuccessful.ClientID%>');
        if (hdIsSuccessful != null && hdIsSuccessful.value == "1") {
            hdIsSuccessful.value = "0";
            hidePopup();
        }
        EventCheckBox();
    }

    function ShowHideBtnEdit() {
        var checkedRow;
        var gvData = $("table[id$='gvData']")[0];
        var checkboxs = gvData.getElementsByTagName("input");
        var mCount = 0;
        for (i = 0; i < checkboxs.length; i++)
            if (checkboxs[i].type == "checkbox" && checkboxs[i].checked && checkboxs[i].id.indexOf("chckSelect") > 0)
                mCount++;
        if (mCount > 1)
            EnableButton(document.getElementById('<%=btnEdit.ClientID %>'), false);
        else
            EnableButton(document.getElementById('<%=btnEdit.ClientID %>'), true);
    }

    $(document).ready(function () {
        $(window).scroll(function () {
            var nHeight = getScrollXY()[1];
            var winwidth = document.all ? document.body.clientWidth : window.innerWidth;
            var left = (winwidth - 980) / 2;
            var eleWidth = $('.GridStyle').width();
            if (nHeight >= 114)
                $("#sidebar").css({ "position": "fixed", "top": "0px" });
            else
                $("#sidebar").css({ "position": "", "top": "0px" });

            if (nHeight >= 170)
                $(".box-header").addClass("topActive").css({ "position": "fixed", "top": "0px", "width": eleWidth - 30 }); // Padding = 15 => eleWidth - 30       
            else
                $(".box-header").removeClass("topActive").css({ "position": "", "top": "0px", "width": "" });
        });
    });

</script>
