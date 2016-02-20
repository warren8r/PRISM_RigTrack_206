using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for JObCostingDTO
/// </summary>
/// 
namespace RigTrack.DatabaseTransferObjects
{
    public class JObCostingDTO
    {
        public int ID { get; set; }
        public int JOBID { get; set; }
        public int CompanyID { get; set; }
        public DateTime? Date { get; set; }
        public int CostGroupID { get; set; }
        public int ChargeBYID { get; set; }
        public double PriceperUnit { get; set; }
        public int NumberOfUnits { get; set; }
        public double Total { get; set; }

    }
}