using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SurveyDTO
/// </summary>
namespace RigTrack.DatabaseTransferObjects
{
    public class SurveyDTO
    {
        public int ID { get; set; }
        public int CurveID { get; set; }
        public int CurveGroupID { get; set; }
        public string Name { get; set; }
        public string CurveColor { get; set; }
        public double MD { get; set; }
        public double INC { get; set; }
        public double Azimuth { get; set; }
        public double TVD { get; set; }
        public double TVDTotal { get; set; }
        public double SubseaTVD { get; set; }
        public double SubseaTVDTotal { get; set; }
        public double NS { get; set; }
        public double NSTotal { get; set; }
        public double EW { get; set; }
        public double EWTotal { get; set; }
        public double VerticalSection { get; set; }
        public double VerticalSectionTotal { get; set; }
        public double CL { get; set; }
        public double ClosureDistance { get; set; }
        public double ClosureDistanceTotal { get; set; }
        public double ClosureDirection { get; set; }
        public double ClosureDirectionTotal { get; set; }
        public double DLS { get; set; }
        public double DLA { get; set; }
        public double BR { get; set; }
        public double WR { get; set; }
        public double TFO { get; set; }
        public string SurveyComment { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifyDate { get; set; }
        public bool isActive { get; set; }
        public int RowNumber { get; set; }

        public double TieInSubsea { get; set; }
        public double TieInNS { get; set; }
        public double TieInEW { get; set; }
        public double TieInVerticalSection { get; set; }
    }
}