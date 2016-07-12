﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess;
using Oracle.DataAccess.Client;
using DataLayer.Model;
using System.Configuration;
using DataLayer.Exceptions;

namespace DataLayer.DataService
{
    public class XxtaDataService : IDataService , IDisposable
    {
        OracleConnection _connection ;
        public XxtaDataService()
        {
            _connection = new OracleConnection();
            _connection.ConnectionString = "user id=itcomp;password=itcomp;data source=" +
                                "(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)" +
                                 "(HOST=10.254.0.170)(PORT=1551))(CONNECT_DATA=" +
                                  "(SERVICE_NAME=DEV)))";
            _connection.Open();
        }
        public void OpenConnection()
        {
            //if (_connection.State != System.Data.ConnectionState.Open)
            //    _connection.Open();  
        }
        public void CloseConnection()
        {
            //if (_connection.State == System.Data.ConnectionState.Open)
            //    _connection.Close();
        }
        public List<Branch> GetAllBranchs()
        {
            List<Branch> branchs = new List<Branch>();
            OracleCommand branchsQuery = new OracleCommand("SELECT BRANCH_ID , SUC_NAME " 
                                                         + "FROM APPS.XXTA_BRANCH_SIT ", _connection);
            //_connection.Open();
            OracleDataReader BranchsReader = branchsQuery.ExecuteReader();
            if (BranchsReader.HasRows)
            {
                while (BranchsReader.Read())
                {
                    Branch branch = new Branch();
                    branch.Id = int.Parse(BranchsReader.GetValue(0).ToString());
                    branch.Name = BranchsReader.GetString(1);
                    branchs.Add(branch);
                }
            }

            BranchsReader.Close();
            //_connection.Close();
            return branchs;
        }

        public List<Technicien> GetAllTechnicians(int BRANCH_ID)
        {
            List<Technicien> mechanics = new List<Technicien>();
            OracleCommand mechanicsQuery = new OracleCommand("SELECT RESOURCE_ID , MECHANIC_NAME "
                                                           + "FROM APPS.XXDMS_JA_CLK_RESOURCES_V "
                                                           + "WHERE BRANCH_CODE = :BRANCH_ID ", _connection);
            mechanicsQuery.Parameters.Add("BRANCH_ID", OracleDbType.Varchar2);
            mechanicsQuery.Parameters["BRANCH_ID"].Value = BRANCH_ID.ToString();
            //_connection.Open();
            OracleDataReader mechanicsReader = mechanicsQuery.ExecuteReader();
            if (mechanicsReader.HasRows)
            {
                while (mechanicsReader.Read())
                {
                    Technicien mechanic = new Technicien();
                    mechanic.Id = (int)((decimal)mechanicsReader.GetValue(0));
                    mechanic.Name = mechanicsReader.GetString(1);
                    mechanics.Add(mechanic);
                }
            }
            mechanicsReader.Close();
            //_connection.Close();
            return mechanics;
        }

