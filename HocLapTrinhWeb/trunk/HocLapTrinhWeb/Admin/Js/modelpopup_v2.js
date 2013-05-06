 //JScript File
var curPopUp = null;
var curPopUpLevel2 = null;
var src_img_load = "css/default/images/loading_2.gif";
var src_img_remove = null;
var src_img_remove_hover = null;
var showClose=true;

function showPopupDiv(idPopUp,title,frm_width,frm_height,isInForm,funcWhenHide,pstrButtonFocus_ID,postion_popup){
	        if(idPopUp){
		        curPopUp = idPopUp;
	        }
	        if (typeof document.body.style.maxHeight === "undefined") {//if IE 6
			        $("body","html").css({height: "100%", width: "100%"});
			        $("html").css("overflow","hidden");
			        if (document.getElementById("TB_HideSelect") === null) {//iframe to hide select elements in ie6
				        $("body").append("<iframe id='TB_HideSelect'></iframe><div id='TB_overlay'></div><div id='TB_window'></div>");
				        //$("#TB_overlay").click(hidePopup);
			        }
	        }else if(isInForm==true || isInForm=='true'){//all others
			        if(document.getElementById("TB_overlay") === null){
				        $("form").append("<div id='TB_overlay'></div><div id='TB_window'></div>");
				        //$("#TB_overlay").click(hidePopup);
			        }
	        }
        	else
        	{
        	    if(document.getElementById("TB_overlay") === null){
				        $("body").append("<div id='TB_overlay'></div><div id='TB_window'></div>");
				        //$("#TB_overlay").click(hidePopup);
			        }
        	}	
	        if(detectMacXFF()){
		        $("#TB_overlay").addClass("TB_overlayMacFFBGHack");//use png overlay so hide flash
	        }else{
		        $("#TB_overlay").addClass("TB_overlayBG");//use background and opacity
	        }
	        if(document.getElementById('TB_load') == null)
		        $("body").append("<div id='TB_load'><table border='0' cellpadding='0' cellspacing='0'><tr><td><img src='"+src_img_load+"' /></td><td></td></tr></table></div>");//add loader to the page
	        else
		        $("body").append(document.getElementById('TB_load'));
	        $('#TB_load').show();//show loader (NFR)
	        if(idPopUp!='TB_load')
	        {
				
				var strText="<div id='Content'>"

                + "<div class='divTitle'>"
                + "<span class='title'>" + title + "</span>";
				if (showClose) {
				    strText += "<a alt='' class='close' onclick='javascript:hidePopup();" + (funcWhenHide ? funcWhenHide + ";" : "") + "' >X</a>";
				}
				strText += "</div>"

                + "<div class='divContent'><div id='divInnerContent'class='divInnerContent'></div></div>";
                
				
	            $("#TB_window").append(strText);
	            $("#divInnerContent").append($('#' + idPopUp).css({ display: "block" }));
		        
                if( postion_popup==null)
		        {
		            postion_popup="fixed";
		        }

	            $("#TB_window").css({display:"block",position:postion_popup});
	            setPosition($('#' + idPopUp ).width(),$('#' + idPopUp).height());
	            
	            $('#TB_load').hide();
	        }
	        if (document.getElementById(pstrButtonFocus_ID))
	            document.getElementById(pstrButtonFocus_ID).focus();
        }

