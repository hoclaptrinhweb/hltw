<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCommentNews.ascx.cs"
    Inherits="Admin_usercontrols_ucCommentNews" %>
<input type="hidden" id="notselect" value="<%=msg.GetMessage("ERR-000007")%>" />
<input type="hidden" id="deleteconfirm" value="<%=msg.GetMessage("ERR-000008")%>" />
<input type="hidden" id="updateconfirm" value='<%=msg.GetMessage("ERR-000012")%>' />
<input type="hidden" id="hdLanguage" value="<%= CurrentPage.Language%>" />
<input type="hidden" id="DateError" value="Ngày không hợp lệ" />
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="Bình luận"></asp:Label>
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
                        <asp:Button ID="btnEditExpress" runat="server" CausesValidation="False" CssClass="button"
                            OnClick="BtnEditExpressClick" Text="Chỉnh sửa nhanh" />
                        <asp:Button ID="btnSaveExpress" runat="server" CausesValidation="False" CssClass="button"
                            OnClick="BtnSaveExpressClick" OnClientClick="if(!Confirm('updateconfirm')) return;"
                            Text="Lưu thay đổi" UseSubmitBehavior="False" />
                        <asp:Button ID="btnEdit" CssClass="button" runat="server" UseSubmitBehavior="False"
                            Text="Sửa" CausesValidation="False" OnClientClick="if(!IsEdit('chckSelect','notselect')) return; showClose=true;isEdit=true;"
                            OnClick="BtnEditClick" />
                        <asp:Button ID="btnDelete" CssClass="button" runat="server" UseSubmitBehavior="False"
                            Text="Xóa" OnClientClick="if(! IsDelete('chckSelect','notselect','deleteconfirm')) return;"
                            CausesValidation="False" OnClick="BtnDeleteClick" />
                        <asp:DropDownList ID="dropCommentNewsType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropCommentNewsTypeSelectedIndexChanged">
                            <asp:ListItem Value="-1">Tất cả</asp:ListItem>
                            <asp:ListItem Value="1">Kích hoạt</asp:ListItem>
                            <asp:ListItem Value="0">Chưa kích hoạt</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="box table">
                    <asp:GridView CellPadding="0" Width="100%" CssClass="GridStyle" AutoGenerateColumns="False"
                        AllowPaging="True" DataKeyNames="CommentNewsID" DataSourceID="ObjData" ID="gvData"
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
                                <HeaderStyle Width="10%"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdCommentNewsID" Value='<%# Eval("CommentNewsID") %>' runat="server" />
                                    <asp:HyperLink ID="hpusername" Target="_blank" runat="server" NavigateUrl='<%# CurrentPage.UrlRoot + "/newsdetail.aspx?newsid="+ Eval("NewsID") %>'
                                        Text='<%# Eval("username") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nội dung">
                                <ItemTemplate>
                                    <asp:Label ID="lbContent" runat="server" Text='<%# Eval("Content") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ngày đăng" SortExpression="CreatedDate">
                                <HeaderStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#DateTime.Parse(Eval("CreatedDate").ToString(), new System.Globalization.CultureInfo(CurrentPage.Language), System.Globalization.DateTimeStyles.None).ToString()  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Duyệt tin" SortExpression="IsActive">
                                <HeaderStyle Width="3%" />
                                <HeaderTemplate>
                                    Duyệt<br />
                                    <asp:CheckBox ID="chckAllIsActive" runat="server" Enabled="false" onclick="ChangeAllCheckBoxStates('chckIsActive',this.checked)"
                                        ToolTip="Chọn/Bỏ chọn tất cả" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chckIsActive" Checked='<%# int.Parse(Eval("IsActive").ToString()) == 0 ? false : true %>'
                                        Enabled="false" onclick="ChangeHeaderAsNeeded('chckIsActive','chckAllIsActive');"
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
                    <asp:ObjectDataSource ID="ObjData" runat="server" TypeName="HocLapTrinhWeb.BLL.vnn_CommentNewsBLL"
                        EnablePaging="True" SelectMethod="GetAllCommentNewsForGridView" SelectCountMethod="GetAllCommentNewsRowCount"
                        OnObjectCreating="ObjDataObjectCreating" OnSelected="ObjDataSelected" OnSelecting="ObjDataSelecting">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="dropCommentNewsType" Name="IsActive" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                <asp:AsyncPostBackTrigger ControlID="btnEditExpress" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="divAdd" class="popup" style="display: none">
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdIsAddSuccessful" Value="0" />
                    <asp:HiddenField runat="server" ID="hdEdit" Value="0" />
                    <asp:HiddenField runat="server" ID="hdCommentNewsID" Value="0" />
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
                                            <asp:Label ID="Label1" CssClass="Caption" runat="server" Text="Kích hoạt"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="cbIsActive" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr valign="top">
                                        <td style="width: 100px">
                                            <asp:Label ID="lblCommentNewsName" CssClass="Caption" runat="server" Text="Tên người gửi"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="txtUsername" Target="_blank" runat="server"></asp:HyperLink>
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
                                            <asp:HyperLink ID="txtTitleNews" Target="_blank" runat="server"></asp:HyperLink>
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
                                            <asp:TextBox ID="txtContent" runat="server" Height="150px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="right">
                                            <asp:Button ID="btnSave" runat="server" Text="Lưu" ValidationGroup="vAdd" CssClass="button"
                                                OnClick="BtnSaveClick" />
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
            showPopupDiv('divAdd', 'Thêm CommentNews', null, null, true, null, null, null); //absolute
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

  

</script>
