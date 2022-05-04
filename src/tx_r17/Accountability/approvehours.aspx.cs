using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Telerik.Web.UI;
using escWeb.tx_r17.ObjectModel;
using System.Net.Mail;

public partial class Accountability_approvehours : region4.escWeb.BasePage
{
   region4.ObjectModel.User CurrentUser;
   Guid selectedUser = Guid.Empty;
    int selectedmonth;
    int selectedyear;
   string selecteduseremail = string.Empty;
   bool issupervisor;
   int approvingmonth;
   int approvingyear;

    protected void Page_Load(object sender, EventArgs e)
    {      
     CurrentUser = ((region4.escWeb.BasePage)System.Web.HttpContext.Current.CurrentHandler).CurrentUser;
     if (!IsPostBack)
     {
       BindDropdownList();
       LoadMonth();
       Loadyear(); 
      }
       
    }
    private void BindDropdownList()
    {
        ddlemployees.DataSource = LoadEmployees().Tables[0];
        ddlemployees.DataValueField = "employeesid";
        ddlemployees.DataTextField = "name";
        ddlemployees.DataBind();
        ddlemployees.Items.Insert(0, new ListItem("----Select ----", "-1"));
        ddlemployees.SelectedIndex = 0;
    }
    private void Loadyear()
    {
        ddlyear.Items.Clear();
        ddlyear.Items.Add(new ListItem("Select", 0.ToString()));
        var currentYear = DateTime.Today.Year;
        for (int i = 1; i >= 0; i--)
        {
            ddlyear.Items.Add(new ListItem((currentYear - i).ToString(),(currentYear -i).ToString()));
        }
        ddlyear.SelectedValue = currentYear.ToString();
       

    }

     private void LoadMonth()
     {
            ddlmonth.Items.Add(new ListItem("Select", 0.ToString()));
            var months = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            for (int i = 0; i < months.Length-1; i++)
            {
                ddlmonth.Items.Add(new ListItem(months[i], (i+1).ToString()));
            }
            ddlmonth.SelectedValue = DateTime.Today.Month.ToString();
     }
    private void BindEmployeeHours()
    {
        lbloverride.Visible = false;
        Btn_Override.Visible = false;
        Btn_Deny.Visible = true;
        if (ddlemployees.SelectedValue != "-1" && ddlmonth.SelectedValue !="-1" && ddlyear.SelectedValue !="-1")
        {
            selectedUser = new Guid(ddlemployees.SelectedValue.ToString());
            selectedmonth = Convert.ToInt32(ddlmonth.SelectedValue);
            selectedyear = Convert.ToInt32(ddlyear.SelectedValue);
            if (CurrentUser.Sid.ToString().ToUpper() == "9AC687A7-2D9C-40A7-9291-475BEDBD9D45") // CFO
                issupervisor = false;
            else
                issupervisor = true;

            DataTable empHours;
            DataTable empapproved;
                
            LoadEmployeeHours(selectedUser,selectedmonth,selectedyear, issupervisor,out empHours,out empapproved);
             
            grdHours.DataSource = empHours;
            grdHours.DataBind();
            if (empHours.Rows.Count > 0)
            {
                String timesheetDate = Convert.ToString(empHours.Rows[0]["DateofService"]);
                approvingmonth = System.DateTime.Parse(timesheetDate).Month;
                approvingyear = System.DateTime.Parse(timesheetDate).Year;

                if (CurrentUser.Sid.ToString().ToUpper() == "9AC687A7-2D9C-40A7-9291-475BEDBD9D45") //CFO
                {
                    PanelButtons.Visible = true;
                    txtcfocomments.Visible = true;
                    Btn_Approve.Visible = true;
                    if (empapproved.Rows.Count > 0)  // Deny button should be visible for CFO even when it is approved by supervisor
                    {
                        lbloverride.Visible = true;
                        Btn_Override.Visible = false; // no need for this button anymore for CFO - request made by the customer
                        Btn_Approve.Visible = false;
                        Btn_Deny.Visible = true;
                    }
                    else
                    {
                        //lbloverride.Visible = true;
                        Btn_Override.Visible = false;// no need for this button anymore for CFO - request made by the customer
                        Btn_Approve.Visible = true;
                        Btn_Deny.Visible = true;
                    }

                }
                else
                {
                    PanelButtons.Visible = true;
                    Btn_Approve.Visible = true;
                    txtdenycomments.Visible = true;
                }

            }
            else
            {

                PanelButtons.Visible = false;
            }

        }
    }
    protected void getemployees_Click(object sender, EventArgs org)
    {
        BindEmployeeHours();
    }
   

