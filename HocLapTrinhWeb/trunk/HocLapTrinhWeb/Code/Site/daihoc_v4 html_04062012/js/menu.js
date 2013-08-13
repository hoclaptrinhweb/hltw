function NextPagging(id) {
var position = $("#"+id).position();
var width = $("#"+id).width();
var left = -position.left + 360;
if (left < width)
$("#"+id).animate({ left: '-=360px' }, 500);
}
function PrevPagging(id) {
var position = $("#"+id).position();
var width = $("#"+id).width();
var left = position.left;
if (left < 0)
$("#"+id).animate({ left: '+=360px' }, 500);
} 