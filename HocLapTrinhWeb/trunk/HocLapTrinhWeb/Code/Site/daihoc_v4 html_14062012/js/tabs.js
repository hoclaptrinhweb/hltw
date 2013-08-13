/***************************/
//@Author: Adrian "yEnS" Mato Gondelle & Ivan Guardado Castro
//@website: www.yensdesign.com
//@email: yensamg@gmail.com
//@license: Feel free to use it, but keep this credits please!					
/***************************/

$(document).ready(function(){
	$(".tab_menu > li").click(function(e){
		switch(e.target.id){
			case "mienbac":
				//change status & style tab_menu
				$("#mienbac").addClass("active");
				$("#mientrung").removeClass("active");
				$("#miennam").removeClass("active");
				//display selected division, hide others
				$("div.mienbac").fadeIn();
				$("div.mientrung").css("display", "none");
				$("div.miennam").css("display", "none");
				$('div.mienbac').jScrollPane({maintainPosition: false,animateScroll: true});
		
			break;
			case "mientrung":
				//change status & style tab_menu
				$("#mienbac").removeClass("active");
				$("#mientrung").addClass("active");
				$("#miennam").removeClass("active");
				//display selected division, hide others
				$("div.mientrung").fadeIn();
				$("div.mienbac").css("display", "none");
				$("div.miennam").css("display", "none");
				$('div.mientrung').jScrollPane({maintainPosition: false,animateScroll: true});
			break;
			case "miennam":
				//change status & style tab_menu
				$("#mienbac").removeClass("active");
				$("#mientrung").removeClass("active");
				$("#miennam").addClass("active");
				//display selected division, hide others
				$("div.miennam").fadeIn();
				$("div.mienbac").css("display", "none");
				$("div.mientrung").css("display", "none");
				$('div.miennam').jScrollPane({maintainPosition: false,animateScroll: true});
			break;
		}
		//alert(e.target.id);
		return false;
	});
});