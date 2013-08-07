<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAlexaDetail.ascx.cs"
    Inherits="Admin_usercontrols_ucAlexaDetail" %>
<%@ Register Assembly="INNO.WebControls" Namespace="INNO.WebControls" TagPrefix="inno" %>
<input type="hidden" id="notselect" value="<%=msg.GetMessage("ERR-000007")%>" />
<input type="hidden" id="deleteconfirm" value="<%=msg.GetMessage("ERR-000008")%>" />
<input type="hidden" id="hdLanguage" value="<%= CurrentPage.Language%>" />
<input type="hidden" id="DateError" value="Ngày không hợp lệ" />
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="Chi tiết thống kê"></asp:Label></h1>
    </div>
    <div class="container">
        <script type="text/javascript">
            $(function () {
                $('#container').highcharts({
                    title: {
                        text: 'Monthly Average Temperature',
                        x: -20
                    },
                    subtitle: {
                        text: 'Source: WorldClimate.com',
                        x: -20
                    },
                    xAxis: {
                        categories: <%= Test() %>
                        },
                    yAxis: {
                        title: {
                            text: 'Temperature (°C)'
                        },
                        plotLines: [{
                            value: 0,
                            width: 1,
                            color: '#808080'
                        }]
                    },
                    tooltip: {
                        valueSuffix: ''
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                    },
                    series: <%= Data() %>
                });
            });


        </script>
        <script src="<%= CurrentPage.UrlRoot %>/Code/Highcharts-3.0.4/js/highcharts.js"></script>
        <script src="<%= CurrentPage.UrlRoot %>/Code/Highcharts-3.0.4/js/modules/exporting.js"></script>

        <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
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
                                    <asp:Button ID="btnDelete" CssClass="button" runat="server" UseSubmitBehavior="False"
                                        Text="Xóa" OnClientClick="if(! IsDelete('chckSelect','notselect','deleteconfirm')) return;"
                                        CausesValidation="False" OnClick="BtnDeleteClick" meta:resourcekey="btnDeleteResource1" />
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
                        AllowPaging="True" DataKeyNames="AlexaDetailID" DataSourceID="ObjData" ID="gvData"
                        AllowSorting="True" runat="server" OnDataBound="GvDataDataBound" OnPageIndexChanging="GvDataPageIndexChanging">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle Width="5%" />
                                <ItemStyle CssClass="cbxCheck" HorizontalAlign="Center"></ItemStyle>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chckAll" onclick="ChangeAllCheckBoxStates('chckSelect',this.checked);ShowHideBtnEdit();"
                                        runat="server" ToolTip="Chọn/Bỏ chọn tất cả" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chckSelect" runat="server" onclick="ChangeHeaderAsNeeded('chckSelect','chckAll',this);ShowHideBtnEdit();" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Link Url" SortExpression="LinkUrl">
                                <HeaderStyle Width="30%"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdAlexaDetailID" Value='<%# Eval("AlexaDetailID") %>' runat="server" />
                                    <asp:HyperLink ID="hpLinkUrl" runat="server" Text='<%# Eval("LinkUrl") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TrafficRank" SortExpression="TrafficRank">
                                <ItemTemplate>
                                    <asp:Label ID="lbTrafficRank" runat="server" Text='<%# String.Format("{0:0,0.#}", double.Parse(Eval("TrafficRank").ToString())) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TrafficRankVN" SortExpression="TrafficRankVN">
                                <ItemTemplate>
                                    <asp:Label ID="lbTrafficRankVN" runat="server" Text='<%# String.Format("{0:0,0.#}", double.Parse(Eval("TrafficRankVN").ToString())) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SiteLink" SortExpression="SiteLink">
                                <ItemTemplate>
                                    <asp:Label ID="lbSiteLink" runat="server" Text='<%# String.Format("{0:0,0.#}", double.Parse(Eval("SiteLink").ToString())) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ngày cập nhật" SortExpression="UpdatedDate">
                                <ItemTemplate>
                                    <asp:Label ID="lbUpdatedDate" runat="server" Text='<%# Eval("UpdatedDate") %>'></asp:Label>
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
                    <asp:ObjectDataSource ID="ObjData" runat="server" TypeName="HocLapTrinhWeb.BLL.vnn_AlexaDetailBLL"
                        EnablePaging="True" SelectMethod="GetAllAlexaDetailForGridView" SelectCountMethod="GetAllAlexaDetailRowCount"
                        OnObjectCreating="ObjDataObjectCreating" OnSelected="ObjDataSelected" OnSelecting="ObjDataSelecting"></asp:ObjectDataSource>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
            </Triggers>
        </asp:UpdatePanel>
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
         
    }
            
    function ShowHideBtnEdit()
    {   
           
    }


</script>

