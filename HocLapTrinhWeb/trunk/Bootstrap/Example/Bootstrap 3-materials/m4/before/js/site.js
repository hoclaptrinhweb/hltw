/// <reference path="jquery-2.0.3.min.js" />
(function () {
    $("#catMenu li a").click(function () {
        $("#catOrther").text($(this).text());
    })
})();