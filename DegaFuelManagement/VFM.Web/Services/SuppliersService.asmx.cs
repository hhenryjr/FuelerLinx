using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for SuppliersService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SuppliersService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateSupplier(object supplierOBJ)
        {
            Suppliers supplier = new Suppliers();
            supplier.Clone(supplierOBJ);
            supplier.Update();
            return supplier.Id;
        }

        [WebMethod(EnableSession = true)]
        public static Suppliers GetSupplier(int id)
        {
            return Suppliers.GetSupplier(id);
            //Suppliers order = new Suppliers(id);
            //return order;
        }

        [WebMethod(EnableSession = true)]
        public static List<Suppliers> GetSuppliersByAdmin(int adminId)
        {
            return Suppliers.LoadByAdminClient(adminId);
        }

        [WebMethod(EnableSession = true)]
        public static List<Suppliers> GetListOfSuppliers()
        {
            return Suppliers.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteSupplier(int id)
        {
            Suppliers.Delete(id);
        }
        #endregion
    }
}
