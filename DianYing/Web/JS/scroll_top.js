$(function(){
    var scr_window_el = $(window);
    var gotop = $('#gotop');

    var fadeTime = 500;
    // to do id is not definded
    //
    gotop.hide()
    
    if ( scr_window_el.scrollTop() > 100 ) {
        gotop.fadeIn(fadeTime);
    }
    else {
        gotop.fadeOut(fadeTime);
    }

    scr_window_el.scroll(function(){
        if ( scr_window_el.scrollTop() > 100 ) {
            if( 'none' == gotop.css('display') ) { 
                //gotop.fadeIn(fadeTime);
                gotop.css('display','block');
            }
        }
        else {
            if( 'block' == gotop.css('display') ) { 
                //gotop.fadeOut(fadeTime);
                gotop.css('display','none');
            }
        }
    });

    gotop.click(function(event){
        scr_window_el.scrollTop(0);
    });
});
