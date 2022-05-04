using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace escWeb.tx_r17.ObjectModel
{
    /// <summary>
    /// Summary description for SessionAttendee
    /// </summary>
    [Serializable]
    public class SessionAttendeeList: region4.ObjectModel.SessionAttendeeList
    {
        public SessionAttendeeList(DataSet reader)
            : base(reader)
        {

        }
    }

}