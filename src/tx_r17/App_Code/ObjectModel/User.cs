using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using region4;
using region4.ObjectModel;
using System.Linq;

namespace escWeb.tx_r17.ObjectModel
{
    [Serializable]
    public class User : region4.ObjectModel.User
    {
        private int _division_id;
        public int Division_Id
        {
            get { return _division_id; }
            set { _division_id = value; }
        }

        private string _division_name;
        public string Division_Name
        {
            get { return _division_name; }
            set { _division_name = value; }
        }

        private int _department_id;
        public int Department_Id
        {
            get { return _department_id; }
            set { _department_id = value; }
        }

        private string _department_name;
        public string Department_Name
        {
            get { return _department_name; }
            set { _department_name = value; }
        }

        private int _team_id;
        public int Team_Id
        {
            get { return _team_id; }
            set { _team_id = value; }
        }

        private string _team_name;
        public string Team_Name
        {
            get { return _team_name; }
            set { _team_name = value; }
        }

        private int _focus_id;
        public int Focus_Id
        {
            get { return _focus_id; }
            set { _focus_id = value; }
        }

        private string _focus_name;
        public string Focus_Name
        {
            get { return _focus_name; }
            set { _focus_name = value; }
        }

        private Guid _manager_sid;
        public Guid Manager_Sid
        {
            get { return _manager_sid; }
            set { _manager_sid = value; }
        }

        private string _manager_first_name;
        public string Manager_FirstName
        {
            get { return _manager_first_name; }
            set { _manager_first_name = value; }
        }

        private string _manager_last_name;
        public string Manager_LastName
        {
            get { return _manager_last_name; }
            set { _manager_last_name = value; }
        }

        private List<CertifyLog> _list_certify_log = new List<CertifyLog>();
        public List<CertifyLog> CertifyLogs
        {
           get{return _list_certify_log;}
           set {_list_certify_log = value;}
        }

        private int _employee_status;
        public int EmployeeStatus
        {
            get { return _employee_status; }
            set { _employee_status = value; }
        }


        public User(int user_pk)
            : base(user_pk)
        {
        }

        public User(Guid sid)
            : base(sid)
        {
        }


        protected override void LoadCustomerInfo(System.Data.SqlClient.SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "[p.objectModel.User.CustomerSpec.Load]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sid", this.Sid);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    _division_id = Convert.ToInt32(reader["division_id"].ToString());
                    _division_name = reader["division_name"].ToString();
                    _department_id = Convert.ToInt32(reader["department_id"].ToString());
                    _department_name = reader["department_name"].ToString();
                    _team_id = Convert.ToInt32(reader["team_id"].ToString());
                    _team_name = reader["team_name"].ToString();
                    _focus_id = Convert.ToInt32(reader["focus_id"].ToString());
                    _focus_name = reader["focus_name"].ToString();
                    _manager_sid = new Guid(reader["manager_sid"].ToString());
                    _manager_first_name = reader["manager_firstname"].ToString();
                    _manager_last_name = reader["manager_lastname"].ToString();
                    _employee_status = reader["employee_status"] == DBNull.Value ? 0 : Convert.ToInt32(reader["employee_status"].ToString());

