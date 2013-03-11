<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUpPost.ascx.cs" Inherits="Admin_usercontrols_ucUpPost" %>
<input type="hidden" id="notselect" value="<%=msg.GetMessage("ERR-000007")%>" />
<input type="hidden" id="deleteconfirm" value="<%=msg.GetMessage("ERR-000008")%>" />
<input type="hidden" id="hdLanguage" value="<%= CurrentPage.Language%>" />
<input type="hidden" id="DateError" value="Ngày không hợp lệ" />
<link href="css/jquery.contextMenu.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" language="javascript" src="js/jquery.contextMenu.js"></script>

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
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div style="text-align: left">
                                    <asp:Button ID="btnNew" runat="server" CssClass="button" EnableViewState="False"
                                        Text="Thêm" CausesValidation="False" OnClientClick="showClose=true;isAdd=true;"
                                        OnClick="BtnNewClick" />
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
                        AllowPaging="True" DataKeyNames="PostID" DataSourceID="ObjData" ID="gvData" AllowSorting="True"
                        runat="server" OnDataBound="GvDataDataBound" OnPageIndexChanging="GvDataPageIndexChanging"
                        OnRowDataBound="GvDataRowDataBound">
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
                            <asp:TemplateField HeaderText="RefAddress" SortExpression="RefAddress">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdPostID" Value='<%# Eval("PostID") %>' runat="server" />
                                    <asp:HyperLink ID="hpRefAddress" runat="server" Text='<%# Eval("RefAddress") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="UserName" SortExpression="UserName">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" ForeColor="blue" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="CreatedDate" SortExpression="CreatedDate">
                                <HeaderStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#DateTime.Parse(Eval("CreatedDate").ToString(), new System.Globalization.CultureInfo(CurrentPage.Language), System.Globalization.DateTimeStyles.None).ToString()  %>'></asp:Label>
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
                    <asp:ObjectDataSource ID="ObjData" runat="server" TypeName="HocLapTrinhWeb.BLL.vnn_UpPostBLL"
                        EnablePaging="True" SelectMethod="GetAllPostForGridView" SelectCountMethod="GetAllPostRowCount"
                        OnObjectCreating="ObjDataObjectCreating" OnSelected="ObjDataSelected" OnSelecting="ObjDataSelecting">
                    </asp:ObjectDataSource>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="divAdd" class="popup" style="display: none">
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdIsAddSuccessful" Value="0" />
                    <asp:HiddenField runat="server" ID="hdEdit" Value="0" />
                    <asp:HiddenField runat="server" ID="hdPostID" Value="0" />
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
                                            <asp:Label ID="lblUserName" CssClass="Caption" runat="server" Text="URL"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUrl" runat="server" Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="Phải nhập url"
                                                Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtUrl" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label1" CssClass="Caption" runat="server" Text="Tên user"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUser" Width="500px" runat="server"></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvPass" runat="server" ErrorMessage="Phải nhập mật khẩu"
                                                Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtUser" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:AutoCompleteExtender runat="server" ID="autoComplete1" TargetControlID="txtUser"
                                                ServicePath="../../AutoComplete.asmx" ServiceMethod="GetUserNameList" MinimumPrefixLength="0"
                                                CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" />
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr class="trEmpty">
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
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<!-- Click Right Menucontent -->
<ul id="myMenucontent" class="contextMenu">
    <li class="open"><a href="#open">Open Link</a></li>
    <li class="edit"><a href="#post">Post Bài</a></li>
    <li class="quit separator"><a href="#cancel">Hủy</a></li>
</ul>
<!-- End Click Right Menucontent -->

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
                    showPopupDiv('divAdd','Thêm user', null, null, true,null,null,null);//absolute
                    isAdd=false;
            }
             if(isEdit)
            {
                    showPopupDiv('divAdd','Sửa thông tin user', null, null, true,null,null,null);
                    isEdit=false;
            }
            var hdIsSuccessful = document.getElementById('<%= hdIsAddSuccessful.ClientID%>');
            if(hdIsSuccessful!=null && hdIsSuccessful.value=="1")
            {
                hdIsSuccessful.value = "0";
                hidePopup();
            }
            $(".GridStyle tr:not(.GridTitle)").contextMenu({
                menu: 'myMenucontent'
            }, function(action, el, pos) {
                if (action == "post") {
                    idnews = $(el.children()[1]).children()[0].value;
                    $.ajax({
                       type: 'GET',
                       cache: false,
                       url: 'upAjaxPost.aspx',
                       data: 'newsid=' + idnews + "&doPost=postreply"
                       ,success: function(data) {
                            alert(data);
                   }});
                }
                else if (action == "open")
                {
                    window.open($(el.children()[1]).children()[1].text,"_blank");
                }
            });
        }
        
        $(document).ready(function() {    
            $(".GridStyle tr:not(.GridTitle)").contextMenu({
                menu: 'myMenucontent'
            }, function(action, el, pos) {
                if (action == "post") {
                    idnews = $(el.children()[1]).children()[0].value;
                    $.ajax({
                       type: 'GET',
                       cache: false,
                       url: 'upAjaxPost.aspx',
                       data: 'newsid=' + idnews + "&doPost=postreply"
                       ,success: function(data) {
                            alert(data);
                   }});
                }
                else if (action == "open")
                {
                    window.open($(el.children()[1]).children()[1].text,"_blank");
                }
            });
       });
</script>

