<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register TagPrefix="ucontrol" TagName="UploadFile" Src="~/lib/Controls/UploadFile.ascx" %>
<asp:Content runat="server" ID="content1" ContentPlaceHolderID="mainBody"><a name="MainBody"></a>
    <a name="MainBody"></a>
    <link type="text/css" rel="stylesheet" href="~/lib/css/escWorksStyle.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.5.8/slick.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.ad-items').slick({
                slidesToShow: 1,
                slidesToScroll: 1,
                autoplay: true,
                autoplaySpeed: 4000
            });
        });
    </script>


    <script language="javascript" type="text/javascript">
        if (typeof String.prototype.trim != 'function') { // detect native implementation
            String.prototype.trim = function () {
                return this.replace(/^\s+/, '').replace(/\s+$/, '');
            };
        }

        function FindSession() {
            var mSession = document.aspnetForm.findSession.value.trim();

            if (mSession.length < 1) {
                alert("Please type " + '<%# region4.escWeb.SiteVariables.ObjectProvider.SessionName %>' + " ID or Keyword");
                document.aspnetForm.findSession.focus();
                return;
            }
            if (isNaN(mSession))
                top.location.href = "search.aspx?SearchCriteria=" + mSession;
            else
                top.location.href = "./catalog/session.aspx?session_id=" + mSession;
        }
    </script>
    <div class="row">
        <div class="col-sm-12 col-lg-12 sidenav">
            <div id="pageheader">
                <span id="headerFont">Welcome to Professional Development Online Registration</span>
            </div>
        </div>
    </div>

    <div id="searchbox">
        <asp:Panel ID="Panel1" DefaultButton="btnSearch" runat="server">
            <div class="row">
                <div class="col-xs-12 col-sm-12">
                    <span style="font-weight: bold; font-size: 1.3em;">
                        <label for="findSession">Search by Session ID or Keyword</label></span>
                </div>
            </div>
            <div class="row">
                <div class="col-6 col-sm-8">
                    <input type="text" name="findSession" id="findSession" class="form-control fullWidth smallFont" style="height: 32px;" />
                </div>
                <div class="col-2 col-sm-3">
                    <asp:Button runat="server" ID="btnSearch" OnClientClick="FindSession();return false;" Text="Search" CssClass="formInput btn btn-R17Blue btn-lg" Style="width: 115px; font-size: small" />
                </div>
            </div>
        </asp:Panel>
    </div>

    <div class="container" style="border: 1px solid black;">
        <div class="row" style="background-color: #ebebeb;">
            <div class="col-sm-12 col-lg-8 text-left">
                <br />
                <label id="slick-slide00"><span style="display: none">Aria</span></label>
                <div class="ad-items" id="divAdItems" runat="server" style="width: 100%; height: auto;">
                </div>
                <a href="about/contact.aspx" style="text-decoration: underline; color: darkred;"><font
                    size="3" color="darkred"><b>Contact Us</b></font></a> <span style="font-size: 9pt">For
                                                    additional questions or assistance please click the Contact Us Link.</span>
                <br />
                <br />
                <div>
                    <ucontrol:UploadFile ID="UploadFile1" runat="server" />
                </div>
            </div>


            <div class="col-sm-12 col-lg-4" style="float: right; border-left: 1px solid black;">
                <div>
                    <h3 style="line-height: 1.6;">
                        <br />
                        <asp:Panel ID="upcomingevents" runat="server">
                        </asp:Panel>
                    </h3>
                </div>
            </div>
        </div>
    </div>
    <br />
    <script type="text/javascript" src="lib/js/swfobject.js"> 
    </script>
</asp:Content>
