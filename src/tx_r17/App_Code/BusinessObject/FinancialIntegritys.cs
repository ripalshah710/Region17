using System;
using System.Collections.Generic;
using System.Text;

using Csla;
using Csla.Security;
using Csla.Data;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

/// <summary>
/// Summary description for Contents
/// </summary>
namespace tx_r17.BusinessObect.Accountability
{
    [Serializable()]
    public class FinancialIntegritys : BusinessListBase<FinancialIntegritys, FinancialIntegrity>
    {
        #region Business Methods

        public bool AddNew(string ItemName)
        {
            FinancialIntegrity item = FinancialIntegrity.NewFinancialIntegrity();
            item.ObjId = ((FinancialIntegrity)this.Parent).ObjId;
            item.ItemId = ((FinancialIntegrity)this.Parent).ItemId;
            item.ItemName = ItemName;
            this.Add(item);

            return true;
        }

        #endregion

        #region Factory Methods

        private FinancialIntegritys()
        { /* require use of factory methods */ }

        internal static FinancialIntegritys NewFinancialIntegritys()
        {
            return DataPortal.CreateChild<FinancialIntegritys>();
        }

        public static FinancialIntegritys GetFinancialIntegritys(int obj_id)
        {
            return DataPortal.FetchChild<FinancialIntegritys>(obj_id);
        }

        #endregion

        #region Data Access

        protected override void Child_Create()
        {
            //
        }

        public bool AddNew(FinancialIntegrity r)
        {
            this.Add(r);

            return true;
        }

        private void Child_Fetch(int obj_id)
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = region4.Common.DataConnection.DbConnection)
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "ItsFinancialIntegritysGet";
                    cm.Parameters.AddWithValue("@obj_id", obj_id);

                    SafeDataReader dr = new SafeDataReader(cm.ExecuteReader());
                    while (dr.Read())
                    {
                        this.Add(FinancialIntegrity.GetFinancialIntegrity(dr));
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