using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for TOU_Function
/// </summary>
namespace ToU
{
    public class touUtility
    {
        public string createSeason(SqlConnection sqlcon, string seasonName, string startMonth, string endMonth)
        {
            try
            {
                sqlcon.Open();
                using (SqlCommand sqlcmd = new SqlCommand("INSERT INTO touSeasons (seasonName, startMonth, endMonth, isActive) VALUES (@seasonName, @startMonth, @endMonth, 'true')", sqlcon))
                {
                    sqlcmd.Parameters.AddWithValue("@seasonName", seasonName);
                    sqlcmd.Parameters.AddWithValue("@startMonth", startMonth);
                    sqlcmd.Parameters.AddWithValue("@endMonth", endMonth);
                    sqlcmd.ExecuteNonQuery();
                }

                return("You have successfully created the season " + seasonName + ".");
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2627)
                    return("A season with the name " + seasonName + " already exists.");
                else
                    return("Please contact your system administrator.");
            }
            catch (Exception ex)
            {
                return("Please contact your system administrator.");
            }
            finally
            {
                sqlcon.Close();
            }
        }

        public string assignSeasons(SqlConnection sqlcon, int programID, int[] seasonIDs)
        {
            try
            {
                sqlcon.Open();
                using (SqlCommand sqlcmd = new SqlCommand("DELETE FROM touSeasonProgram WHERE progID=@progID", sqlcon))
                {
                    sqlcmd.Parameters.AddWithValue("@progID", programID);
                    sqlcmd.ExecuteNonQuery();
                }

                foreach (int seasonID in seasonIDs)
                {
                    using (SqlCommand sqlcmd = new SqlCommand("INSERT INTO touSeasonProgram (seasonID, progID) VALUES (@seasonID, @progID)", sqlcon))
                    {
                        sqlcmd.Parameters.AddWithValue("@seasonID", seasonID);
                        sqlcmd.Parameters.AddWithValue("@progID", programID);
                        sqlcmd.ExecuteNonQuery();
                    }
                }

                return ("You have successfully assigned the Program.");
            }
            catch (Exception ex)
            {
                return ("Please contact your system administrator.");
            }
            finally
            {
                sqlcon.Close();
            }
        }

        public string toggleSeason(SqlConnection sqlcon, int seasonID)
        {
            try
            {
                sqlcon.Open();
                using (SqlCommand sqlcmd = new SqlCommand("toggleSeason", sqlcon))
                {
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@seasonID", seasonID);
                    sqlcmd.ExecuteNonQuery();
                }

                return ("Season was successfully toggled.");
            }
            catch (Exception ex)
            {
                return ("Please contact your system administrator.");
            }
            finally
            {
                sqlcon.Close();
            }
        }

        public string toggleFuelAdjust(SqlConnection sqlcon, int fuelID)
        {
            try
            {
                sqlcon.Open();
                using (SqlCommand sqlcmd = new SqlCommand("toggleFuel", sqlcon))
                {
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@fuelID", fuelID);
                    sqlcmd.ExecuteNonQuery();
                }

                return ("Adjustment was successfully toggled.");
            }
            catch (Exception ex)
            {
                return ("Please contact your system administrator.");
            }
            finally
            {
                sqlcon.Close();
            }
        }

        public string toggleTaxAdjust(SqlConnection sqlcon, int taxID)
        {
            try
            {
                sqlcon.Open();
                using (SqlCommand sqlcmd = new SqlCommand("toggleTax", sqlcon))
                {
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@taxID", taxID);
                    sqlcmd.ExecuteNonQuery();
                }

                return ("Adjustment was successfully toggled.");
            }
            catch (Exception ex)
            {
                return ("Please contact your system administrator.");
            }
            finally
            {
                sqlcon.Close();
            }
        }

        public int retrieveProgamID(SqlConnection sqlcon, string programName)
        {
            int retID = -1;

            try 
            {
                sqlcon.Open();
                using (SqlCommand sqlcmd = new SqlCommand("SELECT ID FROM touPrograms WHERE progName=@progName", sqlcon))
                {
                    sqlcmd.Parameters.AddWithValue("@progName", programName);
                    retID = Convert.ToInt32(sqlcmd.ExecuteScalar());
                }
            }
            catch (Exception ex) { }
            finally 
            {
                sqlcon.Close();
            }

            return retID;
        }

        public static int retrieveTypeID(SqlConnection sqlcon, string programType)
        {
            int retID = -1;

            try
            {
                sqlcon.Open();
                using (SqlCommand sqlcmd = new SqlCommand("SELECT ID FROM progType WHERE typeName=@typeName", sqlcon))
                {
                    sqlcmd.Parameters.AddWithValue("@typeName", programType);
                    retID = Convert.ToInt32(sqlcmd.ExecuteScalar());
                }
            }
            catch (Exception ex) { }
            finally
            {
                sqlcon.Close();
            }

            return retID;
        }

        public int retrieveSeasonID(SqlConnection sqlcon, string seasonName)
        {
            int retID = -1;

            try
            {
                sqlcon.Open();
                using (SqlCommand sqlcmd = new SqlCommand("SELECT ID FROM touSeasons WHERE seasonName=@seasonName", sqlcon))
                {
                    sqlcmd.Parameters.AddWithValue("@seasonName", seasonName);
                    retID = Convert.ToInt32(sqlcmd.ExecuteScalar());
                }
            }
            catch (Exception ex) { }
            finally
            {
                sqlcon.Close();
            }

            return retID;
        }

