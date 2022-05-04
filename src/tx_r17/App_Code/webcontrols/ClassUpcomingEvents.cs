using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using escWorks.BusinessObjects;
using escWeb.tx_r17.ObjectModel;
using System.Collections.Generic;
using System.Text;


namespace escWeb.tx_r17.WebControls
{
    /// <summary>
    /// Summary description for RegistrationHistory
    /// </summary>
    public class ClassUpcomingEvents : region4.WebControls.UpcomingEvents
    {
        public ClassUpcomingEvents()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void Render(HtmlTextWriter writer)
        {
            HtmlTable table;
            SortedList<DateTime, region4.ObjectModel.SessionInfo> session = ReturnSessions();
            table = RenderTable(session);
            table.RenderControl(writer);
        }

        private HtmlTable RenderTable(SortedList<DateTime, region4.ObjectModel.SessionInfo> sessions)
        {
            HtmlTable result = new HtmlTable();
            result.Style.Add("border-collapse", "collapse");
            result.Width = Unit.Pixel(200).ToString();
            result.CellPadding = 2;
            result.CellSpacing = 0;
            DateTime currentDate = new DateTime(1902, 1, 1);

            HtmlTableRow row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            result.Rows.Add(row);
            row.Cells[0].InnerHtml = String.Format("<nobr>Upcoming {0}</nobr>", region4.escWeb.SiteVariables.ObjectProvider.SessionPluralNameCapitalized);
            row.Cells[0].Attributes.Add("class", "upcomingSessionHeader");

            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            result.Rows.Add(row);

            int itemsAdded = 0;
            foreach (KeyValuePair<DateTime, region4.ObjectModel.SessionInfo> pair in sessions)
            {
                if (itemsAdded > _itemsToDisplay)
                    break;
                if (!pair.Value.DisplayOnWeb)
                    continue;
                DateTime sessionDate = new DateTime(pair.Value.StartDate.Year, pair.Value.StartDate.Month, pair.Value.StartDate.Day);
                LiteralControl control;
                if (currentDate != sessionDate)
                {
                    control = new LiteralControl(String.Format("<span class=\"mainBodyBold\">{0} {1}</span><br/>", System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)sessionDate.DayOfWeek], sessionDate.ToShortDateString()));
                    row.Cells[0].Controls.Add(control);
                }

                if (_needSpace)
                    control = new LiteralControl(String.Format("<span class=\"upcomingContent\"><a href=\"{0}catalog/session.aspx?session_id={1}\" class=\"upcomingLink\">{2}</a><br /><br/></span><br/>", ((region4.escWeb.BasePage)this.Page).PathToRoot, pair.Value.SessionID, pair.Value.Title));
                else
                    control = new LiteralControl(String.Format("<span class=\"upcomingContent\"><a href=\"{0}catalog/session.aspx?session_id={1}\" class=\"upcomingLink\">{2}</a><br /><br/></span>", ((region4.escWeb.BasePage)this.Page).PathToRoot, pair.Value.SessionID, pair.Value.Title));

                row.Cells[0].Controls.Add(control);

                itemsAdded++;
                currentDate = sessionDate;
            }

            if (itemsAdded == 0)
                row.Cells[0].Controls.Add(new LiteralControl(String.Format("<span class=\"upcomingContent\">There are no upcoming {0}</span>", region4.escWeb.SiteVariables.ObjectProvider.SessionPluralName)));

            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            result.Rows.Add(row);
            HtmlAnchor a = new HtmlAnchor();
            a.HRef = "~/catalog/calendar.aspx";

            a.InnerHtml = String.Format("<nobr>more {0}...</nobr>", region4.escWeb.SiteVariables.ObjectProvider.SessionPluralName);
            row.Cells[0].Controls.Add(a);

            a.Attributes.Add("class", "upcomingSessionFooter");

            return result;
        }
    }
}