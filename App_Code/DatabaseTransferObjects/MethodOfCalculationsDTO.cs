using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MethodOfCalculationsDTO
/// </summary>
/// 
namespace RigTrack.DatabaseTransferObjects
{
    public class MethodOfCalculationsDTO
    {
        public int CurveGroupID { get; set; }
        public int MethodOfCalculationID { get; set; }
        public int MeasurementUnitsID { get; set; }
        public bool UnitsConverted { get; set; }
        public int InputDirectionID { get; set; }
        public int OutputDirectionID { get; set; }
        public int DoglegID { get; set; }
        public int VerticalSectionID { get; set; }
        public double NSOffset { get; set; }
        public double EWOffset { get; set; }
    }
}