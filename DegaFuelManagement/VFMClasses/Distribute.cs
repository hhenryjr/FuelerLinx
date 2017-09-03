using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Degatech.Common;
using Degatech.Utilities.SQL;
using MailMessage = Degatech.Utilities.Mail.MailMessage;


namespace VFMClasses
{
    public class Distribute : BaseClass
    {
        #region Properties
        public int JobID { get; set; }
        public int AdminClientID { get; set; }
        public int PriceMargin { get; set; }
        public int CustClientID { get; set; }
        public int ContactID { get; set; }
        public SortedList SLEmail { get; set; }
        public SortedList SLEmailHeader { get; set; }
        public SortedList SLEmailBody { get; set; }
        public EmailRouting emailRouting = null;
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactFax { get; set; }
        public string ContactEmail { get; set; }
        public string ContactAddress { get; set; }
        public string FBONameICAO { get; set; }
        public int PriceMarginTemplateID { get; set; } = 0;
        public string LogoURL { get; set; }
        #endregion

        #region Constructors
        public Distribute(int adminClientID)
        {
            AdminClientID = adminClientID;
            EmailRoutingCollection emailsCollection = EmailRouting.GetEmailsByAdminClient(AdminClientID);
            emailRouting = new EmailRouting();
            foreach (EmailRouting emails in emailsCollection)
            {
                if (emails.EmailType == "Price Distribution")
                {
                    emailRouting = emails;
                    break;
                }
            }
            //emailRouting.AdminClientID = 2;
            //emailRouting.FromEmail = "chau@fuelerlinx.com";
            //emailRouting.Subject = "test";
            //emailRouting.ToEmail = "test@fuelerlinx.com";
        }
        #endregion


