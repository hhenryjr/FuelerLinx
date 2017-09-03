using System.Collections.Generic;
using System.Data.SqlClient;

namespace VFMClasses.FuelOrder
{
    public class FuelOrderSupplierPricings : SupplierFuelsPrices
    {
        #region Properties
        public int FuelOrderId { get; set; }
        #endregion

        #region Public Methods

        #endregion
    }

    #region Collection
    public class FuelOrderSupplierPricingsCollection : List<FuelOrderSupplierPricings>
    {
        #region Public Methods
        public void LoadByFuelOrderId(int fuelOrderId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fuelOrderId", fuelOrderId));
            using (
                SqlDataReader reader =
                    Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader(
                        "up_Select_FuelOrderSupplierPricingsByFuelOrderId", parameters))
            {
                LoadFromReader(reader);
            }
        }
        #endregion

        #region Private Methods
        private void LoadFromReader(SqlDataReader reader)
        {
            while (reader.Read())
            {
                FuelOrderSupplierPricings pricing = new FuelOrderSupplierPricings();
                pricing.SetProperties(reader);
                Add(pricing);
            }
        }
        #endregion
    }
    #endregion
}
