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
    class SupplierDetailData
    {
        #region Public Methods
        public bool IsConvertSuccessful(DataSheetCollection supportedSheetCollection, DataTable uploadedSheetTable)
        {
            AcukwikDataSet.AcukwikSupplierDetailDataTable convertedTable = ConvertTable(supportedSheetCollection, uploadedSheetTable);
            if (convertedTable == null) return false;
            UploadDatabaseRecords(convertedTable);
            if (convertedTable.Rows.Count == 0) return false;
            return true;
        }
        #endregion

        #region Private Methods
        private AcukwikDataSet.AcukwikSupplierDetailDataTable ConvertTable(DataSheetCollection supportedSheetCollection, DataTable uploadedSheetTable)
        {
            AcukwikDataSet.AcukwikSupplierDetailDataTable result = null;
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
            AcukwikSupplierDetailTableAdapter adapter = new AcukwikSupplierDetailTableAdapter();
            adapter.Connection = new SqlConnection(ExecutionHelper.GetConnectionString());
            adapter.DeleteData();

            //Bulk insert the new records
            using (SqlBulkCopy s = new SqlBulkCopy(ExecutionHelper.GetConnectionString()))
            {
                s.DestinationTableName = "AcukwikSupplierDetail";
                s.BatchSize = 5000;

                s.ColumnMappings.Add("Airport_ID", "Airport_ID");
                s.ColumnMappings.Add("Supplier_ID", "Supplier_ID");
                s.ColumnMappings.Add("SupplierLongName", "SupplierLongName");
                s.ColumnMappings.Add("SupplierType", "SupplierType");
                s.ColumnMappings.Add("SupplierTelephone", "SupplierTelephone");
                s.ColumnMappings.Add("SupplierFax", "SupplierFax");
                s.ColumnMappings.Add("SupplierTollFree", "SupplierTollFree");
                s.ColumnMappings.Add("SupplierFreq", "SupplierFreq");
                s.ColumnMappings.Add("Acukwik_ID", "Acukwik_ID");

                s.WriteToServer(convertedTable);
                s.Close();
            }
        }

        private AcukwikDataSet.AcukwikSupplierDetailDataTable AttemptToConvertFormat(DataSheet supportedDataSheet, DataTable uploadedSheetTable)
        {
            AcukwikDataSet.AcukwikSupplierDetailDataTable result = new AcukwikDataSet.AcukwikSupplierDetailDataTable();
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
