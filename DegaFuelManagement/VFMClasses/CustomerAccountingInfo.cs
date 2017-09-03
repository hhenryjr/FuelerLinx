using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Degatech.Common;
using Degatech.Utilities.SQL;
using System.Linq;

namespace VFMClasses
{
    #region CustomerDetail
    /// <summary>
    /// This object represents the properties and methods of a CustomerDetail.
    /// </summary>
    public class CustomerAccountingInfo : BaseClass
    {
        #region Properties
        public int AdminClientID { get; set; }
        public int CustClientID { get; set; }
        public DateTime StartDate { get; set; }
        public string AccountRep { get; set; } = String.Empty;
        public string AccountingCode { get; set; } = String.Empty;
        public bool IsFuelerLinxCustomer { get; set; }
        public string BillingRep { get; set; } = String.Empty;
        public string SchedulingSystem { get; set; } = String.Empty;
        public string CreditCardFee { get; set; } = String.Empty;
        public string PaymentTerms { get; set; } = String.Empty;
        public string BillToSetup { get; set; } = String.Empty;
        #endregion

        #region Constructors
        public CustomerAccountingInfo()
        {
        }

        //public CustomerAccountingInfo(int id)
        //{
        //    Id = id;
        //    Load();
        //}

        public CustomerAccountingInfo(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void LoadInfoByAdminAndCustClientID(int adminId, int custId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            parameters.Add(new SqlParameter("@CustClientID", custId));
            using (
                SqlDataReader reader =
                    ExecutionHelper.ExecuteReader("up_Select_CustomerAccountingInfoByAdminAndClientID", parameters))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                {
                    SetProperties(reader);
                }
            }
        }

        //public void Load()
        //{
        //    List<SqlParameter> parameters = new List<SqlParameter>();
        //    parameters.Add(new SqlParameter("@Id", Id));
        //    using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerDetail", parameters))
        //    {
        //        if (reader == null)
        //            return;
        //        if (reader.Read())
        //            SetProperties(reader);
        //    }
        //}

        protected void LoadFromReader(SqlDataReader reader)
        {
            SetProperties(reader);
        }

        //public void Delete()
        //{
        //    DeleteCustomerDetail(Id);
        //}

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_CustomerAccountingInfo", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        //public static CustomerAccountingInfo GetCustomerDetail(int id)
        //{
        //    return new CustomerAccountingInfo(id);
        //}

        public static CustomerAccountingInfoCollection LoadList()
        {
            CustomerAccountingInfoCollection collection = new CustomerAccountingInfoCollection();
            collection.LoadAll();
            return collection;
        }

        public static CustomerAccountingInfo LoadByAdminAndCustClientID(int adminId, int custId)
        {
            CustomerAccountingInfo info = new CustomerAccountingInfo();
            info.LoadInfoByAdminAndCustClientID(adminId, custId);
            return info;
        }

        public static CustomerAccountingInfoCollection DeleteCustomerDetail(int id)
        {
            CustomerAccountingInfoCollection collection = new CustomerAccountingInfoCollection();
            collection.DeleteCustomerDetail(id);
            return collection;
        }

        #endregion
    }

    #region Collection
    public class CustomerAccountingInfoCollection : List<CustomerAccountingInfo>
    {
        #region Public Methods
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerAccountingInfoAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerAccountingInfo detail = new CustomerAccountingInfo();
                    detail.SetProperties(reader);
                    Add(detail);
                }
            }
        }

        public void LoadByAdminAndCustClientID(int adminId, int custId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            parameters.Add(new SqlParameter("@CustClientID", custId));
            using (
                SqlDataReader reader =
                    ExecutionHelper.ExecuteReader("up_Select_CustomerAccountingInfoByAdminAndClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerAccountingInfo detail = new CustomerAccountingInfo();
                    detail.SetProperties(reader);
                    Add(detail);
                }
            }
        }

        public void DeleteCustomerDetail(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustClientID", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_CustomerDetail", parameters);
        }

        public CustomerAccountingInfo GetAdminClientID(int clientId)
        {
            return this.FirstOrDefault(detail => detail.AdminClientID == clientId);
        }

        public CustomerAccountingInfo GetCustClientID(int clientId)
        {
            return this.FirstOrDefault(detail => detail.CustClientID == clientId);
        }

        public CustomerAccountingInfo GetStartDate(DateTime date)
        {
            return this.FirstOrDefault(detail => detail.StartDate == date);
        }

        public CustomerAccountingInfo GetAccountRep(string rep)
        {
            return this.FirstOrDefault(detail => detail.AccountRep == rep);
        }

        public CustomerAccountingInfo GetAccountingCode(string code)
        {
            return this.FirstOrDefault(detail => detail.AccountingCode == code);
        }

        public CustomerAccountingInfo GetIsFuelerLinxCustomer(bool customer)
        {
            return this.FirstOrDefault(detail => detail.IsFuelerLinxCustomer == customer);
        }

        public CustomerAccountingInfo GetBillingRep(string rep)
        {
            return this.FirstOrDefault(detail => detail.BillingRep == rep);
        }

        public CustomerAccountingInfo GetSchedulingSystem(string system)
        {
            return this.FirstOrDefault(detail => detail.SchedulingSystem == system);
        }

        public CustomerAccountingInfo GetCreditCardFee(string fee)
        {
            return this.FirstOrDefault(detail => detail.CreditCardFee == fee);
        }

        public CustomerAccountingInfo GetPaymentTerms(string terms)
        {
            return this.FirstOrDefault(detail => detail.PaymentTerms == terms);
        }

        public CustomerAccountingInfo GetBillToSetup(string setup)
        {
            return this.FirstOrDefault(detail => detail.BillToSetup == setup);
        }
        #endregion
    }
    #endregion

    #endregion
}


