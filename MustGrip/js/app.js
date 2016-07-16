


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
    //初始化html编辑器
    var $clEditor = $("#txtContent").cleditor({ height: 350 });

    //初始化内容
    var pid = Util.getParam("pid");
    
    if (pid > 0) {
        $clEditor.getPassage(pid);
    } else {
        pid = 0;
    }
    $("#passagdid").val(pid);

    $(".menu li:eq(1)").addClass("on");
    $("#btnSavePassage").on("click", function () {
        var data = {};
        data.Title = $("#txtptitle").val();
        data.Author = "肖斐";
        data.Type = 1;
        var html = $clEditor[0].$frame.contents().find('body').html();
        data.content = Util.htmlEncodeByRegExp(html);
        data.PassageId = $("#passagdid").val();
        data.Summary = $(".txtSummary").val();
        $.ajax({
            url: ajaxUrl,
            data: { data: JSON.stringify(data), f: "SavePassage" },
            dataType: "json",
            type: "POST",
            success: function (data) {
                alert(data.msg);
            }

        });

    });

    
    
}

//文章查看页js
var ppInit = function () {
    $(".menu li:eq(1)").addClass("on");
    var pid = Util.getParam("pid");

    var replyTo = function() {
        var self = $(this);
        var parent = self.parent();
        var MessageId = parent.find('.RMessageId').val();
        var rank = self.parent().find('.RPRank').val();
        $(".preply").appendTo(parent);
        $(".preply #txtRMasterMessageId").val(MessageId);
        $(".preply #txtRPRank").val(rank);
        $(".preply small").css("display", "block");
    };

    var cancelReply = function() {
        $(".preply small").css("display", "none");
        $(".pcontent").after($(".preply"));
        $(".preply #txtRMasterMessageId").val("0");
        $(".preply #txtRPRank").val("0");
    };

    $(".preply small a").on("click", cancelReply);


    var getMessage= function(pid) {
        var data = {};
        data.PassageId = pid;
        $("#txtRPassageId").val(pid);
        $.ajax({
            url: ajaxUrl,
            data: { f: "GetMessage", data: JSON.stringify(data) },
            type: "get",
            dataType: "json",
            success: function (jsonData) {
                if (jsonData != null && jsonData.success == 1) {
                    //加载回复内容前先把回复面板放回原位，不然就被清空了,并初始化参数
                    cancelReply();
                    $(".preply h3 span").html(jsonData.result.messageList.length);

                    //加载回复内容
                    var d = jsonData.result.messageList;
                    $(".pcommentList").empty();
                    $("#tmplMessageDepth1").tmpl(d).appendTo(".pcommentList");
                    $(".RReplyTo").each(function () {
                        var self = $(this);
                        self.on("click", replyTo);
                        //self.siblings(".RPRank").val();
                    });

                    //加载回复事件
                    $("#btnRCommit").off("click").on("click", function () {
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
                                    getMessage(pid);
                                    alert("回复成功");
                                }
                            }
                        });
                    });
                }
            }
        });
    }

    if (!!pid) {
        $(".txtContent").getPassage(pid);
        getMessage(pid);

        $(".passage-edit a").on("click", function() {
            location.href = "PassageEdit.aspx?pid=" + pid;
        });

    }
}


$.fn.getPassage = function (pid) {
    var $self = $(this);
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
                
                var htmlcontent = Util.htmlDecodeByRegExp(jsonData.result.content);

                if ($self.is("iframe")) {
                    //加载标题，浏览页面
                    $(".ptitle").html(jsonData.result.passage.Title);

                    $self.contents().find('body').html(htmlcontent);
                    //加载所有图片完毕后，重新计算高度
                    var onload = new Util.onLoadFunc($self.contents().find("html img"), function () {
                        var trueHeight = $self.contents().find("html").height();
                        $self.height(trueHeight+30);
                    });
                    onload();
                } else if ($self[0].$area) {
                    //加载标题，编辑页面
                    $("#txtptitle").val(jsonData.result.passage.Title);

                    //加载摘要
                    $(".txtSummary").val(jsonData.result.passage.Summary);

                    //这段只能这么写了，其实下回应该用ckeditor，这个讲究用一下，直接操作它的wraped对象了
                    $self[0].$area.val(htmlcontent);
                    $self[0].updateFrame();
                }
                

            }
        }
    });
    return $self;
};



