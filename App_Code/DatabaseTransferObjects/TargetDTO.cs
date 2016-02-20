using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TargetDTO
/// </summary>
namespace RigTrack.DatabaseTransferObjects
{
    public class TargetDTO
    {
        public int ID { get; set; }
        public int CurveGroupID { get; set; }
        public string Name { get; set; }
        public double TargetShapeID { get; set; }
        
        public double TVD { get; set; }
        public double NSCoordinate { get; set; }
        public double EWCoordinate { get; set; }
        public double PolarDirection { get; set; }
        public double PolarDistance { get; set; }
        public double INCFromLastTarget { get; set; }
        public double AZMFromLastTarget { get; set; }
        public double InclinationAtTarget { get; set; }
        public double AzimuthAtTarget { get; set; }
        public double NumberVertices { get; set; }
        public double Rotation { get; set; }
        public double TargetThickness { get; set; }
        public double DrawingPattern { get; set; }
        public double TargetOffsetXoffset { get; set; }
        public double TargetOffsetYoffset { get; set; }
        public double DiameterOfCircleXoffset { get; set; }
        public double DiameterOfCircleYoffset { get; set; }
        public double Corner1Xoffset { get; set; }
        public double Corner1Yoffset { get; set; }
        public double Corner2Xoffset { get; set; }
        public double Corner2Yoffset { get; set; }
        public double Corner3Xoffset { get; set; }
        public double Corner3Yoffset { get; set; }
        public double Corner4Xoffset { get; set; }
        public double Corner4Yoffset { get; set; }
        public double Corner5Xoffset { get; set; }
        public double Corner5Yoffset { get; set; }
        public double Corner6Xoffset { get; set; }
        public double Corner6Yoffset { get; set; }
        public double Corner7Xoffset { get; set; }
        public double Corner7Yoffset { get; set; }
        public double Corner8Xoffset { get; set; }
        public double Corner8Yoffset { get; set; }
        public int ReferenceOptionID { get; set; }
        public string TargetComment { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifyDate { get; set; }
        public bool isActive { get; set; }
    }

    public class TargetDTODetails
    {
        public int ID { get; set; }
        public string TargetName { get; set; }
        public int CurveGroupID { get; set; }
       
        public double TargetShapeID { get; set; }

        public double TVD { get; set; }
        public double NSCoordinate { get; set; }
        public double EWCoordinate { get; set; }
        public double PolarDirection { get; set; }
        public double PolarDistance { get; set; }
        public double INCFromLastTarget { get; set; }
        public double AZMFromLastTarget { get; set; }
        public double InclinationAtTarget { get; set; }
        public double AzimuthAtTarget { get; set; }
        public double NumberVertices { get; set; }
        public double Rotation { get; set; }
        public double TargetThickness { get; set; }
        public double DrawingPattern { get; set; }
        public double TargetOffsetXoffset { get; set; }
        public double TargetOffsetYoffset { get; set; }
        public double DiameterOfCircleXoffset { get; set; }
        public double DiameterOfCircleYoffset { get; set; }
        public double Corner1Xofffset { get; set; }
        public double Corner1Yoffset { get; set; }
        public double Corner2Xoffset { get; set; }
        public double Corner2Yoffset { get; set; }
        public double Corner3Xoffset { get; set; }
        public double Corner3Yoffset { get; set; }
        public double Corner4Xoffset { get; set; }
        public double Corner4Yoffset { get; set; }
        public string TargetComment { get; set; }
       public int ReferenceOptionID { get; set; }
        public bool isActive { get; set; }
    }

}