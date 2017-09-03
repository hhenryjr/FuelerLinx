using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Degatech.Utilities.SQL;
using Degatech.Common;

namespace VFMClasses
{
    public class ClientFees : BaseClass
    {
        #region Members
        private static string _CacheKeyPrefix = "ClientDefaultFees_";
        #endregion

        #region Properties
        public int Id { get; set; }
        public int ClientID { get; set; }
        public string FeeDesc { get; set; } = string.Empty;
        #endregion

        #region Constructors
        public ClientFees()
        {
        }

        public ClientFees(int id)
        {
            Id = id;
        }
        #endregion

        #region Public Methods
        public static void Update(ArrayList listOfFees, int clientID)
        {
            StoreInDatabase(listOfFees, clientID);
            StoreInCache(listOfFees, clientID);
        }

        public static ArrayList GetList(int clientID)
        {
            ArrayList result = LoadFromCache(clientID);
            if (result != null)
                return result;
            result = LoadFromDatabase(clientID);
            StoreInCache(result, clientID);
            return result;
        }

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_ClientFee", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }

        public static void Delete(int clientID, string taxDesc)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ClientID", clientID));
            parameters.Add(new SqlParameter("@FeeDesc", taxDesc));
            ExecutionHelper.ExecuteNonQuery("up_Delete_ClientFee", parameters);
        }
        #endregion

        #region Private Methods
        private static ArrayList LoadFromCache(int clientID)
        {
            ArrayList result;
            Degatech.Utilities.Cache.CacheHelper.Get(GetCacheKey(clientID), out result);
            return result;
        }

        private static ArrayList LoadFromDatabase(int clientID)
        {
            ArrayList result = new ArrayList();
            using (SqlDataReader reader = GetReaderForLoading(clientID))
            {
                if (reader == null)
                    return result;
                while (reader.Read())
                    result.Add(reader["FeeDesc"].ToString());
            }
            return result;
        }

        private static SqlDataReader GetReaderForLoading(int clientID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ClientID", clientID));
            return ExecutionHelper.ExecuteReader("up_Transactions_FuelOrderFeesClientDefaultGet", parameters);
        }

        private static void StoreInDatabase(ArrayList listOfFees, int clientID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ClientID", clientID));
            parameters.Add(new SqlParameter("@CommaDelimtedListOfFees", Degatech.Utilities.Data.Utilities.ArrayListToCommaDelimited(listOfFees)));
            ExecutionHelper.ExecuteNonQuery("up_ClientFees_Update", parameters);
        }

        private static void StoreInCache(ArrayList listOfFees, int companyID)
        {
            Degatech.Utilities.Cache.CacheHelper.Add(listOfFees, GetCacheKey(companyID));
        }

        private static string GetCacheKey(int clientID)
        {
            return _CacheKeyPrefix + clientID;
        }
        #endregion
    }
}
