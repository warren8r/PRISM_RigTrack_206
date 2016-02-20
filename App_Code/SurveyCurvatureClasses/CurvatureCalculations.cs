using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CurvatureCalculations
/// </summary>
public static class CurvatureCalculations
{

    //NFV- Suman's implementation of Vertical Section using SurveyBuilders for information
    public static double FindVerticalSection_New(SurveyBuilder previous, SurveyBuilder current, double proposedAzimuth)
    {
        double M13 = proposedAzimuth;
        double AP19 = 0.00;
        double AQ19 = 0.00;
        double AW19 = 0.00;
        double AV19 = 0.00;
        double AX19 = 0.00;
        double AT19 = 0.00;
        if (previous.Inclination == current.Inclination && previous.Azimuth == current.Azimuth)
        {
            // AP19 = (getNorthSouthOfLastSurveyAP18() + (getPreviousPresentDepthAK19() * Math.Sin(getInclinationOfPresentSurveyF19() * Math.PI / 180) * Math.Cos(getAzimuthForPresentSurveyG19() * Math.PI / 180)));
            AP19 = (previous.North + (current.Depth * Math.Sin(current.Inclination * Math.PI / 180) * Math.Cos(current.Azimuth * Math.PI / 180)));
            // AQ19 = (getEastSouthOfLastTargetAQ18() + (getPreviousPresentDepthAK19() * Math.Sin(getInclinationOfPresentSurveyF19() * Math.PI / 180) * Math.Sin(getAzimuthForPresentSurveyG19() * Math.PI / 180)));
            AQ19 = (previous.East + (current.Depth * Math.Sin(current.Inclination * Math.PI / 180) * Math.Sin(current.Azimuth * Math.PI / 180)));
        }
        else
        {
            // AP19 = (getNorthSouthOfLastSurveyAP18() + (((180 * getPreviousPresentDepthAK19()) / (Math.PI * getAS19())) * Math.Tan((getAS19() / 2) * Math.PI / 180) * (Math.Sin(getInclinationOfLastSurveyF18() * Math.PI / 180) * Math.Cos(getPreviousAzimuthOfLastSurveyG18() * Math.PI / 180) + Math.Sin(getInclinationOfPresentSurveyF19() * Math.PI / 180) * Math.Cos(getAzimuthForPresentSurveyG19() * Math.PI / 180))));
            AP19 = (previous.North + (((180 * current.Depth) / (Math.PI * current.dogLegAngle)) * Math.Tan((current.dogLegAngle / 2) * Math.PI / 180) * (Math.Sin(previous.Inclination * Math.PI / 180) * Math.Cos(previous.Azimuth * Math.PI / 180) + Math.Sin(current.Inclination * Math.PI / 180) * Math.Cos(current.Azimuth * Math.PI / 180))));
            //AQ19 = (getEastSouthOfLastTargetAQ18() + (((180 * getPreviousPresentDepthAK19()) / (Math.PI * getAS19())) * Math.Tan((getAS19() / 2) * Math.PI / 180) * (Math.Sin(getInclinationOfLastSurveyF18() * Math.PI / 180) * Math.Sin(getPreviousAzimuthOfLastSurveyG18() * Math.PI / 180) + Math.Sin(getInclinationOfPresentSurveyF19() * Math.PI / 180) * Math.Sin(getAzimuthForPresentSurveyG19() * Math.PI / 180))));
            AQ19 = (previous.East + (((180 * current.Depth) / (Math.PI * current.dogLegAngle) * Math.Tan(current.dogLegAngle /2 ) * Math.PI / 180) * (Math.Sin(previous.Inclination * Math.PI / 180) * Math.Sin(previous.Azimuth * Math.PI /180) + Math.Sin(current.Inclination * Math.PI / 180) * Math.Sin(current.Azimuth * Math.PI / 180))));
        
        }
        if (AP19 > 0 && AQ19 == 0.00)
        {
            AW19 = 0.00;
        }
        else if (AP19 == 0 && AQ19 > 0)
        {
            AW19 = 90;
        }
        else if (AP19 < 0 && AQ19 == 0.00)
        {
            AW19 = 180; 
        }
        else if (AP19 == 0.00 && AP19 < 0)
        {
            AW19 = 270; 
        }
        else if (AP19 == 0.00 && AP19 == 0.00)
        {
            AW19 = 0.00;
        }

        if (AP19 > 0.00 && AQ19 > 0.00)
        {
            AV19 = Math.Atan(Math.Abs(AQ19 / Math.Abs(AP19)) * (180/ Math.PI));
            
        }
        else if(AP19 < 0 && AQ19 > 0.00)
        {
            AV19 = (180 - (Math.Atan(Math.Abs(AQ19 / Math.Abs(AP19)) * (180 / Math.PI))));
        }
        else if (AP19 < 0 && AQ19 < 0.00)
        {
            AV19 = (180 + (Math.Atan(Math.Abs(AQ19 / Math.Abs(AP19)) * (180 / Math.PI))));
        }
        else if (AP19 > 0 && AQ19 < 0.00)
        {
            AV19 = (360 - (Math.Atan(Math.Abs(AQ19 / Math.Abs(AP19)) * (180 / Math.PI))));
        }

        if (AW19 == 0.00 & M13 > AV19)
        {
            AX19 = M13 - AV19;
        }
        else if (AW19 == 0.00 && AV19 > M13)
        {
            AX19 = AV19 - M13;
        }
        else if (AV19 == 0.00 && AW19 > M13)
        {
            AX19 = AW19 - M13;
        }
        else if (AV19 == 0.00 && M13 > AW19)
        {
            AX19 = M13 - AW19;
        }

        AT19 = Math.Sqrt(Math.Pow(AP19, 2) + Math.Pow(AQ19, 2));


        double returnValue = 0.0;
        returnValue = AT19 * (Math.Cos(AX19 * (Math.PI / 180)));
        return returnValue ;
    }


