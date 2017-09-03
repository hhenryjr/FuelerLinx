using Degatech.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFM.API.EntityServices;
using VFM.EDM;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for CustomerDetailsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CustomerDetailsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateCustomerDetail(object detailOBJ)
        {
            CustomerDetails detail = new CustomerDetails();
            detail.Clone(detailOBJ);
            detail.Update();
            return detail.Id;
        }

        [WebMethod(EnableSession = true)]
        public static CustomerDetails GetCustomerDetail(int id)
        {
            return CustomerDetails.GetCustomerDetail(id);
        }

        [WebMethod(EnableSession = true)]
        public static CustomerDetails GetCustomerDetailInfo(int Id)
        {
            CustomerDetails detail = new CustomerDetails(Id);
            detail.Contacts = new ContactsCollection();
            detail.Contacts.LoadByCustClientID(Id);
            detail.CustomerNotes = new CustomerNotesCollection();
            detail.CustomerNotes.LoadByCustClientID(Id);
            detail.User = new Users();
            detail.User.LoadByClientID(Id);
            detail.Registration = new Registration(detail.User.RegistrationID);
            detail.Aircrafts = new AircraftsCollection();
            detail.Aircrafts.LoadByCustClientID(Id);
            detail.CustomerPriceMargin = new CustomerPriceMargins();
            detail.CustomerPriceMargin.LoadByCustClientID(Id);
            detail.CustomerAccountingInfo = new CustomerAccountingInfo();
            detail.CustomerAccountingInfo.LoadInfoByAdminAndCustClientID(detail.AdminClientID, detail.CustClientID);
            detail.CustomFields = new CustomerDetailsCustomFieldsCollection();
            detail.CustomFields.GetCustomFields(detail.AdminClientID, detail.CustClientID);
            var partnerService = new PartnerServiceIntegrations();
            detail.Integration = partnerService.GetByClientID(Id, detail.AdminClientID);
            if (detail.Integration == null)
                detail.Integration = new PartnerServiceIntegration();
            return detail;
        }

        [WebMethod(EnableSession = true)]
        public static List<CustomerDetails> GetListOfCustomerDetails()
        {
            return CustomerDetails.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static List<CustomerDetails> GetListOfCustomersByAdminClient(int clientId)
        {
            return CustomerDetails.LoadListByAdminClient(clientId);
        }

        [WebMethod(EnableSession = true)]
        public static string GetCustomersAutoCompleteListByAdminClientID(int adminId)
        {
            return JsonConvert.SerializeObject(CustomersAutoCompleteDataSource.GetCustomersDataSourceFromCache(adminId));
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteCustomerDetail(int id)
        {
            CustomerDetails.DeleteCustomerDetail(id);
        }
        #endregion
    }
}
