using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Degatech.Common;
using Degatech.Utilities.SQL;

namespace VFMClasses
{
    #region ClientNote
    /// <summary>
    /// This object represents the properties and methods of a ClientNote.
    /// </summary>
    public class CustomerNotes : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int AdminClientID { get; set; }
        public int CustClientID { get; set; }
        public string Note { get; set; } = String.Empty;
        public int AddedByUserID { get; set; }
        public int UpdatedByUserID { get; set; }
        public DateTime DateAdded { get; set; }
        #endregion

        #region Constructors
        public CustomerNotes()
        {
        }

        public CustomerNotes(int id)
        {
            Id = id;
            Load();
        }

        public CustomerNotes(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerNote", parameters))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }

        protected void LoadFromReader(SqlDataReader reader)
        {
            SetProperties(reader);
        }

        public void Delete()
        {
            DeleteCustomerNote(Id);
        }

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_CustomerNote", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods

        public static CustomerNotesCollection LoadList()
        {
            CustomerNotesCollection collection = new CustomerNotesCollection();
            collection.LoadAll();
            return collection;
        }

        public static CustomerNotes GetCustomerNote(int id)
        {
            return new CustomerNotes(id);
        }

        public static CustomerNotesCollection DeleteCustomerNote(int id)
        {
            CustomerNotesCollection collection = new CustomerNotesCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class CustomerNotesCollection : List<CustomerNotes>
    {

        #region Public Methods
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerNotesAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerNotes note = new CustomerNotes();
                    note.SetProperties(reader);
                    Add(note);
                }
            }
        }

        public void LoadByCustClientID(int custCustomerId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustClientID", custCustomerId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerNotesByAndCustClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerNotes note = new CustomerNotes();
                    note.SetProperties(reader);
                    Add(note);
                }
            }
        }

        //public void LoadByCustClientID(int custClientId)
        //{
        //    List<SqlParameter> parameters = new List<SqlParameter>();
        //    parameters.Add(new SqlParameter("@CustClientID", custClientId));
        //    using (
        //        SqlDataReader reader =
        //            ExecutionHelper.ExecuteReader("up_Select_ClientNotesByAndCustClientID", parameters))
        //    {
        //        if (reader == null)
        //            return;
        //        while (reader.Read())
        //        {
        //            CustomerNotes note = new CustomerNotes();
        //            note.SetProperties(reader);
        //            Add(note);
        //        }
        //    }
        //}

        //public void LoadByClient(Clients client)
        //{
        //    if (client.ClientType == Clients.ClientTypes.Admin)
        //    {
        //        LoadByAdminClientID(client.Id);
        //    }
        //    else
        //    {
        //        LoadByCustClientID(client.Id);
        //    }
        //}

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_CustomerNote", parameters);
        }

        public CustomerNotes GetCustomerNotesByID(int id)
        {
            return this.FirstOrDefault(note => note.Id == id);
        }

        public CustomerNotes GetAdminClientID(int adminID)
        {
            return this.FirstOrDefault(note => note.AdminClientID == adminID);
        }

        public CustomerNotes GetCustClientID(int custID)
        {
            return this.FirstOrDefault(note => note.CustClientID == custID);
        }

        public CustomerNotes GetCustomerNote(string customerNote)
        {
            return this.FirstOrDefault(note => note.Note == customerNote);
        }

        public CustomerNotes GetAddedByUserID(int addedByUserId)
        {
            return this.FirstOrDefault(note => note.AddedByUserID == addedByUserId);
        }

        public CustomerNotes GetUpdatedByUserID(int updatedByUserId)
        {
            return this.FirstOrDefault(note => note.UpdatedByUserID == updatedByUserId);
        }

        public CustomerNotes GetDateAdded(DateTime date)
        {
            return this.FirstOrDefault(note => note.DateAdded == date);
        }
        #endregion
    }
    #endregion

}