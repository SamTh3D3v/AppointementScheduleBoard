using System.Collections.Generic;
using DataLayer.Model;

namespace DataLayer.DataService
{
    public interface IDataService
    {
        List<Stall> GetStallsCollection();
        ServerSettings GetServerSettings();
        LocalSettings GetLocalSettings();

    }
}
