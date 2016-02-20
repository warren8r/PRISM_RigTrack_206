using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CurveDTO
/// </summary>
namespace RigTrack.DatabaseTransferObjects
{
    public class CurveDTO
    {
        public int ID { get; set; }
        public int CurveGroupID { get; set; }
        public int TargetID { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int CurveTypeID { get; set; }
        public double NorthOffset { get; set; }
        public double EastOffset { get; set; }
        public double VSDirection { get; set; }
        public double RKBElevation { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifyDate { get; set; }
        public bool isActive { get; set; }
        public string hexColor { get; set; }
    }
}