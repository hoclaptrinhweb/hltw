<%@ WebHandler Language="C#" Class="javascript" %>

using System;
using System.Web;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.BLL;

public class javascript : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        var con = new Connection();        
        if (!con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey)) return;
        VatGia(con);
    }

    private void VatGia(Connection con)
    {
        var cookie = HttpContext.Current.Request.Cookies["hltw"];
        if (cookie != null)
        {
            var autoAdv = new AutoAdvBLL(con);
            var rowAdv = autoAdv.GetAutoAdvByDate(DateTime.Now.ToString("MM/dd/yyyy"), 2);
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
                    rowUpdate.TypeID =2;
                    rowUpdate.UpdatedDate = DateTime.Now;
                    dt.Addtbl_AutoAdvRow(rowUpdate);
                    if (autoAdv.Update(dt, "TotalClickTop", "CurrentClickTop", "UpdatedDate"))
                    {
                        cookie.Value = "2";
                        cookie.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                    break;
                case "1":
                    dt = new dsHocLapTrinhWeb.tbl_AutoAdvDataTable();
                    rowUpdate = dt.Newtbl_AutoAdvRow();
                    rowUpdate.AutoAdvID = rowAdv.AutoAdvID;
                    rowUpdate.TotalClick = rowAdv.TotalClick + 1;
                    rowUpdate.CurrentClick = rowAdv.CurrentClick > 1 ? rowAdv.CurrentClick - 1 : 0;
                    rowUpdate.TypeID = 2;
                    rowUpdate.UpdatedDate = DateTime.Now;
                    dt.Addtbl_AutoAdvRow(rowUpdate);
                    if (autoAdv.Update(dt, "TotalClick", "CurrentClick", "UpdatedDate"))
                    {
                        cookie.Value = "2";
                        cookie.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                    break;
            }
        }
        else
        {
            var autoadv = new AutoAdvBLL(con);
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
                HttpContext.Current.Response.Cookies.Add(aCookie1);
                HttpContext.Current.Response.Write(
                                 @"
var nhtml = '0';
function setCookie(cName, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var cValue = escape(value) + ((exdays == null) ? '' : '; expires=' + exdate.toUTCString())+';path=/';
    document.cookie = cName + '=' + cValue;
};
function getCookie(cName) {
    var i, x, y, arrcookies = document.cookie.split(';');
    for (i = 0; i < arrcookies.length; i++) {
        x = arrcookies[i].substr(0, arrcookies[i].indexOf('='));
        y = arrcookies[i].substr(arrcookies[i].indexOf('=') + 1);
        x = x.replace(/^\s+|\s+$/g, '');
        if (x == cName) {
            return unescape(y);
        }
    }
    return undefined;
};
                                    function PageClick() {
                                    if (nhtml == '1') {
                                        nhtml = '0';
                                        if(getCookie('hltw') == '1' || getCookie('hltw') == '2')
	                                        return ;
                                        setCookie('hltw','1top',1);
                                        var n = Math.floor((Math.random() * 4) + 1);
                                        if($('#vgads-list-ads-bottom li a').length > 0)
                                        $('#vgads-list-ads-bottom li a')[n].click();
                                    }
                                    return false;
                                };" +
                                 "window.document.body.onclick=function(){PageClick();};nhtml ='1';" );
            }
            if (row.IsAcitve != 1 || row.CurrentClick <= 0) return;
            aCookie1 = new HttpCookie("hltw") { Value = "0", Expires = DateTime.Now.AddDays(1) };
            //Khi click được quảng cáo thì gán 1
            HttpContext.Current.Response.Cookies.Add(aCookie1);
            HttpContext.Current.Response.Write(
                             @"
var nhtml = '0';
function setCookie(cName, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var cValue = escape(value) + ((exdays == null) ? '' : '; expires=' + exdate.toUTCString())+';path=/';
    document.cookie = cName + '=' + cValue;
};
function getCookie(cName) {
    var i, x, y, arrcookies = document.cookie.split(';');
    for (i = 0; i < arrcookies.length; i++) {
        x = arrcookies[i].substr(0, arrcookies[i].indexOf('='));
        y = arrcookies[i].substr(arrcookies[i].indexOf('=') + 1);
        x = x.replace(/^\s+|\s+$/g, '');
        if (x == cName) {
            return unescape(y);
        }
    }
    return undefined;
};
                                function PageClick() {
                                if (nhtml == '2') {
                                    nhtml = '0';
                                    var n = Math.floor((Math.random() * 11) + 1);
                                    if(getCookie('hltw') == '1' || getCookie('hltw') == '2')
	                                    return ;
                                    setCookie('hltw','1',1);
                                    var link = $($('.vgads_advtitle a')[n]).attr('onmouseover').replace('adVgSl(this,\'','').replace('\')','');
                                    $($('.vgads_advtitle a')[n]).attr('href',link);
                                     $('.vgads_advtitle a')[n].click();                        
                                }
                                return false;
                            };" +
                             "window.document.body.onclick=function(){PageClick();};nhtml ='2';"
                                             );
        }
    }

    private void Ad360(Connection con)
    {
        var cookie1 = HttpContext.Current.Request.Cookies["hltw1"];
        if (cookie1 != null)
        {
            var autoAdv = new AutoAdvBLL(con);
            var rowAdv = autoAdv.GetAutoAdvByDate(DateTime.Now.ToString("MM/dd/yyyy"), 3);
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
                        rowUpdate.TypeID = 3;
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
                        rowUpdate.TypeID = 3;
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
            var autoadv = new AutoAdvBLL(con);
            var row = autoadv.GetAutoAdvByDate(DateTime.Now.ToString("MM/dd/yyyy"), 3);
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
                HttpContext.Current.Response.Cookies.Add(aCookie1);
                if (row.IsAcitveTop == 1 && row.CurrentClickTop > 0)
                {
                    HttpContext.Current.Response.Write("<script type='text/javascript'>" +

                                     @"
function PageClick() {
                                    if (nhtml == '1') {
                                        nhtml = '0';
                                        if(getCookie('hltw1') == '1' || getCookie('hltw1') == '2')
	                                        return ;
                                        setCookie('hltw1','1top',1);
                                        var n = Math.floor((Math.random() * $('.top_ad iframe').contents().find('.thumb a').length) + 1);
                                        $('.top_ad iframe').contents().find('.thumb a')[n].click();
                                        $.ajax({
                                            type: 'POST',
                                            url: 'http://www.hoclaptrinhweb.com/webservice/commentnews.asmx/PostData',
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
                                     "window.document.body.onclick=function(){PageClick();};nhtml ='1';</script>");
                }
            }
            if (row.IsAcitve == 1 && row.CurrentClick > 0)
            {
                aCookie1 = new HttpCookie("hltw1") { Value = "0", Expires = DateTime.Now.AddDays(1) };
                HttpContext.Current.Response.Cookies.Add(aCookie1);
                HttpContext.Current.Response.Write("<script type='text/javascript'>" +
                                 @"function PageClick() {
                                if (nhtml == '2') {
                                    nhtml = '0';
                                    var n = Math.floor((Math.random() * 11) + 1);
                                    if(getCookie('hltw1') == '1' || getCookie('hltw1') == '2')
	                                    return ;
                                    setCookie('hltw1','1',1);
                                    var n = Math.floor((Math.random() * $('#right_ad360 iframe').contents().find('.thumb a').length) + 1);
                                    $('#right_ad360 iframe').contents().find('.thumb a')[n].click();
                                    $.ajax({
                                        type: 'POST',
                                        url: 'http://www.hoclaptrinhweb.com/webservice/commentnews.asmx/PostData',
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
                                 "window.document.body.onclick=function(){PageClick();};nhtml ='2';</script>");
            }

        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}