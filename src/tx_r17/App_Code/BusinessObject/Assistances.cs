using System;
using System.Collections.Generic;
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
    public class Assistances : BusinessListBase<Assistances, Assistance>
    {
        #region Properties

        private int _index = -1;
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        private bool _isLastItem;
        public bool IsLastItem
        {
            get { return _isLastItem; }
            set { _isLastItem = value; }
        }

        private bool _isFirstItem;
        public bool IsFirstItem
        {
            get { return _isFirstItem; }
            set { _isFirstItem = value; }
        }

        private bool _hasItem;
        public bool HasItem
        {
            get
            {
                if (this.Items.Count > 0)
                    _hasItem = true;
                else
                    _hasItem = false;

                return _hasItem;
            }
        }

        private double _total_hours;

        public enum ActionType
        {
            None = 0,
            New = 1,
            Copy = 2,
            Delete = 3,
            Edit = 4
        }

        private ActionType _action;
        public ActionType Action
        {
            get { return _action; }
            set { _action = value; }
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

        public List<Assistance> GetAssistancesByDateRange(DateTime startDate, DateTime endDate)
        {
            return (from item in this.Items
                    where item.Service_Date >= startDate && item.Service_Date <= endDate
                    orderby item.Service_Date ascending
                    select item
                    ).ToList();
        }

        public Assistance GetAssistanceByObjId(int ObjId)
        {
            return (from item in this.Items
                    where item.ObjId == ObjId
                    orderby item.Service_Date ascending
                    select item
                    ).FirstOrDefault();
        }

        public Assistance MoveNext(List<Assistance> list)
        {
            if (list.Count == 0) { return null; } // no records

            if (_index < list.Count - 1)
                _index++;

            if (list.Count - 1 == _index) // last record
            {
                _isLastItem = true;
            }
            else
                _isLastItem = false;

            return list[_index];

        }

        public Assistance MovePrev(List<Assistance> list)
        {
            if (list.Count == 0) { return null; } // no records

            if (_index > 0)
                _index--;

            if (_index == 0)
                _isFirstItem = true;
            else
            {
                _isFirstItem = false;
            }
            return list[_index];
        }


        public List<Assistance> AddNew(Assistance a, DateTime date)
        {
            this.Add(a);
            List<Assistance> list = this.GetAssistancesByDateRange(date, date);

            return list;
        }

        public List<Assistance> Delete(int ObjId, DateTime date)
        {
            List<Assistance> list = null;

            Assistance a = this.GetAssistanceByObjId(ObjId);
            a.LocationAudiences.Clear();

            this.Remove(a);
            _index = 0;

            return list = this.GetAssistancesByDateRange(date, date);

        }

        public double GetHours(DateTime startDate, DateTime endDate)
        {
            _total_hours = 0.0;
            foreach (Assistance ass in this.GetAssistancesByDateRange(startDate, endDate))
            {
                _total_hours += ass.Service_Length;
            }

            return _total_hours;
        }

        #endregion

        #region Factory Methods

        private Assistances()
        { /* require use of factory methods */ }

        internal static Assistances NewAssistances()
        {
            return DataPortal.Create<Assistances>();
        }

        public static Assistances CreateNew()
        {

            return NewAssistances();
        }


        public static Assistances GetAssistances(DateTime startDate, DateTime endDate, Guid sid)
        {
            AssistancesCriteria criteria = new AssistancesCriteria(startDate, endDate, sid);
            return DataPortal.Fetch<Assistances>(criteria);
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
                : base(typeof(Assistances))
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
                    cm.CommandText = "ItsAssistancesGet";
                    cm.Parameters.AddWithValue("@startDate", criteria.StartDate);
                    cm.Parameters.AddWithValue("@endDate", criteria.EndDate);
                    cm.Parameters.AddWithValue("@sid", criteria.SID);


                    SafeDataReader dr = new SafeDataReader(cm.ExecuteReader());
                    while (dr.Read())
                    {
                        this.Add(Assistance.GetAssistence(dr));
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        protected override void DataPortal_Update()
        {
            base.Child_Update(this);
        }






        #endregion

    }
}
