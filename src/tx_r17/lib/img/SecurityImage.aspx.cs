using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.SessionState;

public partial class SecurityImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Create a CAPTCHA image using the text stored in the Session object.
        string text = string.Empty;
        CaptchaImage ci;

        if (this.Session["CaptchaImageText"] != null)
            text = this.Session["CaptchaImageText"].ToString();

        ci = new CaptchaImage(text, 200, 75, "Comic Sans MS");
        

        // Change the response headers to output a JPEG image.
        this.Response.Clear();
        this.Response.ContentType = "image/jpeg";

        if (!string.IsNullOrEmpty(Request.QueryString["section"]))
        {
            int section;
            int.TryParse(Request.QueryString["section"], out section);

            //
            // Split the image into 5 sections
            GraphicsUnit graphicsUnit = GraphicsUnit.Pixel;
            RectangleF rect = ci.Image.GetBounds(ref graphicsUnit);
            RectangleF clonedRect = new RectangleF(((rect.Width / 5) * (section - 1)), 0, (rect.Width / 5), rect.Height);

            Bitmap clonedImage = ci.Image.Clone(clonedRect, PixelFormat.Undefined);

            // Write the section image to the response stream in JPEG format.
            clonedImage.Save(this.Response.OutputStream, ImageFormat.Jpeg);
            clonedImage.Dispose();
        }
        else
        {
            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);
        }

        // Dispose of the CAPTCHA image object.
        ci.Dispose();
    }
}

/// <summary>
/// Summary description for CaptchaImage.
/// </summary>
public class CaptchaImage
{
    // Public properties (all read-only).
    public string Text
    {
        get { return this.text; }
    }
    public Bitmap Image
    {
        get { return this.image; }
    }
    public int Width
    {
        get { return this.width; }
    }
    public int Height
    {
        get { return this.height; }
    }

    // Internal properties.
    private string text;
    private int width;
    private int height;
    private string familyName;
    private Bitmap image;

    // For generating random numbers.
    private Random random = new Random();

    // ====================================================================
    // Initializes a new instance of the CaptchaImage class using the
    // specified text, width and height.
    // ====================================================================
    public CaptchaImage(string s, int width, int height)
    {
        this.text = s;
        this.SetDimensions(width, height);
        this.GenerateImage();
    }

    // ====================================================================
    // Initializes a new instance of the CaptchaImage class using the
    // specified text, width, height and font family.
    // ====================================================================
    public CaptchaImage(string s, int width, int height, string familyName)
    {
        this.text = s;
        this.SetDimensions(width, height);
        this.SetFamilyName(familyName);
        this.GenerateImage();
    }

    // ====================================================================
    // This member overrides Object.Finalize.
    // ====================================================================
    ~CaptchaImage()
    {
        Dispose(false);
    }

