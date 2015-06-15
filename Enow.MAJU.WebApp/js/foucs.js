(function($, window, undefined) {

	$(function($){
		$('html').addClass('js');

		// top menu bar show or hidden
		$("div.right div.right.center.borderl:has(.menu)").click(function(){
			$("#hideMenu").slideToggle();
		});
		//top search bar show or hidden
		$("div.right div.right.center.borderlr:has(.search)").click(function(){
			$("#hideSearch").slideToggle();
		});


		// consumers product list 3 column change event
		$(".fontm:has(.pbl) > span").click(function(e){
			e.preventDefault();
			if($(this).is(".tab-on")) return;
			var tabs=$(this).parent().children("span");
			var i=tabs.index($(this));
			tabs.removeClass("tab-on c900").addClass("tab")
			.end().end().removeClass("tab").addClass("tab-on c900")
			.parents(".center.fontm").css("overflow","hidden").parent().children(".wrap")
			.hide().eq(i).fadeIn(1000);
			tabs.each(function(x,o){
				if(x==i)$(o).find("img").width("auto").hide().filter(".tab-img-on").show();
				else $(o).find("img").width("auto").hide().filter(".tab-img-off").show();
				});
			});
			// product list tap only show one item
			$(".enter-pro-list > ul").each(function(i, o){
					$(o).children("li").each(function(_i, _o){
					if(_i>0)$(_o).next(".tabs_content, .enter-pro-inlist, div").hide().end().find(".iconAddSub[src*='sub']").hide().end().find(".iconAddSub[src*='add']").show();});
			}).find("> li")
			.click(function (e) {
				$(e.currentTarget).parent().children(".enter-pro-inlist:not(:hidden)").not($(this).next(".tabs_content, .enter-pro-inlist, div")).slideUp()
				.each(function(i, o){$(o).prev().find(".iconAddSub").eq(0).hide().next().show();});
				$(this).next(".tabs_content, .enter-pro-inlist, div").slideToggle()
					.end().find(".iconAddSub").toggle();
				e.preventDefault();
			});
			// close the tell bar
			$(".tellbar-close").click(function(e){
				$(".tellbar").fadeOut(500);
				e.preventDefault();
			});
			
			//ucm bug: a tag href
			if($(".tellbar .tellbar-tel-num").length>0)
			$(".tellbar .tellbar-tel-num").attr("href",$(".tellbar .tellbar-tel-num").attr("href").replace(/.*?(tel\:.*?)\.htm/i,"$1"));
		
			// enterprice product list tab touch swipe change tab active
			$(".nav3 .wrap.fontm > ul").each(function(x,o){
				var tabs=$(o).find('> li > a');
				tabs.click(function(e){
					e.preventDefault();
					if($(e.target).is(".active"))return;
					var i=tabs.index(e.target);
					tabs.removeClass("active").eq(i).addClass("active");
					//if($(e.target).parents(".swipe-nav").length>0) $(e.target).parents(".banner.clearfix").first().find(".swipe-slide.slides_wrap > .slide").eq(i).trigger("activate");
					//else
					$(o).parents(".nav3").next().children().hide().eq(i).fadeIn(500);
				});
			});

			// menu tap slider event
			$("#hideMenu.hidden-list > ul > li:even").bind("click",function(e){
				if($(this).parent().find(".sub-menu-open").length)
				$(this).parent().find(".sub-menu-open")
				.removeClass("sub-menu-open").not($(this)).find(".iconAddSub").eq(1).hide().prev().show().parent().next(".sub-menu").slideUp();
				
				$(this).addClass("sub-menu-open").find(".iconAddSub").toggle()
				.end().next(".sub-menu").slideToggle();
			});



			// More Success Story click event
			$(".btn.gray.center.fontm.radiuss.mtl.mbl > .c3[href='#']").bind("click",function(e){
				var currentDiv=$(this).parents(".banner.clearfix").eq(0);
				var page=parseInt (currentDiv.attr("data-page"))+1;
				if(isNaN(page))page=2;
				e.preventDefault();
			});

			//keyword auto focus
			var keyword_input=$(".search-form .search-input");
			var old_keyword=keyword_input.val();
			//html5 placeholder
			keyword_input.attr("placeholder",old_keyword).val("");
//			keyword_input.bind("focus",function(e){
//				if($(this).val()==old_keyword)$(this).val("");
//			});



		// change the picture event
		$(".picture-page").bind("change",function(e,param){
			var imgs=$(".picture-l > ul").children();
			var i=imgs.index(param);
			$(this).text((i+1)+"/"+imgs.length);
			$(".ad-nav .ad-thumbs .ad-thumb-list .ad-thumb").eq(i).trigger("vchange");
			//console.log(arguments);
		}).trigger("change",[null]);


		// touch swipe event change the tab bar
		$(".nav3 .wrap.fontm > ul").bind("change",function(e,param){
			var tabs=$(this).children();
			tabs.find("> a").removeClass("active");
			tabs.eq(param).find("> a").addClass("active");
		});

		});
	})(jQuery, window);

	// touch swipe the bar
	(function($, window, undefined) {
		$(document).ready(function() {
			var start;
			var wrap = $('.swipe-nav, .swipe-tab-bar, div.swipe-div1, div.swipe-div8');//,
			wrap
			//.addClass('slides_wrap')
			//.removeAttr('style')
			.children()
			//.addClass('slide')
			//.addClass('active')
			//.css('width','800px')
			.bind('movestart', function(e){
				// Only listen to one finger
				if (e.targetTouches && e.targetTouches.length > 1) {
					e.preventDefault();
					return;
				}

				{
					start = {
						x: parseInt($(this).css('left'))//,
						//y: parseInt($(this).css('top'))
					};
				}
			})
			.bind('move', function(e){
				{
					$(this).css({
						left: start.x + e.distX//,
						//	top: start.y + e.distY
					});

				}
			})
			.bind('moveend', function(e) {
				var maxOffset=-(wrap.attr("data-offsetX")?parseInt(wrap.attr("data-offsetX")):100);
				//console.log(maxOffset);
				if(e.distX>0){
					$(this).css('left','');
				}
				if(e.distX<0){
					var left=start.x + e.distX;
					left=left<maxOffset?maxOffset:left;
					$(this).css('left',left+'px');
				}
			});


		});
	})(jQuery,window);


	//touch swipe the slider
	(function($, window, undefined) {
		$(document).ready(function() {
			var wraps = $('.swipe-panel, .swipe-slide, div.swipe-div2, div.swipe-div3, div.swipe-div4, div.swipe-div5, div.picture-l > ul')
			//.addClass('slides_wrap')
			//.removeAttr('style')
			.each(function(x,o){
				var wrap=$(o),
				slides = wrap.children(),
				active = slides.filter('.active'),
				i = slides.index(active),
				width = wrap.width()?wrap.width():wrap.parent().width();

				//wrap.parents(".banner.clearfix").first().find('.banner-num, .model-num').css('z-index',10);
				// Listen for swipe events on slides, and use a custom 'activate'
				// event to add and remove the class 'active' to the previous
				// or next slide, and to keep the index up-to-date. The class
				// 'active' uses CSS transitions to make the slide move.
				slides
				//.removeAttr('style')
				//.css('margin-left','-4px')
				//.addClass('slide')
				//.eq(0)//.addClass('active')
				//.css('margin-left','0')
				//.end()

				.bind('swipeleft', function(e) {
					if (i === slides.length - 1) { //slides.eq(0).trigger('activate');
					return; }
					slides.eq(i + 1).trigger('activate');
				})

				.bind('swiperight', function(e) {
					if (i === 0) { //slides.eq(slides.length - 1).trigger('activate');
					return; }
					slides.eq(i - 1).trigger('activate');
				})

				.bind('activate', function(e) {
					slides.eq(i).removeClass('active');

					$(e.target).addClass('active');
					// Update the active slide index
					i = slides.index(e.target);
					wrap.parent().find('div.banner-num, div.model-num, div.banner_txt').each(function(x,o){
						$(o).find('> a').removeClass('dot-select').addClass('dot')
						.eq(i).removeClass('dot').addClass('dot-select');
                        $(o).find('> span').removeClass('txt_select').addClass('txt').eq(i).removeClass('txt').addClass('txt_select');
					});

					if($(e.target).parents(".picture-page"))
					$(".picture-page").trigger("change",[e.target]);

					$(".nav3 .wrap.fontm.swipe-nav > ul").trigger("change",[i]);
					//					$(e.target).parents(".swipe-panel").eq(0).parent().find(".nav3 .wrap.fontm.swipe-nav > ul").trigger("change",[i]);

				})

				// The code below handles what happens before any swipe event is triggered.
				// It makes the slides demo on this page work nicely, but really doesn't
				// have much to do with demonstrating the swipe events themselves. For more
				// on move events see:
				//
				// http://stephband.info/jquery.event.move

				.bind('movestart', function(e) {
					// If the movestart heads off in a upwards or downwards
					// direction, prevent it so that the browser scrolls normally.
					if ((e.distX > e.distY && e.distX < -e.distY) ||
					(e.distX < e.distY && e.distX > -e.distY)) {
						e.preventDefault();
						return;
					}

					// To allow the slide to keep step with the finger,
					// temporarily disable transitions.
					wrap.addClass('notransition');
				})

				.bind('move', function(e) {
					var left = 100 * e.distX / width;

					// Move slides with the finger
					if (e.distX < 0) {
						if (slides[i+1]) {
							//slides[i].style.left = left + '%';
							//slides[i+1].style.left = (left+100)+'%';
							slides.eq(i).css('left',left + '%');
							slides.eq(i+1).css('left',(left+100)+'%');
						}
						else {
							//slides[i].style.left = left/4 + '%';
							slides.eq(i).css('left',left/5 + '%');
							//slides.first().css('left',(left+100) + '%');
						}
					}
					if (e.distX > 0) {
						if (slides[i-1]) {
							//slides[i].style.left = left + '%';
							//slides[i-1].style.left = (left-100)+'%';
							slides.eq(i).css('left',left + '%');
							slides.eq(i-1).css('left',(left-100)+'%');
						}
						else {
							//slides[i].style.left = left/5 + '%';
							slides.eq(i).css('left',left/5 + '%');
							//slides.last().css('left',(left-100) + '%');
						}
					}
				})

				.bind('moveend', function(e) {
					wrap.removeClass('notransition');
					//slides.eq(i).trigger('activate');
					//return;
					//slides[i].style.left = '';
					slides.eq(i).css('left','');

					if (slides[i+1]) {
						//slides[i+1].style.left = '';
						slides.eq(i+1).css('left','');
					}else{
						//slides.first().css('left','');
					}
					if (slides[i-1]) {
						//slides[i-1].style.left = '';
						slides.eq(i-1).css('left','');
					}else{
						//slides.last().css('left','');
					}
				});

				// Make the buttons work, too. Hijack the id stored in the href and use
				// it to activate the slide.

				wrap.parent().find('div.banner-num > a, div.model-num > a')
				.bind('click', function(e) {
					//var href = e.target.hash;
					var i=$(this).index();
					var href=slides.eq(i);
					$(href).trigger('activate');

					e.preventDefault();
				});
			});

		});
	})(jQuery, window);

