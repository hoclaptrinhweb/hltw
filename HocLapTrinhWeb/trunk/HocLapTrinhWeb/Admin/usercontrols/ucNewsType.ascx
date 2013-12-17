<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucNewsType.ascx.cs" Inherits="Admin_usercontrols_NewsType" %>
<%@ Register Assembly="INNO.WebControls" Namespace="INNO.WebControls" TagPrefix="inno" %>
<input type="hidden" id="notselect" value="<%=msg.GetMessage("ERR-000007")%>" />
<input type="hidden" id="deleteconfirm" value="<%=msg.GetMessage("ERR-000008")%>" />
<input type="hidden" id="hdLanguage" value="<%= CurrentPage.Language%>" />
<input type="hidden" id="DateError" value="Ngày không hợp lệ" />
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="Loại tin tức"></asp:Label></h1>
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
                                        OnClick="BtnNewClick" meta:resourcekey="btnNewResource1" />
                                    <asp:Button ID="btnEdit" CssClass="button" runat="server" UseSubmitBehavior="False"
                                        Text="Sửa" CausesValidation="False" OnClientClick="if(!IsEdit('chckSelect','notselect')) return; showClose=true;isEdit=true;"
                                        OnClick="BtnEditClick" meta:resourcekey="btnEditResource1" />
                                    <asp:Button ID="btnDelete" CssClass="button" runat="server" UseSubmitBehavior="False"
                                        Text="Xóa" OnClientClick="if(! IsDelete('chckSelect','notselect','deleteconfirm')) return;"
                                        CausesValidation="False" OnClick="BtnDeleteClick" meta:resourcekey="btnDeleteResource1" />
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
                        AllowPaging="True" DataKeyNames="NewsTypeID" DataSourceID="ObjData" ID="gvData"
                        AllowSorting="True" runat="server" OnDataBound="GvDataDataBound" OnPageIndexChanging="GvDataPageIndexChanging">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle Width="5%" />
                                <ItemStyle CssClass="cbxCheck" HorizontalAlign="Center"></ItemStyle>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chckAll" onclick="ChangeAllCheckBoxStates('chckSelect',this.checked);ShowHideBtnEdit();"
                                        runat="server" ToolTip="Chọn/Bỏ chọn tất cả" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chckSelect" onclick="ChangeHeaderAsNeeded('chckSelect','chckAll',$(this));ShowHideBtnEdit();"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tên" SortExpression="NewsTypeName">
                                <HeaderStyle Width="25%"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdNewsTypeID" Value='<%# Eval("NewsTypeID") %>' runat="server" />
                                    <asp:HyperLink ID="hpNewsTypeName" Target="_blank" runat="server" NavigateUrl='<%# CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/hltw" + Eval("NewsTypeID")+ ".aspx" %>'
                                        Text='<%# Eval("TreeView") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nội dung" SortExpression="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbDescription" runat="server" Text='<%# Global.GetSubContent(Eval("Description").ToString(),60) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bài viết" SortExpression="TotalNews">
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLogin" runat="server" Text='<%# Eval("TotalNews") %>'></asp:Label>
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
                    <asp:ObjectDataSource ID="ObjData" runat="server" TypeName="HocLapTrinhWeb.BLL.vnn_NewsTypeBLL"
                        EnablePaging="True" SelectMethod="GetAllNewsTypeForGridView" SelectCountMethod="GetAllNewsTypeRowCount"
                        OnObjectCreating="ObjDataObjectCreating" OnSelected="ObjDataSelected" OnSelecting="ObjDataSelecting"></asp:ObjectDataSource>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNew" />
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="btnEdit" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="divAdd" class="popup">
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdIsAddSuccessful" Value="0" />
                    <asp:HiddenField runat="server" ID="hdEdit" Value="0" />
                    <asp:HiddenField runat="server" ID="hdNewsTypeID" Value="0" />
                    <table cellpadding="3">
                        <tr>
                            <td class="midleft"></td>
                            <td class="miditem">
                                <div style="padding-left: 20px">
                                    <asp:ValidationSummary ID="valError1" ValidationGroup="vSave" runat="server" EnableClientScript="False"></asp:ValidationSummary>
                                    <asp:CustomValidator ID="SaveValidate1" ValidationGroup="vSave" runat="server" Display="None"
                                        ErrorMessage="CustomValidator" /><div class="Spacer">
                                        </div>
                                </div>
                                <table cellpadding="0" cellspacing="0">
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label1" runat="server" CssClass="Caption" Text="Thuộc"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td class="Require"></td>
                                        <td>
                                            <asp:DropDownList ID="dropNewsType" runat="server" AutoPostBack="False" DataSourceID="ObjNewsType"
                                                DataTextField="TreeView" DataValueField="NewsTypeID" OnDataBound="DropNewsTypeDataBound">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="ObjNewsType" runat="server" OnObjectCreating="ObjNewsTypeObjectCreating"
                                                SelectMethod="GetAllNewsTypeForGridView" TypeName="HocLapTrinhWeb.BLL.vnn_NewsTypeBLL"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="lblNewsTypeName" CssClass="Caption" runat="server" Text="Tên"></asp:Label>
                                        </td>
                                        <td class="tdempty"></td>
                                        <td class="Require"></td>
                                        <td>
                                            <asp:TextBox ID="txtNewsTypeName" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3"></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvNewsTypeName" runat="server" ErrorMessage="Phải nhập tên"
                                                Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtNewsTypeName"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label2" runat="server" CssClass="Caption" Text="Nội dung"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="txtDescription" runat="server" Width="300px" TextMode="MultiLine"
                                                Height="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label4" runat="server" CssClass="Caption" Text="Hình ảnh"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:Image ID="ImageUrl" runat="server" Width="80px" />
                                            <br />
                                            <asp:FileUpload ID="fileuploadImage" runat="server" Width="90%" />
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label3" runat="server" CssClass="Caption" Text="Thứ tự"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td class="Require"></td>
                                        <td>
                                            <inno:NumericTextBox ID="txtPriority" runat="server" Alignment="Left" Width="300px"></inno:NumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="right">
                                            <asp:Button ID="btnSave" runat="server" Text="Lưu" ValidationGroup="vAdd" CssClass="button"
                                                OnClick="BtnSaveClick" />
                                            <asp:Button ID="btnSaveAndNew" runat="server" Text="Lưu & Tạo mới" ValidationGroup="vAdd"
                                                CssClass="button" OnClick="BtnSaveAndNewClick" meta:resourcekey="btnSaveAndNewResource1" />
                                            <asp:Button ID="btnCancelAdd" UseSubmitBehavior="False" runat="server" Text="Đóng"
                                                OnClientClick="hidePopup();return false;" CausesValidation="False" CssClass="button" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="midright"></td>
                        </tr>
                        <tr>
                            <td class="footerleft"></td>
                            <td class="footeritem"></td>
                            <td class="footerright"></td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
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
            showPopupDiv('divAdd', 'Thêm NewsType', null, null, true, null, null, null); //absolute
            isAdd = false;
        }
        if (isEdit) {
            showPopupDiv('divAdd', 'Sửa thông tin NewsType', null, null, true, null, null, null);
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
</script>
