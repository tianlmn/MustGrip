<%@ Page Title="" Language="C#" MasterPageFile="../MustGrip.Master" AutoEventWireup="true" CodeBehind="Passage.aspx.cs" Inherits="MustGrip.Passage.Passage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="clear">
        <div id="passage">
            <div class="ptitle"></div>
            <div class="pcontent">
                <iframe class="txtContent" id="xxx"></iframe>
                <div>
                    <input type="hidden" id="txtRPassageId" />
                </div>
            </div>
            <div class="passage-edit"><a href="###" type="button">编辑</a></div>
            <div class="preply">
                <h3>发表评论:(目前<span></span>条评论)</h3>
                <small><a href="###">点击这里取消评论</a></small>
                <p>
                    <input type="text" id="txtRAuthor" />
                    <label for="txtRAuthor">名称(<span style="color: red">*</span>)</label>
                </p>
                <p>
                    <input type="text" id="txtREmail" />
                    <label for="txtREmail">邮箱(不会被公开)(<span style="color: red">*</span>)</label>
                </p>
                <p>
                    <input type="text" id="txtRWeb" />
                    <label for="txtRWeb">网站(选填)</label>
                </p>
                <p>
                    <textarea id="txtRContent" cols="100%" rows="10"></textarea>
                    <label for="txtRContent">评论</label>
                </p>
                <p>
                    <input type="button" value="提交" id="btnRCommit" />
                    <input type="hidden" id="txtRMasterMessageId" value="0" />
                    <input type="hidden" id="txtRPRank" value="0" />
                </p>
            </div>
            <div>
                <ol class="pcommentList">
                </ol>
            </div>

            
        </div>

    </div>
    <div id="footer" class="clear">
        <p>
            Copyright© 2016-2016 肖斐<br />
            <a target="_blank" href="http://www.miitbeian.gov.cn/">苏ICP备16013907号</a>
        </p>
    </div>
    <script type="text/x-jquery-tmpl" id="tmplMessageDepth1">
        <li class="depth1">
            <div>
                <img src="#" alt="xxx" />
                <span><a href="http://${WebAddress}">${Name}</a></span><span> 说道:</span>
            </div>
            <p class="comment_time">${CreateTime}</p>
            <p class="comment_message">${Message}</p>
            <div>
                <a href="###" class="RReplyTo">回复:</a>
                <input type="hidden" class="RMessageId" value="${BgMessageId}" />
                <input type="hidden" class="RPRank" value="${MaxRankId}" />
            </div>
            <ul>
                {{tmpl($data.ChildList) "#tmplMessageDepth2"}}
            </ul>
        </li>
    </script>
    <script type="text/x-jquery-tmpl" id="tmplMessageDepth2">
        <li class="depth2">
            <div>
                <img src="#" alt="xxx" />
                <span><a href="http://${WebAddress}">${Name}</a></span><span> 说道:</span>
            </div>
            <p class="comment_time">${CreateTime}</p>
            <p class="comment_message">${Message}</p>
            <div>
                <a href="###" class="RReplyTo">回复:</a>
                <input type="hidden" class="RMessageId" value="${BgMessageId}" />
                <input type="hidden" class="RPRank" value="${MaxRankId}" />
            </div>
            <ul>
                {{tmpl($data.ChildList) "#tmplMessageDepth3"}}
            </ul>
        </li>
    </script>
    <script type="text/x-jquery-tmpl" id="tmplMessageDepth3">
        <li class="depth3">
            <div>
                <img src="#" alt="xxx" />
                <span><a href="http://${WebAddress}">${Name}</a></span><span> 说道:</span>
            </div>
            <p class="comment_time">${CreateTime}</p>
            <p class="comment_message">${Message}</p>
            <div>
                <a href="###" class="RReplyTo">回复:</a>
                <input type="hidden" class="RMessageId" value="${BgMessageId}" />
                <input type="hidden" class="RPRank" value="${MaxRankId}" />
            </div>
            <ul>
                {{tmpl($data.ChildList) "#tmplMessageDepth4"}}
            </ul>
        </li>
    </script>
    <script type="text/x-jquery-tmpl" id="tmplMessageDepth4">
        <li class="depth4">
            <div>
                <img src="#" alt="xxx" />
                <span><a href="http://${WebAddress}">${Name}</a></span><span> 说道:</span>
            </div>
            <p class="comment_time">${CreateTime}</p>
            <p class="comment_message">${Message}</p>
            <div>
                <a href="###" class="RReplyTo">回复:</a>
                <input type="hidden" class="RMessageId" value="${BgMessageId}" />
                <input type="hidden" class="RPRank" value="${MaxRankId}" />
            </div>
            <ul>
                {{tmpl($data.ChildList) "#tmplMessageDepth5"}}
            </ul>
        </li>
    </script>
    <script type="text/x-jquery-tmpl" id="tmplMessageDepth5">
        <li class="depth5">
            <div>
                <img src="#" alt="xxx" />
                <span><a href="http://${WebAddress}">${Name}</a></span><span> 说道:</span>
            </div>
            <p class="comment_time">${CreateTime}</p>
            <p class="comment_message">${Message}</p>
            <div>
                <a href="###" class="RReplyTo">回复:</a>
                <input type="hidden" class="RMessageId" value="${BgMessageId}" />
                <input type="hidden" class="RPRank" value="${MaxRankId}" />
            </div>
            <ul>
                {{tmpl($data.ChildList) "#tmplMessageDepth6"}}
            </ul>
        </li>
    </script>
    <script type="text/x-jquery-tmpl" id="tmplMessageDepth6">
        <li class="depth6">
            <div>
                <img src="#" alt="xxx" />
                <span><a href="http://${WebAddress}">${Name}</a></span><span> 说道:</span>
            </div>
            <p class="comment_time">${CreateTime}</p>
            <p class="comment_message">${Message}</p>
        </li>
    </script>

    <script src="../js/jquery-1.12.2.js"></script>
    <script src="../js/jquery.tmpl.min.js"></script>
    <script src="../js/app.js"></script>
    <script>
        ppInit();
    </script>
</asp:Content>
