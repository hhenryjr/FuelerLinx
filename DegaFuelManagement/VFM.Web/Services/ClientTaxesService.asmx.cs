using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for ClientTaxesService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ClientTaxesService : VFMWebService
    {

        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static ClientTaxes UpdateTax(object taxOBJ)
        {
            ClientTaxes tax = new ClientTaxes();
            tax.Clone(taxOBJ);
            tax.Update();
            return tax;
        }

        [WebMethod(EnableSession = true)]
        public static ClientTaxes GetTax(int id)
        {
            return new ClientTaxes(id);
        }

        //[WebMethod(EnableSession = true)]
        //public static List<ClientTaxes> GetClientTaxesByAdminClient(int clientId)
        //{
        //    return ClientTaxes.LoadListByAdminClient(clientId);
        //}

        [WebMethod(EnableSession = true)]
        public static void DeleteClientTax(int clientID, string taxDesc)
        {
            ClientTaxes.Delete(clientID, taxDesc);
        }
        #endregion
    }
}
