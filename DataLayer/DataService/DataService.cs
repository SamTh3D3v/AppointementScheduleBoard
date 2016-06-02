using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;

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
