<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPermission.ascx.cs" Inherits="Admin_usercontrols_ucPermission" %>
<input type="hidden" id="notselect" value="<%=msg.GetMessage("ERR-000007")%>" />
<input type="hidden" id="deleteconfirm" value="<%=msg.GetMessage("ERR-000008")%>" />
<input type="hidden" id="hdLanguage" value="<%= CurrentPage.Language%>" />
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="Quản lý Permission"></asp:Label></h1>
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
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lbTotal" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnNew" runat="server" CssClass="button" EnableViewState="False"
                                    Text="Thêm mới" CausesValidation="False" OnClientClick="showClose=true;isAdd=true;"
                                    OnClick="BtnNewClick" />
                                <asp:Button ID="btnEdit" CssClass="button" runat="server" UseSubmitBehavior="False"
                                    Text="Sửa" CausesValidation="False" OnClientClick="if(!IsEdit('chckSelect','notselect')) return; showClose=true;isEdit=true;"
                                    OnClick="BtnEditClick" />
                                <asp:Button ID="btnDelete" CssClass="button" runat="server" UseSubmitBehavior="false"
                                    Text="Xóa" OnClientClick="if(! IsDelete('chckSelect','notselect','deleteconfirm')) return;"
                                    CausesValidation="False" OnClick="BtnDeleteClick" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="box table">
                    <asp:GridView CellPadding="0" Width="100%" CssClass="GridStyle" AutoGenerateColumns="False"
                        AllowPaging="True" DataKeyNames="PermissionID" DataSourceID="ObjData" ID="gvData" runat="server"
                        OnDataBound="GvDataDataBound" OnPageIndexChanging="GvDataPageIndexChanging">
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
                            <asp:TemplateField HeaderText="Permission">
                                <HeaderStyle Width="15%"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdPermissionID" Value='<%# Eval("PermissionID") %>' runat="server" />
                                    <asp:HyperLink ID="hpPermissionID" runat="server" Text='<%# Eval("PermissionID") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mô tả">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
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
                    <asp:ObjectDataSource ID="ObjData" runat="server" TypeName="HocLapTrinhWeb.BLL.vnn_PermissionBLL"
                        MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        EnablePaging="True" SelectMethod="GetAllPermissionForGridView" SelectCountMethod="GetAllPermissionRowCount"
                        OnObjectCreating="ObjDataObjectCreating"  OnSelected="ObjDataSelected" OnSelecting="ObjDataSelecting"></asp:ObjectDataSource>
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
                    <asp:HiddenField runat="server" ID="hdPermissionID" Value="0" />
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
                                            <asp:Label ID="lblUserName" CssClass="Caption" runat="server" Text="Permission"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPermission" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="Nhập Permission"
                                                Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtPermission" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label1" CssClass="Caption" runat="server" Text="Mô tả"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDes" Width="300px" TextMode="MultiLine" Text="" runat="server"
                                                Height="92px"></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvPass" runat="server" ErrorMessage="Nhập nội dung mô tả"
                                                Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtDes" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
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
        if (isAdd)
        {
            showPopupDiv('divAdd', 'Thêm Permission', null, null, true, null, null, null); //absolute
            isAdd = false;
        }
        if (isEdit)
        {
            showPopupDiv('divAdd', 'Sửa thông tin Permission', null, null, true, null, null, null);
            isEdit = false;
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
        if (mCount > 1)
            EnableButton(document.getElementById('<%=btnEdit.ClientID %>'), false);
        else
            EnableButton(document.getElementById('<%=btnEdit.ClientID %>'), true);
    }
    function getScrollXY()
    {
        var scrOfX = 0, scrOfY = 0;
        if (typeof (window.pageYOffset) == 'number')
        {
            /*Netscape compliant*/
            scrOfY = window.pageYOffset;
            scrOfX = window.pageXOffset;
        } else if (document.body && (document.body.scrollLeft || document.body.scrollTop))
        {
            /*DOM compliant*/
            scrOfY = document.body.scrollTop;
            scrOfX = document.body.scrollLeft;
        } else if (document.documentElement && (document.documentElement.scrollLeft || document.documentElement.scrollTop))
        {
            /*IE6 standards compliant mode*/
            scrOfY = document.documentElement.scrollTop;
            scrOfX = document.documentElement.scrollLeft;
        }
        return [scrOfX, scrOfY];
    }
    $(document).ready(function ()
    {
        $(window).scroll(function ()
        {
            var nHeight = getScrollXY()[1];
            var winwidth = document.all ? document.body.clientWidth : window.innerWidth;
            var left = (winwidth - 980) / 2;
            var eleWidth = $('.GridStyle').width();
            if (nHeight >= 170)
                $(".box-header").addClass("topActive").css({ "position": "fixed", "top": "0px", "width": eleWidth - 30 }); // Padding = 15 => eleWidth - 30
            else
                $(".box-header").removeClass("topActive").css({ "position": "", "top": "0px", "width": "" });
        });
    });

</script>