    //Updated Methods of calculations 
    public static double DogLegAngle(SurveyBuilder a, SurveyBuilder b)
    {
        double cl = b.Depth - a.Depth;
        double doglegAngle = b.dogLegServerity * (cl / 100);
        doglegAngle = Math.Round(doglegAngle, 2);
        return doglegAngle;
    }
    public static double DogLegServerity(SurveyBuilder a, SurveyBuilder b)
    {
        double cl = b.Depth - a.Depth;
        double variable_A = a.Inclination * Math.PI / 180;
        variable_A = Math.Cos(variable_A);

        double variable_B = b.Inclination * Math.PI / 180;
        variable_B = Math.Cos(variable_B);

        double variable_C = a.Inclination * Math.PI / 180;
        variable_C = Math.Sin(variable_C);

        double variable_D = b.Inclination * Math.PI / 180;
        variable_D = Math.Sin(variable_D);

        double variable_E = b.Azimuth - a.Azimuth;
        variable_E = variable_E * Math.PI / 180;
        variable_E = Math.Cos(variable_E);

        double variable_F = 1 + variable_A * variable_B + variable_C * variable_D * variable_E;
        variable_F = variable_F * 0.5;
        variable_F = Math.Sqrt(variable_F);
        variable_F = Math.Acos(variable_F);
        double variable_G = 200 / cl;
        double doglegServerity = variable_G * variable_F * 180 / Math.PI;

        return doglegServerity;
    }

    public static double MinCurve_TVD(SurveyBuilder a, SurveyBuilder b)
    {
        double TVD = 0.00;
        double cl = b.Depth - a.Depth;
        if (a.Inclination == b.Inclination && a.Azimuth == b.Azimuth)
        {
            double variable_AA = b.Inclination * Math.PI / 180;
            variable_AA = Math.Cos(variable_AA);
            TVD = (a.TVD + cl * variable_AA);
        }
        else
        {
            double variable_A = 180 * cl;
            double variable_B = Math.PI * b.dogLegAngle;

            double variable_C = b.dogLegAngle / 2;
            variable_C = variable_C * Math.PI / 180;
            variable_C = Math.Tan(variable_C);

            double variable_D = a.Inclination * Math.PI / 180;
            variable_D = Math.Cos(variable_D);

            double variable_E = b.Inclination * Math.PI / 180;
            variable_E = Math.Cos(variable_E);

            TVD = (a.TVD + ((variable_A) / (variable_B))* variable_C * (variable_D + variable_E) );
        }

        return TVD;
    }
    public static double MinCurve_East(SurveyBuilder a, SurveyBuilder b)
    {
        double East = 0.0;
        if (a.Inclination == b.Inclination && a.Azimuth == b.Azimuth)
        {
            double AQ18 = a.East;
            double AK19 = b.Depth - a.Depth;
            double F19 = b.Inclination;
            double G19 = b.Azimuth;

            double variable_AA = Math.Sin(F19 * Math.PI / 180);
            double variable_BB = Math.Sin(G19 * Math.PI / 180);

            East = (AQ18 + (AK19 * variable_AA * variable_BB));
        }
        else
        {
            double cl = b.Depth - a.Depth;
            double variable_A = 180 * cl;
            double variable_B = Math.PI * b.dogLegAngle;
            double variable_C = Math.Tan(b.dogLegAngle / 2 * Math.PI / 180);

            double variable_D = a.Inclination * Math.PI / 180;
            variable_D = Math.Sin(variable_D);
            double variable_E = a.Azimuth * Math.PI / 180;
            variable_E = Math.Sin(variable_E);

            double variable_F = b.Inclination * Math.PI / 180;
            variable_F = Math.Sin(variable_F);

            double variable_G = b.Azimuth * Math.PI / 180;
            variable_G = Math.Sin(variable_G);

            East = (a.East + ((variable_A) / variable_B * variable_C) * (variable_D * variable_E + variable_F * variable_G));
        }


        return East;
    }
    public static double MinCurve_North(SurveyBuilder a, SurveyBuilder b)
    {
        double North = 0.0;

        if (a.Inclination == b.Inclination && a.Azimuth == b.Azimuth)
        {
            //Updated Calculations
            double variableAA = b.Depth - a.Depth;
            double variableBB = b.Inclination * Math.PI / 180;
            variableBB = Math.Sin(variableBB);
            double variableCC = b.Azimuth * Math.PI / 180;
            variableCC = Math.Cos(variableCC);
            North = (a.North + (variableAA * variableBB * variableCC));
            //North = a.North + ((b.Depth - a.Depth) * Math.Sin(b.Inclination * (Math.PI/180)) * Math.Cos(b.Azimuth * (Math.PI/180)));
        }
        else
        {
            double cl = b.Depth - a.Depth;

            double variable_A = 180 * cl;
            double variable_B = Math.PI * b.dogLegAngle;
            double variable_C = Math.Tan(b.dogLegAngle / 2 * Math.PI / 180);

            double variable_D = a.Inclination * Math.PI / 180;
            variable_D = Math.Sin(variable_D);
            double variable_E = a.Azimuth * Math.PI / 180;
            variable_E = Math.Cos(variable_E);
            double variable_F = b.Inclination * Math.PI / 180;
            variable_F = Math.Sin(variable_F);
            double variable_G = b.Azimuth * Math.PI / 180;
            variable_G = Math.Cos(variable_G);

            North = (a.North + ((variable_A) / variable_B * variable_C) * (variable_D * variable_E + variable_F * variable_G));

        }
        return North;
    }
    