                    LoadLogDataBySid(this.Sid);
                }
                reader.Close();
            }
            cmd.CommandText = "";
            cmd.Parameters.Clear();
        }

        protected void LoadLogDataBySid(Guid sid)
        {
            using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "[p.Assistance.CertifyLog.Load]";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sid", sid);

                try
                {
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            CertifyLog certifyLog = new CertifyLog();
                            certifyLog.Key = (int)reader["Key"];
                            certifyLog.Employee_Sid = new Guid(reader["Employee_Sid"].ToString());
                            certifyLog.Approving_Month = (int)reader["Approving_Month"];
                            certifyLog.Approving_Year = (int)reader["Approving_Year"];
                            certifyLog.Certified = (bool)reader["Certified"];
                            certifyLog.Certified_Timestamp = (DateTime)(reader["Certified_Timestamp"] == DBNull.Value ? modelConstants.DefaultDateTime : reader["Certified_Timestamp"]);
                            certifyLog.ApprovedBySupervisor = (bool)reader["ApprovedBySupervisor"];
                            certifyLog.Supervisor_Sid = (Guid)(reader["Supervisor_Sid"] == DBNull.Value ? Guid.Empty : reader["Supervisor_Sid"]);
                            certifyLog.SupervisorApprovedTimestamp = (DateTime)(reader["Supervisor_Approved_Timestamp"] == DBNull.Value ? modelConstants.DefaultDateTime : reader["Supervisor_Approved_Timestamp"]);
                            certifyLog.DeniedBySupervisor = (bool)reader["DeniedBySupervisor"];
                            certifyLog.SupervisorDeniedTimestamp = (DateTime)(reader["Supervisor_Deny_Timestamp"] == DBNull.Value ? modelConstants.DefaultDateTime : reader["Supervisor_Deny_Timestamp"]);
                            certifyLog.SupervisorComments = reader["Supervisor_Comments"].ToString();
                            certifyLog.DeniedByCFO = (bool)reader["CFO_Denied"];
                            certifyLog.CFODeniedTimestamp = (DateTime)(reader["CFO_Denied_Timestamp"] == DBNull.Value ? modelConstants.DefaultDateTime : reader["CFO_Denied_Timestamp"]);
                            certifyLog.CFOComments = reader["CFO_Comments"].ToString();

                            _list_certify_log.Add(certifyLog);

                        }
                        reader.Close();
                        reader.Dispose();
                    }

                }
                catch (Exception e)
                {
                    region4.ErrorReporter.ReportError(e, System.Web.HttpContext.Current, ErrorReporter.Severity.notgiven, ErrorReporter.Occurance.objectModel);
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
        }
        

       public CertifyLog getCertifyLog(int year, int month, Guid sid)
        {
                return (from item in this.CertifyLogs
                        where item.Approving_Year == year && item.Approving_Month == month && item.Employee_Sid == sid
                        select item
                        ).FirstOrDefault();
        }

       //protected override bool SaveObject()
       //{
       //    using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
       //    {
       //        SqlCommand cmd = conn.CreateCommand();

       //        try
       //        {
       //            if (cmd.Connection.State != System.Data.ConnectionState.Open)
       //                cmd.Connection.Open();

       //            //Save Basic User Info
       //            #region Save Basic User Info

       //            cmd.CommandText = baseStoredProcedures.User.SaveUser;
       //            cmd.CommandType = System.Data.CommandType.StoredProcedure;

       //            SqlParameter SQLParameter = new SqlParameter("@return", true);
       //            SQLParameter.Direction = System.Data.ParameterDirection.ReturnValue;
       //            cmd.Parameters.Add(SQLParameter);

       //            cmd.Parameters.AddWithValue("@sid", this._sid);
       //            cmd.Parameters.AddWithValue("@firstName", System.Web.HttpUtility.HtmlDecode(this._firstName));
       //            cmd.Parameters.AddWithValue("@lastName", System.Web.HttpUtility.HtmlDecode(this._lastName));
       //            cmd.Parameters.AddWithValue("@middleName", this._middleName);
       //            cmd.Parameters.AddWithValue("@address", System.Web.HttpUtility.HtmlDecode(this._address));
       //            cmd.Parameters.AddWithValue("@city", this._city);
       //            cmd.Parameters.AddWithValue("@state", this._state);
       //            cmd.Parameters.AddWithValue("@zip", this._zip);
       //            cmd.Parameters.AddWithValue("@location_id", this._location_id);
       //            cmd.Parameters.AddWithValue("@site_id", this.Location.Site.LocationID);
       //            cmd.Parameters.AddWithValue("@org_id", this.Location.Site.Organization.LocationID);
       //            cmd.Parameters.AddWithValue("@position_id", this._primaryPosition.ItemId);
       //            cmd.Parameters.AddWithValue("@secondaryEmail", this._secondaryEmail);
       //            cmd.Parameters.AddWithValue("@homePhone", this._homePhone);
       //            cmd.Parameters.AddWithValue("@workPhone", this._workPhone);
       //            cmd.Parameters.AddWithValue("@salutation_id", this._salutation.ItemId);
       //            cmd.Parameters.AddWithValue("@gradelevel_id", this._gradeLevel.ItemId);
       //            cmd.Parameters.AddWithValue("@primaryEmail", System.Web.HttpUtility.HtmlDecode(this._primaryEmail));

       //            this.SaveCustomerInfo(cmd.Parameters);

       //            cmd.ExecuteNonQuery();

       //            if (Convert.ToBoolean(cmd.Parameters[0].Value))
       //                return false;

       //            #endregion

       //            //BING: after saving, we need to refresh the activeuser ID. 
       //            _user_pk = this.GetActiveUser(this._sid);

       //            //Load Customer Info
       //            cmd.CommandText = "";
       //            cmd.Parameters.Clear();
       //            SaveCustomerInfo(cmd);

       //            return true;
       //        }
       //        catch (Exception E)
       //        {
       //            ErrorReporter.ReportError(E, System.Web.HttpContext.Current, ErrorReporter.Severity.notgiven, ErrorReporter.Occurance.objectModel);
       //            SetLoadingException(E);
       //            return false;
       //        }
       //        finally
       //        {
       //            cmd.Connection.Close();
       //        }

       //    }
       //}
    }

    public class CertifyLog
    {
        private int _key;
        public int Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private Guid _employee_sid;
        public Guid Employee_Sid
        {
            get { return _employee_sid; }
            set { _employee_sid = value; }
        }

        private int _approving_month;
        public int Approving_Month
        {
            get { return _approving_month; }
            set { _approving_month = value; }
        }

        private int _approving_year;
        public int Approving_Year
        {
            get { return _approving_year; }
            set { _approving_year = value; }
        }

        private bool _certified;
        public bool Certified
        {
            get { return _certified; }
            set { _certified = value; }
        }

        private DateTime _certified_timestamp;
        public DateTime Certified_Timestamp
        {
            get { return _certified_timestamp; }
            set { _certified_timestamp = value; }
        }

        private bool _approvedBySupervisor;
        public bool ApprovedBySupervisor
        {
            get { return _approvedBySupervisor; }
            set { _approvedBySupervisor = value; }
        }

        private DateTime _supervisor_approved_timestamp;
        public DateTime SupervisorApprovedTimestamp
        {
            get { return _supervisor_approved_timestamp; }
            set { _supervisor_approved_timestamp = value; }
        }

        private Guid _supervisor_sid;
        public Guid Supervisor_Sid
        {
            get { return _supervisor_sid; }
            set { _supervisor_sid = value; }
        }

        private string _supervisor_comments;
        public string SupervisorComments
        {
            get { return _supervisor_comments; }
            set { _supervisor_comments = value; }
        }

        private bool _deniedBySupervisor;
        public bool DeniedBySupervisor
        {
            get { return _deniedBySupervisor; }
            set { _deniedBySupervisor = value; }
        }

        private DateTime _supervisor_denied_timestamp;
        public DateTime SupervisorDeniedTimestamp
        {
            get { return _supervisor_denied_timestamp; }
            set { _supervisor_denied_timestamp = value; }
        }

        private bool _deniedByCFO;
        public bool DeniedByCFO
        {
            get { return _deniedByCFO; }
            set { _deniedByCFO = value; }
        }

        private DateTime _cfo_denied_timestamp;
        public DateTime CFODeniedTimestamp
        {
            get { return _cfo_denied_timestamp; }
            set { _cfo_denied_timestamp = value; }
        }

        private string _cfo_comments;
        public string CFOComments
        {
            get { return _cfo_comments; }
            set { _cfo_comments = value; }
        }

        public CertifyLog()
        {
        }
        public virtual void InsertCertifyLog( Guid employeesid,int approvmonth,int approvyr,bool iscertified,DateTime certifystamp)
        {
            using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "[p.Accountability.InsertLog]";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Employee_Sid", employeesid);
                cmd.Parameters.AddWithValue("@Approving_Month", approvmonth);
                cmd.Parameters.AddWithValue("@Approving_Year", approvyr);
                cmd.Parameters.AddWithValue("@Certified", iscertified);
                cmd.Parameters.AddWithValue("@CertifiedTimestamp", certifystamp);

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
    }

}