        public List<Technicien> GetNotAssignedTechnicians(int BRANCH_ID)
        {
            List<Technicien> notAssignedMechanics = new List<Technicien>();

            foreach (Technicien mechanic in GetAllTechnicians(BRANCH_ID))
            {
                if (!IsMechanicInStall(mechanic.Id))
                    notAssignedMechanics.Add(mechanic);
            }
            return notAssignedMechanics;
        }
        public List<Stall> GetBranchStalls(int BRANCH_ID)
        {
            List<Stall> OrganisationStalls = new List<Stall>();

            OracleCommand query = new OracleCommand("SELECT STALL_ID , BRANCH_ID , STALL_NAME , STALL_DESCRIPTION , IS_ACTIVE " 
                                                   +"FROM  STALL "
                                                   +"WHERE BRANCH_ID = :BRANCH_ID AND IS_ACTIVE = 'Y' ", _connection);
            query.Parameters.Add("BRANCH_ID", OracleDbType.Varchar2);
            query.Parameters["BRANCH_ID"].Value = BRANCH_ID.ToString();

            OracleDataReader stallReader = query.ExecuteReader();
            if (stallReader.HasRows)
            {
                while (stallReader.Read())
                {
                    Stall currentStall = new Stall();
                    currentStall.Id = Int32.Parse(stallReader.GetOracleValue(0).ToString());
                    currentStall.StallName = stallReader.GetOracleString(2).ToString();

                    currentStall.Techniciens = new System.Collections.ObjectModel.ObservableCollection<Technicien>();
                    currentStall.JobTasksCollection = new System.Collections.ObjectModel.ObservableCollection<ITimeLineJobTask>();
                    //fill current stall with techniciens
                    OracleCommand techniciensQuery = new OracleCommand(@"SELECT STALL.STALL_ID , TECH.TECHNICIEN_ID , REC.MECHANIC_NAME
                                                                     FROM STALL 
                                                                     INNER JOIN STALL_TECHNICIENS TECH
                                                                     ON STALL.STALL_ID = TECH.STALL_ID 
                                                                     INNER JOIN APPS.XXDMS_JA_CLK_RESOURCES_V REC
                                                                     ON TECH.TECHNICIEN_ID = REC.RESOURCE_ID
                                                                     WHERE STALL.STALL_ID = :STALL_ID ", _connection);
                    techniciensQuery.Parameters.Add("STALL_ID", OracleDbType.Decimal);
                    techniciensQuery.Parameters["STALL_ID"].Value = (Decimal)currentStall.Id;
                    OracleDataReader techniciensReader = techniciensQuery.ExecuteReader();
                    if (techniciensReader.HasRows)
                    {
                        while (techniciensReader.Read())
                        {
                            Technicien technicien = new Technicien();
                            technicien.Id = (Int32)(decimal)techniciensReader.GetValue(1);
                            technicien.Name = techniciensReader.GetString(2);
                            currentStall.Techniciens.Add(technicien);

                            // Fill current stall job cards with current technicien job cards 
                            OracleCommand jobTasksQuery = new OracleCommand("     SELECT  ALLOC.ALLOCATION_ID , " 
                                                                          + "             TASK.TASK_ID, "
                                                                          + "             SR.INCIDENT_NUMBER AS JOBCARD_NUMBER, "
                                                                          + "             AB.BOOKING_NUMBER, "
                                                                          + "             INCTYPE.NAME AS INCIDENTTYPE, "
                                                                          + "             SR.INC_RESPONDED_BY_DATE AS RECEPTIONTIME, "
                                                                          + "             STATUS.NAME AS STATUS, "
                                                                          + "             SR.OBLIGATION_DATE AS ESTIMATED_STARTTIME, "
                                                                          + "             SR.START_DATE_ACTIVE AS STARTTIME, "
                                                                          + "             SR.EXPECTED_RESOLUTION_DATE AS PDT, "
                                                                          + "             SR.INCIDENT_RESOLVED_DATE AS ENDTIME "
                                                                          + "      FROM XXTA.XXDMS_JOB_ALLCTN_DTLS_ALL ALLOC "
                                                                          + "      INNER JOIN CS.CS_INCIDENTS_ALL_B SR "
                                                                          + "      ON ALLOC.INCIDENT_ID = SR.INCIDENT_ID "
                                                                          + "      INNER JOIN XXTA.XXDMS_AB_HEADERS AB "
                                                                          + "      ON SR.INCIDENT_ID = AB.INCIDENT_ID "
                                                                          + "      INNER JOIN XXTA.XXDMS_ALLCTN_TASK_DTLS TASK "
                                                                          + "      ON ALLOC.ALLOCATION_ID = TASK.ALLOCATION_ID "
                                                                          + "      INNER JOIN CS.CS_INCIDENT_STATUSES_TL STATUS "
                                                                          + "      ON SR.INCIDENT_STATUS_ID = STATUS.INCIDENT_STATUS_ID  AND STATUS.LANGUAGE = 'F' "
                                                                          + "      INNER JOIN CS.CS_INCIDENT_TYPES_TL INCTYPE "
                                                                          + "      ON SR.INCIDENT_TYPE_ID = INCTYPE.INCIDENT_TYPE_ID  AND INCTYPE.LANGUAGE = 'F' "
                                                                          + "      INNER JOIN CS.CS_INCIDENT_SEVERITIES_TL SEVERITY "
                                                                          + "      ON SR.INCIDENT_SEVERITY_ID = SEVERITY.INCIDENT_SEVERITY_ID  AND SEVERITY.LANGUAGE = 'F' "
                                                                          + "      WHERE ALLOC.MECHANIC_ID = :MECHANIC_ID AND TRUNC(ALLOC.ALLOCATION_DT) = TRUNC(SYSDATE) ", _connection);
                            jobTasksQuery.Parameters.Add("MECHANIC_ID", OracleDbType.Int32);
                            jobTasksQuery.Parameters["MECHANIC_ID"].Value = technicien.Id;

                            
                            OracleDataReader jobTasksReader = jobTasksQuery.ExecuteReader();
                            if (jobTasksReader.HasRows)
                            {
                                while (jobTasksReader.Read())
                                {
                                    JobTask jobTask = new JobTask();
                                    jobTask.Id = (Int32)jobTasksReader.GetValue(0);
                                    jobTask.JobType = jobTasksReader.GetString(4);
                                    jobTask.ReceptionTime = jobTasksReader.GetDateTime(5);
                                    jobTask.Status = jobTasksReader.GetString(6);
                                    jobTask.PDT = jobTasksReader.GetDateTime(9);
                                    jobTask.PlannedStartTime = jobTasksReader.GetDateTime(7);
                                    jobTask.ActualStartTime = jobTasksReader.GetDateTime(8);
                                    jobTask.EndTime = jobTasksReader.GetDateTime(10);
                                    if (currentStall.JobTasksCollection.Count(c => c.Id == jobTask.Id) == 0)
                                    {
                                        currentStall.JobTasksCollection.Add(jobTask);
                                    }
                                }
                            }
                            jobTasksReader.Close();
                        }
                    }


                    techniciensReader.Close();

                    OrganisationStalls.Add(currentStall);
                }

            }
            stallReader.Close();

            return OrganisationStalls;
        }

        public int AddStall(Stall newStall)
        {
            OracleCommand insertCommand = new OracleCommand(@"INSERT INTO STALL(STALL_ID , BRANCH_ID , STALL_NAME , STALL_DESCRIPTION , IS_ACTIVE)
                                                          VALUES(STALL_SEQ.NEXTVAL , :BRANCH_ID , :STALL_NAME , :STALL_DESCRIPTION , :IS_ACTIVE)", _connection);

            insertCommand.Parameters.Add("BRANCH_ID", OracleDbType.Varchar2);
            insertCommand.Parameters["BRANCH_ID"].Value = newStall.BranchId.ToString();
            insertCommand.Parameters.Add("STALL_NAME", OracleDbType.Varchar2);
            insertCommand.Parameters["STALL_NAME"].Value = newStall.StallName;
            insertCommand.Parameters.Add("STALL_DESCRIPTION", OracleDbType.Varchar2);
            insertCommand.Parameters["STALL_DESCRIPTION"].Value = newStall.StallDescription;
            insertCommand.Parameters.Add("IS_ACTIVE", OracleDbType.Varchar2);
            insertCommand.Parameters["IS_ACTIVE"].Value = newStall.IsActive;

            insertCommand.ExecuteNonQuery();

            return newStall.Id;
        }

        public int UpdateStall(Stall UpdatedStall)
        {
            OracleCommand updateCommand = new OracleCommand(@"UPDATE STALL 
                                                              SET BRANCH_ID = :BRANCH_ID , 
                                                              STALL_NAME = :STALL_NAME , 
                                                              STALL_DESCRIPTION = :STALL_DESCRIPTION , 
                                                              IS_ACTIVE = :IS_ACTIVE
                                                              WHERE STALL_ID = :STALL_ID ", _connection);
            updateCommand.Parameters.Add("STALL_ID", OracleDbType.Int32);
            updateCommand.Parameters["STALL_ID"].Value = UpdatedStall.Id;
            updateCommand.Parameters.Add("BRANCH_ID", OracleDbType.Varchar2);
            updateCommand.Parameters["BRANCH_ID"].Value = UpdatedStall.BranchId.ToString();
            updateCommand.Parameters.Add("STALL_NAME", OracleDbType.Varchar2);
            updateCommand.Parameters["STALL_NAME"].Value = UpdatedStall.StallName;
            updateCommand.Parameters.Add("STALL_DESCRIPTION", OracleDbType.Varchar2);
            updateCommand.Parameters["STALL_DESCRIPTION"].Value = UpdatedStall.StallDescription;
            updateCommand.Parameters.Add("IS_ACTIVE", OracleDbType.Varchar2);
            updateCommand.Parameters["IS_ACTIVE"].Value = UpdatedStall.IsActive;

            updateCommand.ExecuteNonQuery();

            return UpdatedStall.Id;
        }

        public bool RemoveStall(int stall_Id)
        {
            if (!isStallFree(stall_Id))
                throw new BusinessException("the stall is not free !");

            OracleCommand deleteCommand = new OracleCommand(@"DELETE STALL 
                                                              WHERE STALL_ID = :STALL_ID", _connection);
            deleteCommand.Parameters.Add("STALL_ID", OracleDbType.Int32);
            deleteCommand.Parameters["STALL_ID"].Value = stall_Id;
            
            deleteCommand.ExecuteNonQuery();
            return true;
        }

        public bool isStallFree(int stall_Id)
        {
            OracleCommand query = new OracleCommand(@"SELECT COUNT(*) 
                                                      FROM STALL_TECHNICIENS
                                                      WHERE STALL_ID = :STALL_ID ", _connection);
            query.Parameters.Add("STALL_ID", OracleDbType.Int32);
            query.Parameters["STALL_ID"].Value = stall_Id;

            OracleDataReader reader = query.ExecuteReader();
            reader.Read();
            if (int.Parse(reader.GetValue(0).ToString()) > 0)
                return false;
            else
                return true;

        }
        public bool AssignMechanicToStall(int STALL_ID, int MECHANIC_ID)
        {
            if (IsMechanicInStall(MECHANIC_ID))
            {
                throw new BusinessException("Already assigned !");
            }
            else
            {
                OracleCommand insertCommand = new OracleCommand(@"INSERT INTO STALL_TECHNICIENS
                                                                  VALUES(STALL_TECHNICIENS_SEQ.NEXTVAL , :STALL_ID , :MECHANIC_ID , SYSDATE) ", _connection);
                insertCommand.Parameters.Add("STALL_ID", OracleDbType.Int32);
                insertCommand.Parameters["STALL_ID"].Value = STALL_ID;
                insertCommand.Parameters.Add("MECHANIC_ID", OracleDbType.Int32);
                insertCommand.Parameters["MECHANIC_ID"].Value = MECHANIC_ID;
                
                insertCommand.ExecuteNonQuery();
            }
            return true;
        }

        public bool ReleaseMechanicFromStall(int MECHANIC_ID)
        {
            if (!IsMechanicInStall(MECHANIC_ID))
            {
                throw new BusinessException("Not assigned !");
            }
            else
            {
                OracleCommand deleteCommand = new OracleCommand(@"DELETE FROM STALL_TECHNICIENS
                                                                  WHERE TECHNICIEN_ID = :MECHANIC_ID ", _connection);
                deleteCommand.Parameters.Add("MECHANIC_ID", OracleDbType.Int32);
                deleteCommand.Parameters["MECHANIC_ID"].Value = MECHANIC_ID;

                deleteCommand.ExecuteNonQuery();
            }
            return true;
        }

        public bool IsMechanicInStall(int MECHANIC_ID)
        {
            bool IsInStall = false;
            OracleCommand query = new OracleCommand(@"SELECT COUNT(*) AS COUNT
                                                      FROM STALL_TECHNICIENS
                                                      WHERE TECHNICIEN_ID = :MECHANIC_ID ", _connection);
            query.Parameters.Add("MECHANIC_ID", OracleDbType.Int32);
            query.Parameters["MECHANIC_ID"].Value = MECHANIC_ID;
            //OpenConnection();
            OracleDataReader reader = query.ExecuteReader();

            reader.Read();
            if (reader.GetInt32(0) > 0)
                IsInStall = true;

            reader.Close();
            //CloseConnection();
            return IsInStall;
        }

        public ServerSettings GetServerSettings()
        {
            OracleCommand query = new OracleCommand(@"SELECT SYSDATE FROM DUAL ", _connection);
            OracleDataReader reader = query.ExecuteReader();
            reader.Read();
            DateTime sysDate = reader.GetDateTime(0);
            reader.Close();
            //TODO : Mourad Search the params in EBS
            return new ServerSettings()
            {
                StartHour = new TimeSpan(1, 0, 0),
                EndHour = new TimeSpan(18, 0, 0),
                DatabaseCurrentDate = sysDate
            };
            
        }

        public LocalSettings GetLocalSettings()
        {
            return new LocalSettings()
            {
                IsClockFormat24 = bool.Parse(ConfigurationManager.AppSettings["IsClockFormat24"]),
                RefreshTimeInMinutes = Double.Parse(ConfigurationManager.AppSettings["RefreshTimeInMinutes"]),
                UnitSize = Double.Parse(ConfigurationManager.AppSettings["UnitSize"]),
                IsShipClientWaitingVisible = bool.Parse(ConfigurationManager.AppSettings["IsShipClientWaitingVisible"]),
                IsShipJobtypeVisible = bool.Parse(ConfigurationManager.AppSettings["IsShipJobtypeVisible"]),
                IsShipPdtVisible = bool.Parse(ConfigurationManager.AppSettings["IsShipPdtVisible"]),
                IsShipReceptionTimeVisible = bool.Parse(ConfigurationManager.AppSettings["IsShipReceptionTimeVisible"]),
                IsShipStatusVisible = bool.Parse(ConfigurationManager.AppSettings["IsShipStatusVisible"]),
                IsPlanActualHeaderVisible = bool.Parse(ConfigurationManager.AppSettings["IsPlanActualHeaderVisible"]),
                IsPlanActualMerged = bool.Parse(ConfigurationManager.AppSettings["IsPlanActualMerged"]),
                IsStallNamesVisible = bool.Parse(ConfigurationManager.AppSettings["IsStallNamesVisible"]),
                IsTechnicientsNamesVisible = bool.Parse(ConfigurationManager.AppSettings["IsTechnicientsNamesVisible"]),
                IsTimeHeaderVisible = bool.Parse(ConfigurationManager.AppSettings["IsTimeHeaderVisible"]),



                IrrLateJobVr = ConfigurationManager.AppSettings["IrrLateJobVr"].ToString(),
                IrrLateJobBooked = ConfigurationManager.AppSettings["IrrLateJobBooked"].ToString(),
                IrrPlannedTimeExeeded = ConfigurationManager.AppSettings["IrrPlannedTimeExeeded"].ToString(),
                PdtExceededInProgress = ConfigurationManager.AppSettings["PdtExceededInProgress"].ToString(),
                PdtExceededWaittingForInvoice = ConfigurationManager.AppSettings["PdtExceededWaittingForInvoice"].ToString(),

                IrrLateJobVrBlink = bool.Parse(ConfigurationManager.AppSettings["IrrLateJobVrBlink"].ToString()),
                IrrLateJobBookedBlink = bool.Parse(ConfigurationManager.AppSettings["IrrLateJobBookedBlink"].ToString()),
                IrrPlannedTimeExeededBlink = bool.Parse(ConfigurationManager.AppSettings["IrrPlannedTimeExeededBlink"].ToString()),
                PdtExceededInProgressBlink = bool.Parse(ConfigurationManager.AppSettings["PdtExceededInProgressBlink"].ToString()),
                PdtExceededWaittingForInvoiceBlink = bool.Parse(ConfigurationManager.AppSettings["PdtExceededWaittingForInvoiceBlink"].ToString())

                //IsClockFormat24 = true,
                //RefreshTimeInMinutes = 0.1,
                //UnitSize = 100,
                //IsShipClientWaitingVisible = true,
                //IsShipJobtypeVisible = true,
                //IsShipPdtVisible = true,
                //IsShipReceptionTimeVisible = true,
                //IsShipStatusVisible = true,

                //IsPlanActualHeaderVisible = true,
                //IsPlanActualMerged = false,
                //IsStallNamesVisible = true,
                //IsTechnicientsNamesVisible = true,
                //IsTimeHeaderVisible = true
            };
        }

        public void UpdateLocalSettings(LocalSettings settings)
        {
            ConfigurationManager.AppSettings["IsClockFormat24"] = settings.IsClockFormat24.ToString();
            ConfigurationManager.AppSettings["RefreshTimeInMinutes"] = settings.RefreshTimeInMinutes.ToString();
            ConfigurationManager.AppSettings["UnitSize"] = settings.UnitSize.ToString();
            ConfigurationManager.AppSettings["IsShipClientWaitingVisible"] = settings.IsShipClientWaitingVisible.ToString();
            ConfigurationManager.AppSettings["IsShipJobtypeVisible"] = settings.IsShipJobtypeVisible.ToString();
            ConfigurationManager.AppSettings["IsShipPdtVisible"] = settings.IsShipPdtVisible.ToString();
            ConfigurationManager.AppSettings["IsShipReceptionTimeVisible"] =
                settings.IsShipReceptionTimeVisible.ToString();
            ConfigurationManager.AppSettings["IsShipStatusVisible"] = settings.IsShipStatusVisible.ToString();
            ConfigurationManager.AppSettings["IsPlanActualHeaderVisible"] =
                settings.IsPlanActualHeaderVisible.ToString();
            ConfigurationManager.AppSettings["IsPlanActualMerged"] = settings.IsPlanActualMerged.ToString();
            ConfigurationManager.AppSettings["IsStallNamesVisible"] = settings.IsStallNamesVisible.ToString();
            ConfigurationManager.AppSettings["IsTechnicientsNamesVisible"] =
                settings.IsTechnicientsNamesVisible.ToString();
            ConfigurationManager.AppSettings["IsTimeHeaderVisible"] = settings.IsTimeHeaderVisible.ToString();

            ConfigurationManager.AppSettings["IrrLateJobVr"] = settings.IrrLateJobVr.ToString();
            ConfigurationManager.AppSettings["IrrLateJobBooked"] = settings.IrrLateJobBooked.ToString();
            ConfigurationManager.AppSettings["IrrPlannedTimeExeeded"] = settings.IrrPlannedTimeExeeded.ToString();
            ConfigurationManager.AppSettings["PdtExceededInProgress"] = settings.PdtExceededInProgress.ToString();
            ConfigurationManager.AppSettings["PdtExceededWaittingForInvoice"] = settings.PdtExceededWaittingForInvoice.ToString();

            ConfigurationManager.AppSettings["IrrLateJobVrBlink"] = settings.IrrLateJobVrBlink.ToString();
            ConfigurationManager.AppSettings["IrrLateJobBookedBlink"] = settings.IrrLateJobBookedBlink.ToString();
            ConfigurationManager.AppSettings["IrrPlannedTimeExeededBlink"] = settings.IrrPlannedTimeExeededBlink.ToString();
            ConfigurationManager.AppSettings["PdtExceededInProgressBlink"] = settings.PdtExceededInProgressBlink.ToString();
            ConfigurationManager.AppSettings["PdtExceededWaittingForInvoiceBlink"] = settings.PdtExceededWaittingForInvoiceBlink.ToString();


        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
