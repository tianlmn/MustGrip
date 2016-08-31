<%@ Page Title="" Language="C#" MasterPageFile="../MustGrip.Master" AutoEventWireup="true" CodeBehind="PassageEdit.aspx.cs" Inherits="MustGrip.Passage.PassageEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div id="main" class="clear">
		<div id="passageEdit">
			<div class="ptitle"><span>标题：</span><input type="text" id="txtptitle" /></div>
			<div class="pcontent"><textarea id="txtContent"></textarea></div>
            <p>摘要：</p>
            <div><textarea class="txtSummary"></textarea></div>
            <div><input type="button" value="发表" id="btnSavePassage" /></div>
            <input type="hidden" id="passagdid" value="" />
		</div>
	</div>
    
    <script src="../js/jquery-1.12.2.js"></script>
    <script src="../Plugin/CLEditor1_4_5/jquery.cleditor.js"></script>
    <script src="../js/app.js"></script>

    <script>
        peInit();
    </script>
</asp:Content>