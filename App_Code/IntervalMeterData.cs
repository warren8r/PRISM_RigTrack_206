using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for IntervalMeterData
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class IntervalMeterData : System.Web.Services.WebService {

    public IntervalMeterData () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    [WebMethod]

    public bool ValidateQuantity(string id, string value, object context)
    {
        return true;

        //Dictionary<string, object> values = context as Dictionary<string, object>;



        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"];

        //using (DbConnection connection = DbProviderFactories.GetFactory(connectionString.ProviderName).CreateConnection())
        //{

        //    connection.ConnectionString = connectionString.ConnectionString;



        //    using (DbCommand command = connection.CreateCommand())
        //    {

        //        command.CommandText = "SELECT UnitsInStock FROM Products WHERE ProductID like @ProductID";

        //        command.CommandType = CommandType.Text;

        //        DbParameter parameter = command.CreateParameter();

        //        parameter.ParameterName = "@ProductID";

        //        parameter.Value = Convert.ToInt32(values["ProductID"]);

        //        command.Parameters.Add(parameter);



        //        connection.Open();



        //        int unitsInStock = Convert.ToInt32(command.ExecuteScalar());



        //        if (unitsInStock >= Convert.ToInt32(value))
        //        {

        //            return true;

        //        }

        //        return false;

        //    }

        //}

    }
    
}
