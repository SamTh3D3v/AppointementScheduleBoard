using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;

using Oracle.DataAccess.Client;

namespace DataLayer.DataService
{
    public class DataService: IDataService
    {
        public virtual List<Branch> GetAllBranchs()
        {
            //todo mourad
            throw new NotImplementedException();
        }

        public virtual List<Technicien> GetAllTechnicians(int BRANCH_ID)
        {
            //todo mourad
            throw new NotImplementedException();
        }

        public virtual List<Technicien> GetNotAssignedTechnicians(int BRANCH_ID)
        {
            //todo mourad
            throw new NotImplementedException();
        }

        public virtual List<Stall> GetBranchStalls(int BRANCH_ID)
        {
            //todo mourad
            throw new NotImplementedException();
        }

        public virtual int AddStall(Stall stall)
        {
            //todo mourad
            throw new NotImplementedException();
        }

        public virtual int UpdateStall(Stall stall)
        {
            //todo mourad
            throw new NotImplementedException();
        }

        public virtual bool RemoveStall(int stallId)
        {
            throw new NotImplementedException();
        }

        public virtual bool AssignMechanicToStall(int STALL_ID, int MECHANIC_ID)
        {
            //todo mourad
            throw new NotImplementedException();
        }

        public virtual bool ReleaseMechanicFromStall(int MECHANIC_ID)
        {
            //todo mourad
            throw new NotImplementedException();
        }

        public virtual bool IsMechanicInStall(int MECHANIC_ID)
        {
            //todo mourad
            throw new NotImplementedException();
        }

        public virtual ServerSettings GetServerSettings()
        {
            //todo mourad
            throw new NotImplementedException();
        }

        public virtual LocalSettings GetLocalSettings()
        {
            //todo Oussama
            throw new NotImplementedException();
        }

        public virtual void UpdateLocalSettings(LocalSettings settings)
        {
            throw new NotImplementedException();
        }

        public virtual WorkingHoursSettings GetWorkingHoursSettings()
        {            
            return new WorkingHoursSettings()
            {
                StartHour = TimeSpan.Parse(ConfigurationManager.AppSettings["StartHour"]),
                EndHour = TimeSpan.Parse(ConfigurationManager.AppSettings["EndHour"]),                 
            };
        }
     

        public virtual void UpdateWorkingHoursSettings(WorkingHoursSettings settings)
        {
            ConfigurationManager.AppSettings["StartHour"] = settings.StartHour.ToString();
            ConfigurationManager.AppSettings["EndHour"] = settings.EndHour.ToString();
        }
    }
}
