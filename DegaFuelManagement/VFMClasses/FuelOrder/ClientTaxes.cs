using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using Degatech.Common;
using Degatech.Utilities.SQL;

namespace VFMClasses
{
    public class ClientTaxes : BaseClass
    {
        #region Members
        private static string _CacheKeyPrefix = "ClientDefaultTaxes_";
        #endregion

        #region Properties
        public int Id { get; set; }
        public int ClientID { get; set; }
        public string TaxDesc { get; set; } = string.Empty;
        #endregion

        #region Constructors
        public ClientTaxes()
        {
        }

        public ClientTaxes(int id)
        {
            Id = id;
        }
        #endregion

        #region Public Methods
        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_ClientTax", GetSQLParameters()))
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
            parameters.Add(new SqlParameter("@TaxDesc", taxDesc));
            ExecutionHelper.ExecuteNonQuery("up_Delete_ClientTax", parameters);
        }

        public static void UpdateList(ArrayList listOfTaxes, int clientID)
        {
            StoreInDatabase(listOfTaxes, clientID);
            StoreInCache(listOfTaxes, clientID);
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
                    result.Add(reader["TaxDesc"].ToString());
            }
            return result;
        }

        private static SqlDataReader GetReaderForLoading(int clientID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ClientID", clientID));
            return ExecutionHelper.ExecuteReader("up_Transactions_FuelOrderTaxesClientDefaultGet", parameters);
        }

        private static void StoreInDatabase(ArrayList listOfTaxes, int clientID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ClientID", clientID));
            parameters.Add(new SqlParameter("@CommaDelimtedListOfTaxes", Degatech.Utilities.Data.Utilities.ArrayListToCommaDelimited(listOfTaxes)));
            ExecutionHelper.ExecuteNonQuery("up_ClientTaxes_Update", parameters);
        }

        private static void StoreInCache(ArrayList listOfTaxes, int clientID)
        {
            Degatech.Utilities.Cache.CacheHelper.Add(listOfTaxes, GetCacheKey(clientID));
        }

        private static string GetCacheKey(int clientID)
        {
            return _CacheKeyPrefix + clientID;
        }
        #endregion
    }
}