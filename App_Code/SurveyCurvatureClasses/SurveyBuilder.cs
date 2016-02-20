using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SurveyBuilder
/// </summary>
public class SurveyBuilder
{
    public double Depth;
    public double Inclination;
    public double Azimuth;

    public double dogLegServerity;
    public double dogLegAngle;
    public double TVD;
    public double SubseasTVD;
    public double VerticalSection;
    public double North;
    public double East;
    public double CL;
    public double ClosureDistance;
    public double ClosureDirection;
    public double BR;
    public double WR;
    public double TFO;

	public SurveyBuilder()
	{
        Depth = 0.0;
        Inclination = 0.0;
        Azimuth = 0.0;
        dogLegServerity = 0.0;
        dogLegAngle = 0.0;
        TVD = 0.0;
        SubseasTVD = 0.0;
        North = 0.0;
        East = 0.0;
        CL = 0.0;
        ClosureDistance = 0.0;
        ClosureDirection = 0.0;
        BR = 0.0;
        WR = 0.0;
        TFO = 0.0;
        VerticalSection = 0.0;
		//
		// TODO: Add constructor logic here
		//
	}
}