    // ====================================================================
    // Releases all resources used by this object.
    // ====================================================================
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        this.Dispose(true);
    }

    // ====================================================================
    // Custom Dispose method to clean up unmanaged resources.
    // ====================================================================
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose of the bitmap.
            this.image.Dispose();

            // Dispose all other past keys
            try
            {
                foreach (string key in HttpContext.Current.Session.Keys)
                {
                    if (key.StartsWith("Captcha:Noise:") && !key.Equals("Captcha:Noise:" + this.Text))
                        HttpContext.Current.Session.Remove(key);

                    if (key.StartsWith("Captcha:Points:") && !key.Equals("Captcha:Points:" + this.Text))
                        HttpContext.Current.Session.Remove(key);
                }
            }
            catch
            {
                Dispose(true);
            }

        }
    }

    // ====================================================================
    // Sets the image width and height.
    // ====================================================================
    private void SetDimensions(int width, int height)
    {
        // Check the width and height.
        if (width <= 0)
            throw new ArgumentOutOfRangeException("width", width, "Argument out of range, must be greater than zero.");
        if (height <= 0)
            throw new ArgumentOutOfRangeException("height", height, "Argument out of range, must be greater than zero.");
        this.width = width;
        this.height = height;
    }

    // ====================================================================
    // Sets the font used for the image text.
    // ====================================================================
    private void SetFamilyName(string familyName)
    {
        // If the named font is not installed, default to a system font.
        try
        {
            Font font = new Font(this.familyName, 12F);
            this.familyName = familyName;
            font.Dispose();
        }
        catch (Exception ex)
        {
            this.familyName = System.Drawing.FontFamily.GenericSerif.Name;
        }
    }

    // ====================================================================
    // Creates the bitmap image.
    // ====================================================================
    private void GenerateImage()
    {
        // Create a new 32-bit bitmap image.
        Bitmap bitmap = new Bitmap(this.width, this.height, PixelFormat.Format32bppArgb);

        // Create a graphics object for drawing.
        Graphics g = Graphics.FromImage(bitmap);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        Rectangle rect = new Rectangle(0, 0, this.width, this.height);

        // Fill in the background.
        HatchBrush hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);
        g.FillRectangle(hatchBrush, rect);

        // Set up the text font.
        SizeF size;
        float fontSize = rect.Height + 1;
        Font font;
        // Adjust the font size until the text fits within the image.
        do
        {
            fontSize--;
            font = new Font(this.familyName, fontSize, FontStyle.Bold);
            size = g.MeasureString(this.text, font);
        } while (size.Width > rect.Width);

        // Set up the text format.
        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;

        // Create a path using the text and warp it randomly.
        GraphicsPath path = new GraphicsPath();
        path.AddString(this.text, font.FontFamily, (int)font.Style, fontSize, rect, format);
        float v = 3.5F;

        Matrix matrix = new Matrix();
        matrix.Translate(0F, 0F);

        // Warp text field
        if (HttpContext.Current.Session["Captcha:Points:" + this.Text] == null)
        {
            PointF[] points =
			{
				new PointF(this.random.Next(rect.Width) / v, this.random.Next(rect.Height) / v),
				new PointF(rect.Width - this.random.Next(rect.Width) / v, this.random.Next(rect.Height) / v),
				new PointF(this.random.Next(rect.Width) / v, rect.Height - this.random.Next(rect.Height) / v),
				new PointF(rect.Width - this.random.Next(rect.Width) / v, rect.Height - this.random.Next(rect.Height) / v)
			};

            HttpContext.Current.Session.Add("Captcha:Points:" + this.Text, points);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);
        }
        else
        {
            PointF[] points = (PointF[])HttpContext.Current.Session["Captcha:Points:" + this.Text];
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);
        }


        // Draw the text.
        hatchBrush = new HatchBrush(HatchStyle.LargeConfetti, Color.SteelBlue, Color.Pink);
        g.FillPath(hatchBrush, path);

        HatchBrush hatchBrush2 = new HatchBrush(HatchStyle.LargeConfetti, Color.SteelBlue, Color.Pink);

        // Add some random noise.
        if (HttpContext.Current.Session["Captcha:Noise:" + this.Text] == null)
        {
            int m = Math.Max(rect.Width, rect.Height);
            int max = (int)(rect.Width * rect.Height / 30F);
            int[][] noise = new int[max][];
            for (int i = 0; i < noise.Length; i++)
            {
                noise[i] = new int[] {
                    this.random.Next(rect.Width),
                    this.random.Next(rect.Height),
                    this.random.Next(m / 50),
                    this.random.Next(m / 50)
                    };

                g.FillEllipse(hatchBrush2, noise[i][0], noise[i][1], noise[i][2], noise[i][3]);
            }

            HttpContext.Current.Session.Add("Captcha:Noise:" + this.Text, noise);
        }
        else
        {
            int[][] noise = (int[][])HttpContext.Current.Session["Captcha:Noise:" + this.Text];
            for (int i = 0; i < noise.Length; i++)
            {
                g.FillEllipse(hatchBrush2, noise[i][0], noise[i][1], noise[i][2], noise[i][3]);
            }
        }


        // Clean up.
        font.Dispose();
        hatchBrush.Dispose();
        g.Dispose();

        
        // Set the image.
        this.image = bitmap;
    }
}
