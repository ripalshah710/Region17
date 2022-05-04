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
    public class BudgetInfo : BusinessBase<BudgetInfo>
    {
        #region Properties

        private static BudgetInfo instance;

        private static PropertyInfo<int> ObjIdProperty = RegisterProperty<int>(new PropertyInfo<int>("obj_id"));
        public int ObjId
        {
            get { return GetProperty(ObjIdProperty); }
        }

        private static PropertyInfo<string> AccountNumberProperty = RegisterProperty<string>(new PropertyInfo<string>("accountnumber"));
        public string AccountNumber
        {
            get { return GetProperty(AccountNumberProperty); }
        }

        private static PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(new PropertyInfo<string>("description"));
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
        }

        private static PropertyInfo<bool> ActiveProperty = RegisterProperty<bool>(new PropertyInfo<bool>("active"));
        public bool Active
        {
            get { return GetProperty(ActiveProperty); }

        }

        private static PropertyInfo<bool> DeletedProperty = RegisterProperty<bool>(new PropertyInfo<bool>("deleted"));
        public bool Deleted
        {
            get { return GetProperty(DeletedProperty); }

        }

        #endregion

        #region Factory Methods
        private BudgetInfo()
        { /* require use of factory methods */ }


        public static BudgetInfo GetBudgetInfoByObjId(int objId)
        {

            instance = DataPortal.Fetch<BudgetInfo>(new SingleCriteria<BudgetInfo, int>(objId));

            return instance;
        }

        #endregion

        #region Data Access

        protected void MapFieldsFromData(SafeDataReader dr)
        {
            using (BypassPropertyChecks)
            {
                LoadProperty(ObjIdProperty, dr.GetInt32("obj_id"));
                LoadProperty(AccountNumberProperty, dr.GetString("accountnumber"));
                LoadProperty(DescriptionProperty, dr.GetString("description"));
                LoadProperty(ActiveProperty, dr.GetBoolean("active"));
                LoadProperty(DeletedProperty, dr.GetBoolean("deleted"));
            }
        }

        protected void DataPortal_Fetch(SingleCriteria<BudgetInfo, int> criteria)
        {

            using (SqlConnection cn = region4.Common.DataConnection.DbConnection)
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "ItsBudgetInfoGet";
                    cm.Parameters.AddWithValue("@objId", criteria.Value);

                    SafeDataReader dr = new SafeDataReader(cm.ExecuteReader());
                    while (dr.Read())
                    {
                        MapFieldsFromData(dr);
                    }
                }
            }

        }

        #endregion
    }
}
