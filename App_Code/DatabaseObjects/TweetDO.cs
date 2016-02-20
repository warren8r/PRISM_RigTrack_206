using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for TweetDO
/// </summary>
/// 
namespace RigTrack.DatabaseObjects
{
    public class TweetDO
    {
        public static DataTable GetAllTweets()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[sp_GetAllTweets]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                try
                {
                    da.Fill(dt);

                }
                catch (Exception ex)
                {
                }
            }

            return dt;
        }

        public static void InsertTweetMessageSent(int sensorID)
        {
          
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[sp_InsertUpdateSensorData]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Data", TwitterDTO.UserID);
                cmd.Parameters.AddWithValue("@sensorID", sensorID);
              
             
                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    
                }
                catch (Exception ex)
                {
                }
            }
           
        }
    }


}