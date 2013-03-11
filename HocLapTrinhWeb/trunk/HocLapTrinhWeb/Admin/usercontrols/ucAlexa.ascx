<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAlexa.ascx.cs" Inherits="Admin_usercontrols_ucAlexa" %>
<%@ Register Assembly="LIBDatePicker" Namespace="LIBDatePicker.DatePicker" TagPrefix="cc1" %>
<input type="hidden" id="notselect" value="<%=msg.GetMessage("ERR-000007")%>" />
<input type="hidden" id="deleteconfirm" value="<%=msg.GetMessage("ERR-000008")%>" />
<input type="hidden" id="hdLanguage" value="<%= CurrentPage.Language%>" />
<input type="hidden" id="DateError" value="Ngày không hợp lệ" />
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="Site Alexa"></asp:Label></h1>
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
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div style="text-align: left">
                                    <asp:Button ID="btnNew" runat="server" CssClass="button" EnableViewState="False"
                                        Text="Thêm" CausesValidation="False" OnClientClick="showClose=true;isAdd=true;"
                                        OnClick="BtnNewClick" />
                                    <asp:Button ID="btnEdit" CssClass="button" runat="server" UseSubmitBehavior="False"
                                        Text="Sửa" CausesValidation="False" OnClientClick="if(!IsEdit('chckSelect','notselect')) return; showClose=true;isEdit=true;"
                                        OnClick="BtnEditClick" />
                                    <asp:Button ID="btnDelete" CssClass="button" runat="server" UseSubmitBehavior="False"
                                        Text="Xóa" OnClientClick="if(! IsDelete('chckSelect','notselect','deleteconfirm')) return;"
                                        CausesValidation="False" OnClick="BtnDeleteClick" />
                                    <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Cập nhật hạng Alexa"
                                        UseSubmitBehavior="False" CausesValidation="False" OnClick="BtnUpdateClick"
                                        OnClientClick="if(!IsEdit('chckSelect','notselect')) return; showClose=true;isUpdate=true;" />
                                </div>
                            </td>
                            <td align="right">
                                <asp:Label ID="lbTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="box table">
                    <asp:GridView CellPadding="0" Width="100%" CssClass="GridStyle" AutoGenerateColumns="False"
                        AllowPaging="True" DataKeyNames="AlexaID" DataSourceID="ObjData" ID="gvData"
                        runat="server" OnDataBound="GvDataDataBound" OnPageIndexChanging="GvDataPageIndexChanging">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle Width="5%"></HeaderStyle>
                                <ItemStyle CssClass="cbxCheck" HorizontalAlign="Center"></ItemStyle>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chckAll" onclick="ChangeAllCheckBoxStates('chckSelect',this.checked);ShowHideBtnEdit();"
                                        runat="server" ToolTip="Select all/Unselect all" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chckSelect" onclick="ChangeHeaderAsNeeded('chckSelect','chckAll',$(this));ShowHideBtnEdit();"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Link Url" SortExpression="AlexaName">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdAlexaID" Value='<%# Eval("AlexaID") %>' runat="server" />
                                    <asp:Label ID="lbLinkUrl" runat="server" Text='<%# Eval("LinkUrl") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chi tiết">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:HyperLink ID="hpchitiet" runat="server" NavigateUrl='<%# "~/admin/AlexaDetail.aspx?AlexaID=" + Eval("AlexaID") %>'
                                        Text=" Xem "></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ngày tạo">
                                <HeaderStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%# Eval("CreatedDate") %>'></asp:Label>
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
                    <asp:ObjectDataSource ID="ObjData" runat="server" TypeName="HocLapTrinhWeb.BLL.vnn_AlexaBLL"
                        EnablePaging="True" SelectMethod="GetAllAlexaForGridView" SelectCountMethod="GetAllAlexaRowCount"
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
        <div id="divAdd" class="popup" style="display: none;">
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdIsAddSuccessful" Value="0" />
                    <asp:HiddenField runat="server" ID="hdEdit" Value="0" />
                    <asp:HiddenField runat="server" ID="hdAlexaID" Value="0" />
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
                                            <asp:Label ID="lblAlexaName" CssClass="Caption" runat="server" Text="Link Url"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLinkUrl" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvAlexaName" runat="server" ErrorMessage="Phải nhập tên"
                                                Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtLinkUrl" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label1" runat="server" CssClass="Caption" Text="Ngày tạo"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <cc1:DatePicker ID="txtCreatedDate" runat="server" AutoPostBack="False" ChangeMonth="True"
                                                ChangeYear="True" ChooseMonth="False" DefaultDate="" DefaultFormatLang="True"
                                                Display="block" EditInput="False" FuncOnSelect="" GetDefaultMinDateType="False"
                                                GetTimeDateType="True" InitText="" ListTheme="Flick_Theme" MaxChooseMonth="False"
                                                MaxDate="" MaxYear="+10" MinChooseMonth="True" MinDate="" MinYear="-10" NumberOfMonths="1"
                                                SetNullValueDate="False" ShowIconDatePicker="False" ShowOtherMonths="False" Transition="None"
                                                Transparent="False" Validatepostback="False" Value="08/19/2011 08:19:39" Width="300px" />
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="right">
                                            <asp:Button ID="btnSave" runat="server" Text="Lưu" ValidationGroup="vAdd" CssClass="button"
                                                OnClick="BtnSaveClick" />
                                            <asp:Button ID="btnSaveAndNew" runat="server" Text="Lưu & Tạo mới" ValidationGroup="vAdd"
                                                CssClass="button" OnClick="BtnSaveAndNewClick" meta:resourcekey="btnSaveAndNewResource1" />
                                            <asp:Button ID="btnCancelAdd" UseSubmitBehavior="False" runat="server" Text="Đóng"
                                                OnClientClick="hidePopup();" CausesValidation="False" CssClass="button" OnClick="BtnCancelAddClick"
                                                meta:resourcekey="btnCancelAddResource1" />
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
        <div id="divUpdate" class="popup" style="display: none; z-index: 900">
            <asp:UpdatePanel ID="updatepanel3" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdPopup" Value="0" />
                    <table cellpadding="3">
                        <tr>
                            <td class="midleft">
                            </td>
                            <td class="miditem">
                                <div style="padding-left: 20px">
                                    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="vSave" runat="server"
                                        EnableClientScript="False"></asp:ValidationSummary>
                                    <asp:CustomValidator ID="CustomValidator1" ValidationGroup="vSave" runat="server"
                                        Display="None" ErrorMessage="" /><div class="Spacer">
                                        </div>
                                </div>
                                <table cellpadding="0" cellspacing="0">
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label7" runat="server" Style="font-size: 30px; font-weight: bold"
                                                Text="Đang cập nhật hạng Alexa"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <input type="text" style="border: 0px; background-image: none; font-size: 50px; font-weight: bold;
                                                text-align: center" name="timer" />
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
                    <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<script type="text/javascript">
    var isAdd = false, isEdit = false; isUpdate = false;
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);
    function BeginRequestHandler(sender, args)//begin ajax
    {
        if (isUpdate) {
            showPopupDiv('divUpdate', 'Cập nhật hạng Alexa', 200, null, true, null, null, null); //absolute
            isUpdate = false;
        }
    }

    function EndRequestHandler(sender, args)//ajax return value
    {
        if (isAdd) {
            showPopupDiv('divAdd', 'Thêm Alexa', null, null, true, null, null, null); //absolute
            isAdd = false;
        }
        if (isEdit) {
            showPopupDiv('divAdd', 'Sửa thông tin Alexa', null, null, true, null, null, null);
            isEdit = false;
        }
        var hdIsSuccessful = document.getElementById('<%= hdIsAddSuccessful.ClientID%>');
        if (hdIsSuccessful != null && hdIsSuccessful.value == "1") {
            hdIsSuccessful.value = "0";
            hidePopup();
        }
        var hdPopup = document.getElementById('<%= hdPopup.ClientID%>');
        if (hdPopup != null && hdPopup.value == "1") {
            hdPopup.value = "0";
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


</script>
