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
using Telerik.Web.UI;

namespace tx_r17.Shoebox.Evaluation.Pages
{
	/// <summary>
	/// escWorks.NET Evaluation Form.
	/// </summary>
	public partial class FormPage : System.Web.UI.Page
	{

        private DataSet ItemDS;
        private int mSessionId, mRegistrationId;
        private ArrayList indexControls = new ArrayList();
        private ArrayList checkboxArray = new ArrayList();

		private Evaluation eForm = new Evaluation();

		struct WebControlContainer
		{
            public System.Web.UI.Control _WebControl;
			public System.Type _Type;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			//
			// Set QueryStrings
            mSessionId = Convert.ToInt32(LegacyCode.Strings.GetSafeString("sessionId", LegacyCode.Strings.StringType.QueryString, -1));
            mRegistrationId = Convert.ToInt32(LegacyCode.Strings.GetSafeString("registrationId", LegacyCode.Strings.StringType.QueryString, -1));
			
			if(mRegistrationId < 0)
			{
				this.pCloser.Visible = true;
				this.Load += new System.EventHandler(this.Page_Load);
				base.OnInit(e);
			}

			//
			// Instantiate and load Form object
			eForm.LoadForm(mRegistrationId);

			if (eForm.Title == null)
			{
				lblErrorMessage.Text += "There is no evaluation form for this Registration ID.<BR>";
				pForm.Visible = false;
				pCompleteFailure.Visible = true;
			}
            else if (eForm.Completed)
            {
            }
            else
            {

                ItemDS = eForm.LoadItems(mRegistrationId);

                DataTable objItem = ItemDS.Tables["Items"];

                //
                // Render Form Table
                tbForm.CellPadding = 2;
                tbForm.CellSpacing = 2;
                tbForm.Attributes.Add("align", "left");
                tbForm.Width = Unit.Percentage(100);

                //
                // Declare Form Line Items Variables
                string text;
                int item_id, type, style;
                bool require;
                string indent = String.Format("&nbsp;&nbsp;&nbsp;");
                int i = 0;

                for (int n = 0; n < objItem.Rows.Count; n++)
                {
                    //
                    // Set Form Line Items Variables
                    text = objItem.Rows[n]["text"].ToString();
                    item_id = Convert.ToInt32(objItem.Rows[n]["item_id"].ToString());
                    type = Convert.ToInt32(objItem.Rows[n]["type"].ToString());
                    style = Convert.ToInt32(objItem.Rows[n]["style"].ToString());
                    require = Convert.ToBoolean(objItem.Rows[n]["require"].ToString());

                    //
                    // Parameter "[evaluation.multi.item].[type]"
                    switch (type)
                    {
                        case 1: //Section Text
                            {
                                tbForm.Rows.Add(new TableRow());
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[0].ColumnSpan = 2;
                                tbForm.Rows[i].Cells[0].CssClass = "eFormSectionText";
                                tbForm.Rows[i].Cells[0].Controls.Add(new LiteralControl("<B>" + text + "</B>"));
                                break;
                            }

                        case 2: //Description Text
                            {
                                tbForm.Rows.Add(new TableRow());
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[0].ColumnSpan = 2;
                                tbForm.Rows[i].Cells[0].CssClass = "eFormDescriptionText";
                                tbForm.Rows[i].Cells[0].Controls.Add(new LiteralControl("<I>" + text + "</I>"));
                                break;
                            }

                        case 3: //TextBox
                            {
                                tbForm.Rows.Add(new TableRow());
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[0].Width = Unit.Pixel(25);
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[1].CssClass = "eFormTextBox";
                                tbForm.Rows[i].Cells[1].Controls.Add(new LiteralControl(text));
                                i++;
                                System.Web.UI.WebControls.TextBox textbox = new System.Web.UI.WebControls.TextBox();
                                textbox.ID = "txt" + item_id.ToString();
                                //textbox.Text=eForm.LoadResponse(this.mRegistrationId,Convert.ToInt32(item_id));
                                textbox.CssClass = "INPUT";
                                textbox.Width = Unit.Percentage(95);
                                tbForm.Rows.Add(new TableRow());
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[0].Width = Unit.Pixel(25);
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[1].CssClass = "FormInput";
                                tbForm.Rows[i].Cells[1].Controls.Add(textbox);
                                this.indexControls.Add("txt" + item_id.ToString());
                                WebControlContainer wcc = new WebControlContainer();
                                wcc._WebControl = textbox;
                                wcc._Type = textbox.GetType();
                                this.checkboxArray.Add(wcc);
                                if (require)
                                {
                                    System.Web.UI.WebControls.RequiredFieldValidator requiredValidator = new RequiredFieldValidator();
                                    requiredValidator.ID = "RequiredFieldValidator" + item_id;
                                    requiredValidator.ControlToValidate = "txt" + item_id.ToString();
                                    requiredValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
                                    requiredValidator.CssClass = "RequiredText";
                                    requiredValidator.ErrorMessage = "<FONT SIZE=4><B>*</B></FONT>";
                                    tbForm.Rows[i - 1].Cells[0].Controls.Add(requiredValidator);
                                    requiredValidator.Dispose();
                                }
                                break;
                            }

                        case 4: //TextArea
                            {
                                tbForm.Rows.Add(new TableRow());
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[0].Width = Unit.Pixel(25);
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[1].CssClass = "eFormTextArea";
                                tbForm.Rows[i].Cells[1].Controls.Add(new LiteralControl(text));
                                i++;
                                System.Web.UI.WebControls.TextBox textarea = new System.Web.UI.WebControls.TextBox();
                                textarea.ID = "txt" + item_id.ToString();
                                //textarea.Text=eForm.LoadResponse(this.mRegistrationId,Convert.ToInt32(item_id));
                                textarea.CssClass = "INPUT";
                                textarea.TextMode = TextBoxMode.MultiLine;
                                textarea.Rows = 3;
                                textarea.Width = Unit.Percentage(95);
                                tbForm.Rows.Add(new TableRow());
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[0].Width = Unit.Pixel(25);
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[1].CssClass = "FormInput";
                                tbForm.Rows[i].Cells[1].Controls.Add(textarea);
                                this.indexControls.Add("txt" + item_id.ToString());
                                WebControlContainer wcc = new WebControlContainer();
                                wcc._WebControl = textarea;
                                wcc._Type = textarea.GetType();
                                this.checkboxArray.Add(wcc);
                                if (require)
                                {
                                    System.Web.UI.WebControls.RequiredFieldValidator requiredValidator = new RequiredFieldValidator();
                                    requiredValidator.ID = "RequiredFieldValidator" + item_id;
                                    requiredValidator.ControlToValidate = "txt" + item_id.ToString();
                                    requiredValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
                                    requiredValidator.CssClass = "RequiredText";
                                    requiredValidator.ErrorMessage = "<FONT SIZE=4><B>*</B></FONT>";
                                    tbForm.Rows[i - 1].Cells[0].Controls.Add(requiredValidator);
                                    requiredValidator.Dispose();
                                }
                                break;
                            }

                        case 5: //DropDownList
                            {
                                tbForm.Rows.Add(new TableRow());
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[0].Width = Unit.Pixel(25);
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[1].CssClass = "eFormDropDownList";
                                tbForm.Rows[i].Cells[1].Controls.Add(new LiteralControl(text + indent));
                                System.Web.UI.WebControls.DropDownList ddlist = new System.Web.UI.WebControls.DropDownList();
                                ddlist.ID = "dd" + item_id.ToString();
                                ddlist.CssClass = "INPUT";
                                ddlist.DataSource = eForm.LoadSelections(item_id).Tables["Selections"];
                                ddlist.DataTextField = "text";
                                ddlist.DataValueField = "value";
                                ddlist.DataBind();
                                ddlist.Items.Insert(0, new ListItem(null, null));
                                //ddlist.SelectedIndex = ddlist.Items.IndexOf(ddlist.Items.FindByValue(eForm.LoadResponse(this.mRegistrationId,Convert.ToInt32(item_id))));
                                tbForm.Rows[i].Cells[1].Controls.Add(ddlist);
                                this.indexControls.Add("dd" + item_id.ToString());
                                WebControlContainer wcc = new WebControlContainer();
                                wcc._WebControl = ddlist;
                                wcc._Type = ddlist.GetType();
                                this.checkboxArray.Add(wcc);
                                if (require)
                                {
                                    System.Web.UI.WebControls.RequiredFieldValidator requiredValidator = new RequiredFieldValidator();
                                    requiredValidator.ID = "RequiredFieldValidator" + item_id;
                                    requiredValidator.ControlToValidate = "dd" + item_id.ToString();
                                    requiredValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
                                    requiredValidator.CssClass = "RequiredText";
                                    requiredValidator.ErrorMessage = "<FONT SIZE=4><B>*</B></FONT>";
                                    tbForm.Rows[i].Cells[0].Controls.Add(requiredValidator);
                                    requiredValidator.Dispose();
                                }
                                break;
                            }

                        case 6: //CheckBoxList
                            {
                                tbForm.Rows.Add(new TableRow());
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[0].Width = Unit.Pixel(25);
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[1].CssClass = "eFormCheckBoxList";
                                tbForm.Rows[i].Cells[1].Controls.Add(new LiteralControl(text));
                                i++;
                                tbForm.Rows.Add(new TableRow());
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[0].Width = Unit.Pixel(25);
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[1].CssClass = "FormInput";
                                System.Web.UI.WebControls.CheckBoxList CheckBox = new System.Web.UI.WebControls.CheckBoxList();
                                CheckBox.ID = item_id.ToString();
                                CheckBox.CssClass = "INPUT";
                                CheckBox.DataSource = eForm.LoadSelections(item_id);
                                CheckBox.DataTextField = "text";
                                CheckBox.DataValueField = "text";
                                CheckBox.RepeatColumns = CheckBox.Items.Count;
                                CheckBox.RepeatDirection = RepeatDirection.Vertical;
                                CheckBox.RepeatLayout = RepeatLayout.Flow;
                                CheckBox.TextAlign = TextAlign.Right;
                                CheckBox.DataBind();
                                tbForm.Rows[i].Cells[1].Controls.Add(CheckBox);
                                this.indexControls.Add(item_id.ToString());
                                WebControlContainer wcc = new WebControlContainer();
                                wcc._WebControl = CheckBox;
                                wcc._Type = CheckBox.GetType();
                                this.checkboxArray.Add(wcc);
                                break;
                            }

                        case 7: //RadioButtonList
                            {
                                tbForm.Rows.Add(new TableRow());
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[0].Width = Unit.Pixel(25);
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[1].CssClass = "eFormRadioButtonList";
                                tbForm.Rows[i].Cells[1].Controls.Add(new LiteralControl(text));
                                i++;
                                tbForm.Rows.Add(new TableRow());
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[0].Width = Unit.Pixel(25);
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[1].CssClass = "FormInput";
                                System.Web.UI.WebControls.RadioButtonList RadioButton = new System.Web.UI.WebControls.RadioButtonList();
                                RadioButton.ID = "radio" + item_id.ToString();
                                RadioButton.CssClass = "INPUT";
                                RadioButton.DataSource = eForm.LoadSelections(item_id);
                                RadioButton.DataTextField = "text";
                                RadioButton.DataTextFormatString = indent + "{0}";
                                RadioButton.DataValueField = "value";
                                RadioButton.RepeatColumns = RadioButton.Items.Count;
                                RadioButton.RepeatDirection = RepeatDirection.Horizontal;
                                RadioButton.RepeatLayout = RepeatLayout.Flow;
                                RadioButton.TextAlign = TextAlign.Left;
                                RadioButton.DataBind();
                                //RadioButton.SelectedIndex = RadioButton.Items.IndexOf(RadioButton.Items.FindByValue(eForm.LoadResponse(this.mRegistrationId,Convert.ToInt32(item_id))));
                                tbForm.Rows[i].Cells[1].Controls.Add(RadioButton);
                                this.indexControls.Add("radio" + item_id.ToString());
                                WebControlContainer wcc = new WebControlContainer();
                                wcc._WebControl = RadioButton;
                                wcc._Type = RadioButton.GetType();
                                this.checkboxArray.Add(wcc);
                                if (require)
                                {
                                    System.Web.UI.WebControls.RequiredFieldValidator requiredValidator = new RequiredFieldValidator();
                                    requiredValidator.ID = "RequiredFieldValidator" + item_id;
                                    requiredValidator.ControlToValidate = "radio" + item_id.ToString();
                                    requiredValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
                                    requiredValidator.CssClass = "RequiredText";
                                    requiredValidator.ErrorMessage = "<FONT SIZE=4><B>*</B></FONT>";
                                    tbForm.Rows[i - 1].Cells[0].Controls.Add(requiredValidator);
                                    requiredValidator.Dispose();
                                }
                                break;
                            }
                        case 8: //Slide
                            {
                                tbForm.Rows.Add(new TableRow());
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[0].Width = Unit.Pixel(25); //Validator? 
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[1].CssClass = "eFormRadioButtonList";
                                tbForm.Rows[i].Cells[1].Controls.Add(new LiteralControl(text));
                                i++;
                                tbForm.Rows.Add(new TableRow());
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[0].Width = Unit.Pixel(25);
                                tbForm.Rows[i].Cells.Add(new TableCell());
                                tbForm.Rows[i].Cells[1].CssClass = "FormInput";

                                //Table for the Slider
                                Table table = new Table();
                                TableRow tableRow1 = new TableRow();
                                table.Controls.Add(tableRow1);

                                TableCell tableCell10 = new TableCell();
                                tableCell10.Width = Unit.Pixel(25);
                                tableRow1.Cells.Add(tableCell10);

                                TableCell tableCell11 = new TableCell();
                                tableCell11.CssClass = "eFormRadioButtonList";
                                tableCell11.Controls.Add(new LiteralControl("Strongly Agree"));
                                tableRow1.Cells.Add(tableCell11);

                                TableCell tableCell12 = new TableCell();
                                tableCell12.CssClass = "eFormRadioButtonList";
                                tableRow1.Cells.Add(tableCell12);

                                TableCell tableCell13 = new TableCell();
                                tableCell13.CssClass = "eFormRadioButtonList";
                                tableCell13.Controls.Add(new LiteralControl("Strongly Disagree"));
                                tableRow1.Cells.Add(tableCell13);

                                TableRow tableRow2 = new TableRow();
                                table.Controls.Add(tableRow2);

                                TableCell tableCell20 = new TableCell();
                                tableCell20.Width = Unit.Pixel(25);
                                tableRow2.Cells.Add(tableCell20);

                                string sliderId = "question" + item_id.ToString();

                                TableCell tableCell21 = new TableCell();
                                Panel panel1 = new Panel();
                                panel1.ID = "lbl" + sliderId;
                                panel1.CssClass = "eFormDescriptionText";
                                panel1.Style.Add("font-weight", "bold");
                                panel1.HorizontalAlign = HorizontalAlign.Right;
                                panel1.Width = new Unit(75);
                                panel1.Controls.Add(new LiteralControl("5.0"));
                                tableCell21.Controls.Add(panel1);

                                HiddenField hiddenField = new HiddenField();
                                hiddenField.Value = "5";
                                hiddenField.ID = "hidden" + sliderId;
                                tableCell21.Controls.Add(hiddenField);
                                tableRow2.Cells.Add(tableCell21);

                                TableCell tableCell22 = new TableCell();
                                RadSlider radSlider = new RadSlider();
                                radSlider.ID = sliderId;
                                radSlider.Orientation = Orientation.Horizontal;
                                radSlider.Width = new Unit(250);
                                radSlider.Height = new Unit(25);
                                radSlider.CssClass = "GreenSliderTrack";
                                radSlider.AutoPostBack = false;
                                radSlider.ItemType = SliderItemType.None;
                                radSlider.ShowDecreaseHandle = false;
                                radSlider.ShowIncreaseHandle = false;
                                radSlider.MinimumValue = 1.0m;
                                radSlider.MaximumValue = 5.0m;
                                radSlider.Value = 5.0m;
                                radSlider.SmallChange = 0.1m;
                                radSlider.LargeChange = 1.0m;
                                radSlider.OnClientValueChanged = "OnClientValueChanged";
                                radSlider.OnClientLoad = "OnClientValueChanged"; //This is for clicking Submit, but need to fill required fields
                                radSlider.IsDirectionReversed = true;

                                tableCell22.Controls.Add(radSlider);
                                tableRow2.Cells.Add(tableCell22);

                                TableCell tableCell23 = new TableCell();
                                tableCell23.CssClass = "eFormRadioButtonList";
                                tableRow2.Cells.Add(tableCell23);

                                //Add Comment field
                                TableRow tableRow3 = new TableRow();
                                table.Controls.Add(tableRow3);

                                TableCell tableCell30 = new TableCell();
                                tableCell30.Width = Unit.Pixel(25);
                                tableRow3.Cells.Add(tableCell30);

                                TableCell tableCell31 = new TableCell();
                                tableCell31.CssClass = "eFormTextBox";
                                tableCell31.Controls.Add(new LiteralControl("Comments:"));
                                tableRow3.Cells.Add(tableCell31);

                                TableCell tableCell32 = new TableCell();
                                System.Web.UI.WebControls.TextBox textbox = new System.Web.UI.WebControls.TextBox();
                                textbox.ID = "txt" + sliderId;
                                textbox.CssClass = "INPUT";
                                textbox.Width = Unit.Percentage(95);
                                tableCell32.CssClass = "FormInput";
                                tableCell32.Controls.Add(textbox);
                                tableRow3.Cells.Add(tableCell32);

                                TableCell tableCell33 = new TableCell();
                                tableCell33.CssClass = "eFormRadioButtonList";
                                tableCell33.Controls.Add(new LiteralControl(""));
                                tableRow3.Cells.Add(tableCell33);

                                tbForm.Rows[i].Cells[1].Controls.Add(table);

                                this.indexControls.Add(hiddenField.ID);

                                WebControlContainer wcc = new WebControlContainer();
                                wcc._WebControl = hiddenField;
                                wcc._Type = hiddenField.GetType();
                                this.checkboxArray.Add(wcc);

                                break;
                            }
                        default:
                            break;

                    } // end switch

                    // update index
                    i++;


                } // end if

                //
                //Disable SAVE function
                btnSave.Visible = false;
            }
			
		} // end private void Page_Load

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

