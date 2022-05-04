using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace tx_r17.Shoebox.Evaluation.Pages
{
	/// <summary>
	/// escWorks.NET Evaluation Form.
	/// </summary>
	public partial class DefaultPage : System.Web.UI.Page
	{
		protected DataSet ItemDS;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			//
			// Set QueryStrings
            int mSessionId = Convert.ToInt32(LegacyCode.Strings.GetSafeString("sessionId", LegacyCode.Strings.StringType.QueryString, -1));
            int mRegistrationId = Convert.ToInt32(LegacyCode.Strings.GetSafeString("registrationId", LegacyCode.Strings.StringType.QueryString, -1));
			
			if(mRegistrationId < 0)
			{
				this.pCloser.Visible = true;
				this.Load += new System.EventHandler(this.Page_Load);
				base.OnInit(e);
			}

			pIFrameContent.Controls.Add(new LiteralControl("<iframe src=\"form.aspx?registrationId="+mRegistrationId+"&sessionId="+mSessionId+"\" width=\"575\" height=\"466\" frameborder=\"0\" id=\"iframeContent\">Sorry, your browser does not support iframes; try a browser that supports W3 standards.</iframe>"));

			//
			// Instantiate and load Form obeject
			Evaluation eForm = new Evaluation();
			eForm.LoadForm(mRegistrationId);

			ItemDS = eForm.LoadItems(mRegistrationId);
			
			DataTable objItem = ItemDS.Tables["Items"];

			//
			// Set Form Title
			ctrlFormTitle.Text = eForm.Title;

			
		// end private void Page_Load
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
