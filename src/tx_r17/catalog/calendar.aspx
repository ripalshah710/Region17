<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="calendar.aspx.cs" Inherits="catalog_calendar" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainBody" runat="Server">
    <a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />

    <script type="text/javascript">
        $(document).ready(function () {

            window.onorientationchange = function () { location.reload() };

            $('span ul').wrap('<div class="outer"/>').contents().unwrap();
            $('span .outer li').wrap('<div class="item"/>').contents().unwrap();

            if ($(window).width() < 415) {
                $("#displayButton").show();
            }

            var getUrlParameter = function getUrlParameter(sParam) {
                var sPageURL = window.location.search.substring(1),
                    sURLVariables = sPageURL.split('&'),
                    sParameterName,
                    i;

                for (i = 0; i < sURLVariables.length; i++) {
                    sParameterName = sURLVariables[i].split('=');

                    if (sParameterName[0] === sParam) {
                        return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
                    }
                }
            };

            var currentMonth = new Date().getMonth();
            var whatYear = new Date().getFullYear();
            var currentYear = new Date().getFullYear();
            var nextYear = new Date().getFullYear() + 1;

            var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "Break"];

            if (monthNames[currentMonth] != 'Break') {
                $("#one").text(monthNames[currentMonth]);
                currentMonth++;
                $("#one").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            else {
                currentMonth = 0;
                whatYear = nextYear;
                $("#one").text(monthNames[currentMonth]);
                currentMonth++;
                $("#one").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            if (monthNames[currentMonth] != 'Break') {
                $("#two").text(monthNames[currentMonth]);
                currentMonth++;
                $("#two").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            else {
                currentMonth = 0;
                whatYear = nextYear;
                $("#two").text(monthNames[currentMonth]);
                currentMonth++;
                $("#two").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            if (monthNames[currentMonth] != 'Break') {
                $("#three").text(monthNames[currentMonth]);
                currentMonth++;
                $("#three").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            else {
                currentMonth = 0;
                whatYear = nextYear;
                $("#three").text(monthNames[currentMonth]);
                currentMonth++;
                $("#three").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            if (monthNames[currentMonth] != 'Break') {
                $("#four").text(monthNames[currentMonth]);
                currentMonth++;
                $("#four").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            else {
                currentMonth = 0;
                whatYear = nextYear;
                $("#four").text(monthNames[currentMonth]);
                currentMonth++;
                $("#four").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            if (monthNames[currentMonth] != 'Break') {
                $("#five").text(monthNames[currentMonth]);
                currentMonth++;
                $("#five").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            else {
                currentMonth = 0;
                whatYear = nextYear;
                $("#five").text(monthNames[currentMonth]);
                currentMonth++;
                $("#five").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            if (monthNames[currentMonth] != 'Break') {
                $("#six").text(monthNames[currentMonth]);
                currentMonth++;
                $("#six").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            else {
                currentMonth = 0;
                whatYear = nextYear;
                $("#six").text(monthNames[currentMonth]);
                currentMonth++;
                $("#six").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            if (monthNames[currentMonth] != 'Break') {
                $("#seven").text(monthNames[currentMonth]);
                currentMonth++;
                $("#seven").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            else {
                currentMonth = 0;
                whatYear = nextYear;
                $("#seven").text(monthNames[currentMonth]);
                currentMonth++;
                $("#seven").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            if (monthNames[currentMonth] != 'Break') {
                $("#eight").text(monthNames[currentMonth]);
                currentMonth++;
                $("#eight").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            else {
                currentMonth = 0;
                whatYear = nextYear;
                $("#eight").text(monthNames[currentMonth]);
                currentMonth++;
                $("#eight").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            if (monthNames[currentMonth] != 'Break') {
                $("#nine").text(monthNames[currentMonth]);
                currentMonth++;
                $("#nine").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            else {
                currentMonth = 0;
                whatYear = nextYear;
                $("#nine").text(monthNames[currentMonth]);
                currentMonth++;
                $("#nine").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            if (monthNames[currentMonth] != 'Break') {
                $("#ten").text(monthNames[currentMonth]);
                currentMonth++;
                $("#ten").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            else {
                currentMonth = 0;
                whatYear = nextYear;
                $("#ten").text(monthNames[currentMonth]);
                currentMonth++;
                $("#ten").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            if (monthNames[currentMonth] != 'Break') {
                $("#eleven").text(monthNames[currentMonth]);
                currentMonth++;
                $("#eleven").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            else {
                currentMonth = 0;
                whatYear = nextYear;
                $("#eleven").text(monthNames[currentMonth]);
                currentMonth++;
                $("#eleven").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            if (monthNames[currentMonth] != 'Break') {
                $("#twelve").text(monthNames[currentMonth]);
                currentMonth++;
                $("#twelve").prop("href", "?month=" + currentMonth + "&year=" + whatYear);
            }
            else {
                currentMonth = 0;
                whatYear = nextYear;
                $("#twelve").text(monthNames[currentMonth]);
                currentMonth++;
                $("#twelve").prop("href", "?month=" + currentMonth + "&year=" + nextYear);
            }

            $(".monthButton").click(function () {
                $('.monthButton').removeClass('selected');
                sessionStorage.setItem('mthSelected', $(this).attr("id"));
            });
            $('#' + sessionStorage.getItem('mthSelected')).addClass('selected');
        });
    </script>

    <div id="displayButton" style="display: none;">
        <div class='container'>
            <div class='row'>
                <div class='col-xs-2'><a class='monthButton btn' id='one'></a></div>
                <div class='col-xs-2'><a class='monthButton btn' id='two'></a></div>
                <div class='col-xs-2'><a class='monthButton btn' id='three'></a></div>
                <div class='col-xs-2'><a class='monthButton btn' id='four'></a></div>
            </div>
            <div class='row'>
                <div class='col-xs-2'><a class='monthButton btn' id='five'></></a></div>
                <div class='col-xs-2'><a class='monthButton btn' id='six'></></a></div>
                <div class='col-xs-2'><a class='monthButton btn' id='seven'></></a></div>
                <div class='col-xs-2'><a class='monthButton btn' id='eight'></></a></div>
            </div>
            <div class='row'>
                <div class='col-xs-2'><a class='monthButton btn' id='nine'></></a></div>
                <div class='col-xs-2'><a class='monthButton btn' id='ten'></></a></div>
                <div class='col-xs-2'><a class='monthButton btn' id='eleven'></></a></div>
                <div class='col-xs-2'><a class='monthButton btn' id='twelve'></></a></div>
            </div>
        </div>
    </div>

    <div class="CalendarDesktop">
                <label for="formLabel_month" style="display: none;">formLabel_month</label>
<label for="formLabel_year" style="display: none;">formLabel_year</label>
        <table width="100%" style="border-collapse: collapse; background-color: #ffffff;">
            <tr>
                <td colspan="2">
                    <escWorks:Calendar runat="server" ID="cal1" PreviousText="&lt;&lt; Previous" NextText="Next &gt;&gt;" SetDateText="Go" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="CalendarMobile col-sm-12 col-lg-5" style="float: right">
        <div>
            <h3 style="line-height: 1.6">
                <escWorks:UpcomingEvents runat="server" ID="UpcomingEvents" ItemsToDisplay="100" IsMobile="true" />
            </h3>
        </div>
    </div>
</asp:Content>

