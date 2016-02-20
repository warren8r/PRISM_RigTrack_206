using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace RigTrack.DatabaseTransferObjects
{
    /// <summary>
    /// Summary description for BHABitDataDTO
    /// </summary>
    public class BHABitDataDTO
    {
        public int ID { get; set; }
        public string BHAID { get; set; }
        public string BitSno { get; set; }
        public string BitDesc { get; set; }
        public string ODFrac { get; set; }
        public double BitLength { get; set; }
        public string Connection { get; set; }
        public string BitType { get; set; }
        public string BearingType { get; set; }
        public string BitMfg { get; set; }
        public string BitNumber { get; set; }
        public string NUMJETS { get; set; }
        public string InnerRow { get; set; }
        public string OuterRow { get; set; }
        public string DullChar { get; set; }
        public string Location { get; set; }
        public string BearingSeals { get; set; }
        public string Guage { get; set; }
        public string OtherDullChar { get; set; }
        public string ReasonPulled { get; set; }
        public string BittoSensor { get; set; }
        public string BittoGamma { get; set; }
        public string BittoResistivity { get; set; }
        public string BittoPorosity { get; set; }
        public string BittoDNSC { get; set; }
        public string BittoGyro { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifyDate { get; set; }
        public bool isActive { get; set; }

        public double Jet1 { get; set; }
        public double Jet2 { get; set; }
        public double Jet3 { get; set; }
        public double Jet4 { get; set; }
        public double Jet5 { get; set; }
        public double Jet6 { get; set; }
        public double Jet7 { get; set; }
        public double Jet8 { get; set; }
        public double Jet9 { get; set; }
        public double Jet10 { get; set; }
    }
}