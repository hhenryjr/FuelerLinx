using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for SchedulingUploadsService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SchedulingUploadsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static SchedulingUploads UpdateSchedulingUpload(object uploadOBJ)
        {
            SchedulingUploads upload = new SchedulingUploads();
            upload.Clone(uploadOBJ);
            upload.Update();
            return upload;
        }

        [WebMethod(EnableSession = true)]
        public static SchedulingUploads GetSchedulingUpload(string name, int custId)
        {
            return new SchedulingUploads(name, custId, Users.CurrentUser.ClientID);
        }
        #endregion
    }
}