//图片相关js
var PictureList = function () {
    this.init = function () {
        $(".c_pictures_1").getImageListByType();
        initImgdrag();
        initDeleteImg();
        initNav();
        initSave();
    };

    var HANDLE = "Handle/MustGripPictureHandle.ashx";
    var PICTUREHANDLE = "Handle/UploadHandle.ashx";
    var initImgdrag = function () {
    // 正在拖动的图片的父级DIV 
    var $srcImgDiv = null;

    // 开始拖动 
    $("ul.c_pictures_1").delegate(".img-div img[alt=img]", "dragstart", function () {
        $srcImgDiv = $(this).parent().parent();
        $(this).css("opacity", "0");
    });

    // 拖动到.drop-left,.drop-right上方时触发的事件 
    $("ul.c_pictures_1").delegate("li", "dragover", function (event) {

        // 必须通过event.preventDefault()来设置允许拖放 
        event.preventDefault();
    });

    // 结束拖动放开鼠标的事件 
    $("ul.c_pictures_1").delegate("li", "drop", function (event) {
        event.preventDefault();
        if ($srcImgDiv && $srcImgDiv[0] != $(this)[0]) {
            $(this).before($srcImgDiv);
        }
    });

    $("ul.c_pictures_1").delegate(".img-div img[alt=img]", "dragend", function () {
        $(this).css("opacity", "100");
        $srcImgDiv = null;
    });
};

$.fn.initAddImg = function (type) {
    var self = $(this);

    //判断是否需要初始化控件
    if (self.find(".c_picture_1_a").length==0) {
        self.append('<li>\
            <input type="file" class="c_picture_1_a" name="files[]" multiple style="opacity: 0;z-index: 1;position: absolute;" />\
            <img class="c_picture_1_a" src="resource/img/add.ico" alt="add" style="position: relative;z-index: 2;" />\
            </li>');
        var img = $("img.c_picture_1_a");
        var input = $("input.c_picture_1_a");
        var li = img.parent();
        img.css("width", li.css("width"));
        img.css("height", li.css("height"));

        input.fileupload({
            url:PICTUREHANDLE,
            dataType: "json",
            singleFileUploads: false,
            sequentialUploads: true,
            limitMultiFileUploadSize:100000000,
            done: function (e, data) {
                $.each(data.result.files, function (index, entity) {
                    //移除新增图片控件
                    li.remove();

                    //插入图片
                    self.addImg(type, buildLi(entity));

                });
                //初始化新增图片控件
                self.initAddImg(type);

                //提示异常
                if (data.result.success == 0) {
                    alert(data.result.msg);
                }
            }
        });
        img.on("click", function () {
            return $("input.c_picture_1_a").click();
        });
    }
    
    return self;
}

var buildLi = function (entity) {
    var obj = {
        HostelHotelConfigId: typeof(entity.HostelHotelConfigId) == "undefined" ? "" : entity.HostelHotelConfigId,
        ImageUrl: typeof (entity.ImageUrl) == "undefined" ? "" : entity.ImageUrl,
        Url: typeof (entity.Url) == "undefined" ? "" : entity.Url,
        Type: typeof (entity.Type) == "undefined" ? "" : entity.Type,
        StartDate: typeof (entity.StartDate) == "undefined" ? "" : entity.StartDate,
        EndDate: typeof (entity.EndDate) == "undefined" ? "" : entity.EndDate,
        Title: typeof (entity.Title) == "undefined" ? "" : entity.Title,
        DeputyTitle: typeof (entity.DeputyTitle) == "undefined" ? "" : entity.DeputyTitle,
        Rank: typeof (entity.Rank) == "undefined" ? "" : entity.Rank,
        City: typeof (entity.City) == "undefined" ? "" : entity.City,
        District: typeof (entity.District) == "undefined" ? "" : entity.District,
        Zone: typeof (entity.Zone) == "undefined" ? "" : entity.Zone,
        PlaceName: typeof (entity.PlaceName) == "undefined" ? "" : entity.PlaceName,
        CityDesc: typeof (entity.CityDesc) == "undefined" ? "" : entity.CityDesc,
        Tag: typeof (entity.Tag) == "undefined" ? "" : entity.Tag,
        SubjectId: typeof (entity.SubjectId) == "undefined" ? "" : entity.SubjectId,
        HotelId: typeof (entity.HotelId) == "undefined" ? "" : entity.HotelId,
        HotelName: typeof (entity.HotelName) == "undefined" ? "" : entity.HotelName,
        IsOverseas: typeof (entity.IsOverseas) == "undefined" ? "" : entity.IsOverseas
    };
    return obj;
};

var initDeleteImg = function () {
    $(".c_pictures_1").delegate(".c_picture_1_d", "click", deleteImg);
}

var deleteImg = function () {
    var li = $(this).parent().parent();
    li.remove();

    //初始化新增图片控件
    $(".c_pictures_1").initAddImg($("#hidType").val());
};

var initNav = function () {
    $(".c_nav li").on("click", function () {
        var li = $(this);
        var type = (li.index()+1).toString();
        $("#hidType").val(type);
        var siblings = li.siblings();
        $.each(siblings, function (i, e) {
            $(e).removeClass("c_on");
        });
        li.addClass("c_on");
        
        //类型是1到5的，初始化图片数据和相关控件
        if (type=='1' || type=='2' || type=='3' || type=='4' || type=='5')
        {
            $(".c_pictures_1").getImageListByType();
        }
    });
};

var initSave = function () {
    $("#btnSave").on("click", function () {
        var data = {
            f: "setImageListByType",
            sData: JSON.stringify(getPageInfo()),
            type: $("#hidType").val()
        };
        $.ajax({
            url: HANDLE,
            type: "POST",
            dataType: "json",
            data: data,
            success: function (jsonData) {
                alert(jsonData.msg);
            }
        });
    });
};

var getPageInfo = function () {
    var pageInfo = new Array();
    var type = $("#hidType").val();
    var $list = $(".c_pictures_1>li");


    $.each($list, function (i, e) {
        var $li = $(this);
        if ($li.find(".img-div").length > 0) {
            var p = {};
            p.Rank = $li.index() + 1;
            p.ImageUrl = $li.find("img[alt='img']").attr("src");
            p.HostelHotelConfigId = $li.find(".c_picture_id").val();
            p.Type = type;
            switch (type) {
                case "1":
                    //顶图的第一张，类型和其他图片不同
                    if (i == 0) {
                        p.Url = $li.find(".c_picture_1_url").val();
                        p.StartDate = $li.find(".c_picture_1_startdate").val();
                        p.EndDate = $li.find(".c_picture_1_enddate").val();
                        p.Title = $li.find(".c_picture_1_title").val();
                        p.DeputyTitle = $li.find(".c_picture_1_deputytitil").val();
                    } else {
                        p.Url = $li.find(".c_picture_1_url").val();
                    }
                    break;
                    case "2":
                    //国内目的地
                    p.City = $li.find(".c_picture_2_city").val();
                    p.District = $li.find(".c_picture_2_district").val();
                    p.Zone = $li.find(".c_picture_2_zone").val();
                    p.PlaceName = $li.find(".c_picture_2_placeName").val();
                    p.CityDesc = $li.find(".c_picture_2_cityDesc").val();
                    p.Tag = $li.find(".c_picture_2_tag").val();
                    break;
                    case "3":
                    //海外目的地
                    p.City = $li.find(".c_picture_3_city").val();
                    p.District = $li.find(".c_picture_3_district").val();
                    p.Zone = $li.find(".c_picture_3_zone").val();
                    p.PlaceName = $li.find(".c_picture_3_placeName").val();
                    p.CityDesc = $li.find(".c_picture_3_cityDesc").val();
                    p.Tag = $li.find(".c_picture_3_tag").val();
                    break;
                    case "4":
                    //精选好去处
                    p.SubjectId = $li.find(".c_picture_4_subjectId").val();
                    p.Url = $li.find(".c_picture_4_url").val();
                    p.Title = $li.find(".c_picture_4_title").val();
                    p.DeputyTitle = $li.find(".c_picture_4_deputyTitle").val();
                    break;
                    case "5":
                    //酒店列表
                    p.HotelId = $li.find(".c_picture_5_hotelId").val();
                    
                    break;
                    default:
                    break;
                }

                pageInfo.push(buildLi(p));
            }

        });



    return pageInfo;
};

$.fn.getImageListByType = function () {
    var self = $(this);
    self.empty();
    var type = $("#hidType").val();
    var data = {
        f: "getImageListByType",
        type: type
    };
    $.ajax({
        url: HANDLE,
        type: "POST",
        dataType: "json",
        data: data,
        success: function (jsonData) {
            if (jsonData.success == "1") {

                //初始化图片列表
                $.each(jsonData.result, function (i, e) {
                    self.addImg(e.Type.toString(), buildLi(e));
                });

                //初始化新增图片控件
                self.initAddImg(type);

                //初始化日期控件
                initDate();
            }
        }
    });
    return self;
};

var initDate = function () {
    if ($(".c_picture_1_startdate").length > 0) {
        Util.BindDate("c_picture_1_startdate");
        Util.BindDate("c_picture_1_enddate");
    }
};


$.fn.addImg = function (type, obj) {
    var self = this;
    var li = $("<li></li>");
    var img = $('<img src="' + obj.ImageUrl + '" alt="img" />');
    var imgdel = $('<img src="resource/img/delete.ico" class="c_picture_1_d" alt="delete" />\
            <input type="hidden" class="c_picture_id"  style="display: none;" value="' + obj.HostelHotelConfigId + '" />');
    var divclear = $('<div class="clear"></div>');

    var divl = $('<div class="img-div"></div>');
    var divr = $('<div class="drop-right"></div>');

    divl.append(img)
    .append(imgdel)
    .append(divclear);


    switch (type) {
        case "1":
            if (self.find("input.c_picture_1_startdate").length == 0) {
                divl.append($('<div>\
                    <label>URL链接:</label><input type="text" class="c_picture_1_url" value="' + obj.Url + '" />\
                    </div>\
                    <div>\
                    <label>活动起止时间:</label>\
                    <br />\
                    <input type="text" class="c_picture_1_startdate" value="' + Util.DateHandle(obj.StartDate) + '" /><span>到</span><input type="text" class="c_picture_1_enddate" value="' + Util.DateHandle(obj.EndDate) + '" />\
                    </div>\
                    <div>\
                    <label>主标题:</label><input type="text" class="c_picture_1_title" value="' + obj.Title + '" />\
                    </div>\
                    <div>\
                    <label>副标题:</label><input type="text" class="c_picture_1_deputytitil" value="' + obj.DeputyTitle + '" />\
                    </div>'));

            } else {
                divl.append($('<div>\
                    <label>URL链接:</label><input type="text" class="c_picture_1_url" value="'+ obj.Url + '" />\
                    </div>'));
            }

            break;
        case "2":
            divl.append($('<div>\
                <label>城市ID:</label><input type="text" class="c_picture_2_city" value="' + obj.City + '" />\
                </div>\
                <div>\
                <label>景区ID:</label><input type="text" class="c_picture_2_district" value="'+ obj.District + '" />\
                </div>\
                <div>\
                <label>商业区ID:</label><input type="text" class="c_picture_2_zone" value="'+ obj.Zone + '" />\
                </div>\
                <div>\
                <label>城市名称:</label><input type="text" class="c_picture_2_placeName" value="'+ obj.PlaceName + '" />\
                </div>\
                <div>\
                <label>城市描述:</label><input type="text" class="c_picture_2_cityDesc" value="'+ obj.CityDesc + '" />\
                </div>\
                <div>\
                <label>标签:</label><input type="text" class="c_picture_2_tag" value="'+ obj.Tag + '" />\
                </div>'));
            break;
        case "3":
            divl.append($('<div>\
                <label>城市ID:</label><input type="text" class="c_picture_3_city" value="'+ obj.City + '" />\
                </div>\
                <div>\
                <label>景区ID:</label><input type="text" class="c_picture_3_district" value="'+ obj.District + '" />\
                </div>\
                <div>\
                <label>商业区ID:</label><input type="text" class="c_picture_3_zone" value="'+ obj.Zone + '" />\
                </div>\
                <div>\
                <label>城市名称:</label><input type="text" class="c_picture_3_placeName" value="'+ obj.PlaceName + '" />\
                </div>\
                <div>\
                <label>城市描述:</label><input type="text" class="c_picture_3_cityDesc" value="'+ obj.CityDesc + '" />\
                </div>\
                <div>\
                <label>标签:</label><input type="text" class="c_picture_3_tag" value="'+ obj.Tag + '" />\
                </div>'));
            break;
        case "4":
            divl.append($('<div>\
                <label>专题ID:</label><input type="text" class="c_picture_4_subjectId" value="'+ obj.SubjectId + '" />\
                </div>\
                <div>\
                <label>专题链接:</label><input type="text" class="c_picture_4_url" value="'+ obj.Url + '" />\
                </div>\
                <div>\
                <label>主标题:</label><input type="text" class="c_picture_4_title" value="'+ obj.Title + '" />\
                </div>\
                <div>\
                <label>副标题:</label><input type="text" class="c_picture_4_deputyTitle" value="'+ obj.DeputyTitle + '" />\
                </div>\
                </div>'));
            break;
        case "5":
            divl.append($('<div>\
                <label>酒店ID:</label><input type="text" class="c_picture_5_hotelId" value="'+ obj.HotelId + '" />\
                </div>\
                <div>\
                <label>酒店名称:</label><input type="text" class="c_picture_5_hotelName" value="'+ obj.HotelName + '" />\
                </div>'));
            break;
        default:
            break;
    }
    li.append(divl).append(divr);
    self.append(li);
    return self;
};

var Util = {
    //绑定日期控件
    BindDate: function (id) {
        var idName = "." + id;
        $(idName).datepicker();
        $(idName).datepicker("option", "dateFormat", 'yy-mm-dd');
        $(idName).datepicker("option", "dayNamesMin", ['日', '一', '二', '三', '四', '五', '六']);
        $(idName).datepicker("option", "monthNames", ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']);
        $(idName).datepicker("option", "changeYear", true);
    },
    DateHandle: function (objDate) {
        objDate = new Date(); //创建一个日期对象表示当前时间   
        var year = objDate.getFullYear();   //四位数字年   
        var month = objDate.getMonth() + 1;   //getMonth()返回的月份是从0开始的，还要加1   
        var date = objDate.getDate();
        var date = year + "-" + month + "-" + date;
        return date;
    }

};



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
    },

    //加载完毕后执行回调的函数
    onLoadFunc: function(jq,callback) {
        var t_img; // 定时器
        var isLoad = true; // 控制变量
        var trycount = 10;

        // 判断图片加载的函数
        function isImgLoad() {
            // 查找所有封面图，迭代处理
            jq.each(function () {
                // 找到为0就将isLoad设为false，并退出each
                if (this.height === 0) {
                    isLoad = false;
                    return false;
                }
            });
            // 为true，没有发现为0的，或者加载了一定次数后，加载完毕
            if (isLoad || (trycount--)==0) {
                clearTimeout(t_img); // 清除定时器
                // 回调函数
                callback();
                // 为false，因为找到了没有加载完成的图，将调用定时器递归
            } else {
                isLoad = true;
                t_img = setTimeout(function () {
                    isImgLoad(callback); // 递归扫描
                }, 500); // 500毫秒就扫描一次
            }
        }

        return isImgLoad;

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