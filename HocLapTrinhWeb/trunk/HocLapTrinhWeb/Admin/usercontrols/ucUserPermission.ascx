<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUserPermission.ascx.cs"
    Inherits="dhadmincp_usercontrols_ucUserPermission" %>
<input id="notselect" type="hidden" value='<%=msg.GetMessage("ERR-000007")%>' />
<input id="updateconfirm" type="hidden" value='<%=msg.GetMessage("ERR-000012")%>' />
<input id="deleteconfirm" type="hidden" value='<%=msg.GetMessage("ERR-000008")%>' />
<input id="hdDateFormat" type="hidden" value="<%= CurrentPage.Language%>" />
<input id="DateError" type="hidden" value="Ngày không hợp lệ" />
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="UserPermission"></asp:Label></h1>
    </div>
    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <asp:ValidationSummary ID="valError" runat="server" EnableClientScript="False" />
                    <asp:CustomValidator ID="SaveValidate" runat="server" CssClass="FormStyle" Display="None"
                        ErrorMessage="CustomValidator"></asp:CustomValidator>
                </div>
                <div class="box-header">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <div style="text-align: left">
                                    <asp:Button ID="btnNew" runat="server" CssClass="button" EnableViewState="False"
                                        Text="Thêm mới" CausesValidation="False" OnClientClick="showClose=true;isAdd=true;"
                                        OnClick="btnNew_Click" />
                                    <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button"
                                        OnClick="btnDelete_Click" OnClientClick="if(! IsDelete('chckSelect','notselect','deleteconfirm')) return;"
                                        Text="Xóa" UseSubmitBehavior="false" />
                                    <asp:Button ID="btnVTQC" runat="server" PostBackUrl="~/admin/View.aspx?action=role" CausesValidation="False"
                                        CssClass="button" EnableViewState="False" Text="Role" />
                                    <asp:Button ID="btnChude" runat="server" CausesValidation="False" PostBackUrl="~/admin/View.aspx?action=user"
                                        CssClass="button" EnableViewState="False" Text="User" />
                                </div>
                            </td>
                            <td align="right">
                                <asp:Label ID="Label4" runat="server" Text="UserName"></asp:Label>
                                <asp:DropDownList ID="dropUser" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSource1"
                                    DataTextField="UserName" DataValueField="UserID" 
                                    OnDataBound="dropUser_DataBound" Width="100px">
                                </asp:DropDownList>
                                &nbsp;
                                <asp:Label ID="Role" runat="server" Text="Role"></asp:Label>&nbsp;<asp:DropDownList
                                    ID="dropRole" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSource2"
                                    DataTextField="RoleID" DataValueField="RoleID" OnDataBound="dropRole_DataBound">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetAllRoleForGridView"
                                    TypeName="HocLapTrinhWeb.BLL.vnn_RoleBLL" OnObjectCreating="ObjectRole_ObjectCreating">
                                </asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OnObjectCreating="ObjectUser_ObjectCreating"
                                    SelectMethod="GetAllUserForGridView" TypeName="HocLapTrinhWeb.BLL.ltk_UserBLL">
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="box table">
                    <asp:HiddenField ID="hdEdit" Value="0" runat="server" />
                    <asp:GridView ID="gvData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="0" CssClass="GridStyle" DataKeyNames="UserID,RoleID" DataSourceID="ObjData"
                        OnDataBound="gvData_DataBound" Width="100%" AllowSorting="True">
                        <PagerSettings Mode="NumericFirstLast" />
                        <EmptyDataRowStyle HorizontalAlign="Center" />
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
                            <asp:TemplateField HeaderText="UserName" SortExpression="UserName">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hpUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:HyperLink>
                                    <asp:HiddenField ID="hdUserID" Value='<%# Eval("UserID") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RoleID" SortExpression="RoleID">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdRoleID" Value='<%# Eval("RoleID") %>' runat="server" />
                                    <asp:HyperLink ID="hpRoleID" runat="server" Text='<%# Eval("RoleID") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PermissionID" SortExpression="PermissionID">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdPermissionID" Value='<%# Eval("PermissionID") %>' runat="server" />
                                    <asp:HyperLink ID="hpPermissionID" runat="server" Text='<%# Eval("PermissionID") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoItem" runat="server" Text="Chưa có dữ liệu"></asp:Label>
                        </EmptyDataTemplate>
                          <HeaderStyle CssClass="GridTitle" />
                        <RowStyle CssClass="RowStyle" />
                        <AlternatingRowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <PagerStyle CssClass="Paging" HorizontalAlign="Left" />
                        <PagerSettings Mode="NumericFirstLast" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjData" runat="server" EnablePaging="True" MaximumRowsParameterName="maximumRows"
                        OnObjectCreating="ObjData_ObjectCreating" SelectCountMethod="GetAllUserPermissionRowCount"
                        SelectMethod="GetAllUserPermissionForGridView" StartRowIndexParameterName="startRowIndex"
                        TypeName="HocLapTrinhWeb.BLL.vnn_UserPermissionBLL">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="dropUser" Name="UserID" PropertyName="SelectedValue" />
                            <asp:ControlParameter ControlID="dropRole" Name="RoleID" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNew" />
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="divAdd" class="popup" style="display: none">
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdIsAddSuccessful" Value="0" />
                    <asp:HiddenField runat="server" ID="HiddenField1" Value="0" />
                    <asp:HiddenField runat="server" ID="hdUserID" Value="0" />
                    <table cellpadding="3">
                        <tr>
                            <td class="midleft">
                            </td>
                            <td class="miditem">
                                <div style="padding-left: 20px">
                                    <asp:ValidationSummary ID="valError1" ValidationGroup="vSave" runat="server" EnableClientScript="False"
                                        meta:resourcekey="valError1Resource1"></asp:ValidationSummary>
                                    <asp:CustomValidator ID="SaveValidate1" ValidationGroup="vSave" runat="server" Display="None"
                                        ErrorMessage="CustomValidator" /><div class="Spacer">
                                        </div>
                                </div>
                                <table cellpadding="0" cellspacing="0">
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="lblUserName" CssClass="Caption" runat="server" Text="UserName"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dropUserAdd" runat="server" DataSourceID="ObjectDataSource1"
                                                DataTextField="UserName" DataValueField="UserID" OnDataBound="dropUserAdd_DataBound"
                                                Width="220px">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="ObjectDataSource1Add" runat="server" OnObjectCreating="ObjectUser_ObjectCreating"
                                                SelectMethod="GetAllUserForGridView" TypeName="HocLapTrinhWeb.BLL.ltk_UserBLL">
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label1" CssClass="Caption" runat="server" Text="Role"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dropRoleAdd" runat="server" DataSourceID="ObjectDataSource2"
                                                DataTextField="RoleID" DataValueField="RoleID" OnDataBound="dropRoleAdd_DataBound"
                                                Width="220px">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="ObjectDataSource2Add" runat="server" SelectMethod="GetAllRoleForGridView"
                                                TypeName="HocLapTrinhWeb.BLL.vnn_RoleBLL" OnObjectCreating="ObjectRole_ObjectCreating">
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label2" CssClass="Caption" runat="server" Text="Permission"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dropPermissionAdd" runat="server" DataSourceID="ObjectDataSource3Add"
                                                DataTextField="PermissionID" DataValueField="PermissionID" OnDataBound="dropPermission_DataBound"
                                                Width="220px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:ObjectDataSource ID="ObjectDataSource3Add" runat="server" SelectMethod="GetAllPermissionForGridView"
                                                TypeName="HocLapTrinhWeb.BLL.vnn_PermissionBLL" OnObjectCreating="ObjectDataSource3Add_ObjectCreating">
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="right">
                                            <asp:Button ID="btnSave" runat="server" Text="Lưu" ValidationGroup="vAdd" CssClass="button"
                                                OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancelAdd" UseSubmitBehavior="False" runat="server" Text="Đóng"
                                                OnClientClick="hidePopup();" CausesValidation="False" CssClass="button" OnClick="btnCancelAdd_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midright">
                                        </td>
                                        <tr>
                                            <td class="footerleft">
                                            </td>
                                            <td class="footeritem">
                                            </td>
                                            <td class="footerright">
                                            </td>
                                        </tr>
                                    </tr>
                            </td>
                        </tr>
                        </tr>
                    </table>
                    </table> </td>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnNew" />
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
        if (isAdd)
        {
            showPopupDiv('divAdd', 'Thêm UserPermission', null, null, true, null, null, null); //absolute
            isAdd = false;
        }
        var hdIsSuccessful = document.getElementById('<%= hdIsAddSuccessful.ClientID%>');
        if (hdIsSuccessful != null && hdIsSuccessful.value == "1")
        {
            hdIsSuccessful.value = "0";
            hidePopup();
        }
        EventCheckBox();
    }


    function ShowHideBtnEdit()
    {

        var checkedRow;
        var gvData = $("table[id$='gvData']")[0];
        var checkboxs = gvData.getElementsByTagName("input");
        var mCount = 0;
        for (i = 0; i < checkboxs.length; i++)
            if (checkboxs[i].type == "checkbox" && checkboxs[i].checked && checkboxs[i].id.indexOf("chckSelect") > 0)
                mCount++;
    }
</script>
