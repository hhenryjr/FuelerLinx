using Degatech.Common;
using Degatech.Utilities.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VFMClasses.FuelOrder
{
    public class FuelOrderInvoices : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int FuelOrderID { get; set; }
        public byte[] InvoiceData { get; set; }
        public string InvoiceName { get; set; }
        public string ContentType { get; set; }
        #endregion

        #region Public Methods
        public FuelOrderInvoices GetAttachmentFile()
        {
            FuelOrderInvoices fileInfo = new FuelOrderInvoices();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrderInvoice", parameters))
            {
                if (ConnectionHelper.IsReadingOneRowSuccessful(reader))
                {
                    fileInfo.ContentType = reader["ContentType"].ToString();
                    fileInfo.InvoiceData = (byte[]) reader["InvoiceData"];
                    SetProperties(reader);
                }
            }
            return fileInfo;
        }

        public void Update(HttpPostedFile file, int fuelOrderId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            parameters.Add(new SqlParameter("@FuelOrderID", fuelOrderId));
            parameters.Add(new SqlParameter("@InvoiceName", System.IO.Path.GetFileName(file.FileName)));
            parameters.Add(new SqlParameter("@InvoiceData", SqlDbType.Binary) { Value = Degatech.Utilities.IO.File.Helper.ToByteArray(file) });
            parameters.Add(new SqlParameter("@ContentType", file.ContentType));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_FuelOrderInvoice", parameters))
            {
                if (ConnectionHelper.IsReadingOneRowSuccessful(reader))
                    SetProperties(reader);
            }
        }

        public void Update()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            parameters.Add(new SqlParameter("@FuelOrderID", FuelOrderID));
            parameters.Add(new SqlParameter("@InvoiceName", System.IO.Path.GetFileName(InvoiceName)));
            parameters.Add(new SqlParameter("@InvoiceData", SqlDbType.Binary) { Value = InvoiceData });
            parameters.Add(new SqlParameter("@ContentType", ContentType));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_FuelOrderInvoice", parameters))
            {
                if (ConnectionHelper.IsReadingOneRowSuccessful(reader))
                    SetProperties(reader);
            }
        }

        public void DownloadAttachment(HttpContext context, int id)
        {
            FuelOrderInvoices invoice = new FuelOrderInvoices();
            invoice.Id = id;
            invoice.GetAttachmentFile();
            DownloadFileFromBinary(context, invoice.ContentType, invoice.InvoiceName, invoice.InvoiceData);
        }

        public void DeleteFromDatabase()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FuelOrderID", FuelOrderID));
            parameters.Add(new SqlParameter("@InvoiceName", InvoiceName));
            ExecutionHelper.ExecuteNonQuery("up_Invoices_Delete", parameters);
        }

        private void DownloadFileFromBinary(HttpContext context, string contentType, string fileName, byte[] byteArray)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Buffer = true;
            response.Charset = "";
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.ContentType = contentType;
            response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            response.BinaryWrite(byteArray);
            response.Flush();
            response.End();
        }

        public static FuelOrderInvoicesCollection GetInvoicesByFuelOrder(int fuelOrderId)
        {
            FuelOrderInvoicesCollection collection = new FuelOrderInvoicesCollection();
            collection.GetInvoicesByFuelOrder(fuelOrderId);
            return collection;
        }

        public static FuelOrderInvoicesCollection DeleteInvoice(int id)
        {
            FuelOrderInvoicesCollection collection = new FuelOrderInvoicesCollection();
            collection.DeleteInvoice(id);
            return collection;
        }

        public static FuelOrderInvoicesCollection DeleteInvoicesByFuelOrder(int fuelOrderId)
        {
            FuelOrderInvoicesCollection collection = new FuelOrderInvoicesCollection();
            collection.DeleteInvoicesByFuelOrder(fuelOrderId);
            return collection;
        }

        #endregion

        #region Objects
        public class MessageAttachmentFileInfo
        {
            #region Properties
            public string ContentType { get; set; }
            public byte[] FileData { get; set; }
            #endregion
        }

        public class FuelOrderInvoicesCollection : List<FuelOrderInvoices>
        {
            #region Public Methods
            public void GetInvoicesByFuelOrder(int fuelOrderId)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@FuelOrderID", fuelOrderId));
                using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrderInvoicesByFuelOrderID", parameters))
                {
                    if (reader == null)
                        return;
                    while (reader.Read())
                    {
                        FuelOrderInvoices invoice = new FuelOrderInvoices();
                        invoice.SetProperties(reader);
                        Add(invoice);
                    }
                }
            }

            public void DeleteInvoice(int Id)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Id", Id));
                ExecutionHelper.ExecuteNonQuery("up_Delete_FuelOrderInvoice", parameters);
            }

            public void DeleteInvoicesByFuelOrder(int fuelOrderID)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@FuelOrderID", fuelOrderID));
                ExecutionHelper.ExecuteNonQuery("up_Delete_FuelOrderInvoicesByFuelOrderID", parameters);
            }
            #endregion
        }
        #endregion
    }
}
