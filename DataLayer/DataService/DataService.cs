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
        /// <summary>
        /// Get all the stalls with their job cards
        /// </summary>
        /// <returns>A collection of stall object</returns>      
        public virtual List<Stall> GetStallsCollection()
        {
            //todo @Morad
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get the server related settings like the working hours ect...
        /// </summary>
        /// <returns>All server related settings are in ServerSettings class</returns>
        public virtual ServerSettings GetServerSettings()
        {
            //todo @Morad
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get the local settings like the notification colors ect from the app xml file
        /// </summary>
        /// <returns></returns>
        public virtual LocalSettings GetLocalSettings()
        {
            //todo @Oussama
            throw new NotImplementedException();
        }
        /// <summary>
        /// Add a new empty (no jobCards assign to it) stall
        /// </summary>
        /// <param name="stall">the stall object that need to be added</param>
        public virtual void AddStall(Stall stall)
        {
            //todo @Morad
            throw new NotImplementedException();
        }
        /// <summary>
        /// Remove a spesific stall
        /// </summary>
        /// <param name="stallId">The Guid that identify the stall</param>
        public virtual void RemoveStall(Guid stallId)
        {
            //todo @Morad
            throw new NotImplementedException();
        }
        /// <summary>
        /// Change the info related to a stall like its name ect
        /// </summary>
        /// <param name="stall">the updated stall object</param>
        public virtual void UpdateStall(Stall stall)
        {
            //todo @Morad
            throw new NotImplementedException();
        }
        /// <summary>
        /// Affect a spesific jobCard to a stall
        /// </summary>
        /// <param name="stallId">the Guid that identify the Stall</param>
        /// <param name="jobCardId"> the Guid that identify the JobCard</param>
        public virtual void AssignJobTaskToStall(Guid stallId, Guid jobCardId)
        {
            //todo @Morad
            throw new NotImplementedException();
        }
        /// <summary>
        /// Remove a spesific JobCard from a stall (UnAssign)
        /// </summary>
        /// <param name="stallId"> the Guid that identify the stall</param>
        /// <param name="jobTaskId">the Guid that identify the JobCard</param>
        public virtual void RemoveJobTaskFromStall(Guid stallId, Guid jobTaskId)
        {
            //todo @Morad
            throw new NotImplementedException();
        }
        /// <summary>
        /// get all jobcards those are assigned to a spesific Stall
        /// </summary>
        /// <param name="stallId">the guid that identify the stall</param>
        /// <returns></returns>
        public virtual List<JobTask> GetStallAssignedJobTasks(Guid stallId)
        {
            //todo @Morad
            throw new NotImplementedException();
        }
         /// <summary>
        /// get all the jobcards those are planned in a spesific date
        /// </summary>
        /// <param name="date">dd/mm/yy that we need its jobcards </param>
        /// <returns>the list of the JobCards those are planned in this date</returns>
        public List<JobTask> GetJobTasks(DateTime date)
        {
            //todo @Morad
            throw new NotImplementedException();
        }
    }
}
