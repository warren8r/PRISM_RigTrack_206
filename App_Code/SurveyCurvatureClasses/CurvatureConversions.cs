using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CurvatureConversions
/// </summary>
public static class CurvatureConversions
{
	

    public static double ConvertToRadian(double d)
    {
        double returnValue = d / 180 * Math.PI;
        return returnValue;
    }
    public static double ConvertToDegree(double d)
    {
        double returnValue = d * 180 / Math.PI;
        return returnValue;
    }
}