    public static double AngleAvg_North(SurveyBuilder a, SurveyBuilder b)
    {
        double North = 0.0;

        double variable_A = b.Depth - a.Depth;

        double variable_B = a.Inclination + b.Inclination;
        variable_B = variable_B * Math.PI / 180;
        variable_B = variable_B / 2;
        variable_B = Math.Sin(variable_B);

        double variable_C = a.Azimuth + b.Azimuth;
        variable_C = variable_C * Math.PI / 180;
        variable_C = variable_C / 2;
        variable_C = Math.Cos(variable_C);

        North = (a.North + (variable_A * variable_B * variable_C));

        return North;
    }
    public static double AngleAvg_East(SurveyBuilder a, SurveyBuilder b)
    {
        double East = 0.0;

        double variable_A = b.Depth - a.Depth;

        double variable_B = a.Inclination + b.Inclination;
        variable_B = variable_B * Math.PI / 180;
        variable_B = variable_B / 2;
        variable_B = Math.Sin(variable_B);

        double variable_C = a.Azimuth + b.Azimuth;
        variable_C = variable_C * Math.PI / 180;
        variable_C = variable_C / 2;
        variable_C = Math.Sin(variable_C);

        East = (a.East + (variable_A * variable_B * variable_C));
        return East;
    }
    public static double AngleAvg_TVD(SurveyBuilder a, SurveyBuilder b)
    {
        double variable_A = b.Depth - a.Depth;

        double variable_B = a.Inclination + b.Inclination;
        variable_B = variable_B * Math.PI / 180;
        variable_B = Math.Cos(variable_B);

        double TVD = 0.0;
        TVD = (a.TVD + (variable_A * variable_B));

        return TVD;
    }

