var cookie = {
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
        var tmp_value = cookie.getCookie( c_name );
        if ( tmp_value != null && tmp_value != "" ) {
            return tmp_value;
        }
        else {
            if ( c_value != null && c_value != "" ) {
                cookie.setCookie( c_name, c_value, expiredays );
            }
        }
    }
    
}


