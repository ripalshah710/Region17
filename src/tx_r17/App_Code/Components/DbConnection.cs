using System;
using System.Collections.Generic;
using System.Text;

    public class DbConnection
    {
        public static System.Data.SqlClient.SqlConnection Connection
        {
            get
            {
                return new System.Data.SqlClient.SqlConnection(@"server=livedb1\escdb;integrated security=true;database=_modelstandard;");
            }
        }
    }