    public static double Radius_North(SurveyBuilder a, SurveyBuilder b)
    {
        double north = 0.0;
        /*
        double north = 0.0;
        double variable_A = b.Depth - a.Depth;

        double variable_B = a.Inclination * Math.PI / 180;
        variable_B = Math.Cos(variable_B);

        double variable_C = b.Inclination * Math.PI / 180;
        variable_C = Math.Cos(variable_B);

        double variable_D = b.Azimuth * Math.PI / 180;
        variable_D = Math.Sin(variable_D);

        double variable_E = a.Azimuth * Math.PI / 180;
        variable_E = Math.Sin(variable_E);

        double variable_F = variable_A * (variable_B - variable_C) * (variable_D - variable_E);
        double variable_G = (b.Inclination - a.Inclination) * (b.Azimuth - a.Azimuth);

        north = variable_F / variable_G;
        double variable_H = (180 / Math.PI) * (180 / Math.PI);
        north = north * variable_H;
        north = north + a.North;
        return north;
         */
        if (a.Inclination == b.Inclination )
        {
            north = MinCurve_North(a, b);
            return north;
            //b.Azimuth = b.Azimuth * Math.Pow(10, -4);
        }
        if (a.Azimuth == b.Azimuth)
        {
            north = MinCurve_North(a, b);
            return north;
        }

        
        double variable_A = b.Depth - a.Depth;
        double variable_B = a.Inclination * Math.PI / 180;
        variable_B = Math.Cos(variable_B);
        double variable_C = b.Inclination * Math.PI / 180;
        variable_C = Math.Cos(variable_C);
        double variable_E = b.Azimuth * Math.PI / 180;
        variable_E = Math.Sin(variable_E);
        double variable_F = a.Azimuth * Math.PI / 180;
        variable_F = Math.Sin(variable_F);

        double variable_G = variable_B - variable_C;
        double variable_H = variable_E - variable_F;
        variable_A = variable_A * variable_G * variable_H;

        double variable_I = (b.Inclination - a.Inclination) * (b.Azimuth - a.Azimuth);

        variable_A = variable_A / variable_I;
        variable_A = variable_A * Math.Pow((180.00 / Math.PI), 2.00);
        north = variable_A;
        return north;
    }
    public static double Radius_East(SurveyBuilder a, SurveyBuilder b)
    {
        double east = 0.0;

        if (a.Inclination == b.Inclination)
        {
            east = MinCurve_East(a, b);
            return east;
        }
        if (a.Azimuth == b.Azimuth)
        {
            east = MinCurve_East(a, b);
            return east;
        }

        double variable_A = b.Depth - a.Depth;

        double variable_B = a.Inclination * Math.PI / 180;
        variable_B = Math.Cos(variable_B);

        double variable_C = b.Inclination * Math.PI / 180;
        variable_C = Math.Cos(variable_C);

        double variable_D = b.Azimuth * Math.PI / 180;
        variable_D = Math.Cos(variable_D);

        double variable_E = a.Azimuth * Math.PI / 180;
        variable_E = Math.Cos(variable_E);

        double variable_F = variable_A * (variable_B - variable_C) * (variable_D - variable_E);

        double variable_G = (b.Inclination - a.Inclination) * (b.Azimuth - a.Azimuth);

        east = variable_F / variable_G;
        double variable_H = (180 / Math.PI) * (180 / Math.PI);
        east = east * variable_H;
        east = east + a.East;

        return east;
    }
    public static double RadiusTVD(SurveyBuilder a, SurveyBuilder b)
    {
        double TVD = 0.0;

        double variable_A = b.Depth - a.Depth;

        double variable_B = b.Inclination * Math.PI / 180;
        variable_B = Math.Sin(variable_B);

        double variable_C = a.Inclination * Math.PI / 180;
        variable_C = Math.Sin(variable_C);
        if (a.Inclination == b.Inclination)
        {
            TVD = MinCurve_TVD(a, b);
            return TVD;
        }
        double variable_D = b.Inclination - a.Inclination;

        TVD = (variable_A * (variable_B - variable_C)) / variable_D;
        TVD = TVD * 180 / Math.PI;
        TVD = TVD + a.TVD;
        return TVD;
    }

    public static double Tangential_North(SurveyBuilder a, SurveyBuilder b)
    {
        double north = 0.0;

        double variable_A = b.Depth - a.Depth;

        double variable_B = b.Inclination * Math.PI / 180;
        variable_B = Math.Sin(variable_B);

        double variable_C = b.Azimuth * Math.PI / 180;
        variable_C = Math.Cos(variable_C);

        north = variable_A * (variable_B * variable_C);
        north = north + a.North;

        return north;
    }
    public static double Tangential_East(SurveyBuilder a, SurveyBuilder b)
    {
        double east = 0.0;

        double variable_A = b.Depth - a.Depth;

        double variable_B = b.Inclination * Math.PI / 180;
        variable_B = Math.Sin(variable_B);

        double variable_C = b.Azimuth * Math.PI / 180;
        variable_C = Math.Sin(variable_C);

        east = variable_A * (variable_B * variable_C);
        east = east + a.East;
        return east;

    }
    public static double Tangential_TVD(SurveyBuilder a, SurveyBuilder b)
    {
        double TVD = 0.0;

        double variable_A = b.Depth - a.Depth;

        double variable_B = b.Inclination * Math.PI / 180;
        variable_B = Math.Cos(variable_B);

        TVD = variable_A * variable_B;

        TVD = TVD + a.TVD;

        return TVD;
    }