(function ($, undefined) {
$(function($){
//bof
$(".banner-num").parent().css("height",Math.round($("body").width()*300/540));
setTimeout(function(){
$(".notransition").removeClass("notransition");
$(".js .slide").addClass("slide-transition");
$(".banner-num").parent().css("height","");
}, 500);
//auto player banner slide 
(function(t){
	if($(".banner  .banner-num .dot-select").length==0)return;
	var auto_slide=setInterval(function(){	var i=$(".banner  .banner-num .dot-select").index();
	var count=$(".banner .banner-num").children().length;
	if(i>=count-1)i=0;
	else i+=1;
	$(".banner  .banner-num").prev().children(".slide").eq(i).trigger("activate");},t);

})(1000*8);


//eof
});
})(jQuery);


(function ($, undefined) {
$(function($){
//bof
// 
if($(".picture-l").length==0) return;

$(".ad-nav .ad-thumbs .ad-thumb-list .ad-thumb").bind("click",function(e){
	$(".picture-l .swipe-slide .slide").eq($(this).index()).trigger("activate");
	$(this).trigger("vchange");
})
.bind("vchange",function(e){console.log(arguments,this);
	$(this).parent().children(".active").removeClass("active").end().end().addClass("active");
	var i=$(this).index();
	var $this=$(this);
	var $margin_div=$(this).parent();
	if(i<=1||i>=$margin_div.children().length-2)return;
	$margin_div.stop().animate({"margin-left":-((i-2)*parseInt($(this).outerWidth()))},500);
})
.eq(0).addClass("active");

$(".ad-nav .ad-back").bind("click",function(e){
	$(".picture-l .swipe-slide .slide.active").prev().trigger("activate");
	$(".ad-nav .ad-thumbs .ad-thumb-list .ad-thumb.active").prev().trigger("vchange");
	//$(".ad-nav .ad-thumbs .ad-thumb-list").css("margin-left",parseInt($(".ad-nav .ad-thumbs .ad-thumb-list").css("margin-left"))+98);
	
});
$(".ad-nav .ad-forward").bind("click",function(e){
	$(".picture-l .swipe-slide .slide.active").next().trigger("activate");
	$(".ad-nav .ad-thumbs .ad-thumb-list .ad-thumb.active").next().trigger("vchange");
	//$(".ad-nav .ad-thumbs .ad-thumb-list").css("margin-left",parseInt($(".ad-nav .ad-thumbs .ad-thumb-list").css("margin-left"))-98);
});

//eof
});
})(jQuery);

(function ($, window, undefined) {
    $(function ($) {
        //bof

        $(".banner-num, .model-num").each(function (i, o) {
            var count = $(this).parent().find(".slides_wrap > .slide").length;
            if (count <= 1) {
                $(this).empty();
                return;
            }

            $dots = [];
            for (var k = 0; k < count; k++)
                $dots.push('<a class="radiusl dot"></a>');

            $(this).empty()
                .append($dots.join("")).width((count + 0.1) + "em").children()
                .eq(0).addClass("dot-select")
                .end().each(function (i, o) {
                    $(this).unbind().bind('click', function (e) {
                        //var href = e.target.hash;
                        var i = $(this).index();
                        var href = $(this).parent().parent().find(".slides_wrap > .slide").eq(i);
                        $(href).trigger('activate');

                        e.preventDefault();
                    });

                });

        });



        //eof
    });
})(jQuery, window);
