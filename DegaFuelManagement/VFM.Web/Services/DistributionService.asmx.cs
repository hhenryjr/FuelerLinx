using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for PriceMarginsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DistributionService : VFMWebService
    {
        [WebMethod(EnableSession = true)]
        public static bool DistributeCompany(int AdminClientID, int CustClientID)
        {
            Distribute distribute = new Distribute(AdminClientID);
            distribute.CustClientID = CustClientID;
            distribute.SelectDistributionEmails();
            distribute.StartThreadingEmails();
            return true;
        }

        [WebMethod(EnableSession = true)]
        public static bool DistributeContact(int AdminClientID, int ContactID)
        {
            Distribute distribute = new Distribute(AdminClientID);
            distribute.ContactID = ContactID;
            VFMClasses.Contacts contact = new Contacts(ContactID);
            distribute.SLEmail = new SortedList();
            distribute.SLEmail.Add(contact.CustClientID, contact.Email);
            distribute.SelectDistributionEmails();
            distribute.StartThreadingEmails();
            return true;
        }

        [WebMethod(EnableSession = true)]
        public static void DistributeAllMargins(int AdminClientID)
        {
            Distribute distribute = new Distribute(AdminClientID);
            distribute.SelectDistributionEmails();
            distribute.StartThreadingEmails();
        }

        [WebMethod(EnableSession = true)]
        public static void DistributeMargin(int AdminClientID, int PriceMarginID)
        {
            Distribute distribute = new Distribute(AdminClientID);
            distribute.PriceMargin = PriceMarginID;
            distribute.SelectDistributionEmails();
            distribute.StartThreadingEmails();
        }
    }
}
