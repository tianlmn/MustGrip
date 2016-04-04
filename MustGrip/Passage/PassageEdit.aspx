<%@ Page Title="" Language="C#" MasterPageFile="~/MustGrip.Master" AutoEventWireup="true" CodeBehind="PassageEdit.aspx.cs" Inherits="MustGrip.Passage.PassageEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="clear">
		<div class="passageEdit">
			<div class="ptitle"><span>标题：</span><input type="text" id="txtptitle" /></div>
            <div class="ptool"><input type="button" id="btnInsertImage" value="插入网络图片"><input type="text" class="txtppath" value="http://qlogo2.store.qq.com/qzone/452120205/452120205/100?1325487627"></div>
			<div class="pcontent"><iframe type="text" id="txtpcontent"></iframe>></div>
            <div><input type="button" value="发表" id="btnSavePassage" /></div>
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
        peInit();
    </script>
</asp:Content>