function showPopupUrl(url,title,frm_width,frm_height){
	if (typeof document.body.style.maxHeight === "undefined") {//if IE 6
			$("body","html").css({height: "100%", width: "100%"});
			$("html").css("overflow","hidden");
			if (document.getElementById("TB_HideSelect") === null) {//iframe to hide select elements in ie6
				$("body").append("<iframe id='TB_HideSelect'></iframe><div id='TB_overlay'></div><div id='TB_window'></div>");
				$("#TB_overlay").click(hidePopup);
			}
	}else{//all others
			if(document.getElementById("TB_overlay") === null){
				$("body").append("<div id='TB_overlay'></div><div id='TB_window'></div>");
				$("#TB_overlay").click(hidePopup);
			}
	}
		
	if(detectMacXFF()){
		$("#TB_overlay").addClass("TB_overlayMacFFBGHack");//use png overlay so hide flash
	}else{
		$("#TB_overlay").addClass("TB_overlayBG");//use background and opacity
	}
	$("body").append("<div id='TB_load'><table border='0' cellpadding='0' cellspacing='0'><tr><td><img src='"+src_img_load+"' /></td><td>Loading...</td></tr></table></div>");//add loader to the page
	$('#TB_load').show();//show loader
	$("#TB_window").append("<div id='Content' style='width:"+frm_width+"px;height:"+frm_height+"px'>"+
	                "<div class='dvTop'><div class='dvTopLeft'></div><div class='dvTopRight'></div></div><div class='dvTitle'><div style='float:left'><span style='padding-left:5px'>"+title+"</span></div><div style='float:right;height:auto;padding-left:5px;padding-right:5px;padding-top:3px;padding-bottom:3px'><img alt='' border='0' src='"+src_img_remove+"' style='cursor: pointer' onclick='javascript:hidePopup();' /></div></div>"+
	                "<div id='dvContent'class='dvContent'></div>"+
	                "<div class='dvBottom'><div class='dvBottomLeft'></div><div class='dvBottomRight'></div></div></div>");
	$("#dvContent").load(url, function()
    {
                                $("#TB_load").remove();	
								$("#TB_window").css({display:"block"});
								setPosition(frm_width,frm_height);
    });
	
}

//function setPosition(w,h) {
//$("#TB_window").css({marginLeft: '-' + parseInt((w / 2),10) + 'px', width: w + 'px'});
//	if ( !(jQuery.browser.msie && jQuery.browser.version < 7)) { // take away IE6
//		$("#TB_window").css({marginTop: '-' + parseInt(250,10) + 'px'});
//	}
//}
function setPosition(w,h) {

    var winwidth = document.all ? document.body.clientWidth : window.innerWidth;
    var winheight = document.all ? document.documentElement.clientHeight : window.innerHeight;
    $("#TB_window").css({ left: (winwidth - $('#TB_window').width() - 10) / 2 + 'px',
        top: (winheight - $('#TB_window').height()) / 2 + 'px'
    });

    
}

function hidePopupOtherPage(){
	if(curPopUp !=null){	
		$('body').append($('#' + curPopUp ).hide());	
	}
	$("#TB_window").fadeOut("fast",function(){$('#TB_window,#TB_overlay,#TB_HideSelect').trigger("unload").unbind().remove();});
	$("#TB_load").hide();
	$('#dvLoad').remove();
	if (typeof document.body.style.maxHeight == "undefined") {//if IE 6
		$("body","html").css({height: "auto", width: "auto"});
		$("html").css("overflow","");
	}
	return false;
}
function hidePopup(){
	if(curPopUp !=null){	
		$('body').append($('#' + curPopUp ).hide());	
	}
	$("#TB_window").fadeOut("fast",function(){$('#TB_window,#TB_overlay,#TB_HideSelect').trigger("unload").unbind().remove();});
	$("#TB_load").hide();
	if (typeof document.body.style.maxHeight == "undefined") {//if IE 6
		$("body","html").css({height: "auto", width: "auto"});
		$("html").css("overflow","");
	}
	return false;
}
function hidePopupHaveID(id1, id2){
   if(curPopUpLevel2 !=null)
   {
    if(id1!="undefined" && id1!=null)
    {
		$('#' + id1).append($('#' + id2).hide());
	}
	else
	{
	    $('body').append($('#' + id2).hide());
	}
   }
	$("#TB_windowLevel2").fadeOut("fast",function(){$('#TB_windowLevel2,#TB_overlayLevel2,#TB_HideSelectLevel2').trigger("unload").unbind().remove();});
	$("#TB_loadLevel2").hide();
	if (typeof document.body.style.maxHeight == "undefined") {//if IE 6
		$("body","html").css({height: "auto", width: "auto"});
		$("html").css("overflow","");
	}
	return false;
}
function detectMacXFF() {
  var userAgent = navigator.userAgent.toLowerCase();
  if (userAgent.indexOf('mac') != -1 && userAgent.indexOf('firefox')!=-1) {
    return true;
  }
}

