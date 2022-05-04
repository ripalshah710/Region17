using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace escWeb.tx_r17.ObjectModel
{
    [Serializable]
    public class Location : region4.ObjectModel.Location
    {

        public Location(int location_id)
            : base(location_id)
        {
         }
        protected sealed override int HashIdentifier
        {
            get { return (int)region4.ObjectModel.BaseHashCodes.Location; }
        }
        
        private string _contactemail;

        public string ContactEmail { get { return _contactemail; } }

        protected override void LoadAdditionalData(System.Data.SqlClient.SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "[p.objectModel.location.CustomerSpec.Load]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@locationid", base.obj_id);
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    _contactemail = reader["contactemail"].ToString();

                }

                cmd.CommandText = "";
                cmd.Parameters.Clear();
            }

            catch (Exception e)
            {
                base.SetLoadingException(e);
            }
            finally
            {
                cmd.Connection.Close();
            }
        } 
    }


    [Serializable]
    public class Site : region4.ObjectModel.Site
    {
        //bool _noPOPayment = false;
        //public bool NoPOPayment { get { return _noPOPayment; } }
        private bool _isDidNotMeetFirst;
        public bool IsDidNotMeetFirst
        {
            get { return _isDidNotMeetFirst; }
            set { _isDidNotMeetFirst = value; }
        }

        public Site(int obj_id)
            : base(obj_id)
        {
        }

        protected override void LoadCustomerData(SqlDataReader reader)
        {
            // _noPOPayment = Convert.ToBoolean(reader["NoPOPayment"]);
            _isDidNotMeetFirst = (bool)reader["DidNotMeetFirst"];
        }

        //protected override void LoadCustomerData(System.Data.SqlClient.SqlCommand cmd)
        //{

        //}

        //protected override void SaveCustomerData(System.Data.SqlClient.SqlCommand cmd)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}


    }

    [Serializable]
    public class Room : region4.ObjectModel.Room
    {
        //bool _noPOPayment = false;
        //public bool NoPOPayment { get { return _noPOPayment; } }

        public Room(int obj_id)
            : base(obj_id)
        {
        }

        //protected override void LoadCustomerData(SqlDataReader reader)
        //{
        //    _noPOPayment = Convert.ToBoolean(reader["NoPOPayment"]);
        //}

        //protected override void LoadCustomerData(System.Data.SqlClient.SqlCommand cmd)
        //{

        //}

        //protected override void SaveCustomerData(System.Data.SqlClient.SqlCommand cmd)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

    }

    [Serializable]
    public class Organization : region4.ObjectModel.Organization
    {
        public Organization(int obj_id)
            : base(obj_id)
        {
        }


        //protected override void LoadCustomerData(System.Data.SqlClient.SqlCommand cmd)
        //{

        //}

        //protected override void SaveCustomerData(System.Data.SqlClient.SqlCommand cmd)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}


        //protected override bool IsEntityOrganization
        //{
        //   get { throw new Exception("The method or operation is not implemented."); }
        //}
    }


}