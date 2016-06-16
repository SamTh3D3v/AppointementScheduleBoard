using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess;
using Oracle.DataAccess.Client;
using DataLayer.Model;

namespace DataLayer.DataService
{
    public class XxtaDataService 
    {
        OracleConnection _connection ;
        public XxtaDataService()
        {
            _connection = new OracleConnection();
            _connection.ConnectionString = "user id=itcomp;password=itcomp;data source=" +
                                "(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)" +
                                 "(HOST=10.254.0.170)(PORT=1551))(CONNECT_DATA=" +
                                  "(SERVICE_NAME=DEV)))";
        }

        public List<Stall> GetOrganisationStalls(int BRANCH_ID)
        {
            List<Stall> OrganisationStalls = new List<Stall>();

            OracleCommand query = new OracleCommand("SELECT STALL_ID , BRANCH_ID , STALL_NAME , STALL_DESCRIPTION , IS_ACTIVE FROM  STALL WHERE BRANCH_ID = @BRANCH_ID AND IS_ACTIVE = 'Y' ", _connection);
            query.Parameters.Add("@BRANCH_ID", OracleDbType.Varchar2);
            query.Parameters["@BRANCH_ID"].Value = BRANCH_ID.ToString();
            _connection.Open();
            OracleDataReader stallReader = query.ExecuteReader();
            if (stallReader.HasRows)
            {
                while (stallReader.Read())
                {
                    Stall currentStall = new Stall();
                    currentStall.Id = Int32.Parse(stallReader.GetOracleValue(0).ToString());
                    currentStall.StallName = stallReader.GetOracleString(2).ToString();

                    //fill current stall with techniciens
                    OracleCommand techniciensQuery = new OracleCommand("SELECT REC.RESOURCE_ID , REC.RESOURCE_NAME "
                                                                     + "FROM STALL_TECHNICIENS ACCTAB "
                                                                     + "INNER JOIN XXTA.XXTA_TECH_PRODS REC "
                                                                     + "ON ACCTAB.TECHNICIEN_ID = REC.RESOURCE_ID "
                                                                     + "WHERE ACCTAB.STALL_ID = @STALL_ID" , _connection);
                    techniciensQuery.Parameters.Add("@STALL_ID", OracleDbType.Int32);
                    techniciensQuery.Parameters["@STALL_ID"].Value = currentStall.Id;
                    OracleDataReader techniciensReader = techniciensQuery.ExecuteReader();
                    if (techniciensReader.HasRows)
                    {
                        while (techniciensReader.Read())
                        {
                            Technicien technicien = new Technicien();
                            technicien.Id = Int32.Parse(techniciensReader.GetOracleValue(0).ToString());
                            technicien.Name = techniciensReader.GetOracleString(1).ToString();
                            currentStall.Techniciens.Add(technicien);

                            // Fill current stall job cards with current technicien job cards 

                            // TODO : Mourad : Replace With Job Cards Query  
                            OracleCommand jobCardsQuery = new OracleCommand("", _connection);
                        }
                    }
                    techniciensReader.Close();
                }
            }
            stallReader.Close();
            _connection.Close();
            return OrganisationStalls;
        }
        public int AddNewStall(Stall newStall)
        {
            OracleCommand insertCommand = new OracleCommand("INSERT INTO STALL(STALL_ID , BRANCH_ID , STALL_NAME , STALL_DESCRIPTION , IS_ACTIVE) "
                                                          + "VALUES(@STALL_ID , @BRANCH_ID , @STALL_NAME , @STALL_DESCRIPTION , @IS_ACTIVE)", _connection);
            insertCommand.Parameters.Add("@STALL_ID", OracleDbType.Int32);
            insertCommand.Parameters["@STALL_ID"].Value = newStall.Id;
            insertCommand.Parameters.Add("@BRANCH_ID", OracleDbType.Varchar2);
            insertCommand.Parameters["@BRANCH_ID"].Value = newStall.BranchId;
            insertCommand.Parameters.Add("@STALL_NAME", OracleDbType.Varchar2);
            insertCommand.Parameters["@STALL_NAME"].Value = newStall.StallName;
            insertCommand.Parameters.Add("@STALL_DESCRIPTION", OracleDbType.Varchar2);
            insertCommand.Parameters["@STALL_DESCRIPTION"].Value = newStall.StallDescription;
            insertCommand.Parameters.Add("@IS_ACTIVE", OracleDbType.Varchar2);
            insertCommand.Parameters["@IS_ACTIVE"].Value = newStall.IsActive;
            _connection.Open();
            insertCommand.ExecuteNonQuery();
            _connection.Close();
            return newStall.Id;
        }
        public int UpdateExistingStall(Stall UpdatedStall)
        {
            OracleCommand updateCommand = new OracleCommand("UPDATE STALL SET BRANCH_ID = @BRANCH_ID , "
                                                            + "STALL_NAME = @STALL_NAME , "
                                                            + "STALL_DESCRIPTION = @STALL_DESCRIPTION , "
                                                            + "IS_ACTIVE = @IS_ACTIVE "
                                                            + "WHERE STALL_ID = @STALL_ID ", _connection);
            updateCommand.Parameters.Add("@STALL_ID", OracleDbType.Int32);
            updateCommand.Parameters["@STALL_ID"].Value = UpdatedStall.Id;
            updateCommand.Parameters.Add("@BRANCH_ID", OracleDbType.Varchar2);
            updateCommand.Parameters["@BRANCH_ID"].Value = UpdatedStall.BranchId;
            updateCommand.Parameters.Add("@STALL_NAME", OracleDbType.Varchar2);
            updateCommand.Parameters["@STALL_NAME"].Value = UpdatedStall.StallName;
            updateCommand.Parameters.Add("@STALL_DESCRIPTION", OracleDbType.Varchar2);
            updateCommand.Parameters["@STALL_DESCRIPTION"].Value = UpdatedStall.StallDescription;
            updateCommand.Parameters.Add("@IS_ACTIVE", OracleDbType.Varchar2);
            updateCommand.Parameters["@IS_ACTIVE"].Value = UpdatedStall.IsActive;

            _connection.Open();
            updateCommand.ExecuteNonQuery();
            _connection.Close();
            return UpdatedStall.Id;
        }
        public bool DeleteExistingStall(int stall_Id)
        {
            OracleCommand deleteCommand = new OracleCommand("DELETE STALL WHERE STALL_ID = @STALL_ID", _connection);
            deleteCommand.Parameters.Add("@STALL_ID", OracleDbType.Int32);
            deleteCommand.Parameters["@STALL_ID"].Value = stall_Id;
            _connection.Open();
            deleteCommand.ExecuteNonQuery();
            _connection.Close();
            return true;
        }
    }
}
