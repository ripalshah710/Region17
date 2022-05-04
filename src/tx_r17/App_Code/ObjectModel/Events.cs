using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace escWeb.tx_r17.ObjectModel
{
    [Serializable]
    public class Event : region4.ObjectModel.Event
    {

        bool _allowMultiEnroll;
        public bool AllowMultiEnroll { get { return _allowMultiEnroll; } }

        public Event(int event_id)
            : base(event_id)
        {
        }

        protected override void LoadCustomerData(System.Data.SqlClient.SqlDataReader reader)
        {
            _allowMultiEnroll = (bool)reader["allowMultiEnroll"];
        }

    }

    [Serializable]
    public class Conference : region4.ObjectModel.Conference
    {
        public Conference(int conference_id) : base(conference_id)
        {

        }

        protected override void LoadCustomerData(SqlDataReader reader)
        {
            _dimensions = reader["dimensions"].ToString();
            _standards = reader["standards"].ToString();

        }
    }


    [Serializable]
    public class MultiVenue : region4.ObjectModel.MultiVenue
    {
        public MultiVenue(int event_id)
            : base(event_id)
        {

        }
    }
}