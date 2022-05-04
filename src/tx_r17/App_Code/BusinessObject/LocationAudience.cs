using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

using Csla;
using Csla.Security;
using Csla.Data;

namespace tx_r17.BusinessObect.Accountability
{
    [Serializable()]
    public class LocationAudience : BusinessBase<LocationAudience>
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

        private static PropertyInfo<int> OrgIdProperty = RegisterProperty<int>(new PropertyInfo<int>("org_id"));
        public int OrgId
        {
            get { return GetProperty(OrgIdProperty); }
            set { SetProperty(OrgIdProperty, value); }
        }

        private static PropertyInfo<string> OrganizationNameProperty = RegisterProperty<string>(new PropertyInfo<string>("org_name"));
        public string OrganizationName
        {
            get { return GetProperty(OrganizationNameProperty); }
            set { SetProperty(OrganizationNameProperty, value); }
        }

        private static PropertyInfo<int> SiteIdProperty = RegisterProperty<int>(new PropertyInfo<int>("site_id"));
        public int SiteId
        {
            get { return GetProperty(SiteIdProperty); }
            set { SetProperty(SiteIdProperty, value); }
        }

        private static PropertyInfo<string> SiteNameProperty = RegisterProperty<string>(new PropertyInfo<string>("site_name"));
        public string SiteName
        {
            get { return GetProperty(SiteNameProperty); }
            set { SetProperty(SiteNameProperty, value); }
        }

        private static PropertyInfo<int> RoomIdProperty = RegisterProperty<int>(new PropertyInfo<int>("room_id"));
        public int RoomId
        {
            get { return GetProperty(RoomIdProperty); }
            set { SetProperty(RoomIdProperty, value); }
        }

        private static PropertyInfo<string> RoomNameProperty = RegisterProperty<string>(new PropertyInfo<string>("room_name"));
        public string RoomName
        {
            get { return GetProperty(RoomNameProperty); }
            set { SetProperty(RoomNameProperty, value); }
        }

        private static PropertyInfo<int> AudienceIdProperty = RegisterProperty<int>(new PropertyInfo<int>("audience_id"));
        public int AudienceId
        {
            get { return GetProperty(AudienceIdProperty); }
            set { SetProperty(AudienceIdProperty, value); }
        }

        private static PropertyInfo<string> AudienceNameProperty = RegisterProperty<string>(new PropertyInfo<string>("audience_name"));
        public string AudienceName
        {
            get { return GetProperty(AudienceNameProperty); }
            set { SetProperty(AudienceNameProperty, value); }
        }

        private static PropertyInfo<int> AmountProperty = RegisterProperty<int>(new PropertyInfo<int>("amount"));
        public int Amount
        {
            get { return GetProperty(AmountProperty); }
            set { SetProperty(AmountProperty, value); }
        }

        #endregion

        #region Factory Methods

        private LocationAudience()
        { /* require use of factory methods */ }

        internal static LocationAudience NewLocationAudience()
        {
            return DataPortal.CreateChild<LocationAudience>();
        }

        internal static LocationAudience GetLocationAudience(SafeDataReader dr)
        {

            return DataPortal.FetchChild<LocationAudience>(dr);
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

        public static LocationAudience CreateNew()
        {
            return NewLocationAudience();
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
                LoadProperty(OrgIdProperty, dr.GetInt32("org_id"));
                LoadProperty(OrganizationNameProperty, dr.GetString("org_name"));
                LoadProperty(SiteIdProperty, dr.GetInt32("site_id"));
                LoadProperty(SiteNameProperty, dr.GetString("site_name"));
                LoadProperty(RoomIdProperty, dr.GetInt32("room_id"));
                LoadProperty(RoomNameProperty, dr.GetString("room_name"));
                LoadProperty(AudienceIdProperty, dr.GetInt32("audience_id"));
                LoadProperty(AudienceNameProperty, dr.GetString("audience_name"));
                LoadProperty(AmountProperty, dr.GetInt32("amount"));
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
                    cmd.CommandText = "ItsLocationAudiencesInsert";
                    cmd.Parameters.AddWithValue("@key", this.Key).Direction = ParameterDirection.Output;

                    this.MapFieldsToParams(cmd);
                    cmd.ExecuteNonQuery();
                    this.Key = Convert.ToInt32(cmd.Parameters["@key"].Value);
                }
            }
        }

        protected void Child_DeleteSelf(Assistances a)
        {
            if (this.IsDeleted)
            {
                using (SqlConnection cn = region4.Common.DataConnection.DbConnection)
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ItsLocationAudiencesDelete";
                        cmd.Parameters.AddWithValue("@obj_id", this.ObjId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void MapFieldsToParams(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@obj_id", ObjId);
            cmd.Parameters.AddWithValue("@org_id", OrgId);
            cmd.Parameters.AddWithValue("@site_id", SiteId);
            cmd.Parameters.AddWithValue("@room_id", RoomId);
            cmd.Parameters.AddWithValue("@audience_id", AudienceId);
            cmd.Parameters.AddWithValue("@amount", Amount);
        }

        #endregion
    }
}