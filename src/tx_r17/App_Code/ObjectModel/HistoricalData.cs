using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace escWeb.tx_r17.ObjectModel
{
    /// <summary>
    /// Summary description for AttendeeInfo
    /// </summary>
    [Serializable]
    public class HistoricalData : region4.ObjectModel.HistoricalData
    {
        public HistoricalData(DataRow reader) : base(reader)
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}