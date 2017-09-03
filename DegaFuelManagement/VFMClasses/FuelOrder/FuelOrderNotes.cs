using Degatech.Common;
using Degatech.Utilities.SQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFMClasses
{
    #region FuelOrderNote
    /// <summary>
    /// This object represents the properties and methods of a FuelOrderNote.
    /// </summary>
    public class FuelOrderNotes : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int ClientID { get; set; }
        public int FuelOrderID { get; set; }
        public string Note { get; set; } = String.Empty;
        public int AddedByUserID { get; set; }
        public int UpdatedByUserID { get; set; }
        public DateTime DateAdded { get; set; }
        #endregion

        #region Constructors
        public FuelOrderNotes()
        {
        }

        public FuelOrderNotes(int id)
        {
            Id = id;
            Load();
        }

        public FuelOrderNotes(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrderNote", parameters))
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
            DeleteFuelOrderNote(Id);
        }

        public void Update()
        {
            ArrayList propertiesToOmit = new ArrayList();
            propertiesToOmit.Add("DateAdded");
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_FuelOrderNote", GetSQLParameters(propertiesToOmit)))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods

        public static FuelOrderNotesCollection LoadList()
        {
            FuelOrderNotesCollection collection = new FuelOrderNotesCollection();
            collection.LoadAll();
            return collection;
        }

        public static FuelOrderNotes GetFuelOrderNote(int id)
        {
            return new FuelOrderNotes(id);
        }

        public static FuelOrderNotesCollection DeleteFuelOrderNote(int id)
        {
            FuelOrderNotesCollection collection = new FuelOrderNotesCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class FuelOrderNotesCollection : List<FuelOrderNotes>
    {

        #region Public Methods
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrderNotesAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FuelOrderNotes note = new FuelOrderNotes();
                    note.SetProperties(reader);
                    Add(note);
                }
            }
        }

        public void LoadByFuelOrder(int fuelOrderId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FuelOrderID", fuelOrderId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrderNotesByAndFuelOrderID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FuelOrderNotes note = new FuelOrderNotes();
                    note.SetProperties(reader);
                    Add(note);
                }
            }
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_FuelOrderNote", parameters);
        }

        public FuelOrderNotes GetFuelOrderNotesByID(int id)
        {
            return this.FirstOrDefault(note => note.Id == id);
        }

        public FuelOrderNotes GetClientID(int clientId)
        {
            return this.FirstOrDefault(note => note.ClientID == clientId);
        }

        public FuelOrderNotes GetFuelOrderID(int fuelOrderId)
        {
            return this.FirstOrDefault(note => note.FuelOrderID == fuelOrderId);
        }

        public FuelOrderNotes GetNote(string fuelOrderNote)
        {
            return this.FirstOrDefault(note => note.Note == fuelOrderNote);
        }

        public FuelOrderNotes GetAddedByUserID(int addedByUserId)
        {
            return this.FirstOrDefault(note => note.AddedByUserID == addedByUserId);
        }

        public FuelOrderNotes GetUpdatedByUserID(int updatedByUserId)
        {
            return this.FirstOrDefault(note => note.UpdatedByUserID == updatedByUserId);
        }

        public FuelOrderNotes GetFuelOrderNoteByDateAdded(DateTime date)
        {
            return this.FirstOrDefault(note => note.DateAdded == date);
        }
        #endregion
    }
    #endregion

}