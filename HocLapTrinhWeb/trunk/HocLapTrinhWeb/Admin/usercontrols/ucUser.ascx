<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUser.ascx.cs" Inherits="administrator_usercontrols_ucUser" %>
<%@ Register Assembly="LIBDatePicker" Namespace="LIBDatePicker.DatePicker" TagPrefix="cc1" %>
<input type="hidden" id="notselect" value="<%=msg.GetMessage("ERR-000007")%>" />
<input type="hidden" id="deleteconfirm" value="<%=msg.GetMessage("ERR-000008")%>" />
<input type="hidden" id="hdLanguage" value="<%= CurrentPage.Language%>" />
<input type="hidden" id="DateError" value="Ngày không hợp lệ" />
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="Thành viên"></asp:Label></h1>
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
                        <asp:Button ID="btnNew" runat="server" CssClass="button" EnableViewState="False"
                            Text="Thêm" CausesValidation="False" OnClientClick="showClose=true;isAdd=true;"
                            OnClick="BtnNewClick" />
                        <asp:Button ID="btnEdit" CssClass="button" runat="server" UseSubmitBehavior="False"
                            Text="Sửa" CausesValidation="False" OnClientClick="if(!IsEdit('chckSelect','notselect')) return; showClose=true;isEdit=true;"
                            OnClick="BtnEditClick" />
                        <asp:Button ID="btnDelete" CssClass="button" runat="server" UseSubmitBehavior="False"
                            Text="Xóa" OnClientClick="if(! IsDelete('chckSelect','notselect','deleteconfirm')) return;"
                            CausesValidation="False" OnClick="BtnDeleteClick" />
                    </div>
                </div>
                <div class="box table">
                    <asp:GridView CellPadding="0" Width="100%" CssClass="GridStyle" AutoGenerateColumns="False"
                        AllowPaging="True" DataKeyNames="UserID" DataSourceID="ObjData" ID="gvData" runat="server"
                        OnDataBound="GvDataDataBound" OnPageIndexChanging="GvDataPageIndexChanging">
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
                                    <asp:CheckBox ID="chckSelect" runat="server" onclick="ChangeHeaderAsNeeded('chckSelect','chckAll',$(this));ShowHideBtnEdit();" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tên đăng nhập">
                                <HeaderStyle Width="15%"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdUserID" Value='<%# Eval("UserID") %>' runat="server" />
                                    <asp:HyperLink ID="hpUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tên đầy đủ">
                                <ItemTemplate>
                                    <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <HeaderStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ngày tạo">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%# DateTime.Parse(Eval("CREATEDDATE").ToString(), new System.Globalization.CultureInfo(CurrentPage.Language), System.Globalization.DateTimeStyles.None).ToShortDateString() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Admin">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chckIsAdmin" Checked='<%# Eval("IsAdmin") %>' Enabled="False" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Kịch hoạt">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chckIsActive" Checked='<%# Eval("IsActive") %>' Enabled="False"
                                        runat="server" />
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
                    <asp:ObjectDataSource ID="ObjData" runat="server" TypeName="HocLapTrinhWeb.BLL.ltk_UserBLL"
                        EnablePaging="True" SelectMethod="GetAllUserForGridView" SelectCountMethod="GetAllUserRowCount"
                        OnObjectCreating="ObjDataObjectCreating" OnSelected="ObjDataSelected" OnSelecting="ObjDataSelecting">
                    </asp:ObjectDataSource>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNew" />
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="btnEdit" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="divAdd" class="popup" style="display: none;" >
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdIsAddSuccessful" Value="0" />
                    <asp:HiddenField runat="server" ID="hdEdit" Value="0" />
                    <asp:HiddenField runat="server" ID="hdUserID" Value="0" />
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
                                        <td>
                                            <asp:Label ID="lblUserName" CssClass="Caption" runat="server" Text="Tên đăng nhập"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUserName" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="Phải nhập tên"
                                                Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtUserName" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label1" CssClass="Caption" runat="server" Text="Mật khẩu"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPass" Width="300px" TextMode="Password" runat="server"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdPass" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvPass" runat="server" ErrorMessage="Phải nhập mật khẩu"
                                                Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtPass" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label2" CssClass="Caption" runat="server" Text="Nhập lại mật khẩu"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPassConfirm" Width="300px" TextMode="Password" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rqvConfirmPass" runat="server" ErrorMessage="Phải nhập lại mật khẩu"
                                                Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtPassConfirm" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvPass" runat="server" ControlToCompare="txtPass" ValidationGroup="vAdd"
                                                ControlToValidate="txtPassConfirm" Display="Dynamic" ErrorMessage="Mật khẩu không giống nhau"
                                                SetFocusOnError="True"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label3" runat="server" CssClass="Caption" Text="Email"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmail" Width="300px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Phải nhập Email"
                                                Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtEmail" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                                ValidationGroup="vAdd" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Không phải Email"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label4" CssClass="Caption" runat="server" Text="Tên đầy đủ"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFullName" Width="300px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label5" runat="server" CssClass="Caption" Text="Điện thoại"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPhone" Width="300px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label8" runat="server" CssClass="Caption" Text="Website"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtHomePage" Width="300px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label9" runat="server" CssClass="Caption" Text="IpAddress"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtIpAddress" Width="300px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label6" CssClass="Caption" runat="server" Text="Địa chỉ"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAddress" Width="300px" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px">
                                        </td>
                                        <td style="height: 20px">
                                        </td>
                                        <td style="height: 20px">
                                        </td>
                                        <td style="height: 20px">
                                            <asp:CheckBox CssClass="checkbox" ID="chckIsAdmin" Text="Control" runat="server" />
                                            <asp:CheckBox CssClass="checkbox" ID="chckIsActive" Checked="True" Text="Active"
                                                runat="server" /><asp:CheckBox ID="chckChangePass" Text="Change Password" CssClass="checkbox"
                                                    Visible="False" runat="server" AutoPostBack="True" OnCheckedChanged="ChckChangePassCheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label7" runat="server" CssClass="Caption" Text="Ngày tạo"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <cc1:DatePicker ID="txtCurrentDate" Display="none" runat="server" DefaultFormatLang="True"
                                                SetNullValueDate="False" AutoPostBack="False" ChangeMonth="True" ChangeYear="True"
                                                ChooseMonth="False" DefaultDate="" EditInput="False" FuncOnSelect="" GetDefaultMinDateType="False"
                                                GetTimeDateType="True" InitText="" ListTheme="Flick_Theme" MaxChooseMonth="False"
                                                MaxDate="" MaxYear="+10" MinChooseMonth="True" MinDate="" MinYear="-10" NumberOfMonths="1"
                                                ShowIconDatePicker="False" ShowOtherMonths="False" Transition="None" Transparent="False"
                                                Validatepostback="False" Value="08/19/2011 08:19:39" />
                                            <cc1:DatePicker ID="txtCreatedDate" Width="300px" runat="server" FuncOnSelect="CheckDate(value,date)"
                                                DefaultFormatLang="True" SetNullValueDate="False" AutoPostBack="False" ChangeMonth="True"
                                                ChangeYear="True" ChooseMonth="False" DefaultDate="" Display="block" EditInput="False"
                                                GetDefaultMinDateType="False" GetTimeDateType="True" InitText="" ListTheme="Flick_Theme"
                                                MaxChooseMonth="False" MaxDate="" MaxYear="+10" MinChooseMonth="True" MinDate=""
                                                MinYear="-10" NumberOfMonths="1" ShowIconDatePicker="False" ShowOtherMonths="False"
                                                Transition="None" Transparent="False" Validatepostback="False" Value="08/19/2011 08:19:39" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="right">
                                            <asp:Button ID="btnSave" runat="server" Text="Lưu" ValidationGroup="vAdd" CssClass="button"
                                                OnClick="BtnSaveClick" />
                                            <asp:Button ID="btnSaveAndNew" runat="server" Text="Lưu & Tạo mới" ValidationGroup="vAdd"
                                                CssClass="button" OnClick="BtnSaveAndNewClick" />
                                            <asp:Button ID="btnCancelAdd" UseSubmitBehavior="False" runat="server" Text="Đóng"
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
                    <asp:AsyncPostBackTrigger ControlID="btnNew" />
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
    {

    }
    function EndRequestHandler(sender, args)//ajax return value
    {
        if (isAdd) {
            showPopupDiv('divAdd', 'Thêm user', null, null, true, null, null, null); //absolute
            isAdd = false;
        }
        if (isEdit) {
            showPopupDiv('divAdd', 'Sửa thông tin user', null, null, true, null, null, null);
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

    function CheckDate(value, date) {
        var curLang = document.getElementById("hdLanguage").value;
        if (curLang == "vi-VN") {
            var arrdate = value.split("/");
            value = arrdate[1] + "/" + arrdate[0] + "/" + arrdate[2];
        }
        var txtDate = new Date(value);
        var txtCurrentDate = $.datepicker._getValue('<%=txtCurrentDate.ClientID %>');
        if (txtDate != txtCurrentDate) {
            var msg = $get('DateError');
            alert(msg.value);
            return false;
        }
        //$.datepicker._setValue('<%=txtCurrentDate.ClientID %>', txtDate);
        //$.datepicker._addDate('+1D','<%=txtCurrentDate.ClientID %>');
        return true;
    }
</script>
