using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class UsersAssignedView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String eventcode = null;

        if (Request["eventid"] != null)
            eventcode = Request["eventid"];

        if (!String.IsNullOrEmpty(eventcode))
        {
            String sqlqry = "SELECT eventUser.id, eventUser.active, category.categoryName, Users.firstName + ' ' + Users.lastName AS fullName, events.eventName + ' ' + '(' + events.eventCode + ')' AS eventDetail, Users.userID as UserID, events.id as EventId " +
                            "FROM category " +
                            "LEFT JOIN eventCategory ON category.id = eventCategory.categoryId " +
                            "LEFT JOIN events ON eventCategory.eventId = events.id " +
                            "INNER JOIN eventUser ON events.id = eventUser.eventId " +
                            "INNER JOIN Users ON eventUser.userId = Users.userID " +
                            "WHERE events.eventCode='" + eventcode + "'";

            eventsAssignedToUsers.SelectCommand = sqlqry;
            RadGrid1.DataBind();
        }
    }



    protected String EventName()
    {
        String eventcode = null;

        if (Request["eventid"] != null)
            eventcode = Request["eventid"];

        if (!String.IsNullOrEmpty(eventcode))
            return EventName(eventcode);

        return "Wrong";
    }

    protected String EventName(String EventCode)
    {
        string eventname = "";
        String sqlqry = "SELECT events.eventName " +
                        "FROM category " +
                        "LEFT JOIN eventCategory ON category.id = eventCategory.categoryId " +
                        "LEFT JOIN events ON eventCategory.eventId = events.id " +
                        "INNER JOIN eventUser ON events.id = eventUser.eventId " +
                        "INNER JOIN Users ON eventUser.userId = Users.userID " +
                        "WHERE events.eventCode='" + EventCode + "'";

        SqlConnection sqlcon = new SqlConnection(Session["client_database"].ToString());
        sqlcon.Open();
        SqlCommand sqlcmd = new SqlCommand(sqlqry, sqlcon);
        eventname = sqlcmd.ExecuteScalar() as String;
        sqlcon.Close();

        return eventname;
    }
}