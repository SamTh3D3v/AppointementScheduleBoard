using System;
using System.Collections.Generic;
using DataLayer.Model;

namespace DataLayer.DataService
{
    public interface IDataService
    {
        // Get all Branchs
        List<Branch> GetAllBranchs();

        // Get All Mechanics
        List<Technicien> GetAllTechnicians(int BRANCH_ID);

        List<Technicien> GetNotAssignedTechnicians(int BRANCH_ID);

        // Get stalls by branch ID
        List<Stall> GetBranchStalls(int BRANCH_ID);

        /// <summary>
        /// Add a new empty stall(no jobCards assign to it) stall
        /// </summary>
        /// <param name="stall">the stall object that need to be added</param>
        int AddStall(Stall stall);              
        /// <summary>
        /// Change the info related to a stall like its name ect
        /// </summary>
        /// <param name="stall">the updated stall object</param>
        int UpdateStall(Stall stall);
        /// <summary>
        /// Remove a spesific stall
        /// </summary>
        /// <param name="stallId">The Guid that identify the stall</param>
        bool RemoveStall(int stallId);

        bool AssignMechanicToStall(int STALL_ID, int MECHANIC_ID);

        bool ReleaseMechanicFromStall(int MECHANIC_ID);

        bool IsMechanicInStall(int MECHANIC_ID);

        /// <summary>
        /// Get the server related settings like the working hours ect...
        /// </summary>
        /// <returns>All server related settings are in ServerSettings class</returns>
        ServerSettings GetServerSettings();
        /// <summary>
        /// Get the local settings like the notification colors ect from the app xml file
        /// </summary>
        /// <returns></returns>
        LocalSettings GetLocalSettings();

        void UpdateLocalSettings(LocalSettings settings);
    }
}
