using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Degatech.Common;
using Degatech.Utilities.SQL;
using Degatech.Utilities.IEnumerable;

namespace VFMClasses
{
    public class FuelOrderTaxes : BaseClass
    {
        private int id;
        #region Enums

        #endregion

        #region Properties
        public int Id { get; set; }
        public int FuelOrderID { get; set; }
        public string TaxDesc { get; set; } = string.Empty;
        public double TaxGal { get; set; }
        public double TaxAmt { get; set; }
        public bool OmitPPG { get; set; }
        public bool IsCustomizable { get; set; }
        public bool HasPricingData()
        {
            return (TaxGal > 0 || TaxAmt > 0);
        }

        #endregion

        #region Constructors

        public FuelOrderTaxes()
        {
        }

        public FuelOrderTaxes(int id)
        {
            Id = id;//this.id = id;
        }

        #endregion

        #region Public Methods
        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_FuelOrderTaxes", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static void DeleteAll(int FuelOrderID)
        {
            List<SqlParameter> Params = new List<SqlParameter>();
            SqlParameter Param = new SqlParameter("@FuelOrderID", SqlDbType.Int);
            Param.Value = FuelOrderID;
            Params.Add(Param);
            ExecutionHelper.ExecuteNonQuery("up_Delete_FuelOrderTaxes", Params);
        }

        public static FuelOrderTaxesCollection DeleteFuelOrderTax(int id)
        {
            FuelOrderTaxesCollection collection = new FuelOrderTaxesCollection();
            collection.Delete(id);
            return collection;
        }

        public static FuelOrderTaxesCollection LoadList(int FuelOrderID)
        {
            FuelOrderTaxesCollection collection = new FuelOrderTaxesCollection();
            collection.LoadAllTaxes(FuelOrderID);
            return collection;
        }

        #endregion
    }

    public class FuelOrderTaxesCollection : List<FuelOrderTaxes>
    {
        #region Members
        private FuelOrders _fuelOrder;
        #endregion

        #region Public Methods
        public void LoadAllTaxes(int FuelOrderID)
        {
            List<SqlParameter> Params = new List<SqlParameter>();
            SqlParameter Param = new SqlParameter("@FuelOrderID", SqlDbType.Int);
            Param.Value = FuelOrderID;
            Params.Add(Param);
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Load_FuelOrderTaxes", Params))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FuelOrderTaxes fuelOrderTax = new FuelOrderTaxes();
                    fuelOrderTax.SetProperties(reader);
                    Add(fuelOrderTax);
                }
            }
        }

        public void LoadClientTaxes(int clientID)
        {
            ArrayList defaultTaxes = ClientTaxes.GetList(clientID);
            if (defaultTaxes == null)
                return;
            foreach (string defaultTax in defaultTaxes)
            {
                var tax = GetTaxByDescription(defaultTax);
                if (tax == null)
                {
                    tax = new FuelOrderTaxes() { TaxDesc = defaultTax };
                    this.Add(tax);
                }
                tax.IsCustomizable = true;
            }
            this.SortByCustomizable();
        }

        public void UpdateAll(int FuelOrderID)
        {
            FuelOrderTaxes.DeleteAll(FuelOrderID);
            foreach (FuelOrderTaxes tax in this)
                tax.FuelOrderID = FuelOrderID;
            ExecutionHelper.BulkInsert(this.GetAllWithPricingData().AsDataTable(),
                "dbo.FuelOrderTaxes", ExecutionHelper.GetConnectionString());
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_FuelOrderTaxes", parameters);
        }

        public void SortByDescription()
        {
            Sort((x, y) => x.TaxDesc.CompareTo(y.TaxDesc));
        }

        public void SortByCustomizable()
        {
            Sort((x, y) => x.IsCustomizable.CompareTo(y.IsCustomizable));
        }

        public FuelOrderTaxes GetTaxByDescription(string description)
        {
            description = description.ToLower();
            return this.FirstOrDefault(tax => tax.TaxDesc.ToLower() == description);
        }

        public FuelOrderTaxesCollection GetAllWithPricingData()
        {
            FuelOrderTaxesCollection result = new FuelOrderTaxesCollection();
            foreach (FuelOrderTaxes tax in this)
            {
                if (tax.HasPricingData())
                    result.Add(tax);
            }
            return result;
        }

        //public void AddTaxesFromFuelReqFees(FuelOrders fuelOrder)
        //{
        //    _fuelOrder = fuelOrder;
        //    //This process adds all of the older, static fields as new "tax" objects
        //    if (GetTaxByDescription("State Excise Tax") == null)
        //        Insert(0, new FuelOrderTaxes() {FuelOrderID = fuelOrder.Id, TaxDesc = "State Excise Tax"});
        //    if (GetTaxByDescription("LUST") == null)
        //        Insert(0, new FuelOrderTaxes() { FuelOrderID = fuelOrder.Id, TaxDesc = "LUST"});
        //    if (GetTaxByDescription("Deficit Reduction") == null)
        //        Insert(0, new FuelOrderTaxes() { FuelOrderID = fuelOrder.Id, TaxDesc = "Deficit Reduction"});
        //    InsertReqFeeTaxes(_fuelOrder, "FedTax", "Fed Excise Tax");
        //    InsertReqFeeTaxes(_fuelOrder, "Vat", "VAT");
        //    InsertReqFeeTaxes(_fuelOrder, "Mot", "Mineral Oil Tax");
        //    InsertReqFeeTaxes(_fuelOrder, "Additive", "Additive (Prist)");
        //    InsertReqFeeTaxes(_fuelOrder, "StateOilSpillFee", "State Oil Spill Fee");
        //    InsertReqFeeTaxes(_fuelOrder, "OilSpillFee", "Fed Oil Spill Fee");
        //    InsertReqFeeTaxes(_fuelOrder, "ITPFee", "ITP Fee");
        //    InsertReqFeeTaxes(_fuelOrder, "FlowFee", "Flowage Fee");
        //    InsertReqFeeTaxes(_fuelOrder, "SalesTax", "Sales Tax");
        //    InsertReqFeeTaxes(_fuelOrder, "StateTax", "State Tax");
        //    InsertReqFeeTaxes(_fuelOrder, "MiscTax", "Misc. Tax");
        //}

        #endregion

        #region Private Methods

        //private void InsertReqFeeTaxes(FuelOrders fuelOrder, string propertyName, string taxName)
        //{
        //    FuelOrderTaxes tax = GetTaxByDescription(taxName);
        //    if (tax == null)
        //    {
        //        tax = new FuelOrderTaxes() {TaxDesc = taxName, FuelOrderID = fuelOrder.Id};
        //        Insert(0, tax);
        //        tax.IsCustomizable = false;
        //    }
        //    else
        //    {
        //        //Move the item to maintain the order of taxes we prefer
        //        this.RemoveAt(this.IndexOf(tax));
        //        this.Insert(0, tax);
        //    }
        //    if (tax.TaxAmt < 0.000001)
        //        tax.TaxAmt = (double) Data.Utilities.GetPropertyValue(reqFees, propertyName);
        //    if (tax.TaxGal < 0.000001 && fuelOrder.InvoicedVolume > 0)
        //        tax.TaxGal = tax.TaxAmt/double.Parse(fuelOrder.InvoicedVolume.ToString());
        //    Data.Utilities.SetPropertyValue(reqFees, propertyName, 0);
        //}

        #endregion
    }
}