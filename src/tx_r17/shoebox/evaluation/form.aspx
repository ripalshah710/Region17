<%@ Page Language="c#" Inherits="tx_r17.Shoebox.Evaluation.Pages.FormPage" EnableViewState="False"
    EnableViewStateMac="False" CodeFile="form.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD html 4.0 Transitional//EN" >
<html lang="en" xml:lang="en">
<head>
    <title>escWorks® .NET Evaluation Form</title>
    <link href="../../lib/css/local.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        html, body, form
        {
            height: 100%;
            margin: 0px;
            padding: 0px;
        }
        .GreenSliderTrack .rslSelectedregion
        {
            background: #8ae234 !important;
            height: 10px !important;
        }
        .GreenSliderTrack .rslTrack
        {
            background: #8ae234 !important;
            height: 10px !important;
        }
        .RedSliderTrack .rslSelectedregion
        {
            background: #ef2929 !important;
            height: 10px !important;
        }
        .RedSliderTrack .rslTrack
        {
            background: #ef2929 !important;
            height: 10px !important;
        }
    </style>
    <script language="javascript" type="text/javascript">
			function Page_OnInit()
			{
				<asp:PlaceHolder ID="pCloser" Runat="Server" Visible="False">parent.close();</asp:PlaceHolder>
				<asp:PlaceHolder ID="pComplete" Runat="Server" Visible="False">alert("Thank you for completing the form!");
                    parent.window.opener.location.reload();
                    parent.close();return false;
                </asp:PlaceHolder>
			}

        function OnClientValueChanged(sender, args) {

            var sliderId = sender.get_id();
            var currentValue = sender.get_value();

            if (currentValue >= 4.5)
                document.getElementById(sliderId).className = "RadSlider RadSlider_Default GreenSliderTrack";
            else
                document.getElementById(sliderId).className = "RadSlider RadSlider_Default RedSliderTrack";

            if(currentValue.toString().length == 1)
                document.getElementById("lbl" + sliderId).innerHTML = currentValue.toString() + ".0";
            else
                document.getElementById("lbl" + sliderId).innerHTML = currentValue;

            document.getElementById("hidden" + sliderId).value = currentValue;
        }
    </script>
</head>
<body onload="Page_OnInit();" style="margin: 0px;">
    <form id="EvaluationForm" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <table cellspacing="0" cellpadding="0" width="555" height="100%" border="0">
        <asp:PlaceHolder ID="pForm" runat="server">
            <tr valign="middle">
                <td colspan="2">
                    <asp:Table ID="tbForm" runat="Server" />
                </td>
            </tr>
            <tr>
                <td width="100%" align="right">
                    <input type="Button" id="btnCancel" value="Cancel" runat="Server" causesvalidation="False"
                        onserverclick="Click_btnCancel" /><input type="Button" id="btnSave" value="Save"
                            runat="Server" name="btnSave" causesvalidation="False" onserverclick="Click_btnSave" />
                </td>
                <td align="left">
                    <asp:Button ID="btnSubmit" Text="Submit" Name="btnSubmit" CausesValidation="False"
                        OnClick="Click_btnSubmit" runat="Server" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="pCompleteFailure" Visible="False" runat="Server">
            <tr valign="middle">
                <td colspan="2">
                    <font color="#ff0000" size="4"><b><i>Sorry!...An error occurred!</i></b></font>
                    <br>
                    <br>
                    <b><i>The error message reported was:</i></b>
                    <hr align="left" width="400" noshade size="1">
                </td>
            </tr>
            <tr>
                <td class="FormInput">
                    <asp:Label ID="lblErrorMessage" runat="Server"></asp:Label>
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
    </form>
</body>
</html>
