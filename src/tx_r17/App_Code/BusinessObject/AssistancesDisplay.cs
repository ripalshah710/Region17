using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Csla;
using Csla.Security;
using Csla.Data;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Csla.Validation;
using System.Reflection;
using Csla.Core;

namespace tx_r17.BusinessObect.Accountability
{

    [Serializable()]
    public class AssistancesDisplay : BusinessListBase<AssistancesDisplay, AssistanceDisplay>
    {
        #region Properties

        private double _total_hours;

        public enum ActionType
        {
            None = 0,
            New = 1,
            Copy = 2,
            Delete = 3,
            Edit = 4
        }

        public System.Drawing.Color GetColor(DateTime date)
        {
            double dailyHours = GetHours(date, date);

            if (dailyHours >= 8.0)
                return System.Drawing.Color.LightGreen;
            else
                return System.Drawing.Color.Orange;
        }


        #endregion

        #region Business Methods

        public List<AssistanceDisplay> GetAssistancesDisplayByDateRange(DateTime startDate, DateTime endDate)
        {
            return (from item in this.Items
                    where item.Service_Date >= startDate && item.Service_Date <= endDate
                    orderby item.Service_Date ascending
                    select item
                    ).ToList();
        }


        public double GetHours(DateTime startDate, DateTime endDate)
        {
            _total_hours = 0.0;

            foreach (AssistanceDisplay ass in this.GetAssistancesDisplayByDateRange(startDate, endDate))
            {
                _total_hours += ass.Service_Length;
            }

            return _total_hours;
        }

        public double getMinimHours(DateTime date)
        {
            ConfigurationInfoList list = ConfigurationInfoList.GetConfigurationInfoList(date.Year.ToString());
            ConfigurationInfo ci = list.GetConfigurationInfo(date.Month.ToString(), date.Year.ToString());

            return ci.MinimHours;
        }

        #endregion

        #region Factory Methods

        private AssistancesDisplay()
        { /* require use of factory methods */ }


        public static AssistancesDisplay GetAssistancesDisplay(DateTime startDate, DateTime endDate, Guid sid)
        {
            AssistancesCriteria criteria = new AssistancesCriteria(startDate, endDate, sid);
            return DataPortal.Fetch<AssistancesDisplay>(criteria);
        }

        #endregion


        #region Data Access

        protected override void DataPortal_Create()
        {
            //
        }

        [Serializable]
        internal class AssistancesCriteria : Csla.CriteriaBase
        {
            private DateTime _startDate;
            public DateTime StartDate
            {
                get { return _startDate; }
            }

            private DateTime _endDate;
            public DateTime EndDate
            {
                get { return _endDate; }
            }

            private Guid _sid;
            public Guid SID
            {
                get { return _sid; }
            }

            public AssistancesCriteria(DateTime start_date, DateTime end_date, Guid sid)
                : base(typeof(AssistancesDisplay))
            {
                _startDate = start_date;
                _endDate = end_date;
                _sid = sid;
            }
        }

        private void DataPortal_Fetch(AssistancesCriteria criteria)
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = region4.Common.DataConnection.DbConnection)
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "ItsAssistancesDisplayGet";
                    cm.Parameters.AddWithValue("@startDate", criteria.StartDate);
                    cm.Parameters.AddWithValue("@endDate", criteria.EndDate);
                    cm.Parameters.AddWithValue("@sid", criteria.SID);


                    SafeDataReader dr = new SafeDataReader(cm.ExecuteReader());
                    while (dr.Read())
                    {
                        this.Add(AssistanceDisplay.GetAssistenceDisplay(dr));
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        #endregion

    }
}
