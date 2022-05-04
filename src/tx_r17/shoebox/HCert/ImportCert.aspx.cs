using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using escWorks.BusinessObjects;
using escWeb.tx_r17.ObjectModel;
using System.Data.SqlClient;
using System.Data;



public partial class shoebox_ImportCert : region4.escWeb.BasePage
{
    region4.ObjectModel.User CurrentUser;

    private HtmlTable _registrationTable = new HtmlTable();
    protected int _hcertificateID;
    public HtmlTable RegistrationTable;
    public virtual int hcertificate_id { get { return Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["report.hcertificate"]); } }
    public virtual int hofficialtranscript_id { get { return Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["report.hofficialtranscriptid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUser = ((region4.escWeb.BasePage)System.Web.HttpContext.Current.CurrentHandler).CurrentUser;
        DisplayHistoryEvents(); 
        
    }
    protected virtual void DisplayHistoryEvents()
    {
        CreateHHeader();
        using (SqlCommand cmd = new SqlCommand("[P.ObjectModel.User.GetHistory]", region4.Common.DataConnection.DbConnection))
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 200).Value = CurrentUser.PrimaryEmail.ToLower().Trim();

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                using (DataSet data = new DataSet())
                {
                    da.Fill(data);

                    if (data != null)
                    {
                        foreach (DataRow dr in data.Tables[0].Rows)
                        {
                            HtmlTableRow row = new HtmlTableRow();
                            row.ID = "HtmlTableRow_" + dr[0].ToString();
                            row.Cells.Add(new HtmlTableCell());
                            row.Cells.Add(new HtmlTableCell());
                            row.Cells.Add(new HtmlTableCell());
                            row.Cells.Add(new HtmlTableCell());

                            row.Cells[0].InnerText = dr[0].ToString();
                            row.Cells[1].InnerText = dr[1].ToString();
                            row.Cells[2].InnerText = dr[2].ToString();
                            PlaceHolder pLinks = new PlaceHolder();
                            LinkButton link;
                            link = new LinkButton();
                            link.Text = "Certificate";
                            link.CssClass = "link";
                            link.ID = "showCertificate_" + dr[0].ToString();
                            link.CommandArgument = dr[0].ToString() + "|" + dr[3].ToString() + "|" + hcertificate_id.ToString();
                            link.Click += new EventHandler(hcertificateLink_Click);
                            AddLink(pLinks, link);
                            row.Cells[3].Controls.Add(pLinks);
                            _registrationTable.Rows.Add(row);
                            pimportcert.Controls.Add(_registrationTable);

                        }
                    }
                }
            }
        }


    }


    public virtual void hcertificateLink_Click(object sender, EventArgs e)
    {
        LinkButton link = sender as LinkButton;
        if (link == null)
            throw new Exception("expected a link button but didn't get one");

        string[] certAr = link.CommandArgument.Split('|');
        string sessionid = (certAr[0]);
        string email = (certAr[1]);        
        HSendCertificate(hcertificate_id, sessionid, email);

    }

    public virtual void HSendCertificate(int hcertificate_id, string sessionid, string email)
    {
        string queryString = "&reportid=" + hcertificate_id
                        + "&instruction=download:pdf"
                        + "&table1=rds_ImportCertificate&field1=sessionid&operator1={0}={1}&value1='" + sessionid + "'"
                        + "&table2=rds_ImportCertificate&field2=email&operator2={0}={1}&value2='" + email+"'";


        GetReport(queryString);
    }


    public virtual void HSendOfficialTranscript(int hofficialtranscript_id,string email)
    {
        string queryString = "&reportid=" + hofficialtranscript_id
                           + "&instruction=download:pdf"
                           + "&table1=rds_ImportCertificate&field1=email&operator1={0}={1}&value1='" + email + "'";
                           

        GetReport(queryString);
    }
    public void GetReport(string queryString)
    {
        System.Web.UI.Page myBasePage = ((System.Web.UI.Page)System.Web.HttpContext.Current.Handler);
        string strUrl = region4.escWeb.SiteVariables.escWorksReportServer
            + "GetReport.aspx?cid="
            + region4.escWeb.SiteVariables.customer_id
            + queryString;
        myBasePage.Response.Redirect(strUrl);
    }
   
    protected void AddLink(PlaceHolder p, LinkButton link)
    {
        if (p.Controls.Count == 0)
            p.Controls.Add(link);
        else
        {
            p.Controls.Add(new System.Web.UI.LiteralControl(" | "));
            p.Controls.Add(link);
        }
    }
    public void CreateHHeader()
    {
        _registrationTable.Attributes.Add("class", "mainBody");

        //Generate title row
        HtmlTableRow row = new HtmlTableRow();
        row.Cells.Add(new HtmlTableCell());
        row.Cells.Add(new HtmlTableCell());
        row.Cells.Add(new HtmlTableCell());
        row.Cells.Add(new HtmlTableCell());


        row.Cells[0].InnerText = "Session ID";
        row.Cells[1].InnerText = "Title";
        row.Cells[2].InnerText = "Start Date";
        row.Cells[3].InnerText = "Certificate";


        _registrationTable.Width = Unit.Percentage(100).ToString();

        row.Cells[0].Attributes.Add("class", "tableHeading");
        row.Cells[1].Attributes.Add("class", "tableHeading");
        row.Cells[2].Attributes.Add("class", "tableHeading");
        row.Cells[3].Attributes.Add("class", "tableHeading");


        _registrationTable.Rows.Add(row);
    }

    protected void Bimporttransript_Onclick(object sender,EventArgs e)
    {
        HSendOfficialTranscript(hofficialtranscript_id, CurrentUser.PrimaryEmail);
    }
}
