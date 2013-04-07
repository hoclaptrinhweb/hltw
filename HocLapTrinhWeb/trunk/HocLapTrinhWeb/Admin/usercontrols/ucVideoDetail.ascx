<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucVideoDetail.ascx.cs"
    Inherits="Admin_usercontrols_ucVideoDetail" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="INNO.WebControls" Namespace="INNO.WebControls" TagPrefix="inno" %>
<link rel="stylesheet" type="text/css" href="http://xoxco.com/projects/code/tagsinput/jquery.tagsinput.css" />
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js"></script>
<script type="text/javascript" src="http://xoxco.com/projects/code/tagsinput/jquery.tagsinput.js"></script>
<script type='text/javascript' src='https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.13/jquery-ui.min.js'></script>
<link href="../admin/css/jquery-ui.css" rel="stylesheet" type="text/css" />
<input id="notselect" type="hidden" value='<%=msg.GetMessage("ERR-000007")%>' />
<input id="deleteconfirm" type="hidden" value='<%=msg.GetMessage("ERR-000008")%>' />
<input id="hdDateFormat" type="hidden" value="<%= CurrentPage.Language%>" />
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="Tin Tức"></asp:Label>&nbsp;</h1>
    </div>
    <div class="container">
        <div class="box-headerdetail">
            <asp:Label ID="lblBoxHeader" runat="server" Text="Thêm / Chỉnh sửa"></asp:Label>
        </div>
        <div class="box">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr valign="top">
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td colspan="6">
                        <asp:ValidationSummary ID="valError" runat="server" EnableClientScript="False" />
                        <asp:CustomValidator ID="SaveValidate" runat="server" Display="None" CssClass="FormStyle"
                            ErrorMessage="" />
                    </td>
                </tr>
                <tr valign="top">
                    <td style="width: 100px">
                        <asp:HiddenField ID="hdVideoID" runat="server" />
                        <asp:Label ID="Label2" runat="server" Text="Loại tin" CssClass="Caption"></asp:Label>
                    </td>
                    <td class="tdEmpty">
                    </td>
                    <td class="Require">
                    </td>
                    <td>
                        <asp:HiddenField ID="hdVideoTypeID" runat="server" />
                        <asp:DropDownList ID="dropVideoType" runat="server" DataSourceID="ObjectDataSource1"
                            DataTextField="TreeView" DataValueField="VideoTypeID" OnDataBound="DropVideoTypeDataBound">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OnObjectCreating="ObjectDataSource1ObjectCreating"
                            SelectMethod="GetAllVideoTypeForGridView" TypeName="HocLapTrinhWeb.BLL.vnn_VideoTypeBLL">
                        </asp:ObjectDataSource>
                    </td>
                    <td class="tdBetween">
                    </td>
                    <td style="width: 110px">
                        <asp:Label ID="Label8" runat="server" CssClass="Caption" Text="Tin chuyển từ"></asp:Label>
                    </td>
                    <td class="tdEmpty">
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="txtMoveFrom" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dropVideoType"
                            Display="Dynamic" ErrorMessage="Bạn chưa chọn loại tin" InitialValue="-1"></asp:RequiredFieldValidator>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td colspan="3">
                    </td>
                    <td>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Tiêu đề" CssClass="Caption"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td class="Require">
                    </td>
                    <td colspan="6">
                        <asp:TextBox ID="txtTitle" runat="server" Width="100%" MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                    <td colspan="6">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="dynamic"
                            ControlToValidate="txtTitle" ValidationExpression="^([\S\s]{0,500})$" ErrorMessage="<p>Tối đa 500 ký tự</p>"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                            ControlToValidate="txtTitle" ErrorMessage="Hãy nhập dữ liệu"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td colspan="3">
                    </td>
                    <td>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                 <tr valign="top">
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Link Video" CssClass="Caption"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td class="Require">
                    </td>
                    <td colspan="6">
                        <asp:TextBox ID="txtLinkVideo" runat="server" Width="100%" MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                    <td colspan="6">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                            ControlToValidate="txtLinkVideo" ErrorMessage="Hãy nhập dữ liệu"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td colspan="3">
                    </td>
                    <td>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Tóm tắt" CssClass="Caption"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td class="Require">
                    </td>
                    <td colspan="6">
                        <asp:TextBox ID="txtBrief" runat="server" Rows="5" Width="100%" MaxLength="2000"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr class="trEmpty">
                    <td colspan="3">
                    </td>
                    <td>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Từ khóa" CssClass="Caption"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td colspan="6">
                        <asp:TextBox ID="txtkeyword" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td colspan="3">
                    </td>
                    <td>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Nội dung" CssClass="Caption"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td colspan="6">
                        <CKEditor:CKEditorControl ID="FCKContent" runat="server" Height="400" BasePath="~/ckeditor">
                        </CKEditor:CKEditorControl>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td colspan="3">
                    </td>
                    <td>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label19" runat="server" Text="Nguồn" CssClass="Caption"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td colspan="6">
                        <asp:TextBox ID="txtNguon" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td colspan="3">
                    </td>
                    <td>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Ảnh" CssClass="Caption"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td colspan="6">
                        <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fileuploadThumbnail" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Image ID="imgThumbnail" CssClass="MaxImage" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                    <td colspan="6">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="fileuploadThumbnail"
                            ErrorMessage="Sai định dạng. Chọn định dạng jpg, gif, png, bmp" Display="Dynamic"
                            ValidationExpression="^.*\.((j|J)(p|P)(e|E)?(g|G)|(g|G)(i|I)(f|F)|(p|P)(n|N)(g|G)|(b|B)(m|M)(p|P))$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td colspan="3">
                    </td>
                    <td>
                    </td>
                    <td colspan="5">
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
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="Caption" Text="Trạng thái"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td colspan="6">
                        <asp:CheckBox ID="cboxHot" runat="server" class="checkbox" Text="Hot" />
                        <asp:CheckBox ID="cboxDelete" runat="server" class="checkbox" Text="Xóa" />
                        <asp:CheckBox ID="cboxActive" runat="server" class="checkbox" Text="Kích hoạt" />
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td colspan="3">
                    </td>
                    <td>
                    </td>
                    <td colspan="5">
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
                        <a onclick="ShowAdv(this)">Hiện thêm thông tin</a>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td colspan="3">
                    </td>
                    <td>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                <tr valign="top" class="none">
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="Đô ưu tiên" CssClass="Caption"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <inno:NumericTextBox ID="txtDouutien" runat="server" Precision="0" MaxLength="5"
                            Width="220px" Alignment="Left"></inno:NumericTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Số lần xem" CssClass="Caption"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSolanxem" runat="server" ReadOnly="True" Width="220px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td colspan="3">
                    </td>
                    <td>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                <tr valign="top" class="none">
                    <td>
                        <asp:Label ID="Label21" runat="server" Text="IP tạo tin" CssClass="Caption"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIPCreate" runat="server" ReadOnly="True" Width="220px"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="Label18" runat="server" Text="IP cập nhật" CssClass="Caption"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIPUpdate" runat="server" ReadOnly="True" Width="221px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td colspan="3">
                    </td>
                    <td>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                <tr valign="top" class="none">
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="Ngày đăng tin" CssClass="Caption"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNgaytao" runat="server" ReadOnly="false" Width="220px"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="Label16" runat="server" Text="Ngày cập nhật" CssClass="Caption"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNgaycapnhat" runat="server" ReadOnly="false" Width="220px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td colspan="3">
                    </td>
                    <td>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                <tr valign="top" class="none">
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="Người đăng tin" CssClass="Caption"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserCreate" runat="server" ReadOnly="True" Width="220px"></asp:TextBox>
                        <asp:HiddenField ID="hdCreatedBy" runat="server" Value="" />
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="Label17" runat="server" Text="Người cập nhật" CssClass="Caption"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserUpdate" runat="server" ReadOnly="True" Width="220px"></asp:TextBox>
                        <asp:HiddenField ID="hdUpdateBy" runat="server" Value="" />
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td colspan="3">
                    </td>
                    <td>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td colspan="3">
                    </td>
                    <td>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                <tr valign="top">
                    <td align="right" colspan="9">
                        <asp:Button ID="btnSave" runat="server" CssClass="button" OnClick="BtnSaveClick"
                            Text="Lưu" />
                        <asp:Button ID="btnSaveAndNew" runat="server" CssClass="button" OnClick="BtnSaveAndNewClick"
                            Text="Lưu & Tạo mới" />
                        <asp:Button ID="btnCancelAdd" runat="server" CausesValidation="False" CssClass="button"
                            OnClick="BtnCancelAddClick" Text="Ðóng" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
<script type="text/javascript">
    var test = '<%= FCKContent.ClientID %>';
    function ShowAdv(t) {
        if ($(t).text() == "Hiện thêm thông tin") {
            $(t).text('Ẩn thông tin');
            $(".none").show();
        } else {
            $(t).text('Hiện thêm thông tin');
            $(".none").hide();
        }
    }
    $(function () {
        $('#<%= txtkeyword.ClientID %>').tagsInput({
            width: 'auto',
            autocomplete_url: '<%= CurrentPage.UrlRoot %>/Handler/AutoCompleteTag.ashx'
        });
    });


    function onAddTag() {
        alert('add');
    }
    function onRemoveTag() {
        alert('remove');
    }
    function onChange() {
        alert('change');
    }
</script>
<style type="text/css">
    .MaxImage
    {
        max-width: 500px;
    }
</style>
