using Degatech.Common.DataSheet.DataSheetParsing;
using Degatech.Utilities.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFMClasses.DataSets;
using VFMClasses.DataSets.AcukwikDataSetTableAdapters;

namespace VFMClasses.AcukwikSheets.AcukwikTableConverter
{
    class SubdivisionStatesData
    {
        #region Public Methods
        public bool IsConvertSuccessful(DataSheetCollection supportedSheetCollection, DataTable uploadedSheetTable)
        {
            AcukwikDataSet.AcukwikSubdivisionStatesDataTable convertedTable = ConvertTable(supportedSheetCollection, uploadedSheetTable);
            if (convertedTable == null) return false;
            UploadDatabaseRecords(convertedTable);
            if (convertedTable.Rows.Count == 0) return false;
            return true;
        }
        #endregion

        #region Private Methods
        private AcukwikDataSet.AcukwikSubdivisionStatesDataTable ConvertTable(DataSheetCollection supportedSheetCollection, DataTable uploadedSheetTable)
        {
            AcukwikDataSet.AcukwikSubdivisionStatesDataTable result = null;
            foreach (DataSheet supportedDataSheet in supportedSheetCollection)
            {
                result = AttemptToConvertFormat(supportedDataSheet, uploadedSheetTable);
                if (result != null && result.Rows.Count > 0)
                    return result;
            }
            return null;
        }

        private void UploadDatabaseRecords(DataTable convertedTable)
        {
            //Delete all old records for that supplier/admin
            AcukwikSubdivisionStatesTableAdapter adapter = new AcukwikSubdivisionStatesTableAdapter();
            adapter.Connection = new SqlConnection(ExecutionHelper.GetConnectionString());
            adapter.DeleteData();

            //Bulk insert the new records
            using (SqlBulkCopy s = new SqlBulkCopy(ExecutionHelper.GetConnectionString()))
            {
                s.DestinationTableName = "AcukwikSubdivisionStates";
                s.BatchSize = 5000;

                s.ColumnMappings.Add("StateSubdivision_ID", "StateSubdivision_ID");
                s.ColumnMappings.Add("StateSubdivisionName", "StateSubdivisionName");
                s.ColumnMappings.Add("StateSubdivisionAbbr", "StateSubdivisionAbbr");
                s.ColumnMappings.Add("Country", "Country");

                s.WriteToServer(convertedTable);
                s.Close();
            }
        }

        private AcukwikDataSet.AcukwikSubdivisionStatesDataTable AttemptToConvertFormat(DataSheet supportedDataSheet, DataTable uploadedSheetTable)
        {
            AcukwikDataSet.AcukwikSubdivisionStatesDataTable result = new AcukwikDataSet.AcukwikSubdivisionStatesDataTable();
            try
            {
                if (!supportedDataSheet.PopulateRowsFromData(result, uploadedSheetTable))
                    return null;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
            return result;
        }
        #endregion
    }
}
