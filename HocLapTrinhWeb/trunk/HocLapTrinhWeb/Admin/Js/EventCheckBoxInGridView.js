function AddEventCheckBox() {
    $(".RowStyle td:not(.cbxCheck)").bind("click",function () {
        $(this).parents("tr").find(".cbxCheck input").click();
    });

    $(".GridItem td:not(.cbxCheck)").bind("click",function(){
        var cb = $(this).parent('tr').children(':first-child').children(':first-child')[0];
        if(cb.checked == true)
        {
            cb.checked = false;
            $($(this).parent('tr')[0]).css({backgroundColor: ''});
        }
        else
        {
            cb.checked = true;
            $($(this).parent('tr')[0]).css({backgroundColor: 'yellow'});
        }
       ChangeHeaderAsNeeded('chckSelect','chckAll',cb);ShowHideBtnEdit();
        return false;
    })
}

$(function() {AddEventCheckBox();})