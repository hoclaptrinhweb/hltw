<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUpNewsDetail.ascx.cs"
    Inherits="Admin_usercontrols_ucUpNewsDetail" %>
<%@ Register Assembly="INNO.WebControls" Namespace="INNO.WebControls" TagPrefix="inno" %>
<link href="Js/bbeditor/bbeditor.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="Js/bbeditor/bbeditor.js"></script>

<input id="notselect" type="hidden" value='<%=msg.GetMessage("ERR-000007")%>' />
<input id="deleteconfirm" type="hidden" value='<%=msg.GetMessage("ERR-000008")%>' />
<input id="hdDateFormat" type="hidden" value="<%= CurrentPage.Language%>" />
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="Tin Tức"></asp:Label>&nbsp;</h1>
    </div>
    <div class="container">
        <div class="box-header">
            <asp:Label ID="lblBoxHeader" runat="server" Text="Thêm / Chỉnh sửa"></asp:Label>
        </div>
        <div class="box">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr valign="top">
                    <td style="width: 100px;">
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td colspan="6">
                        <asp:ValidationSummary ID="valError" runat="server" EnableClientScript="False" />
                        <asp:CustomValidator ID="SaveValidate" runat="server" Display="None" CssClass="FormStyle"
                            ErrorMessage="" /></td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Loại tin" CssClass="Caption"></asp:Label><asp:HiddenField
                            ID="hdNewsID" runat="server" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td colspan="6">
                        <asp:HiddenField ID="hdNewsTypeID" runat="server" />
                        <asp:Label ID="lbTotal" runat="server"></asp:Label><asp:DropDownList ID="dropNewsType"
                            runat="server" DataSourceID="ObjectDataSource1" DataTextField="NewsTypeName"
                            AutoPostBack="false" DataValueField="NewsTypeID" OnDataBound="DropNewsTypeDataBound">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OnObjectCreating="ObjectDataSource1ObjectCreating"
                            SelectMethod="GetAllUpNewsTypeForGridView" TypeName="HocLapTrinhWeb.BLL.vnn_UpNewsTypeBLL">
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td colspan="6">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dropNewsType"
                            Display="Dynamic" ErrorMessage="Bạn chưa chọn loại tin" InitialValue="-1"></asp:RequiredFieldValidator></td>
                </tr>
                <tr class="trEmpty">
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Tiêu đề" CssClass="Caption"></asp:Label></td>
                    <td>
                    </td>
                    <td class="Require">
                    </td>
                    <td colspan="6">
                        <asp:TextBox ID="txtTitle" runat="server" Width="100%" MaxLength="500"></asp:TextBox></td>
                </tr>
                <tr class="trEmpty">
                </tr>
                <tr valign="top">
                    <td style="width: 100px">
                        <asp:Label ID="Label5" runat="server" Text="Nội dung" CssClass="Caption"></asp:Label></td>
                    <td>
                    </td>
                    <td class="Require">
                    </td>
                    <td colspan="6">
                        <textarea runat="server" id="txtContent" name="content" cols="80" rows="15" class="bbeditor"
                            style="width: 99%"></textarea></td>
                </tr>
                <tr class="trEmpty">
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label19" runat="server" Text="Tag" CssClass="Caption"></asp:Label></td>
                    <td>
                    </td>
                    <td class="Require">
                    </td>
                    <td colspan="6">
                        <asp:TextBox ID="txtTag" runat="server" Width="100%"></asp:TextBox></td>
                </tr>
                <tr class="trEmpty">
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="Caption" Text="Ngày"></asp:Label></td>
                    <td>
                    </td>
                    <td class="Require">
                    </td>
                    <td colspan="6">
                        <asp:TextBox ID="txtCreatedDate" runat="server" Width="100%"></asp:TextBox></td>
                </tr>
                <tr class="trEmpty">
                </tr>
            </table>
        </div>
        <div>
            <asp:Button ID="btnSave" runat="server" CssClass="button" OnClick="BtnSaveClick"
                Text="Lưu" />
            <asp:Button ID="btnSaveAndNew" runat="server" CssClass="button" OnClick="BtnSaveAndNewClick"
                Text="Lưu & Tạo mới" />
            <asp:Button ID="btnCancelAdd" runat="server" CausesValidation="False" CssClass="button"
                OnClick="BtnCancelAddClick" Text="Ðóng" />
        </div>
    </div>
</div>
