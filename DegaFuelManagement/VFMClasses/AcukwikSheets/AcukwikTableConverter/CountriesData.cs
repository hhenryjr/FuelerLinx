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
    class CountriesData
    {
        #region Public Methods
        public bool IsConvertSuccessful(DataSheetCollection supportedSheetCollection, DataTable uploadedSheetTable)
        {
            AcukwikDataSet.AcukwikCountriesDataTable convertedTable = ConvertTable(supportedSheetCollection, uploadedSheetTable);
            if (convertedTable == null) return false;
            UploadDatabaseRecords(convertedTable);
            if (convertedTable.Rows.Count == 0) return false;
            return true;
        }
        #endregion

        #region Private Methods
        private AcukwikDataSet.AcukwikCountriesDataTable ConvertTable(DataSheetCollection supportedSheetCollection, DataTable uploadedSheetTable)
        {
            AcukwikDataSet.AcukwikCountriesDataTable result = null;
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
            AcukwikCountriesTableAdapter adapter = new AcukwikCountriesTableAdapter();
            adapter.Connection = new SqlConnection(ExecutionHelper.GetConnectionString());
            adapter.DeleteData();

            //Bulk insert the new records
            using (SqlBulkCopy s = new SqlBulkCopy(ExecutionHelper.GetConnectionString()))
            {
                s.DestinationTableName = "AcukwikCountries";
                s.BatchSize = 5000;

                s.ColumnMappings.Add("COUNTRY_ID", "COUNTRY_ID");
                s.ColumnMappings.Add("Country", "Country");
                s.ColumnMappings.Add("FullCountryName", "FullCountryName");
                s.ColumnMappings.Add("ISOCountryID", "ISOCountryID");
                s.ColumnMappings.Add("ISO_Name", "ISO_Name");
                s.ColumnMappings.Add("ISOCode2", "ISOCode2");
                s.ColumnMappings.Add("ISOCode3", "ISOCode3");
                s.ColumnMappings.Add("Region", "Region");

                s.WriteToServer(convertedTable);
                s.Close();
            }
        }

        private AcukwikDataSet.AcukwikCountriesDataTable AttemptToConvertFormat(DataSheet supportedDataSheet, DataTable uploadedSheetTable)
        {
            AcukwikDataSet.AcukwikCountriesDataTable result = new AcukwikDataSet.AcukwikCountriesDataTable();
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
