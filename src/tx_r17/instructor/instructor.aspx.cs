using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using region4.ObjectModel;
using Telerik.Web.UI;
using System.Collections.Generic;
using region4.escWeb.BasePages.Instructor;

public partial class instructor_instructor : instructor
{
    protected override void AssignControlsToBase()
    {
        this._lblSessionId = this.lblSessionId;
        this._radGridAttendeeList = this.RadGrid1;
        this._saveButton = this.btnSave;
    }
}
