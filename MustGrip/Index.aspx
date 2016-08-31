<%@ Page Title="" Language="C#" MasterPageFile="./MustGrip.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Mustgrip.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="clear">
        <div class="intro">
            <div class="introcontainer">
                <img src="resource/img/nav_1.jpg" width="800px"></img>
                <img src="resource/img/nav_2.jpg" width="800px"></img>
                <img src="resource/img/nav_3.jpg" width="800px"></img>
                <img src="resource/img/nav_4.jpg" width="800px"></img>
                <img src="resource/img/nav_5.jpg" width="800px"></img>
                <img src="resource/img/nav_6.jpg" width="800px"></img>
            </div>
            <div class="intronav">
                <ul>
                    <li class="on"><a href="javascript:void(0)">1</a></li>
                    <li><a href="javascript:void(0)">2</a></li>
                    <li><a href="javascript:void(0)">3</a></li>
                    <li><a href="javascript:void(0)">4</a></li>
                    <li><a href="javascript:void(0)">5</a></li>
                    <li><a href="javascript:void(0)">6</a></li>
                </ul>
            </div>
        </div>
    </div>
    <div id="footer" class="clear">
        <p>
            Copyright© 2016-2016 肖斐<br />
            <a target="_blank" href="http://www.miitbeian.gov.cn/">苏ICP备16013907号</a>
        </p>
    </div>
    <script src="js/jquery-1.12.2.js"></script>
    <script src="js/app.js"></script>
    <script>
        indexInit();
    </script>
</asp:Content>
