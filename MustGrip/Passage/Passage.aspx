<%@ Page Title="" Language="C#" MasterPageFile="~/MustGrip.Master" AutoEventWireup="true" CodeBehind="Passage.aspx.cs" Inherits="MustGrip.Passage.Passage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="clear">
        <div id="passage">
            <div class="ptitle"></div>
            <div class="pcontent">
                <iframe class="txtContent"></iframe>
                <div>
                    <input type="hidden" id="txtRPassageId" />
                </div>
            </div>
            <div class="pcommentList">
                <ul>
                    
                </ul>
            </div>
            <div class="preply">
                <h3>发表评论:(目前n条评论)</h3>
                <p>
                    <input type="text" id="txtRAuthor" />
                    <label for="txtRAuthor"><span style="color: red">*</span></label>
                </p>
                <p>
                    <input type="text" id="txtREmail" />
                    <label for="txtREmail">(<span style="color: red">*</span>)</label>
                </p>
                <p>
                    <input type="text" id="txtRWeb" />
                    <label for="txtRWeb">你的网站(选填)</label>
                </p>
                <p>
                    <textarea id="txtRContent" cols="100%" rows="10" > </textarea>
                    <label for="txtRContent">评论</label>
                </p>
                <p>
                    <input type="button" value="提交" id="btnRCommit" />
                    <input type="hidden" id="txtRMasterMessageId" value="0" />
                    <input type="hidden" id="txtRPRank" value="0" />
                </p>
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
                <span>${Author}</span><span>说道:</span>
            </div>
            <div>
                <span>${CreateTime}</span>
            </div>
            <div>
                <p>${Message}</p>
            </div>
            <div>
                <span><a href="###">回复:</a></span>
            </div>
<%--            <ul>
                {{tmpl($data.ReplyList) "#tmplMessageDepth2"}}
            </ul>--%>
        </li>
    </script>
    <script type="text/x-jquery-tmpl" id="tmplMessageDepth2">
        <li class="depth2">
            <div>
                <img src="#" alt="xxx" />
                <span>${Author}</span><span>说道:</span>
            </div>
            <div>
                <span>${CreateTime}</span>
            </div>
            <div>
                <p>${Message}</p>
            </div>
            <div>
                <span><a href="###">回复:</a></span>
            </div>
            <ul>
                {{tmpl($data.ReplyList) "#tmplMessageDepth3"}}
            </ul>
        </li>
    </script>
    <script type="text/x-jquery-tmpl" id="tmplMessageDepth3">
        <li class="depth3">
            <div>
                <img src="#" alt="xxx" />
                <span>${Author}</span><span>说道:</span>
            </div>
            <div>
                <span>${CreateTime}</span>
            </div>
            <div>
                <p>${Message}</p>
            </div>
            <div>
                <span><a href="###">回复:</a></span>
            </div>
        </li>
    </script>

    <script src="../js/jquery-1.12.2.js"></script>
    <script src="../js/jquery.tmpl.min.js"></script>
    <script src="../js/app.js"></script>
    <script>
        ppInit();
    </script>
</asp:Content>
