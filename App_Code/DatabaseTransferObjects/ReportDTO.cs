using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReportDTO
/// </summary>
namespace RigTrack.DatabaseTransferObjects
{
    public class ReportDTO
    {
        public int ID { get; set; }
        public int? TargetID { get; set; }
        public int? CompanyID { get; set; }
        public int? Grouping { get; set; }
        public int? CurveGroupID { get; set; }
        public string ReportName { get; set; }
        public string HeaderComments { get; set; }
        public int? CurveID { get; set; }
        public bool MeasuredDepth { get; set; }
        public bool Inclination { get; set; }
        public bool Azimuth { get; set; }
        public bool TrueVerticalDepth { get; set; }
        public bool NSCoordinates { get; set; }
        public bool EWCoordinates { get; set; }
        public bool VerticalSection { get; set; }
        public bool ClosureDistance { get; set; }
        public bool ClosureDirection { get; set; }
        public bool DogLegSeverity { get; set; }
        public bool ScienceFiction { get; set; }
        public bool CourseLength { get; set; }
        public bool WalkRate { get; set; }
        public bool BuildRate { get; set; }
        public bool ToolFace { get; set; }
        public bool Comment { get; set; }
        public bool SubSeaDepth { get; set; }
        public bool Radius { get; set; }
        public bool GridX { get; set; }
        public bool GridY { get; set; }
        public bool LeftRight { get; set; }
        public bool UpDown { get; set; }
        public bool FNLFSL { get; set; }
        public bool FELFWL { get; set; }
        public bool isActive { get; set; }
        public bool BoxedComments { get; set; }
        public bool ProjectToBit { get; set; }
        public bool ProjToTVD { get; set; }
        public bool ExtraHeader { get; set; }
        public bool InterpolatedReports { get; set; }
        public int? ModeReport { get; set; }
        public int? EWNSReferences { get; set; }

        public string JobNumber { get; set; }
        public string StateCountry { get; set; }
        public string Company { get; set; }
        public string Declination { get; set; }
        public string LeaseWell { get; set; }
        public string Grid { get; set; }
        public string Location { get; set; }
        public string JobName { get; set; }
        public string RigName { get; set; }
        public string CurveName { get; set; }
        public string RKB { get; set; }
        public string DateTime { get; set; }
        public string GLorMSL { get; set; }

    }
    

    public class ReportSearchDTO
    {
        public int? ID { get; set; }
        public int? CurveGroupID { get; set; }
        public string CurveGroupName { get; set; }
        public int? TargetID { get; set; }
        public string TargetName { get; set; }
        public int? CurveID { get; set; }
        public string CurveName { get; set; }
        public int? CompanyID { get; set; }
        public bool? isActive { get; set; }
    }


    public class attachments
    {
        public string name, format;
        public int  reportID,sizeInBytes;
        public byte[] data;
    }

}