    public static double BalancedTangential_North(SurveyBuilder a, SurveyBuilder b)
    {
        double north = 0.0;

        double variable_A = b.Depth - a.Depth;
        variable_A = variable_A / 2;

        double variable_B = a.Inclination * Math.PI / 180;
        variable_B = Math.Sin(variable_B);

        double variable_C = a.Azimuth * Math.PI / 180;
        variable_C = Math.Cos(variable_C);

        double variable_D = b.Inclination * Math.PI / 180;
        variable_D = Math.Sin(variable_D);

        double variable_E = b.Azimuth * Math.PI / 180;
        variable_E = Math.Cos(variable_E);

        north = (variable_A * (variable_B * variable_C + variable_D * variable_E));
        north = north + a.North;

        return north;
    }
    public static double BalancedTangential_East(SurveyBuilder a, SurveyBuilder b)
    {
        double east = 0.0;
        double variable_A = b.Depth - a.Depth;
        variable_A = variable_A / 2;

        double variable_B = a.Inclination * Math.PI / 180;
        variable_B = Math.Sin(variable_B);

        double variable_C = a.Azimuth * Math.PI / 180;
        variable_C = Math.Sin(variable_C);

        double variable_D = b.Inclination * Math.PI / 180;
        variable_D = Math.Sin(variable_D);

        double variable_E = b.Azimuth * Math.PI / 180;
        variable_E = Math.Sin(variable_E);

        east = (variable_A * (variable_B * variable_C + variable_D * variable_E));
        east = east + a.East;
        return east;
    }
    public static double BalancedTangential_TVD(SurveyBuilder a, SurveyBuilder b)
    {
        double tvd = 0.0;

        double variable_A = b.Depth - a.Depth;
        variable_A = variable_A / 2;

        double variable_B = a.Inclination * Math.PI / 180;
        variable_B = Math.Cos(variable_B);

        double variable_C = b.Inclination * Math.PI / 180;
        variable_C = Math.Cos(variable_C);

        tvd = variable_A * (variable_B + variable_C);

        tvd = tvd + a.TVD;

        return tvd;
    }

