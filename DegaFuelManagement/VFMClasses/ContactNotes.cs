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
    #region ContactNote
    /// <summary>
    /// This object represents the properties and methods of a ContactNote.
    /// </summary>
    public class ContactNotes : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int ContactID { get; set; }
        public string Note { get; set; } = String.Empty;
        public int AddedByUserID { get; set; }
        public int UpdatedByUserID { get; set; }
        public int CustClientID { get; set; }
        public DateTime DateAdded { get; set; }
        #endregion

        #region Constructors
        public ContactNotes()
        {
        }

        public ContactNotes(int id)
        {
            Id = id;
            Load();
        }

        public ContactNotes(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_ContactNote", parameters))
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
            DeleteContactNote(Id);
        }

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_ContactNote", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods

        public static ContactNotesCollection LoadList()
        {
            ContactNotesCollection collection = new ContactNotesCollection();
            collection.LoadAll();
            return collection;
        }

        public static ContactNotes GetContactNote(int id)
        {
            return new ContactNotes(id);
        }

        public static ContactNotesCollection DeleteContactNote(int id)
        {
            ContactNotesCollection collection = new ContactNotesCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class ContactNotesCollection : List<ContactNotes>
    {

        #region Public Methods
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_ContactNotesAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    ContactNotes note = new ContactNotes();
                    note.SetProperties(reader);
                    Add(note);
                }
            }
        }

        public void LoadByContactID(int contactId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ContactID", contactId));
            using (
                SqlDataReader reader =
                    ExecutionHelper.ExecuteReader("up_Select_ContactNotesByAndContactID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    ContactNotes note = new ContactNotes();
                    note.SetProperties(reader);
                    Add(note);
                }
            }
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_ContactNote", parameters);
        }

        public ContactNotes GetContactNotesByID(int id)
        {
            return this.FirstOrDefault(note => note.Id == id);
        }

        public ContactNotes GetContactID(int contactID)
        {
            return this.FirstOrDefault(note => note.ContactID == contactID);
        }

        public ContactNotes GetContactNote(string contacNote)
        {
            return this.FirstOrDefault(note => note.Note == contacNote);
        }

        public ContactNotes GetAddedID(int userId)
        {
            return this.FirstOrDefault(note => note.AddedByUserID == userId);
        }

        public ContactNotes GetUpdatedID(int userId)
        {
            return this.FirstOrDefault(note => note.UpdatedByUserID == userId);
        }

        public ContactNotes GetCustClientID(int clientId)
        {
            return this.FirstOrDefault(note => note.CustClientID == clientId);
        }

        public ContactNotes GetContactNoteByDateAdded(DateTime date)
        {
            return this.FirstOrDefault(note => note.DateAdded == date);
        }
        #endregion
    }
    #endregion

}