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
    public class LocationAudiences : BusinessListBase<LocationAudiences, LocationAudience>
    {
        #region Business Methods

        public bool AddNew(string organizationName, string siteName, string roomName, string audienceName)
        {
            LocationAudience item = LocationAudience.NewLocationAudience();
            item.ObjId = ((LocationAudience)this.Parent).ObjId;
            item.OrgId = ((LocationAudience)this.Parent).OrgId;
            item.OrganizationName = organizationName;
            item.SiteId = ((LocationAudience)this.Parent).SiteId;
            item.SiteName = siteName;
            item.RoomId = ((LocationAudience)this.Parent).RoomId;
            item.RoomName = roomName;
            item.AudienceId = ((LocationAudience)this.Parent).AudienceId;
            item.AudienceName = audienceName;
            item.Amount = ((LocationAudience)this.Parent).Amount;

            this.Add(item);

            return true;
        }

        #endregion

        #region Factory Methods

        private LocationAudiences()
        { /* require use of factory methods */ }

        internal static LocationAudiences NewLocationAudiences()
        {
            return DataPortal.CreateChild<LocationAudiences>();
        }

        public static LocationAudiences GeLocationAudiences(int obj_id)
        {
            return DataPortal.FetchChild<LocationAudiences>(obj_id);
        }

        #endregion


        #region Data Access

        protected override void Child_Create()
        {
            //
        }

        public bool AddNew(LocationAudience o)
        {
            this.Add(o);

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
                    cm.CommandText = "ItsLocationAudiencesGet";
                    cm.Parameters.AddWithValue("@obj_id", obj_id);

                    SafeDataReader dr = new SafeDataReader(cm.ExecuteReader());
                    while (dr.Read())
                    {
                        this.Add(LocationAudience.GetLocationAudience(dr));
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
