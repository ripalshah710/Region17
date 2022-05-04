<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TexasDashboard.aspx.cs" Inherits="TexasDashboard" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html>
<html lang="en-us">
<head>
    <title>Texas Dashboard</title>
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

                top.document.getElementById('iframe1').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/57ebf76cdcd5548018000223?folder=57d86f12566352546d0003ca&embed=True&r=False&l=false");
                top.document.getElementById('iframe2').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/57ebf8fddcd554801800029d?folder=57d86f12566352546d0003ca&embed=True&r=false&l=false");
                top.document.getElementById('iframe4').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/57ebf9e5dcd554801800033b?folder=57d86f12566352546d0003ca&embed=True&l=false");
            }
            else if (value == "2015-2016") {
                top.document.getElementById('iframe1').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/560d5c8ce5bdead008001050?folder=560c4c8490c0666833000870&embed=true&r=False&l=false");
                top.document.getElementById('iframe2').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/5611a936e892eb581f000397?folder=560c4c8490c0666833000870&embed=true&r=False&l=false");
                top.document.getElementById('iframe4').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/5611ab3ce892eb581f000439?folder=560c4c8490c0666833000870&embed=true&l=false");
            }
            else if (value == "2014-2015") {

                top.document.getElementById('iframe1').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/5565e219f27d90f42c000011?folder=5565e205f27d90f42c00000f&embed=true&r=False&l=false");
                top.document.getElementById('iframe2').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/5565e351f27d90f42c000058?folder=5565e205f27d90f42c00000f&embed=true&r=False&l=false");
                top.document.getElementById('iframe4').setAttribute("src", "https://dashboard.escworks.net/app/main#/dashboards/5565e373f27d90f42c00009d?folder=5565e205f27d90f42c00000f&embed=true&l=false");
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
                <h1 align="center"><em>Welcome to the Texas Dashboard</em></h1>
                <p align="center">&nbsp;</p>
            </header>
            <h5 align="center">Powered by:</h5>
            <h1 align="center">
                <a href="#">
                    <img src="escworks_logo_lrg.png" alt="escWorks Logo" width="198" height="42" align="absmiddle"></a></h1>
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
                    <telerik:RadTab runat="server" Text="About the TEXAS DASHBOARD" Value="AboutTheDashboard" PostBack="false">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Standard 1" Value="Standard1" PostBack="false">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Standard 2" Value="Standard2" PostBack="false">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Standard 4" Value="Standard4" PostBack="false">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <asp:HiddenField ID="hiddenFieldTabValue" runat="server" Value="AboutTheDashboard" />
            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
                <telerik:RadPageView runat="server" ID="RadPageView1">
                    <section>
                        <h2 align="center">About the Dashboard...</h2>
                        <h4 align="center"><em>Texas Dashboard requires an HTML 5 compatible browser such as Chrome (the preferred choice), Firefox, or Internet Explorer 10 or higher</em></h4>
                        <table width="100%" border="0" cellspacing="2" cellpadding="0" summary="Links to ESC Standards and Indicators, User Guide, Data Description documents">
                            <caption>
                                &nbsp;
                            </caption>
                            <tr>
                                <th scope="col">
                                    <p>
                                        <img src="documentation.png" width="38" height="49" alt="Data Sources">
                                    </p>
                                    <p><a href="escWorks Dashboard Guide_v3.pdf" title="How to Use the Dashboard" target="_new">• How to Use the Dashboard</a></p>
                                    <p></p>
                                    <p></p>
                                    <p></p>
                                </th>
                                <th scope="col">
                                    <p>
                                        <img src="documentation.png" width="38" height="49" alt="Data Sources">
                                    </p>
                                    <p><a href="Data Definitions.pdf" title="How it Adds Up - Data Dictionary" target="_new">• How it Adds Up</a></p>
                                    <p></p>
                                    <p></p>
                                    <p></p>
                                </th>
                                <th scope="col">
                                    <p>
                                        <img src="documentation.png" width="38" height="49" alt="document image">
                                    </p>
                                    <p style="color: #005695">ESC Standards and Indicators</p>
                                    <p><a href="Standards and Indicators.pdf" title="Regional ESC Performance Standards and Indicators Manual (PDF)" target="_new">• State Document</a></p>
                                    <p><a href="Indicator Dictionary.pdf" title="Data Dictionary" target="_new">• Indicator Dictionary</a></p>
                                    <p><a href="Dropdown.pdf" title="Dropdown Lists" target="_new">• Dropdown Lists</a></p>
                                </th>
                            </tr>
                        </table>
                        <hr />
                    </section>
                    <h2 align="center">About the Data...</h2>
                    <h5 align="center">The data within the escWorks Texas Dashboard is updated on a nightly basis. All
                    data should be current as of 5:00 AM the day of viewing.</h5>
                    <hr>
                    </section>
                <section>
                </section>
                    <section>
                        <h2 align="center">Helpful Information...</h2>
                        <h5 align="center">Please note: Revisions to the Texas Dashboard will be made in order to align with changes to the Standards and Indicators.</h5>
                        <h3 align="center">Have a question? Contact us at: <span style="color: white; link color: white; visited color: white; hover color: white; active color: black"><a href="mailto:helpdesk@esc4.net">helpdesk@esc4.net</a></span></h3>
                    </section>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView2" Height="600px" runat="server">

                    <p></p>
                    <h2>Standard 1 </h2>
                    <table width="100%" border="0" cellspacing="2" cellpadding="0">
                        <tr>
                            <th width="50%" align="left" scope="col">
                                <h5>1.1b - Performance Index - The &quot;How&quot; Description <i>(Applies only to 2014-2015)</i></h5>
                            </th>
                            <th width="50%" align="left" scope="col">
                                <h5>1.3 - Assistance and/or Training Low Performing Districts/Schools</h5>
                            </th>
                        </tr>
                        <tr>
                            <td align="left">
                                <h5>1.2 - List of Assistance and/or Activities Provided that Support Opportunities for
                                All Students to Earn Postsecondary Credit</h5>
                            </td>
                            <td align="left">
                                <h5>1.4 - Training Evaluation Rating</h5>
                            </td>
                        </tr>
                    </table>

                    <iframe frameborder="1" scrolling="yes" width="99.7%" height="600px" radius-r="5px" id="iframe1"
                        src="https://dashboard.escworks.net/app/main#/dashboards/57ebf76cdcd5548018000223?folder=57d86f12566352546d0003ca&embed=True&r=False&l=false"></iframe>

                    <hr width="90%" />
                    <h5>Copyright © 2014-2016 escWorks.NET<sup>®</sup> All Rights Reserved
                    </h5>

                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView3" Height="600px" runat="server">

                    <p></p>
                    <h2>Standard 2</h2>
                    <table width="100%" border="0" cellspacing="2" cellpadding="0">
                        <tr>
                            <th align="left" scope="col">
                                <h5>2.1A - Number of Individuals Trained / Training Contact Hours</h5>
                            </th>
                            <th align="left" scope="col">
                                <h5>2.3A - Products and Services Related to School Finance Provided to Districts and
                                Charter Schools</h5>
                            </th>
                        </tr>
                        <tr>
                            <td align="left">
                                <h5>2.1B - Consulting Assistance Contacts and Consulting Assistance of a Quarter Hour
                                or Greater</h5>
                            </td>
                            <td align="left">
                                <h5>2.3B - Technical Consulting Assistance of a Quarter Hour or Greater Related to School
                                Finance</h5>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <h5>2.2B - Technical Assistance Provided to Districts and Charter Schools that do not
                                Meet Standard on the Financial Integrity Rating System of Texas (FIRST)</h5>
                            </td>
                            <td align="left">
                                <h5>2.4 - Number of Educators Involved in Extended Professional Development Activites
                                and Number of Contact Hours</h5>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <h5>2.5 - Number of District/Charter Personnel Participating in Learning through Regional
                                Distance Learning and the Number of Distance Learning Events</h5>
                            </td>
                        </tr>
                    </table>

                    <iframe frameborder="1" scrolling="yes" width="99.7%" height="600px" id="iframe2"
                        src="https://dashboard.escworks.net/app/main#/dashboards/57ebf8fddcd554801800029d?folder=57d86f12566352546d0003ca&embed=True&r=false&l=false"></iframe>

                    <hr width="90%" />
                    <h5>Copyright © 2014-2016 escWorks.NET<sup>®</sup> All Rights Reserved
                    </h5>

                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView4" Height="600px" runat="server">

                    <p></p>
                    <h2>Standard 4</h2>
                    <table width="100%" border="0" cellspacing="2" cellpadding="0">
                        <tr>
                            <th scope="col">
                                <h5>4.1 Core Technical Assistance and Professional Development</h5>
                            </th>
                        </tr>
                    </table>

                    <iframe frameborder="1" scrolling="yes" width="99.7%" height="600px" id="iframe4"
                        src="https://dashboard.escworks.net/app/main#/dashboards/57ebf9e5dcd554801800033b?folder=57d86f12566352546d0003ca&embed=True&l=false"></iframe>

                    <hr width="90%" />
                    <h5>Copyright © 2014-2016 escWorks.NET<sup>®</sup> All Rights Reserved
                    </h5>
                </telerik:RadPageView>
            </telerik:RadMultiPage>

        </div>
    </form>
</body>
</html>
