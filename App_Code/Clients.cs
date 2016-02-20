using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Clients
/// </summary>
public class Clients
{
	public Clients()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string firstname = "", lastname = "", title = "", loginname = "", password = "", email = "", address = "", address2 = "", city = "", state = "", country = ""
        , zip = "", phone = "", category = "", loginstatus = "", cellno = "", activationcode = "", status="",clientcode="",company="";
    private DateTime lastupdateddate, createdate, expdate;
    private  bool cellmessagestatus = false, pointofcontact = false,locked=false;
    private int usertypeid = 0, activationexpiry = 0, attemts = 0, passwordexpiry = 0, userid=0;
	
    public bool CellMessageStatus
    {
        get
        {
            return cellmessagestatus;
        }
        set
        {
            cellmessagestatus = value;
        }
    }
    public bool PointofContact
    {
        get
        {
            return pointofcontact;
        }
        set
        {
            pointofcontact = value;
        }
    }
    public bool Locked
    {
        get
        {
            return locked;
        }
        set
        {
            locked = value;
        }
    }

    public string FirstName
    {
        get
        {
            return firstname;
        }
        set
        {
            firstname = value;
        }
    }
    public string LastName
    {
        get
        {
            return lastname;
        }
        set
        {
            lastname = value;
        }
    }
    public string Title
    {
        get
        {
            return title;
        }
        set
        {
            title = value;
        }
    }
    public string Company
    {
        get
        {
            return company;
        }
        set
        {
            company = value;
        }
    }
    public string LoginName
    {
        get
        {
            return loginname;
        }
        set
        {
            loginname = value;
        }
    }
    public string Password
    {
        get
        {
            return password;
        }
        set
        {
            password = value;
        }
    }
    public string Email
    {
        get
        {
            return email;
        }
        set
        {
            email = value;
        }
    }
    public string Address
    {
        get
        {
            return address;
        }
        set
        {
            address = value;
        }
    }
    public string Address2
    {
        get
        {
            return address2;
        }
        set
        {
            address2 = value;
        }
    }
    public string City
    {
        get
        {
            return city;
        }
        set
        {
            city = value;
        }
    }
    public string State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
        }
    }
    public string Country
    {
        get
        {
            return country;
        }
        set
        {
            country = value;
        }
    }
    public string Zip
    {
        get
        {
            return zip;
        }
        set
        {
            zip = value;
        }
    }
    public string Phone
    {
        get
        {
            return phone;
        }
        set
        {
            phone = value;
        }
    }
    public string LoginStatus
    {
        get
        {
            return loginstatus;
        }
        set
        {
            loginstatus = value;
        }
    }
    public string Cellno
    {
        get
        {
            return cellno;
        }
        set
        {
            cellno = value;
        }
    }
    public string Activationcode
    {
        get
        {
            return activationcode;
        }
        set
        {
            activationcode = value;
        }
    }
    public string Status
    {
        get
        {
            return status;
        }
        set
        {
            status = value;
        }
    }
    public string ClientCode
    {
        get
        {
            return clientcode;
        }
        set
        {
            clientcode = value;
        }
    }

    // Declare an Age property of type int:
    public int UserID
    {
        get
        {
            return userid;
        }
        set
        {
            userid = value;
        }
    }
    public int UserTypeID
    {
        get
        {
            return usertypeid;
        }
        set
        {
            usertypeid = value;
        }
    }
    public int ActivationExpiry
    {
        get
        {
            return activationexpiry;
        }
        set
        {
            activationexpiry = value;
        }
    }
    public int Attempts
    {
        get
        {
            return attemts;
        }
        set
        {
            attemts = value;
        }
    }
    public int PasswordExpiry
    {
        get
        {
            return passwordexpiry;
        }
        set
        {
            passwordexpiry = value;
        }
    }
    public DateTime LastUpdatedDate
    {
        get
        {
            return lastupdateddate;
        }
        set
        {
            lastupdateddate = value;
        }
    }
    public DateTime ExpiryDate
    {
        get
        {
            return expdate;
        }
        set
        {
            expdate = value;
        }
    }
   
    public DateTime CreateDate
    {
        get
        {
            return createdate;
        }
        set
        {
            createdate = value;
        }
    }
}