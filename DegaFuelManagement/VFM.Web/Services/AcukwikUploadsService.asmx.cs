using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for AcukwikUploadsService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AcukwikUploadsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static AcukwikUploads UpdateAcukwikUpload(object uploadOBJ)
        {
            AcukwikUploads upload = new AcukwikUploads();
            upload.Clone(uploadOBJ);
            upload.Update();
            return upload;
        }

        [WebMethod(EnableSession = true)]
        public static AcukwikUploads GetAcukwikUpload(string name)
        {
            return new AcukwikUploads(name);
        }
        #endregion
    }
}
