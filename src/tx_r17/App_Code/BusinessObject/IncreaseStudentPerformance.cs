using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

using Csla;
using Csla.Security;
using Csla.Data;
/// <summary>
/// Summary description for Content
/// </summary>
namespace tx_r17.BusinessObect.Accountability
{
    [Serializable()]
    public class IncreaseStudentPerformance : BusinessBase<IncreaseStudentPerformance>
    {
        #region Properties

        private static PropertyInfo<int> KeyProperty = RegisterProperty<int>(new PropertyInfo<int>("key"));
        public int Key
        {
            get { return GetProperty(KeyProperty); }
            set { SetProperty(KeyProperty, value); }
        }

        private static PropertyInfo<int> ObjIdProperty = RegisterProperty<int>(new PropertyInfo<int>("obj_id"));
        public int ObjId
        {
            get { return GetProperty(ObjIdProperty); }
            set { SetProperty(ObjIdProperty, value); }
        }

        private static PropertyInfo<int> ItemIdProperty = RegisterProperty<int>(new PropertyInfo<int>("item_id"));
        public int ItemId
        {
            get { return GetProperty(ItemIdProperty); }
            set { SetProperty(ItemIdProperty, value); }
        }

        private static PropertyInfo<string> ItemNameProperty = RegisterProperty<string>(new PropertyInfo<string>("item_name"));
        public string ItemName
        {
            get { return GetProperty(ItemNameProperty); }
            set { SetProperty(ItemNameProperty, value); }
        }

        #endregion

        #region Factory Methods

        private IncreaseStudentPerformance()
        { /* require use of factory methods */ }

        internal static IncreaseStudentPerformance NewIncreaseStudentPerformance()
        {
            return DataPortal.CreateChild<IncreaseStudentPerformance>();
        }

        internal static IncreaseStudentPerformance GetIncreaseStudentPerformance(SafeDataReader dr)
        {

            return DataPortal.FetchChild<IncreaseStudentPerformance>(dr);
        }

        #endregion

        #region DataAccess
        protected override void Child_Create()
        {
            using (BypassPropertyChecks)
            {
                this.Key = -1;
            }
        }

        public static IncreaseStudentPerformance CreateNew()
        {
            return NewIncreaseStudentPerformance();
        }

        private void Child_Fetch(SafeDataReader dr)
        {
            MapFieldsFromData(dr);
        }

        protected void MapFieldsFromData(SafeDataReader dr)
        {
            using (BypassPropertyChecks)
            {
                LoadProperty(KeyProperty, dr.GetInt32("key"));
                LoadProperty(ObjIdProperty, dr.GetInt32("obj_id"));
                LoadProperty(ItemIdProperty, dr.GetInt32("item_id"));
                LoadProperty(ItemNameProperty, dr.GetString("item_name"));

            }
        }

        protected void Child_Insert(Assistance assistance, SqlConnection cn)
        {
            if (this.IsNew)
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    this.ObjId = assistance.ObjId;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ItsIncreaseStudentPerformancesInsert";
                    cmd.Parameters.AddWithValue("@key", this.Key).Direction = ParameterDirection.Output;

                    this.MapFieldsToParams(cmd);
                    cmd.ExecuteNonQuery();
                    this.Key = Convert.ToInt32(cmd.Parameters["@key"].Value);
                }
            }
        }

        protected void Child_DeleteSelf(escWorks.BusinessObjects.Assistances a)
        {
            if (this.IsDeleted)
            {
                using (SqlConnection cn = region4.Common.DataConnection.DbConnection)
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ItsIncreaseStudentPerformancesDelete";
                        cmd.Parameters.AddWithValue("@obj_id", this.ObjId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void MapFieldsToParams(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@obj_id", ObjId);
            cmd.Parameters.AddWithValue("@item_id", ItemId);
        }

        #endregion

    }
}