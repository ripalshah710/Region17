using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace tx_r17.Shoebox.Evaluation
{
    /// <summary>
    /// Evaluation Form
    /// </summary>
    public class Evaluation
    {
        private string _Title, _Description;
        private bool _Completed;
        private System.Data.SqlClient.SqlConnection Connection = region4.Common.DataConnection.DbConnection;

        public Evaluation()
        {
        }

        public Evaluation(string Title, string Description, bool Completed)
        {
            _Title = Title;
            _Description = Description;
            _Completed = Completed;
        }

        public string Title
        {
            get { return this._Title; }
        }

        public string Description
        {
            get { return this._Description; }
        }

        public bool Completed
        {
            get { return this._Completed; }
        }

        public bool LoadForm(int user_id)
        {
            using (SqlCommand SQLCommand = new SqlCommand(region4.baseStoredProcedures.LegacyCode.LoadEvaluation, Connection))//this._DataProvider.GenerateConnection()))
            {
                SQLCommand.CommandType = CommandType.StoredProcedure;
                SQLCommand.Parameters.AddWithValue("@attendee_id", user_id);
                try
                {
                    SQLCommand.Connection.Open();
                    using (SqlDataReader Dr = SQLCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (Dr.Read())
                        {
                            this._Title = Dr["title"].ToString();
                            this._Description = Dr["description"].ToString();
                            this._Completed = Convert.ToBoolean((Dr["complete"].ToString()));
                        }
                    }
                    return true;
                }
                catch (Exception Ex)
                {
                    System.Web.HttpContext.Current.Response.Write(Ex.Message);
                    return false;
                }
                finally
                {
                    if (SQLCommand.Connection.State != ConnectionState.Closed)
                        SQLCommand.Connection.Close();
                }
            }
        }

        public DataSet LoadItems(int user_id)
        {
            using (SqlCommand SQLCommand = new SqlCommand(region4.baseStoredProcedures.LegacyCode.LoadEvaluationItems, Connection))//this._DataProvider.GenerateConnection()))
            {
                SQLCommand.CommandType = CommandType.StoredProcedure;
                SQLCommand.Connection.Open();
                SQLCommand.Parameters.AddWithValue("@attendee_id", user_id);
                try
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(SQLCommand);
                    da.Fill(ds, "Items");
                    return ds;
                }
                catch (Exception Ex)
                {
                    System.Web.HttpContext.Current.Response.Write(Ex.Message);
                    return null;
                }
                finally
                {
                    if (SQLCommand.Connection.State != ConnectionState.Closed)
                        SQLCommand.Connection.Close();
                }
            }
        }

        // WORK IN PROGRRESS
        /*		public string LoadResponse(int user_id, int item_id)
                {
                    using(SqlCommand SQLCommand = new SqlCommand("[/shoebox/evaluation/default/LoadResponse]", Connection))//this._DataProvider.GenerateConnection()))
                    {
                        SQLCommand.CommandType = CommandType.StoredProcedure;
                        SQLCommand.Connection.Open();
                        SQLCommand.Parameters.Add("@attendee_id",user_id);
                        SQLCommand.Parameters.Add("@item_id",item_id);
                        try
                        {
                            using(SqlDataReader dr = SQLCommand.ExecuteReader(CommandBehavior.CloseConnection))
                            {
                                if(dr.Read())
                                {
                                    return dr["response_text"].ToString();
                                }
                                return null;
                            }
                        }
                        catch(Exception Ex)
                        {
                            System.Web.HttpContext.Current.Response.Write(Ex.Message);
                            return null;
                        }
                        finally
                        {
                            if(SQLCommand.Connection.State!=ConnectionState.Closed)
                                SQLCommand.Connection.Close();
                        }
                    }
                }
        */

        public DataSet LoadSelections(int item_id)
        {
            using (SqlCommand SQLCommand = new SqlCommand(region4.baseStoredProcedures.LegacyCode.LoadEvaluationSelections, Connection))//this._DataProvider.GenerateConnection()))
            {
                SQLCommand.CommandType = CommandType.StoredProcedure;
                SQLCommand.Connection.Open();
                SQLCommand.Parameters.AddWithValue("@item_id", item_id);
                try
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(SQLCommand);
                    da.Fill(ds, "Selections");
                    return ds;
                }
                catch (Exception Ex)
                {
                    System.Web.HttpContext.Current.Response.Write(Ex.Message);
                    return null;
                }
                finally
                {
                    if (SQLCommand.Connection.State != ConnectionState.Closed)
                        SQLCommand.Connection.Close();
                }
            }
        }


        public void SaveResponse(int user_id, int item_id, string response_text)
        {
            using (SqlCommand SQLCommand = new SqlCommand(region4.baseStoredProcedures.LegacyCode.SaveEvaluationResponses, Connection))
            {
                SQLCommand.CommandType = CommandType.StoredProcedure;
                SQLCommand.Parameters.AddWithValue("@attendee_id", user_id);
                SQLCommand.Parameters.AddWithValue("@item_id", item_id);
                SQLCommand.Parameters.Add("@response_text", SqlDbType.NVarChar, 2048).Value = response_text;
                try
                {
                    SQLCommand.Connection.Open();
                    SQLCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Web.HttpContext.Current.Response.Write(ex.Message);
                }
                finally
                {
                    if (SQLCommand.Connection.State != ConnectionState.Closed)
                        SQLCommand.Connection.Close();
                }
            }
        }

        public void CompleteForm(int user_id)
        {
            using (SqlCommand SQLCommand = new SqlCommand(region4.baseStoredProcedures.LegacyCode.CompleteEvaluationForm, Connection))
            {
                SQLCommand.CommandType = CommandType.StoredProcedure;
                SQLCommand.Parameters.AddWithValue("@attendee_id", user_id);
                try
                {
                    SQLCommand.Connection.Open();
                    SQLCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Web.HttpContext.Current.Response.Write(ex.Message);
                }
                finally
                {
                    if (SQLCommand.Connection.State != ConnectionState.Closed)
                        SQLCommand.Connection.Close();
                }
            }
        }

        public int ValidateAccount(int session_id, int user_id, string last_name)
        {
            using (SqlCommand SQLCommand = new SqlCommand(region4.baseStoredProcedures.LegacyCode.EvaluationValidateAccount, Connection))
            {
                SqlParameter output = new SqlParameter();
                output.Direction = ParameterDirection.ReturnValue;
                SQLCommand.CommandType = CommandType.StoredProcedure;
                SQLCommand.Parameters.Add(output);
                SQLCommand.Parameters.AddWithValue("@session_id", session_id);
                SQLCommand.Parameters.AddWithValue("@user_id", user_id);
                SQLCommand.Parameters.Add("@last_name", System.Data.SqlDbType.NVarChar, 50).Value = last_name.ToLower().Trim();
                try
                {
                    SQLCommand.Connection.Open();
                    SQLCommand.ExecuteNonQuery();
                    return Convert.ToInt32(SQLCommand.Parameters[0].Value);
                }
                catch (Exception ex)
                {
                    System.Web.HttpContext.Current.Response.Write(ex.Message);
                    return -1;
                }
                finally
                {
                    if (SQLCommand.Connection.State != ConnectionState.Closed)
                        SQLCommand.Connection.Close();
                }
            }
        }



        //
        // End public class Evaluation
    }

    //
    // End namespace tx_r16
}
