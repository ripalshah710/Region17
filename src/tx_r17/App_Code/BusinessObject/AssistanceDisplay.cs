using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Csla;
using Csla.Security;
using System.Data.SqlClient;
using System.Data;
using Csla.Data;

namespace tx_r17.BusinessObect.Accountability
{
    [Serializable()]
    public class AssistanceDisplay : BusinessBase<AssistanceDisplay>
    {
        #region Properties

        private static PropertyInfo<int> ObjIdProperty = RegisterProperty<int>(new PropertyInfo<int>("obj_id"));
        public int ObjId
        {
            get { return GetProperty(ObjIdProperty); }

        }

        private static PropertyInfo<Guid> SidProperty = RegisterProperty<Guid>(new PropertyInfo<Guid>("sid"));
        public Guid Sid
        {
            get { return GetProperty(SidProperty); }

        }

        private static PropertyInfo<int> Service_Hour_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("service_hour_id"));
        public int Service_Hour_Id
        {
            get { return GetProperty(Service_Hour_IdProperty); }
            set { SetProperty(Service_Hour_IdProperty, value); }
        }

        private static PropertyInfo<double> Service_LengthProperty = RegisterProperty<double>(new PropertyInfo<double>("totHours"));
        public double Service_Length
        {
            get { return GetProperty(Service_LengthProperty); }
        }


        private static PropertyInfo<DateTime> Service_DateProperty = RegisterProperty<DateTime>(new PropertyInfo<DateTime>("date"));
        public DateTime Service_Date
        {
            get { return GetProperty(Service_DateProperty); }
            set
            {
                SetProperty(Service_DateProperty, value);

            }
        }

        private static PropertyInfo<int> Travel_Hour_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("travel_hour_id"));
        public int Travel_Hour_Id
        {
            get { return GetProperty(Travel_Hour_IdProperty); }
            set { SetProperty(Travel_Hour_IdProperty, value); }
        }

        protected static PropertyInfo<int> IsTAProperty = RegisterProperty<int>(new PropertyInfo<int>("isTA"));
        public int IsTA
        {
            get { return GetProperty(IsTAProperty); }
            set { SetProperty(IsTAProperty, value); }
        }

        //private double _service_hour_amount;
        //public double Service_Hour_Amount
        //{
        //    get { return _service_hour_amount; }
        //    set { _service_hour_amount = value; }

        //}

        //private double _travel_hour_amount;
        //public double Travel_Hour_Amount
        //{
        //    get { return _travel_hour_amount; }
        //    set { _travel_hour_amount = value; }

        //}

        #endregion
        #region Business Rules

        #endregion


        #region Factory Methods
        private AssistanceDisplay()
        { /* require use of factory methods */ }



        public static AssistanceDisplay GetAssistenceDisplay(SafeDataReader dr)
        {
            return DataPortal.FetchChild<AssistanceDisplay>(dr);
        }

        #endregion

        #region Data Access


        protected void MapFieldsFromData(SafeDataReader dr)
        {
            using (BypassPropertyChecks)
            {
                LoadProperty(ObjIdProperty, dr.GetInt32("obj_id"));
                LoadProperty(Service_Hour_IdProperty, dr.GetInt32("service_hour_id"));
                LoadProperty(Service_LengthProperty, dr.GetDouble("totHours"));
                LoadProperty(Service_DateProperty, dr.GetSmartDate("date", true));
                //LoadProperty(IsTAProperty, dr.GetInt32("isTA"));
            }
        }

        private void Child_Fetch(SafeDataReader dr)
        {
            MapFieldsFromData(dr);

            //_service_hour_amount = GetHours(dr.GetInt32("service_hour_id"));
            //_travel_hour_amount = GetHours(dr.GetInt32("travel_hour_id"));
        }

        //public double GetHours(int id)
        //{
        //    double amount = 0.0;

        //    using (SqlConnection cn = region4.Common.DataConnection.DbConnection)
        //    {
        //        cn.Open();
        //        using (SqlCommand cmd = cn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "ItsHoursAmountGet";
        //            cmd.Parameters.AddWithValue("@id", id);
        //            cmd.Parameters.AddWithValue("@amount", amount).Direction = ParameterDirection.Output;
        //            cmd.ExecuteNonQuery();
        //            amount = Convert.ToDouble(cmd.Parameters["@amount"].Value);
        //        }

        //    }

        //    return amount;
        //}

        public bool isEditable()
        {
            ConfigurationInfoList list = ConfigurationInfoList.GetConfigurationInfoList(Service_Date.Year.ToString());
            ConfigurationInfo ci = list.GetConfigurationInfo(Service_Date.Month.ToString(), Service_Date.Year.ToString());
            if (ci == null) //Do not set a close date
                return false;
            else
            {
                if (DateTime.Today > ci.ClosingDate) // Closing date is over/no closing date is set
                    return false;
                else
                    return true;
            }
        }

        #endregion
    }
}
