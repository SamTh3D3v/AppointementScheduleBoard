using System;
using System.Collections.Generic;
using DataLayer.Model;

namespace DataLayer.DataService
{
    public interface IDataService
    {
        /// <summary>
        /// Get all the stalls with their job cards
        /// </summary>
        /// <returns>A collection of stall object</returns>  
        List<Stall> GetStallsCollection();
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
        /// <summary>
        /// Add a new empty stall(no jobCards assign to it) stall
        /// </summary>
        /// <param name="stall">the stall object that need to be added</param>
        void AddStall(Stall stall);
        /// <summary>
        /// Remove a spesific stall
        /// </summary>
        /// <param name="stallId">The Guid that identify the stall</param>
        void RemoveStall(Guid stallId);
        /// <summary>
        /// Change the info related to a stall like its name ect
        /// </summary>
        /// <param name="stall">the updated stall object</param>
        void UpdateStall(Stall stall);
        /// <summary>
        /// Affect a spesific jobCard to a stall
        /// </summary>
        /// <param name="stallId">the Guid that identify the Stall</param>
        /// <param name="jobCardId"> the Guid that identify the JobCard</param>
        void AssignJobTaskToStall(Guid stallId, Guid jobCardId);

        /// <summary>
        /// Remove a spesific JobCard from a stall (UnAssign)
        /// </summary>
        /// <param name="stallId"> the Guid that identify the stall</param>
        /// <param name="jobTaskId">the Guid that identify the JobCard</param>
        void RemoveJobTaskFromStall(Guid stallId, Guid jobTaskId);
        /// <summary>
        /// get all jobcards those are assigned to a spesific Stall
        /// </summary>
        /// <param name="stallId">the guid that identify the stall</param>
        /// <returns></returns>
        List<JobTask> GetStallAssignedJobTasks(Guid stallId);

        /// <summary>
        /// get all the jobcards those are planned in a spesific date
        /// </summary>
        /// <param name="date">dd/mm/yy that we need its jobcards </param>
        /// <returns>the list of the JobCards those are planned in this date</returns>
        List<JobTask> GetJobTasks(DateTime date);


    }
}
