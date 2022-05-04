<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExecDashboard.aspx.cs" Inherits="ExecDashboard" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html>
<html lang="en-us">
<head>
    <title>Executive Dashboard</title>
<style type="text/css">
        body {
            background: #42410C;
            margin: 0;
            padding: 0;
            color: #000;
            font-weight: bold;
            background-color: #FFFFFF;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 1em;
            line-height: normal;
            font-variant: normal;
            text-transform: none;
        }

        ul, ol, dl {
            padding: 0;
            margin: 0;
            font-variant: normal;
            text-transform: none;
        }

        .container .content p {
            font-size: 16px;
            font-family: "Arial Narrow", Arial, Helvetica, sans-serif;
            font-variant: normal;
            text-transform: none;
        }

        h1, h2, h3, h4, h5, h6, p {
            margin-top: 0;
            padding-right: 15px;
            padding-left: 15px;
            color: #000;
            font-size: 16px;
            font-family: "Arial Narrow", Arial, Helvetica, sans-serif;
            font-variant: normal;
            text-transform: none;
        }

        a img {
            border: none;
        }

        a:link {
            color: #005695;
            text-decoration: none;
			font-family: "Arial Narrow", Arial, Helvetica, sans-serif;
        }

        a:visited {
            color: #005695;
            text-decoration: none;
			font-family: "Arial Narrow", Arial, Helvetica, sans-serif;
        }

        a:hover, a:active, a:focus {
            text-decoration: none;
            color: #3399ff;
            font-size: 14px;
            font-family: "Arial Narrow", Arial, Helvetica, sans-serif;
        }

        .container {
            width: 99%;
            background: #FFFFFF;
            margin: 0 auto;
        }

        header {
            background-color: #005695;
        }


        .sidebar1 {
            float: left;
            width: 180px;
            background: #EADCAE;
            padding-bottom: 10px;
        }

        .content {
            width: 100%;
            float: none;
            padding-top: 10px;
            padding-right: 0;
            padding-bottom: 10px;
            padding-left: 0;
            color: #005695;
            font-size: 18px;
        }

        aside {
            float: left;
            width: 180px;
            background: #EADCAE;
            padding: 10px 0;
        }


        .content ul, .content ol {
            padding: 0 15px 15px 40px;
        }


        nav ul {
            list-style: none;
            border-top: 1px solid #666;
            margin-bottom: 15px;
        }

            nav ul li {
                border-bottom: 1px solid #666;
            }

            nav ul a, nav ul a:visited {
                padding: 5px 5px 5px 15px;
                display: block;
                width: 160px;
                text-decoration: none;
                background: #C6D580;
            }

                nav ul a:hover, nav ul a:active, nav ul a:focus {
                    background: #ADB96E;
                    color: #FFF;
                }

        footer {
            padding: 10px 0;
            position: relative;
            clear: both;
            background-color: #99CCFF;
            color: #fff;
            text-decoration: none;
            a: link text decoration: none;
            color: #ffffff;
            a: visited color:#ffffff;
            a: hover color:#ffffff;
        }

        .fltrt {
            float: right;
            margin-left: 8px;
        }

        .fltlft {
            float: left;
            margin-right: 8px;
        }

        .clearfloat {
            clear: both;
            height: 0;
            font-size: 1px;
            line-height: 0px;
        }


        header, section, footer, aside, nav, article, figure {
            display: block;
            font-size: 12px;
        }

        #links {
            font-family: "Arial Narrow", Arial, Helvetica, sans-serif;
            font-size: 16px;
            font-style: normal;
            font-weight: normal;
            font-variant: normal;
            color: #005695;
            text-decoration: none;
            background-color: #FFF;
            padding: 3px;
            border-top-width: 1px;
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
            border-top-style: solid;
            border-right-style: solid;
            border-bottom-style: solid;
            border-left-style: solid;
            position: relative;
        }

        .container .content section table tr td div p img {
            font-family: "Arial Narrow", Arial, Helvetica, sans-serif;
        }

        .container .content section table tr td div {
            font-family: "Arial Narrow", Arial, Helvetica, sans-serif;
        }

        .container footer p {
            color: #FFF;
            a: link=#fff;
            a: visited=#fff;
            a: hover=#fff;
        }

        .container {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 16px;
            font-style: normal;
            line-height: normal;
            font-weight: bold;
            font-variant: small-caps;
            color: #FFF;
            text-decoration: none;
        }

        body, td, th {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 16px;
        }

        a {
            font-family: Arial, Helvetica, sans-serif;
        }

        h1 {
            font-size: 18px;
            color: #FFF;
        }

        h2 {
            font-size: 18px;
        }

        h4 {
            font-size: 14px;
        }

        h5 {
            font-size: 12px;
        }

        h6 {
            font-size: 10px;
            color: #FFF;
        }
    </style>

</head>
<body link="#005695" vlink="#3399ff" alink="#3399ff">
    <form id="form1" runat="server">
        <div class="container">
            <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
            </asp:ScriptManager>
            <header></header>

          
            <h1 align="center"><h5 align="center">Powered by:&nbsp;&nbsp;
                <a href="#">
                    <img src="escworks_logo_lrg.png" alt="escWorks Logo" width="110" height="28" align="absmiddle"></a></h5></h1>
            <h4 align="center"><a href="escWorks Executive Dashboard Guide ver2.pdf" title=" How To Use the Dashboard " target="_new">How to Use the Dashboard  </a>    </span></h4>
			

               <iframe frameborder="0" width="99.7%" height="900px" id="iframe1" 
                         src="https://qlik.escworks.app/sense/app/bc59defc-9e0e-4bc6-9802-76560c98bd42">
                  </iframe>
                </h2>
                <hr width="90%"/>
                 <h5>
             Copyright � 2014 -
<script language="JavaScript" type="text/javascript">
    now = new Date
    theYear = now.getYear()
    if (theYear < 1900)
        theYear = theYear + 1900
    document.write(theYear)
</script>
escWorks.NET<sup>�</sup> All Rights Reserved
        </h5>
               
           

        </div>
    </form>
</body>
</html>
