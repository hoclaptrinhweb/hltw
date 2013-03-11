<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUpForum.ascx.cs" Inherits="Admin_Forumcontrols_ucUpForum" %>
<link href="css/jquery.contextMenu.css" rel="stylesheet" type="text/css" />
<input type="hidden" id="notselect" value="<%=msg.GetMessage("ERR-000007")%>" />
<input type="hidden" id="deleteconfirm" value="<%=msg.GetMessage("ERR-000008")%>" />
<input type="hidden" id="hdLanguage" value="<%= CurrentPage.Language%>" />

<script type="text/javascript" language="javascript" src="js/EventCheckBoxInGridView.js"></script>

<script type="text/javascript" language="javascript" src="js/jquery.contextMenu.js"></script>

<input type="hidden" id="DateError" value="Ngày không hợp lệ" />
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="Quản lý Forum"></asp:Label></h1>
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
                                        Text="Thêm Forum" CausesValidation="False" OnClientClick="showClose=true;isAdd=true;"
                                        OnClick="BtnNewClick" />
                                    <asp:Button ID="btnEdit" CssClass="button" runat="server" UseSubmitBehavior="False"
                                        Text="Sửa" CausesValidation="False" OnClientClick="if(!IsEdit('chckSelect','notselect')) return; showClose=true;isEdit=true;"
                                        OnClick="BtnEditClick" />
                                    <asp:Button ID="btnNewUser" CssClass="button" runat="server" UseSubmitBehavior="False"
                                        Text="Thêm User" CausesValidation="False" OnClientClick="if(! IsAddUser('chckSelect','notselect')) return;isAddUser =true" />
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
                        AllowPaging="True" DataKeyNames="ForumID" DataSourceID="ObjData" ID="gvData"
                        AllowSorting="True" runat="server" OnDataBound="GvDataDataBound" OnPageIndexChanging="GvDataPageIndexChanging">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle Width="5%"></HeaderStyle>
                                <ItemStyle CssClass="cbxCheck" HorizontalAlign="Center"></ItemStyle>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chckAll" onclick="ChangeAllCheckBoxStates('chckSelect',this.checked);ShowHideBtnEdit();"
                                        runat="server" ToolTip="Chọn/Bỏ chọn tất cả" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chckSelect" onclick="ChangeHeaderAsNeeded('chckSelect','chckAll',this);ShowHideBtnEdit();"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ForumName" SortExpression="ForumName">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdForumID" Value='<%# Eval("ForumID") %>' runat="server" />
                                    <asp:HyperLink ID="hpForumName" runat="server" Text='<%# Eval("ForumName") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Alexa" SortExpression="Alexa">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblAlexa" ForeColor="blue" runat="server" Text='<%# String.Format("{0:0,0}", double.Parse(Eval("Alexa").ToString())) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TotalUser" SortExpression="TotalUser">
                                <HeaderStyle Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalUser" runat="server" Text='<%# Eval("TotalUser") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridTitle" />
                        <RowStyle CssClass="GridItem" />
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoItem" runat="server" Text="Updating data"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjData" runat="server" TypeName="HocLapTrinhWeb.BLL.vnn_UpForumBLL"
                        EnablePaging="True" SelectMethod="GetAllForumForGridView" SelectCountMethod="GetAllForumRowCount"
                        OnObjectCreating="ObjDataObjectCreating" OnSelected="ObjDataSelected" OnSelecting="ObjDataSelecting">
                    </asp:ObjectDataSource>
                </div>
                <div>
                    <asp:FileUpload ID="fileImport" runat="server" />
                    <asp:Button ID="btnUpLoad" CssClass="button" runat="server" UseSubmitBehavior="False"
                        Text="Upload" CausesValidation="False" OnClick="BtnUpLoadClick" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNew" />
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                <asp:PostBackTrigger ControlID="btnUpLoad" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="divAdd" class="popup" style="display: none">
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdIsAddSuccessful" Value="0" />
                    <asp:HiddenField runat="server" ID="hdEdit" Value="0" />
                    <asp:HiddenField runat="server" ID="hdForumID" Value="0" />
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
                                            <asp:Label ID="lblForumName" CssClass="Caption" runat="server" Text="ForumName"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtForumName" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvForumName" runat="server" ErrorMessage="Phải nhập tên"
                                                Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtForumName" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label3" runat="server" CssClass="Alexa" Text="Alexa"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAlexa" Width="300px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label1" runat="server" CssClass="Alexa" Text="Login"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLogin" Width="300px" runat="server"></asp:TextBox>
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
        <div id="divAddUser" class="popup" style="display: none">
            <asp:UpdatePanel ID="updatepanel3" runat="server">
                <ContentTemplate>
                    <table cellpadding="3">
                        <tr>
                            <td class="midleft">
                            </td>
                            <td class="miditem">
                                <div style="padding-left: 20px">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="False">
                                    </asp:ValidationSummary>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" Display="None" ErrorMessage="CustomValidator" /><div
                                        class="Spacer">
                                    </div>
                                </div>
                                <table cellpadding="0" cellspacing="0">
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="Label4" runat="server" CssClass="Alexa" Text="UserName"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUserName" runat="server" autocomplete="off" CssClass="ui-autocomplete-input"></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender ID="autoComplete1" runat="server" CompletionInterval="100"
                                                CompletionSetCount="20" EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetUserNameList"
                                                ServicePath="../../AutoComplete.asmx" TargetControlID="txtUserName">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="right">
                                            <asp:Button ID="btnAddNewUser" runat="server" Text="Lưu" UseSubmitBehavior="False"
                                                CssClass="button" OnClick="BtnAddNewUserClick" />
                                            <asp:Button ID="btnCloseUser" UseSubmitBehavior="False" runat="server" Text="Đóng"
                                                OnClientClick="hidePopup();" CausesValidation="False" CssClass="button" />
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
                    <asp:AsyncPostBackTrigger ControlID="btnAddNewUser" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<!-- Click Right Menucontent -->
