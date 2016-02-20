using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BHAInfoDTO
/// </summary>
namespace RigTrack.DatabaseTransferObjects
{
    public class BHAInfoDTO
    {
        public int ID { get; set; }
        public int JOBID { get; set; }
        public int CompanyID { get; set; }
        public string BHANumber{ get; set; }
	    public string BHADesc{ get; set; }
	    public int BHAType{ get; set; }
	    public string BitSno{ get; set; }
	    public string BitDesc{ get; set; }
	    public string ODFrac{ get; set; }
	    public double BitLength{ get; set; }
	    public string Connection { get; set; }
	    public string BitType{ get; set; }
        public string BearingType { get; set; }
        public string BitMfg { get; set; }
	    public string BitNumber { get; set; }
	    public string NUMJETS { get; set; }
	    public string InnerRow{ get; set; }
	    public string OuterRow { get; set; }
	    public string DullChar{ get; set; }
	    public string Location{ get; set; }
	    public string BearingSeals { get; set; }
	    public string Guage { get; set; }
	    public string OtherDullChar { get; set; }
	    public string ReasonPulled{ get; set; }
	    public string MotorDesc { get; set; }
	    public string MotorMFG{ get; set; }
	    public string NBStabilizer { get; set; }
	    public string Model { get; set; }
	    public string Revolutions { get; set; }
	    public string Bend { get; set; }
	    public string RotorJet { get; set; }
	    public string BittoBend { get; set; }
	    public string PropBUR{ get; set; }
	    public string RealBUR{ get; set; }
	    public string PadOD{ get; set; }
	    public string AverageDifferential{ get; set; }
	    public string Lobes{ get; set; }
	    public string OffBottomDifference{ get; set; }
	    public string Stages{ get; set; }
	    public string StallPressure{ get; set; }
	    public string BittoSensor{ get; set; }
	    public string BittoGamma{ get; set; }
	    public string BittoResistivity{ get; set; }
	    public string BittoPorosity{ get; set; }
	    public string BittoDNSC{ get; set; }
	    public string BittoGyro{ get; set; }
	    public DateTime CreateDate{ get; set; }
	    public DateTime LastModifyDate{ get; set; }
        public bool isActive { get; set; }
    }
}