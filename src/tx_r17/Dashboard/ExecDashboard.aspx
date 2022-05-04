<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExecDashboard.aspx.cs" Inherits="ExecDashboard" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html>
<html lang="en-us">
<head>
    <title>Executive Dashboard</title>
    <style type="text/css">
        body {
            background: #42413C;
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
            font-family: Arial, Helvetica, sans-serif;
            font-variant: normal;
            text-transform: none;
        }

        h1, h2, h3, h4, h5, h6, p {
            margin-top: 0;
            padding-right: 15px;
            padding-left: 15px;
            color: #000;
            font-size: 16px;
            font-family: Arial, Helvetica, sans-serif;
            font-variant: normal;
            text-transform: none;
        }

        a img {
            border: none;
        }

        a:link {
            color: #005695;
            text-decoration: none;
        }

        a:visited {
            color: #005695;
            text-decoration: none;
        }

        a:hover, a:active, a:focus {
            text-decoration: none;
            color: #3399ff;
            font-size: 16px;
            font-family: Arial, Helvetica, sans-serif;
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
            font-family: Arial, Helvetica, sans-serif;
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
            font-family: Arial, Helvetica, sans-serif;
        }

        .container .content section table tr td div {
            font-family: Arial, Helvetica, sans-serif;
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
        .rtsSelected, .rtsSelected span {
            background: url(../Images/btnUpdate.jpg) no-repeat 0 100% !important;
            background-color: dimgray !important;
            text-align: center;
            color: white;
        }

        .RadTabStrip_Telerik li a.slected {
            background: url(../Images/btnUpdate.jpg) no-repeat 0 100% !important;
            background-color: dimgray !important;
            text-align: center;
            color: white;
        }
    </style>
   
    <script type="text/javascript">
        function OnClientItemSelected(sender, eventArgs) {

            var item = eventArgs.get_item();
            var value = item.get_value();
            if (value == "2016-2017") {
                top.document.getElementById('iframe1').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/57f3eea88051f4b0f20002b8?folder=57d988dbdc3717b414000072&embed=true&r=false&l=false");
                top.document.getElementById('iframe2').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/57f3ef578051f4b0f2000374?folder=57d988e4dc3717b414000074&embed=true&r=false&l=false");
                top.document.getElementById('iframe3').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/57f3ea418051f4b0f20001a8?folder=57d98901dc3717b414000078&embed=true&r=false&l=false");
                top.document.getElementById('iframe4').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/57f3ecaf8051f4b0f2000268?folder=57d98901dc3717b414000078&embed=true&l=false&r=false");
                top.document.getElementById('iframe5').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/56d99330586e5108140003ae?folder=56d98ab3586e510814000396&embed=true&l=false");
            }
            else if (value == "2014-2015") {
                top.document.getElementById('iframe1').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/56d66b4c05bc0b541a000704?folder=56cc7ee16735627c8c000140&embed=true&l=false&r=false");
                top.document.getElementById('iframe2').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/56d66e4405bc0b541a0007d8?folder=56cc7ee16735627c8c000140&embed=true&l=false&r=false");
                top.document.getElementById('iframe3').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/56ce2d61d2e4331c0f000e7b?folder=56cc7fe96735627c8c000144&embed=true&l=false&r=false");
                top.document.getElementById('iframe4').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/56ce2db4d2e4331c0f000ed6?folder=56cc7fe96735627c8c000144&embed=true&l=false&r=false");
                top.document.getElementById('iframe5').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/56d99330586e5108140003ae?folder=56d98ab3586e510814000396&embed=true&l=false");
            }
            else if (value == "2015-2016") {
                top.document.getElementById('iframe1').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/56ce2d0ad2e4331c0f000dec?folder=56cc7f806735627c8c000142&embed=true&l=false&r=false");
                top.document.getElementById('iframe2').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/56ce2d37d2e4331c0f000e48?folder=56cc7f806735627c8c000142&embed=true&l=false&r=false");
                top.document.getElementById('iframe3').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/56ce2d61d2e4331c0f000e7b?folder=56cc7fe96735627c8c000144&embed=true&l=false&r=false");
                top.document.getElementById('iframe4').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/56ce2db4d2e4331c0f000ed6?folder=56cc7fe96735627c8c000144&embed=true&l=false&r=false");
                top.document.getElementById('iframe5').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/56d99330586e5108140003ae?folder=56d98ab3586e510814000396&embed=true&l=false");
            }
        }

        function OnTabSelected(sender, args) {
            var hidField = document.getElementById("<%=hiddenFieldTabValue.ClientID%>");
            hidField.value = args.get_tab().get_value();
        }	
    </script>
</head>
<body link="#005695" vlink="#3399ff" alink="#3399ff">
    <form id="form1" runat="server">
        <div class="container">
            <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
            </asp:ScriptManager>
            <header>
                <br />
                <br />
                <h1 align="center"><em>Welcome to the Region 17 Executive Dashboard</em></h1>
                <p align="center">&nbsp;</p>
            </header>
            <h5 align="center">Powered by:</h5>
            <h1 align="center">
                <a href="#">
                    <img src="escworks_logo_lrg.png" alt="escWorks logo" width="198" height="42" align="absmiddle"></a></h1>
            <h1 align="center">
                <telerik:RadDropDownList runat="server" ID="FiscalYear" OnClientItemSelected="OnClientItemSelected"
                    Width="240">
                    <Items>
                        <telerik:DropDownListItem Value="2016-2017" Text="September 1, 2016 - August 31, 2017" />
                        <telerik:DropDownListItem Value="2015-2016" Text="September 1, 2015 - August 31, 2016" />
                        <telerik:DropDownListItem Value="2014-2015" Text="September 1, 2014 - August 31, 2015" />
                    </Items>
                </telerik:RadDropDownList>
                &nbsp;</h1>
            <font color="white">Hold down Shift and M key to bring into focus anytime even in the event focus is lost and then navigate through arrow keys and hit enter.</font>
            <br /><br />
            <telerik:RadTabStrip ID="radTabStrip" AutoPostBack="true" runat="server" MultiPageID="RadMultiPage1"
                SelectedIndex="0" OnClientTabSelected="OnTabSelected" RenderMode="Lightweight">
                <KeyboardNavigationSettings CommandKey="Shift" FocusKey="M" />
                <Tabs>
                    <telerik:RadTab runat="server" Text="About the Executive Dashboard" Value="About the Executive Dashboard"
                        PostBack="false">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Professional Development" Value="Professional Development"
                        PostBack="false">
                    </telerik:RadTab>

                    <telerik:RadTab runat="server" Text="Technical Assistance" Value="Technical Assistance"
                        PostBack="false">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="PD Comparisons" Value="PD Comparisons"
                        PostBack="false">
                    </telerik:RadTab>

                    <telerik:RadTab runat="server" Text="TA Comparisons" Value="TA Comparisons"
                        PostBack="false">
                    </telerik:RadTab>

                    <telerik:RadTab runat="server" Text="COOPs" Value="COOPs"
                        PostBack="false">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <asp:HiddenField ID="hiddenFieldTabValue" runat="server" Value="About the Executive Dashboard" />
            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
                <telerik:RadPageView runat="server" ID="RadPageView1">
                    <section>
                        <h2 align="center">
                            <p></p>
                            About the Executive Dashboard...</h2>
                        <h4 align="center"><em>Executive Dashboard requires an HTML 5 compatible browser such as Chrome (the preferred choice), Firefox, or Internet Explorer 10 or higher</em></h4>
                        <table width="100%" border="0" cellspacing="2" cellpadding="0" summary="Links to User Guide, Data Description documents">
                            <caption>
                                &nbsp;
                            </caption>
                            <tr>
                                <th scope="col">
                                    <p>
                                        <img src="documentation.png" width="38" height="49" alt="Data Sources"></p>
                                    <p><a href="escWorks Executive Dashboard Guide.pdf" title="How to Use the Dashboard" target="_new">• How to Use the Dashboard</a></p>
                                    <p></p>
                                    <p></p>
                                    <p></p>
                                </th>


                            </tr>
                        </table>
                        <hr />
                    </section>
                    <h2 align="center">About the Data...</h2>
                    <h5 align="center">The data within the escWorks Dashboard is updated on a nightly basis. All data should
                    be current as of 5:00 AM the day of viewing.</h5>
                    <hr>
                    </section>
                <section>
                </section>
                    <section>
                        <h2 align="center">Helpful Information...</h2>
                        <h5 align="center">Please note: Revisions to the escWorks Dashboard will be made in order to better inform our customers.</h5>
                        <h3 align="center">Have a question? Contact us at: <span style="color: white; link color: white; visited color: white; hover color: white; active color: black"><a href="mailto:helpdesk@esc4.net">helpdesk@esc4.net</a></span></h3>
                    </section>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView2" Height="700px" runat="server">

                    <h2>
                        <p></p>
                        Professional Development - </h2>


                    <iframe frameborder="1" scrolling="yes" width="99.7%" height="700px" radius-r="5px" id="iframe1"
                        src="https://dashboard.escworks.net/app/main#/dashboards/57f3eea88051f4b0f20002b8?folder=57d988dbdc3717b414000072&embed=true&r=false&l=false"></iframe>

                    <hr width="90%" />
                    <h5>Copyright © 2014-16 escWorks.NET<sup>®</sup> All Rights Reserved
                    </h5>

                </telerik:RadPageView>

                <telerik:RadPageView ID="RadPageView3" Height="700px" runat="server">

                    <h2>
                        <p></p>
                        Technical Assistance - </h2>


                    <iframe frameborder="1" scrolling="yes" width="99.7%" height="700px" radius-r="5px" id="iframe2"
                        src="https://dashboard.escworks.net/app/main#/dashboards/57f3ef578051f4b0f2000374?folder=57d988e4dc3717b414000074&embed=true&r=false&l=false"></iframe>

                    <hr width="90%" />
                    <h5>Copyright © 2014-16 escWorks.NET<sup>®</sup> All Rights Reserved
                    </h5>

                </telerik:RadPageView>

                <telerik:RadPageView ID="RadPageView4" Height="700px" runat="server">

                    <h2>
                        <p></p>
                        PD Comparison - </h2>


                    <iframe frameborder="1" scrolling="yes" width="99.7%" height="700px" radius-r="5px" id="iframe3"
                        src="https://dashboard.escworks.net/app/main#/dashboards/57f3ea418051f4b0f20001a8?folder=57d98901dc3717b414000078&embed=true&r=false&l=false"></iframe>

                    <hr width="90%" />
                    <h5>Copyright © 2014-16 escWorks.NET<sup>®</sup> All Rights Reserved
                    </h5>

                </telerik:RadPageView>

                <telerik:RadPageView ID="RadPageView5" Height="700px" runat="server">

                    <h2>
                        <p></p>
                        TA Comparison - </h2>


                    <iframe frameborder="1" scrolling="yes" width="99.7%" height="700px" radius-r="5px" id="iframe4"
                        src="https://dashboard.escworks.net/app/main#/dashboards/57f3ecaf8051f4b0f2000268?folder=57d98901dc3717b414000078&embed=true&l=false&r=false"></iframe>

                    <hr width="90%" />
                    <h5>Copyright © 2014-16 escWorks.NET<sup>®</sup> All Rights Reserved
                    </h5>

                </telerik:RadPageView>

                <telerik:RadPageView ID="RadPageView6" Height="700px" runat="server">

                    <h2>
                        <p></p>
                        COOPs - </h2>


                    <iframe frameborder="1" scrolling="yes" width="99.7%" height="700px" radius-r="5px" id="iframe5"
                        src="https://dashboard.escworks.net/app/main#/dashboards/56d99330586e5108140003ae?folder=56d98ab3586e510814000396&embed=true&l=false"></iframe>

                    <hr width="90%" />
                    <h5>Copyright © 2014-16 escWorks.NET<sup>®</sup> All Rights Reserved
                    </h5>

                </telerik:RadPageView>
            </telerik:RadMultiPage>

        </div>
    </form>
</body>
</html>
