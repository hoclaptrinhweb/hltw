/*
    Author: Le Tan Kiet
    Created Date: 19/10/2010
    Description: Bao gom nhung ham script
*/

//Hàm thay đổi trạng thái tất cả check con khi check header thay đổi
function ChangeAllCheckBoxStates(CheckboxSelectName, checkState)
{
    if(checkState == true)
        $(".RowStyle").css({ backgroundColor: 'yellow' });
    else
        $(".RowStyle").css({ backgroundColor: '' });
    var elm=document.forms[0].elements;  
	for(i=0;i<elm.length;i++)
	    if(elm[i].type=="checkbox" && elm[i].id.substring(elm[i].id.length-CheckboxSelectName.length)==CheckboxSelectName)
		{
    		ChangeCheckBoxState(elm[i].id, checkState);//Thay doi trang thai cua tung checkbox
		}   
}
//Check and uncheck on GridView
function ChangeCheckBoxState(id, checkState)//Thay doi trang thai cua 1 check box
{
    var cb = document.getElementById(id);
    if (cb != null)
    {
       cb.checked = checkState;       
    }
}

//Thay doi trang thai cua Header Checkbox nếu tất cả checkbox con đc check
function ChangeHeaderAsNeeded(CheckboxSelectName,CheckboxAllName,t)
{
    if (t.is(':checked'))
        t.parents("tr").css({ backgroundColor: 'yellow' });
    else
        t.parents("tr").css({ backgroundColor: '' });
    
    var cbAll=null;
    var elm=document.forms[0].elements;            
    for(i=0;i<elm.length;i++)
	    if(elm[i].type=="checkbox" && elm[i].id.substring(elm[i].id.length-CheckboxAllName.length)==CheckboxAllName)
	    {
	        cbAll = elm[i]; break;
	    }
	
	for(i=0;i<elm.length;i++)
	{
	    if(elm[i].type=="checkbox" && elm[i].id.substring(elm[i].id.length-CheckboxSelectName.length)==CheckboxSelectName)
		{
    		if (!elm[i].checked)//Neu co mot checkbox trong Gridview khong check thi bo check header checkbox
            {
                ChangeCheckBoxState(cbAll.id, false);
                return;
            }
		}		
	}
	ChangeCheckBoxState(cbAll.id, true);        
} 

//Kiem tra checkbox trong gridview da duoc check chua, neu chua thi thong bao loi
function IsDelete(FullName,msg, msgConfirm)
{
    var elm=document.forms[0].elements;	
    for(i=0;i<elm.length;i++)
    {
        if(elm[i].type=="checkbox" && elm[i].id.substring(elm[i].id.length-FullName.length)==FullName)
        {
            if(elm[i].checked==true)
            { 
                if(msgConfirm!='')
                {
                    var errorConfirm = document.getElementById(msgConfirm);
                    if (!window.confirm(errorConfirm.value)) 
                        return false;	    	                    
                    return true;
                }
            return true;
            }	    	                
        }
    }
    var notselect = document.getElementById(msg);
    alert(notselect.value);
    return false;	    
}

function IsAddUser(FullName,msg)
{
    var elm=document.forms[0].elements;	
    for(i=0;i<elm.length;i++)
    {
        if(elm[i].type=="checkbox" && elm[i].id.substring(elm[i].id.length-FullName.length)==FullName)
        {
            if(elm[i].checked==true)
                return true;	                
        }
    }
    var notselect = document.getElementById(msg);
    alert(notselect.value);
    return false;	    
}



//Kiem tra checkbox trong gridview da duoc check chua, neu chua thi thong bao loi
function Confirm(msgConfirm)
{
    if(msgConfirm!='')
    {
        var errorConfirm = document.getElementById(msgConfirm);
        if (!window.confirm(errorConfirm.value)) 
            return false;	    	                    
        return true;
    }
    return false;	
}

function IsEdit(FullName,msg)
{
    var elm=document.forms[0].elements;	
    for(i=0;i<elm.length;i++)
    {
        if(elm[i].type=="checkbox" && elm[i].id.substring(elm[i].id.length-FullName.length)==FullName)
        {
            if(elm[i].checked==true)
                return true;
        }
    }
    var notselect = document.getElementById(msg);
    alert(notselect.value);
    return false;	    
}

function EnableButton(obj, enable)
{   
    if(obj==null)
        return;
    if (enable == false)  
       
        obj.disabled = true;
    else 
        obj.disabled = false;
    
}
///FullName: chckSelect hoac chckAll. Gan vao su kien rowdatabound
function CheckEdit(FullName,btnEdit)
{      
    var flag=false;
    var result=0;
    var btn=document.getElementById(btnEdit);
    var elm=document.forms[0].elements;	
    for(i=0;i<elm.length;i++)
        if(elm[i].type=="checkbox" && (elm[i].id.indexOf("chckSelect") > 0 || elm[i].id.indexOf("chckAll") > 0))
        {   
            if(flag && elm[i].checked==true)
            {
                result++;
            }
            if(!flag)flag=true;
        }
        
    if(result > 1)
        EnableButton(btn, false);
    else 
        pri_EnableButton(btn, true); 
}


//auto height cho page
function AutoHeight()
{
        var y = document.documentElement.clientHeight-130;
        document.getElementById("autoheight").style.minHeight=y+"px";
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
$(document).ready(function () {
    $(window).scroll(function () {
        var nHeight = getScrollXY()[1];
        var winwidth = document.all ? document.body.clientWidth : window.innerWidth;
        var left = (winwidth - 980) / 2;
        var eleWidth = $('.GridStyle').width();
        if (nHeight >= 114)
            $("#sidebar").css({ "position": "fixed", "top": "0px" });
        else
            $("#sidebar").css({ "position": "", "top": "0px" });

        if (nHeight >= 170)
            $(".box-header").addClass("topActive").css({ "position": "fixed", "top": "0px", "width": eleWidth - 30 }); // Padding = 15 => eleWidth - 30       
        else
            $(".box-header").removeClass("topActive").css({ "position": "", "top": "0px", "width": "" });
    });
    EventCheckBox();
});

function EventCheckBox() {
    $(".RowStyle td:not(.cbxCheck)").bind("click", function () {
        var t = $(this).parents("tr").find(".cbxCheck input");
        t.prop('checked', !t.is(':checked'));
        ChangeHeaderAsNeeded('chckSelect', 'chckAll', t); 
        ShowHideBtnEdit();
    });
}