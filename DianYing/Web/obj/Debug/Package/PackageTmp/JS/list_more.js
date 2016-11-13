$(function() {
    var list_more = $('.x-movie-more');
    var g_window = $(window);
    var g_document = $(document);
    function listMoreMovie($obj, method, argsString){
        var $btn = list_more;
        var $loading_pic = $('#loading_pic');
        $btn.hide();
        $loading_pic.show();
        var next = $btn.attr('data-page');
        $.getJSON('/reflect/' + method + '/' + argsString + '/' + next, function(json){
            $btn.attr('data-disable', '0');
            $loading_pic.hide()
            $obj.append(json.html);
            $btn.attr('data-page', json.nextp);
            if(json.more){
                $btn.show();
                g_window.trigger('scroll');
            }else
                list_more = null;
        });
    }
    $('.x-movie-entry').live('mouseenter', function(e){
        $(this).children('.x-movie-tags').show();
    }).live('mouseleave', function(e){
        $(this).children('.x-movie-tags').hide();
    });

    function sayILikeIt(_id){
        $.getJSON('/Handler/IdentityHandler.ashx' + _id, function (json) {
            if(json.code != 0){
                alert('error when setting like movie');
            }
        });
    }


    var typeColors = new Array();
    typeColors['like'] = 'danger'; typeColors['plan'] = 'warning'; typeColors['seen'] = 'success'; typeColors['dislike'] = 'success';


    var miedeng = function(btn){
        btn = $(btn);
        var data_btn = btn.parent().attr('data-btn');
        var type = btn.attr('data-type'); 
        var sib = '';
        if(type == 'plan'){
           sib = btn.siblings('a[data-type="seen"]'); 
        }
        else if (type == 'seen'){
            sib = btn.siblings('a[data-type="plan"]');
        }
        else if (type == 'like') {
            sib = btn.siblings('a[data-type="dislike"]');
        }
        else if (type == 'dislike') {
            sib = btn.siblings('a[data-type="like"]');
        }
        sib = $(sib);
        sib.removeClass('unmark btn-'+typeColors[sib.attr('data-type')])
        .addClass('mark')
        .addClass(data_btn);
    };

    $('.x-usermovie-controls').delegate('.mark', 'click', function(e) {
        e.stopPropagation();
        e.preventDefault();
        var btn = $(this);
        var movie_id = btn.attr('data-id');
        var type = btn.attr('data-type');
        $.getJSON('/Handler/IdentityHandler.ashx' + type + '/e30=/' + movie_id, function (json) {
            if(json.result == 0) {
                btn.addClass('unmark btn-' + typeColors[type]);
                btn.removeClass('mark btn-normal');

                miedeng(btn); 
                btn.children('i').addClass('icon-white');
            }
            else{
                window.location.href = '/login?next=' + encodeURIComponent(window.location.href);
            }
        });
        if(type == 'like')
            sayILikeIt(movie_id);

    }).delegate('.unmark', 'click', function(e) {
        e.stopPropagation();
        e.preventDefault();
        var btn = $(this);
        var movie_id = btn.attr('data-id');
        var type = btn.attr('data-type');
        $.getJSON('/reflect/user_unmark_' + type + '/e30=/' + movie_id, function(json){
            if(json.result == 0) {
                var data_btn = btn.parent().attr('data-btn');
                btn.addClass('mark').
                addClass(data_btn);
                btn.removeClass('unmark btn-' + typeColors[type]);
//                btn.children('i').removeClass('icon-white');
            }
            else{
                window.location.href = '/login?next=' + encodeURIComponent(window.location.href);
            }
        });

    })


    list_more.click(function(event) {
        var target = $(event.target);
        var disable = target.attr('data-disable');
        if ( disable != '0' ) {
            return false;
        }
        var method = target.attr('data-method');
        if(method == 'get_message')
            var $obj = $('table.x-msg-box');
        else
            var $obj = target.parents('div').prev('ul');
        var args = target.attr('data-args');
        listMoreMovie($obj, method, args);
        target.attr('data-disable', '1');
        var ct = target.attr('data-more-ct');
        target.attr('data-more-ct', parseInt(ct)+1);
    });

    g_window.scroll(function(event){
        if(!list_more)
            return true;
        var ct = list_more.attr('data-more-ct');
        if ( ct > 6 ) {
            return false;
        }
        if ( g_window.scrollTop() + 200 >= g_document.height() - g_window.height() ) {
            if(list_more)
                list_more.trigger('click');
        }
    });

    g_window.trigger('scroll');

});