    public static double FindCL(SurveyBuilder a, SurveyBuilder b)
    {
        double returnValue = 0;
        returnValue = b.Depth - a.Depth;
        return returnValue;
    }
    //Will find the 'BR' I2 - I1 - Inclination measurement
    public static double FindBR(SurveyBuilder a, SurveyBuilder b)
    {

        double returnValue = 0.0;

        double variable_A = 100.00;
        double variable_B = b.Depth - a.Depth;
        double variable_C = b.Inclination - a.Inclination;

        returnValue = ((variable_A / variable_B) * (variable_C));

        
        return returnValue;
    }
    //Will Find the 'WR' A2 - A1 -Azimuth measurement
    public static double FindWR(SurveyBuilder a, SurveyBuilder b)
    {
        double returnValue = 0.0;
        if (a.Azimuth > 270 && b.Azimuth < 90)
        {
            double variable_A = 100;
            double variable_B = b.Depth - a.Depth;
            variable_A = variable_A / variable_B;
            double variable_C = 360 - a.Azimuth;
            variable_C = variable_C + b.Azimuth;
            returnValue = variable_A * variable_C;

        }
        else if (a.Azimuth < 90 && b.Azimuth > 270)
        {
            double variable_AA = 100;
            double variable_BB = b.Depth - a.Depth;
            variable_AA = variable_AA / variable_BB;

            double variable_CC = b.Azimuth;
            double variable_DD = a.Azimuth + 360;
            variable_CC = variable_CC - variable_DD;

            returnValue = variable_AA * variable_CC;

        }
        else
        {
            double variable_AAA = 100;
            double variable_BBB = b.Depth - a.Depth;
            variable_AAA = variable_AAA / variable_BBB;
            double variable_CCC = b.Azimuth - a.Azimuth;
            returnValue = variable_AAA * variable_CCC;

           
        }
        return returnValue;
    }
    //Will find the Closure Distance based upon calculations for North,East
    //Closure Distance = SquareRoot[(North)^2 + (East)^2]
    public static double FindClosureDistance(double north, double east)
    {
        north = CurvatureConversions.ConvertToRadian(north);
        east = CurvatureConversions.ConvertToRadian(east);
        
        double returnValue = 0.0;
        double a = north * north;
        double b = east * east;
        double c = a + b;
        c = Math.Sqrt(c);
        //Convert to Degree
        returnValue = c * 180 / Math.PI;
        //returnValue = Math.Round(returnValue, 2);
        return returnValue;
    }
    //Will find the Closure Direction based upon calculations for North, East
    //Closure Direction = Tan^-1(East/North)
    public static double FindClosureDirection(double north, double east)
    {

        //double returnValue = 0.0;
        //north = CurvatureConversions.ConvertToRadian(north);
        //east = CurvatureConversions.ConvertToRadian(east);

        //double a = east / north;
        ////a = a * Math.PI / 180;
        //a = Math.Atan(a);
        //returnValue = CurvatureConversions.ConvertToDegree(a);
        //returnValue = Math.Round(returnValue, 2);
        //return returnValue;
        
        //NFV 9-24 New calculation for Closure Direction
        if (north == 0.0)
        {
            east = east * Math.PI / 180;
            //north = north * Math.PI / 180; 
            
            //double aa = (Math.Abs(east / 0.00001)) * (180 / Math.PI);
            //aa = Math.Atan(aa);
            //return aa;

            double c = Math.Atan(Math.Abs(east / 0.00001)) * (180 / Math.PI);
            return c;
        }
        else
        {
            east = east * Math.PI / 180;
            north = north * Math.PI / 180; 
            
            //double a = Math.Abs(east);
            //double b = Math.Abs(north);
            //double c = (a / b) ;
            //c = Math.Atan(c) * (180 / Math.PI);
            //return c;

            double c = Math.Atan(Math.Abs(east) / Math.Abs(north)) * (180 / Math.PI);
            return c;
        }
    }
    //To find vertical section, must be calculated after closure direction and distance is recieved 
    public static double FindVerticalSection(SurveyBuilder a, double proposedAzimuth)
    {
        //double variable_A = a.Azimuth - a.ClosureDirection;
        //variable_A = variable_A * Math.PI / 180;
        //variable_A = Math.Cos(variable_A);
        //variable_A = variable_A * a.ClosureDistance;

        //return variable_A;
        //double variable_A = a.ClosureDistance * Math.Cos(a.ClosureDirection * (Math.PI / 180));
        //return variable_A;

        //New Calculation for VS
        bool blCL_DIR1 = false;
        bool blCL_DIR2 = false;
        double CL_DIR1 = 0;
        double CL_DIR2 = 0;
        double DIR_DIF = 0;

        //Calculate CL_DIR1
        if (a.North > 0 && a.East > 0)
        {
            CL_DIR1 = a.ClosureDirection;
            blCL_DIR1 = true;
        }
        else if (a.North < 0 && a.East > 0)
        {
            CL_DIR1 = 180 - a.ClosureDirection;
            blCL_DIR1 = true;
        }
        else if (a.North < 0 && a.East < 0)
        {
            CL_DIR1 = 180 + a.ClosureDirection;
            blCL_DIR1 = true;
        }
        else if (a.North > 0 && a.East < 0)
        {
            CL_DIR1 = 360 - a.ClosureDirection;
            blCL_DIR1 = true;
        }
        else
        {
            blCL_DIR1 = false;
        }

        //Calculate CL_DIR2
        if (a.North > 0 && a.East == 0)
        {
            CL_DIR2 = 0;
            blCL_DIR2 = true;
        }
        else if (a.North == 0 && a.East > 0)
        {
            CL_DIR2 = 90;
            blCL_DIR2 = true;
        }
        else if (a.North < 0 && a.East == 0)
        {
            CL_DIR2 = 180;
            blCL_DIR2 = true;
        }
        else if (a.North == 0 && a.East < 0)
        {
            CL_DIR2 = 270;
            blCL_DIR2 = true;
        }
        else if (a.North == 0 && a.East == 0)
        {
            CL_DIR2 = 0;
            blCL_DIR2 = true;
        }
        else
        {
            blCL_DIR2 = false;
        }

        //Calcualte DIR_DIF
        if (blCL_DIR2 == false && proposedAzimuth > CL_DIR1)
        {
            DIR_DIF = proposedAzimuth - CL_DIR1;
        }
        else if (blCL_DIR2 == false && CL_DIR1 > proposedAzimuth)
        {
            DIR_DIF = CL_DIR1 - proposedAzimuth;
        }
        else if (blCL_DIR1 == false && CL_DIR2 > proposedAzimuth)
        {
            DIR_DIF = CL_DIR2 - proposedAzimuth;
        }
        else if (blCL_DIR1 == false && proposedAzimuth > CL_DIR2)
        {
            DIR_DIF = proposedAzimuth - CL_DIR2;
        }

        //Calculate VS
        double verticalSection = a.ClosureDistance * Math.Cos(DIR_DIF * (Math.PI / 180));
        return verticalSection;
        
    }
    //NFV 8-17-2016 new method of calculation
    //Calculation for Dogleg Sserverity 
    //This is done correctly, and instead of doing conversions on numbers, do inline using 180 and Math.pi 
    public static double FindDogLeg(SurveyBuilder a, SurveyBuilder b)
    {
        double cl = b.Depth - a.Depth;
        double variable_A = a.Inclination * Math.PI / 180;
        variable_A = Math.Cos(variable_A);

        double variable_B = b.Inclination * Math.PI / 180;
        variable_B = Math.Cos(variable_B);

        double variable_C = a.Inclination * Math.PI / 180;
        variable_C = Math.Sin(variable_C);

        double variable_D = b.Inclination * Math.PI / 180;
        variable_D = Math.Sin(variable_D);

        double variable_E = b.Azimuth - a.Azimuth;
        variable_E = Math.Cos(variable_E);

        double variable_F = 1 + variable_A * variable_B + variable_C * variable_D * variable_E;
        variable_F = variable_F * 0.5;
        variable_F = Math.Sqrt(variable_F);
        variable_F = Math.Acos(variable_F);
        double variable_G = 200 / cl;
        double answer = variable_G * variable_F * 180 / Math.PI;

        double dogLegAngle = answer * (cl / 100);
        return answer;
    }
    //NFV 8-25-2016 Meter/ Feet conversions
    public static double MetersToFeet(double meters)
    {
        return meters / 0.304800610;
    }
    public static double FeetToMeters(double feet)
    {
        return feet * 0.304800610;
    }
    #region Dead
    //NFV- Inital Methods of calculations.
    //No longer Used. 
    //public static MinimumCurvationResults MinimumCurvation(SurveyBuilder a, SurveyBuilder b)
    //{
    //    MinimumCurvationResults results = new MinimumCurvationResults();
    //    double CL =  FindCL(a, b);
    //    results.CL = CL;
    //    double BR = FindBR(a, b);
    //    results.BR = BR;
    //    double WR = FindWR(a, b);
    //    results.WR = WR;
    //    BR = CurvatureConversions.ConvertToRadian(BR);
    //    WR = CurvatureConversions.ConvertToRadian(WR);

