


//首页js
var indexInit = function () {
    var Page = function () {
        this.lastImg = 1;
    }
    var page = new Page();


    $(".intronav li").bind("mouseover", function () {
        var self = $(this);
        var lis = self.closest("ul").children("li");
        lis.removeClass("on");
        self.addClass("on");
        var nowImg = self.index();

        $(".intro img").stop(true).animate({ left: "-" + 800 * nowImg + "px" });
        page.lastImg = nowImg;

    });

    $(".menu li:eq(0)").addClass("on");
};

//文章列表页js
var plInit = function () {
    var PAGESIZE = 100;
    var IsNeedSearch = true;
    var CurrentPage = 1;
    $(".menu li:eq(1)").addClass("on");
    $("#btnSearch").click(function () {
        pageCallback(0);
    });
    $("#btnGoToPage").click(function () {
        var p = $("#targetPage").val();
        pageCallback(p - 1);
    });

    var getPassageList = function () {
        $.ajax({
            url: ajaxUrl,
            type: "get",
            dataType: "json",
            data: { f: "GetPassageList",data:"" },
            success: function (jsonData) {
                IsNeedSearch = false;
                initPage(jsonData.result.TotalCount);
                $("#passageList ul").empty();

                if (jsonData.success) {
                    $(".passageList ul").append($(".tmplPassageList").tmpl(jsonData.result.PassageList));
                } else {
                    IsNeedSearch = true;
                }
            }
        });
    };
    var initPage = function (total) {
        $("#Pagination").pagination(total, {
            callback: pageCallback,
            prev_text: '上一页',
            next_text: '下一页',
            items_per_page: PAGESIZE,
            num_display_entries: 5,
            current_page: CurrentPage - 1,
            num_edge_entries: 1
        });
    };

    var pageCallback = function (index, jq) {
        CurrentPage = index + 1;
        if (IsNeedSearch) {
            getPassageList();
        } else {
            IsNeedSearch = true;
        }
        return false;
    };

    getPassageList();
}

//文章编辑页js
var peInit = function () {

    $("#btnSavePassage").on("click", function () {
        var data = {};
        data.Title = $("#txtptitle").val();
        data.Author = "肖斐";
        data.Type = 1;
        data.content = getContentHtml();
        data.PassageId = 0;
        $.ajax({
            url: "~/Handle/MustGripHandle.ashx",
            data: { data: JSON.stringify(data), f: "SavePassage" },
            dataType: "json",
            type: "POST",
            success: function () {
                alert("success");
            }

        });

    });

    //编辑器初始化
    $d = $("#txtpcontent")[0].contentWindow.document; // IE、FF都兼容
    $d.designMode = "on";
    $d.contentEditable = true;
    $d.open();
    $d.close();

    $('#btnInsertImage').click(function () {
        // 在iframe中插入一张图片                                    
        var img = '<img src="' + $('.txtppath').val() + '" />';
        $("body", $d).append(img);
    });
    var getContentHtml = function () {
        var html = $('#txtpcontent').contents().find('body').html();
        return Util.htmlEncodeByRegExp(html);
    }

    //test
    $.ajax({
        url: ajaxUrl,
        data: { f: "ReadPassage" },
        dataType: "json",
        type: "post",
        success: function (data) {
            var content = Util.htmlDecodeByRegExp(data.msg);
            $('#txtpcontent').contents().find('body').html(content);
        }

    });
}

//工具类
var Util = {
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
    },
    bindDatepicker: function (id) {
        var idName = "#" + id;
        $(idName).datepicker();
        $(idName).datepicker("option", "dateFormat", 'yy-mm-dd');
        $(idName).datepicker("option", "dayNamesMin", ['日', '一', '二', '三', '四', '五', '六']);
        $(idName).datepicker("option", "monthNames", ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']);
        $(idName).datepicker("option", "changeYear", true);
    },
    getParam: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    },

    //js获取项目根路径，如： http://localhost:8083/uimcardprj
    pathUtil: function () {
        //获取当前网址，如： http://localhost:8083/uimcardprj/share/meun.jsp
        this.curWwwPath = window.document.location.href;
        //获取主机地址之后的目录，如： uimcardprj/share/meun.jsp
        this.pathName = window.document.location.pathname;
        this.pos = this.curWwwPath.indexOf(this.pathName);
        //获取主机地址，如： http://localhost:8083
        this.localhostPath = this.curWwwPath.substring(0, this.pos);
        //获取带"/"的项目名，如：/uimcardprj
        this.projectName = this.pathName.substring(0, this.pathName.substr(1).indexOf('/') + 1);
        this.rootPath = this.localhostPath + this.projectName;
    }

};


//header头js
$(".menu li").bind("mouseover", function () {
    var self = $(this);
    self.addClass("hoveron");
}).bind("mouseout", function () {
    var self = $(this);
    self.removeClass("hoveron");
});
var PathUtil = new Util.pathUtil();
var ajaxUrl = PathUtil.localhostPath + "/Handle/MustGripHandle.ashx";