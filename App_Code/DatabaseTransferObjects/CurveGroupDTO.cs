using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CurveGroupDTO
/// </summary>
namespace RigTrack.DatabaseTransferObjects
{
    public class CurveGroupDTO
    {
        public int ID { get; set; }
        public string CurveGroupName { get; set; }
        public string JobNumber { get; set; }
        public int Company { get; set; }
        public string LeaseWell { get; set; }
        public string JobLocation { get; set; }
        public string RigName { get; set; }
        public int? StateID { get; set; }
        public int? CountryID { get; set; }
        public double? Declination { get; set; }
        public string Grid { get; set; }
        public string RKB { get; set; }
        public int? GLorMSLID { get; set; }
        public int? MethodOfCalculationID { get; set; }
        public int? DogLegSeverityID { get; set; }
        public int? MeasurementUnitsID { get; set; }
        public bool? UnitsConvert { get; set; }
        public int? OutputDirectionID { get; set; }
        public int? InputDirectionID { get; set; }
        public int? VerticalSectionReferenceID { get; set; }
        public double? EWOffset { get; set; }
        public double? NSOffset { get; set; }
        public int WorkNumber { get; set; }
        public int PlanNumber { get; set; }
        public double MD { get; set; }
        public double Incl { get; set; }
        public double Azimuth { get; set; }
        public double TVD { get; set; }
        public double NSCoord { get; set; }
        public double EWCoord { get; set; }
        public double VSection { get; set; }
        public double WRate { get; set; }
        public double BRate { get; set; }
        public double DLS { get; set; }
        public double TFO { get; set; }
        public double Closure { get; set; }
        public int LocationID { get; set; }
        public double BitToSensor { get; set; }
        public bool LeastDistanceOnOff { get; set; }
        public string LeaseDistanceText { get; set; }
        public string AtHSide { get; set; }
        public string TVDComp { get; set; }
        public int ComparisonCurveValue { get; set; }
        public string ComparisonCurveText { get; set; }
        public double At { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifyDate { get; set; }
        public DateTime? JobStartDate { get; set; }
        public DateTime? JobEndDate { get; set; }
        public DateTime? JobStartDateEnd { get; set; }
        public bool? isActive { get; set; }
        public string LatitudeLongitude { get; set; }
    }
}