    //    a.Inclination = CurvatureConversions.ConvertToRadian(a.Inclination);
    //    b.Inclination = CurvatureConversions.ConvertToRadian(b.Inclination);

    //    a.Azimuth = CurvatureConversions.ConvertToRadian(a.Azimuth);
    //    b.Azimuth = CurvatureConversions.ConvertToRadian(b.Azimuth);

    //    double variable_A = Math.Cos(BR);
    //    double variable_B = Math.Sin(a.Inclination);
    //    double variable_C = Math.Sin(b.Inclination);
    //    double variable_D = 1 - Math.Cos(WR);

    //    double variable_E = (variable_B * variable_C * variable_D);
    //    double variable_F = variable_A - variable_E;
    //    ////int i = 0;
    //    //double j = 200;
    //    //double k = CL;
    //    //double l = 0;
    //    ////int variable_G = Math.DivRem(200, CL, out i);
    //    //l = j / k;
        
    //    double dogLeg_Radian =  Math.Acos(variable_F);
    //    results.dogLeg_Radian = dogLeg_Radian;
    //    double dogleg_Degrees = CurvatureConversions.ConvertToDegree(dogLeg_Radian);
    //    results.dogLeg_Degrees = dogleg_Degrees;

    //    double ratioFactor = 2 / dogLeg_Radian * Math.Tan(dogLeg_Radian / 2);
    //    ratioFactor = Math.Round(ratioFactor, 5);
    //    results.RatioFactor = ratioFactor;

    //    double north = CL / 2 * (Math.Sin(a.Inclination) * Math.Cos(a.Azimuth) + Math.Sin(b.Inclination) * Math.Cos(b.Azimuth)) * ratioFactor;
    //    results.North = north;
    //    double east = CL / 2 * (Math.Sin(a.Inclination) * Math.Sin(a.Azimuth) + Math.Sin(b.Inclination) * Math.Sin(b.Azimuth)) * ratioFactor;
    //    results.East = east;
    //    double TVD = CL / 2 * (Math.Cos(a.Inclination) + Math.Cos(b.Inclination)) * ratioFactor;
    //    results.TVD = TVD;

    //    results.dogLeg_Radian = Math.Round(results.dogLeg_Radian, 5);
    //    results.dogLeg_Degrees = Math.Round(results.dogLeg_Degrees, 2);
    //    results.RatioFactor = Math.Round(results.RatioFactor, 5);
    //    results.North = Math.Round(results.North, 2);
    //    results.East = Math.Round(results.East, 2);
    //    results.TVD = Math.Round(results.TVD, 2);

    //    results.ClosureDistance = FindClosureDistance(results.North, results.East);
    //    results.ClosureDirection = FindClosureDirection(results.North, results.East);

    //    return results;
    //}

    //public static RadiusOfCurvatureResults RadiusOfCurvature(SurveyBuilder a, SurveyBuilder b)
    //{
    //    RadiusOfCurvatureResults results = new RadiusOfCurvatureResults();
    //    double MD = FindCL(a, b);
    //    a.Inclination = CurvatureConversions.ConvertToRadian(a.Inclination);
    //    a.Azimuth = CurvatureConversions.ConvertToRadian(a.Azimuth);
    //    b.Inclination = CurvatureConversions.ConvertToRadian(b.Inclination);
    //    b.Azimuth = CurvatureConversions.ConvertToRadian(b.Azimuth);

    //    double variable_A = (Math.Cos(a.Inclination) - Math.Cos(b.Inclination));
    //    double variable_B = (Math.Sin(b.Azimuth) - Math.Sin(a.Azimuth));
    //    double variable_C = MD * variable_A * variable_B;
    //    double variable_D = (b.Inclination - a.Inclination) * (b.Azimuth - a.Azimuth);
    //    results.North = variable_C / variable_D;
    //    double variable_E = (Math.Cos(a.Inclination) - Math.Cos(b.Inclination));
    //    double variable_F = (Math.Cos(a.Azimuth) - Math.Cos(b.Azimuth));
    //    double variable_G = MD * variable_E * variable_F;
    //    results.East = variable_G / variable_D;

    //    double variable_H = (Math.Sin(b.Inclination) - Math.Sin(a.Inclination));
    //    double variable_I = MD * variable_H;
    //    results.TVD = variable_I / (b.Inclination - a.Inclination);

