<%@ Page Title="" Language="C#" MasterPageFile="~/MustGrip.Master" AutoEventWireup="true" CodeBehind="Passage.aspx.cs" Inherits="MustGrip.Passage.Passage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="clear">
        <div id="passage">
            <div class="ptitle"></div>
            <div class="pcontent">
                <iframe class="txtContent"></iframe>
            </div>
            <div class="pcommentList">
            </div>
            <div class="preply">
            </div>
        </div>
    </div>
    <div id="footer" class="clear">
        <p>
            Copyright© 2016-2016 肖斐<br />
            <a target="_blank" href="http://www.miitbeian.gov.cn/">苏ICP备16013907号</a>
        </p>
    </div>
    <script src="../js/jquery-1.12.2.js"></script>
    <script src="../js/app.js"></script>
    <script>
        ppInit();
    </script>
</asp:Content>
