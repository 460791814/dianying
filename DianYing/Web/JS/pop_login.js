$(function(){
            var login_register_dialog_locate = function(){ 
                var window_height = $(window).height();
                var window_width = $(window).width();
                var login_dialog_top = (window_height - 300) / 2 +'px'; 
                var login_dialog_left = (window_width - 555) / 2 +'px'; 
                var register_dialog_top = (window_height - 350) / 2 +'px'; 
                $(".pop-login-dialog").css({"left":login_dialog_left,"top":login_dialog_top});
                $(".pop-dialog-playmask").css({"height":window_height,"width":window_width});
                $(".pop-register-dialog").css({"left":login_dialog_left,"top":register_dialog_top});
            }
            login_register_dialog_locate();
            $(".login-register-dialog").click(function(){
                $(".fresh-control").each(function(){
                    $(this).val("");
                });
                $(".pop-dialog-playmask").show();
                $(".pop-login-dialog").fadeIn();

            });
            $("#close_login_dialog").click(function(){
                $(".pop-login-dialog").hide(); 
                $(".pop-dialog-playmask").hide();
                $("#login_error_tips").hide();
            });
            $("#close_register_dialog").click(function(){
                $(".pop-register-dialog").hide(); 
                $(".pop-dialog-playmask").hide();
                $("#register_error_tips").hide();
            });
            $(window).resize(login_register_dialog_locate); 
            
            $("#login_to_register").click(function(){
                $("#close_login_dialog").trigger('click');
                $(".fresh-control").each(function(){
                    $(this).val("");
                }); 
                $(".pop-dialog-playmask").show();
                $(".pop-register-dialog").fadeIn();
            

             });
            $("#register_to_login").click(function(){
                $("#close_register_dialog").trigger('click');
                $("#login").trigger("click"); 
            });

            $("#refresh_captcha").click(function(){
                $("#captcha_img").trigger("click"); 
            }); 
        
            
        



            

        });

