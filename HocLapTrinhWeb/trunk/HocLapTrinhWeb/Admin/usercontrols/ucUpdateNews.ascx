<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUpdateNews.ascx.cs"
    Inherits="Admin_usercontrols_ucUpdateNews" %>
<input type="hidden" id="deleteconfirm" value="<%=msg.GetMessage("ERR-000008")%>" />
<input type="hidden" id="notselect" value="<%=msg.GetMessage("ERR-000007")%>" />
<input type="hidden" id="hdLanguage" value="<%= CurrentPage.Language%>" />
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="Label6" runat="server" Text="Cập nhật tin tức"></asp:Label></h1>
    </div>
    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="False" />
                    <asp:CustomValidator ID="CustomValidator1" runat="server" Display="None" CssClass="FormStyle"
                        ErrorMessage="CustomValidator" />
                </div>
                <div class="box-header">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <asp:Button ID="btnUpdate" runat="server" CssClass="button" EnableViewState="False"
                                    Text="Cập nhật tin" UseSubmitBehavior="false" OnClientClick="if(!IsEdit('chckSelect','notselect')) return; showClose=true;isUpdate=true;"
                                    OnClick="BtnUpdateClick" />
                                <asp:Button ID="btnRefresh" CssClass="button" runat="server" UseSubmitBehavior="false"
                                    Text="Làm mới trang" OnClick="BtnRefreshClick" />
                                <asp:Button ID="btnDelete" CssClass="button" runat="server" UseSubmitBehavior="False"
                                    Text="Xóa" OnClientClick="if(! IsDelete('chckSelect','notselect','deleteconfirm')) return;"
                                    CausesValidation="False" OnClick="BtnDeleteClick" />
                                <asp:Button ID="btnEdit" CssClass="button" runat="server" UseSubmitBehavior="False"
                                    Text="Sửa" CausesValidation="False" OnClientClick="if(!IsEdit('chckSelect','notselect')) return; showClose=true;isEdit=true;"
                                    OnClick="BtnEditClick" />
                                <asp:Button ID="btnNew" runat="server" CssClass="button" EnableViewState="False"
                                    Text="Thêm" CausesValidation="False" OnClientClick="showClose=true;isAdd=true;"
                                    OnClick="BtnNewClick" />
                            </td>
                            <td align="right">
                                <asp:Label ID="lbTotal" runat="server"></asp:Label>
                                <asp:Label ID="Label8" CssClass="Caption" runat="server" Text=" Loại tin: "></asp:Label>
                                <asp:ObjectDataSource ID="ObjNewsType" runat="server" OnObjectCreating="ObjNewsTypeObjectCreating"
                                    SelectMethod="GetAllNewsTypeForGridView" TypeName="HocLapTrinhWeb.BLL.vnn_NewsTypeBLL">
                                </asp:ObjectDataSource>
                                <asp:DropDownList ID="drNewsType" AutoPostBack="True" DataTextField="TreeView" DataValueField="NewsTypeID"
                                    runat="server" OnDataBound="DrNewsTypeDataBound" OnSelectedIndexChanged="DrNewsTypeSelectedIndexChanged"
                                    DataSourceID="ObjNewsType">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="box table">
                    <asp:GridView CellPadding="0" Width="100%" CssClass="GridStyle" AutoGenerateColumns="False"
                        AllowPaging="True" AllowSorting="true" DataKeyNames="ReferenceSiteID" DataSourceID="ObjData"
                        ID="gvData" runat="server" OnDataBound="GvDataDataBound" OnPageIndexChanging="GvDataPageIndexChanging"
                        OnRowDataBound="GvDataRowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle Width="5%" />
                                <ItemStyle CssClass="cbxCheck" HorizontalAlign="Center"></ItemStyle>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chckAll" onclick="ChangeAllCheckBoxStates('chckSelect',this.checked);ShowHideBtnEdit();"
                                        runat="server" ToolTip="Chọn/Bỏ chọn tất cả" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chckSelect" runat="server" onclick="ChangeHeaderAsNeeded('chckSelect','chckAll',$(this));ShowHideBtnEdit();" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Loại tin" SortExpression="NewsTypeName">
                                <HeaderStyle></HeaderStyle>
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdNewsTypeID" Value='<%# Eval("NewsTypeID") %>' runat="server" />
                                    <asp:HiddenField ID="hdReferenceSiteID" Value='<%# Eval("ReferenceSiteID") %>' runat="server" />
                                    <asp:Label ID="lblNewsTypeName" runat="server" Text='<%# Eval("NewsTypeName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nguồn" SortExpression="RefSite">
                                <HeaderStyle Width="10%" />
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:Label ID="lblRefSite" runat="server" Text='<%# Eval("RefSite") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Giờ cập nhật cuối" SortExpression="UpdatedDate">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%# DateTime.Parse(Eval("UpdatedDate").ToString(), new System.Globalization.CultureInfo(CurrentPage.Language), System.Globalization.DateTimeStyles.None).ToString() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Số tin" SortExpression="UpdateRows">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUpdateRows" runat="server" Text='<%# Eval("UpdateRows") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tự động" SortExpression="IsAutoRun">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chckIsAutoRun" Checked='<%# Eval("IsAutoRun") %>' Enabled="false"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="T.Trạng">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Image ID="imgNews" runat="server" Visible="true" ToolTip="Mới cập nhật" Width="25px"
                                        ImageUrl="~/admin/img/check.png" />
                                    <asp:Image ID="imgWarning" runat="server" Visible="true" ToolTip="Đã lâu không cập nhật"
                                        Width="25px" ImageUrl="~/admin/img/warning.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridTitle" />
                        <RowStyle CssClass="RowStyle" />
                        <AlternatingRowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <PagerStyle CssClass="Paging" HorizontalAlign="Left" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoItem" runat="server" Text="Chưa có dữ liệu"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjData" runat="server" TypeName="HocLapTrinhWeb.BLL.ltk_ReferenceSiteBLL"
                        MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        EnablePaging="True" SelectMethod="GetNewsTypeRefSiteForGridView" SelectCountMethod="GetNewsTypeRefSiteRowCount"
                        OnObjectCreating="ObjDataObjectCreating" OnSelected="ObjDataSelected" OnSelecting="ObjDataSelecting">
                        <SelectParameters>
                            <asp:ControlParameter Name="newsTypeID" ControlID="drNewsType" Type="int32" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNew" />
                <asp:AsyncPostBackTrigger ControlID="btnRefresh" />
                <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                <asp:AsyncPostBackTrigger ControlID="drNewsType" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="divAdd" style="display: none;">
            <asp:UpdatePanel ID="updatepanel3" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdIsAddSuccessful" Value="0" />
                    <asp:HiddenField runat="server" ID="hdEdit" Value="0" />
                    <asp:HiddenField runat="server" ID="hdReferenceSiteID" Value="0" />
                    <table cellpadding="3">
                        <tr>
                            <td class="midleft">
                            </td>
                            <td class="miditem">
                                <div style="padding-left: 20px">
                                    <asp:ValidationSummary ID="ValidationSummary2" ValidationGroup="vSave" runat="server"
                                        EnableClientScript="False"></asp:ValidationSummary>
                                    <asp:CustomValidator ID="CustomValidator2" ValidationGroup="vSave" runat="server"
                                        Display="None" ErrorMessage="CustomValidator" /><div class="Spacer">
                                        </div>
                                </div>
                                <table cellpadding="0" cellspacing="0">
                                    <tr valign="top">
                                        <td>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drNewsTypeAdd" runat="server" DataTextField="TreeView" DataValueField="NewsTypeID"
                                                OnDataBound="DrNewsTypeAddDataBound" DataSourceID="ObjNewsType0">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="ObjNewsType0" runat="server" OnObjectCreating="ObjNewsTypeObjectCreating"
                                                SelectMethod="GetAllNewsTypeForGridView" TypeName="HocLapTrinhWeb.BLL.vnn_NewsTypeBLL">
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="3">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Link Page {paraPage}" CssClass="Caption"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLinkPage" runat="server" Width="528px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="3">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label1" runat="server" CssClass="Caption" Text="Total Page"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTotalPage" runat="server" Width="528px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="3">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label4" runat="server" CssClass="Caption" Text="Tiêu đề (Para)"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtParaTitle" runat="server" Width="528px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="3">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label9" runat="server" CssClass="Caption" Text="Nội dung tóm tắt (Para)"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtParaBrief" runat="server" Width="528px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="3">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label3" runat="server" CssClass="Caption" Text="Hình ảnh (Para)"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtParaImage" runat="server" Width="528px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="3">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label5" runat="server" CssClass="Caption" Text="Nội dung chi tiết (Para)"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtParaContent" runat="server" Width="528px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="3">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label10" runat="server" CssClass="Caption" Text="Tự động"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="cbAuto" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="3">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:GridView ID="gvData1" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# ((ArrayList)(Container.DataItem))[2] %>'
                                                                runat="server"><%# ((ArrayList)(Container.DataItem))[0] %></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="right">
                                            <asp:Button ID="btnUpdates" runat="server" CssClass="button" Text="Test" OnClick="BtnUpdatesClick" />
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
        <div id="divUpdate" class="popup" style="display: none; z-index: 900">
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdIsAddSuccessful1" Value="0" />
                    <table cellpadding="3">
                        <tr>
                            <td class="midleft">
                            </td>
                            <td class="miditem">
                                <div style="padding-left: 20px">
                                    <asp:ValidationSummary ID="valError1" ValidationGroup="vSave" runat="server" EnableClientScript="False">
                                    </asp:ValidationSummary>
                                    <asp:CustomValidator ID="SaveValidate1" ValidationGroup="vSave" runat="server" Display="None"
                                        ErrorMessage="" /><div class="Spacer">
                                        </div>
                                </div>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label7" runat="server" Style="font-size: 30px; font-weight: bold"
                                                Text="Website đang cập nhật tin tức..."></asp:Label>
                                        </td>
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
    var isUpdate = false; var isAdd = false, isEdit = false;
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);
    function BeginRequestHandler(sender, args) {
        document.title = "Bắt đầu";
        if (isUpdate) {
            showPopupDiv('divUpdate', 'Cập nhật tin tức', 200, null, true, null, null, null); //absolute
            isUpdate = false;
        }
        if (isAdd) {
            showPopupDiv('divAdd', 'Thêm', null, null, true, null, null, null); //absolute
            isAdd = false;
        }
        if (isEdit) {
            showPopupDiv('divAdd', 'Sửa thông tin user', null, null, true, null, null, null);
            isEdit = false;
        }
        var hdIsSuccessful = document.getElementById('<%= hdIsAddSuccessful.ClientID %>');
        if (hdIsSuccessful != null && hdIsSuccessful.value == "1") {
            hdIsSuccessful.value = "0";
            hidePopup();
        }
    }

    function EndRequestHandler(sender, args) {
        var hdIsSuccessful = document.getElementById('<%= hdIsAddSuccessful.ClientID%>');
        if (hdIsSuccessful != null && hdIsSuccessful.value == "1") {
            hdIsSuccessful.value = "0";
            hidePopup();
        }
        var hdIsSuccessful1 = document.getElementById('<%= hdIsAddSuccessful1.ClientID%>');
        if (hdIsSuccessful1 != null && hdIsSuccessful1.value == "1") {
            hdIsSuccessful1.value = "0";
            document.title = "Cập nhật xong ";
            hidePopup();
        }
        EventCheckBox();

    }

    function ShowHideBtnEdit()
    { }
  
</script>
