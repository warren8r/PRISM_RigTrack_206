using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RigTrackDO
/// </summary>
namespace RigTrack.DatabaseObjects
{
    public class RigTrackDO
    {

        // Jd manage jobs / Curve Groups section-------------------------------------------------------------------------------------------------------------
        public static DataTable GetAllDogLeg()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllDogLegs]", conn);
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

        public static DataTable GetAllMangeJobsCurveGroups()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetManageJobsCurveGroups]", conn);
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

        public static DataTable SearchJobsCurveGroups(RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_SearchManageJobsCurveGroups]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@ID", curveGroupDTO.ID);
                cmd.Parameters.AddWithValue("@CurveGroupName", curveGroupDTO.CurveGroupName);
                cmd.Parameters.AddWithValue("@Company", curveGroupDTO.Company);
                cmd.Parameters.AddWithValue("@LeaseWell", curveGroupDTO.LeaseWell);
                cmd.Parameters.AddWithValue("@JobLocation", curveGroupDTO.JobLocation);
                cmd.Parameters.AddWithValue("@RigName", curveGroupDTO.RigName);

                //cmd.Parameters.AddWithValue("@NSOffset", curveGroupDTO.NSOffset);
                cmd.Parameters.AddWithValue("@JobNumber", curveGroupDTO.JobNumber);
                //cmd.Parameters.AddWithValue("@Grid", curveGroupDTO.Grid);
                //cmd.Parameters.AddWithValue("@RKB", curveGroupDTO.RKB);
                //cmd.Parameters.AddWithValue("@Country", curveGroupDTO.CountryID);
                //cmd.Parameters.AddWithValue("@State", curveGroupDTO.StateID);
                //cmd.Parameters.AddWithValue("@Method", curveGroupDTO.MethodOfCalculationID);
                //cmd.Parameters.AddWithValue("@Convert", curveGroupDTO.UnitsConvert);
                //cmd.Parameters.AddWithValue("@MetersFeet", curveGroupDTO.MeasurementUnitsID);
                //cmd.Parameters.AddWithValue("@DogLeg", curveGroupDTO.DogLegSeverityID);
                //cmd.Parameters.AddWithValue("@GLmsl", curveGroupDTO.GLorMSLID);
                //cmd.Parameters.AddWithValue("@Declination", curveGroupDTO.Declination);
                //cmd.Parameters.AddWithValue("@OutPutDirection", curveGroupDTO.OutputDirectionID);
                //cmd.Parameters.AddWithValue("@InPutDirection", curveGroupDTO.InputDirectionID);
                //cmd.Parameters.AddWithValue("@VSection", curveGroupDTO.VerticalSectionReferenceID);
                //cmd.Parameters.AddWithValue("@EWOffset", curveGroupDTO.EWOffset); 

                cmd.Parameters.AddWithValue("@JobStartDate", curveGroupDTO.JobStartDate);
                cmd.Parameters.AddWithValue("@JobStartDateEnd", curveGroupDTO.JobStartDateEnd);

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


        public static int InsertUpdateJobsCurveGroups(RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO)
        {
            int curveGroupID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertUpdateManageJobsCurveGroups]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", curveGroupDTO.ID);
                cmd.Parameters.AddWithValue("@CurveGroupName", curveGroupDTO.CurveGroupName);
                cmd.Parameters.AddWithValue("@Company", curveGroupDTO.Company);
                cmd.Parameters.AddWithValue("@LeaseWell", curveGroupDTO.LeaseWell);
                cmd.Parameters.AddWithValue("@JobLocation", curveGroupDTO.JobLocation);
                cmd.Parameters.AddWithValue("@RigName", curveGroupDTO.RigName);

                cmd.Parameters.AddWithValue("@NSOffset", curveGroupDTO.NSOffset);
                cmd.Parameters.AddWithValue("@JobNumber", curveGroupDTO.JobNumber);
                cmd.Parameters.AddWithValue("@Grid", curveGroupDTO.Grid);
                cmd.Parameters.AddWithValue("@RKB", curveGroupDTO.RKB);
                cmd.Parameters.AddWithValue("@Country", curveGroupDTO.CountryID);
                cmd.Parameters.AddWithValue("@State", curveGroupDTO.StateID);
                cmd.Parameters.AddWithValue("@Method", curveGroupDTO.MethodOfCalculationID);
                //cmd.Parameters.AddWithValue("@Convert", curveGroupDTO.UnitsConvert);
                cmd.Parameters.AddWithValue("@MetersFeet", curveGroupDTO.MeasurementUnitsID);
                cmd.Parameters.AddWithValue("@DogLeg", curveGroupDTO.DogLegSeverityID);
                cmd.Parameters.AddWithValue("@GLmsl", curveGroupDTO.GLorMSLID);
                cmd.Parameters.AddWithValue("@Declination", curveGroupDTO.Declination);
                cmd.Parameters.AddWithValue("@OutPutDirection", curveGroupDTO.OutputDirectionID);
                cmd.Parameters.AddWithValue("@InPutDirection", curveGroupDTO.InputDirectionID);
                cmd.Parameters.AddWithValue("@VSection", curveGroupDTO.VerticalSectionReferenceID);
                cmd.Parameters.AddWithValue("@EWOffset", curveGroupDTO.EWOffset);
                cmd.Parameters.AddWithValue("@isActive", curveGroupDTO.isActive);
                cmd.Parameters.AddWithValue("@JobStartDate", curveGroupDTO.JobStartDate);
                cmd.Parameters.AddWithValue("@LatitudeLongitude", curveGroupDTO.LatitudeLongitude);
                //cmd.Parameters.AddWithValue("@JobEndDate", curveGroupDTO.JobEndDate);


                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    curveGroupID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return curveGroupID;
        }

        // End Of Jd manage jobs / Curve Groups section-------------------------------------------------------------------------------------------------------------


        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllCountries]", conn);
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

        public static DataTable GetAllCurveTypes()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllCurveTypes]", conn);
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

        public static DataTable GetAllGLMSLs()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllGLMSLs]", conn);
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

        public static DataTable GetAllInputOutputDirections()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllInputOutputDirections]", conn);
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

        public static DataTable GetAllLocations()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllLocations]", conn);
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

        public static DataTable GetAllMeasurementUnits()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllMeasurementUnits]", conn);
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

        public static DataTable GetAllMethodsOfCalculation()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllMethodsOfCalculation]", conn);
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

        public static DataTable GetAllStates()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllStates]", conn);
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

        public static DataTable GetAllVerticalSectionRefs()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllVerticalSectionRefs]", conn);
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

        public static DataTable GetGLorMSLforCurveGroup(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetGLorMSLforCurveGroup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);

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

        public static DataTable GetInputDirectionForCurveGroup(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetInputDirectionForCurveGroup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);

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

        public static DataTable GetIsUnitsConvertForCurveGroup(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetIsUnitsConvertForCurveGroup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);

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

        public static DataTable GetLocationForCurveGroup(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetLocationForCurveGroup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);

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

        public static DataTable GetLocationName(int locationID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetLocationName]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LocationID", locationID);

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

        public static DataTable GetMeasurementUnitsForCurveGroup(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetMeasurementUnitsForCurveGroup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);

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

        public static DataTable GetMethodOfCalculationForCurveGroup(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetMethodOfCalculationForCurveGroup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);

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

        public static DataTable GetOutputDirectionForCurveGroup(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetOutputDirectionForCurveGroup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);

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

        public static DataTable GetStateCountryForCurveGroup(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetStateCountryForCurveGroup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);

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

        public static DataTable GetVerticalSectionRefForCurveGroup(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetVerticalSectionRefForCurveGroup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);

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

        public static int InsertUpdateCurveGroup(RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO)
        {
            int curveGroupID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertUpdateCurveGroup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", curveGroupDTO.ID);
                cmd.Parameters.AddWithValue("@CurveGroupName", curveGroupDTO.CurveGroupName);
                cmd.Parameters.AddWithValue("@JobNumber", curveGroupDTO.JobNumber);
                cmd.Parameters.AddWithValue("@Company", curveGroupDTO.Company);
                cmd.Parameters.AddWithValue("@LeaseWell", curveGroupDTO.LeaseWell);
                cmd.Parameters.AddWithValue("@JobLocation", curveGroupDTO.JobLocation);
                cmd.Parameters.AddWithValue("@RigName", curveGroupDTO.RigName);
                cmd.Parameters.AddWithValue("@StateID", curveGroupDTO.StateID);
                cmd.Parameters.AddWithValue("@CountryID", curveGroupDTO.CountryID);
                cmd.Parameters.AddWithValue("@Declination", curveGroupDTO.Declination);
                cmd.Parameters.AddWithValue("@Grid", curveGroupDTO.Grid);
                cmd.Parameters.AddWithValue("@RKB", curveGroupDTO.RKB);
                cmd.Parameters.AddWithValue("@GLorMSLID", curveGroupDTO.GLorMSLID);
                cmd.Parameters.AddWithValue("@MethodOfCalculationID", curveGroupDTO.MethodOfCalculationID);
                cmd.Parameters.AddWithValue("@DogLegSeverityID", curveGroupDTO.DogLegSeverityID);
                cmd.Parameters.AddWithValue("@OutputDirectionID", curveGroupDTO.OutputDirectionID);
                cmd.Parameters.AddWithValue("@InputDirectionID", curveGroupDTO.InputDirectionID);
                cmd.Parameters.AddWithValue("@VerticalSectionReferenceID", curveGroupDTO.VerticalSectionReferenceID);
                cmd.Parameters.AddWithValue("@EWOffset", curveGroupDTO.EWOffset);
                cmd.Parameters.AddWithValue("@NSOffset", curveGroupDTO.NSOffset);
                cmd.Parameters.AddWithValue("@UnitsConvert", curveGroupDTO.UnitsConvert);
                cmd.Parameters.AddWithValue("@MeasurementUnitsID", curveGroupDTO.MeasurementUnitsID);
                cmd.Parameters.AddWithValue("@WorkNumber", curveGroupDTO.WorkNumber);
                cmd.Parameters.AddWithValue("@PlanNumber", curveGroupDTO.PlanNumber);
                cmd.Parameters.AddWithValue("@MD", curveGroupDTO.MD);
                cmd.Parameters.AddWithValue("@Incl", curveGroupDTO.Incl);
                cmd.Parameters.AddWithValue("@Azimuth", curveGroupDTO.Azimuth);
                cmd.Parameters.AddWithValue("@TVD", curveGroupDTO.TVD);
                cmd.Parameters.AddWithValue("@NSCoord", curveGroupDTO.NSCoord);
                cmd.Parameters.AddWithValue("@EWCoord", curveGroupDTO.EWCoord);
                cmd.Parameters.AddWithValue("@VSection", curveGroupDTO.VSection);
                cmd.Parameters.AddWithValue("@WRate", curveGroupDTO.WRate);
                cmd.Parameters.AddWithValue("@BRate", curveGroupDTO.BRate);
                cmd.Parameters.AddWithValue("@DLS", curveGroupDTO.DLS);
                cmd.Parameters.AddWithValue("@TFO", curveGroupDTO.TFO);
                cmd.Parameters.AddWithValue("@Closure", curveGroupDTO.Closure);
                cmd.Parameters.AddWithValue("@BitToSensor", curveGroupDTO.BitToSensor);
                cmd.Parameters.AddWithValue("@At", curveGroupDTO.At);
                cmd.Parameters.AddWithValue("@Location", curveGroupDTO.LocationID);
                cmd.Parameters.AddWithValue("@LeastDistanceOnOff", curveGroupDTO.LeastDistanceOnOff);
                cmd.Parameters.AddWithValue("@LeastDistanceText", curveGroupDTO.LeaseDistanceText);
                cmd.Parameters.AddWithValue("@AtHSide", curveGroupDTO.AtHSide);
                cmd.Parameters.AddWithValue("@TVDComp", curveGroupDTO.TVDComp);
                cmd.Parameters.AddWithValue("@ComparisonCurveValue", curveGroupDTO.ComparisonCurveValue);
                cmd.Parameters.AddWithValue("@ComparisonCurveText", curveGroupDTO.ComparisonCurveText);
                cmd.Parameters.AddWithValue("@isActive", curveGroupDTO.isActive);

                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    curveGroupID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return curveGroupID;
        }

        public static void InsertUpdateSurvey(RigTrack.DatabaseTransferObjects.SurveyDTO surveyDTO)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertUpdateSurvey]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", surveyDTO.ID);
                cmd.Parameters.AddWithValue("@CurveID", surveyDTO.CurveID);
                cmd.Parameters.AddWithValue("@CurveGroupID", surveyDTO.CurveGroupID);
                cmd.Parameters.AddWithValue("@Name", surveyDTO.Name);
                cmd.Parameters.AddWithValue("@MD", surveyDTO.MD);
                cmd.Parameters.AddWithValue("@INC", surveyDTO.INC);
                cmd.Parameters.AddWithValue("@Azimuth", surveyDTO.Azimuth);
                cmd.Parameters.AddWithValue("@TVD", surveyDTO.TVD);

                cmd.Parameters.AddWithValue("@SubseaTVD", surveyDTO.SubseaTVD);

                cmd.Parameters.AddWithValue("@NS", surveyDTO.NS);

                cmd.Parameters.AddWithValue("@EW", surveyDTO.EW);

                cmd.Parameters.AddWithValue("@VerticalSection", surveyDTO.VerticalSection);

                cmd.Parameters.AddWithValue("@CL", surveyDTO.CL);
                cmd.Parameters.AddWithValue("@ClosureDistance", surveyDTO.ClosureDistance);

                cmd.Parameters.AddWithValue("@ClosureDirection", surveyDTO.ClosureDirection);

                cmd.Parameters.AddWithValue("@DLS", surveyDTO.DLS);
                cmd.Parameters.AddWithValue("@DLA", surveyDTO.DLA);
                cmd.Parameters.AddWithValue("@BR", surveyDTO.BR);
                cmd.Parameters.AddWithValue("@WR", surveyDTO.WR);
                cmd.Parameters.AddWithValue("@TFO", surveyDTO.TFO);
                cmd.Parameters.AddWithValue("@SurveyComment", surveyDTO.SurveyComment);
                cmd.Parameters.AddWithValue("@isActive", surveyDTO.isActive);
                cmd.Parameters.AddWithValue("@RowNumber", surveyDTO.RowNumber);
                cmd.Parameters.AddWithValue("@TieInSubseaTVD", surveyDTO.TieInSubsea);
                cmd.Parameters.AddWithValue("@TieInNS", surveyDTO.TieInNS);
                cmd.Parameters.AddWithValue("@TieInEW", surveyDTO.TieInEW);
                cmd.Parameters.AddWithValue("@TieInVerticalSection", surveyDTO.TieInVerticalSection);

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

        public static void UpdateCurve(RigTrack.DatabaseTransferObjects.CurveDTO curveDTO)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_UpdateCurve]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", curveDTO.ID);
                cmd.Parameters.AddWithValue("@CurveGroupID", curveDTO.CurveGroupID);
                cmd.Parameters.AddWithValue("@Number", curveDTO.Number);
                cmd.Parameters.AddWithValue("@Name", curveDTO.Name);
                cmd.Parameters.AddWithValue("@CurveTypeID", curveDTO.CurveTypeID);
                cmd.Parameters.AddWithValue("@NorthOffset", curveDTO.NorthOffset);
                cmd.Parameters.AddWithValue("@EastOffset", curveDTO.EastOffset);
                cmd.Parameters.AddWithValue("@VSDirection", curveDTO.VSDirection);
                cmd.Parameters.AddWithValue("@RKBElevation", curveDTO.RKBElevation);
                cmd.Parameters.AddWithValue("@isActive", curveDTO.isActive);

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

        public static DataTable GetAllCurvesForCurveGroup(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllCurvesForCurveGroup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);

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

        public static void DeleteCurveGroupAndCurves(int curveGroupID)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_DeleteCurveGroupAndCurves]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);

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

        public static DataTable GetAllCurveGroupNames()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllCurveGroupNames]", conn);
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

        public static DataTable GetCurveGroupsForPlotByCompany(int CompanyID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCurveGroupsForPlotByCompany]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
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

        public static DataTable GetAllCurveGroupNamesHaveTargets()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllCurveGroupNamesTargets]", conn);
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

        public static DataTable GetAllTargetShapes(int TargetID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllTargetShapes]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@targetID", TargetID);
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

        public static DataTable GetAllModeReports()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllModeReports]", conn);
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

        public static DataTable GetAllEWNSReferences()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllEWNSReferences]", conn);
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

        public static DataTable GetAllJobNumbers()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllJobNumbers]", conn);
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

        public static DataTable GetAllCompanies()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllCompanies]", conn);
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
        public static DataTable GetAllCompanies2()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllCompanies2]", conn);
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

        public static DataTable GetAllLeaseWell()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllLeaseWell]", conn);
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

        public static DataTable GetAllRigNames()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllRigNames]", conn);
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

        public static DataTable GetAllCurveGroups()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllCurveGroups]", conn);
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

        public static DataTable SearchAllCurveGroups(RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_SearchAllCurveGroups]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupDTO.ID);
                cmd.Parameters.AddWithValue("@CurveGroupName", curveGroupDTO.CurveGroupName);
                cmd.Parameters.AddWithValue("@JobNumber", curveGroupDTO.JobNumber);
                cmd.Parameters.AddWithValue("@JobLocation", curveGroupDTO.JobLocation);
                cmd.Parameters.AddWithValue("@Company", curveGroupDTO.Company);
                cmd.Parameters.AddWithValue("@LeaseWell", curveGroupDTO.LeaseWell);
                cmd.Parameters.AddWithValue("@RigName", curveGroupDTO.RigName);
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

        public static DataTable GetCurveGroupFromID(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCurveGroupFromID]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);

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

        public static DataTable GetTargetsForCurveGroup(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetsForCurveGroup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);

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


        public static DataTable GetTargetsForAllCurves(int curveID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetsForAllCurves]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveID", curveID);

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


        public static DataTable GetSurveysForCurve(int curveID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetSurveysForCurve]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveID", curveID);
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

        public static DataTable GetCurvesForTarget(int targetID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCurvesForTarget]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TargetID", targetID);
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



        public static DataTable GetCurvesForCurveGroupActive(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCurvesForCurveGroupActive]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);
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
        public static DataTable GetCurvesForCurveGroup(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCurvesForCurveGroup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);
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

        public static int InsertUpdateCurve(RigTrack.DatabaseTransferObjects.CurveDTO curveDTO)
        {
            int curveID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertUpdateCurve]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", curveDTO.ID);
                cmd.Parameters.AddWithValue("@CurveGroupID", curveDTO.CurveGroupID);
                cmd.Parameters.AddWithValue("@TargetID", curveDTO.TargetID);
                cmd.Parameters.AddWithValue("@Number", curveDTO.Number);
                cmd.Parameters.AddWithValue("@Name", curveDTO.Name);
                cmd.Parameters.AddWithValue("@CurveTypeID", curveDTO.CurveTypeID);
                cmd.Parameters.AddWithValue("@NorthOffset", curveDTO.NorthOffset);
                cmd.Parameters.AddWithValue("@EastOffset", curveDTO.EastOffset);
                cmd.Parameters.AddWithValue("@VSDirection", curveDTO.VSDirection);
                cmd.Parameters.AddWithValue("@RKBElevation", curveDTO.RKBElevation);
                cmd.Parameters.AddWithValue("@Color", curveDTO.hexColor);

                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    curveID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return curveID;
        }

        public static DataTable GetTargetForCurve(int curveID)
        {
            DataTable returnTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetForCurve]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveID", curveID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    da.Fill(returnTable);
                }
                catch (Exception ex)
                {

                }
            }
            return returnTable;
        }

        public static void DeleteSurvey(int surveyID)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_DeleteSurvey]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SurveyID", surveyID);
                conn.Open();
                try
                {

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static int InsertUpdateTarget(RigTrack.DatabaseTransferObjects.TargetDTO targetDTO)
        {
            int curveID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertUpdateTargets]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", targetDTO.ID);
                cmd.Parameters.AddWithValue("@CurveGroupID", targetDTO.CurveGroupID);
                cmd.Parameters.AddWithValue("@Name", targetDTO.Name);
                cmd.Parameters.AddWithValue("@TargetOffsetXoffset", targetDTO.TargetOffsetXoffset);
                cmd.Parameters.AddWithValue("@TargetOffsetYoffset", targetDTO.TargetOffsetYoffset);
                cmd.Parameters.AddWithValue("@DiameterOfCircleXoffset", targetDTO.DiameterOfCircleXoffset);
                cmd.Parameters.AddWithValue("@DiameterOfCircleYoffset", targetDTO.DiameterOfCircleYoffset);
                cmd.Parameters.AddWithValue("@Corner1Xofffset", targetDTO.Corner1Xoffset);
                cmd.Parameters.AddWithValue("@Corner1Yoffset", targetDTO.Corner1Yoffset);
                cmd.Parameters.AddWithValue("@Corner2Xoffset", targetDTO.Corner2Xoffset);
                cmd.Parameters.AddWithValue("@Corner2Yoffset", targetDTO.Corner2Yoffset);
                cmd.Parameters.AddWithValue("@Corner3Xoffset", targetDTO.Corner3Xoffset);
                cmd.Parameters.AddWithValue("@Corner3Yoffset", targetDTO.Corner3Yoffset);
                cmd.Parameters.AddWithValue("@Corner4Xoffset", targetDTO.Corner4Xoffset);
                cmd.Parameters.AddWithValue("@Corner4Yoffset", targetDTO.Corner4Yoffset);
                cmd.Parameters.AddWithValue("@ReferenceOptionID", targetDTO.ReferenceOptionID);
                cmd.Parameters.AddWithValue("@CreateDate", targetDTO.CreateDate);
                cmd.Parameters.AddWithValue("@LastModifyDate", targetDTO.LastModifyDate);
                cmd.Parameters.AddWithValue("@isActive", targetDTO.isActive);
                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    curveID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return curveID;
        }

        public static int InsertUpdateTargetDetails(RigTrack.DatabaseTransferObjects.TargetDTODetails targetDTODetails)
        {
            int curveID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertUpdateTargetDetails]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", targetDTODetails.ID);
                cmd.Parameters.AddWithValue("@TargetName", targetDTODetails.TargetName);
                cmd.Parameters.AddWithValue("@CurveGroupID", targetDTODetails.CurveGroupID);
                cmd.Parameters.AddWithValue("@TargetShapeID", targetDTODetails.TargetShapeID);
                cmd.Parameters.AddWithValue("@TVD", targetDTODetails.TVD);
                cmd.Parameters.AddWithValue("@NSCoordinate", targetDTODetails.NSCoordinate);
                cmd.Parameters.AddWithValue("@EWCoordinate", targetDTODetails.EWCoordinate);
                cmd.Parameters.AddWithValue("@PolarDirection", targetDTODetails.PolarDirection);
                cmd.Parameters.AddWithValue("@PolarDistance", targetDTODetails.PolarDistance);
                cmd.Parameters.AddWithValue("@INCFromLastTarget", targetDTODetails.INCFromLastTarget);
                cmd.Parameters.AddWithValue("@AZMFromLastTarget", targetDTODetails.AZMFromLastTarget);
                cmd.Parameters.AddWithValue("@InclinationAtTarget", targetDTODetails.InclinationAtTarget);
                cmd.Parameters.AddWithValue("@AzimuthAtTarget", targetDTODetails.AzimuthAtTarget);
                cmd.Parameters.AddWithValue("@NumberVertices", targetDTODetails.NumberVertices);
                cmd.Parameters.AddWithValue("@Rotation", targetDTODetails.Rotation);
                cmd.Parameters.AddWithValue("@TargetThickness", targetDTODetails.TargetThickness);
                cmd.Parameters.AddWithValue("@DrawingPattern", targetDTODetails.DrawingPattern);
                cmd.Parameters.AddWithValue("@TargetComment", targetDTODetails.TargetComment);
                cmd.Parameters.AddWithValue("@TargetOffsetXoffset", targetDTODetails.TargetOffsetXoffset);
                cmd.Parameters.AddWithValue("@TargetOffsetYoffset", targetDTODetails.TargetOffsetYoffset);
                cmd.Parameters.AddWithValue("@DiameterOfCircleXoffset", targetDTODetails.DiameterOfCircleXoffset);
                cmd.Parameters.AddWithValue("@DiameterOfCircleYoffset", targetDTODetails.DiameterOfCircleYoffset);
                cmd.Parameters.AddWithValue("@Corner1Xofffset", targetDTODetails.Corner1Xofffset);
                cmd.Parameters.AddWithValue("@Corner1Yoffset", targetDTODetails.Corner1Yoffset);
                cmd.Parameters.AddWithValue("@Corner2Xoffset", targetDTODetails.Corner2Xoffset);
                cmd.Parameters.AddWithValue("@Corner2Yoffset", targetDTODetails.Corner2Yoffset);
                cmd.Parameters.AddWithValue("@Corner3Xoffset", targetDTODetails.Corner3Xoffset);
                cmd.Parameters.AddWithValue("@Corner3Yoffset", targetDTODetails.Corner3Yoffset);
                cmd.Parameters.AddWithValue("@Corner4Xoffset", targetDTODetails.Corner4Xoffset);
                cmd.Parameters.AddWithValue("@Corner4Yoffset", targetDTODetails.Corner4Yoffset);
                
                cmd.Parameters.AddWithValue("@ReferenceOptionID", targetDTODetails.ReferenceOptionID);
                cmd.Parameters.AddWithValue("@isActive", targetDTODetails.isActive);

                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    curveID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return curveID;
        }
        #region Close Job Methods
        public static int CloseJob(int CurveGroupID, DateTime CloseDate, string UserComments, bool IsAttachment)
        {
            int OperationReturn = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_CloseJob]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", CurveGroupID);
                cmd.Parameters.AddWithValue("@CloseDate", CloseDate);
                cmd.Parameters.AddWithValue("@Comments", UserComments);
                cmd.Parameters.AddWithValue("@isAttachment", IsAttachment);
                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    OperationReturn = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return OperationReturn;
        }
        public static DataTable GetAllCurveGroupsNotClosed()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllCurveGroupsNotClosed]", conn);
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
        public static DataTable GetAllCurveGroupsNotClosed_WithParm(int IsActive)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllCurveGroupsNotClosed_WithParm]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
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
        public static DataTable GetAttachments(int CurveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAttachments]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", CurveGroupID);
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
        public static DataTable SearchAllCurveGroupsForClose(RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_SearchAllCurveGroups2]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupDTO.ID);
                cmd.Parameters.AddWithValue("@CurveGroupName", curveGroupDTO.CurveGroupName);
                cmd.Parameters.AddWithValue("@JobNumber", curveGroupDTO.JobNumber);
                cmd.Parameters.AddWithValue("@JobLocation", curveGroupDTO.JobLocation);
                cmd.Parameters.AddWithValue("@Company", curveGroupDTO.Company);
                cmd.Parameters.AddWithValue("@LeaseWell", curveGroupDTO.LeaseWell);
                cmd.Parameters.AddWithValue("@RigName", curveGroupDTO.RigName);
                cmd.Parameters.AddWithValue("@Status", curveGroupDTO.isActive);
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
        public static string GetCurveGroupName(int CurveGroupID)
        {
            string CurveGroupName = "";
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCurveGroupName]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", CurveGroupID);
                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    CurveGroupName = Convert.ToString(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return CurveGroupName;
        }
        public static DataTable GetAllCurveGroupID_Names(int CompanyID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCurveGroupID_Names]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
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
        public static DataTable GetCurveGroupStatus(int CurveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCurveGroupStatus]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", CurveGroupID);
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
        public static DataTable GetTargetID_Names(int CompanyID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetID_Names]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
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
        public static DataTable GetTargetID_TargetNameWithCompany(int CurveGroupID, string CompanyName)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetID_TargetNameWithCompany]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", CurveGroupID);
                cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
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
        public static DataTable GetTargetID_TargetNameWithCompanyID(int CurveGroupID, int CompanyID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetID_TargetNameWithCompanyID]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", CurveGroupID);
                cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
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
        public static DataTable GetCompanyID_CompanyNameWithTargetID(int TargetID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCompanyID_CompanyNameWithTarget]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TargetID", TargetID);
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
        public static DataTable GetCurveGroupID_CurveGroupNameWithTarget(int TargetID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCurveGroupID_NamesWithTargetID]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TargetID", TargetID);
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
        public static DataTable GetEveryCurveGroupName()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllCurveGroupID_Names]", conn);
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
        public static DataTable GetEveryCompany()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllCompanyID_Names]", conn);
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
        public static DataTable GetEveryCompanyThatHasCurveGroup()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllCompanyID_NamesWithCurveGroup]", conn);
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
        public static DataTable GetCurveGroupForCompany(int CompanyID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCompanyCurveGroupID_Names]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
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
        public static int UploadFiles(string Filename, byte[] FileBytes, string FileType, int CurveGroupID)
        {
            int AttachmentID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertAttachment]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", CurveGroupID);
                cmd.Parameters.AddWithValue("@Name", Filename);
                cmd.Parameters.AddWithValue("@Type", FileType);
                cmd.Parameters.AddWithValue("@Attachment", FileBytes);
                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    AttachmentID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return AttachmentID;
        }
        #endregion

        public static DataTable GetTargetsForCurveGroupID(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetsForCurveGroupID]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);

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

        public static DataTable GetTargetsOnTargetID(int TargetID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetsOnTargetID]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TargetID", TargetID);

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

        public static DataTable GetTargetsForCurveID(int curveID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetsForCurveID]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveID", curveID);

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

        public static DataTable GetCurveGroupInfo(int CurveGroupID)
        {
            DataTable returnTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCurveGroupInfo]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", CurveGroupID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    da.Fill(returnTable);
                }
                catch (Exception ex)
                {
                }

            }
            return returnTable;
        }

        public static void UpdateMethodOfCalculation(DatabaseTransferObjects.MethodOfCalculationsDTO DTO)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_UpdateCurveGroupCalculations]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", DTO.CurveGroupID);
                cmd.Parameters.AddWithValue("@MethodOfCalculationID", DTO.MethodOfCalculationID);
                cmd.Parameters.AddWithValue("@MeasurementUnitsID", DTO.MeasurementUnitsID);
                //cmd.Parameters.AddWithValue("@UnitsConverted", DTO.UnitsConverted);
                cmd.Parameters.AddWithValue("@InputDirectionID", DTO.InputDirectionID);
                cmd.Parameters.AddWithValue("@OutputDirectionID", DTO.OutputDirectionID);
                cmd.Parameters.AddWithValue("@DogLegID", DTO.DoglegID);
                cmd.Parameters.AddWithValue("@VerticalSectionID", DTO.VerticalSectionID);
                cmd.Parameters.AddWithValue("@NSOffset", DTO.NSOffset);
                cmd.Parameters.AddWithValue("@EWOffset", DTO.EWOffset);
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static DataTable GetManageJobsCurveGroupsFromID(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetManageJobsCurveGroupsFromID]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);

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

        public static DataTable GetTargetForSurvey(int targetID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetForSurvey]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TargetID", targetID);

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

        public static DataTable GetCurveInfo(int curveID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCurveInfo]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveID", curveID);
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

        public static void UpdateCurveFromSurvey(DatabaseTransferObjects.CurveDTO DTO)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_UpdateCurveFromSurvey]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", DTO.ID);
                cmd.Parameters.AddWithValue("@NorthOffset", DTO.NorthOffset);
                cmd.Parameters.AddWithValue("@EastOffset", DTO.EastOffset);
                cmd.Parameters.AddWithValue("@VSDirection", DTO.VSDirection);
                cmd.Parameters.AddWithValue("@RKBElevation", DTO.RKBElevation);

                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }


            }
        }

        public static void UpdateSurveyName(int surveyID, string name)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_UpdateSurveyName]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", surveyID);
                cmd.Parameters.AddWithValue("@Name", name);
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static void UpdateSurveyComments(int surveyID, string comments)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_UpdateSurveyComments]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", surveyID);
                cmd.Parameters.AddWithValue("@Comments", comments);
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        #region Reports
        public static DataTable GetAllReports()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_SearchReports]", conn);
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
        public static int InsertUpdateReports(RigTrack.DatabaseTransferObjects.ReportDTO createReports)
        {
            int reportID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertUpdateReports]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", createReports.ID);
                cmd.Parameters.AddWithValue("@Name", createReports.ReportName);
                cmd.Parameters.AddWithValue("@CompanyID", createReports.CompanyID);
                cmd.Parameters.AddWithValue("@CurveGroupID", createReports.CurveGroupID);
                cmd.Parameters.AddWithValue("@CurveID", createReports.CurveID);
                cmd.Parameters.AddWithValue("@MeasuredDepth", createReports.MeasuredDepth);
                cmd.Parameters.AddWithValue("@Inclination", createReports.Inclination);
                cmd.Parameters.AddWithValue("@Azimuth", createReports.Azimuth);
                cmd.Parameters.AddWithValue("@TrueVerticalDepth", createReports.TrueVerticalDepth);
                cmd.Parameters.AddWithValue("@N_SCoordinates", createReports.NSCoordinates);
                cmd.Parameters.AddWithValue("@E_WCoordinates", createReports.EWCoordinates);
                cmd.Parameters.AddWithValue("@VerticalSection", createReports.VerticalSection);
                cmd.Parameters.AddWithValue("@ClosureDistance", createReports.ClosureDistance);
                cmd.Parameters.AddWithValue("@ClosureDirection", createReports.ClosureDirection);
                cmd.Parameters.AddWithValue("@DogLegSeverity", createReports.DogLegSeverity);
                cmd.Parameters.AddWithValue("@CourseLength", createReports.CourseLength);
                cmd.Parameters.AddWithValue("@WalkRate", createReports.WalkRate);
                cmd.Parameters.AddWithValue("@BuildRate", createReports.BuildRate);
                cmd.Parameters.AddWithValue("@ToolFace", createReports.ToolFace);
                cmd.Parameters.AddWithValue("@Comment", createReports.Comment);
                cmd.Parameters.AddWithValue("@SubseaDepth", createReports.SubSeaDepth);  
                //cmd.Parameters.AddWithValue("@Radius", createReports.Radius);
                //cmd.Parameters.AddWithValue("@GridX", createReports.GridX);
                //cmd.Parameters.AddWithValue("@GridY", createReports.GridY);
                //cmd.Parameters.AddWithValue("@Left_Right", createReports.LeftRight);
                //cmd.Parameters.AddWithValue("@Up_Down", createReports.UpDown);
                //cmd.Parameters.AddWithValue("@FNL_FSL", createReports.FNLFSL);
                cmd.Parameters.AddWithValue("@FEL_FWL", createReports.FELFWL);
                //cmd.Parameters.AddWithValue("@isActive", createReports.isActive);

                cmd.Parameters.AddWithValue("@HeaderComments", createReports.HeaderComments);
                cmd.Parameters.AddWithValue("@Grouping", createReports.Grouping);
                cmd.Parameters.AddWithValue("@BoxedComments", createReports.BoxedComments);
                cmd.Parameters.AddWithValue("@IncludeProjectToBit", createReports.ProjectToBit);
                cmd.Parameters.AddWithValue("@ShowProjToTVDInc", createReports.ProjToTVD);
                cmd.Parameters.AddWithValue("@ExtraHeader", createReports.ExtraHeader);
                cmd.Parameters.AddWithValue("@InterpolatedReports", createReports.InterpolatedReports);
                cmd.Parameters.AddWithValue("@Mode", createReports.ModeReport);
                cmd.Parameters.AddWithValue("@EWNSReference", createReports.EWNSReferences);
                cmd.Parameters.AddWithValue("@TargetID ", createReports.TargetID);
                cmd.Parameters.AddWithValue("@isActive", createReports.isActive);

                conn.Open();
                try
                {

                    object rtnObj = cmd.ExecuteScalar();
                    reportID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return reportID;
        }


       


        public static int AttachLogo(string Filename, byte[] FileBytes, string FileType, int ReportID)
        {
            int AttachmentID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertAttachmentLogo]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReportID", ReportID);
                cmd.Parameters.AddWithValue("@Name", Filename);
                cmd.Parameters.AddWithValue("@Type", FileType);
                cmd.Parameters.AddWithValue("@Attachment", FileBytes);
                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    AttachmentID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return AttachmentID;
        }
        public static int AttachCompanyLogo(string Filename, byte[] FileBytes, string FileType, int CompanyID)
        {
            int AttachmentID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertCompanyAttachmentLogo]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
                cmd.Parameters.AddWithValue("@Name", Filename);
                cmd.Parameters.AddWithValue("@Type", FileType);
                cmd.Parameters.AddWithValue("@Attachment", FileBytes);
                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    AttachmentID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return AttachmentID;
        }
        public static DataTable GetTargetID_TargetName_CurveID_CurveName(int CurveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetID_TargetName_CurveID_CurveName]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", CurveGroupID);
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
        public static DataTable GetTargetID_TargetName_CurveName(int CurveID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetID_TargetName_CurveName]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveID", CurveID);
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
        public static DataTable GetTargetID_TargetName(int CurveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetID_TargetName]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", CurveGroupID);
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
        public static DataTable GetCompanyID_CompanyName(int CurveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCompanyID_CompanyName]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", CurveGroupID);
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
        public static DataTable GetTargetID_TargetName_WithTarget(int TargetID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetID_TargetNameWithTarget]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TargetID", TargetID);
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
        public static DataTable SearchReports(RigTrack.DatabaseTransferObjects.ReportSearchDTO ReportSearchDTO)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_SearchReports]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", ReportSearchDTO.CurveGroupID);
                cmd.Parameters.AddWithValue("@CurveGroupName", ReportSearchDTO.CurveGroupName);
                cmd.Parameters.AddWithValue("@TargetID", ReportSearchDTO.TargetID);
                cmd.Parameters.AddWithValue("@TargetName", ReportSearchDTO.TargetName);
                cmd.Parameters.AddWithValue("@CurveID", ReportSearchDTO.CurveID);
                cmd.Parameters.AddWithValue("@CurveName", ReportSearchDTO.CurveName);
                cmd.Parameters.AddWithValue("@CompanyID", ReportSearchDTO.CompanyID);
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
        public static DataTable GetReport(int ReportID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetReport]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReportID", ReportID);
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
        public static DataTable PullAllReportValues(int ReportID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_PullAllReportValues]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReportID", ReportID);
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
        public static DataTable GetReportFromID(int reportID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetReportFromID]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReportID", reportID);
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

        public static DataTable GetAllReports(int CurveGroupID, int TargetID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_ViewDefaultReport]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", CurveGroupID);
                cmd.Parameters.AddWithValue("@TargetID", TargetID);
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
        public static DataTable GetAllJobReports(int CurveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_ViewDefaultJobReport]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", CurveGroupID);
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
        public static DataTable GetAllCompanyReports(int CompanyID, int StateID, DateTime StartDate, DateTime EndDate, bool Status)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_ViewCompanyReport]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
                cmd.Parameters.AddWithValue("@StateID", StateID);
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                cmd.Parameters.AddWithValue("@Status", Status);
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
        #endregion
        #region TargetReport
        public static DataTable GetTargetReportFromTargetID(int TargetID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetReportFromTargetID]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TargetID", TargetID);
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
        #endregion

        public static string GetCurveGroupMethodOfCalculation(int curveGroupID)
        {
            string method = "";
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCurveGroupMethodOfCalculation]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);
                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    method = Convert.ToString(rtnObj);
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }

            return method;
        }
        #region CreateReports
        public static DataTable GetCurveID_CurveNameForTarget(int targetID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCurveID_CurveNameForTarget]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TargetID", targetID);
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
        #endregion

        public static DataTable GetTieInOriginalData(int SurveyID)
        {
            DataTable returnTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTieInOriginalData]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SurveyID", SurveyID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    da.Fill(returnTable);
                }
                catch (Exception ex)
                {
                }
            }
            return returnTable;
        }

        public static DataTable GetTargetInfoFromTargetID(int targetID)
        {
            DataTable returnTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetInfoFromTargetID]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TargetID", targetID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    da.Fill(returnTable);
                }
                catch (Exception ex)
                {
                }
            }
            return returnTable;
        }

        public static void UpdateCurveLocationDetails(int CurveID, int locationID, double BitToSensor, bool ListDistanceBool, int ComparisonCurve, double HSide, double TVDComp)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_UpdateCurveLocationDetails]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", CurveID);
                cmd.Parameters.AddWithValue("@LocationID", locationID);
                cmd.Parameters.AddWithValue("@BitToSensor", BitToSensor);
                cmd.Parameters.AddWithValue("@ListDistanceBool", ListDistanceBool);
                cmd.Parameters.AddWithValue("@ComparisonCurve", ComparisonCurve);
                cmd.Parameters.AddWithValue("@AtHSide", HSide);
                cmd.Parameters.AddWithValue("@TVDComp", TVDComp);
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        #region CreateTargets
        public static DataTable SearchTargets(RigTrack.DatabaseTransferObjects.TargetDTO TargetDTO)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_SearchReports]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", TargetDTO.CurveGroupID);
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
        #endregion

        public static int CloseCurve(int curveID)
        {
            int returnCurve = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_CloseCurve]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveID", curveID);
                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    returnCurve = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return returnCurve;
        }

        public static DataTable GetTargetsLastSurvey(int targetID, int curveNumber)
        {
            DataTable returnTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetsLastSurvey]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TargetID", targetID);
                cmd.Parameters.AddWithValue("@CurveNumber", curveNumber);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    da.Fill(returnTable);
                }
                catch (Exception ex)
                {
                }
            }
            return returnTable;
        }

        public static int InsertUpdateManageTarget(RigTrack.DatabaseTransferObjects.TargetDTO targetDTO)
        {
            int targetID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertUpdateTarget]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", targetDTO.ID);
                cmd.Parameters.AddWithValue("@CurveGroupID", targetDTO.CurveGroupID);
                cmd.Parameters.AddWithValue("@TargetName", targetDTO.Name);
                cmd.Parameters.AddWithValue("@TargetShapeID", targetDTO.TargetShapeID);
                cmd.Parameters.AddWithValue("@TVD", targetDTO.TVD);
                cmd.Parameters.AddWithValue("@NSCoordinate", targetDTO.NSCoordinate);
                cmd.Parameters.AddWithValue("@EWCoordinate", targetDTO.EWCoordinate);
                cmd.Parameters.AddWithValue("@PolarDistance", targetDTO.PolarDistance);
                cmd.Parameters.AddWithValue("@PolarDirection", targetDTO.PolarDirection);
                cmd.Parameters.AddWithValue("@INCFromLastTarget", targetDTO.INCFromLastTarget);
                cmd.Parameters.AddWithValue("@AZMFromLastTarget", targetDTO.AZMFromLastTarget);
                cmd.Parameters.AddWithValue("@InclinationAtTarget", targetDTO.InclinationAtTarget);
                cmd.Parameters.AddWithValue("@AzimuthAtTarget", targetDTO.AzimuthAtTarget);
                cmd.Parameters.AddWithValue("@NumberVertices", targetDTO.NumberVertices);
                cmd.Parameters.AddWithValue("@Rotation", targetDTO.Rotation);
                cmd.Parameters.AddWithValue("@Thickness", targetDTO.TargetThickness);
                cmd.Parameters.AddWithValue("@DrawingPattern", targetDTO.DrawingPattern);
                cmd.Parameters.AddWithValue("@TargetComment", targetDTO.TargetComment);
                cmd.Parameters.AddWithValue("@TargetOffsetXOffset", targetDTO.TargetOffsetXoffset);
                cmd.Parameters.AddWithValue("@TargetOffsetYOffset", targetDTO.TargetOffsetYoffset);
                cmd.Parameters.AddWithValue("@DiameterOfCircleXOffset", targetDTO.DiameterOfCircleXoffset);
                cmd.Parameters.AddWithValue("@DiameterOfCircleYOffset", targetDTO.DiameterOfCircleYoffset);
                cmd.Parameters.AddWithValue("@Corner1XOffset", targetDTO.Corner1Xoffset);
                cmd.Parameters.AddWithValue("@Corner1YOffset", targetDTO.Corner1Yoffset);
                cmd.Parameters.AddWithValue("@Corner2XOffset", targetDTO.Corner2Xoffset);
                cmd.Parameters.AddWithValue("@Corner2YOffset", targetDTO.Corner2Yoffset);
                cmd.Parameters.AddWithValue("@Corner3XOffset", targetDTO.Corner3Xoffset);
                cmd.Parameters.AddWithValue("@Corner3YOffset", targetDTO.Corner3Yoffset);
                cmd.Parameters.AddWithValue("@Corner4XOffset", targetDTO.Corner4Xoffset);
                cmd.Parameters.AddWithValue("@Corner4YOffset", targetDTO.Corner4Yoffset);
                cmd.Parameters.AddWithValue("@Corner5XOffset", targetDTO.Corner5Xoffset);
                cmd.Parameters.AddWithValue("@Corner5YOffset", targetDTO.Corner5Yoffset);
                cmd.Parameters.AddWithValue("@Corner6XOffset", targetDTO.Corner6Xoffset);
                cmd.Parameters.AddWithValue("@Corner6YOffset", targetDTO.Corner6Yoffset);
                cmd.Parameters.AddWithValue("@Corner7XOffset", targetDTO.Corner7Xoffset);
                cmd.Parameters.AddWithValue("@Corner7YOffset", targetDTO.Corner7Yoffset);
                cmd.Parameters.AddWithValue("@Corner8XOffset", targetDTO.Corner8Xoffset);
                cmd.Parameters.AddWithValue("@Corner8YOffset", targetDTO.Corner8Yoffset);
                cmd.Parameters.AddWithValue("@ReferenceOptionID", targetDTO.ReferenceOptionID);

                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    targetID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return targetID;
        }

        public static DataTable GetSurveysForCurveGroup(int curveGroupID)
        {
            DataTable returnTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetSurveysForCurveGroup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    da.Fill(returnTable);
                }
                catch (Exception ex)
                {
                }
            }
            return returnTable;
        }

        public static DataTable GetSurveysForTarget(int targetID)
        {
            DataTable returnTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetSurveysForTarget]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TargetID", targetID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    da.Fill(returnTable);
                }
                catch (Exception ex)
                {
                }
            }
            return returnTable;
        }

        public static DataTable GetTargetShapeDetails(int targetID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetTargetShapeDetails]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TargetID", targetID);
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

        public static DataTable GetSurveysForPlot(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetSurveysForPlot]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);
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

        public static DataTable GetAllCurveGroupNamesForSurvey()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllCurveGroupNamesForSurveys]", conn);
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

        public static void SavePlotComments(int curveGroupID, string comments)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_SavePlotComments]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);
                cmd.Parameters.AddWithValue("@Comments", comments);
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public static string GetPlotComments(int curveGroupID)
        {
            string returnValue = "";
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetPlotComments]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);
                conn.Open();
                try
                {

                    object rtnObj = cmd.ExecuteScalar();
                    returnValue = Convert.ToString(rtnObj);
                }
                catch (Exception ex)
                {

                }

            }
            return returnValue;
        }

        public static DataTable GetSurveysForPlotFromCurve(int curveID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetSurveysForPlotFromCurve]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveID", curveID);
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

        public static DataTable GetCurvesForPlot(int curveGroupID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCurvesForPlot]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);
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

        public static DataTable GetAllCompanies_new()
        {
            throw new NotImplementedException();
        }

        public static int InsertUpdateCompany(RigTrack.DatabaseTransferObjects.CompanyDTO companyDTO)
        {
            int companyID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertUpdateCompany]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", companyDTO.ID);
                cmd.Parameters.AddWithValue("@CompanyName", companyDTO.CompanyName);
                cmd.Parameters.AddWithValue("@CompanyAddress1", companyDTO.CompanyAddress1);
                cmd.Parameters.AddWithValue("@CompanyAddress2", companyDTO.CompanyAddress2);
                cmd.Parameters.AddWithValue("@CompanyContactFirstName", companyDTO.CompanyContactFirstName);
                cmd.Parameters.AddWithValue("@CompanyContactLastName", companyDTO.CompanyContactLastName);
                cmd.Parameters.AddWithValue("@ContactPhone", companyDTO.ContactPhone);
                cmd.Parameters.AddWithValue("@ContactEmail", companyDTO.ContactEmail);
                cmd.Parameters.AddWithValue("@City", companyDTO.City);
                cmd.Parameters.AddWithValue("@StateID", companyDTO.StateID);
                cmd.Parameters.AddWithValue("@CountryID", companyDTO.CountryID);
                cmd.Parameters.AddWithValue("@Zip", companyDTO.Zip);
                cmd.Parameters.AddWithValue("@isAttachment", companyDTO.isAttachment);
                cmd.Parameters.AddWithValue("@isActive", companyDTO.isActive);

                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    companyID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return companyID;
        }

        public static DataTable GetCompany()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCompany]", conn);
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

        public static DataTable SearchCompany(RigTrack.DatabaseTransferObjects.CompanyDTO companyDTO)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_SearchCompany]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyName", companyDTO.CompanyName);
                cmd.Parameters.AddWithValue("@City", companyDTO.City);
                cmd.Parameters.AddWithValue("@StateID", companyDTO.StateID);

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

        public static object GetCompanyWithJob()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCompanyWithJob]", conn);
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
        public static int InsertUpdateBHAInfoDetails(RigTrack.DatabaseTransferObjects.BHAInfoDTO bhaDTO)
        {
            int curveID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertUpdateBHAInfoDetails]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", bhaDTO.ID);
                cmd.Parameters.AddWithValue("@JOBID", bhaDTO.JOBID);
                cmd.Parameters.AddWithValue("@BHANumber", bhaDTO.BHANumber);
                cmd.Parameters.AddWithValue("@BHADesc", bhaDTO.BHADesc);
                cmd.Parameters.AddWithValue("@BHAType", bhaDTO.BHAType);
                cmd.Parameters.AddWithValue("@BitSno", bhaDTO.BitSno);
                cmd.Parameters.AddWithValue("@BitDesc", bhaDTO.BitDesc);
                cmd.Parameters.AddWithValue("@ODFrac", bhaDTO.ODFrac);
                cmd.Parameters.AddWithValue("@BitLength", bhaDTO.BitLength);
                cmd.Parameters.AddWithValue("@Connection", bhaDTO.Connection);
                cmd.Parameters.AddWithValue("@BitType", bhaDTO.BitType);
                cmd.Parameters.AddWithValue("@BearingType", bhaDTO.BearingType);
                cmd.Parameters.AddWithValue("@BitMfg", bhaDTO.BitMfg);
                cmd.Parameters.AddWithValue("@BitNumber", bhaDTO.BitNumber);
                cmd.Parameters.AddWithValue("@NUMJETS", bhaDTO.NUMJETS);
                cmd.Parameters.AddWithValue("@InnerRow", bhaDTO.InnerRow);
                cmd.Parameters.AddWithValue("@OuterRow", bhaDTO.OuterRow);
                cmd.Parameters.AddWithValue("@DullChar", bhaDTO.DullChar);
                cmd.Parameters.AddWithValue("@Location", bhaDTO.Location);
                cmd.Parameters.AddWithValue("@BearingSeals", bhaDTO.BearingSeals);
                cmd.Parameters.AddWithValue("@Guage", bhaDTO.Guage);
                cmd.Parameters.AddWithValue("@OtherDullChar", bhaDTO.OtherDullChar);
                cmd.Parameters.AddWithValue("@ReasonPulled", bhaDTO.ReasonPulled);
                cmd.Parameters.AddWithValue("@MotorDesc", bhaDTO.MotorDesc);
                cmd.Parameters.AddWithValue("@MotorMFG", bhaDTO.MotorMFG);
                cmd.Parameters.AddWithValue("@NBStabilizer", bhaDTO.NBStabilizer);
                cmd.Parameters.AddWithValue("@Model", bhaDTO.Model);
                cmd.Parameters.AddWithValue("@Revolutions", bhaDTO.Revolutions);
                cmd.Parameters.AddWithValue("@Bend", bhaDTO.Bend);
                cmd.Parameters.AddWithValue("@RotorJet", bhaDTO.RotorJet);
                cmd.Parameters.AddWithValue("@BittoBend", bhaDTO.BittoBend);
                cmd.Parameters.AddWithValue("@PropBUR", bhaDTO.PropBUR);
                cmd.Parameters.AddWithValue("@RealBUR", bhaDTO.RealBUR);
                cmd.Parameters.AddWithValue("@PadOD", bhaDTO.PadOD);
                cmd.Parameters.AddWithValue("@AverageDifferential", bhaDTO.AverageDifferential);
                cmd.Parameters.AddWithValue("@Lobes", bhaDTO.Lobes);
                cmd.Parameters.AddWithValue("@OffBottomDifference", bhaDTO.OffBottomDifference);
                cmd.Parameters.AddWithValue("@Stages", bhaDTO.Stages);
                cmd.Parameters.AddWithValue("@StallPressure", bhaDTO.StallPressure);
                cmd.Parameters.AddWithValue("@BittoSensor", bhaDTO.BittoSensor);
                cmd.Parameters.AddWithValue("@BittoGamma", bhaDTO.BittoGamma);
                cmd.Parameters.AddWithValue("@BittoResistivity", bhaDTO.BittoResistivity);
                cmd.Parameters.AddWithValue("@BittoPorosity", bhaDTO.BittoPorosity);
                cmd.Parameters.AddWithValue("@BittoDNSC", bhaDTO.BittoDNSC);
                cmd.Parameters.AddWithValue("@BittoGyro", bhaDTO.BittoGyro);
                cmd.Parameters.AddWithValue("@CreateDate", bhaDTO.CreateDate);
                cmd.Parameters.AddWithValue("@LastModifyDate", bhaDTO.LastModifyDate);
                cmd.Parameters.AddWithValue("@isActive", bhaDTO.isActive);

                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    curveID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return curveID;
        }

        public static DataTable GetSurveysForJobReport(int curveID)
        {

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetSurveysForJobReport]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveID", curveID);
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

        public static string GetCurveGroupsCompany(int curveGroupID)
        {
            string returnValue = string.Empty;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetCurveGroupsCompany]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID);
                try
                {
                    conn.Open();
                    object rtnObj = cmd.ExecuteScalar();
                    if (rtnObj != null)
                    {
                        returnValue = rtnObj.ToString();
                    }

                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }

            return returnValue;
        }

        public static DataTable GetAllCurveGroupsForCompany(int companyID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetAllCurveGroupsForCompany]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyID", companyID);
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

        public static void UpdateCurveColor(int curveID, string curveColor)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_UpdateCurveColor]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", curveID);
                cmd.Parameters.AddWithValue("@Color", curveColor);

                conn.Open();
                try
                {
                    cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public static DataTable GetSurveysForWellPlan(int curveGroupID_int)
        {
            DataTable dt = new DataTable() ;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_GetSurveysForWellPlan]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", curveGroupID_int);
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

        public static void CurveGroupHasWellPlan(int CurveGroupID)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_CurveGroupHasWellPlan]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurveGroupID", CurveGroupID);
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
            }
        }
        public static int InsertUpdateBHABitData(RigTrack.DatabaseTransferObjects.BHABitDataDTO bhaDTO)
        {
            int curveID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertUpdateBHABitData]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", bhaDTO.ID);
                cmd.Parameters.AddWithValue("@BHAID", bhaDTO.BHAID);
                cmd.Parameters.AddWithValue("@BitSno", bhaDTO.BitSno);
                cmd.Parameters.AddWithValue("@BitDesc", bhaDTO.BitDesc);
                cmd.Parameters.AddWithValue("@ODFrac", bhaDTO.ODFrac);
                cmd.Parameters.AddWithValue("@BitLength", bhaDTO.BitLength);
                cmd.Parameters.AddWithValue("@Connection", bhaDTO.Connection);
                cmd.Parameters.AddWithValue("@BitType", bhaDTO.BitType);
                cmd.Parameters.AddWithValue("@BearingType", bhaDTO.BearingType);
                cmd.Parameters.AddWithValue("@BitMfg", bhaDTO.BitMfg);
                cmd.Parameters.AddWithValue("@BitNumber", bhaDTO.BitNumber);
                cmd.Parameters.AddWithValue("@NUMJETS", bhaDTO.NUMJETS);
                cmd.Parameters.AddWithValue("@InnerRow", bhaDTO.InnerRow);
                cmd.Parameters.AddWithValue("@OuterRow", bhaDTO.OuterRow);
                cmd.Parameters.AddWithValue("@DullChar", bhaDTO.DullChar);
                cmd.Parameters.AddWithValue("@Location", bhaDTO.Location);
                cmd.Parameters.AddWithValue("@BearingSeals", bhaDTO.BearingSeals);
                cmd.Parameters.AddWithValue("@Guage", bhaDTO.Guage);
                cmd.Parameters.AddWithValue("@OtherDullChar", bhaDTO.OtherDullChar);
                cmd.Parameters.AddWithValue("@ReasonPulled", bhaDTO.ReasonPulled);
                cmd.Parameters.AddWithValue("@BittoSensor", bhaDTO.BittoSensor);
                cmd.Parameters.AddWithValue("@BittoGamma", bhaDTO.BittoGamma);
                cmd.Parameters.AddWithValue("@BittoResistivity", bhaDTO.BittoResistivity);
                cmd.Parameters.AddWithValue("@BittoPorosity", bhaDTO.BittoPorosity);
                cmd.Parameters.AddWithValue("@BittoDNSC", bhaDTO.BittoDNSC);
                cmd.Parameters.AddWithValue("@BittoGyro", bhaDTO.BittoGyro);
                cmd.Parameters.AddWithValue("@CreateDate", bhaDTO.CreateDate);
                cmd.Parameters.AddWithValue("@LastModifyDate", bhaDTO.LastModifyDate);
                cmd.Parameters.AddWithValue("@isActive", bhaDTO.isActive);
                cmd.Parameters.AddWithValue("@Jet1", bhaDTO.Jet1);
                cmd.Parameters.AddWithValue("@Jet2", bhaDTO.Jet2);
                cmd.Parameters.AddWithValue("@Jet3", bhaDTO.Jet3);
                cmd.Parameters.AddWithValue("@Jet4", bhaDTO.Jet4);
                cmd.Parameters.AddWithValue("@Jet5", bhaDTO.Jet5);
                cmd.Parameters.AddWithValue("@Jet6", bhaDTO.Jet6);
                cmd.Parameters.AddWithValue("@Jet7", bhaDTO.Jet7);
                cmd.Parameters.AddWithValue("@Jet8", bhaDTO.Jet8);
                cmd.Parameters.AddWithValue("@Jet9", bhaDTO.Jet9);
                cmd.Parameters.AddWithValue("@Jet10", bhaDTO.Jet10);

                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    curveID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return curveID;
        }

        public static int InsertUpdateBHAMotorData(RigTrack.DatabaseTransferObjects.BHAMotorDataDTO bhaDTO)
        {
            int curveID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertUpdateBHAMotorData]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", bhaDTO.ID);
                cmd.Parameters.AddWithValue("@CompanyID", bhaDTO.CompanyID);
                cmd.Parameters.AddWithValue("@JOBID", bhaDTO.JOBID);
                cmd.Parameters.AddWithValue("@BHAID", bhaDTO.BHAID);
                cmd.Parameters.AddWithValue("@MotorDesc", bhaDTO.MotorDesc);
                cmd.Parameters.AddWithValue("@MotorMFG", bhaDTO.MotorMFG);
                cmd.Parameters.AddWithValue("@NBStabilizer", bhaDTO.NBStabilizer);
                cmd.Parameters.AddWithValue("@Model", bhaDTO.Model);
                cmd.Parameters.AddWithValue("@Revolutions", bhaDTO.Revolutions);
                cmd.Parameters.AddWithValue("@Bend", bhaDTO.Bend);
                cmd.Parameters.AddWithValue("@RotorJet", bhaDTO.RotorJet);
                cmd.Parameters.AddWithValue("@BittoBend", bhaDTO.BittoBend);
                cmd.Parameters.AddWithValue("@PropBUR", bhaDTO.PropBUR);
                cmd.Parameters.AddWithValue("@RealBUR", bhaDTO.RealBUR);
                cmd.Parameters.AddWithValue("@PadOD", bhaDTO.PadOD);
                cmd.Parameters.AddWithValue("@AverageDifferential", bhaDTO.AverageDifferential);
                cmd.Parameters.AddWithValue("@Lobes", bhaDTO.Lobes);
                cmd.Parameters.AddWithValue("@OffBottomDifference", bhaDTO.OffBottomDifference);
                cmd.Parameters.AddWithValue("@Stages", bhaDTO.Stages);
                cmd.Parameters.AddWithValue("@StallPressure", bhaDTO.StallPressure);
                cmd.Parameters.AddWithValue("@Clearence", bhaDTO.Clearence);
                cmd.Parameters.AddWithValue("@AvgOnBottomSPP", bhaDTO.AvgOnBottomSPP);
                cmd.Parameters.AddWithValue("@AvgOffBottomSPP", bhaDTO.AvgOffBottomSPP);
                cmd.Parameters.AddWithValue("@NoOfStalls", bhaDTO.NoOfStalls);
                cmd.Parameters.AddWithValue("@StallTime", bhaDTO.StallTime);
                cmd.Parameters.AddWithValue("@Formation", bhaDTO.Formation);
                cmd.Parameters.AddWithValue("@BentSubDeg", bhaDTO.BentSubDeg);
                cmd.Parameters.AddWithValue("@Elastomer", bhaDTO.Elastomer);
                cmd.Parameters.AddWithValue("@BendType", bhaDTO.BendType);
                cmd.Parameters.AddWithValue("@ClearenceRng", bhaDTO.ClearenceRng);
                cmd.Parameters.AddWithValue("@MEDCompany", bhaDTO.MEDCompany);
                cmd.Parameters.AddWithValue("@NoOfMWDRuns", bhaDTO.NoOfMWDRuns);
                cmd.Parameters.AddWithValue("@InspectionCmpny", bhaDTO.InspectionCmpny);
                cmd.Parameters.AddWithValue("@MotorFailure", bhaDTO.MotorFailure);
                cmd.Parameters.AddWithValue("@ExtendedPowerSection", bhaDTO.ExtendedPowerSection);
                cmd.Parameters.AddWithValue("@Inspected", bhaDTO.Inspected);
                cmd.Parameters.AddWithValue("@ReasonPulled", bhaDTO.ReasonPulled);
                cmd.Parameters.AddWithValue("@BHAObjectives", bhaDTO.BHAObjectives);
                cmd.Parameters.AddWithValue("@BHAPerformance", bhaDTO.BHAPerformance);
                cmd.Parameters.AddWithValue("@AdditionalComments", bhaDTO.AdditionalComments);
                cmd.Parameters.AddWithValue("@BotStabilizerType", bhaDTO.BotStabilizerType);
                cmd.Parameters.AddWithValue("@BotStabBladeType", bhaDTO.BotStabBladeType);
                cmd.Parameters.AddWithValue("@BotStabLength", bhaDTO.BotStabLength);
                cmd.Parameters.AddWithValue("@LowerStabOD", bhaDTO.LowerStabOD);
                cmd.Parameters.AddWithValue("@EvenWall", bhaDTO.EvenWall);
                cmd.Parameters.AddWithValue("@TopStabilizerType", bhaDTO.TopStabilizerType);
                cmd.Parameters.AddWithValue("@TopStabBladeType", bhaDTO.TopStabBladeType);
                cmd.Parameters.AddWithValue("@TopStabLength", bhaDTO.TopStabLength);
                cmd.Parameters.AddWithValue("@UpperStabOD", bhaDTO.UpperStabOD);
                cmd.Parameters.AddWithValue("@InterferenceFit", bhaDTO.InterferenceFit);
                cmd.Parameters.AddWithValue("@InterferenceTol", bhaDTO.InterferenceTol);
                cmd.Parameters.AddWithValue("@WearPad", bhaDTO.WearPad);
                cmd.Parameters.AddWithValue("@WearPadType", bhaDTO.WearPadType);
                cmd.Parameters.AddWithValue("@Wearpadlength", bhaDTO.Wearpadlength);
                cmd.Parameters.AddWithValue("@WearpadHeight", bhaDTO.WearpadHeight);
                cmd.Parameters.AddWithValue("@WearpadWidth", bhaDTO.WearpadWidth);
                cmd.Parameters.AddWithValue("@WearpadGuage", bhaDTO.WearpadGuage);
                cmd.Parameters.AddWithValue("@BitToWearpad", bhaDTO.BitToWearpad);
                cmd.Parameters.AddWithValue("@MaxSurfRPM", bhaDTO.MaxSurfRPM);
                cmd.Parameters.AddWithValue("@MaxDLRotating", bhaDTO.MaxDLRotating);
                cmd.Parameters.AddWithValue("@MaxDLSliding", bhaDTO.MaxDLSliding);
                cmd.Parameters.AddWithValue("@MaxDiffPress", bhaDTO.MaxDiffPress);
                cmd.Parameters.AddWithValue("@MaxFlowRate", bhaDTO.MaxFlowRate);
                cmd.Parameters.AddWithValue("@MaxTorque", bhaDTO.MaxTorque);

                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    curveID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return curveID;
        }
        public static int InsertUpdateBHARotatorySteerableData(RigTrack.DatabaseTransferObjects.BHARotatorySteerableDTO bhaDTO)
        {
            int curveID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertUpdateBHARotatorySteerableData]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", bhaDTO.ID);
                cmd.Parameters.AddWithValue("@CompanyID", bhaDTO.CompanyID);
                cmd.Parameters.AddWithValue("@JOBID", bhaDTO.JOBID);
                cmd.Parameters.AddWithValue("@BHAID", bhaDTO.BHAID);
                cmd.Parameters.AddWithValue("@RSDesc", bhaDTO.RSDesc);
                cmd.Parameters.AddWithValue("@RSMfg", bhaDTO.RSMfg);
                cmd.Parameters.AddWithValue("@PushPointType", bhaDTO.PushPointType);
                cmd.Parameters.AddWithValue("@FirmWarever", bhaDTO.FirmWarever);
                cmd.Parameters.AddWithValue("@MaxDLS", bhaDTO.MaxDLS);
                cmd.Parameters.AddWithValue("@RSLowerStab", bhaDTO.RSLowerStab);
                cmd.Parameters.AddWithValue("@BitNB", bhaDTO.BitNB);
                cmd.Parameters.AddWithValue("@CtrBlades", bhaDTO.CtrBlades);
                cmd.Parameters.AddWithValue("@RSStabToTopStab", bhaDTO.RSStabToTopStab);
                cmd.Parameters.AddWithValue("@BatteryInAhOut", bhaDTO.BatteryInAhOut);
                cmd.Parameters.AddWithValue("@NumberOfBlades", bhaDTO.NumberOfBlades);
                cmd.Parameters.AddWithValue("@BladeTypes", bhaDTO.BladeTypes);
                cmd.Parameters.AddWithValue("@BladeProfile", bhaDTO.BladeProfile);
                cmd.Parameters.AddWithValue("@RSSno", bhaDTO.RSSno);
                cmd.Parameters.AddWithValue("@WakeupRPMDrill", bhaDTO.WakeupRPMDrill);
                cmd.Parameters.AddWithValue("@BladeCollapseTime", bhaDTO.BladeCollapseTime);
                cmd.Parameters.AddWithValue("@Hardfacing", bhaDTO.Hardfacing);
                cmd.Parameters.AddWithValue("@RSGuageLength", bhaDTO.RSGuageLength);
                cmd.Parameters.AddWithValue("@RSPadOD", bhaDTO.RSPadOD);
                cmd.Parameters.AddWithValue("@RSFailure", bhaDTO.RSFailure);
                cmd.Parameters.AddWithValue("@RunWithMotor", bhaDTO.RunWithMotor);
                cmd.Parameters.AddWithValue("@ReasonPulled", bhaDTO.ReasonPulled);
                cmd.Parameters.AddWithValue("@RSPerformance", bhaDTO.RSPerformance);
                cmd.Parameters.AddWithValue("@AdditionalComments", bhaDTO.AdditionalComments);
                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    curveID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return curveID;
        }

        public static int InsertUpdateJobCostDetails(RigTrack.DatabaseTransferObjects.JObCostingDTO DTO)
        {
            int curveID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertUpdateJobCostingDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", DTO.ID);
                cmd.Parameters.AddWithValue("@CompanyID", DTO.CompanyID);
                cmd.Parameters.AddWithValue("@JOBID", DTO.JOBID);
                cmd.Parameters.AddWithValue("@Date", DTO.Date);
                cmd.Parameters.AddWithValue("@CostGroupID", DTO.CostGroupID);
                cmd.Parameters.AddWithValue("@ChargeBYID", DTO.ChargeBYID);
                cmd.Parameters.AddWithValue("@PriceperUnit", DTO.PriceperUnit);
                cmd.Parameters.AddWithValue("@NumberOfUnits", DTO.NumberOfUnits);
                cmd.Parameters.AddWithValue("@Total", DTO.Total);
                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    curveID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return curveID;
        }

        public static int InsertUpdatePrismAssets(RigTrack.DatabaseTransferObjects.ToolInventoryDTO DTO)
        {
            int ID = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[RigTrack].[sp_InsertUpdatePrismAssets]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", DTO.id);
                cmd.Parameters.AddWithValue("@Insert", DTO.insert);
                cmd.Parameters.AddWithValue("@AssetId", DTO.assetID);
                cmd.Parameters.AddWithValue("@WarehouseId", DTO.warehouseID);
                cmd.Parameters.AddWithValue("@AssetCategoryId", DTO.assetCategoryID);
                cmd.Parameters.AddWithValue("@AssetName", DTO.assetName);
                cmd.Parameters.AddWithValue("@SerialNumber", DTO.serialNumber);
                cmd.Parameters.AddWithValue("@Description", DTO.description);
                cmd.Parameters.AddWithValue("@Manufacturer", DTO.manufacturer);
                cmd.Parameters.AddWithValue("@Status", DTO.status);
                cmd.Parameters.AddWithValue("@ODFrac", DTO.odFrac);
                cmd.Parameters.AddWithValue("@IDFrac", DTO.idFrac);
                cmd.Parameters.AddWithValue("@Length", DTO.length);
                cmd.Parameters.AddWithValue("@TopConnection", DTO.topConnection);
                cmd.Parameters.AddWithValue("@BottomConnection", DTO.bottomConnection);
                cmd.Parameters.AddWithValue("@FishingNeck", DTO.fishingNeck);
                cmd.Parameters.AddWithValue("@StabCenterPoint", DTO.stabCenterPoint);
                cmd.Parameters.AddWithValue("@StabBladeOD", DTO.stabBladeOD);
                cmd.Parameters.AddWithValue("@Weight", DTO.weight);
                cmd.Parameters.AddWithValue("@EI", DTO.ei);
                cmd.Parameters.AddWithValue("@SizeCategory", DTO.sizeCategory);
                cmd.Parameters.AddWithValue("@Cost", DTO.cost);

                conn.Open();
                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    ID = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return ID;
        }
    }
}