		protected void Click_btnCancel(object sender , System.EventArgs e)
		{
			this.pForm.Visible = false;
			this.pCloser.Visible = true;
			base.OnInit(e);
		}

		public void Click_btnSave(object sender, System.EventArgs e)
		{
			try
			{
				// save to old evaluation table
				/*eForm.save(
					this.mRegistrationId,
					this.mSessionId,
					Convert.ToInt32(escWorks.Strings.GetSafeString("dd1",escWorks.Strings.StringType.Form,0)),
					Convert.ToInt32(escWorks.Strings.GetSafeString("dd2",escWorks.Strings.StringType.Form,0)),
					escWorks.Strings.GetSafeString("radio5",escWorks.Strings.StringType.Form),
					escWorks.Strings.GetSafeString("radio6",escWorks.Strings.StringType.Form),
					escWorks.Strings.GetSafeString("radio7",escWorks.Strings.StringType.Form),
					escWorks.Strings.GetSafeString("radio8",escWorks.Strings.StringType.Form),
					Convert.ToInt32((Request.Form["15:0"]=="on"?1:0)),
					Convert.ToInt32((Request.Form["15:1"]=="on"?1:0)),
					Convert.ToInt32((Request.Form["15:2"]=="on"?1:0)),
					Convert.ToInt32((Request.Form["15:3"]=="on"?1:0)),
					Convert.ToInt32((Request.Form["15:4"]=="on"?1:0)),
					Convert.ToInt32((Request.Form["15:5"]=="on"?1:0)),
					Convert.ToInt32((Request.Form["15:6"]=="on"?1:0)),
					Convert.ToInt32((Request.Form["15:7"]=="on"?1:0)),
					escWorks.Strings.GetSafeString("txt11",escWorks.Strings.StringType.Form),
					escWorks.Strings.GetSafeString("txt13",escWorks.Strings.StringType.Form)
					);*/

				// save to new table
				foreach (WebControlContainer wcc in checkboxArray)
				{
					switch (wcc._Type.ToString())
					{
						/*******************************
						 *	CheckBoxList
						 *******************************/
						case "System.Web.UI.WebControls.CheckBoxList":
								
							System.Web.UI.WebControls.CheckBoxList obj = wcc._WebControl as System.Web.UI.WebControls.CheckBoxList;
								
							string ResponseText = string.Empty;
							int x=0;
								
							foreach(ListItem item in obj.Items)
							{
								if (item.Selected)
								{
									ResponseText += (x==0?item.Text:", "+item.Text);
									x++;
								}
							}

							eForm.SaveResponse(
								this.mRegistrationId,
								Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(wcc._WebControl.ClientID.ToLower(),@"(([a-zA-Z\.]*))","")),
								ResponseText
								);

							break;
						/*******************************
						 *	Generic WebControl Base
						 *******************************/
						default:

							eForm.SaveResponse(
								this.mRegistrationId,
								Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(wcc._WebControl.ClientID.ToLower(),@"(([a-zA-Z\.]*))","")),
                                LegacyCode.Strings.GetSafeString(wcc._WebControl.ClientID, LegacyCode.Strings.StringType.Form)
								);

							break;
					}

				}

				Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Your form has been saved!\");</SCRIPT>");
			}
			catch(Exception ex)
			{
				this.lblErrorMessage.Text += ex.ToString()+"<BR>";
				this.pForm.Visible = false;
				this.pCompleteFailure.Visible = true;
			}
		}

		public void Click_btnSubmit(object sender, System.EventArgs e)
		{
			Page.Validate();
			if (Page.IsValid)
			{
				this.pForm.Visible = false;

				try
				{
					// save to old table
					/*eForm.save(
						this.mRegistrationId,
						this.mSessionId,
						Convert.ToInt32(escWorks.Strings.GetSafeString("dd1",escWorks.Strings.StringType.Form,0)),
						Convert.ToInt32(escWorks.Strings.GetSafeString("dd2",escWorks.Strings.StringType.Form,0)),
						escWorks.Strings.GetSafeString("radio5",escWorks.Strings.StringType.Form),
						escWorks.Strings.GetSafeString("radio6",escWorks.Strings.StringType.Form),
						escWorks.Strings.GetSafeString("radio7",escWorks.Strings.StringType.Form),
						escWorks.Strings.GetSafeString("radio8",escWorks.Strings.StringType.Form),
						Convert.ToInt32((Request.Form["15:0"]=="on"?1:0)),
						Convert.ToInt32((Request.Form["15:1"]=="on"?1:0)),
						Convert.ToInt32((Request.Form["15:2"]=="on"?1:0)),
						Convert.ToInt32((Request.Form["15:3"]=="on"?1:0)),
						Convert.ToInt32((Request.Form["15:4"]=="on"?1:0)),
						Convert.ToInt32((Request.Form["15:5"]=="on"?1:0)),
						Convert.ToInt32((Request.Form["15:6"]=="on"?1:0)),
						Convert.ToInt32((Request.Form["15:7"]=="on"?1:0)),
						escWorks.Strings.GetSafeString("txt11",escWorks.Strings.StringType.Form),
						escWorks.Strings.GetSafeString("txt13",escWorks.Strings.StringType.Form)
						);*/

					// save to new table
					foreach (WebControlContainer wcc in checkboxArray)
					{
						switch (wcc._Type.ToString())
						{
							/*******************************
							 *	CheckBoxList
							 *******************************/
							case "System.Web.UI.WebControls.CheckBoxList":
								
								System.Web.UI.WebControls.CheckBoxList obj = wcc._WebControl as System.Web.UI.WebControls.CheckBoxList;
								
								string ResponseText = string.Empty;
								int x=0;
								
								foreach(ListItem item in obj.Items)
								{
									if (item.Selected)
									{
										ResponseText += (x==0?item.Text:", "+item.Text);
										x++;
									}
								}

								eForm.SaveResponse(
									this.mRegistrationId,
									Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(wcc._WebControl.ClientID.ToLower(),@"(([a-zA-Z\.]*))","")),
									ResponseText
									);

								break;
							/*******************************
							 *	Generic WebControl Base
							 *******************************/
							default:

								eForm.SaveResponse(
									this.mRegistrationId,
									Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(wcc._WebControl.ClientID.ToLower(),@"(([a-zA-Z\.]*))","")),
                                    LegacyCode.Strings.GetSafeString(wcc._WebControl.ClientID, LegacyCode.Strings.StringType.Form)
									);

								break;
						}

					}

					eForm.CompleteForm(this.mRegistrationId);
					pComplete.Visible=true;
                    //TTTTTTTTTTTTTTTTTTTTTTTTTTTBING Added TTTTTTTTTTTTTTTTT
                    region4.ObjectModel.User user = Session["profile"] as region4.ObjectModel.User;
                    user.Clear();
                    //LLLLLLLLLLLLLLLLLLLLLLLLLLLBING Added LLLLLLLLLLLLLLLLL
					this.Load += new System.EventHandler(this.Page_Load);
					base.OnInit(e);
				}
				catch(Exception ex)
				{
					this.lblErrorMessage.Text += ex.ToString()+"<BR>";
					this.pForm.Visible = false;
					this.pCompleteFailure.Visible = true;
				}
			}
		}
	}
}