    //    results.North = Math.Round(results.North, 2);
    //    results.East = Math.Round(results.East, 2);
    //    results.TVD = Math.Round(results.TVD, 2);

    //    return results;
      
    //}

    //public static AngleAverageResults AngleAverage(SurveyBuilder a, SurveyBuilder b)
    //{
    //    AngleAverageResults results = new AngleAverageResults();
    //    double MD = b.Depth - a.Depth;

    //    a.Inclination = CurvatureConversions.ConvertToRadian(a.Inclination);
    //    a.Azimuth = CurvatureConversions.ConvertToRadian(a.Azimuth);
    //    b.Inclination = CurvatureConversions.ConvertToRadian(b.Inclination);
    //    b.Azimuth = CurvatureConversions.ConvertToRadian(b.Azimuth);

    //    double variable_A = a.Inclination + b.Inclination;
    //    variable_A = variable_A / 2;
    //    variable_A = Math.Sin(variable_A);
    //    double variable_B = a.Azimuth + b.Azimuth;
    //    variable_B = variable_B / 2;
    //    variable_B = Math.Cos(variable_B);

    //    double North = MD * variable_A * variable_B;
    //    North = Math.Round(North, 2);
    //    results.North = North;

    //    double variable_D = a.Azimuth + b.Azimuth;
    //    variable_D = variable_D / 2;
    //    variable_D = Math.Sin(variable_D);
    //    double East = MD * variable_A * variable_D;
    //    East = Math.Round(East, 2);
    //    results.East = East;
    //    double variable_F = a.Inclination + b.Inclination;
    //    variable_F = variable_F / 2;
    //    variable_F = Math.Cos(variable_F);
    //    double TVD = MD * variable_F;
    //    TVD = Math.Round(TVD, 2);
    //    results.TVD = TVD;

    //    return results;

        
    //}

    //public static TangentialResults Tangential(SurveyBuilder a, SurveyBuilder b)
    //{
    //    TangentialResults results = new TangentialResults();

    //    double MD = b.Depth - a.Depth;
    //    a.Inclination = CurvatureConversions.ConvertToRadian(a.Inclination);
    //    a.Azimuth = CurvatureConversions.ConvertToRadian(a.Azimuth);

    //    b.Inclination = CurvatureConversions.ConvertToRadian(b.Inclination);
    //    b.Azimuth = CurvatureConversions.ConvertToRadian(b.Azimuth);

    //    double variable_A = Math.Sin(b.Inclination);
    //    double variable_B = Math.Cos(b.Azimuth);
    //    double north = MD * variable_A * variable_B;
    //    north = Math.Round(north, 2);
    //    results.North = north;
    //    double variable_D = Math.Sin(b.Azimuth);
    //    double east = MD * variable_A * variable_D;
    //    east = Math.Round(east, 2);
    //    results.East = east;
    //    double variable_E = Math.Cos(b.Inclination);
    //    double TVD = MD * variable_E;
    //    TVD = Math.Round(TVD, 2);
    //    results.TVD = TVD;
    //    return results;

    //}

    //public static BalancedTangentialResults BalancedTangential(SurveyBuilder a, SurveyBuilder b)
    //{
    //    BalancedTangentialResults results = new BalancedTangentialResults();

    //    double MD = b.Depth - a.Depth;
    //    a.Inclination = CurvatureConversions.ConvertToRadian(a.Inclination);
    //    a.Azimuth = CurvatureConversions.ConvertToRadian(a.Azimuth);
    //    b.Inclination = CurvatureConversions.ConvertToRadian(b.Inclination);
    //    b.Azimuth = CurvatureConversions.ConvertToRadian(b.Azimuth);

    //    double variable_A = Math.Sin(a.Inclination);
    //    double variable_B = Math.Cos(a.Azimuth);
    //    double variable_C = Math.Sin(b.Inclination);
    //    double variable_D = Math.Cos(b.Azimuth);
    //    double variable_E = variable_A * variable_B;
    //    double variable_F = variable_C * variable_D;
    //    double North = variable_E + variable_F;
    //    North = MD / 2 * North;
    //    North = Math.Round(North, 2);
    //    results.North = North;

    //    double variable_G = Math.Sin(a.Azimuth);
    //    double variable_H = Math.Sin(b.Azimuth);
    //    double variable_I = variable_A * variable_G;
    //    double variable_J = variable_C * variable_H;
    //    double East = variable_I + variable_J;

    //    East = MD / 2 * East;
    //    East = Math.Round(East, 2);
    //    results.East = East;

    //    double variable_K = Math.Cos(a.Inclination);
    //    double variable_L = Math.Cos(b.Inclination);
    //    double TVD = variable_K + variable_L;
    //    TVD = MD / 2 * TVD;
    //    TVD = Math.Round(TVD, 2); 
    //    results.TVD = TVD;

    //    return results;

        
    //}


    //Will find the 'Course length' MD2 - MD1 -measurement depth
    #endregion

}