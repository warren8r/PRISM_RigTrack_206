using System;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.UI;
using System.Collections.Generic;

using TweetSharp;
using System.Web.Configuration;
using System.Text.RegularExpressions;

using System.Data.SqlClient;
using System.Text;

public partial class Modules_RigTrack_SubmitTwitter : System.Web.UI.Page
{
    private string consumerKey = WebConfigurationManager.AppSettings["consumerKey"];
    private string consumerSecret = WebConfigurationManager.AppSettings["consumerSecret"];

  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["oauth_token"] == null || Request.QueryString["oauth_verifier"] == null ||
                   Request.QueryString["oauth_token"] == "" || Request.QueryString["oauth_verifier"] == "")
            {
                AuthorizeTwitter();
            }
        }
    }

    


    public void AuthorizeTwitter()
    {
        // Retrieve key and secret from webconfig
        TwitterService service = new TwitterService(consumerKey, consumerSecret);

        // This is the registered callback URL  
        OAuthRequestToken requestToken = service.GetRequestToken("http://67.40.65.179:206/Modules/RigTrack/SubmitTwitter.aspx");

        //Uri uri = service.GetAuthorizationUri(requestToken); // if you want to always make the user Authorization before sending Tweets
        Uri uri = service.GetAuthenticationUrl(requestToken);
        Response.Redirect(uri.ToString());
    }


    protected void btnSubmitTwitter_Click(object sender, EventArgs e)
    {

        OAuthRequestToken requestToken = new OAuthRequestToken();
        requestToken.Token = Request.QueryString["oauth_token"];
        requestToken.TokenSecret = Request.QueryString["oauth_verifier"];


        // Retrieve key and secret from webconfig
        TwitterService service = new TwitterService(consumerKey, consumerSecret);

        OAuthAccessToken accessToken = new OAuthAccessToken();
        accessToken = service.GetAccessToken(requestToken, requestToken.TokenSecret);

        service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);

        DataTable sensorData = RigTrack.DatabaseObjects.TweetDO.GetAllTweets();
        foreach (DataRow sensorRow in sensorData.Rows)
        {
            TwitterUser user = new TwitterUser();
            user = service.GetUserProfileFor(new GetUserProfileForOptions { ScreenName = sensorRow["Username"].ToString() });
            int sensorID = int.Parse(sensorRow["sensorID"].ToString());
        
            //int userID = int.Parse(sensorRow["UserID"].ToString());
            int data = int.Parse(sensorRow["data"].ToString());
            bool isSent = Boolean.Parse(sensorRow["isSent"].ToString());
            TwitterDirectMessage sendMessage = new TwitterDirectMessage();

            if (data > 80 && isSent != true)
            {
                sendMessage = service.SendDirectMessage(new SendDirectMessageOptions { UserId = user.Id, Text = "Temperature is Greater Than 80 Degrees." + " Your Current Temperature Is " + data });
                RigTrack.DatabaseObjects.TweetDO.InsertTweetMessageSent(sensorID);
            }
        }

        Response.Redirect("http://67.40.65.179:206/Modules/RigTrack/SubmitTwitter.aspx");

    }

    protected void MainTimer_Tick(object sender, EventArgs e)
    {
        OAuthRequestToken requestToken = new OAuthRequestToken();
        requestToken.Token = Request.QueryString["oauth_token"];
        requestToken.TokenSecret = Request.QueryString["oauth_verifier"];


        // Retrieve key and secret from webconfig
        TwitterService service = new TwitterService(consumerKey, consumerSecret);

        OAuthAccessToken accessToken = new OAuthAccessToken();
        accessToken = service.GetAccessToken(requestToken, requestToken.TokenSecret);

        service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);

        DataTable sensorData = RigTrack.DatabaseObjects.TweetDO.GetAllTweets();
        foreach (DataRow sensorRow in sensorData.Rows)
        {
            TwitterUser user = new TwitterUser();
            user = service.GetUserProfileFor(new GetUserProfileForOptions { ScreenName = sensorRow["Username"].ToString() });
            int sensorID = int.Parse(sensorRow["sensorID"].ToString());

            //int userID = int.Parse(sensorRow["UserID"].ToString());
            int data = int.Parse(sensorRow["data"].ToString());
            bool isSent = Boolean.Parse(sensorRow["isSent"].ToString());
            TwitterDirectMessage sendMessage = new TwitterDirectMessage();

            if (data > 80 && isSent != true)
            {
                sendMessage = service.SendDirectMessage(new SendDirectMessageOptions { UserId = user.Id, Text = "Temperature is Greater Than 80 Degrees." + " Your Current Temperature Is " + data });
                RigTrack.DatabaseObjects.TweetDO.InsertTweetMessageSent(sensorID);
            }
        }

        Response.Redirect("http://67.40.65.179:206/Modules/RigTrack/SubmitTwitter.aspx");
    }

  

    /// for sending timeline message

    //protected void btnSendTweet_Click(object sender, EventArgs e)
    //{
    //    OAuthRequestToken requestToken = new OAuthRequestToken();
    //    requestToken.Token = Request.QueryString["oauth_token"];

    //    string verifier = Request.QueryString["oauth_verifier"];


    //    // Retrieve key and secret from webconfig
    //    TwitterService service = new TwitterService(consumerKey, consumerSecret);

    //    OAuthAccessToken accessToken = service.GetAccessToken(requestToken, verifier);

    //    service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);

    //    SendTweetOptions options = new SendTweetOptions();
    //    //options.Status = txtTweetMessage.Text.ToString();

    //    options.Status = HttpUtility.HtmlEncode(txtTweetMessage.Text);

    //    TwitterStatus status = service.SendTweet(options);// HttpUtility.HtmlEncode(txtTweetMessage.Text)

    //    // Jquery Dialog Box
    //    string script = "SuccessDialog(\" (Tweet) " + txtTweetMessage.Text + " (Successfully Sent) \");";
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "SendTweet", script, true);

    //}

}