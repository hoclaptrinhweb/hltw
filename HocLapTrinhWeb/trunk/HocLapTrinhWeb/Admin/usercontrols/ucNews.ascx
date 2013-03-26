<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucNews.ascx.cs" Inherits="administrator_usercontrols_ucNews" %>
<%@ Register Assembly="LIBDatePicker" Namespace="LIBDatePicker.DatePicker" TagPrefix="cc1" %>
<input id="notselect" type="hidden" value='<%=msg.GetMessage("ERR-000007")%>' />
<input id="deleteconfirm" type="hidden" value='<%=msg.GetMessage("ERR-000008")%>' />
<input id="updateconfirm" type="hidden" value='<%=msg.GetMessage("ERR-000012")%>' />
<input id="hdDateFormat" type="hidden" value="<%= CurrentPage.Language%>" />
<input id="DateError" type="hidden" value="Ngày không hợp lệ" />
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="Tin Tức"></asp:Label></h1>
    </div>
    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <div>
                    <asp:ValidationSummary ID="valError" runat="server" EnableClientScript="False" />
                    <asp:CustomValidator ID="SaveValidate" runat="server" CssClass="FormStyle" Display="None"
                        ErrorMessage="CustomValidator"></asp:CustomValidator>
                </div>
                <div class="box-header">
                    <div style="text-align: left">
                        <asp:Label ID="lbTotal" runat="server"></asp:Label>
                        <asp:Button ID="btnNew" runat="server" CausesValidation="False" CssClass="button"
                            EnableViewState="False" OnClick="BtnNewClick" Text="Thêm mới" />
                        <asp:Button ID="btnEditExpress" runat="server" CausesValidation="False" CssClass="button"
                            OnClick="BtnEditExpressClick" Text="Chỉnh sửa nhanh" />
                        <asp:Button ID="btnEdit" runat="server" CausesValidation="False" CssClass="button"
                            OnClick="BtnEditClick" OnClientClick="if(!IsEdit('chckSelect','notselect')) return;"
                            Text="Chỉnh sửa" UseSubmitBehavior="False" />
                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button"
                            OnClick="BtnDeleteClick" OnClientClick="if(! IsDelete('chckSelect','notselect','deleteconfirm')) return;"
                            Text="Xóa" UseSubmitBehavior="false" />
                        <asp:Button ID="btnMoveNews" runat="server" CausesValidation="False" CssClass="button"
                            OnClientClick="if(!IsEdit('chckSelect','notselect')) return; showClose=true;isMoveNews=true;"
                            Text="Chuyển tin" UseSubmitBehavior="False" />
                        <asp:Button ID="btnSaveExpress" Visible="false" runat="server" CausesValidation="False"
                            CssClass="button" OnClick="BtnSaveExpressClick" OnClientClick="if(!Confirm('updateconfirm')) return;"
                            Text="Lưu thay đổi" UseSubmitBehavior="False" />
                    </div>
                    <table width="100%" cellpadding="0" cellspacing="0" style="font-weight: normal">
                        <tr>
                            <td style="padding-right: 5px">
                                Từ ngày
                            </td>
                            <td>
                                Đến này
                            </td>
                            <td>
                                Chuyên mục
                            </td>
                            <td>
                                <span lang="en-us">Nguồn</span>
                            </td>
                            <td>
                                T.Thái
                            </td>
                            <td>
                                Người tạo
                            </td>
                            <td>
                                <span lang="en-us">D</span>òng
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 5px">
                                <cc1:DatePicker ID="txtFromDate" runat="server" EditInput="True" SetNullValueDate="false"
                                    ShowIconDatePicker="False" Width="70px" />
                            </td>
                            <td>
                                <cc1:DatePicker ID="txtToDate" runat="server" EditInput="True" SetNullValueDate="false"
                                    ShowIconDatePicker="False" Width="70px" />
                            </td>
                            <td>
                                &nbsp;
                                <asp:DropDownList ID="dropNewsType" runat="server" DataSourceID="ObjectDataSource1"
                                    DataTextField="TreeView" DataValueField="NewsTypeID" OnDataBound="DropNewsTypeDataBound"
                                    Width="100px">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OnObjectCreating="ObjectDataSource1ObjectCreating"
                                    SelectMethod="GetAllNewsTypeForGridView" TypeName="HocLapTrinhWeb.BLL.vnn_NewsTypeBLL">
                                </asp:ObjectDataSource>
                            </td>
                            <td>
                                <asp:DropDownList ID="dropRefSite" runat="server" DataSourceID="objRefSite" DataTextField="refsite"
                                    DataValueField="refsite" OnDataBound="DropRefSiteDataBound" Width="100px">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="objRefSite" runat="server" OnObjectCreating="ObjRefSiteObjectCreating"
                                    SelectMethod="GetNewsTypeRefSiteForDropDownList" TypeName="HocLapTrinhWeb.BLL.ltk_ReferenceSiteBLL">
                                </asp:ObjectDataSource>
                            </td>
                            <td>
                                <asp:DropDownList ID="dropIsActive" runat="server" Width="80px">
                                    <asp:ListItem Value="-1">Tất cả</asp:ListItem>
                                    <asp:ListItem Value="0">Chưa kích hoạt</asp:ListItem>
                                    <asp:ListItem Value="1">Đã kích hoạt</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="dropTag" runat="server" Width="80px">
                                    <asp:ListItem Value="-1">Tất cả</asp:ListItem>
                                    <asp:ListItem Value="0">Chưa tag</asp:ListItem>
                                    <asp:ListItem Value="1">Đã tag</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <span lang="en-us">
                                    <asp:DropDownList ID="dropPageSize" runat="server" Width="55px">
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                    </asp:DropDownList>
                                </span>
                            </td>
                            <td>
                                <span lang="en-us"><span lang="en-us">
                                    <asp:Button ID="btnXem" runat="server" CssClass="button" OnClick="BtnXemClick" Text="Xem"
                                        Width="50px" />
                                </span></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="box table">
                    <asp:HiddenField ID="hdEdit" Value="0" runat="server" />
                    <asp:GridView ID="gvData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="0" CssClass="GridStyle" DataKeyNames="NewsID" DataSourceID="ObjData"
                        AllowSorting="true" OnDataBound="GvDataDataBound" Width="100%" OnRowDataBound="GvDataRowDataBound">
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
                            <asp:TemplateField HeaderText="Chủ đề" SortExpression="Title">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdNewsID" Value='<%# Eval("NewsID") %>' runat="server" />
                                    <asp:HiddenField ID="hdNewsTypeID" Value='<%# Eval("NewsTypeID") %>' runat="server" />
                                     <asp:HiddenField ID="hdImage" Value='<%# Eval("Image") %>' runat="server" />
                                    <asp:HiddenField ID="hdThumbnail" Value='<%# Eval("Thumbnail") %>' runat="server" />
                                    <asp:HyperLink ID="hlTitle" runat="server" NavigateUrl='<%# "~/Admin/NewsDetail.aspx?NewsID="+Eval("NewsID") %>'
                                        Text='<%# Eval("Title") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Xem">
                                <HeaderStyle Width="6%" />
                                <ItemTemplate>
                                    <asp:HyperLink ID="htLink" Target="_blank" runat="server" NavigateUrl='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'
                                        Text="Xem"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tác giả" SortExpression="CreatedByUserName">
                                <HeaderStyle Width="8%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedByUserName" runat="server" Text='<%# Eval("CreatedByUserName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Thumb" SortExpression="Thumbnail">
                                <HeaderStyle Width="3%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chckThumbnail" Checked='<%# !string.IsNullOrEmpty(Eval("Thumbnail").ToString()) && Eval("Thumbnail").ToString() != "Upload/Image/noimage.jpg" %>'
                                        Enabled="false" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tin Hot" SortExpression="IsHot">
                                <HeaderStyle Width="3%" />
                                <HeaderTemplate>
                                    Hot<br />
                                    <asp:CheckBox ID="chckAllIsHot" runat="server" Enabled="false" onclick="ChangeAllCheckBoxStates('chckIsHot',this.checked)"
                                        ToolTip="Chọn/Bỏ chọn tất cả" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chckIsHot" Checked='<%# Eval("IsHot") %>' Enabled="false" runat="server"
                                        onclick="ChangeHeaderAsNeeded('chckIsHot','chckAllIsHot');" />
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
                                    <asp:CheckBox ID="chckIsActive" Checked='<%# Eval("IsActive") %>' Enabled="false"
                                        onclick="ChangeHeaderAsNeeded('chckIsActive','chckAllIsActive');" runat="server" />
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
                    <asp:ObjectDataSource ID="ObjData" runat="server" EnablePaging="True" MaximumRowsParameterName="maximumRows"
                        OnObjectCreating="ObjDataObjectCreating" SelectCountMethod="GetAllNewsRowCount"
                        SelectMethod="GetAllNewsForGridView" StartRowIndexParameterName="startRowIndex"
                        TypeName="HocLapTrinhWeb.BLL.vnn_NewsBLL" OnSelected="ObjDataSelected" OnSelecting="ObjDataSelecting">
                        <SelectParameters>
                            <asp:ControlParameter Name="NewsTypeID" ControlID="dropNewsType" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnXem" />
                <asp:AsyncPostBackTrigger ControlID="btnNew" />
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                <asp:AsyncPostBackTrigger ControlID="btnEditExpress" />
                <asp:AsyncPostBackTrigger ControlID="btnMoveNews" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="divMoveNews" class="popup" style="display: none">
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdIsAddSuccessful" Value="0" />
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
                                            <asp:Label ID="lblNewsType" CssClass="Caption" runat="server" Text="Chuyển tin đã chọn sang"></asp:Label>
                                        </td>
                                        <td class="td1empty">
                                        </td>
                                        <td class="Require">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drNewsTypeMove" runat="server" DataSourceID="ObjectDataSource1"
                                                DataTextField="TreeView" DataValueField="NewsTypeID" OnDataBound="DrNewsTypeMoveDataBound">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                            <asp:CompareValidator ID="cvNewsTypeMove" runat="server" ValidationGroup="vMove"
                                                ControlToValidate="drNewsTypeMove" ErrorMessage="" Display="Dynamic" Operator="NotEqual"
                                                SetFocusOnError="true" ValueToCompare="-1"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="right">
                                            <asp:Button ID="btnSave" runat="server" Text="Lưu" ValidationGroup="vMove" CssClass="button"
                                                OnClick="BtnSaveClick" />
                                            <asp:Button ID="btnCancel" UseSubmitBehavior="False" runat="server" Text="Đóng" OnClientClick="hidePopup();"
                                                CausesValidation="False" CssClass="button" OnClick="BtnCancelClick" />
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
    var isMoveNews = false;
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);
    function BeginRequestHandler(sender, args)//begin ajax
    {

    }

    function EndRequestHandler(sender, args)//ajax return value
    {

        if (isMoveNews) {
            showPopupDiv('divMoveNews', 'Dời tin', null, null, true, null, null, null); //absolute
            isAdd = false;
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
