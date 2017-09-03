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
    class FBOHandlerDetailData
    {
        #region Public Methods
        public bool IsConvertSuccessful(DataSheetCollection supportedSheetCollection, DataTable uploadedSheetTable)
        {
            AcukwikDataSet.AcukwikFBOHandlerDetailDataTable convertedTable = ConvertTable(supportedSheetCollection, uploadedSheetTable);
            if (convertedTable == null) return false;
            UploadDatabaseRecords(convertedTable);
            if (convertedTable.Rows.Count == 0) return false;
            return true;
        }
        #endregion

        #region Private Methods
        private AcukwikDataSet.AcukwikFBOHandlerDetailDataTable ConvertTable(DataSheetCollection supportedSheetCollection, DataTable uploadedSheetTable)
        {
            AcukwikDataSet.AcukwikFBOHandlerDetailDataTable result = null;
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
            AcukwikFBOHandlerDetailTableAdapter adapter = new AcukwikFBOHandlerDetailTableAdapter();
            adapter.Connection = new SqlConnection(ExecutionHelper.GetConnectionString());
            adapter.DeleteData();

            //Bulk insert the new records
            using (SqlBulkCopy s = new SqlBulkCopy(ExecutionHelper.GetConnectionString()))
            {
                s.DestinationTableName = "AcukwikFBOHandlerDetail";
                s.BatchSize = 5000;

                s.ColumnMappings.Add("Airport_ID", "Airport_ID");
                s.ColumnMappings.Add("Handler_ID", "Handler_ID");
                s.ColumnMappings.Add("HandlerLongName", "HandlerLongName");
                s.ColumnMappings.Add("HandlerType", "HandlerType");
                s.ColumnMappings.Add("HandlerTelephone", "HandlerTelephone");
                s.ColumnMappings.Add("HandlerFax", "HandlerFax");
                s.ColumnMappings.Add("HandlerTollFree", "HandlerTollFree");
                s.ColumnMappings.Add("HandlerFreq", "HandlerFreq");
                s.ColumnMappings.Add("HandlerFuelBrand", "HandlerFuelBrand");
                s.ColumnMappings.Add("HandlerFuelBrand2", "HandlerFuelBrand2");
                s.ColumnMappings.Add("HandlerFuelSupply", "HandlerFuelSupply");
                s.ColumnMappings.Add("HandlerLocationOnField", "HandlerLocationOnField");
                s.ColumnMappings.Add("MultiService", "MultiService");
                s.ColumnMappings.Add("Avcard", "Avcard");
                s.ColumnMappings.Add("Acukwik_ID", "Acukwik_ID");

                s.WriteToServer(convertedTable);
                s.Close();
            }
        }

        private AcukwikDataSet.AcukwikFBOHandlerDetailDataTable AttemptToConvertFormat(DataSheet supportedDataSheet, DataTable uploadedSheetTable)
        {
            AcukwikDataSet.AcukwikFBOHandlerDetailDataTable result = new AcukwikDataSet.AcukwikFBOHandlerDetailDataTable();
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
