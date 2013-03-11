<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UpCategory.ascx.cs" Inherits="Admin_usercontrols_UpCategory" %>
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
                    <asp:Label ID="lblBoxHeader" runat="server" Text="Danh sách"></asp:Label>
                </div>
                <div class="box table">
                    <asp:GridView CellPadding="0" Width="100%" CssClass="GridStyle" AutoGenerateColumns="False"
                        AllowPaging="True" DataKeyNames="CategoryID" DataSourceID="ObjData" ID="gvData"
                        AllowSorting="True" runat="server" OnDataBound="GvDataDataBound" OnPageIndexChanging="GvDataPageIndexChanging">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chckAll" onclick="ChangeAllCheckBoxStates('chckSelect',this.checked);ShowHideBtnEdit();"
                                        runat="server" ToolTip="Select all/Unselect all" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chckSelect" onclick="ChangeHeaderAsNeeded('chckSelect','chckAll');ShowHideBtnEdit();"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CategoryName" SortExpression="CategoryName">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdCategoryID" Value='<%# Eval("CategoryID") %>' runat="server" />
                                    <asp:HyperLink ID="hpCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ParentName" SortExpression="ParentName">
                                <ItemTemplate>
                                    <asp:Label ID="lblParentName" runat="server" Text='<%# Eval("ParentName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="priority" SortExpression="priority">
                                <HeaderStyle Width="5%"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblLogin" runat="server" Text='<%# Eval("priority") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridTitle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoItem" runat="server" Text="Updating data"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjData" runat="server" TypeName="HocLapTrinhWeb.BLL.vnn_UpCategoryBLL"
                        EnablePaging="True" SelectMethod="GetAllCategoryForGridView" SelectCountMethod="GetAllCategoryRowCount"
                        OnObjectCreating="ObjDataObjectCreating"></asp:ObjectDataSource>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNew" />
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="btnEdit" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="divAdd" class="popup" style="display: none">
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdIsAddSuccessful" Value="0" />
                    <asp:HiddenField runat="server" ID="hdEdit" Value="0" />
                    <asp:HiddenField runat="server" ID="hdCategoryID" Value="0" />
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
                                            <asp:Label ID="Label1" runat="server" CssClass="Alexa" Text="Loại"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dropCategory" runat="server" AutoPostBack="False" DataSourceID="ObjCategory"
                                                DataTextField="CategoryName" DataValueField="CategoryID" OnDataBound="DropCategoryDataBound">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="ObjCategory" runat="server" OnObjectCreating="ObjCategoryObjectCreating"
                                                SelectMethod="GetAllCategoryForGridView" TypeName="HocLapTrinhWeb.BLL.vnn_UpCategoryBLL">
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="lblCategoryName" CssClass="Caption" runat="server" Text="CategoryName"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCategoryName" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" ErrorMessage="Phải nhập tên"
                                                Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtCategoryName"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label3" runat="server" CssClass="Alexa" Text="Priority"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPriority" Width="300px" runat="server" Text="0"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr class="trEmpty">
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
        <div>
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
    </div>
</div>

<script type="text/javascript">
        var isAdd=false, isEdit=false;
        var prm = Sys.WebForms.PageRequestManager.getInstance(); 
        prm.add_beginRequest(BeginRequestHandler);   
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args)//begin ajax
        { 
           
        }
        
        function EndRequestHandler(sender, args)//ajax return value
        { 
            if(isAdd)
            {
                    showPopupDiv('divAdd','Thêm Category', null, null, true,null,null,null);//absolute
                    isAdd=false;
            }
             if(isEdit)
            {
                    showPopupDiv('divAdd','Sửa thông tin Category', null, null, true,null,null,null);
                    isEdit=false;
            }
            var hdIsSuccessful = document.getElementById('<%= hdIsAddSuccessful.ClientID%>');
            if(hdIsSuccessful!=null && hdIsSuccessful.value=="1")
            {
                hdIsSuccessful.value = "0";
                hidePopup();
            }
        }
            
        function ShowHideBtnEdit()
        {   
            var checkedRow;   
            var gvData = $("table[id$='gvData']")[0];           
            var checkboxs = gvData.getElementsByTagName("input");     
            var mCount = 0;
            for(i=0;i<checkboxs.length;i++)
                if(checkboxs[i].type=="checkbox" && checkboxs[i].checked && checkboxs[i].id.indexOf("chckSelect") >0)
                   mCount++;
            if(mCount > 1)
                EnableButton(document.getElementById('<%=btnEdit.ClientID %>'),false);   
            else
                EnableButton(document.getElementById('<%=btnEdit.ClientID %>'),true);   
        }


</script>

