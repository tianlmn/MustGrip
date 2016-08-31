<%@ Page Title="" Language="C#" MasterPageFile="../MustGrip.Master" AutoEventWireup="true" CodeBehind="PassageList.aspx.cs" Inherits="MustGrip.Passage.PassageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div id="main" class="clear">
        <div>
            <a href="/Passage/PassageEdit.aspx" type="button">新增</a>
        </div>
        <div id="passageList">
            <ul>
            </ul>
            <div class="pagination" style="float: right; margin-top: 10px;">
                <span id="Pagination"></span>
                <span id="pageCount"></span><span>到第</span>
                <input type="text" id="targetPage" class="text-small" onkeyup="value=value.replace(/[^\d]/g,'');" />
                <span>页</span>
                <input type="button" id="btnGoToPage" value="确定" />
            </div>
        </div>
    </div>
    <script type="text/x-jquery-tmpl" class="tmplPassageList">
        <li>
            <div class="lihead">${CreateTime}</div>
            <div class="libody break">
                <h2>${Title}</h2>
                <small>作者:${Author}</small>
                <div>
                    <h3>${Summary}</h3>
                    <h4><a href="Passage.aspx?pid=${PassageId}">阅读全文</a></h4>
                </div>
            </div>
        </li>
    </script>

    <script src="../js/jquery-1.12.2.js"></script>
    <script src="../js/jquery.pagination.js"></script>
    <script src="../js/jquery.tmpl.min.js"></script>
    <script src="../js/app.js"></script>

    <script>
        plInit();
    </script>
</asp:Content>
