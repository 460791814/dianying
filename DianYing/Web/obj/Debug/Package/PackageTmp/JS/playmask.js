$(function() {
    $('.x-category-result').delegate('a.z-movie-playlink', 'mouseleave', function(e) {
        $(this).children('.z-movie-playmask').css('visibility','hidden');
    })
    .delegate('a.z-movie-playlink', 'mouseenter', function(e) {
        $(this).children('.z-movie-playmask').css('visibility','visible');
    });
});
