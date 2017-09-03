using System.Web;
using System.Web.Http;
using VFMClasses;
using VFMClasses.FuelSheets;
using VFMClasses.AcukwikSheets;
using VFM.Web.Models.Responses;
using VFM.Web.Services;
using System.Net.Http;
using Degatech.Utilities.Session;
using VFMClasses.FuelOrder;
using VFMClasses.ScheduleSheets;
using System;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/files")]
    public class FileAPIController : ApiController
    {
        [Route("supplier/{id:int}/{name}"), HttpPost]
        public HttpResponseMessage SupplierUpload(int id, string name)
        {
            var request = HttpContext.Current.Request;
            bool IsSucccess = false;
            foreach (string fileKey in request.Files)
            {
                HttpPostedFile file = request.Files[fileKey];
                IsSucccess = PerformStandardSave(file, id, name);
            }

            ItemResponse<string> response = new ItemResponse<string>();
            response.Item = FileUploadService.UploadResult(IsSucccess);
            return Request.CreateResponse(response);
        }

        [Route("acukwik/{acukwikName:alpha}"), HttpPost]
        public HttpResponseMessage AcukwikUpload(string acukwikName)
        {
            var request = HttpContext.Current.Request;
            bool IsSucccess = false;
            foreach (string fileKey in request.Files)
            {
                HttpPostedFile file = request.Files[fileKey];
                IsSucccess = PerformAcukwikSave(file, acukwikName);
            }

            ItemResponse<string> response = new ItemResponse<string>();
            response.Item = FileUploadService.UploadResult(IsSucccess);
            return Request.CreateResponse(response);
        }

        [Route("attachment/{fuelOrderId:int}"), HttpPost]
        public HttpResponseMessage UploadPDF(int fuelOrderId)
        {
            var request = HttpContext.Current.Request;
            bool IsSucccess = false;
            foreach (string fileKey in request.Files)
            {
                HttpPostedFile file = request.Files[fileKey];
                IsSucccess = PerformAttachmentSave(file, HttpContext.Current, fuelOrderId);
            }

            ItemResponse<string> response = new ItemResponse<string>();
            response.Item = FileUploadService.UploadResult(IsSucccess);
            return Request.CreateResponse(response);
        }

        [Route("scheduling/{name:alpha}/{schedulingId:int}/{clientId:int}"), HttpPost]
        public HttpResponseMessage UploadCSV(string name, int schedulingId, int clientId)
        {
            var request = HttpContext.Current.Request;
            bool IsSucccess = false;
            foreach (string fileKey in request.Files)
            {
                HttpPostedFile file = request.Files[fileKey];
                IsSucccess = PerformSchedulingSave(file, name, schedulingId, clientId);
            }

            ItemResponse<string> response = new ItemResponse<string>();
            response.Item = FileUploadService.UploadResult(IsSucccess);
            return Request.CreateResponse(response);
        }

        #region Private Methods
        private string GetSaveDirectory()
        {
            var request = HttpContext.Current.Request;
            string serverPath = request.PhysicalApplicationPath;
            string uploadPath = serverPath + @"Uploads\";
            return uploadPath;
        }

        private bool PerformStandardSave(HttpPostedFile file, int supplierId, string supplierName)
        {
            try
            {
                string fileType = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);
                string savePath = string.Format("{0}\\{1}_{2}.{3}", GetSaveDirectory(), Users.CurrentUser.Id, supplierId, fileType);

                file.SaveAs(savePath);
                SessionHelper.Instance.MostRecentlyUploadedFilePath = savePath;

                Suppliers Supplier = new Suppliers();
                Supplier.Id = supplierId;
                Supplier.SupplierName = supplierName;

                FuelSheetHandler handler = new FuelSheetHandler();
                return handler.Process(savePath, Users.CurrentUser, Supplier);
            }
            catch (Exception exception)
            {
                return false;
                throw new Exception(exception.ToString());
            }
        }
        private bool PerformAcukwikSave(HttpPostedFile file, string acukwikName)
        {
            try
            {
                string fileType = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);
                string savePath = string.Format("{0}\\{1}_{2}.{3}", GetSaveDirectory(), Users.CurrentUser.Id, acukwikName, fileType);

                AcukwikUploads acukwikData = new AcukwikUploads();
                acukwikData.Name = acukwikName;
                AcukwikUploadsService.UpdateAcukwikUpload(acukwikData);

                file.SaveAs(savePath);
                SessionHelper.Instance.MostRecentlyUploadedFilePath = savePath;
                AcukwikSheetHandler handler = new AcukwikSheetHandler();
                return handler.Process(savePath, acukwikName);
            }
            catch (System.Exception exception)
            {
                return false;
                throw new System.Exception(exception.ToString());
            }
        }

        private bool PerformSchedulingSave(HttpPostedFile file, string name, int scheduleId, int custId)
        {
            try
            {
                string fileType = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);
                string savePath = string.Format("{0}\\{1}_{2}_{3}.{4}", GetSaveDirectory(), Users.CurrentUser.Id, name, custId, fileType);

                SchedulingUploads schedule = new SchedulingUploads();
                schedule.Name = name;
                schedule.CustClientID = custId;
                schedule.AdminClientID = Users.CurrentUser.ClientID;
                SchedulingUploadsService.UpdateSchedulingUpload(schedule);

                file.SaveAs(savePath);
                SessionHelper.Instance.MostRecentlyUploadedFilePath = savePath;
                SchedulingSheetHandler handler = new SchedulingSheetHandler();
                return handler.Process(savePath, Users.CurrentUser.ClientID, scheduleId, custId);
            }
            catch (Exception exception)
            {
                return false;
                throw new Exception(exception.ToString());
            }
        }

        private bool PerformAttachmentSave(HttpPostedFile file, HttpContext context, int fuelOrderId)
        {
            try
            {
                string fileType = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);
                string savePath = string.Format("{0}\\{1}_{2}.{3}", GetSaveDirectory(), Users.CurrentUser.Id, fuelOrderId, fileType);

                file.SaveAs(savePath);
                FuelOrderInvoices invoice = new FuelOrderInvoices();
                invoice.Update(file, fuelOrderId);
                HttpContext.Current.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(invoice));
                return true;
            }
            catch (Exception exception)
            {
                return false;
                throw new Exception(exception.ToString());
            }
        }
        #endregion
    }
}
