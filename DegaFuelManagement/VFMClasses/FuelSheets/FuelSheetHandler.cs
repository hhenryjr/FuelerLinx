using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Degatech.Common.DataSheet.DataSheetParsing;
using Degatech.Utilities.Data;
using Degatech.Utilities.Data.Parsers;
using Degatech.Utilities.SQL;
using VFMClasses.DataSets;
using VFMClasses.DataSets.FuelManagementPricesTableAdapters;

namespace VFMClasses.FuelSheets
{
    public class FuelSheetHandler
    {
        #region Private Members
        private int _AdminClientId;
        private int _SupplierId;
        private string _FileName;
        private DataSheetCollection _SuppoortedSheetCollection;
        private DataTable _UploadedSheetTable;
        #endregion

        #region Public Methods
        public bool Process(string fileName, Users user, Suppliers supplier)
        {
            _FileName = fileName;
            _SupplierId = supplier.Id;
            _AdminClientId = user.Client.Id;
            _SuppoortedSheetCollection = TemplateLoader.GetSupplierDataSheetCollection(user.Client.Name, supplier.SupplierName);
            LoadFileIntoDataTable();
            FuelManagementPrices.SupplierFuelsPricesDataTable convertedTable = ConvertUploadedDataTable();
            if (convertedTable == null)
                return false;
            PrepareConvertedTableForDatabaseUpdate(convertedTable);
            UploadDatabaseRecords(convertedTable);
            if (convertedTable.Rows.Count == 0)
                return false;
            return true;
        }
        #endregion

        #region Private Methods
        private FuelManagementPrices.SupplierFuelsPricesDataTable ConvertUploadedDataTable()
        {
            FuelManagementPrices.SupplierFuelsPricesDataTable result = null;
            foreach (DataSheet supportedDataSheet in _SuppoortedSheetCollection)
            {
                result = AttemptToConvertFormat(supportedDataSheet);
                if (result != null && result.Rows.Count > 0)
                    return result;
            }
            return null;
        }

        private FuelManagementPrices.SupplierFuelsPricesDataTable AttemptToConvertFormat(DataSheet supportedDataSheet)
        {
            FuelManagementPrices.SupplierFuelsPricesDataTable result = new FuelManagementPrices.SupplierFuelsPricesDataTable();
            try
            {
                if (!supportedDataSheet.PopulateRowsFromData(result, _UploadedSheetTable))
                    return null;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
            return result;
        }

        private void LoadFileIntoDataTable()
        {
            if (File.Exists(_FileName))
            {
                string fileType = _FileName.Substring(_FileName.LastIndexOf(".") + 1);
                switch (fileType.ToLower())
                {
                    case "csv":
                        string csvText = Utilities.readFile(_FileName);
                        _UploadedSheetTable = CsvParser.Parse(csvText);
                        break;
                    case "xls":
                    case "xlsx":
                    case "xlsm":
                        _UploadedSheetTable = ExcelParser.Parse(_FileName);
                        break;
                    case "pdf":
                        _UploadedSheetTable = PDFParser.Parse(_FileName);
                        break;
                    default:
                        break;
                }
            }
            if (_UploadedSheetTable == null)
                throw new Exception("There was an issue converting the file to a data table.");
        }

        private void PrepareConvertedTableForDatabaseUpdate(FuelManagementPrices.SupplierFuelsPricesDataTable convertedTable)
        {
            //Update the records with the appropriate admin/supplier
            foreach (FuelManagementPrices.SupplierFuelsPricesRow row in convertedTable.Rows)
            {
                row.AdminClientId = _AdminClientId;
                row.SupplierId = _SupplierId;
            }
        }

        public void UploadDatabaseRecords(DataTable convertedTable)
        {
            //Delete all old records for that supplier/admin
            SupplierFuelsPricesTableAdapter adapter = new SupplierFuelsPricesTableAdapter();
            adapter.Connection = new SqlConnection(ExecutionHelper.GetConnectionString());
            adapter.DeleteAll(_AdminClientId, _SupplierId);

            //Bulk insert the new records
            using (SqlBulkCopy s = new SqlBulkCopy(ExecutionHelper.GetConnectionString()))
            {
                s.DestinationTableName = "SupplierFuelsPrices";
                s.BatchSize = 5000;

                s.ColumnMappings.Add("Vendor", "Vendor");
                s.ColumnMappings.Add("IATA", "IATA");
                s.ColumnMappings.Add("ICAO", "ICAO");
                s.ColumnMappings.Add("FBOName", "FBOName");
                s.ColumnMappings.Add("Min", "Min");
                s.ColumnMappings.Add("Max", "Max");
                s.ColumnMappings.Add("TotalWithTax", "TotalWithTax");
                s.ColumnMappings.Add("Expires", "Expires");
                s.ColumnMappings.Add("Product", "Product");
                s.ColumnMappings.Add("Notes", "Notes");
                s.ColumnMappings.Add("AdminClientId", "AdminClientID");
                s.ColumnMappings.Add("SupplierId", "SupplierID");
                s.ColumnMappings.Add("EffectiveDate", "EffectiveDate");
                s.ColumnMappings.Add("VendorEmail", "VendorEmail");

                s.WriteToServer(convertedTable);
                s.Close();
            }

            adapter.UpdateSupplierFuelsPricesMax(_AdminClientId, _SupplierId);
        }
        #endregion
    }
}
