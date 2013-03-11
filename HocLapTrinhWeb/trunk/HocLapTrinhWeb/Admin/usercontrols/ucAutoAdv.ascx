<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAutoAdv.ascx.cs" Inherits="Admin_usercontrols_ucAutoAdv" %>
<%@ Register Assembly="LIBDatePicker" Namespace="LIBDatePicker.DatePicker" TagPrefix="cc1" %>
<input type="hidden" id="notselect" value="<%=msg.GetMessage("ERR-000007")%>" />
<input type="hidden" id="deleteconfirm" value="<%=msg.GetMessage("ERR-000008")%>" />
<input type="hidden" id="hdLanguage" value="<%= CurrentPage.Language%>" />
<input type="hidden" id="DateError" value="Ngày không hợp lệ" />
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="Site AutoAdv"></asp:Label></h1>
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
                        AllowPaging="True" DataKeyNames="AutoAdvID" DataSourceID="ObjData" ID="gvData"
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
                            <asp:TemplateField HeaderText="Ngày" SortExpression="CreatedDate">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdAutoAdvID" Value='<%# Eval("AutoAdvID") %>' runat="server" />
                                    <asp:Label ID="lbCreatedDate" runat="server" Text='<%# Eval("CreatedDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tổng Click">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lbTotalClick" runat="server" Text='<%# Eval("TotalClick") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Còn lại">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lbCurrentClickTop" runat="server" Text='<%# Eval("CurrentClick") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ClickTop">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lbTotalClickTop" runat="server" Text='<%# Eval("TotalClickTop") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="C.Top">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lbCurrentClick" runat="server" Text='<%# Eval("CurrentClickTop") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Limit">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lbTimeLimit" runat="server" Text='<%# Eval("TimeLimit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="TypeID">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lbTypeID" runat="server" Text='<%# Eval("TypeID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cập nhật">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lbUpdatedDate" runat="server" Text='<%# Eval("UpdatedDate") %>'></asp:Label>
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
                    <asp:ObjectDataSource ID="ObjData" runat="server" TypeName="HocLapTrinhWeb.BLL.vnn_AutoAdvBLL"
                        EnablePaging="True" SelectMethod="GetAllAutoAdvForGridView" SelectCountMethod="GetAllAutoAdvRowCount"
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
        <div id="divAdd" style="display: none;" >
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdIsAddSuccessful" Value="0" />
                    <asp:HiddenField runat="server" ID="hdEdit" Value="0" />
                    <asp:HiddenField runat="server" ID="hdAutoAdvID" Value="0" />
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
                                            <asp:Label ID="lblAutoAdvName" CssClass="Caption" runat="server" Text="Click còn lại"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCurrentClick" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label2" CssClass="Caption" runat="server" Text="Tổng click"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTotalClick" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label3" CssClass="Caption" runat="server" Text="Kích hoạt"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="cbIsActive" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label4" CssClass="Caption" runat="server" Text="C.Top"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCurrentClickTop" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label5" CssClass="Caption" runat="server" Text="ClickTop"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTotalClickTop" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label7" CssClass="Caption" runat="server" Text="Giới hạn"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTimeLimit" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label6" CssClass="Caption" runat="server" Text="Kích hoạt Top"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="cbIsActiveTop" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label8" CssClass="Caption" runat="server" Text="Loại"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTypeID" runat="server" Width="300px"></asp:TextBox>
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
    var isAdd = false, isEdit = false; isUpdate = false;
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);
    function BeginRequestHandler(sender, args)//begin ajax
    {
    }

    function EndRequestHandler(sender, args)//ajax return value
    {
        if (isAdd) {
            showPopupDiv('divAdd', 'Thêm AutoAdv', null, null, true, null, null, null); //absolute
            isAdd = false;
        }
        if (isEdit) {
            showPopupDiv('divAdd', 'Sửa thông tin AutoAdv', null, null, true, null, null, null);
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
        var gvData = $("table[id$='gvData']")[0];
        var checkboxs = gvData.getElementsByTagName("input");
        var mCount = 0;
        for (var i = 0; i < checkboxs.length; i++)
            if (checkboxs[i].type == "checkbox" && checkboxs[i].checked && checkboxs[i].id.indexOf("chckSelect") > 0)
                mCount++;
    if (mCount > 1)
        EnableButton(document.getElementById('<%=btnEdit.ClientID %>'), false);
    else
        EnableButton(document.getElementById('<%=btnEdit.ClientID %>'), true);
}


</script>
