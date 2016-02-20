using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace RigTrack.DatabaseTransferObjects
{
    /// <summary>
    /// Summary description for BHABitDataDTO
    /// </summary>
    public class BHAMotorDataDTO
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public int JOBID { get; set; }
        public int BHAID { get; set; }
        public string MotorDesc { get; set; }
        public string MotorMFG { get; set; }
        public string NBStabilizer { get; set; }
        public string Model { get; set; }
        public string Revolutions { get; set; }
        public string Bend { get; set; }
        public string RotorJet { get; set; }
        public string BittoBend { get; set; }
        public string PropBUR { get; set; }
        public string RealBUR { get; set; }
        public string PadOD { get; set; }
        public string AverageDifferential { get; set; }
        public string Lobes { get; set; }
        public string OffBottomDifference { get; set; }
        public string Stages { get; set; }
        public string StallPressure { get; set; }
        public string Clearence { get; set; }
        public string AvgOnBottomSPP { get; set; }
        public string AvgOffBottomSPP { get; set; }
        public string NoOfStalls { get; set; }
        public string StallTime { get; set; }
        public string Formation { get; set; }
        public string BentSubDeg { get; set; }
        public string Elastomer { get; set; }
        public string BendType { get; set; }
        public string ClearenceRng { get; set; }
        public string MEDCompany { get; set; }
        public string NoOfMWDRuns { get; set; }
        public string InspectionCmpny { get; set; }
        public bool MotorFailure { get; set; }
        public bool ExtendedPowerSection { get; set; }
        public bool Inspected { get; set; }
        public string ReasonPulled { get; set; }
        public string BHAObjectives { get; set; }
        public string BHAPerformance { get; set; }
        public string AdditionalComments { get; set; }
        public string BotStabilizerType { get; set; }
        public string BotStabBladeType { get; set; }
        public string BotStabLength { get; set; }
        public string LowerStabOD { get; set; }
        public string EvenWall { get; set; }
        public string TopStabilizerType { get; set; }
        public string TopStabBladeType { get; set; }
        public string TopStabLength { get; set; }
        public string UpperStabOD { get; set; }
        public string InterferenceFit { get; set; }
        public string InterferenceTol { get; set; }
        public string WearPad { get; set; }
        public string WearPadType { get; set; }
        public string Wearpadlength { get; set; }
        public string WearpadHeight { get; set; }
        public string WearpadWidth { get; set; }
        public string WearpadGuage { get; set; }
        public string BitToWearpad { get; set; }
        public string MaxSurfRPM { get; set; }
        public string MaxDLRotating { get; set; }
        public string MaxDLSliding { get; set; }
        public string MaxDiffPress { get; set; }
        public string MaxFlowRate { get; set; }
        public string MaxTorque { get; set; }
    }
}