using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;

public partial class alertmarker : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         // We generate a new bitmap and   // draw an ellipse on it
   Bitmap oCanvas = new       Bitmap(12,12);

        //get color from query string
   string color = "#" + Request.QueryString["color"];
        
     

    //   System.Drawing.ColorConverter oConverter = new System.Drawing.ColorTranslator();
      // Color myColor = (Color)oConverter.ConvertF(color);


   Graphics g =  Graphics.FromImage(oCanvas);
   g.Clear(System.Drawing.ColorTranslator.FromHtml(color));
   Rectangle myRectangle = new Rectangle(0, 12, 12, 12);
   // Create pen.
   Pen blackPen = new Pen(System.Drawing.ColorTranslator.FromHtml(color), 3);

   // Create rectangle.
   Rectangle rect = new Rectangle(0, 0, 12, 12);
   g.DrawRectangle(blackPen, rect);
   // Now, we only need to send it    // to the client
   Response.ContentType = "image/jpeg";
   oCanvas.Save(Response.OutputStream,      ImageFormat.Jpeg);
   Response.End();

   // Cleanup
   g.Dispose();
   oCanvas.Dispose();


    }
}