function showPopupDivLevel2(idPopUp,title,frm_width,frm_height,isInForm,id2){
	        if(idPopUp){
		        curPopUpLevel2 = idPopUp;
	        }
	        if (typeof document.body.style.maxHeight === "undefined") {//if IE 6
			        $("body","html").css({height: "100%", width: "100%"});
			        $("html").css("overflow","hidden");
			        if (document.getElementById("TB_HideSelectLevel2") === null) {//iframe to hide select elements in ie6
				        $("body").append("<iframe id='TB_HideSelectLevel2'></iframe><div id='TB_overlayLevel2'></div><div id='TB_windowLevel2'></div>");
				        //$("#TB_overlay").click(hidePopup);
			        }
	        }else if(isInForm==true || isInForm=='true'){//all others
			        if(document.getElementById("TB_overlayLevel2") === null){
				        $("form").append("<div id='TB_overlayLevel2'></div><div id='TB_windowLevel2'></div>");
				        //$("#TB_overlay").click(hidePopup);
			        }
	        }
        	else
        	{
        	    if(document.getElementById("TB_overlayLevel2") === null){
				        $("body").append("<div id='TB_overlayLevel2'></div><div id='TB_windowLevel2'></div>");
				        //$("#TB_overlay").click(hidePopup);
			        }
        	}	
	        if(detectMacXFF()){
		        $("#TB_overlayLevel2").addClass("TB_overlayMacFFBGHackLevel2");//use png overlay so hide flash
	        }else{
		        $("#TB_overlayLevel2").addClass("TB_overlayBGLevel2");//use background and opacity
	        }
	        if(document.getElementById('TB_loadLevel2') == null)
		        $("body").append("<div id='TB_loadLevel2'><table border='0' cellpadding='0' cellspacing='0'><tr><td><img src='"+src_img_load+"' /></td><td></td></tr></table></div>");//add loader to the page
	        else
		        $("body").append(document.getElementById('TB_loadLevel2'));
	        $('#TB_loadLevel2').show();//show loader
	        if(curPopUpLevel2!='TB_loadLevel2')
	        {
	            var strText="<div id='ContentLevel2' style='border:1px solid #CFCFCF;width:"+frm_width+"px;height:"+frm_height+"px;'>" +
	                "<div class='dvTitle'><span style='float:left;padding-left:17px;padding-top:3px; color:#3B5998; font-weight:bold;'>"+title+"</span>";
				if(showClose)
					//strText+="<img alt='' border='0' class='PopupClose' onclick='javascript:hidePopup();' />";
					strText+="<a alt='' style='float: right; cursor: pointer; padding-top: 2px; padding-right: 5px;' onclick=\"javascript:hidePopupHaveID('"+id2+"','"+idPopUp+"');\" ><img id='ImgClose' src='" + src_img_remove + "' border='0' onmouseover='OnMouseOverImageClose(this);' onmouseout='OnMouseOutImageClose(this);' /></a>";
				
				strText+="</div>"+
	                "<div id='dvContentLevel2'class='dvContent'></div></div>";
	            $("#TB_windowLevel2").append(strText);
		        $("#dvContentLevel2").append($('#'+idPopUp).css({display:"block"}));
	            $("#TB_windowLevel2").css({display:"block"});
	            setPositionLevel2($('#' + idPopUp ).width(),$('#' + idPopUp).height());
	            
	            $('#TB_loadLevel2').hide();
	        }
        }
function hidePopupLevel2(){
	if(curPopUpLevel2 !=null){	
		$('body').append($('#' + curPopUpLevel2 ).hide());	
	}
	$("#TB_windowLevel2").fadeOut("fast",function(){$('#TB_windowLevel2,#TB_overlayLevel2,#TB_HideSelectLevel2').trigger("unload").unbind().remove();});
	$("#TB_loadLevel2").hide();
	if (typeof document.body.style.maxHeight == "undefined") {//if IE 6
		$("body","html").css({height: "auto", width: "auto"});
		$("html").css("overflow","");
	}
	return false;
}        
function setPositionLevel2(w,h) {
$("#TB_windowLevel2").css({marginLeft: '-' + parseInt((w / 2),10) + 'px', width: w + 'px'});
	if ( !(jQuery.browser.msie && jQuery.browser.version < 7)) { // take away IE6
		$("#TB_windowLevel2").css({marginTop: '-' + parseInt((h / 2),10) + 'px'});
	}
}
function OnMouseOverImageClose(objImg)
{
    objImg.src = src_img_remove_hover;
}

function OnMouseOutImageClose(objImg)
{
    objImg.src = src_img_remove;
}