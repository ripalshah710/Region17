using System;
using System.Collections.Generic;
using System.Text;

using Csla;
using Csla.Security;
using System.Data.SqlClient;
using Csla.Data;
using System.Data;

namespace tx_r17.BusinessObect.Accountability
{
    [Serializable()]
    public class ConfigurationInfo : BusinessBase<ConfigurationInfo>
    {
        #region Properties



        private static PropertyInfo<int> ObjIdProperty = RegisterProperty<int>(new PropertyInfo<int>("obj_id"));
        public int ObjId
        {
            get { return GetProperty(ObjIdProperty); }
        }

        private static PropertyInfo<DateTime> ClosingDateProperty = RegisterProperty<DateTime>(new PropertyInfo<DateTime>("closing_date"));
        public DateTime ClosingDate
        {
            get { return GetProperty(ClosingDateProperty); }

        }

        private static PropertyInfo<DateTime> ModifiedDateProperty = RegisterProperty<DateTime>(new PropertyInfo<DateTime>("modified_date"));
        public DateTime ModifiedDate
        {
            get { return GetProperty(ModifiedDateProperty); }

        }

        private static PropertyInfo<Guid> ModifiedByProperty = RegisterProperty<Guid>(new PropertyInfo<Guid>("modified_by"));
        public Guid ModifiedBy
        {
            get { return GetProperty(ModifiedByProperty); }

        }

        private static PropertyInfo<bool> ActiveProperty = RegisterProperty<bool>(new PropertyInfo<bool>("active"));
        public bool Active
        {
            get { return GetProperty(ActiveProperty); }

        }

        private static PropertyInfo<int> For_MonthProperty = RegisterProperty<int>(new PropertyInfo<int>("for_month"));
        public int For_Month
        {
            get { return GetProperty(For_MonthProperty); }
        }

        private static PropertyInfo<string> For_Month_CodeProperty = RegisterProperty<string>(new PropertyInfo<string>("for_month_code"));
        public string For_Month_Code
        {
            get { return GetProperty(For_Month_CodeProperty); }
        }

        private static PropertyInfo<string> For_Month_NameProperty = RegisterProperty<string>(new PropertyInfo<string>("for_month_name"));
        public string For_Month_Name
        {
            get { return GetProperty(For_Month_NameProperty); }
        }

        private static PropertyInfo<int> For_YearProperty = RegisterProperty<int>(new PropertyInfo<int>("for_year"));
        public int For_Year
        {
            get { return GetProperty(For_YearProperty); }
        }

        private static PropertyInfo<string> For_Year_CodeProperty = RegisterProperty<string>(new PropertyInfo<string>("for_year_code"));
        public string For_Year_Code
        {
            get { return GetProperty(For_Year_CodeProperty); }
        }

        private static PropertyInfo<string> For_Year_NameProperty = RegisterProperty<string>(new PropertyInfo<string>("for_year_name"));
        public string For_Year_Name
        {
            get { return GetProperty(For_Year_NameProperty); }
        }

        private static PropertyInfo<DateTime> Time_StampProperty = RegisterProperty<DateTime>(new PropertyInfo<DateTime>("time_stamp"));
        public DateTime Time_Stamp
        {
            get { return GetProperty(Time_StampProperty); }

        }

        private static PropertyInfo<Guid> CreatedByProperty = RegisterProperty<Guid>(new PropertyInfo<Guid>("created_by"));
        public Guid CreatedBy
        {
            get { return GetProperty(CreatedByProperty); }

        }

        private static PropertyInfo<double> MinimHoursProperty = RegisterProperty<double>(new PropertyInfo<double>("minHours"));
        public double MinimHours
        {
            get { return GetProperty(MinimHoursProperty); }
        }

        #endregion

        #region Factory Methods
        private ConfigurationInfo()
        { /* require use of factory methods */ }


        public static ConfigurationInfo GetConfigurationInfo(SafeDataReader dr)
        {

            return DataPortal.FetchChild<ConfigurationInfo>(dr);
        }

        #endregion

        #region Data Access

        protected void MapFieldsFromData(SafeDataReader dr)
        {
            using (BypassPropertyChecks)
            {
                LoadProperty(ObjIdProperty, dr.GetInt32("obj_id"));
                LoadProperty(ClosingDateProperty, dr.GetSmartDate("closing_date"));
                LoadProperty(ModifiedDateProperty, dr.GetSmartDate("modified_date"));
                LoadProperty(ModifiedByProperty, dr.GetGuid("modified_by"));
                LoadProperty(ActiveProperty, dr.GetBoolean("active"));
                LoadProperty(For_MonthProperty, dr.GetInt32("for_month"));
                LoadProperty(For_Month_CodeProperty, dr.GetString("for_month_code"));
                LoadProperty(For_Month_NameProperty, dr.GetString("for_month_name"));
                LoadProperty(For_YearProperty, dr.GetInt32("for_year"));
                LoadProperty(For_Year_CodeProperty, dr.GetString("for_year_code"));
                LoadProperty(For_Year_NameProperty, dr.GetString("for_year_name"));
                LoadProperty(Time_StampProperty, dr.GetSmartDate("time_stamp"));
                LoadProperty(CreatedByProperty, dr.GetGuid("created_by"));
                LoadProperty(MinimHoursProperty, dr.GetDouble("minHours"));

            }
        }

        //protected void DataPortal_Fetch(SingleCriteria<ConfigurationInfo, string> criteria)
        //{

        //    using (SqlConnection cn = new SqlConnection(DBConnection.ConnectionString))
        //    {
        //        cn.Open();
        //        using (SqlCommand cm = cn.CreateCommand())
        //        {
        //            cm.CommandType = CommandType.StoredProcedure;
        //            cm.CommandText = "ItsConfigurationInfoGet";
        //            cm.Parameters.AddWithValue("@for_month", criteria.Value);

        //            SafeDataReader dr = new SafeDataReader(cm.ExecuteReader());
        //            while (dr.Read())
        //            {
        //                MapFieldsFromData(dr);
        //            }
        //        }
        //    }

        //}

        private void Child_Fetch(SafeDataReader dr)
        {
            MapFieldsFromData(dr);
        }


        #endregion
    }
}