    private DataSet LoadEmployees()
    {
         DataSet data = null;
         using (SqlCommand cmd = new SqlCommand("[p.Accountability.Employees.Load]", region4.Common.DataConnection.DbConnection))
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@sid", SqlDbType.UniqueIdentifier, 200).Value = CurrentUser.Sid;

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                using ( data = new DataSet())
                {
                    da.Fill(data);
                }
            }
         }

        return data;
     }

    private void LoadEmployeeHours(Guid employeeid,int month,int year, bool issup,out DataTable employeeHours,out DataTable isApproved)
    {
        DataSet data = null;
      using (SqlCommand cmd = new SqlCommand("[p.Accountability.Employees.LoadHours]", region4.Common.DataConnection.DbConnection))
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@employeesid", SqlDbType.UniqueIdentifier, 200).Value = employeeid;
            cmd.Parameters.Add("@month", SqlDbType.Int).Value = month;
            cmd.Parameters.Add("@year", SqlDbType.Int).Value = year;
            cmd.Parameters.Add("@issup", SqlDbType.Bit).Value = issup;
            


            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                using (data = new DataSet())
                {
                    da.Fill(data);

                }
            }
            
        }
     // return data ;
      employeeHours = data.Tables[0];
      isApproved = data.Tables[1];
    }

    protected void Btn_Approve_Click(object sender, EventArgs e)
    { 
       BindEmployeeHours();
       InsertLogData(true, null, null, approvingmonth, approvingyear,false);
       EmailEmployee_Approval();
       Page.ClientScript.RegisterStartupScript(this.GetType(), "Approved", "<script language=\"javascript\">alert('You have successfully approved the Hours...');window.location.href('approvehours.aspx');</script>");
    }

    protected void Btn_Deny_Click(object sender, EventArgs e)
    {
        BindEmployeeHours();
        if (CurrentUser.Sid.ToString().ToUpper() == "9AC687A7-2D9C-40A7-9291-475BEDBD9D45") //CFO
        {
            if (String.IsNullOrEmpty(txtcfocomments.Text))
            {
               
                Page.ClientScript.RegisterStartupScript(this.GetType(), "txtdenycomments", "<script language=\"javascript\">alert('Please enter the reason for denying the hours that are approved by the supervisor .')</script>");
            }
            else
            {
                
                InsertLogData(false, null, txtcfocomments.Text, approvingmonth, approvingyear,false);
                EmailEmployeeandSupervisor(txtcfocomments.Text);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Denied", "<script language=\"javascript\">alert('You have successfully denied the Hours ...')</script>");
            }
        }
        else
        {
            if (String.IsNullOrEmpty(txtdenycomments.Text))
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "txtdenycomments", "<script language=\"javascript\">alert('Please enter the reason for denying the hours .')</script>");
            }
            else
            {
               
                InsertLogData(false, txtdenycomments.Text, null, approvingmonth, approvingyear,false);
                EmailEmployee_Denial(txtdenycomments.Text);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Denied", "<script language=\"javascript\">alert('You have successfully denied the Hours...'); window.location.href('approvehours.aspx');</script>");
                
            }
        }
        
        
    }

    protected void BtnOverride_Click(object sender, EventArgs e)
    {
        BindEmployeeHours();
        InsertLogData(true, null, null, approvingmonth, approvingyear, true);
        EmailSupervisor_Override();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Override Approval", "<script language=\"javascript\">alert('You have successfully overrided supervisor approval...');window.location.href('default.aspx');</script>");
    }
    protected void InsertLogData(bool approve, string comments,string cfocomments, int approvmonth, int approvyear,bool overrideapprove)
    {
        selectedUser = new Guid(ddlemployees.SelectedValue.ToString());
        using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
         {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "[p.Accountability.UpdateLog]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@isapprove",approve);
            cmd.Parameters.AddWithValue("@denycomments", String.IsNullOrEmpty(comments)? DBNull.Value:(object)comments);        
            cmd.Parameters.AddWithValue("@CFOdenycomments",!(String.IsNullOrEmpty(cfocomments)) ?(object) cfocomments : DBNull.Value);
            cmd.Parameters.AddWithValue("@employeesid", selectedUser);
            cmd.Parameters.AddWithValue("@supervisor_sid",CurrentUser.Sid);
            cmd.Parameters.AddWithValue("@approvingmonth",approvmonth);
            cmd.Parameters.AddWithValue("@approvingyear", approvyear);
            cmd.Parameters.AddWithValue("@override", overrideapprove);

        
        try
          {
             cmd.Connection.Open();
             cmd.ExecuteNonQuery();
            }
         catch (Exception e)
            {
               region4.ErrorReporter.ReportError(e, System.Web.HttpContext.Current, region4.ErrorReporter.Severity.notgiven, region4.ErrorReporter.Occurance.customerWeb);
                
             }
          finally
              {
                  cmd.Connection.Close();
               }
            }
            
        }

    public static string ReturnEmailID(Guid sid)
    {
       string result;
        using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
        {
            SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "[p.Accountability.GetEmailID]";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sid", sid);
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
    protected void EmailEmployee_Approval()
    {
        selectedUser = new Guid(ddlemployees.SelectedValue.ToString());
        selecteduseremail= ReturnEmailID(selectedUser);
        

        MailMessage sendMail = new MailMessage();
        sendMail.To.Add(new MailAddress(selecteduseremail));
        sendMail.From = new MailAddress(region4.escWeb.SiteVariables.emailFrom);
        sendMail.Subject = "Certified Hours - Approved";
        sendMail.IsBodyHtml = true;
        string mailBody = "<html><Body> Your certified Hours for the month have been approved.<BR><BR>";

        sendMail.Body = mailBody;
        try
        {
            SmtpClient smtpClient = new SmtpClient(region4.escWeb.SiteVariables.emailServer);
            smtpClient.Send(sendMail);
        }
        catch
        { }
    
    }
    protected void EmailEmployeeandSupervisor(string cfocomments)
    {
        selectedUser = new Guid(ddlemployees.SelectedValue.ToString());
        selecteduseremail = ReturnEmailID(selectedUser);
        string supervisoremail = ReturnSupervisor(selectedUser);

        MailMessage sendMail = new MailMessage();
        sendMail.To.Add(new MailAddress(selecteduseremail));
        sendMail.CC.Add(new MailAddress(supervisoremail));
        sendMail.From = new MailAddress(region4.escWeb.SiteVariables.emailFrom);
        sendMail.Subject = "Certified Hours - Declined";
        sendMail.IsBodyHtml = true;

        string mailBody = "<html><Body>Thanks for your certification. Your certified Hours have been DECLINED and RETURNED to you for editing/correction by your coordinator.Please discuss with your supervisor as soon as possible.<BR><BR> Declined Notes: " + cfocomments + "<br/><br/>";
        sendMail.Body = mailBody;
        try
        {
            SmtpClient smtpClient = new SmtpClient(region4.escWeb.SiteVariables.emailServer);
            smtpClient.Send(sendMail);
        }
        catch
        { }
    }
    protected void EmailEmployee_Denial(string supervisorcomments)
    {
        selectedUser = new Guid(ddlemployees.SelectedValue.ToString());
        selecteduseremail = ReturnEmailID(selectedUser);

        MailMessage sendMail = new MailMessage();
        sendMail.To.Add(new MailAddress(selecteduseremail));
        sendMail.From = new MailAddress(region4.escWeb.SiteVariables.emailFrom);
        sendMail.Subject = "Certified Hours - Declined";
        sendMail.IsBodyHtml = true;

        string mailBody = "<html><Body>Thanks for your certification. Your certified Hours have been DECLINED and RETURNED to you for editing/correction by your coordinator.<BR><BR> Declined Notes: " + supervisorcomments + "<br/><br/>";
        sendMail.Body = mailBody;
        try
        {
            SmtpClient smtpClient = new SmtpClient(region4.escWeb.SiteVariables.emailServer);
            smtpClient.Send(sendMail);
        }
        catch
        { }
    
    }
    protected void EmailSupervisor_Override()
    {
        selectedUser = new Guid(ddlemployees.SelectedValue.ToString());
        selecteduseremail = ReturnEmailID(selectedUser);
        string supervisoremail = ReturnSupervisor(selectedUser);
        
        MailMessage sendMail = new MailMessage();
        sendMail.To.Add(new MailAddress(supervisoremail));
        sendMail.From = new MailAddress(region4.escWeb.SiteVariables.emailFrom);
        sendMail.Subject = "Approved Hours - Overridden";
        sendMail.IsBodyHtml = true;
        string mailBody = "<html><Body> Your approved Hours for the Employee "+ ddlemployees.SelectedItem.Text +"  has been overridden.<BR><BR>";

        sendMail.Body = mailBody;
        try
        {
            SmtpClient smtpClient = new SmtpClient(region4.escWeb.SiteVariables.emailServer);
            smtpClient.Send(sendMail);
        }
        catch
        { }

    }

}