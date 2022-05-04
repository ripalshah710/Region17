using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using region4.Common;

public partial class lib_Controls_UploadFile : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Context.User.Identity.IsAuthenticated && Context.User.IsInRole("account")))
        {
            pnlUpload.Visible = true;
        }

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName != "")
        {
            string strImageName = FileUpload1.PostedFile.FileName;
            strImageName = strImageName.Substring(strImageName.LastIndexOf(@"\") + 1);
            string strImageType = FileUpload1.PostedFile.ContentType;
            byte[] imageSize = new byte[FileUpload1.PostedFile.ContentLength];
            HttpPostedFile uploadedImage = FileUpload1.PostedFile;
            uploadedImage.InputStream.Read(imageSize, 0, (int)FileUpload1.PostedFile.ContentLength);

            // Create SQL Connection 

            using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
            {
                SqlCommand cmd = conn.CreateCommand();
                try
                {
                    cmd.CommandText = region4.baseStoredProcedures.escWeb.HomePageFileAttachementInsert;
                    SqlParameter ImageName = new SqlParameter("@FileName", SqlDbType.VarChar, 50);
                    ImageName.Value = strImageName.Substring(0, strImageName.LastIndexOf(".")); ;
                    cmd.Parameters.Add(ImageName);

                    SqlParameter UploadedImage = new SqlParameter("@FileContent", SqlDbType.Image, imageSize.Length);
                    UploadedImage.Value = imageSize;
                    cmd.Parameters.Add(UploadedImage);

                    SqlParameter ImageType = new SqlParameter("@FileType", SqlDbType.VarChar, 50);
                    ImageType.Value = strImageType.ToString();
                    cmd.Parameters.Add(ImageType);

                    SqlParameter ImageLength = new SqlParameter("@FileSize", SqlDbType.Int, 50);
                    ImageLength.Value = FileUpload1.PostedFile.ContentLength;
                    cmd.Parameters.Add(ImageLength);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (cmd.Connection.State != System.Data.ConnectionState.Open)
                        cmd.Connection.Open();

                    int result = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    if (result > 0)
                        lblMessage.Text = "File Uploaded";
                }
                catch (Exception ex)
                {
                    region4.ErrorReporter.ReportError(ex, HttpContext.Current, region4.ErrorReporter.Severity.notgiven, region4.ErrorReporter.Occurance.webControls);
                }
                finally
                {
                    if (cmd.Connection.State != ConnectionState.Closed)
                        cmd.Connection.Close();

                }
            }
        }
    }

}
