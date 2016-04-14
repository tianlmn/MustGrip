


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
    var PAGESIZE = 4;
    var CurrentPage = 1;
    var AllPassageList;
    $(".menu li:eq(1)").addClass("on");
    $("#btnGoToPage").click(function () {
        var p = $("#targetPage").val();
        pageCallback(p - 1);
    });

    var getPassageList = function () {
        var data = {};
        $.ajax({
            url: ajaxUrl,
            type: "get",
            dataType: "json",
            data: { f: "GetPassageList", data: JSON.stringify(data) },
            success: function (jsonData) {
                if (jsonData.success) {
                    AllPassageList = jsonData.result.PassageList;
                    initPage(AllPassageList.length);
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
        $("#passageList ul").empty();
        $("#passageList ul").append($(".tmplPassageList").tmpl(AllPassageList.slice(index * PAGESIZE, CurrentPage * PAGESIZE)));
        $("#targetPage").val(CurrentPage);
        return false;
    };

    getPassageList();
}

//文章编辑页js
var peInit = function () {
    $(".menu li:eq(1)").addClass("on");
    $("#btnSavePassage").on("click", function () {
        var data = {};
        data.Title = $("#txtptitle").val();
        data.Author = "肖斐";
        data.Type = 1;
        var html = $('.pcontent').find('iframe').contents().find('body').html();
        data.content = Util.htmlEncodeByRegExp(html);
        data.PassageId = 0;
        data.Summary = $(".txtSummary").val();
        $.ajax({
            url: ajaxUrl,
            data: { data: JSON.stringify(data), f: "SavePassage" },
            dataType: "json",
            type: "POST",
            success: function () {
                alert("success");
            }

        });
        
    });
    
    //初始化html编辑器
    $("#txtContent").cleditor({height:350});
}

var ppInit = function () {
    $(".menu li:eq(1)").addClass("on");
    var pid = Util.getParam("pid");
    if (!!pid) {
        var data = {};
        data.PassageId = pid;
        $("#txtRPassageId").val(pid);
        $.ajax({
            url: ajaxUrl,
            data: { f: "GetPassage", data: JSON.stringify(data) },
            type: "get",
            dataType: "json",
            success: function (jsonData) {
                if (jsonData != null && jsonData.success == 1) {
                    //加载内容
                    $(".ptitle").html(jsonData.result.passage.Title);
                    var htmlcontent = Util.htmlDecodeByRegExp(jsonData.result.content);
                    $('.pcontent').find('iframe').contents().find('body').html(htmlcontent);
                    var contentheight = $('.pcontent').find('iframe').contents().find('html').height();
                    $('.pcontent').find('iframe').height(contentheight);

                    //加载回复内容
                    var d = jsonData.result.messageList;
                    $("#tmplMessageDepth1").tmpl(d).appendTo(".pcommentList ul");

                    //加载回复面板
                    $("#btnRCommit").on("click", function () {
                        var data = {};
                        data.PassageId = $.trim($("#txtRPassageId").val());
                        data.Message = $.trim($("#txtRContent").val());
                        data.MasterMessageId = $.trim($("#txtRMasterMessageId").val());
                        data.PRankId = $.trim($("#txtRPRank").val());


                        var userdata = {};
                        userdata.Name = $.trim($("#txtRAuthor").val());
                        userdata.Email = $.trim($("#txtREmail").val());
                        userdata.WebAddress = $.trim($("#txtRWeb").val());

                        $.ajax({
                            url: ajaxUrl,
                            type: "post",
                            data: { f: "PostMessage", data: JSON.stringify(data), userdata: JSON.stringify(userdata) },
                            dataType: "json",
                            success: function (jsonData) {
                                if (!!jsonData && jsonData.success == 1) {
                                    alert(jsonData.msg);
                                }
                            }
                        });
                    });
                }
            }
        });
    }

    



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
    },

    initIframe: function (select) {
        //使用插件。。。html编辑器实在是搞不定，一个简单的替换<br />都比想象的要复杂得多
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