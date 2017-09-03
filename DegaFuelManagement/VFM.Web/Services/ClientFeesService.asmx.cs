using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for ClientFeesService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ClientFeesService : VFMWebService
    {

        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static ClientFees UpdateFee(object feeOBJ)
        {
            ClientFees fee = new ClientFees();
            fee.Clone(feeOBJ);
            fee.Update();
            return fee;
        }

        [WebMethod(EnableSession = true)]
        public static ClientFees GetFee(int id)
        {
            return new ClientFees(id);
        }

        //[WebMethod(EnableSession = true)]
        //public static List<ClientFees> GetClientFeesByAdminClient(int clientId)
        //{
        //    return ClientFees.LoadListByAdminClient(clientId);
        //}

        [WebMethod(EnableSession = true)]
        public static void DeleteClientFee(int clientID, string feeDesc)
        {
            ClientFees.Delete(clientID, feeDesc);
        }
        #endregion
    }
}
