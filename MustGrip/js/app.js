//公共js
$(".menu li").bind("mouseover",function(){
	var self = $(this);
	self.addClass("hoveron");
}).bind("mouseout",function(){
	var self = $(this);
	self.removeClass("hoveron");
});


//首页js
var indexInit = function(){
	var Page=function(){
		this.lastImg = 1;
	}
	var page=new Page();	


	$(".intronav li").bind("mouseover",function(){
		var self = $(this);
		var lis = self.closest("ul").children("li");
		lis.removeClass("on");
		self.addClass("on");
		var nowImg = self.index();

		$(".intro img").stop(true).animate({left:"-"+800*nowImg+"px"});
		page.lastImg=nowImg;

	});

    $(".menu li:eq(0)").addClass("on");
};

//文章列表页js
var plInit = function(){

    $(".menu li:eq(1)").addClass("on");
}