<ul id="myMenucontent" class="contextMenu">
    <li class="open"><a href="#open">Open Link</a></li>
    <li class="edit"><a href="#createUse">Create Use</a></li>
    <li class="quit separator"><a href="#cancel">Cancel</a></li>
</ul>
<!-- End Click Right Menucontent -->

<script type="text/javascript">
        var isAdd=false, isEdit=false;isAddUser=false;
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
                    showPopupDiv('divAdd','Thêm Forum', null, null, true,null,null,null);//absolute
                    isAdd=false;
            }
             if(isEdit)
            {
                    showPopupDiv('divAdd','Sửa thông tin Forum', null, null, true,null,null,null);
                    isEdit=false;
            }
            if(isAddUser)
            {
                showPopupDiv('divAddUser','Thêm User trong Forum', null, null, true,null,null,null);
                isAddUser=false;
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
                if (action == "createUse") {
                    idnews = $(el.children()[1]).children()[0].value;
                    alert(idnews);
                }
                else if (action == "open")
                {
                    window.open($(el.children()[1]).children()[1].text,"_blank");
                }
            });
            AddEventCheckBox();
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
         function getScrollXY() {
            var scrOfX = 0, scrOfY = 0;
            if (typeof (window.pageYOffset) == 'number') {
                /*Netscape compliant*/
                scrOfY = window.pageYOffset;
                scrOfX = window.pageXOffset;
            } else if (document.body && (document.body.scrollLeft || document.body.scrollTop)) {
                /*DOM compliant*/
                scrOfY = document.body.scrollTop;
                scrOfX = document.body.scrollLeft;
            } else if (document.documentElement && (document.documentElement.scrollLeft || document.documentElement.scrollTop)) {
                /*IE6 standards compliant mode*/
                scrOfY = document.documentElement.scrollTop;
                scrOfX = document.documentElement.scrollLeft;
            }
            return [scrOfX, scrOfY];
        }
        
        $(document).ready(function() {
            $(window).scroll(function() {
                var nHeight = getScrollXY()[1];
                var winwidth = document.all ? document.body.clientWidth : window.innerWidth;
                var left = (winwidth - 980)/2;
                var eleWidth = $('.GridStyle').width();        
                if (nHeight >= 114)
                    $("#sidebar").css({"position":"fixed","top":"0px"});
                else
                    $("#sidebar").css({"position":"","top":"0px"});
                
                if (nHeight >= 170)
                    $(".box-header").addClass("topActive").css({"position":"fixed","top":"0px","width":eleWidth - 30}); // Padding = 15 => eleWidth - 30       
                else
                    $(".box-header").removeClass("topActive").css({"position":"","top":"0px","width":""});
            });
            
            $(".GridStyle tr:not(.GridTitle)").contextMenu({
                menu: 'myMenucontent'
            }, function(action, el, pos) {
                if (action == "createUse") {
                    idnews = $(el.children()[1]).children()[0].value;
                    alert(idnews);
                }
                else if (action == "open")
                {
                    window.open($(el.children()[1]).children()[1].text,"_blank");
                }
            });
        });


</script>

