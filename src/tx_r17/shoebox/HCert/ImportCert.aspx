<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportCert.aspx.cs"  EnableViewState="False" EnableViewStateMac="False"  MasterPageFile="~/MasterPage.master" Inherits="shoebox_ImportCert" %>


 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="mainBody"><a name="MainBody"></a>
   <asp:PlaceHolder ID="pimporttranscript" runat="server" Visible="true" >
    <b>Official Record</b>
            <br />
     <span >An official record cannot be altered. It contains
                       a record of the
                                <%# region4.escWeb.SiteVariables.ObjectProvider.SessionPluralName%>
                                that you have attended at
                                <%# region4.escWeb.SiteVariables.customer_name %>. </span>

       Click the button to view/print the past Official Record <br /><br /><asp:Button ID="Bimporttransript" runat="server" Text="Official Record"  OnClick="Bimporttransript_Onclick"/>

   </asp:PlaceHolder>
   <br /><br />
    
    <asp:placeholder id="pimportcert" Runat="server" Visible="true">
    <b>Certificates</b> 
    </asp:placeholder>
    </asp:Content>


      
      