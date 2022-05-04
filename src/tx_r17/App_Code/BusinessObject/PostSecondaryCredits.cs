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
    public class PostSecondaryCredits : BusinessListBase<PostSecondaryCredits, PostSecondaryCredit>
    {
        #region Business Methods

        public bool AddNew(string ItemName)
        {
            PostSecondaryCredit item = PostSecondaryCredit.NewPostSecondaryCredit();
            item.ObjId = ((PostSecondaryCredit)this.Parent).ObjId;
            item.ItemId = ((PostSecondaryCredit)this.Parent).ItemId;
            item.ItemName = ItemName;
            this.Add(item);

            return true;
        }

        #endregion

        #region Factory Methods

        private PostSecondaryCredits()
        { /* require use of factory methods */ }

        internal static PostSecondaryCredits NewPostSecondaryCredits()
        {
            return DataPortal.CreateChild<PostSecondaryCredits>();
        }

        public static PostSecondaryCredits GetPostSecondaryCredits(int obj_id)
        {
            return DataPortal.FetchChild<PostSecondaryCredits>(obj_id);
        }

        #endregion

        #region Data Access

        protected override void Child_Create()
        {
            //
        }

        public bool AddNew(PostSecondaryCredit r)
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
                    cm.CommandText = "ItsPostSecondaryCreditsGet";
                    cm.Parameters.AddWithValue("@obj_id", obj_id);

                    SafeDataReader dr = new SafeDataReader(cm.ExecuteReader());
                    while (dr.Read())
                    {
                        this.Add(PostSecondaryCredit.GetPostSecondaryCredit(dr));
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