        public static DataTable dtblfillPrograms(SqlConnection sqlcon, int programType)
        {
            DataTable dt = new DataTable();

            try
            {
                sqlcon.Open();
                using (SqlDataAdapter sqladapter = new SqlDataAdapter("SELECT ID, progName FROM touPrograms WHERE progTypeID=@typeID", sqlcon))
                {
                    sqladapter.SelectCommand.Parameters.AddWithValue("@typeID", programType);
                    sqladapter.Fill(dt);
                }
            }
            catch (Exception ex) { }
            finally
            {
                sqlcon.Close();
            }

            return dt;
        }

        public static DataTable dtblfillPrograms_Active(SqlConnection sqlcon, int programType)
        {
            DataTable dt = new DataTable();

            try
            {
                sqlcon.Open();
                using (SqlDataAdapter sqladapter = new SqlDataAdapter("SELECT ID, progName FROM touPrograms WHERE progTypeID=@typeID AND isActive='true'", sqlcon))
                {
                    sqladapter.SelectCommand.Parameters.AddWithValue("@typeID", programType);
                    sqladapter.Fill(dt);
                }
            }
            catch (Exception ex) { }
            finally
            {
                sqlcon.Close();
            }

            return dt;
        }

        public static DataTable dtblfillTypes(SqlConnection sqlcon)
        {
            DataTable dt = new DataTable();

            try
            {
                sqlcon.Open();
                SqlDataAdapter sqladapter = new SqlDataAdapter("SELECT ID, typeName FROM progType", sqlcon);
                sqladapter.Fill(dt);
            }
            catch (Exception ex) { }
            finally
            {
                sqlcon.Close();
            }

            return dt;
        }

        public DataTable dtblfillSeasons(SqlConnection sqlcon)
        {
            DataTable dt = new DataTable();

            try
            {
                sqlcon.Open();
                using (SqlDataAdapter sqladapter = new SqlDataAdapter("SELECT ID, seasonName FROM touSeasons", sqlcon))
                {
                    sqladapter.Fill(dt);
                }
            }
            catch (Exception) { }
            finally
            {
                sqlcon.Close();
            }

            return dt;
        }

        public DataTable peaksBind(SqlConnection sqlcon, int season_id, int programID)
        {
            DataTable dt = new DataTable();

            try
            {
                sqlcon.Open();
                //using (SqlDataAdapter sqladapter = new SqlDataAdapter("SELECT id, peakType FROM touPeakTypes WHERE isActive='true'", sqlcon))
                using (SqlDataAdapter sqladapter = new SqlDataAdapter("SELECT id, peakType FROM touPeakTypes WHERE touPeakTypes.id IN (SELECT peakTypeID FROM touSeasonPeakTypes WHERE touSeasonPeakTypes.seasonID='" + season_id + "' AND touSeasonPeakTypes.programID='" + programID + "') AND isActive='true'", sqlcon))
                {
                    sqladapter.Fill(dt);
                }
            }
            catch (Exception) { }
            finally
            {
                sqlcon.Close();
            }

            return dt;
        }

        public DataTable dtblfillAssignedSeasons(SqlConnection sqlcon, int programID)
        {
            DataTable dt = new DataTable();

            try
            {
                sqlcon.Open();
                using (SqlDataAdapter sqladapter = new SqlDataAdapter("SELECT seasonID FROM touSeasonProgram WHERE progID=@progID", sqlcon))
                {
                    sqladapter.SelectCommand.Parameters.AddWithValue("@progID", programID);
                    sqladapter.Fill(dt);
                }
            }
            catch (Exception ex) { }
            finally
            {
                sqlcon.Close();
            }

            return dt;
        }


        public DataTable dtblfillDayTypes(SqlConnection sqlcon, int seasonID)
        {
            DataTable dt = new DataTable();

            try
            {
                sqlcon.Open();
                using (SqlDataAdapter sqladapter = new SqlDataAdapter("SELECT ID, name FROM touDayDefinitions WHERE seasonID=@seasonID", sqlcon))
                {
                    sqladapter.SelectCommand.Parameters.AddWithValue("@seasonID", seasonID);
                    sqladapter.Fill(dt);
                }
            }
            catch (Exception ex) { }
            finally
            {
                sqlcon.Close();
            }

            return dt;
    
        }

        public static string getTypes(DataTable dtTypes)
        {
            string tmpVal = "[";
            foreach (DataRow dr in dtTypes.Rows)
            {
                tmpVal += "\"";
                tmpVal += dr["typeName"].ToString().Trim();
                tmpVal += "\",";
            }
            tmpVal = tmpVal.TrimEnd(',');
            tmpVal += "];";

            return tmpVal;
        }

        public static string getProgs(DataTable dtNames)
        {
            string tmpVal = "[";
            foreach (DataRow dr in dtNames.Rows)
            {
                tmpVal += "\"";
                tmpVal += dr["progName"].ToString().Trim();
                tmpVal += "\",";
            }
            tmpVal = tmpVal.TrimEnd(',');
            tmpVal += "];";

            return tmpVal;
        }

        public touUtility()
        {
        }
    }
}