//Home Cicle Large
$(function() {

	$('.cicle').hover(function() {//handover
		$('.cicle').each(function() {
			$(this).css("opacity","0.3");
		});
		$(this).css("opacity","1");
	}, function(){//handout
		
		$('.cicle').each(function() {
			$(this).css("opacity","1");
		});
	});

	 <!--click vao cicle-->

	$('.c1').click(function() {
		
		var duplicate = $(this).clone();
		$(duplicate).appendTo($('.cicle_large'));
		
		$(duplicate).animate({
							top: "+=180",
							opacity:0
						}, 600,function() {
							//after move position
							$(duplicate).remove();

						}); 

	});
	
	$('.c2').click(function() {
		var duplicate = $(this).clone();
		$(duplicate).appendTo($('.cicle_large'));
		
		$(duplicate).animate({
							top:"+=90",
							left: "+=170",
							opacity:0
						}, 600 ,function() {
							$('.cicle_logo').animate({opacity:0.5},200)
											.animate({opacity:1},200)
							;
							$(duplicate).remove();
						});  
	});
	
	$('.c3').click(function() {
		var duplicate = $(this).clone();
		$(duplicate).appendTo($('.cicle_large'));
		
		$(duplicate).animate({
							bottom: "+=90",
							left: "+=170",
							opacity:0
						}, 600 ,function() {
							$('.cicle_logo').animate({opacity:0.5},200)
											.animate({opacity:1},200)
							;
							$(duplicate).remove();
						});  
	});
	$('.c4').click(function() {
		var duplicate = $(this).clone();
		$(duplicate).appendTo($('.cicle_large'));
		
		$(duplicate).animate({
							bottom: "+=180",
							opacity:0
						}, 600 ,function() {
							$('.cicle_logo').animate({opacity:0.5},200)
											.animate({opacity:1},200)
							;
							$(duplicate).remove();
						});  
	});
	$('.c5').click(function() {
		var duplicate = $(this).clone();
		$(duplicate).appendTo($('.cicle_large'));
		
		$(duplicate).animate({
							bottom: "+=90",
							right: "+=170",
							opacity:0
						}, 600 ,function() {
							$('.cicle_logo').animate({opacity:0.5},200)
											.animate({opacity:1},200)
							;
							$(duplicate).remove();
						});  
	});
	$('.c6').click(function() {
		var duplicate = $(this).clone();
		$(duplicate).appendTo($('.cicle_large'));
		
		$(duplicate).animate({
							top: "+=90",
							right: "+=170",
							opacity:0
						}, 600 ,function() {
							$('.cicle_logo').animate({opacity:0.5},200)
											.animate({opacity:1},200)
							;
							$(duplicate).remove();
						}); 
	});
		  
		  
//1. load data
//2. random background
//$('.newsdetail').each(function(){
		
		//option1: random all color
		/*var hue = 'rgb(' + (Math.floor((256-199)*Math.random()) + 200) + ',' +
		(Math.floor((256-199)*Math.random()) + 200) + ','+ 
		(Math.floor((256-199)*Math.random()) + 200) + ')';
	
		var hue = 'rgb(' + (Math.floor(Math.random() * 256)) + ',' + 
			 (Math.floor(Math.random() * 256)) + ',' + 
			 (Math.floor(Math.random() * 256)) + ')';
		$(this).find('.bg').css("background-color",hue);
		 */
		 
		 //Option2: random in array color
	//	var colors = ["#2e8af1","#6ab31c","#be1e4c","#009619","#603cba","#db552f","#6eb71f","#ff8100","#dbbe00","#aae9fe","#8e8e8e","#e29d1b","#e61919","#4398b0","#94d05a","#0e6c38"]; 
//		var rand = Math.floor(Math.random()*colors.length);
//		$(this).find('.bg').css("background-color",colors[rand]);
//	});
});
