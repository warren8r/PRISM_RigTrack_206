using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BHARotatorySteerableDTO
/// </summary>
namespace RigTrack.DatabaseTransferObjects
{
    public class BHARotatorySteerableDTO
    {

        public int ID { get; set; }
        public int CompanyID { get; set; }
        public int JOBID { get; set; }
        public int BHAID { get; set; }
        public string RSDesc { get; set; }
        public string RSMfg { get; set; }
        public string PushPointType { get; set; }
        public string FirmWarever { get; set; }
        public string MaxDLS { get; set; }
        public string RSLowerStab { get; set; }
        public string BitNB { get; set; }
        public string CtrBlades { get; set; }
        public string RSStabToTopStab { get; set; }
        public string BatteryInAhOut { get; set; }
        public string NumberOfBlades { get; set; }
        public string BladeTypes { get; set; }
        public string BladeProfile { get; set; }
        public string RSSno { get; set; }
        public string WakeupRPMDrill { get; set; }
        public string BladeCollapseTime { get; set; }
        public string Hardfacing { get; set; }
        public string RSGuageLength { get; set; }
        public string RSPadOD { get; set; }
        public bool RSFailure { get; set; }
        public bool RunWithMotor { get; set; }
        public string ReasonPulled { get; set; }
        public string RSPerformance { get; set; }
        public string AdditionalComments { get; set; }
    }

}