<%@ Page Title="" Language="C#" MasterPageFile="~/MustGrip.Master" AutoEventWireup="true" CodeBehind="PassageList.aspx.cs" Inherits="MustGrip.Passage.PassageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="clear">
        <div>
            <a href="/Passage/PassageEdit.aspx" type="button">新增</a>
        </div>
        <div class="passageList">
            <ul>
            </ul>
        </div>
        <div class="pagination" style="float: right; margin-top: 10px;">
        <span id="Pagination"></span>
        <span id="pageCount"></span><span>到第</span>
        <input type="text" id="targetPage" class="text-small" onkeyup="value=value.replace(/[^\d]/g,'');" />
        <span>页</span>
        <input type="button" id="btnGoToPage" value="确定" />
    </div>
    </div>
    <div id="footer" class="clear">
        <p>
            Copyright© 2016-2016 肖斐<br />
            <a target="_blank" href="http://www.miitbeian.gov.cn/">苏ICP备16013907号</a>
        </p>
    </div>

    <script type="text/x-jquery-tmpl" class="tmplPassageList">
        <li>
            <div class="lihead">${CreateTime}</div>
            <div class="libody break">
                <h2>${PassageName}</h2>
                <small>${Author}</small>
                <div>
                    <p>${Summary}</p>
                    <p><a href="###">阅读全文</a></p>
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
