using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class shoebox_transcripts_default : region4.escWeb.BasePages.ShoeBox.transcripts.default_aspx
{

    protected override void AssignControlsToBase()
    {
        
        base._pOfficial = this.pOfficial;
        base._pOfficialFailure = this.pOfficialFailure;
        base._ddOfficialYear = this.ddOfficialYear;
        base._btnOfficialTranscript = this.btnOfficialTranscript;
        base._btnPersonalTranscript = this.btnPersonalTranscript;
        
    }


}
