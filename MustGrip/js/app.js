

//header头js
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

//文章编辑页js
var peInit = function(){
    $("#btnSavePassage").on("click", function () {
		var data = {};
		data.Title = $("#txtptitle").val();
		data.Author = "肖斐";
		data.Type = 1;
		data.content = getContentHtml();
	    data.PassageId = 0;
		$.ajax({
			url:"/Handle/MustGripHandle.ashx",
			data: { data: JSON.stringify(data), f: "SavePassage"},
			dataType:"json",
			type:"POST",
			success: function(){
				alert("success");
			}
			
		});		
		
	});

	//编辑器初始化
	$d = $("#txtpcontent")[0].contentWindow.document; // IE、FF都兼容
	$d.designMode="on";
	$d.contentEditable= true;
	$d.open();
	$d.close();

	$('#btnInsertImage').click(function(){
        // 在iframe中插入一张图片                                    
        var img = '<img src="' + $('.txtppath').val() +'" />';
        $("body", $d).append(img);
    });
	var getContentHtml=function(){
	    var html = $('#txtpcontent').contents().find('body').html();
	    return HtmlUtil.htmlEncodeByRegExp(html);
	}
}

//工具类
var HtmlUtil = {
    /*1.用正则表达式实现html转码*/
    htmlEncodeByRegExp: function (str) {
        var s = "";
        if (str.length == 0) return "";
        s = str.replace(/&/g, "&amp;");
        s = s.replace(/</g, "&lt;");
        s = s.replace(/>/g, "&gt;");
        s = s.replace(/ /g, "&nbsp;");
        s = s.replace(/\'/g, "&#39;");
        s = s.replace(/\"/g, "&quot;");
        return s;
    },
    /*2.用正则表达式实现html解码*/
    htmlDecodeByRegExp: function (str) {
        var s = "";
        if (str.length == 0) return "";
        s = str.replace(/&amp;/g, "&");
        s = s.replace(/&lt;/g, "<");
        s = s.replace(/&gt;/g, ">");
        s = s.replace(/&nbsp;/g, " ");
        s = s.replace(/&#39;/g, "\'");
        s = s.replace(/&quot;/g, "\"");
        return s;
    }
};