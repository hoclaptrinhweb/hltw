var $PW = (function () {
    var HOVER_TIMEOUT = 100,
    BEFOREGET_TIMEOUT = 500,
    LEAVE_TIMEOUT = 0;

    $(document).after('<div class="ctndisbox news_tooltip" id ="ppopup" style="top:-9999px;left:0px;" ></div>');
    $(document).after('<div class="ctndisbox news_tooltip" id="mppopuptemp" style="top:9999px;left:0px;" ></div>');
    $("#mppopup").hover(function () {
        if ($PW.leaveTimeout)
            clearTimeout($PW.leaveTimeout);
    }, function () {
        if ($PW.hoveredElement) {
            $PW.leaveTimeout = setTimeout(function () {
                hideMini();
                $PW.hoveredElement = null;
            }, LEAVE_TIMEOUT);
        }
    }).mouseup(function (e) {
        e.stopPropagation && e.stopPropagation();
        e.cancelBubble = true;
    }).html('<span id="mparrowtop" class="icodisbox" style="display: none"></span><div id="mpctn" class="disbox"></div><span id="mparrowbot" class="icodisbox" style="display: none"></span>');
    $("#mppopuptemp").html('<span class="icodisbox topright"></span><div id="mpctntemp" class="disbox"></div><span id="mparrowbottemp" class="icodisbox botleft"></span>');

    $(window).scroll(function () {
        $PW.hm();
    });


    function setPopupPosition(elem, ctn, before, after) {
        var pop = ctn || $('#mppopup'),
        ze = $(elem),
        offs = ze.offset(true),
        vp = $.getViewport(),
        h = ze.outerHeight(),
        cy = offs.top + h / 2,
        top, left, ha, va;
        offs.left += $.intval(ze.css('padding-left')) + $.intval(ze.css('margin-left'));
        if (vp.offsetY + vp.height / 2 <= cy) {
            top = offs.top - pop.outerHeight() - 5;
            ha = 'bot';
        } else {
            top = offs.top + h + 5;
            ha = 'top';
        }
        var arrow = $('#mparrow' + ha);
        arrow.show();
        $('#mparrow' + (ha == 'top' ? 'bot' : 'top')).hide();
        var powidth = pop.outerWidth(),
        right = offs.left + powidth + 30;
        if (vp.offsetX + vp.width > right) {
            left = offs.left - $.intval($('#mpctn').css('margin-left'));
            va = 'left';
        } else {
            left = offs.left + ze.width() - powidth + $.intval($('#mpctn').css('margin-left'));
            va = 'right';
        }
        typeof before == 'function' && before();
        arrow.attr('class', 'icodisbox ' + ha + va);
        $('#mppopup').css({
            top: top + 'px',
            left: left + 'px'
        });
        typeof after == 'function' && after();
    };

    function showMini(elem, data) {
        $('#mpctntemp').html(data);
        setPopupPosition(elem, $('#mppopuptemp'), function () {
            $('#mpctn').empty().removeClass('nodata').append($('#mpctntemp').children());
        });
    }

    function hideMini() {
        $('#mppopup').css({
            top: '-9999px',
            left: '0px'
        });
    }

    function showError(elem, err) {
        switch (err) {
            case 1:
                $('#mpctn').html('<p align="center">Tài khoản này không tồn tại.</p>').addClass('nodata');
                break;
            default:
                $('#mpctn').html('<p align="center">Có lỗi xảy ra! Xin vui lòng thử lại.</p>').addClass('nodata');
                break;
        }
        setPopupPosition(elem);
    }

    function startLoading(elem) {
        $('#mpctn').html('<p class="disbox_loading"></p>').addClass('nodata');
        setPopupPosition(elem);
    }

    return {
        mixHover: function (elem, articleId, isSel) {
            var ze;
            if ($.isString(elem) && !isSel)
                elem = '#' + elem;
            if (elem instanceof $Core)
                ze = elem;
            else
                ze = $(elem);
            ze.each(function () {
                if (!$.data(this, 'miniprofile')) {
                    var zt = $(this), href = zt.attr('href');
                    $.isString(href) && href.indexOf('/u/') >= 0 && zt.hover(function () {
                        $PW.sm(this);
                    }, function () {
                        $PW.hm();
                    });
                    $.data(this, 'miniprofile', true);
                }
            });
        },

        showMiniProfile: function (articleId, elem) {
            if (!articleId || !elem)
                return;
            $PW.hoveredElement = elem;
            var data = $.data(window, 'znTooltip.data.' + articleId);
            if (data)
                showMini(elem, data);
            else {
                setTimeout(function () {
                    startLoading(elem);
                    var xmlhttp;
                    show = elem == $PW.hoveredElement;
                    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                        xmlhttp = new XMLHttpRequest();
                    }
                    else {// code for IE6, IE5
                        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                    }
                    xmlhttp.onreadystatechange = function () {
                        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                            $.data(window, 'znTooltip.data.' + articleId, xmlhttp.responseText);
                            show && showMini(elem, xmlhttp.responseText);
                        } else {
                            //show && showError(elem, err);
                        }
                    }
                    var link = "/tooltip/" + articleId + ".html";
                    xmlhttp.open("GET", link, true);
                    xmlhttp.send();
                }, BEFOREGET_TIMEOUT);
            }
        },

        hideMiniProfile: function () {
            if ($PW.hoverTimeout)
                clearTimeout($PW.hoverTimeout);
            if ($PW.leaveTimeout)
                clearTimeout($PW.leaveTimeout);
            hideMini();
        },

        sm: function (t) {
            if ($PW.leaveTimeout)
                clearTimeout($PW.leaveTimeout);
            if ($PW.hoverTimeout)
                clearTimeout($PW.hoverTimeout);
            $PW.hoverTimeout = setTimeout(function () {
                var idString = t.toString();
                var articleId = idString.match(/\/a((\d)*).html/);
                $PW.showMiniProfile(articleId[1], t);
            }, HOVER_TIMEOUT);
        },

        hm: function () {
            if ($PW.hoverTimeout)
                clearTimeout($PW.hoverTimeout);
            $PW.leaveTimeout = setTimeout(function () {
                hideMini();
            }, LEAVE_TIMEOUT);
        }
    };
})();


