using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Telerik.Web.UI;
using escWeb.tx_r17.ObjectModel;

public partial class Search : System.Web.UI.UserControl
{
    protected void Page_Init(object sender, EventArgs e)
    {
        BindingItemList();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["SearchCriteria"] != null)
            {
                txtSearch.Text = Request.QueryString["SearchCriteria"].ToString();
            }
        }
        LoadData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!chkFF.Checked && !chkVideo.Checked)
        {
            lbError.Text = "You must select either Face-to-Face or Online.";
            lbError.Visible = true;
        }
        else
        {
            lbError.Visible = false;
            LoadData();
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtSearch.Text = string.Empty;
        chkFF.Checked = true;
        chkVideo.Checked = true;
        chkWeekend.Checked = false;
        chkFree.Checked = false;

        lbError.Visible = false;

        var collection = ddItems.CheckedItems;
        foreach (var item in collection)
            item.Checked = false;

        LoadData();
    }

    private void LoadData()
    {
        List<SessionInfo> sessionInfoList = LoadSearchSessions();
        string keywords = txtSearch.Text.Trim();

        if (ddItems.CheckedItems.Count > 0)
        {
            keywords += "~" + getSelectedItems();
        }
        grdSearch.DataSource = SearchInMemory(sessionInfoList, chkFF.Checked, chkVideo.Checked, chkFree.Checked, chkWeekend.Checked, keywords);
        grdSearch.DataBind();

        grdSearchMobile.DataSource = SearchInMemory(sessionInfoList, chkFF.Checked, chkVideo.Checked, chkFree.Checked, chkWeekend.Checked, keywords);
        grdSearchMobile.DataBind();
    }

    private static List<SessionInfo> LoadSearchSessions()
    {
        string cacheKey = String.Format("objProvider_{0}_sessioninfoList_LoadSearchSessions", region4.escWeb.SiteVariables.customer_id);

        List<SessionInfo> SessionInfoList = HttpContext.Current.Cache[cacheKey] as List<SessionInfo>;
        if (SessionInfoList == null)
        {
            using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = region4.baseStoredProcedures.Session.GetSearchSessions;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@date", DateTime.Today.ToShortDateString());

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    using (DataSet data = new DataSet())
                    {
                        da.Fill(data);

                        if (data != null)
                        {
                            SessionInfoList = new List<SessionInfo>();
                            foreach (DataRow dr in data.Tables[0].Rows)
                            {
                                SessionInfo sessionInfo = (SessionInfo)region4.ObjectModel.ObjectProvider.ReturnSessionInfo(dr);
                                SessionInfoList.Add(sessionInfo);
                            }
                        }
                    }
                }
            }
            HttpContext.Current.Cache.Add(cacheKey, SessionInfoList, null, DateTime.Now.AddSeconds(60), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
        }
        return SessionInfoList;
    }

    protected List<SessionInfo> SearchInMemory(List<SessionInfo> sessionInfoList, bool faceToFace, bool online, bool free, bool weekend, string Keywords)
    {
        List<SessionInfo> resultList = new List<SessionInfo>();
        int number;
        bool isNumber = false;
        if (Int32.TryParse(Keywords, out number))
            isNumber = true;
        foreach (SessionInfo sessionInfo in sessionInfoList)
        {
            //If Keywords is SessionID, then use DisplayOnWebOmitOnline, otherwise, use DisplayOnWeb
            bool OKtoAdd;
            if (isNumber && (sessionInfo.SessionID == number))
                OKtoAdd = sessionInfo.DisplayOnWebOmitOnline;
            else
                OKtoAdd = sessionInfo.DisplayOnWeb;

            OKtoAdd &= (faceToFace && (sessionInfo.IsFaceToFace || sessionInfo.IsConference || sessionInfo.IsMultiVenue))
                    || (online && (sessionInfo.IsOnline || sessionInfo.IsMultiVenueOnline || sessionInfo.IsVideoConference));

            if (free)
                OKtoAdd &= (sessionInfo.Fee <= 0);

            if (weekend && (sessionInfo.IsFaceToFace || sessionInfo.IsConference || sessionInfo.IsMultiVenue || sessionInfo.IsVideoConference))
                OKtoAdd &= (sessionInfo.StartDate.DayOfWeek == DayOfWeek.Saturday || sessionInfo.StartDate.DayOfWeek == DayOfWeek.Sunday);

            if (Keywords.Length > 0)
            {
                string[] SearchTerms = null;
                string[] TPESS = null;

                if (!Keywords.Contains("~"))
                    SearchTerms = Keywords.Split(' ');
                else
                {
                    string[] MixedSearchTerms = Keywords.Split('~');
                    SearchTerms = MixedSearchTerms[0].Split(' ');
                    TPESS = MixedSearchTerms[1].Split(';');

                }
                string SearchField = sessionInfo.SessionID.ToString() + "|" + sessionInfo.EventID.ToString() + "|"
                    + sessionInfo.Title + " " + sessionInfo.Description + " "
                    + sessionInfo.EventTypeDisplay + " " + sessionInfo.Subtitle + " "
                    + sessionInfo.SearchField;
                foreach (string term in SearchTerms)
                {
                    OKtoAdd &= SearchField.ToLower().Contains(term.ToLower());
                }
                if (OKtoAdd && TPESS != null)
                {
                    foreach (string tp in TPESS)
                    {
                        if (SearchField.ToLower().Contains(tp.ToLower()) && tp != "")
                        {
                            OKtoAdd = true;
                            break;
                        }
                        else
                            OKtoAdd = false;
                    }
                }
            }
            if (OKtoAdd && sessionInfo.IsConference)//Conference only add once
            {
                foreach (SessionInfo temp in resultList)
                {
                    if (temp.EventID == sessionInfo.EventID)
                    {
                        OKtoAdd = false;
                        break;
                    }
                }
            }

            if (OKtoAdd)
                resultList.Add(sessionInfo);
        }
        return resultList;
    }

    private void BindingItemList()
    {
        DataTable clients = new DataTable();
        using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "[p.event.dimensions.load]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using (SqlDataAdapter SqlDa = new SqlDataAdapter(cmd))
            {
                SqlDa.Fill(clients);

                foreach (DataRow row in clients.Rows)
                {
                    RadComboBoxItem item = new RadComboBoxItem(row["display"].ToString(), row["id"].ToString());
                    ddItems.Items.Add(item);
                }
            }
        }
    }

    protected string getSelectedItems()
    {
        string sValues = "";

        foreach (RadComboBoxItem checkeditem in ddItems.CheckedItems)
        {
            string _value = checkeditem.Text.Trim();
            if (Convert.ToInt32(checkeditem.Value) < 9000)
                sValues += _value + ";";
        }

        return sValues;
    }
}
