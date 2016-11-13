var GTOOL = {
    urlencode: function( dic ) {
        if ( typeof dic == "undefined" || dic == null ) {
            return false;
        }
        var arg = []
        for ( k in dic ) {
            arg.push( k + '=' + dic[k] );
        }
        arg = arg.join('&');
        return arg;
    }
};

var GCOOKIE = {
    getCookie: function( c_name ) {
        var i, x, c_value, ARRcookies = document.cookie.split( ";" );
        for ( i = 0; i < ARRcookies.length; i++ ) {
            x = ARRcookies[i].substr( 0, ARRcookies[i].indexOf( "=" ) );
            x=x.replace(/^\s+|\s+$/g,"");
            if ( c_name == x ) {
                c_value = ARRcookies[i].substr( ARRcookies[i].indexOf( "=" ) + 1 );
                return unescape( c_value );
            }
        }
    },
    setCookie: function( c_name, c_value, expiredays ) {
        var exdate = new Date();
        exdate.setDate( exdate.getDate() + expiredays );
        var expires = ( expiredays == null ) ? "" : ";expires=" + exdate.toUTCString();
        var tmp_value = [ 
            escape( c_value ),
            expires
        ].join("");
        document.cookie = c_name + "=" + tmp_value;
    },
    checkCookie: function( c_name, c_value, expiredays ) {
        var tmp_value = GCOOKIE.getCookie( c_name );
        if ( tmp_value != null && tmp_value != "" ) {
            return tmp_value;
        }
        else {
            if ( c_value != null && c_value != "" ) {
                GCOOKIE.setCookie( c_name, c_value, expiredays );
            }
        }
    }
}

var GTRACKER = {
    trackURL: function( arg ) {
        var referrer = document.referrer;
        var url = document.location.href;

        arg['referrer'] = encodeURIComponent(referrer);
        arg['url'] = encodeURIComponent(url);
        GTRACKER.sendReq(arg);
    },
    trackEvent: function( arg ) {
        GTRACKER.sendReq(arg);
    },
    tracker: function(  type, arg ) {
        if ( arguments.length <= 0 ) {
            return false;
        }
        var type = type || 'url';
        var arg = arg || {};

        //GTRACKER.ftn['type'](arg);
        if ( type == 'url' ) {
            GTRACKER.trackURL(arg);
        }
        else {
            GTRACKER.trackEvent(arg);
        }
    },
    other: function( arg ) {
    },
    sendReq: function( arg ) {
        var cookie = GCOOKIE.getCookie('xxz');
        var log = function(arg) {
            var cookie = GCOOKIE.getCookie('xxz');
            var url = 'http://www.pv.cc/b/__t.gif';
            arg['xxz'] = cookie;
            url += '?' + GTOOL.urlencode(arg) + '&callback=?';
            //url = encodeURIComponent(url);
            $(document).append('<img src="' + url + '" alt="">');
        };
        if ( cookie == null || cookie == "" || cookie.length != 6 ) {
            var url = 'http://www.pv.cc/b/__ck.json?callback=?';
            $.getJSON(url, function(res) {
                cookie = res;
                GCOOKIE.setCookie('xxz', cookie);
                
                log(arg);
            })
        }
        else {
            log(arg);
        }
    }
    //,
    //ftn: {
    //    url: GTRACKER.trackURL,
    //    evt: GTRACKER.trackEvent
    //}
};
