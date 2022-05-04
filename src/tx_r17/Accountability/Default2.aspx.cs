using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.SqlClient;
using Csla;

using Telerik.Web.UI;
using Csla.Validation;
using tx_r17.BusinessObect.Accountability;
using escWeb.tx_r17.ObjectModel;
using System.Net.Mail;

public partial class Accountability_Default : System.Web.UI.Page
{
    private escWeb.tx_r17.ObjectModel.User user;
    private List<tx_r17.BusinessObect.Accountability.Assistance> dailyList;
    private List<AssistanceDisplay> dailyListDisplay;
    private tx_r17.BusinessObect.Accountability.Assistances myList;
    private tx_r17.BusinessObect.Accountability.AssistancesDisplay myListDisplay;   
    private string date = string.Empty;
    private string strObjId = string.Empty;
    private string strDetail = string.Empty;
    private ConfigurationInfo ci;
    public string sid = string.Empty;
    public string url = string.Empty;
    public string report_time_sheet_id = string.Empty;
    public string report_summary_id = string.Empty;
    public DateTime firstDayofMonth;
    public DateTime lastDayofMonth;
    
    

    protected void Page_Init(object sender, EventArgs e)
    {
        //PopulateDropDownList(ddlRegion, "[p.Accountability.Location.Region.Load]", 0, "dropdownlsit_region", 60);
        
        //PopulateDropDownList(ddlTravelTime, "[p.Accountability.DropdownLists.Load]", region4.escWeb.SiteVariables.AccountabilityTimeLength, "dropdownlsit_traveltime", 60);
        PopulateDropDownList(ddlContactMethod, "[p.Accountability.DropdownLists.Load]", region4.escWeb.SiteVariables.AccountabilityContactMethod, "dropdownlsit_contactmethod", 60);
        PopulateDropDownList(ddlActivities, "[p.Accountability.DropdownLists.Load]", region4.escWeb.SiteVariables.AccountabilityActivity, "dropdownlsit_activities", 5);
        PopulateDropDownList(ddlTEC, "[p.Accountability.DropdownLists.Load]", region4.escWeb.SiteVariables.AccountabilityTEC, "dropdownlsit_tec", 60);
        PopulateDropDownList(ddlNonFirstStandardFinancialAssistanceList, "[p.Accountability.DropdownLists.Load]", 3005, "dropdownlsit_ddlNonFirstStandardFinancialAssistanceList", 60);
        PopulateDropDownList(ddlFunding, "[p.Accountability.DropdownLists.Load]", 3006, "dropdownlsit_ddlFunding", 60);
        PopulateDropDownList(ddlPerformanceList, "[p.Accountability.DropdownLists.Load]", region4.escWeb.SiteVariables.AccountabilityIncreaseStudentPerformance, "dropdownlsit_performanceList", 60);
        PopulateDropDownList(ddlCreditList, "[p.Accountability.DropdownLists.Load]", region4.escWeb.SiteVariables.AccountabilityPostSecondaryCredit, "dropdownlsit_creditList", 60);
        PopulateDropDownList(ddlIntegrityList, "[p.Accountability.DropdownLists.Load]", region4.escWeb.SiteVariables.AccountabilityFinancialIntegrity, "dropdownlsit_integrityList", 60);
        PopulateDropDownList(AudienceList, "[p.Accountability.DropdownLists.Load]", region4.escWeb.SiteVariables.AccountabilityAudience, "dropdownlsit_audienceList", 5);
        PopulateDropDownList(ddlStrategicPriorityList, "[p.Accountability.DropdownLists.Load]", 10000, "dropdownlsit_ddlStrategicPriorityList", 60); //Added by VM 6-19-2018
        //PopulateCombBox(ddlClient);               

        if (Session["profile"] != null)
            user = (escWeb.tx_r17.ObjectModel.User)Session["profile"];
        litEmployeeName.Text = user.FirstName + " " + user.LastName;

        PopulateDropDownList(ddlFocus, "[p.Accountability.Focus.Load]", 0, "dropdownlsit_focus", 3);
        PopulateDropDownList(ddlBudgetCode, "[p.Accountability.BudgetCode.Load]", 0, "dropdownlsit_budgetcode", 3);
        PopulateDropDownList(ddlServiceLength, "[p.Accountability.Hours.Load]",0, "dropdownlsit_servicelength", 3);

        sid = user.Sid.ToString();
        url = region4.escWeb.SiteVariables.escWorksReportServer;
        report_time_sheet_id = region4.escWeb.SiteVariables.AccountabilityTimeSheetReport.ToString();
        report_summary_id = region4.escWeb.SiteVariables.AccountabilitySimmaryReport.ToString();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        date = LegacyCode.Strings.GetSafeString("date", LegacyCode.Strings.StringType.QueryString);
        strObjId = LegacyCode.Strings.GetSafeString("obj_id", LegacyCode.Strings.StringType.QueryString);
        strDetail = LegacyCode.Strings.GetSafeString("detail", LegacyCode.Strings.StringType.QueryString);

        if (!Page.IsPostBack && !Page.IsCallback)
        {
            if (strDetail == "")
                myListDisplay = tx_r17.BusinessObect.Accountability.AssistancesDisplay.GetAssistancesDisplay(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1), user.Sid);
            else
                myListDisplay = ((tx_r17.BusinessObect.Accountability.AssistancesDisplay)Session["myListDisplay"]);

            Session["myListDisplay"] = myListDisplay;

            if (myListDisplay != null && myListDisplay.Count > 0)
            {
                firstDayofMonth = new DateTime(myListDisplay[0].Service_Date.Year, myListDisplay[0].Service_Date.Month, 1);
                lastDayofMonth = firstDayofMonth.AddMonths(1).AddDays(-1);
                litreqhours.Text = Convert.ToString(myListDisplay.getMinimHours(myListDisplay[0].Service_Date));
            }

            if (myListDisplay.Count == 0)
            {
                //no records
                btnCopy.Enabled = false;
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
            }

            if (date != "" && strObjId != "")
            {
                dailyList = ((tx_r17.BusinessObect.Accountability.Assistances)Session["myList"]).GetAssistancesByDateRange(Convert.ToDateTime(date), Convert.ToDateTime(date));
                Session["dailyList"] = dailyList;
                foreach (tx_r17.BusinessObect.Accountability.Assistance a in dailyList)
                {
                    ((tx_r17.BusinessObect.Accountability.Assistances)Session["myList"]).Index++;

                    if (a.ObjId == Convert.ToInt32(strObjId))
                    {
                        BindingData(a);
                        break;
                    }
                }
                if (dailyList.Count > 1) //Added 7-20-2012
                {
                    btnNext.Visible = true;
                    btnPrev.Visible = true;

                }
                else
                {
                    btnNext.Visible = false;
                    btnPrev.Visible = false;
                }
                pnlDetail.Visible = true;
                pnlReports.Visible = !pnlDetail.Visible;

                myList = (tx_r17.BusinessObect.Accountability.Assistances)Session["myList"];

                litDayHours.Text = myList.GetHours(Convert.ToDateTime(date), Convert.ToDateTime(date)).ToString();
                litThisWeekHours.Text = myListDisplay.GetHours(firstDayofMonth, lastDayofMonth).ToString();
                litDate.Text = "<b>" + Convert.ToDateTime(date).ToShortDateString() + "</b>";

                panelSpace.Visible = false;
            }
            else
            {
                btnNext.Visible = false;
                btnPrev.Visible = false;

                litDayHours.Text = myListDisplay.GetHours(DateTime.Today, DateTime.Today).ToString();
                litThisWeekHours.Text = myListDisplay.GetHours(firstDayofMonth, lastDayofMonth).ToString();
                litDate.Text = "<b>" + DateTime.Today.ToShortDateString() + "</b>";
            }
            if (user.EmployeeStatus == 6055)
                Preqhours.Visible = true;

            /*********** Certify Part***********************************/

            if (myListDisplay != null && myListDisplay.Count > 0)
            {
                CertifyLog myLog = new CertifyLog();
                myLog = user.getCertifyLog(myListDisplay[0].Service_Date.Year, myListDisplay[0].Service_Date.Month, user.Sid);

                if (myListDisplay[0].isEditable()) // checking close date
                {

                    //myLog = user.getCertifyLog(myListDisplay[0].Service_Date.Year, myListDisplay[0].Service_Date.Month, user.Sid);

                    if (user.EmployeeStatus == 6055) // checking employee status
                    {

                        if (myListDisplay.GetHours(firstDayofMonth, lastDayofMonth) >= myListDisplay.getMinimHours(myListDisplay[0].Service_Date) && (myLog == null || !myLog.Certified)) // no records in log table
                            certifyPanel.Visible = true;

                        if (myLog != null)
                        {
                            if (myLog.DeniedBySupervisor || myLog.DeniedByCFO) // denied
                                certifyPanel.Visible = true;
                            else if (!myLog.DeniedBySupervisor && !myLog.DeniedByCFO) // approved or waiting for approving
                                certifyPanel.Visible = false;

                            else { }
                        }
                    }
                    else
                    {
                        //certifyPanel.Visible = true;
                        if (myLog != null)
                        {
                            if (myLog.DeniedBySupervisor || myLog.DeniedByCFO) // denied
                                certifyPanel.Visible = true;
                            else if (!myLog.DeniedBySupervisor && !myLog.DeniedByCFO) // approved or waiting for approving
                                certifyPanel.Visible = false;

                            else { }
                        }
                        else
                            certifyPanel.Visible = true;
                    }
                }

                else
                    certifyPanel.Visible = false;

                if (myLog != null)
                {
                    if (myLog.Certified == true)
                    {
                        lblcertifymsg.Visible = true;
                        lblcertifymsg.ForeColor = System.Drawing.Color.Green;
                        lblcertifymsg.Text = "You have certified hours for the month of " + myListDisplay[0].Service_Date.ToString("MMMM");
                    }
                    if (myLog.Certified == false)
                    {
                        lblcertifymsg.Visible = true;
                        lblcertifymsg.ForeColor = System.Drawing.Color.Red;
                        lblcertifymsg.Text = "You have not certified hours for the month of " + myListDisplay[0].Service_Date.ToString("MMMM");
                    }
                    if (myLog.DeniedBySupervisor == true && myLog.Certified == false)
                    {
                        lblcertifymsg.Visible = true;
                        lblcertifymsg.ForeColor = System.Drawing.Color.Orange;
                        lblcertifymsg.Text = "Certification returned by the Supervisor for the month of " + myListDisplay[0].Service_Date.ToString("MMMM");
                    }
                }
                else
                {
                    lblcertifymsg.Visible = true;
                    lblcertifymsg.ForeColor = System.Drawing.Color.Red;
                    lblcertifymsg.Text = "You have not certified hours for the month of " + myListDisplay[0].Service_Date.ToString("MMMM");

                }
            }
            else
            {
                certifyPanel.Visible = false;
                lblcertifymsg.Visible = false;
            }

            if (!certifyPanel.Visible && !lblcertifymsg.Text.Contains("not") && lblcertifymsg.Text.Trim() !="") //Added by VM 7-19-2018
                panelActionButtons.Enabled = false;
            else
                panelActionButtons.Enabled = true;

            /****************************************************************************/

        }
        else
        {
            if (credit.Value.Trim() != "") { SetupMultiValueControl(ref lbCredit, ref ddlCreditList, ref credit, credit.Value); }
            if (integrity.Value.Trim() != "") { SetupMultiValueControl(ref lbIntegrity, ref ddlIntegrityList, ref integrity, integrity.Value); }
            if (NonFirstStandardFinancialAssistance.Value.Trim() != "") { SetupMultiValueControl(ref lbNonFirstStandardFinancialAssistance, ref ddlNonFirstStandardFinancialAssistanceList, ref NonFirstStandardFinancialAssistance, NonFirstStandardFinancialAssistance.Value); }
            if (StrategicPriority.Value.Trim() != "") { SetupMultiValueControl(ref lbStrategicPriority, ref ddlStrategicPriorityList, ref StrategicPriority, StrategicPriority.Value); } // Added by VM 6-19-2018
            
        }


