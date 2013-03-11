<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUserForum.ascx.cs" Inherits="Admin_usercontrols_ucUserForum" %>
<link rel="stylesheet" href="js/autocomplete/themes/base/jquery.ui.all.css">
<link rel="stylesheet" href="js/autocomplete/demos.css">
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
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div style="text-align: left">
                                    <asp:Button ID="btnNew" runat="server" CssClass="button" EnableViewState="False"
                                        Text="Thêm" CausesValidation="False" OnClientClick="showClose=true;isAdd=true;"
                                        OnClick="BtnNewClick" />
                                    <asp:Button ID="btnEdit" CssClass="button" Visible="false" runat="server" UseSubmitBehavior="False"
                                        Text="Sửa" CausesValidation="False" OnClientClick="if(!IsEdit('chckSelect','notselect')) return; showClose=true;isEdit=true;"
                                        OnClick="BtnEditClick" />
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
                        AllowPaging="True" DataKeyNames="UserForumID" DataSourceID="ObjData" ID="gvData"
                        AllowSorting="True" runat="server" OnDataBound="GvDataDataBound" OnPageIndexChanging="GvDataPageIndexChanging">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle Width="5%"></HeaderStyle>
                                <ItemStyle CssClass="cbxCheck" HorizontalAlign="Center"></ItemStyle>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chckAll" onclick="ChangeAllCheckBoxStates('chckSelect',this.checked);ShowHideBtnEdit();"
                                        runat="server" ToolTip="Select all/Unselect all" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chckSelect" onclick="ChangeHeaderAsNeeded('chckSelect','chckAll',this);ShowHideBtnEdit();"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ForumName" SortExpression="ForumName">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hpForumName" runat="server" Text='<%# Eval("ForumName") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UserName" SortExpression="UserName">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdUserForumID" Value='<%# Eval("UserForumID") %>' runat="server" />
                                    <asp:HyperLink ID="hpUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridTitle" />
                        <RowStyle CssClass="GridItem" />
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <PagerStyle CssClass="GridPager" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoItem" runat="server" Text="Updating data"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjData" runat="server" TypeName="HocLapTrinhWeb.BLL.vnn_UpUserForumBLL"
                        EnablePaging="True" SelectMethod="GetAllUserForumForGridView" SelectCountMethod="GetAllUserForumRowCount"
                        OnObjectCreating="ObjDataObjectCreating" OnSelected="ObjDataSelected" OnSelecting="ObjDataSelecting">
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
                    <asp:HiddenField runat="server" ID="hdEdit" Value="0" />
                    <asp:HiddenField runat="server" ID="hdUserForumID" Value="0" />
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
                                            <asp:Label ID="Label1" runat="server" CssClass="Caption" Text="Tên đăng nhập"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:DropDownList class="editable-select" ID="dropUser" runat="server" AutoPostBack="False"
                                                DataSourceID="ObjUser" DataTextField="UserName" DataValueField="UserID" OnDataBound="DropUserDataBound">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdUser" runat="server" />
                                            <asp:ObjectDataSource ID="ObjUser" runat="server" OnObjectCreating="ObjUserObjectCreating"
                                                SelectMethod="GetAllUserForGridView" TypeName="HocLapTrinhWeb.BLL.vnn_UpUserBLL">
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="lblUserForumName" CssClass="Caption" runat="server" Text="Tên diễn đàn"></asp:Label>
                                        </td>
                                        <td class="tdempty">
                                        </td>
                                        <td class="Require">
                                        </td>
                                        <td>
                                            <asp:TextBox CssClass="ui-autocomplete-input" runat="server" ID="txtForumName" autocomplete="off" />
                                            <ajaxToolkit:AutoCompleteExtender runat="server" ID="autoComplete1" TargetControlID="txtForumName"
                                                ServicePath="../../AutoComplete.asmx" ServiceMethod="GetForumNameList" MinimumPrefixLength="0"
                                                CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr class="trEmpty">
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="right">
                                            <asp:Button ID="btnSave" runat="server" Text="Lưu" ValidationGroup="vAdd" CssClass="button"
                                                OnClick="BtnSaveClick" OnClientClick="CheckValue();" />
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
        var isAdd=false, isEdit=false;
        var prm = Sys.WebForms.PageRequestManager.getInstance(); 
        prm.add_beginRequest(BeginRequestHandler);   
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args)//begin ajax
        { 
           
        }
        
        function CheckValue()
        {
             $('#<%= hdUser.ClientID %>').val($('.<%= dropUser.ClientID %>')[0].value);
        }
        
        function EndRequestHandler(sender, args)//ajax return value
        { 
            if(isAdd)
            {
                    showPopupDiv('divAdd','Thêm UserForum', null, null, true,null,null,'absolute');//absolute
                    isAdd=false;
            }
             if(isEdit)
            {
                    showPopupDiv('divAdd','Sửa thông tin UserForum', null, null, true,null,null,'absolute');
                    isEdit=false;
            }
            var hdIsSuccessful = document.getElementById('<%= hdIsAddSuccessful.ClientID%>');
            if(hdIsSuccessful!=null && hdIsSuccessful.value=="1")
            {
                hdIsSuccessful.value = "0";
                hidePopup();
            }
            $( ".editable-select" ).combobox();
            $('input').keypress(function(e) {
                if(e.which == 13) {
                    //Bỏ qua
                    return false
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
                if (nHeight >= 120)
                    $("#sidebar").css({"position":"fixed","top":"0px"});
                else
                    $("#sidebar").css({"position":"","top":"0px"});
                
                if (nHeight >= 180)
                    $(".box-header").addClass("topActive").css({"position":"fixed","top":"0px","width":eleWidth - 30}); // Padding = 15 => eleWidth - 30       
                else
                    $(".box-header").removeClass("topActive").css({"position":"","top":"0px","width":""});
            });
        });
</script>

