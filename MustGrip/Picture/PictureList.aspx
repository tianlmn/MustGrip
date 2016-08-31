<%@ Page Title="" Language="C#" MasterPageFile="~/MustGrip.Master" AutoEventWireup="true" CodeBehind="PictureList.aspx.cs" Inherits="MustGrip.Picture.PictureList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="pic">
        <div class="pic-panel"></div>
        
        <div class="pic-list">
            <ul class="pic-list-p">
                <li></li>
            </ul>
        </div>
        
        
        

    </div>
    <script type="text/x-jquery-tmpl" id="tmpl-piclist">
        <li>
            <img src="${Url}" alt="${Name}" />
        </li>
    </script>
    <script src="../js/jquery-1.12.2.js"></script>
    <script src="../js/jquery.tmpl.min.js"></script>
    <script src="../js/app.js"></script>
    <script>
        PicListInit.init();

    </script>
</asp:Content>






























