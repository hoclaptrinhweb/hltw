<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUpNews.ascx.cs" Inherits="administrator_usercontrols_ucUpNews" %>
<link href="css/jquery.contextMenu.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="js/autocomplete/demos.css">
<link rel="stylesheet" href="js/autocomplete/themes/base/jquery.ui.all.css">
<input id="notselect" type="hidden" value='<%=msg.GetMessage("ERR-000007")%>' />
<input id="deleteconfirm" type="hidden" value='<%=msg.GetMessage("ERR-000008")%>' />
<input id="updateconfirm" type="hidden" value='<%=msg.GetMessage("ERR-000012")%>' />
<input id="hdDateFormat" type="hidden" value="<%= CurrentPage.Language%>" />
<input id="DateError" type="hidden" value="Ngày không hợp lệ" />
<style>
	.ui-button { margin-left: -1px; }
	.ui-button-icon-only .ui-button-text { padding: 0.35em; } 
	.ui-autocomplete-input { margin: 0; padding: 0.48em 0 0.47em 0.45em;width:400px; }
</style>

<script type="text/javascript" language="javascript" src="js/EventCheckBoxInGridView.js"></script>

<script type="text/javascript" language="javascript" src="js/autocomplete/jquery-1.7.1.js"></script>

<script type="text/javascript" language="javascript" src="js/autocomplete/jquery.ui.core.js"></script>

<script type="text/javascript" language="javascript" src="js/autocomplete/jquery.ui.widget.js"></script>

<script type="text/javascript" language="javascript" src="js/autocomplete/jquery.ui.button.js"></script>

<script type="text/javascript" language="javascript" src="js/autocomplete/jquery.ui.position.js"></script>

<script type="text/javascript" language="javascript" src="js/autocomplete/jquery.ui.autocomplete.js"></script>

<script type="text/javascript" language="javascript" src="js/jquery.contextMenu.js"></script>

<script type="text/javascript" language="javascript">
	(function( $ ) {
		$.widget( "ui.combobox", {
			_create: function() {
				var self = this,
					select = this.element.hide(),
					selected = select.children( ":selected" ),
					value = selected.val() ? selected.text() : "";
				var input = this.input = $( "<input>" )
					.insertAfter( select )
					.val( value )
					.attr('id','txt' +  this.element[0].id)
					.autocomplete({
						delay: 0,
						minLength: 0,
						source: function( request, response ) {
							var matcher = new RegExp( $.ui.autocomplete.escapeRegex(request.term), "i" );
							response( select.children( "option" ).map(function() {
								var text = $( this ).text();
								if ( this.value && ( !request.term || matcher.test(text) ) )
									return {
										label: text.replace(
											new RegExp(
												"(?![^&;]+;)(?!<[^<>]*)(" +
												$.ui.autocomplete.escapeRegex(request.term) +
												")(?![^<>]*>)(?![^&;]+;)", "gi"
											), "<strong>$1</strong>" ),
										value: text,
										option: this
									};
							}) );
						},
						select: function( event, ui ) {
						    
							ui.item.option.selected = true;
							self._trigger( "selected", event, {
								item: ui.item.option
							});
						},
						change: function( event, ui ) {
							if ( !ui.item ) {
								var matcher = new RegExp( "^" + $.ui.autocomplete.escapeRegex( $(this).val() ) + "$", "i" ),
									valid = false;
								select.children( "option" ).each(function() {
									if ( $( this ).text().match( matcher ) ) {
										this.selected = valid = true;
										return false;
									}
								});
								if ( !valid ) {
									// remove invalid value, as it didn't match anything
									$( this ).val( "" );
									select.val( "" );
									input.data( "autocomplete" ).term = "";
									return false;
								}
							}
						}
					})
					.addClass( "ui-widget ui-widget-content ui-corner-left " + this.element[0].id);

				input.data( "autocomplete" )._renderItem = function( ul, item ) {
					return $( "<li></li>" )
						.data( "item.autocomplete", item )
						.append( "<a>" + item.label + "</a>" )
						.appendTo( ul );
				};

				this.button = $( "<button type='button'>&nbsp;</button>" )
					.attr( "tabIndex", -1 )
					.attr( "title", "Show All Items" )
					.insertAfter( input )
					.button({
						icons: {
							primary: "ui-icon-triangle-1-s"
						},
						text: false
					})
					.removeClass( "ui-corner-all" )
					.addClass( "ui-corner-right ui-button-icon" )
					.click(function() {
						// close if already visible
						if ( input.autocomplete( "widget" ).is( ":visible" ) ) {
							input.autocomplete( "close" );
							return;
						}

						// work around a bug (likely same cause as #5265)
						$( this ).blur();

						// pass empty string as value to search for, displaying all results
						input.autocomplete( "search", "" );
						input.focus();
					});
			},

			destroy: function() {
				this.input.remove();
				this.button.remove();
				this.element.show();
				$.Widget.prototype.destroy.call( this );
			}
		});
	})( jQuery );
