using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TweetDTO
/// </summary>
/// namespace RigTrack.DatabaseTransferObjects
namespace RigTrack.DatabaseTransferObjects
{
    public class TweetDTO
    {
        public int ID { get; set; }
        public int Data { get; set; }
        public int UserID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool isSent { get; set; }
        public bool isActive { get; set; }


        // user stuff
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}