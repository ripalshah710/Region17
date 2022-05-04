using System;
using System.Collections.Generic;
using System.Text;

using Csla;
using Csla.Security;
using Csla.Data;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace tx_r17.BusinessObect.Accountability
{
    [Serializable()]
    public class ConfigurationInfoList : BusinessListBase<ConfigurationInfoList, ConfigurationInfo>
    {

        #region Business Methods

        public ConfigurationInfo GetConfigurationInfo(string strMonCode, string strYearCode)
        {
            return (from item in this.Items
                    where (item.For_Month_Code == strMonCode) && (item.For_Year_Code == strYearCode)
                    select item
                  ).FirstOrDefault();
        }



        #endregion

        #region Factory Methods

        private ConfigurationInfoList()
        { /* require use of factory methods */ }

        internal static ConfigurationInfoList NewConfigurationInfoList()
        {
            return DataPortal.CreateChild<ConfigurationInfoList>();
        }

        public static ConfigurationInfoList GetConfigurationInfoList(string for_year)
        {
            return DataPortal.FetchChild<ConfigurationInfoList>(for_year);
        }

        #endregion


        #region Data Access

        protected override void Child_Create()
        {
            //
        }

        private void Child_Fetch(string for_year)
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = region4.Common.DataConnection.DbConnection)
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "ItsConfigurationInfoGet";
                    cm.Parameters.AddWithValue("@for_year", for_year);

                    SafeDataReader dr = new SafeDataReader(cm.ExecuteReader());
                    while (dr.Read())
                    {
                        this.Add(ConfigurationInfo.GetConfigurationInfo(dr));
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        #endregion

    }
}
