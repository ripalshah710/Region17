using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ScheduleHelper
/// </summary>
namespace escWeb.tx_r17.ObjectModel
{
    [Serializable]
    public class ScheduleHelper : region4.ObjectModel.ScheduleHelper
    {
        public ScheduleHelper()
            : base()
        {

        }
        //public override string Locations
        //{
        //    get
        //    {
        //        string result = "";
        //        List<Room> itemsToRemove = new List<Room>();
        //        List<Room> copy = new List<Room>();
        //        foreach (Room r in locations)
        //            copy.Add(r);

        //        while (copy.Count > 0)
        //        {
        //            Room location = copy[0];
        //            itemsToRemove.Add(location);
        //            foreach (Room l in copy)
        //                if (l == location)
        //                    continue;
        //                else if (l.Site == location.Site)
        //                    itemsToRemove.Add(l);
        //            string temp = "";
        //            foreach (Room r in itemsToRemove)
        //            {
        //                if (r.ExceptionOccurred)
        //                    continue;
        //                if (temp.Length == 0)
        //                {
        //                    if (r.RoomTypeID == 20009)
        //                    {
        //                        temp += r.Name + (r.Site.Name == "Region 9 ESC" ? ", Online Course" : ", " + r.Name);
        //                        break;
        //                    }
        //                    else

        //                        temp += r.Site.IsServiceCenter ? r.Site.Name + " - " + r.Name : r.Name;
        //                }
        //                else
        //                    temp += ", " + r.Name;
        //            }
        //            if (
        //                 (location.Site.IsServiceCenter && (!String.IsNullOrEmpty(location.Site.Address1) && !String.IsNullOrEmpty(location.Site.City) && !String.IsNullOrEmpty(location.Site.Zip)))
        //                   || (!location.Site.IsServiceCenter && (!String.IsNullOrEmpty(location.Address1) && !String.IsNullOrEmpty(location.City) && !String.IsNullOrEmpty(location.Zip))))
        //            {
        //                temp += String.Format("<br /><a style\"font-size: 8pt; font-style:italic;\">{0}, {1}, {2}</a><br />", location.Site.IsServiceCenter ? location.Site.Address1 : location.Address1,
        //                    location.Site.IsServiceCenter ? location.Site.City : location.City,
        //                    location.Site.IsServiceCenter ? location.Site.Zip : location.Zip);
        //            }

        //            result += temp;
        //            foreach (Room r in itemsToRemove)
        //                copy.Remove(r);
        //        }
        //        return result;
        //    }

        //}

        public override string Locations
        {
            get
            {
                string result = "";
                List<Room> itemsToRemove = new List<Room>();
                List<Room> copy = new List<Room>();
                foreach (Room r in locations)
                    copy.Add(r);

                while (copy.Count > 0)
                {
                    Room location = copy[0];
                    itemsToRemove.Add(location);
                    foreach (Room l in copy)
                        if (l == location)
                            continue;
                        else if (l.Site == location.Site)
                            itemsToRemove.Add(l);
                    string temp = "";
                    foreach (Room r in itemsToRemove)
                    {
                        if (r.ExceptionOccurred)
                            continue;
                        if (temp.Length == 0)
                        {
                            if (r.RoomTypeID == 21010)
                            {
                                temp += r.Name + ((r.Site.Name.ToUpper() == "REGION 17 ESC") ? ", Online Course " : ", " + r.Site.Name);
                                break;
                            }
                            else
                                temp += r.Site.IsServiceCenter ? r.Site.Name + " - " + r.Name : r.Name;
                        }
                        else
                            temp += ", " + r.Name;
                    }

                    if (location.RoomTypeID == 21010)
                        temp += "";
                    else
                    {
                        if (
                             (location.Site.IsServiceCenter && (!String.IsNullOrEmpty(location.Site.Address1) && !String.IsNullOrEmpty(location.Site.City) && !String.IsNullOrEmpty(location.Site.Zip)))
                               || (!location.Site.IsServiceCenter && (!String.IsNullOrEmpty(location.Address1) && !String.IsNullOrEmpty(location.City) && !String.IsNullOrEmpty(location.Zip))))
                        {
                            temp += String.Format("<br /><a style\"font-size: 8pt; font-style:italic;\">{0}, {1}, {2}</a><br />", location.Site.IsServiceCenter ? location.Site.Address1 : location.Address1,
                                location.Site.IsServiceCenter ? location.Site.City : location.City,
                                location.Site.IsServiceCenter ? location.Site.Zip : location.Zip);
                        }
                    }

                    result += temp;
                    foreach (Room r in itemsToRemove)
                        copy.Remove(r);
                }
                return result;
            }

        }
    }
}