        #region Private Methods
        private void PopulateDistributionEmailsSortedList(SqlDataReader reader)
        {
            if (ContactID == 0)
                SLEmail = new SortedList();
            SLEmailBody = new SortedList();
            SLEmailHeader = new SortedList();

            while (reader.Read())
            {
                if (ContactID == 0)
                {
                    if (SLEmail.Contains(reader["CustClientID"]))
                    {
                        if (reader["Email"] != "" && !reader["Email"].ToString().Contains("jetfuelx"))
                            SLEmail[reader["CustClientID"]] += ";" + reader["Email"];
                    }
                    else
                    {
                        int clientid = 0;
                        if (reader["Email"] != "" && !reader["Email"].ToString().Contains("jetfuelx"))
                        {
                            clientid = int.Parse(reader["CustClientID"].ToString());
                        }
                        SLEmail.Add(clientid, reader["email"]);
                    }
                }
            }

            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    SLEmailBody.Add(reader["Id"], reader["Note"]);
                }
            }

            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    SLEmailHeader.Add(reader["CustClientID"], reader["Name"]);
                }
            }
            ExecutionHelper.CloseReader(reader);
        }

        private void CreateDistributionDataTable(ref DataTable dt, ref DataTable dt2, DictionaryEntry key)
        {
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[9].ToString() == key.Key.ToString())
                {
                    if (PriceMarginTemplateID == 0)
                        PriceMarginTemplateID = int.Parse(dr[11].ToString());

                    DataRow tempDR = dt2.NewRow();
                    tempDR.ItemArray = dt.Rows[i].ItemArray;
                    dt2.Rows.Add(tempDR);
                    dr.Delete();
                }
                i++;
            }
            dt2.Columns.RemoveAt(11);
            dt2.Columns.RemoveAt(9);
            dt.AcceptChanges();
        }

        private StreamWriter CreateCSV(DataTable toExcel, StreamWriter writer)
        {
            //Testing out the CSV
            //StringBuilder sb = new StringBuilder();

            //IEnumerable<string> columnNames = toExcel.Columns.Cast<DataColumn>().
            //                                  Select(column => column.ColumnName);
            //sb.AppendLine(string.Join(",", columnNames));

            //foreach (DataRow row in toExcel.Rows)
            //{
            //    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
            //    sb.AppendLine(string.Join(",", fields));
            //}

            //File.WriteAllText(Degatech.Utilities.Data.Utilities.TestEmailsFolderPath() + "\\test.csv", sb.ToString());

            foreach (DataColumn column in toExcel.Columns)
            {
                writer.Write(column.ColumnName.ToUpper() + ",");
            }

            writer.Write(Environment.NewLine);

            foreach (DataRow row in toExcel.Rows)
            {
                for (int j = 1; (j < toExcel.Columns.Count); j++)
                {
                    writer.Write(row[j].ToString().Replace(",", string.Empty) + ",");
                }

                writer.Write(Environment.NewLine);
            }
            return writer;
        }

        private string CreateCSV(DataTable toExcel)
        {
            StringBuilder sb=new StringBuilder();
            foreach (DataColumn column in toExcel.Columns)
            {
                sb.Append(column.ColumnName.ToUpper() + ",");
            }

            sb.Append(Environment.NewLine);

            foreach (DataRow row in toExcel.Rows)
            {
                for (int j = 0; (j < toExcel.Columns.Count); j++)
                {
                    sb.Append(row[j].ToString().Replace(",", string.Empty) + ",");
                }

                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        private void SendEmail(DataTable dt, string emails)
        {
            DataTable toExcel = dt.Copy();
            byte[] data = Encoding.ASCII.GetBytes(CreateCSV(toExcel));

            using (var ms = new System.IO.MemoryStream(data))
            {
                System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ms, "text/csv");
                attach.ContentDisposition.FileName = DateTime.Now.ToString("MM/dd/yyyy").Replace("/", "-") + ".csv";

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(emailRouting.FromEmail);
                string[] email = emails.Split(';');
                foreach (string e in email)
                {
                    if (e != "")
                    {
                        mailMessage.To.Add(new MailAddress(e));
                    }
                }

                mailMessage.Attachments.Add(attach);
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;
                mailMessage.Subject = emailRouting.Subject;
                if (LogoURL != "")
                    mailMessage.Body = "<img src='" + LogoURL + "' ><br/><br/>";
                mailMessage.Body += SLEmailBody[PriceMarginTemplateID].ToString();

                try
                {
                    Degatech.Utilities.Data.Utilities.SendEmail(mailMessage);
                }
                catch (Exception ex)
                {
                    //LogDistributionError("Current email: " + emails.ToString() + " Error message: " + ex.Message + "<br/>", _GroupID, _JobID);
                }
                finally
                {
                    mailMessage.Attachments.Dispose();
                }
                // after sending email
            }
        }

        private void GetLogoURL()
        {
            List<SqlParameter> Params = new List<SqlParameter>();
            SqlParameter Param = new SqlParameter("@AdminClientID", SqlDbType.Int);
            Param.Value = AdminClientID;
            Params.Add(Param);
            SqlDataReader reader = ExecutionHelper.ExecuteReader("up_DistributionEmails_GetLogoURL", Params);

            while (reader.Read())
            {
                LogoURL = reader["LogoURL"].ToString();
            }
        }
        #endregion

        #region Public Methods
        public void SelectDistributionEmails()
        {
            List<SqlParameter> Params = new List<SqlParameter>();
            SqlParameter Param = new SqlParameter("@AdminClientID", SqlDbType.Int);
            Param.Value = AdminClientID;
            Params.Add(Param);
            if (CustClientID > 0)
            {
                Param = new SqlParameter("@CustClientID", SqlDbType.Int);
                Param.Value = CustClientID;
                Params.Add(Param);
            }
            PopulateDistributionEmailsSortedList(ExecutionHelper.ExecuteReader("up_DistributionEmails_Get", Params));
        }

        //public void LogDistributionError(string error, int GroupID, int JobID)
        //{
        //    List<SqlParameter> Params = new List<SqlParameter>();
        //    SqlParameter Param = new SqlParameter("@error", SqlDbType.VarChar);
        //    Param.Value = error;
        //    Params.Add(Param);
        //    Param = new SqlParameter("@GroupID", SqlDbType.Int);
        //    Param.Value = GroupID;
        //    Params.Add(Param);
        //    Param = new SqlParameter("@JobID", SqlDbType.Int);
        //    Param.Value = JobID;
        //    Params.Add(Param);
        //    ExecutionHelper.ExecuteNonQuery("up_DistributionEmails_LogError", Params);
        //}

        //public void LogSuccessfulEmailSent(string email, int GroupID, int CustomerID, int JobID)
        //{
        //    List<SqlParameter> Params = new List<SqlParameter>();
        //    SqlParameter Param = new SqlParameter("@email", SqlDbType.VarChar);
        //    Param.Value = email;
        //    Params.Add(Param);
        //    Param = new SqlParameter("@GroupID", SqlDbType.Int);
        //    Param.Value = GroupID;
        //    Params.Add(Param);
        //    Param = new SqlParameter("@CustomerID", SqlDbType.Int);
        //    Param.Value = CustomerID;
        //    Params.Add(Param);
        //    Param = new SqlParameter("@JobID", SqlDbType.Int);
        //    Param.Value = JobID;
        //    Params.Add(Param);
        //    ExecutionHelper.ExecuteNonQuery("up_DistributionEmails_LogSuccess", Params);
        //}

        //public SqlDataReader GetDistributionErrors(int GroupID)
        //{
        //    List<SqlParameter> Params = new List<SqlParameter>();
        //    SqlParameter Param = new SqlParameter("@GroupID", SqlDbType.Int);
        //    Param.Value = GroupID;
        //    Params.Add(Param);
        //    Param = new SqlParameter("@JobID", SqlDbType.Int);
        //    Param.Value = JobID;
        //    Params.Add(Param);
        //    return ExecutionHelper.ExecuteReader("up_DistributionEmails_GetErrors", Params);
        //}

        //public void InsertJobID(int GroupID)
        //{
        //    List<SqlParameter> Params = new List<SqlParameter>();
        //    SqlParameter Param = new SqlParameter("@GroupID", SqlDbType.VarChar);
        //    Param.Value = GroupID;
        //    Params.Add(Param);
        //    JobID = int.Parse(ExecutionHelper.ExecuteScalar("up_Insert_Jobs", Params).ToString());
        //}

        public void CreateDistributionCsvAndSendEmail()
        {
            DataTable dt = null;

            GetLogoURL();

            foreach (DictionaryEntry key in SLEmail)
            {
                if (CustClientID == 0)
                    CustClientID = int.Parse(key.Key.ToString());

                dt = GetDistributePricing();

                if (dt.Rows.Count > 0)
                {
                    DataTable dt2 = dt.Clone();
                    CreateDistributionDataTable(ref dt, ref dt2, key);

                    if (dt2.Rows.Count > 0)
                    {
                        SendEmail(dt2, key.Value.ToString());
                    }
                }

                CustClientID = 0;
            }
        }

        public DataTable GetDistributePricing()
        {
            List<SqlParameter> Params = new List<SqlParameter>();
            SqlParameter Param = new SqlParameter("@AdminClientID", AdminClientID);
            Params.Add(Param);
            if (CustClientID > 0)
            {
                Param = new SqlParameter("@CustClientID", CustClientID);
                Params.Add(Param);
            }
            if (ContactID > 0)
            {
                Param = new SqlParameter("@ContactID", ContactID);
                Params.Add(Param);
            }
            if (PriceMargin > 0)
            {
                Param = new SqlParameter("@PriceMargin", PriceMargin);
                Params.Add(Param);
            }
            Param = new SqlParameter("@Mode", "distribute");
            Params.Add(Param);

            DataSet ds = ExecutionHelper.ExecuteDataset("up_Load_Pricing", Params);
            return ds.Tables[0];
        }

        public void StartThreadingEmails()
        {
            Thread t;
            t = new Thread(this.CreateDistributionCsvAndSendEmail);
            t.Start();
        }
        #endregion

        #region Static Methods

        #endregion
    }
}
