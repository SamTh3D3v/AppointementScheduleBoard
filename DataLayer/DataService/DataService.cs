using System;
using System.Collections.Generic;
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
    }
}
