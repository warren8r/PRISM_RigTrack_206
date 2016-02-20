using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for DatesbetweenDatatable
/// </summary>
public class DatesbetweenDatatable
{
	public DatesbetweenDatatable()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable getdatatable(DateTime startdate, DateTime stopdate, int interval)
    {
        bool boolDay = true;
        DataTable dtIntervals = new DataTable();
        DataColumn dcDate = new DataColumn();
        dcDate.ColumnName = "Interval";
        dtIntervals.Columns.Add(dcDate);
        DateTime timeinterval = startdate;
        int intervalValue = interval;
        while (boolDay)
        {
            timeinterval = timeinterval.AddMinutes(intervalValue);
            DataRow row = dtIntervals.NewRow();
            row[dcDate] = getdatetimeformat(timeinterval.ToString());
            dtIntervals.Rows.Add(row);
            if (stopdate == timeinterval)
            {

                boolDay = false;
            }
        }
        return dtIntervals;
    }
    public static string getdatetimeformat(string date)
    {
        date = Convert.ToDateTime(date).ToString("yyyy-MM-dd HH:mm:ss");
        return date;
    }
    public static string getdbFormateDateinput(DateTime dtInput)
    {
        IFormatProvider yyyymmddFormat = new System.Globalization.CultureInfo(String.Empty, false);
        return dtInput.ToString("MM/dd/yyyy", yyyymmddFormat);
    }
}