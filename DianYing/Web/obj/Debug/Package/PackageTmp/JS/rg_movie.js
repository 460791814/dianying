$(function() {
    var gid = 0;
    var gcnt = 6;
    var cache = {'rec_0':$('.item.movie_rec_area.active').html()};

    var get_rec = function(m_id,gid,gcnt){
        if (cache['rec_'+gid] == undefined){
            url = '/reflect/movie_rec/e30=/'+m_id+'_'+gid+'_'+gcnt;
            $.getJSON(url,function(lst){
                 var tmp = $('.item.movie_rec_area.active').clone(true,true);
                 tmp.find('li').each( function(i,item){
                     item = $(item);
                     var a = item.children('.z-movie-playlink');
                     a.attr('alt',lst[i].zh_title);
                     a.attr('href','/movie/'+lst[i].u_name);

                     var img = $(a.find('img'));
                     img.attr('src','/poster/m/'+lst[i].poster_id);
                     img.attr('alt',lst[i].u_name);

                     a.find('b').html(lst[i].zh_title);
                });
                cache['rec_'+gid] = tmp.html();
                $('.movie_rec_area').html(cache['rec_'+gid]);
            });
        }
        else {
            $('.movie_rec_area').html(cache['rec_'+gid]);
        }
    };
    $('a#movie_rec_next').click(function(){ 
        gid = (gid+1) % gcnt;
        get_rec($(this).attr('m_id'),gid,gcnt);
    });

    $('a#movie_rec_prev').click(function(){ 
        gid = (gid-1+gcnt) % gcnt;
        get_rec($(this).attr('m_id'),gid,gcnt);
    });
});
