using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CompanyDTO
/// </summary>
namespace RigTrack.DatabaseTransferObjects
{
    public class CompanyDTO
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CompanyContactFirstName { get; set; }
        public string CompanyContactLastName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string City { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public string Zip { get; set; }
        public bool isAttachment { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifyDate { get; set; }
        public bool isActive { get; set; }
    }
}