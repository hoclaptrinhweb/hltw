$(document).ready(function(){
	//function for contact form dropdown
	function contact() {
		if ($("#rssForm").is(":hidden")){
			$("#rssForm").slideDown("slow");
			$("#backgroundContactPopup").css({"opacity": "0.7"});
			$("#backgroundContactPopup").fadeIn("slow"); 
		}
		else{
			$("#rssForm").slideUp("slow");
			$("#backgroundContactPopup").fadeOut("slow");  
		}
	}
	 
	//run contact form when any contact link is clicked
	$(".rss").click(function(){contact()});
	
	//animation for same page links #
	$('a[href*=#]').each(function() {
		if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'')
		&& location.hostname == this.hostname
		&& this.hash.replace(/#/,'') ) {
		  var $targetId = $(this.hash), $targetAnchor = $('[name=' + this.hash.slice(1) +']');
		  var $target = $targetId.length ? $targetId : $targetAnchor.length ? $targetAnchor : false;
			if ($(this.hash).length) {
				$(this).click(function(event) {
					var targetOffset = $(this.hash).offset().top;
					var target = this.hash;
					event.preventDefault();			   
					$('html, body').animate({scrollTop: targetOffset}, 500);
					return false;
				});
			}
		}
	});

	//only need force for IE6  
	$("#backgroundContactPopup").css({  
		"height": document.documentElement.clientHeight 
	});  
});