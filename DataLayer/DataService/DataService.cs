using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;

using Oracle.DataAccess.Client;

class ConnectionSample
{
    static void sample()
    {
        OracleConnection con = new OracleConnection();

        //using connection string attributes to connect to Oracle Database
        con.ConnectionString = "user id=PPDWHSIN;password=oracle4U;data source=" +
                                "(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)" +
                                 "(HOST=192.168.70.240)(PORT=1521))(CONNECT_DATA=" +
                                  "(SERVICE_NAME=orcl)))";

        OracleCommand query = new OracleCommand("select sysdate from dual", con);
        con.Open();
        OracleDataReader reader = query.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Console.WriteLine("{0}", reader.GetOracleDate(0));
            }
        }
        else
        {
            Console.WriteLine("No rows found.");
        }
        reader.Close();

        Console.WriteLine("Connected to Oracle" + con.ServerVersion);
        Console.ReadLine();
        // Close and Dispose OracleConnection object
        con.Close();
        con.Dispose();
        Console.WriteLine("Disconnected");
    }
}


namespace DataLayer.DataService
{
    public class DataService: IDataService
    {        
        public virtual List<Stall> GetStallsCollection()
        {
            //todo @Morad
            throw new NotImplementedException();
        }

        public virtual ServerSettings GetServerSettings()
        {
            //todo @Morad
            throw new NotImplementedException();
        }

        public virtual LocalSettings GetLocalSettings()
        {
            //todo @Morad
            throw new NotImplementedException();
        }      
    }
}
