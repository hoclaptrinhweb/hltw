using System;
using System.Web;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucCopyRight : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        CheckAdv();
    }

    private void CheckAdv()
    {
        var rnd = new Random();
        var k = rnd.Next(0, 2);
        if (k == 1)
            Ad360();
        else
            VatGia();
    }

    private void VatGia()
    {
        var cookie = Request.Cookies["hltw"];
        if (cookie != null)
        {
            var autoAdv = new AutoAdvBLL(CurrentPage.getCurrentConnection());
            var rowAdv = autoAdv.GetAutoAdvByDate(DateTime.Now.ToString("MM/dd/yyyy"), 1);
            if (rowAdv == null)
                return;
            dsHocLapTrinhWeb.tbl_AutoAdvDataTable dt;
            dsHocLapTrinhWeb.tbl_AutoAdvRow rowUpdate;
            switch (cookie.Value)
            {
                case "1top":
                    dt = new dsHocLapTrinhWeb.tbl_AutoAdvDataTable();
                    rowUpdate = dt.Newtbl_AutoAdvRow();
                    rowUpdate.AutoAdvID = rowAdv.AutoAdvID;
                    rowUpdate.TotalClickTop = rowAdv.TotalClickTop + 1;
                    rowUpdate.CurrentClickTop = rowAdv.CurrentClickTop > 1 ? rowAdv.CurrentClickTop - 1 : 0;
                    rowUpdate.TypeID = 1;
                    rowUpdate.UpdatedDate = DateTime.Now;
                    dt.Addtbl_AutoAdvRow(rowUpdate);
                    if (autoAdv.Update(dt, "TotalClickTop", "CurrentClickTop", "UpdatedDate"))
                    {
                        cookie.Value = "2";
                        cookie.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                    return;
                case "1":
                    dt = new dsHocLapTrinhWeb.tbl_AutoAdvDataTable();
                    rowUpdate = dt.Newtbl_AutoAdvRow();
                    rowUpdate.AutoAdvID = rowAdv.AutoAdvID;
                    rowUpdate.TotalClick = rowAdv.TotalClick + 1;
                    rowUpdate.CurrentClick = rowAdv.CurrentClick > 1 ? rowAdv.CurrentClick - 1 : 0;
                    rowUpdate.TypeID = 1;
                    rowUpdate.UpdatedDate = DateTime.Now;
                    dt.Addtbl_AutoAdvRow(rowUpdate);
                    if (autoAdv.Update(dt, "TotalClick", "CurrentClick", "UpdatedDate"))
                    {
                        cookie.Value = "2";
                        cookie.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                    return;
            }
        }
        if (cookie != null && cookie.Value == "2") return;
        var autoadv = new AutoAdvBLL(getCurrentConnection());
        var row = autoadv.GetAutoAdvByDate(DateTime.Now.ToString("MM/dd/yyyy"), 1);
        if (row == null)
            return;
        //Tính thời gian
        var nTime = DateTime.Now.Subtract(row.UpdatedDate).TotalSeconds;
        if (nTime <= row.TimeLimit)
            return;
        HttpCookie aCookie1;
        if (row.IsAcitveTop == 1 && row.CurrentClickTop > 0)
        {
            aCookie1 = new HttpCookie("hltw") { Value = "0", Expires = DateTime.Now.AddDays(1) };
            //Khi click được quảng cáo thì gán 1
            Response.Cookies.Add(aCookie1);
            lbAutoAdv.Text = "<script type='text/javascript'>" +
                             @"function PageClick() {
                                    if (nhtml == '1') {
                                        nhtml = '0';
                                        if(getCookie('hltw') == '1' || getCookie('hltw') == '2')
	                                        return ;
                                        var n = Math.floor((Math.random() * $('.vgads_content a').length) + 1);
                                        if(n < 2)
                                            return;
                                        setCookie('hltw','1top',1);

                                        $('#vgads-list-ads-bottom li a')[n].click(); 
                                        $.ajax({
                                            type: 'POST',
                                            url: '" + CurrentPage.UrlRoot +
                             @"/webservice/commentnews.asmx/PostData',
                                        data: '{\'type\':\'top\'}',
                                            contentType: 'application/json; charset=utf-8',
                                            dataType: 'json',
                                            success: function (msg) {
                                                setCookie('hltw','2',1);        
                                            },
                                            error: function () { }
                                        });
                                    }
                                    return false;
                                };" +
                             "window.document.body.onclick=function(){PageClick();};nhtml ='1';</script>";
        }
        if (row.IsAcitve != 1 || row.CurrentClick <= 0) return;
        aCookie1 = new HttpCookie("hltw") { Value = "0", Expires = DateTime.Now.AddDays(1) };
        //Khi click được quảng cáo thì gán 1
        Response.Cookies.Add(aCookie1);
        lbAutoAdv.Text = "<script type='text/javascript'>" +
                         @"function PageClick() {
                                if (nhtml == '2') {
                                    nhtml = '0';
                                    if(getCookie('hltw') == '1' || getCookie('hltw') == '2')
	                                    return ;
                                    var n = Math.floor((Math.random() * $('.vgads_content a').length) + 1);
                                    if(n < 2)
                                        return;
                                    var link = $($('.vgads_advtitle a')[n]).attr('onmouseover').replace('adVgSl(this,\'','').replace('\')','');
                                    $($('.vgads_advtitle a')[n]).attr('href',link);
                                     $('.vgads_advtitle a')[n].click(); 
                                    $.ajax({
                                        type: 'POST',
                                        url: '" + CurrentPage.UrlRoot +
                         @"/webservice/commentnews.asmx/PostData',
                                        data: '{\'type\':\'\'}',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        success: function (msg) {
                                            setCookie('hltw','2',1);
                                        },
                                        error: function () { }
                                    });                                    
                                }
                                return false;
                            };" +
                         "window.document.body.onclick=function(){PageClick();};nhtml ='2';</script>";
    }

    private void Ad360()
    {
        var cookie1 = Request.Cookies["hltw1"];
        if (cookie1 != null)
        {
            var autoAdv = new AutoAdvBLL(CurrentPage.getCurrentConnection());
            var rowAdv = autoAdv.GetAutoAdvByDate(DateTime.Now.ToString("MM/dd/yyyy"), 0);
            if (rowAdv != null)
            {
                dsHocLapTrinhWeb.tbl_AutoAdvDataTable dt;
                dsHocLapTrinhWeb.tbl_AutoAdvRow rowUpdate;
                switch (cookie1.Value)
                {
                    case "1top":
                        dt = new dsHocLapTrinhWeb.tbl_AutoAdvDataTable();
                        rowUpdate = dt.Newtbl_AutoAdvRow();
                        rowUpdate.AutoAdvID = rowAdv.AutoAdvID;
                        rowUpdate.TotalClickTop = rowAdv.TotalClickTop + 1;
                        rowUpdate.CurrentClickTop = rowAdv.CurrentClickTop > 1 ? rowAdv.CurrentClickTop - 1 : 0;
                        rowUpdate.UpdatedDate = DateTime.Now;
                        rowUpdate.TypeID = 0;
                        dt.Addtbl_AutoAdvRow(rowUpdate);
                        if (autoAdv.Update(dt, "TotalClickTop", "CurrentClickTop", "UpdatedDate"))
                        {
                            cookie1.Value = "2";
                            cookie1.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Current.Response.Cookies.Add(cookie1);
                        }
                        break;
                    case "1":
                        dt = new dsHocLapTrinhWeb.tbl_AutoAdvDataTable();
                        rowUpdate = dt.Newtbl_AutoAdvRow();
                        rowUpdate.AutoAdvID = rowAdv.AutoAdvID;
                        rowUpdate.TotalClick = rowAdv.TotalClick + 1;
                        rowUpdate.CurrentClick = rowAdv.CurrentClick > 1 ? rowAdv.CurrentClick - 1 : 0;
                        rowUpdate.UpdatedDate = DateTime.Now;
                        rowUpdate.TypeID = 0;
                        dt.Addtbl_AutoAdvRow(rowUpdate);
                        if (autoAdv.Update(dt, "TotalClick", "CurrentClick", "UpdatedDate"))
                        {
                            cookie1.Value = "2";
                            cookie1.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Current.Response.Cookies.Add(cookie1);
                        }
                        break;
                }
            }
        }

        if (cookie1 != null && cookie1.Value == "2") return;
        var autoadv = new AutoAdvBLL(getCurrentConnection());
        var row = autoadv.GetAutoAdvByDate(DateTime.Now.ToString("MM/dd/yyyy"), 0);
        if (row == null)
            return;
        var nTime = DateTime.Now.Subtract(row.UpdatedDate).TotalSeconds;
        if (nTime <= row.TimeLimit)
            return;
        HttpCookie aCookie1;
        if (row.IsAcitveTop == 1 && row.CurrentClickTop > 0)
        {
            aCookie1 = new HttpCookie("hltw1") { Value = "0", Expires = DateTime.Now.AddDays(1) };
            //Khi click được quảng cáo thì gán 1
            Response.Cookies.Add(aCookie1);
            if (row.IsAcitveTop == 1 && row.CurrentClickTop > 0)
            {
                lbAutoAdv.Text = "<script type='text/javascript'>" +

                                 @"
function PageClick() {
                                    if (nhtml == '1') {
                                        nhtml = '0';
                                        if(getCookie('hltw1') == '1' || getCookie('hltw1') == '2')
	                                        return ;
                                        var n = Math.floor((Math.random() * $('.top_ad iframe').contents().find('.thumb a').length) + 1);
                                        if(n < 2 )
                                            return;
                                        setCookie('hltw1','1top',1);
                                        $('.top_ad iframe').contents().find('.thumb a')[n].click();
                                        $.ajax({
                                            type: 'POST',
                                            url: '" + CurrentPage.UrlRoot +
                                 @"/webservice/commentnews.asmx/PostData',
                                        data: '{\'type\':\'top360\'}',
                                            contentType: 'application/json; charset=utf-8',
                                            dataType: 'json',
                                            success: function (msg) {
                                                setCookie('hltw1','2',1);        
                                            },
                                            error: function () { }
                                        });
                                    }
                                    return false;
                                };" +
                                 "window.document.body.onclick=function(){PageClick();};nhtml ='1';</script>";
            }
        }
        if (row.IsAcitve == 1 && row.CurrentClick > 0)
        {
            aCookie1 = new HttpCookie("hltw1") { Value = "0", Expires = DateTime.Now.AddDays(1) };
            //Khi click được quảng cáo thì gán 1
            Response.Cookies.Add(aCookie1);
            lbAutoAdv.Text = "<script type='text/javascript'>" +
                             @"function PageClick() {
                                if (nhtml == '2') {
                                    nhtml = '0';
                                    if(getCookie('hltw1') == '1' || getCookie('hltw1') == '2')
	                                    return ;
                                    var n = Math.floor((Math.random() * 11) + 1);
                                    if (n < 2 ) 
                                        return;
                                    setCookie('hltw1','1',1);
                                    var n = Math.floor((Math.random() * $('#right_ad360 iframe').contents().find('.thumb a').length) + 1);
                                    $('#right_ad360 iframe').contents().find('.thumb a')[n].click();
                                    $.ajax({
                                        type: 'POST',
                                        url: '" + CurrentPage.UrlRoot + @"/webservice/commentnews.asmx/PostData',
                                        data: '{\'type\':\'360\'}',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        success: function (msg) {
                                            setCookie('hltw1','2',1);
                                        },
                                        error: function () { }
                                    });                                    
                                }
                                return false;
                            };" +
                             "window.document.body.onclick=function(){PageClick();};nhtml ='2';</script>";
        }
    }

    private void Adnet()
    {
        var cookie1 = Request.Cookies["hltw2"];
        if (cookie1 != null)
        {
            var autoAdv = new AutoAdvBLL(CurrentPage.getCurrentConnection());
            var rowAdv = autoAdv.GetAutoAdvByDate(DateTime.Now.ToString("MM/dd/yyyy"), 2);
            if (rowAdv != null)
            {
                dsHocLapTrinhWeb.tbl_AutoAdvDataTable dt;
                dsHocLapTrinhWeb.tbl_AutoAdvRow rowUpdate;
                switch (cookie1.Value)
                {
                    case "1top":
                        dt = new dsHocLapTrinhWeb.tbl_AutoAdvDataTable();
                        rowUpdate = dt.Newtbl_AutoAdvRow();
                        rowUpdate.AutoAdvID = rowAdv.AutoAdvID;
                        rowUpdate.TotalClickTop = rowAdv.TotalClickTop + 1;
                        rowUpdate.CurrentClickTop = rowAdv.CurrentClickTop > 1 ? rowAdv.CurrentClickTop - 1 : 0;
                        rowUpdate.UpdatedDate = DateTime.Now;
                        rowUpdate.TypeID = 2;
                        dt.Addtbl_AutoAdvRow(rowUpdate);
                        if (autoAdv.Update(dt, "TotalClickTop", "CurrentClickTop", "UpdatedDate"))
                        {
                            cookie1.Value = "2";
                            cookie1.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Current.Response.Cookies.Add(cookie1);
                        }
                        break;
                    case "1":
                        dt = new dsHocLapTrinhWeb.tbl_AutoAdvDataTable();
                        rowUpdate = dt.Newtbl_AutoAdvRow();
                        rowUpdate.AutoAdvID = rowAdv.AutoAdvID;
                        rowUpdate.TotalClick = rowAdv.TotalClick + 1;
                        rowUpdate.CurrentClick = rowAdv.CurrentClick > 1 ? rowAdv.CurrentClick - 1 : 0;
                        rowUpdate.UpdatedDate = DateTime.Now;
                        rowUpdate.TypeID = 2;
                        dt.Addtbl_AutoAdvRow(rowUpdate);
                        if (autoAdv.Update(dt, "TotalClick", "CurrentClick", "UpdatedDate"))
                        {
                            cookie1.Value = "2";
                            cookie1.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Current.Response.Cookies.Add(cookie1);
                        }
                        break;
                }
            }
        }
        else
        {
            var autoadv = new AutoAdvBLL(getCurrentConnection());
            var row = autoadv.GetAutoAdvByDate(DateTime.Now.ToString("MM/dd/yyyy"), 2);
            if (row == null)
                return;
            var nTime = DateTime.Now.Subtract(row.UpdatedDate).TotalSeconds;
            if (nTime <= row.TimeLimit)
                return;
            if (row.IsAcitveTop == 1 && row.CurrentClickTop > 0)
            {
                var aCookie1 = new HttpCookie("hltw2") { Value = "0", Expires = DateTime.Now.AddDays(1) };
                //Khi click được quảng cáo thì gán 1
                Response.Cookies.Add(aCookie1);
                if (row.IsAcitveTop == 1 && row.CurrentClickTop > 0)
                {
                    lbAutoAdv.Text = "<script type='text/javascript'>" +

                                     @"
function PageClick() {
                                    if (nhtml == '1') {
                                        nhtml = '0';
                                        if(getCookie('hltw2') == '1' || getCookie('hltw2') == '2')
	                                        return ;
                                        setCookie('hltw2','1top',1);
                                        var n = Math.floor((Math.random() * $('#adnet_widget_20114 a').length) + 1);
                                        $('#adnet_widget_20114 a')[n].click();
                                        $.ajax({
                                            type: 'POST',
                                            url: '" + CurrentPage.UrlRoot +
                                     @"/webservice/commentnews.asmx/PostData',
                                        data: '{\'type\':\'topAdnet\'}',
                                            contentType: 'application/json; charset=utf-8',
                                            dataType: 'json',
                                            success: function (msg) {
                                                setCookie('hltw2','2',1);        
                                            },
                                            error: function () { }
                                        });
                                    }
                                    return false;
                                };" +
                                     "window.document.body.onclick=function(){PageClick();};nhtml ='1';</script>";
                }
            }
        }
    }

}