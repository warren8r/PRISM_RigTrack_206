using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace RigTrack.DatabaseTransferObjects
{
    /// <summary>
    /// Summary description for ToolInventoryDTO
    /// </summary>
    public class ToolInventoryDTO
    {
        public int id { get; set; }
        public int insert { get; set; }
        public int assetID { get; set; }
        public int warehouseID { get; set; }
        public int assetCategoryID { get; set; }
        public string assetName { get; set; }
        public string serialNumber { get; set; }
        public string description { get; set; }
        public int manufacturer { get; set; }
        public bool status { get; set; }
        public string odFrac { get; set; }
        public string idFrac { get; set; }
        public double length { get; set; }
        public string topConnection { get; set; }
        public string bottomConnection { get; set; }
        public string fishingNeck { get; set; }
        public string stabCenterPoint { get; set; }
        public string stabBladeOD { get; set; }
        public string weight { get; set; }
        public string ei { get; set; }
        public string sizeCategory { get; set; }
        public double cost { get; set; }

    }
}