</script>

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
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div style="text-align: left">
                                    <asp:Button ID="btnNew" runat="server" CausesValidation="False" CssClass="button"
                                        EnableViewState="False" OnClick="BtnNewClick" Text="Thêm mới" />
                                    <asp:Button ID="btnEdit" runat="server" CausesValidation="False" CssClass="button"
                                        OnClick="BtnEditClick" OnClientClick="if(!IsEdit('chckSelect','notselect')) return;"
                                        Text="Chỉnh sửa" UseSubmitBehavior="False" />
                                    <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button"
                                        OnClick="BtnDeleteClick" OnClientClick="if(! IsDelete('chckSelect','notselect','deleteconfirm')) return;"
                                        Text="Xóa" UseSubmitBehavior="false" />
                                </div>
                            </td>
                            <td align="right">
                                <asp:Label ID="lbTotal" runat="server"></asp:Label>&nbsp; &nbsp;<asp:DropDownList
                                    ID="dropNewsType" runat="server" DataSourceID="ObjectDataSource1" DataTextField="NewsTypeName"
                                    AutoPostBack="true" DataValueField="NewsTypeID" OnDataBound="DropNewsTypeDataBound">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OnObjectCreating="ObjectDataSource1ObjectCreating"
                                    SelectMethod="GetAllUpNewsTypeForGridView" TypeName="HocLapTrinhWeb.BLL.vnn_UpNewsTypeBLL">
                                </asp:ObjectDataSource>
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
                                <HeaderStyle Width="4%" />
                                <ItemStyle CssClass="cbxCheck" HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chckAll" runat="server" onclick="ChangeAllCheckBoxStates('chckSelect',this.checked);ShowHideBtnEdit();"
                                        ToolTip="Chọn/Bỏ chọn tất cả" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chckSelect" runat="server" onclick="ChangeHeaderAsNeeded('chckSelect','chckAll',this);ShowHideBtnEdit();" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tiêu đề" SortExpression="Title">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdNewsID" Value='<%# Eval("NewsID") %>' runat="server" />
                                    <asp:HiddenField ID="hdNewsTypeID" Value='<%# Eval("NewsTypeID") %>' runat="server" />
                                    <asp:HyperLink ID="hlTitle" runat="server" NavigateUrl='<%# "~/dhadmincp/NewsDetail.aspx?NewsID="+Eval("NewsID") %>'
                                        Text='<%# Eval("Title") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Loại tin" SortExpression="NewsTypeName">
                                <HeaderStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblNewsTypeName" runat="server" Text='<%# Eval("NewsTypeName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Ng&#224;y đăng" SortExpression="CreatedDate">
                                <HeaderStyle Width="6%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#DateTime.Parse(Eval("CreatedDate").ToString(), new System.Globalization.CultureInfo(CurrentPage.Language), System.Globalization.DateTimeStyles.None).ToString()  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                  <asp:TemplateField HeaderText="IP đăng tin" SortExpression="IPAddress">
                                <HeaderStyle Width="9%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblIPAddress" runat="server" Text='<%# Eval("IPAddress") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Người đăng" SortExpression="CreatedByUserName">
                                <HeaderStyle Width="4%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedByUserName" runat="server" Text='<%# Eval("CreatedByUserName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
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
                        <AlternatingRowStyle CssClass="AlternateRowStyle" />
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
                        TypeName="HocLapTrinhWeb.BLL.vnn_UpNewsBLL" OnSelected="ObjDataSelected" OnSelecting="ObjDataSelecting">
                        <SelectParameters>
                            <asp:ControlParameter Name="NewsTypeID" ControlID="dropNewsType" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
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
                    <asp:HiddenField runat="server" ID="HiddenField1" Value="0" />
                    <asp:HiddenField runat="server" ID="hdCategoryForumID" Value="0" />
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
                                            <asp:DropDownList class="editable-select" ID="dropCategory" runat="server" AutoPostBack="False"
                                                DataSourceID="ObjCategory" DataTextField="CategoryName" DataValueField="CategoryID"
                                                OnDataBound="DropCategoryDataBound">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdCategory" runat="server" />
                                            <asp:ObjectDataSource ID="ObjCategory" runat="server" OnObjectCreating="ObjCategoryObjectCreating"
                                                SelectMethod="GetAllCategoryForGridView" TypeName="HocLapTrinhWeb.BLL.vnn_UpCategoryBLL">
                                            </asp:ObjectDataSource>
                                        </td>
                                        <td>
                                            <input id="btnPost" class="button" onclick="PostNews()" type="button" value="Gửi bài" /></td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr class="trEmpty">
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
<!-- Click Right Menucontent -->
<ul id="myMenucontent" class="contextMenu">
    <li class="edit"><a href="#rename">Post Bài</a></li>
    <li class="quit separator"><a href="#cancel">Hủy</a></li>
</ul>
<!-- End Click Right Menucontent -->

<script type="text/javascript">
        var isMoveNews=false;
        var prm = Sys.WebForms.PageRequestManager.getInstance(); 
        prm.add_beginRequest(BeginRequestHandler);   
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args)//begin ajax
        { 
           
        }
        
        function EndRequestHandler(sender, args)//ajax return value
        {
            $( ".editable-select" ).combobox();
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
        
        function PostNews()
        {
            $('#<%= hdCategory.ClientID %>').val($('.<%= dropCategory.ClientID %>')[0].value);
           $.ajax({
           type: 'GET',
           cache: false,
           url: 'upAjaxPost.aspx',
           data: 'newsid=' + idnews + "&categoryid=" +  $('#<%= hdCategory.ClientID %>').val()
           ,success: function(data) {
                alert(data);
           }});
            
        }
var idnews ;      
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
            if (action == "rename") {
                idnews = $(el.children()[1]).children()[0].value;
                showPopupDiv('divAdd','Post bài viết', null, null, true,null,null,'absolute');//absolute
            }
    });
    $( ".editable-select" ).combobox();
});
</script>