        if (date == "")
            calActivity.VisibleDate = DateTime.Today;
        else
            calActivity.VisibleDate = Convert.ToDateTime(date);

        litMessage.Text = string.Empty;
 
        if (CertifyOK.Value == "TRUE" && certifyPanel.Visible)
        {
            CertifyUpdate();
        }
    }


    protected void CertifyUpdate()
    {
        myListDisplay = ((tx_r17.BusinessObect.Accountability.AssistancesDisplay)Session["myListDisplay"]);

        if (myListDisplay != null && myListDisplay.Count > 0)
        {
            CertifyLog certifylog = new CertifyLog();
            try
            {
                certifylog.InsertCertifyLog(user.Sid, myListDisplay[0].Service_Date.Month, myListDisplay[0].Service_Date.Year, true, DateTime.Now);
                user = region4.escWeb.SiteVariables.ObjectProvider.ReturnUser(user.User_Pk, true) as escWeb.tx_r17.ObjectModel.User;
                Session["profile"] = user;
            }
            catch (Exception EX)
            {
                //System.Web.HttpContext.Current.Response.Write(EX.Message);

                region4.ErrorReporter.ReportError(EX, HttpContext.Current, region4.ErrorReporter.Severity.notgiven, region4.ErrorReporter.Occurance.webControls);
            }
            lblcertifymsg.Visible = true;
            lblcertifymsg.ForeColor = System.Drawing.Color.Green;
            lblcertifymsg.Text = "You have certified hours for the month of " + myListDisplay[0].Service_Date.ToString("MMMM");
            EmailSupervisor();
            certifyPanel.Visible = false;
            panelActionButtons.Enabled = false;
        }
    
    }

    public static string ReturnSupervisor(Guid currentusersid)
    {
        string result;
        using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "[p.Accountability.GetSupervisorEmailID]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sid", currentusersid);
            try
            {
                cmd.Connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    result = reader["email"].ToString();
                }
            }

            catch (Exception E)
            {
                return string.Empty;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        return result;
    }

    protected void EmailSupervisor()
    {
        string calmonth = myListDisplay[0].Service_Date.ToString("MMMM"); //calActivity.VisibleDate.ToString("MMMM");
        string toemail = string.Empty;
        string supervisoremail = ReturnSupervisor(user.Sid);
        myListDisplay = ((tx_r17.BusinessObect.Accountability.AssistancesDisplay)Session["myListDisplay"]);
        DateTime firstDate = new DateTime(myListDisplay[0].Service_Date.Year, myListDisplay[0].Service_Date.Month, 1);
        DateTime lastDate = firstDate.AddMonths(1);

        if (supervisoremail == null)
            toemail = "cgossett@esc17.net";
        else
            toemail = supervisoremail;


        MailMessage sendMail = new MailMessage();
        sendMail.To.Add(new MailAddress(toemail));
        sendMail.From = new MailAddress(region4.escWeb.SiteVariables.emailFrom);
        sendMail.Subject = user.FirstName + "  " + user.LastName + " - Certified Hours";
        sendMail.IsBodyHtml = true;
        string mailBody = String.Format("{0} has certified time for the month of  {1}.<br/> Attached is the current Time Sheet report for {2}.<br/> please <a href=\"https://txr17.escworks.net/accountability/approvehours.aspx\"> click here </a> to approve or deny the time. <br/>Thank you.", user.FirstName + "  " + user.LastName, calmonth, user.FirstName + "  " + user.LastName);
        sendMail.Body = mailBody;

        region4.reportService.crServer server = new region4.reportService.crServer();
        region4.reportService.Report report = new region4.reportService.Report();
        report.customerID = System.Configuration.ConfigurationManager.AppSettings["CustomerId"];
        report.DataSourceID = 87;
        report.outputType = region4.reportService.CROutput.AdobePDF;
        report.ReportID = 1014;

        region4.reportService.Parameter ap = new region4.reportService.Parameter();
        ap.ColumnName = "sid";
        ap.Operator = "{0}={1}";
        ap.TableName = "view_Assistance";
        ap.Value = user.Sid;

        region4.reportService.Parameter ap2 = new region4.reportService.Parameter();
        ap2.ColumnName = "date";
        ap2.Operator = "{0}>={1}";
        ap2.TableName = "view_Assistance";
        ap2.Value = firstDate; 

        region4.reportService.Parameter ap3 = new region4.reportService.Parameter();
        ap3.ColumnName = "endDate";
        ap3.Operator = "{0}<{1}";
        ap3.TableName = "view_Assistance";
        ap3.Value = lastDate; 


        report.parameters = new region4.reportService.Parameter[] { ap, ap2, ap3 };

        byte[] reportBinary = null;
        try
        {
            string tmp;
            reportBinary = server.ReturnReport(report, out tmp);
        }
        catch (Exception EX)
        {
            region4.ErrorReporter.ReportError(EX, HttpContext.Current, region4.ErrorReporter.Severity.notgiven, region4.ErrorReporter.Occurance.webControls);
            /* string myMessage = EX.Message;
             string filePath = @"C:\ItsSolutions\tx_r17\Error.txt";

             using (StreamWriter writer = new StreamWriter(filePath, true))
             {
                 writer.WriteLine("Inside Email Supervisor exception after return report");
                 writer.WriteLine("Message :" + myMessage + "<br/>" + Environment.NewLine + "StackTrace :" + EX.StackTrace +
                    "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                 writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
             }
              */
        }

        if (reportBinary != null && reportBinary.Length > 0) 
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream(reportBinary);

            Attachment attachment = new Attachment(stream, "currentTimeSheet.pdf");
            sendMail.Attachments.Add(attachment);
        }
      

        try
        {
            SmtpClient smtpClient = new SmtpClient(region4.escWeb.SiteVariables.emailServer);
            smtpClient.Send(sendMail);
        }
        catch(Exception e)
        {
            region4.ErrorReporter.ReportError(e, HttpContext.Current, region4.ErrorReporter.Severity.notgiven, region4.ErrorReporter.Occurance.webControls);
            /*string myMessage = e.Message;
            string filePath = @"C:\ItsSolutions\tx_r17\Error.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Inside Email Supervisor exception after email");
                writer.WriteLine("Message :" + myMessage + "<br/>" + Environment.NewLine + "StackTrace :" + e.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
              */
            
        }
    }
    
    //Calendar Events

    protected void calActivity_DayRender(object sender, DayRenderEventArgs e)
    {
        if (Session["myListDisplay"] != null)
        {

            dailyListDisplay = ((AssistancesDisplay)Session["myListDisplay"]).GetAssistancesDisplayByDateRange(e.Day.Date, e.Day.Date);
            if (dailyListDisplay.Count > 0)
            {
                e.Cell.Text = "<a style=\"cursor:pointer;text-decoration: underline\" onClick=\"OpenWindow('" + e.Day.Date.ToShortDateString() + "');\">" + e.Day.Date.Day.ToString() + "</a>";
                e.Cell.BackColor = ((AssistancesDisplay)Session["myListDisplay"]).GetColor(e.Day.Date);
            }
            else
                e.Cell.Text = e.Day.Date.Day.ToString();
        }
        else
        {
            e.Cell.Text = e.Day.Date.Day.ToString();
        }

        
    }

    protected void calActivity_VisibleMonthChanged(object sender, System.Web.UI.WebControls.MonthChangedEventArgs e)
    {
        myListDisplay = tx_r17.BusinessObect.Accountability.AssistancesDisplay.GetAssistancesDisplay(new DateTime(e.NewDate.Year, e.NewDate.Month, 1), new DateTime(e.NewDate.Year, e.NewDate.Month, 1).AddMonths(1).AddDays(-1), user.Sid);
        Session["myListDisplay"] = myListDisplay;

        if (myListDisplay != null && myListDisplay.Count > 0)
        {
            CertifyLog myLog = new CertifyLog();
            firstDayofMonth = new DateTime(myListDisplay[0].Service_Date.Year, myListDisplay[0].Service_Date.Month, 1);
            lastDayofMonth = firstDayofMonth.AddMonths(1);
            myLog = user.getCertifyLog(myListDisplay[0].Service_Date.Year, myListDisplay[0].Service_Date.Month, user.Sid);

            if (myListDisplay[0].isEditable()) // checking close date
            {
                //CertifyLog myLog = new CertifyLog();

                //myLog = user.getCertifyLog(myListDisplay[0].Service_Date.Year, myListDisplay[0].Service_Date.Month, user.Sid);

                if (user.EmployeeStatus == 6055) // checking employee status
                {

                    if (myListDisplay.GetHours(firstDayofMonth, lastDayofMonth) >= myListDisplay.getMinimHours(myListDisplay[0].Service_Date) && (myLog == null || !myLog.Certified)) // no records in log table
                        certifyPanel.Visible = true;
                    else
                        certifyPanel.Visible = false;
                    if (myLog != null)
                    {
                        if (myLog.DeniedBySupervisor || myLog.DeniedByCFO) // denied
                            certifyPanel.Visible = true;
                        else if (!myLog.DeniedBySupervisor && !myLog.DeniedByCFO) // approved or waiting for approving
                            certifyPanel.Visible = false;

                        else { }
                    }     
                }
                else
                {
                    //certifyPanel.Visible = true;
                    if (myLog != null)
                    {
                        if (myLog.DeniedBySupervisor || myLog.DeniedByCFO) // denied
                            certifyPanel.Visible = true;
                        else if (!myLog.DeniedBySupervisor && !myLog.DeniedByCFO) // approved or waiting for approving
                            certifyPanel.Visible = false;

                        else { }
                    }
                    else
                        certifyPanel.Visible = true;
                }
            }

            else

                certifyPanel.Visible = false;

            if (myLog != null)
            {
                if (myLog.Certified == true)
                {
                    lblcertifymsg.Visible = true;
                    lblcertifymsg.ForeColor = System.Drawing.Color.Green;
                    lblcertifymsg.Text = "You have certified hours for the month of " + myListDisplay[0].Service_Date.ToString("MMMM");
                }
                if (myLog.Certified == false)
                {
                    lblcertifymsg.Visible = true;
                    lblcertifymsg.ForeColor = System.Drawing.Color.Red;
                    lblcertifymsg.Text = "You have not certified hours for the month of " + myListDisplay[0].Service_Date.ToString("MMMM");
                }
                if (myLog.DeniedBySupervisor == true && myLog.Certified == false)
                {
                    lblcertifymsg.Visible = true;
                    lblcertifymsg.ForeColor = System.Drawing.Color.Orange;
                    lblcertifymsg.Text = "Certification returned by the Supervisor for the month of " + myListDisplay[0].Service_Date.ToString("MMMM");
                }

            }
            else
            {
                lblcertifymsg.Visible = true;
                lblcertifymsg.ForeColor = System.Drawing.Color.Red;
                lblcertifymsg.Text = "You have not certified hours for the month of " + myListDisplay[0].Service_Date.ToString("MMMM");

            }

        }
        else
        {
            certifyPanel.Visible = false;
            lblcertifymsg.Visible = false;
        }


        ClearForm();
        panelSpace.Visible = true;
        pnlDetail.Visible = false;

        if (!certifyPanel.Visible && !lblcertifymsg.Text.Contains("not")) //Added by VM 7-19-2018
            panelActionButtons.Enabled = false;
        else
            panelActionButtons.Enabled = true;
        
    }

    protected void PopulateCombBox(RadComboBox combo)
    {
        DataTable clients = new DataTable();
        using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "[p.Accountability.Location.Client.Load_2]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using (SqlDataAdapter SqlDa = new SqlDataAdapter(cmd))
            {
                SqlDa.Fill(clients);

                foreach (DataRow row in clients.Rows)
                {
                    RadComboBoxItem item = new RadComboBoxItem(row["display"].ToString(), row["side_id"].ToString());
                    combo.Items.Add(item);
                }
            }
        }
    }

    protected void PopulateCombBox(RadComboBox combo, int key)
    {
        DataTable clients = new DataTable();
        using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "[p.Accountability.Location.Site.Load]";
            cmd.Parameters.AddWithValue("@id", key);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using (SqlDataAdapter SqlDa = new SqlDataAdapter(cmd))
            {
                SqlDa.Fill(clients);

                foreach (DataRow row in clients.Rows)
                {
                    RadComboBoxItem item = new RadComboBoxItem(row["display"].ToString(), row["key"].ToString());
                    combo.Items.Add(item);
                }
            }
        }
    }

    protected void ddlClient_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        RadComboBox combo = (RadComboBox)o;
        combo.Items.Clear();

        DataTable clients = new DataTable();
        using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "[p.Accountability.Location.Client.Load]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@text", e.Text);
            cmd.Parameters.AddWithValue("@Ck22b", CK22B.Checked);

            using (SqlDataAdapter SqlDa = new SqlDataAdapter(cmd))
            {
                SqlDa.Fill(0, 50, clients);

                foreach (DataRow row in clients.Rows)
                {
                    RadComboBoxItem item = new RadComboBoxItem(row["display"].ToString(), row["side_id"].ToString());
                    combo.Items.Add(item);
                }
            }
        }
    }


    protected void ddlClient_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {

        if (ddlSite.Items.Count > 1)
            ddlSite.Items.Clear();

        if (ddlClient.SelectedValue == "")
        {
            ddlClient.SelectedIndex = -1;
            ddlClient.Text = string.Empty;
            litMessage.Text = "<font color=\"red\">Please select a client.</font>";

            RadScriptManager1.SetFocus(ddlClient.ClientID + "_Input");
            return;
        }


        PopulateCombBox(ddlSite, Convert.ToInt32(ddlClient.SelectedValue));

        //ddlRegion.SelectedValue = getRegionId(Convert.ToInt32(ddlClient.SelectedValue)).ToString();

        // set focus to ddlSit
        ddlSite.SelectedIndex = -1;
        ddlSite.Text = ddlSite.EmptyMessage;

        RadScriptManager1.SetFocus(ddlSite.ClientID + "_Input");

    }

    protected void ddlSite_SelectedIndexChanged(object o, EventArgs e)
    {
        // set focus to ddlActivities
        if (ddlSite.SelectedValue == "")
        {
            ddlSite.SelectedIndex = -1;
            ddlSite.Text = string.Empty;
            litMessage.Text = "<font color=\"red\">Please select a specific site.</font>";

            RadScriptManager1.SetFocus(ddlSite.ClientID + "_Input");
            return;
        }
        //ddlActivities.Focus();
    }

    protected void ddlActivities_SelectedIndexChanged(object o, EventArgs e)
    {
        //if (ddlActivities.SelectedItem.Text.ToLower() == "leave" || ddlActivities.SelectedItem.Text.ToLower() == "Consulting Assistance via phone, email, face to face, including research time")
        if (ConfigurationManager.AppSettings["accountability.ta"].Contains(ddlActivities.SelectedItem.Value))
        {
            litContactMethod.Visible = true;
            litBudgetCode.Visible = true;
            litTEC.Visible = true;
            litAudience.Visible = true;

        }
        else
        {
            litContactMethod.Visible = false;
            litBudgetCode.Visible = true;
            litTEC.Visible = true;
            litAudience.Visible = false;
        }

    }

    protected void PopulateDropDownList(DropDownList ddControl, string spname, int key, string cacheKey, int cacheMinutes)
    {
        using (SqlCommand SQLCommand = new SqlCommand(spname, region4.Common.DataConnection.DbConnection))
        {
            SQLCommand.CommandType = CommandType.StoredProcedure;
            if (spname == "[p.Accountability.BudgetCode.Load]")
                SQLCommand.Parameters.AddWithValue("@sid", user.Sid);
            SQLCommand.Parameters.AddWithValue("@id", key);
            try
            {
                SQLCommand.Connection.Open();
                SqlDataReader SQLdr = SQLCommand.ExecuteReader(CommandBehavior.CloseConnection);

                ddControl.Items.Add(new ListItem("Please select ...", "0"));

                while (SQLdr.Read())
                {
                    ListItem li = new ListItem();
                    li.Text = SQLdr["display"].ToString();
                    li.Value = SQLdr["key"].ToString();
                    li.Attributes.Add("title", li.Text);

                    ddControl.Items.Add(li);
                }
                ddControl.Attributes.Add("onmouseover", "this.title=this.options[this.selectedIndex].title");
                HttpContext.Current.Cache.Add(cacheKey, ddControl.Items, null, DateTime.Now.AddMinutes(cacheMinutes), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }
            catch (Exception EX)
            {
                System.Web.HttpContext.Current.Response.Write(EX.Message);
            }
            finally
            {
                if (SQLCommand.Connection.State != ConnectionState.Closed)
                    SQLCommand.Connection.Close();
            }
        }
    }

    protected void PopulateDropDownList(DropDownList ddControl, ListItemCollection cachedListItems)
    {
        foreach (ListItem itm in cachedListItems)
            ddControl.Items.Add(itm);
    }

    protected void BindingData(tx_r17.BusinessObect.Accountability.Assistance assistance)
    {
        CertifyLog cLog = user.getCertifyLog(assistance.Service_Date.Year, assistance.Service_Date.Month, user.Sid);

        if (!assistance.isEditable() || (cLog != null && cLog.Certified))
        {
            disableFormControls();
        }

        txtDate.Text = assistance.Service_Date.ToShortDateString();
        ddlServiceLength.SelectedValue = assistance.Service_Hour_Id.ToString();
        ddlContactMethod.SelectedValue = assistance.Contact_Id.ToString();
        PopulateCombBox(ddlSite, assistance.Site_Id);

        ddlClient.SelectedValue = assistance.Site_Id.ToString();
        ddlClient.Text = assistance.Site_Name;
        ddlSite.SelectedValue = assistance.Room_Id.ToString();
        ddlSite.Text = assistance.Room_Name;

        ddlActivities.SelectedValue = assistance.Activity_Id.ToString();
        //ddlTravelTime.SelectedValue = assistance.Travel_Hour_Id.ToString();
        ddlFocus.SelectedValue = assistance.Focus_Id.ToString();
        ddlBudgetCode.SelectedValue = assistance.Budget_Id.ToString();
        ddlTEC.SelectedValue = assistance.TEC_Purpose_Id.ToString();
        txtComments.Text = assistance.Comments;

        AudienceListBox.Items.Clear();
        if (assistance.LocationAudiences.Count > 0)
        {
            for (int i = 0; i < assistance.LocationAudiences.Count; i++)
            {
                ListItem itm = new ListItem(assistance.LocationAudiences[i].OrganizationName + " | " + assistance.LocationAudiences[i].SiteName + " | " + assistance.LocationAudiences[i].RoomName + " | " + assistance.LocationAudiences[i].AudienceName + " | " + assistance.LocationAudiences[i].Amount.ToString(),
                    assistance.LocationAudiences[i].OrgId.ToString() + "|" + assistance.LocationAudiences[i].SiteId.ToString() + "|" + assistance.LocationAudiences[i].RoomId.ToString() + "|" + assistance.LocationAudiences[i].AudienceId.ToString() + "|" + assistance.LocationAudiences[i].Amount.ToString());

                AudienceListBox.Items.Add(itm);
            }
        }

        //lbLocationList.Items.Clear();

        //if (assistance.Organizations.Count > 0)
        //{
        //    for (int i = 0; i < assistance.Organizations.Count; i++)
        //    {
        //        ListItem itm = new ListItem(assistance.Organizations[i].OrganizationName + " | " + assistance.Sites[i].SiteName +
        //            " | " + (assistance.Rooms.Count == 0 ? "" : assistance.Rooms[i].RoomName), assistance.Organizations[i].OrgId.ToString() + "|" + assistance.Sites[i].SiteId.ToString() +
        //            "|" + (assistance.Rooms.Count == 0 ? "-1" : assistance.Rooms[i].RoomId.ToString()));

        //        lbLocationList.Items.Add(itm);
        //    }
        //}
        //performance
        string performance_served = string.Empty;
        foreach (IncreaseStudentPerformance p in assistance.IncreaseStudentPerformances)
        {
            if (performance_served == string.Empty)
            {
                if (assistance.IncreaseStudentPerformances.Count == 1)
                    performance_served = p.ItemId.ToString() + ",";
                else
                    performance_served = p.ItemId.ToString();
            }
            else
                performance_served += "," + p.ItemId.ToString();
        }

        performance.Value = performance_served;
        SetupMultiValueControl(ref lbPerformance, ref ddlPerformanceList, ref performance, performance_served);

        //creadit
        string credit_served = string.Empty;
        foreach (PostSecondaryCredit c in assistance.PostSecondaryCredits)
        {
            if (credit_served == string.Empty)
            {
                if (assistance.PostSecondaryCredits.Count == 1)
                    credit_served = c.ItemId.ToString() + ",";
                else
                    credit_served = c.ItemId.ToString();
            }
            else
                credit_served += "," + c.ItemId.ToString();
        }

        credit.Value = credit_served;
        SetupMultiValueControl(ref lbCredit, ref ddlCreditList, ref credit, credit_served);

        //integrity
        string integrity_served = string.Empty;
        foreach (FinancialIntegrity f in assistance.FinancialIntegritys)
        {
            if (integrity_served == string.Empty)
            {
                if (assistance.FinancialIntegritys.Count == 1)
                    integrity_served = f.ItemId.ToString() + ",";
                else
                    integrity_served = f.ItemId.ToString();
            }
            else
                integrity_served += "," + f.ItemId.ToString();
        }

        integrity.Value = integrity_served;
        SetupMultiValueControl(ref lbIntegrity, ref ddlIntegrityList, ref integrity, integrity_served);

        //Non-FIRST Standard Financial Assistance
        string assistance_served = string.Empty;
        foreach (tx_r17.BusinessObect.Accountability.NonFirstStandardFinancialAssistance n in assistance.NonFirstStandardFinancialAssistances)
        {
            if (assistance_served == string.Empty)
            {
                if (assistance.NonFirstStandardFinancialAssistances.Count == 1)
                    assistance_served = n.ItemId.ToString() + ",";
                else
                    assistance_served = n.ItemId.ToString();
            }
            else
                assistance_served += "," + n.ItemId.ToString();
        }

        NonFirstStandardFinancialAssistance.Value = assistance_served;
        SetupMultiValueControl(ref lbNonFirstStandardFinancialAssistance, ref ddlNonFirstStandardFinancialAssistanceList, ref NonFirstStandardFinancialAssistance, assistance_served);

        //Added by VM 6-19-2018
        string strategic_priority = string.Empty;
        foreach (tx_r17.BusinessObect.Accountability.StrategicPriority s in assistance.StrategicPrioritys)
        {
            if (strategic_priority == string.Empty)
            {
                if (assistance.StrategicPrioritys.Count == 1)
                    strategic_priority = s.ItemId.ToString() + ",";
                else
                    strategic_priority = s.ItemId.ToString();
            }
            else
                strategic_priority += "," + s.ItemId.ToString();
        }

        StrategicPriority.Value = strategic_priority;
        SetupMultiValueControl(ref lbStrategicPriority, ref ddlStrategicPriorityList, ref StrategicPriority, strategic_priority);
        cbFinance.Checked = assistance.IsSchoolFinanceRelated;
        cbExtendedLearningOpportunity.Checked = assistance.IsExtendedLearningOpportunities;
        cbCoreService.Checked = assistance.IsCoreService;
        ddlFunding.SelectedValue = assistance.FundingId.ToString();

        txtObjID.Text = assistance.ObjId.ToString();

        if (assistance.Activity_Id == 1276 || assistance.Activity_Id == 1281)
        {
            litContactMethod.Visible = false;
            litBudgetCode.Visible = false;
            litTEC.Visible = false;
            litAudience.Visible = false;
        }
        else
        {
            litContactMethod.Visible = true;
            litBudgetCode.Visible = true;
            litTEC.Visible = true;
            litAudience.Visible = true;
            escWeb.tx_r17.ObjectModel.Site site = region4.ObjectModel.ObjectProvider.ReturnSite(assistance.Site_Id) as escWeb.tx_r17.ObjectModel.Site;
            if (site.IsDidNotMeetFirst)
                panelNonFirstStandardFinancialAssistance.Visible = true;
            else
                panelNonFirstStandardFinancialAssistance.Visible = false;         
            
        }
        CK22B.Checked = assistance.Is22bMassCommunication; //Added by VM 10-2-2018
    }

    protected void ClearForm()
    {
        txtDate.Text = string.Empty;
        ddlServiceLength.SelectedIndex = -1;
        ddlClient.SelectedIndex = -1;
        ddlContactMethod.SelectedIndex = -1;
        ddlClient.Text = ddlClient.EmptyMessage;
        ddlSite.SelectedIndex = -1;
        ddlSite.Text = ddlSite.EmptyMessage;
        //ddlRegion.SelectedIndex = -1;
        ddlActivities.SelectedIndex = -1;
        //ddlTravelTime.SelectedIndex = -1;
        ddlFocus.SelectedIndex = -1;
        ddlBudgetCode.SelectedIndex = -1;
        ddlTEC.SelectedIndex = -1;

        AudienceListBox.Items.Clear();
        AudienceValue.Text = string.Empty;
        AudienceList.SelectedIndex = -1;

        cbFinance.Checked = false;
        cbExtendedLearningOpportunity.Checked = false;
        cbCoreService.Checked = false;
        lbPerformance.Items.Clear();
        lbCredit.Items.Clear();
        credit.Value = string.Empty;

        lbIntegrity.Items.Clear();
        integrity.Value = string.Empty;

        ddlFunding.SelectedIndex = -1;
        lbNonFirstStandardFinancialAssistance.Items.Clear();
        NonFirstStandardFinancialAssistance.Value = string.Empty;

        panelNonFirstStandardFinancialAssistance.Visible = false;

        txtComments.Text = string.Empty;
        txtObjID.Text = string.Empty; //Added by VM 7-25-2012
        litMessage.Text = string.Empty;

        lbStrategicPriority.Items.Clear(); //Added by VM 6-19-2018
        StrategicPriority.Value = string.Empty; //Added by VM 6-19-2018
        CK22B.Checked = false; // Added by VM 10-2-2018
    }

    protected void SetupMultiValueTextBoxControl(ref ListBox listbox, ref DropDownList dropdownlist, ref HtmlInputHidden input, string value)
    {

        listbox.Items.Clear();

        string inputValue = string.Empty;

        foreach (string item in value.Split(','))
        {
            try
            {
                if (item != string.Empty)
                {
                    string[] itm = item.Split(':');
                    ListItem li = dropdownlist.Items.FindByValue(itm[0].ToString());
                    if (li != null)
                    {
                        if (inputValue.Length == 0)
                        {
                            inputValue = item;
                        }
                        else
                        {
                            inputValue += "," + item;
                        }

                        ListItem newItem = new ListItem(li.Text + " | " + itm[1], li.Value + ":" + itm[1]);
                        listbox.Items.Add(newItem);
                    }
                }
            }
            catch
            {
            }
        }

        input.Value = inputValue;
    }

    protected void SetupMultiValueControl(ref ListBox listbox, ref DropDownList dropdownlist, ref HtmlInputHidden input, string value)
    {

        listbox.Items.Clear();

        string inputValue = string.Empty;

        foreach (string item in value.Split(','))
        {
            try
            {
                if (item != string.Empty)
                {
                    ListItem li = dropdownlist.Items.FindByValue(item);

                    if (li != null)
                    {
                        if (inputValue.Length == 0)
                        {
                            inputValue = item;
                        }
                        else
                        {
                            inputValue += "," + item;
                        }

                        ListItem newItem = new ListItem(li.Text, li.Value);
                        listbox.Items.Add(newItem);
                    }
                }
            }
            catch
            {
            }
        }

        input.Value = inputValue;
    }

    protected void lnkChangePassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("shoebox/account/password.aspx");
    }

    protected void btnNext_Click(object sender, ImageClickEventArgs e)
    {

        BindingData((tx_r17.BusinessObect.Accountability.Assistance)((tx_r17.BusinessObect.Accountability.Assistances)Session["myList"]).MoveNext((List<tx_r17.BusinessObect.Accountability.Assistance>)Session["dailyList"]));

        if (((tx_r17.BusinessObect.Accountability.Assistances)Session["myList"]).IsLastItem)
        {
            btnNext.Visible = false;
            if (!btnPrev.Visible)
                btnPrev.Visible = true;
        }
        else
        {
            if (!btnPrev.Visible)
                btnPrev.Visible = true;
        }
    }

    protected void btnPrev_Click(object sender, ImageClickEventArgs e)
    {
        BindingData(((tx_r17.BusinessObect.Accountability.Assistance)((tx_r17.BusinessObect.Accountability.Assistances)Session["myList"]).MovePrev((List<tx_r17.BusinessObect.Accountability.Assistance>)Session["dailyList"])));

        if (((tx_r17.BusinessObect.Accountability.Assistances)Session["myList"]).IsFirstItem)
        {
            btnPrev.Visible = false;
            if (!btnNext.Visible)
                btnNext.Visible = true;
        }
        else
        {
            if (!btnNext.Visible)
                btnNext.Visible = true;
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ClearForm();
        enableFormControls();
        pnlDetail.Visible = true;
        pnlReports.Visible = !pnlDetail.Visible;
        btnCopy.Visible = false;
        btnDelete.Visible = false;
        btnNext.Visible = false;
        btnPrev.Visible = false;

        btnNew.Visible = false; //Added 6-25-2012
        btnCancel.Visible = true; //Added 6-25-2012

        if (!btnSave.Enabled) //Added 7-14-2012
            btnSave.Enabled = true;

        tx_r17.BusinessObect.Accountability.Assistances assistanceList = (tx_r17.BusinessObect.Accountability.Assistances)Session["myList"];
        if (assistanceList != null)
        {
            assistanceList.Action = tx_r17.BusinessObect.Accountability.Assistances.ActionType.New;
            Session["myList"] = assistanceList;
        }

        txtDate.Text = DateTime.Today.ToShortDateString();
        //if (user.Focus_Id.ToString() == "0") // if no focus, then select the first item 
        //    ddlFocus.SelectedIndex = 1;
        //else
        //    ddlFocus.SelectedValue = user.Focus_Id.ToString();

        panelSpace.Visible = false;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string script = string.Empty;

        if (strObjId == "" && txtObjID.Text == string.Empty)
        {
            script = "<script language='javascript' ID='NeedId'>alert('Please select a record to delete.')</script>";
            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "NeedId", script, false);
            return;
        }


        tx_r17.BusinessObect.Accountability.Assistances assistanceList = (tx_r17.BusinessObect.Accountability.Assistances)Session["myList"];
        assistanceList.Action = tx_r17.BusinessObect.Accountability.Assistances.ActionType.Delete;
        tx_r17.BusinessObect.Accountability.Assistance a = assistanceList.GetAssistanceByObjId(Convert.ToInt32(txtObjID.Text));

        if (!a.isEditable())
        {
            ShowError("The closing date has passed, you cannot delete an entry.");
            btnCancel_Click(this, null);
            return;
        }

        dailyList = assistanceList.Delete(Convert.ToInt32(txtObjID.Text), Convert.ToDateTime(date));
        assistanceList.Save();
        if (dailyList.Count <= 1) //Modified by VM 7-20-2012
        {
            if (dailyList.Count == 1)
            {
                BindingData(dailyList[0]);
                pnlDetail.Visible = true;
            }
            else
                pnlDetail.Visible = false;

            pnlReports.Visible = !pnlDetail.Visible;

            btnNext.Visible = false;
            btnPrev.Visible = false;
        }
        else
        {
            Session["dailyList"] = dailyList;
            ClearForm();
            BindingData(dailyList[0]);

            btnNext.Visible = true;
            btnPrev.Visible = false;
        }

        Session["myList"] = assistanceList;

        myListDisplay = tx_r17.BusinessObect.Accountability.AssistancesDisplay.GetAssistancesDisplay(new DateTime(a.Service_Date.Year, a.Service_Date.Month, 1), new DateTime(a.Service_Date.Year, a.Service_Date.Month, 1).AddMonths(1).AddDays(-1), user.Sid);
        Session["myListDisplay"] = myListDisplay;

        firstDayofMonth = new DateTime(a.Service_Date.Year, a.Service_Date.Month, 1);
        lastDayofMonth = firstDayofMonth.AddMonths(1).AddDays(-1);

        litMessage.Text = "<font color=\"green\">The record has been deleted successfully.</font>";

        litDayHours.Text = ((tx_r17.BusinessObect.Accountability.Assistances)Session["myList"]).GetHours(a.Service_Date, a.Service_Date).ToString();
        litThisWeekHours.Text = myListDisplay.GetHours(firstDayofMonth, lastDayofMonth).ToString();

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Session["myList"] != null)
        {
            if (((tx_r17.BusinessObect.Accountability.Assistances)Session["myList"]).Action == tx_r17.BusinessObect.Accountability.Assistances.ActionType.New)
            {
                //ClearForm();
                ((tx_r17.BusinessObect.Accountability.Assistances)Session["myList"]).Action = tx_r17.BusinessObect.Accountability.Assistances.ActionType.None;
                Session["myList"] = ((tx_r17.BusinessObect.Accountability.Assistances)Session["myList"]);

                btnCopy.Visible = true;
                btnDelete.Visible = true;
                btnNew.Visible = true; //Added 6-25-2012
                btnCancel.Visible = false; //Added 6-25-2012
                pnlDetail.Visible = false;
                pnlReports.Visible = !pnlDetail.Visible;
            }
            else
            {
                if (Session["dailyList"] != null)
                {
                    foreach (tx_r17.BusinessObect.Accountability.Assistance a in (List<tx_r17.BusinessObect.Accountability.Assistance>)Session["dailyList"])
                    {
                        if (a.ObjId == (Convert.ToInt32(strObjId)))
                            BindingData(a);
                    }

                    if (((List<Assistance>)Session["dailyList"]).Count > 1) // Added 7-20-2012
                    {
                        btnNext.Visible = true;
                        btnPrev.Visible = true;
                    }
                    else
                    {
                        btnNext.Visible = false;
                        btnPrev.Visible = false;
                    }

                    btnNew.Visible = true;
                    btnDelete.Visible = true;
                    btnCopy.Visible = true;
                }

            }
        }
        else
        {
            btnNew.Visible = true;
            btnCopy.Visible = true;
            btnDelete.Visible = true;
            btnSave.Enabled = false;
        }

        btnCancel.Visible = false; //Added on 6-25-2012
        ClearForm();
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        string script = string.Empty;

        if (strObjId == "")
        {
            script = "<script language='javascript' ID='NeedId'>alert('Please select a record to copy.')</script>";
            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "NeedId", script, false);
            return;
        }
        else
        {
            enableFormControls();
            btnDelete.Visible = false;
            btnNew.Visible = false;
            btnNext.Visible = false;
            btnPrev.Visible = false;
            btnCopy.Visible = false; // Added 6-25-2012
            btnCancel.Visible = true; // Added 6-25-2012

            tx_r17.BusinessObect.Accountability.Assistances assistanceList = (tx_r17.BusinessObect.Accountability.Assistances)Session["myList"];
            assistanceList.Action = tx_r17.BusinessObect.Accountability.Assistances.ActionType.Copy;
            Session["myList"] = assistanceList;
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        tx_r17.BusinessObect.Accountability.Assistances assistanceList = (tx_r17.BusinessObect.Accountability.Assistances)Session["myList"];
        if (assistanceList == null) // Added 7-14-2012 If no records
        {
            assistanceList = tx_r17.BusinessObect.Accountability.Assistances.CreateNew();
            assistanceList.Action = tx_r17.BusinessObect.Accountability.Assistances.ActionType.New;
        }

        string script = string.Empty;

        //if (DateTime.Today < (txtDate.Text == "" ? DateTime.MinValue : Convert.ToDateTime(txtDate.Text)))
        //{
        //    ShowError("You could not insert an entry.");
        //    btnCancel_Click(this, null);
        //    return;
        //}

        if (assistanceList.Action == tx_r17.BusinessObect.Accountability.Assistances.ActionType.New || assistanceList.Action == tx_r17.BusinessObect.Accountability.Assistances.ActionType.Copy) //Add a new activity
        {
            tx_r17.BusinessObect.Accountability.Assistance a = tx_r17.BusinessObect.Accountability.Assistance.CreateNew();
            a.Service_Date = txtDate.Text == "" ? DateTime.MinValue : Convert.ToDateTime(txtDate.Text);
            if (!a.isEditable())
            {
                ShowError("The closing date has passed or a closing date has not been set for the current month, you cannot create an entry.");
                txtDate.Text = string.Empty;
                return;
            }
            a.Sid = user.Sid;
            a.User_Id = user.UserID;
            a.Room_Id = -1;
            a.Site_Id = -1;
            //region4.ObjectModel.Site s = region4.ObjectModel.ObjectProvider.ReturnSite(a.Site_Id);
            a.SiteType_Id = 0; // s.SiteTypeID;
            a.Org_Id = -1;
            a.Focus_Id = Convert.ToInt32(ddlFocus.SelectedValue);
            a.Team_Id = user.Team_Id;
            a.Department_Id = user.Department_Id;
            a.Division_Id = user.Division_Id;
            a.Service_Hour_Id = Convert.ToInt32(ddlServiceLength.SelectedValue);
            a.Service_Hour_Amount = a.GetHours(a.Service_Hour_Id);
            a.Contact_Id = Convert.ToInt32(ddlContactMethod.SelectedValue);
            a.Activity_Id = Convert.ToInt32(ddlActivities.SelectedValue);
            a.Activity_Name = ddlActivities.SelectedItem.Text;
            a.Budget_Id = Convert.ToInt32(ddlBudgetCode.SelectedValue);
            //a.Travel_Hour_Id = Convert.ToInt32(ddlTravelTime.SelectedValue);
            a.Travel_Hour_Amount = a.GetHours(a.Travel_Hour_Id);
            a.Cross_Divisional_Focus = false;
            a.TEC_Purpose_Id = Convert.ToInt32(ddlTEC.SelectedValue);
            a.ESC_Strand_Id = -1; //Convert.ToInt32(ddlESC.SelectedValue);
            a.ESC_U_Competency_Id = 0; //Convert.ToInt32(ddlCompetencies.SelectedValue);
            a.ECampus = string.Empty; //txtECampus.Text.ToString();
            a.Manager_Sid = Guid.Empty; //user.Manager_Sid;
            a.Comments = txtComments.Text;

            a.IsSchoolFinanceRelated = cbFinance.Checked ? true : false;
            a.IsExtendedLearningOpportunities = cbExtendedLearningOpportunity.Checked ? true : false;
            a.IsCoreService = cbCoreService.Checked ? true : false;
            a.FundingId = Convert.ToInt32(ddlFunding.SelectedValue);
            a.Is22bMassCommunication = CK22B.Checked ? true : false; //Added by VM 10-2-2018

            //get locations and audiences
            List<Item> list = new List<Item>(); //Added by VM 10-2-2018
            foreach (ListItem item in AudienceListBox.Items) //Added by VM 9-18-2014
            {
                string[] itm_value = item.Value.Split('|');
                string[] itm_text = item.Text.Split('|');
                if (Convert.ToInt32(itm_value[1]) == 607318) // prod 607318
                {
                    list = getSitecampusPair();
                    foreach (Item i in list)
                    {
                        LocationAudience la = LocationAudience.CreateNew();
                        la.ObjId = a.ObjId;
                        la.OrgId = itm_value[0] == "" ? 0 : Convert.ToInt32(itm_value[0]);
                        la.OrganizationName = itm_text[0].ToString();

                        la.ObjId = a.ObjId;
                        la.OrgId = itm_value[0] == "" ? 0 : Convert.ToInt32(itm_value[0]);

                        la.SiteId = i.SiteId;
                        la.SiteName = i.SiteName;
                        la.RoomId = i.RoomId;
                        la.RoomName = i.RoomName;
                        la.AudienceId = itm_value[3] == "" ? 0 : Convert.ToInt32(itm_value[3]);
                        la.AudienceName = itm_text[3].ToString();
                        la.Amount = itm_value[4] == "" ? 0 : Convert.ToInt32(itm_value[4]);

                        a.LocationAudiences.AddNew(la);
                    }
                }
                else
                {
                    LocationAudience la = LocationAudience.CreateNew();
                    la.ObjId = a.ObjId;
                    la.OrgId = itm_value[0] == "" ? 0 : Convert.ToInt32(itm_value[0]);
                    la.OrganizationName = itm_text[0].ToString();
                    la.SiteId = itm_value[1] == "" ? 0 : Convert.ToInt32(itm_value[1]);
                    la.SiteName = itm_text[1].ToString();
                    la.RoomId = itm_value[2] == "" ? 0 : Convert.ToInt32(itm_value[2]);
                    la.RoomName = itm_text[2].ToString();
                    la.AudienceId = itm_value[3] == "" ? 0 : Convert.ToInt32(itm_value[3]);
                    la.AudienceName = itm_text[3].ToString();
                    la.Amount = itm_value[4] == "" ? 0 : Convert.ToInt32(itm_value[4]);

                    a.LocationAudiences.AddNew(la);
                }
            }

            //performance
            SetupMultiValueControl(ref lbPerformance, ref ddlPerformanceList, ref performance, performance.Value);
            foreach (ListItem itm in lbPerformance.Items)
            {
                IncreaseStudentPerformance ip = IncreaseStudentPerformance.CreateNew();
                ip.ItemId = Convert.ToInt32(itm.Value);
                ip.ObjId = a.ObjId;
                ip.ItemName = itm.Text;
                a.IncreaseStudentPerformances.Add(ip);
            }

            //secondary credit
            SetupMultiValueControl(ref lbCredit, ref ddlCreditList, ref credit, credit.Value);
            foreach (ListItem itm in lbCredit.Items)
            {
                PostSecondaryCredit pc = PostSecondaryCredit.CreateNew();
                pc.ItemId = Convert.ToInt32(itm.Value);
                pc.ObjId = a.ObjId;
                pc.ItemName = itm.Text;
                a.PostSecondaryCredits.Add(pc);
            }

            //financial integrity
            SetupMultiValueControl(ref lbIntegrity, ref ddlIntegrityList, ref integrity, integrity.Value);
            foreach (ListItem itm in lbIntegrity.Items)
            {
                FinancialIntegrity fi = FinancialIntegrity.CreateNew();
                fi.ItemId = Convert.ToInt32(itm.Value);
                fi.ObjId = a.ObjId;
                fi.ItemName = itm.Text;
                a.FinancialIntegritys.Add(fi);
            }

            //Non-FIRST Standard Financial Assistance
            if (panelNonFirstStandardFinancialAssistance.Visible)
            {
                SetupMultiValueControl(ref lbNonFirstStandardFinancialAssistance, ref ddlNonFirstStandardFinancialAssistanceList, ref NonFirstStandardFinancialAssistance, NonFirstStandardFinancialAssistance.Value);
                foreach (ListItem itm in lbNonFirstStandardFinancialAssistance.Items)
                {
                    tx_r17.BusinessObect.Accountability.NonFirstStandardFinancialAssistance fa = tx_r17.BusinessObect.Accountability.NonFirstStandardFinancialAssistance.CreateNew();
                    fa.ItemId = Convert.ToInt32(itm.Value);
                    fa.ObjId = a.ObjId;
                    fa.ItemName = itm.Text;
                    a.NonFirstStandardFinancialAssistances.Add(fa);
                }
            }

            //Added by VM 6-19-2018
            SetupMultiValueControl(ref lbStrategicPriority, ref ddlStrategicPriorityList, ref StrategicPriority, StrategicPriority.Value);
            foreach (ListItem itm in lbStrategicPriority.Items)
            {
                tx_r17.BusinessObect.Accountability.StrategicPriority sp = tx_r17.BusinessObect.Accountability.StrategicPriority.CreateNew();
                sp.ItemId = Convert.ToInt32(itm.Value);
                sp.ObjId = a.ObjId;
                sp.ItemName = itm.Text;
                a.StrategicPrioritys.Add(sp);
            }
            
            a.CallCheckRules(a);
            if (a.BrokenRulesCollection.Count > 0)
            {
                System.Text.StringBuilder message = new System.Text.StringBuilder();

                foreach (Csla.Validation.BrokenRule rule in a.BrokenRulesCollection)
                    message.AppendFormat(@"{0}\n\r", rule.Description);

                ShowError(message.ToString());

                return;

            }
            else
            {
                ClearForm();

                dailyList = assistanceList.AddNew(a, a.Service_Date);

                assistanceList.Save();
                assistanceList = tx_r17.BusinessObect.Accountability.Assistances.GetAssistances(a.Service_Date, a.Service_Date, user.Sid);
                dailyList = assistanceList.GetAssistancesByDateRange(a.Service_Date, a.Service_Date);
                myListDisplay = tx_r17.BusinessObect.Accountability.AssistancesDisplay.GetAssistancesDisplay(new DateTime(a.Service_Date.Year, a.Service_Date.Month, 1), new DateTime(a.Service_Date.Year, a.Service_Date.Month, 1).AddMonths(1).AddDays(-1), user.Sid);
                Session["myListDisplay"] = myListDisplay;
                Session["myList"] = assistanceList;
                Session["dailyList"] = dailyList;

                BindingData(dailyList[dailyList.Count - 1]);
                assistanceList.Index = dailyList.Count - 1;

                txtObjID.Text = dailyList[dailyList.Count - 1].ObjId.ToString();

                if (dailyList.Count > 1) //Added by VM 7-20-2012
                {
                    btnNext.Visible = true;
                    btnPrev.Visible = true;
                }
                else
                {
                    btnNext.Visible = false;
                    btnPrev.Visible = false;
                }

                if (!btnDelete.Enabled)
                    btnDelete.Enabled = true;

                btnDelete.Visible = true;


                /*
                 * Added on 6-25-2012
                 */
                btnNew.Visible = true;

                if (!btnCopy.Enabled)
                    btnCopy.Enabled = true;

                btnCopy.Visible = true;
                btnCancel.Visible = false;

                litMessage.Text = "<font color=\"green\">The record has been added successfully.</font>";
                assistanceList.Action = tx_r17.BusinessObect.Accountability.Assistances.ActionType.Edit;

                calActivity.VisibleDate = a.Service_Date;
            }

            firstDayofMonth = new DateTime(a.Service_Date.Year, a.Service_Date.Month, 1);
            lastDayofMonth = firstDayofMonth.AddMonths(1).AddDays(-1);

            litDayHours.Text = ((tx_r17.BusinessObect.Accountability.Assistances)Session["myList"]).GetHours(a.Service_Date, a.Service_Date).ToString();
            litThisWeekHours.Text = myListDisplay.GetHours(firstDayofMonth, lastDayofMonth).ToString();
            litDate.Text = "<b>" + a.Service_Date.ToShortDateString() + "</b>";



        }
        else if (assistanceList.Action != tx_r17.BusinessObect.Accountability.Assistances.ActionType.Delete) // edit
        {
            int currPos;
            if (strObjId == "" && txtObjID.Text == string.Empty)
            {
                script = "<script language='javascript' ID='NeedId'>alert('Please select a record to update.')</script>";
                ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "NeedId", script, false);
                return;
            }

            tx_r17.BusinessObect.Accountability.Assistance a = assistanceList.GetAssistanceByObjId(Convert.ToInt32(txtObjID.Text));
            if (a.ObjId == Convert.ToInt32(txtObjID.Text))
            {
                a.Service_Date = Convert.ToDateTime(txtDate.Text);
                if (!a.isEditable())
                {
                    ShowError("The closing date has passed, you cannot update an entry.");
                    btnCancel_Click(this, null);
                    return;
                }

                a.Sid = user.Sid;
                a.User_Id = user.UserID;

                a.Room_Id = -1;
                a.Site_Id = -1;
                //region4.ObjectModel.Site s = region4.ObjectModel.ObjectProvider.ReturnSite(a.Site_Id);
                a.SiteType_Id = 0; // s.SiteTypeID;

                a.Org_Id = -1;
                a.Focus_Id = Convert.ToInt32(ddlFocus.SelectedValue);
                a.Team_Id = user.Team_Id;
                a.Department_Id = user.Department_Id;
                a.Division_Id = user.Division_Id;
                a.Service_Hour_Id = Convert.ToInt32(ddlServiceLength.SelectedValue);
                a.Service_Hour_Amount = a.GetHours(a.Service_Hour_Id);
                a.Contact_Id = Convert.ToInt32(ddlContactMethod.SelectedValue);
                a.Activity_Id = Convert.ToInt32(ddlActivities.SelectedValue);
                a.Activity_Name = ddlActivities.SelectedItem.Text;
                a.Budget_Id = Convert.ToInt32(ddlBudgetCode.SelectedValue);
                //a.Travel_Hour_Id = Convert.ToInt32(ddlTravelTime.SelectedValue);
                a.Travel_Hour_Amount = a.GetHours(a.Travel_Hour_Id);

                a.Cross_Divisional_Focus = false;
                a.TEC_Purpose_Id = Convert.ToInt32(ddlTEC.SelectedValue);
                a.ESC_Strand_Id = -1; // Convert.ToInt32(ddlESC.SelectedValue);
                a.ESC_U_Competency_Id = 0; //Convert.ToInt32(ddlCompetencies.SelectedValue);
                a.ECampus = string.Empty; // txtECampus.Text.ToString();
                a.Manager_Sid = Guid.Empty; //user.Manager_Sid;
                a.Comments = txtComments.Text;

                a.IsSchoolFinanceRelated = cbFinance.Checked ? true : false;
                a.IsExtendedLearningOpportunities = cbExtendedLearningOpportunity.Checked ? true : false;
                a.IsCoreService = cbCoreService.Checked ? true : false;
                a.FundingId = Convert.ToInt32(ddlFunding.SelectedValue);
                a.Is22bMassCommunication = CK22B.Checked ? true : false; //Added by VM 10-2-2018

                a.LocationAudiences.Clear();
                foreach (ListItem item in AudienceListBox.Items) //Added by VM 9-18-2014
                {
                    string[] itm_value = item.Value.Split('|');
                    string[] itm_text = item.Text.Split('|');

                    LocationAudience la = LocationAudience.CreateNew();
                    la.ObjId = a.ObjId;
                    la.OrgId = itm_value[0] == "" ? 0 : Convert.ToInt32(itm_value[0]);
                    la.OrganizationName = itm_text[0].ToString();
                    la.SiteId = itm_value[1] == "" ? 0 : Convert.ToInt32(itm_value[1]);
                    la.SiteName = itm_text[1].ToString();
                    la.RoomId = itm_value[2] == "" ? 0 : Convert.ToInt32(itm_value[2]);
                    la.RoomName = itm_text[2].ToString();
                    la.AudienceId = itm_value[3] == "" ? 0 : Convert.ToInt32(itm_value[3]);
                    la.AudienceName = itm_text[3].ToString();
                    la.Amount = itm_value[4] == "" ? 0 : Convert.ToInt32(itm_value[4]);

                    a.LocationAudiences.AddNew(la);
                }


                //performance
                a.IncreaseStudentPerformances.Clear();

                SetupMultiValueControl(ref lbPerformance, ref ddlPerformanceList, ref performance, performance.Value);

                foreach (ListItem itm in lbPerformance.Items)
                {
                    IncreaseStudentPerformance ip = IncreaseStudentPerformance.CreateNew();
                    ip.ItemId = Convert.ToInt32(itm.Value);
                    ip.ObjId = a.ObjId;
                    ip.ItemName = itm.Text;
                    a.IncreaseStudentPerformances.Add(ip);
                }

                //secondary credit
                a.PostSecondaryCredits.Clear();

                SetupMultiValueControl(ref lbCredit, ref ddlCreditList, ref credit, credit.Value);

                foreach (ListItem itm in lbCredit.Items)
                {
                    PostSecondaryCredit pc = PostSecondaryCredit.CreateNew();
                    pc.ItemId = Convert.ToInt32(itm.Value);
                    pc.ObjId = a.ObjId;
                    pc.ItemName = itm.Text;
                    a.PostSecondaryCredits.Add(pc);
                }

                //financial integrity
                a.FinancialIntegritys.Clear();

                SetupMultiValueControl(ref lbIntegrity, ref ddlIntegrityList, ref integrity, integrity.Value);

                foreach (ListItem itm in lbIntegrity.Items)
                {
                    FinancialIntegrity fi = FinancialIntegrity.CreateNew();
                    fi.ItemId = Convert.ToInt32(itm.Value);
                    fi.ObjId = a.ObjId;
                    fi.ItemName = itm.Text;
                    a.FinancialIntegritys.Add(fi);
                }

                //Non-FIRST Standard Financial Assistance
                if (panelNonFirstStandardFinancialAssistance.Visible)
                {
                    a.NonFirstStandardFinancialAssistances.Clear();
                    SetupMultiValueControl(ref lbNonFirstStandardFinancialAssistance, ref ddlNonFirstStandardFinancialAssistanceList, ref NonFirstStandardFinancialAssistance, NonFirstStandardFinancialAssistance.Value);
                    foreach (ListItem itm in lbNonFirstStandardFinancialAssistance.Items)
                    {
                        tx_r17.BusinessObect.Accountability.NonFirstStandardFinancialAssistance fa = tx_r17.BusinessObect.Accountability.NonFirstStandardFinancialAssistance.CreateNew();
                        fa.ItemId = Convert.ToInt32(itm.Value);
                        fa.ObjId = a.ObjId;
                        fa.ItemName = itm.Text;
                        a.NonFirstStandardFinancialAssistances.Add(fa);
                    }
                }

                //Added by VM 6-19-2018
                a.StrategicPrioritys.Clear();
                SetupMultiValueControl(ref lbStrategicPriority, ref ddlStrategicPriorityList, ref StrategicPriority, StrategicPriority.Value);
                foreach (ListItem itm in lbStrategicPriority.Items)
                {
                    tx_r17.BusinessObect.Accountability.StrategicPriority sp = tx_r17.BusinessObect.Accountability.StrategicPriority.CreateNew();
                    sp.ItemId = Convert.ToInt32(itm.Value);
                    sp.ObjId = a.ObjId;
                    sp.ItemName = itm.Text;
                    a.StrategicPrioritys.Add(sp);
                }

                a.CallCheckRules(a);

                if (a.BrokenRulesCollection.Count > 0)
                {
                    System.Text.StringBuilder message = new System.Text.StringBuilder();

                    foreach (Csla.Validation.BrokenRule rule in a.BrokenRulesCollection)
                        message.AppendFormat(@"{0}\n\r", rule.Description);


                    ShowError(message.ToString());
                    return;
                }
                else
                {
                    assistanceList.Save();

                    BindingData(a);
                    currPos = assistanceList.Index;
                    assistanceList = tx_r17.BusinessObect.Accountability.Assistances.GetAssistances(a.Service_Date, a.Service_Date, user.Sid);
                    assistanceList.Index = currPos;
                    Session["myList"] = assistanceList;
                    dailyList = assistanceList.GetAssistancesByDateRange(Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtDate.Text));
                    Session["dailyList"] = dailyList;

                    myListDisplay = tx_r17.BusinessObect.Accountability.AssistancesDisplay.GetAssistancesDisplay(new DateTime(a.Service_Date.Year, a.Service_Date.Month, 1), new DateTime(a.Service_Date.Year, a.Service_Date.Month, 1).AddMonths(1).AddDays(-1), user.Sid);
                    Session["myListDisplay"] = myListDisplay;

                    litMessage.Text = "<font color=\"green\">The record has been updated successfully.</font>";
                }

            }

            firstDayofMonth = new DateTime(a.Service_Date.Year, a.Service_Date.Month, 1);
            lastDayofMonth = firstDayofMonth.AddMonths(1).AddDays(-1);

            litDayHours.Text = ((tx_r17.BusinessObect.Accountability.Assistances)Session["myList"]).GetHours(a.Service_Date, a.Service_Date).ToString();
            litThisWeekHours.Text = myListDisplay.GetHours(firstDayofMonth, lastDayofMonth).ToString();
            litDate.Text = "<b>" + a.Service_Date.ToShortDateString() + "</b>";
        }

    }

    protected void disableFormControls()
    {
        pnlDetail.Enabled = false;

        string script = "<script language='javascript' ID='Message'>disableButtons();</script>";
        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Message", script, false);

        txtDate.Enabled = false;
        btnAdd.Enabled = false;
        btnDeleteLocationList.Enabled = false;

        //txtDate.Enabled = false;
        //ddlServiceLength.Enabled = false;
        //ddlContactMethod.Enabled = false;
        //ddlClient.Enabled = false;
        //ddlSite.Enabled = false;
        ////ddlRegion.Enabled = false;
        //ddlActivities.Enabled = false;
        ////ddlTravelTime.Enabled = false;
        //ddlFocus.Enabled = false;
        //ddlBudgetCode.Enabled = false;
        //ddlTEC.Enabled = false;
        //txtComments.Enabled = false;
        //AudienceListBox.Enabled = false;
        //AudienceList.Enabled = false;
        //AudienceValue.Enabled = false;
    }

    protected void enableFormControls()
    {
        pnlDetail.Enabled = true;

        string script = "<script language='javascript' ID='Message'>enableButtons();</script>";
        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Message", script, false);

        txtDate.Enabled = true;
        btnAdd.Enabled = true;
        btnDeleteLocationList.Enabled = true;

        //txtDate.Enabled = true;
        //ddlServiceLength.Enabled = true;
        //ddlContactMethod.Enabled = true;
        //ddlClient.Enabled = true;
        //ddlSite.Enabled = true;

        ////ddlRegion.Enabled = true;
        //ddlActivities.Enabled = true;
        ////ddlTravelTime.Enabled = true;
        //ddlFocus.Enabled = true;
        //ddlBudgetCode.Enabled = true;
        //ddlTEC.Enabled = true;
        //txtComments.Enabled = true;
        //AudienceListBox.Enabled = true;
        //AudienceList.Enabled = true;
        //AudienceValue.Enabled = true;
    }

    protected void ShowError(string Message)
    {

        string script = "<script language='javascript' ID='Message'>alert('" + Message + "')</script>";
        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Message", script, false);

    }

    protected DateTime getStartFiscalYear(DateTime date)
    {
        int month = date.Month;

        if (month >= 9)
            return Convert.ToDateTime("9/1/" + date.Year.ToString());
        else
            return Convert.ToDateTime("9/1/" + (date.Year - 1).ToString());
    }

    protected bool isDoNotMeetFirst(DateTime startDate, DateTime endDate, int siteId)
    {
        bool result = false;

        using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "[p.Accountability.Location.GetDoNotMeetFirst]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@start", startDate);
            cmd.Parameters.AddWithValue("@end", endDate);
            cmd.Parameters.AddWithValue("@siteId", siteId);
            cmd.Parameters.AddWithValue("@isDoNotMeetFirst", false).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();

                result = (bool)cmd.Parameters[3].Value;

            }
            catch (Exception exc)
            {
                System.Web.HttpContext.Current.Response.Write(exc.Message);
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

                cmd.Connection.Dispose();
            }
        }

        return result;
    }

    protected void lnkHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("default2.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int siteTypeId = -1;
        int amount;

        if (ddlClient.Text == string.Empty)
        {
            ShowError("Please select a client.");
            return;
        }

        region4.ObjectModel.Site s = region4.ObjectModel.ObjectProvider.ReturnSite(Convert.ToInt32(ddlClient.SelectedValue));

        if (ddlSite.SelectedIndex == -1)
        {
            siteTypeId = s.SiteTypeID;
            if ((siteTypeId == 521 || siteTypeId == 522) && ddlSite.Text != "No Sites")
            {
                ShowError("Please select a site.");
                return;
            }
        }

        if (AudienceList.SelectedIndex == 0)
        {
            ShowError("Please select audience.");
            return;
        }

        if (AudienceValue.Text == string.Empty || !Int32.TryParse(AudienceValue.Text, out amount))
        {
            ShowError("Invalid Served #.");
            return;
        }

        if (txtDate.Text.Trim() == "")
        {
            ShowError("Please select a service date");
            return;
        }

        ListItem itm = new ListItem(s.Organization.Name + " | " + ddlClient.Text + " | " + ddlSite.Text + " | " + AudienceList.SelectedItem.Text + " | " + AudienceValue.Text,
           s.Organization.OrganizationID.ToString() + "|" + ddlClient.SelectedValue + "|" + ddlSite.SelectedValue + "|" + AudienceList.SelectedValue + "|" + AudienceValue.Text);

        AudienceListBox.Items.Add(itm);
        ddlClient.SelectedIndex = -1;
        ddlClient.Text = string.Empty;
        ddlSite.SelectedIndex = -1;
        ddlSite.Text = string.Empty;
        AudienceList.SelectedIndex = -1;
        AudienceValue.Text = string.Empty;

        if (DisplayPanelNonFirst(AudienceListBox))
            panelNonFirstStandardFinancialAssistance.Visible = true;
        else
            panelNonFirstStandardFinancialAssistance.Visible = false;
    }

    protected bool DisplayPanelNonFirst(ListBox lb)
    {
        bool show = false;
        escWeb.tx_r17.ObjectModel.Site site;

        foreach (ListItem itm in lb.Items)
        {
            string[] myValue = itm.Value.Split('|');
            if (myValue[1] != "")
            {
                site = region4.ObjectModel.ObjectProvider.ReturnSite(Convert.ToInt32(myValue[1])) as escWeb.tx_r17.ObjectModel.Site;
                if (isDoNotMeetFirst(getStartFiscalYear(Convert.ToDateTime(txtDate.Text)), getStartFiscalYear(Convert.ToDateTime(txtDate.Text)).AddYears(1).AddDays(-1), site.SiteID))
                {
                    show = true;
                    break;
                }
            }

        }

        return show;

    }

    protected void btnDeleteLocationList_Click(object sender, EventArgs e)
    {
        ListItem itm = AudienceListBox.SelectedItem;
        AudienceListBox.Items.Remove(itm);

      
    }

    protected List<Item> getSitecampusPair() //Added by VM 10-2-2018
    {
        List<Item> myList = new List<Item>();

        using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "[p.Accountability.Location.SiteRoomPair]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                cmd.Connection.Open();
                SqlDataReader SQLdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (SQLdr.Read())
                {
                    Item itm = new Item();
                    itm.SiteId = Convert.ToInt32(SQLdr["site_id"]);
                    itm.RoomId = Convert.ToInt32(SQLdr["campus_id"]);
                    itm.SiteName = SQLdr["site_name"].ToString();
                    itm.RoomName = SQLdr["room_name"].ToString();
                    myList.Add(itm);
                }

            }
            catch (Exception exc)
            {
                System.Web.HttpContext.Current.Response.Write(exc.Message);
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

                cmd.Connection.Dispose();
            }

            return myList;
        }

    }

    protected struct Item //Added by VM 10-2-2018
    {
        private int _siteId;
        public int SiteId
        {
            get { return _siteId; }
            set { _siteId = value; }
        }

        private int _roomId;
        public int RoomId
        {
            get { return _roomId; }
            set { _roomId = value; }
        }

        private string _siteName;
        public string SiteName
        {
            get { return _siteName; }
            set { _siteName = value; }
        }

        private string _roomName;
        public string RoomName
        {
            get { return _roomName; }
            set { _roomName = value; }
        }
    }
}
