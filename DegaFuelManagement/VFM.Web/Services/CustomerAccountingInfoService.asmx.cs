using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for CustomerAccountingInfoService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CustomerAccountingInfoService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static CustomerAccountingInfo UpdateInfo(object detailOBJ)
        {
            CustomerAccountingInfo detail = new CustomerAccountingInfo();
            detail.Clone(detailOBJ);
            detail.Update();
            return detail;
        }

        [WebMethod(EnableSession = true)]
        public static CustomerAccountingInfo GetInfoByAdminAndCustClientID(int adminId, int custId)
        {
            return CustomerAccountingInfo.LoadByAdminAndCustClientID(adminId, custId);
        }

        //[WebMethod(EnableSession = true)]
        //public static CustomerAccountingInfo GetCustomerAccountingInfoInfo(int Id)
        //{
        //    CustomerAccountingInfo detail = new CustomerAccountingInfo(Id);
        //    detail.Contacts = new ContactsCollection();
        //    detail.Contacts.LoadByCustClientID(Id);
        //    detail.CustomerNotes = new CustomerNotesCollection();
        //    detail.CustomerNotes.LoadByCustClientID(Id);
        //    detail.Users = new UsersCollection();
        //    detail.Users.LoadByClientID(Id);
        //    detail.Aircrafts = new AircraftsCollection();
        //    detail.Aircrafts.LoadByCustClientID(Id);
        //    return detail;
        //}

        //[WebMethod(EnableSession = true)]
        //public static List<CustomerAccountingInfo> GetListOfCustomerAccountingInfo()
        //{
        //    return CustomerAccountingInfo.LoadList();
        //}

        //[WebMethod(EnableSession = true)]
        //public static List<CustomerAccountingInfo> GetListOfCustomersByAdminClient(int clientId)
        //{
        //    return CustomerAccountingInfo.LoadListByAdminClient(clientId);
        //}

        //[WebMethod(EnableSession = true)]
        //public static void DeleteCustomerAccountingInfo(int id)
        //{
        //    CustomerAccountingInfo.DeleteCustomerAccountingInfo(id);
        //}
        #endregion
    }
}
