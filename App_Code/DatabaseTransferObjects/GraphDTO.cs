using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GraphDTO
/// </summary>
namespace RigTrack.DatabaseTransferObjects
{
    public class GraphDTO
    {
        public int ID { get; set; }
        public int CurveID { get; set; }
        public int CurveNumber { get; set; }
        public int TargetID { get; set; }
        public string Name { get; set; }
        public int ModelID { get; set; }
        public double MD { get; set; }
        public double INC { get; set; }
        public double Azimuth { get; set; }
        public double TVD { get; set; }
        public double EW { get; set; }
        public double NS { get; set; }
        public double Buildrate { get; set; }
        public double Walkrate { get; set; }
        public double DLS { get; set; }
        public double HoldLen { get; set; }
        public int SpacingID { get; set; }
        public bool CurrentTrend { get; set; }
        public bool StraightLineTVD { get; set; }
        public bool BuildAndWalk { get; set; }
        public bool ProposalCurve { get; set; }
        public bool MinDLSCurve { get; set; }
        public bool DLSCurve { get; set; }
        public bool ShowVertical { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifyDate { get; set; }
        public bool isActive { get; set; }
    }
}