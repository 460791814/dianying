var J50Npi = J50Npi || {  
    currentScript: null,  
    getJSON: function(url, data, callback) {
      var src = url + (url.indexOf("?")+1 ? "&" : "?");
      var head = document.getElementsByTagName("head")[0];
      var newScript = document.createElement("script");
      var params = [];
      var param_name = ""

      this.success = callback;

      data["callback"] = "J50Npi.success";
      for(param_name in data){  
          params.push(param_name + "=" + encodeURIComponent(data[param_name]));  
      }
      src += params.join("&")

      newScript.type = "text/javascript";  
      newScript.src = src;

      if(this.currentScript) head.removeChild(currentScript);
      head.appendChild(newScript); 
    },
    success: null
}; 

var PA = PA || {
    _sid: '',
    _sent: '',
    urlEncode: function( dic ) {
        if ( typeof dic == "undefined" || dic == null ) {
            return false;
        }
        var arg = []
        for ( k in dic ) {
            arg.push( k + '=' + dic[k] );
        }
        arg = arg.join('&');
        return arg;
    },
    setCookie: function(cname,cvalue,exsecs)
    {
        var d = new Date();
        d.setTime(d.getTime()+(exsecs*1000));
        var expires = "expires="+d.toGMTString();
        document.cookie = cname + "=" + cvalue + "; " + expires;
    },
    getCookie: function(cname)
    {
        var name = cname + "=";
        var ca = document.cookie.split(';');
        for(var i=0; i<ca.length; i++) 
        {
            var c = ca[i].trim();
            if (c.indexOf(name)==0) return c.substring(name.length,c.length);
        }
        return "";
    },
    trackUrl: function( arg ) {
        arg = arg || {};
        arg['ref'] = document.referrer;
        arg['url'] = document.location.href; 
        arg['title'] = document.title; 
        arg['type'] = 'url';
        PA.sendRequest(arg);
    },
    trackAction: function( arg ) {
        arg['url'] = document.location.href; 
        arg['type'] = 'action';
        PA.sendRequest(arg);
    },
    trackUser: function( arg ) {
        arg['url'] = document.location.href; 
        arg['type'] = 'user';
        PA.sendRequest(arg);
    },
    track: function(type, arg) {
        if ( arguments.length <= 0 )
            return false;
        var type = type || 'url';
        var arg = arg || {};
        if ( type == 'url' ) {
            PA.trackUrl(arg);
        }else if( type == 'user' ) {
            PA.trackUser(arg);
        }else {
            PA.trackAction(arg);
        }
    },
    sendRequest: function( arg ) {
        for(var k in arg){
            arg[k] = encodeURIComponent(arg[k]);
        }
        var log = function(arg) {
            var url = 'http://analytics.pv.cc/tracker/collect';
            arg['sid'] = PA._sid;
            var args = PA.urlEncode(arg);

            if(PA._sent.indexOf(args + ';') > -1)
                return false;
            PA._sent += args + ';';

            url += '?' + args + '&r=' + Math.random();
            var image = new Image;
            image.src = url;
            document.body.appendChild(image);
        };
        log(arg);
    },
    setSite: function(sid){
        PA._sid = sid;
    }
};
