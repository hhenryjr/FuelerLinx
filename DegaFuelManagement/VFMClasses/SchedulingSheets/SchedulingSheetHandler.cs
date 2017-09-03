using Degatech.Common.DataSheet.DataSheetParsing;
using Degatech.Utilities.Data;
using Degatech.Utilities.Data.Parsers;
using Degatech.Utilities.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFMClasses.DataSets;
using VFMClasses.DataSets.SchedulingDataSetTableAdapters;
using VFMClasses.SchedulingSheets;

namespace VFMClasses.ScheduleSheets
{
    public class SchedulingSheetHandler
    {
        #region Private Members
        private int _AdminClientId;
        private int _CustClientId;
        private int _SchedulingId;
        private string _FileName;
        private DataSheetCollection _SupportedSheetCollection;
        private DataTable _UploadedSheetTable;
        #endregion

        #region Public Methods
        public bool Process(string fileName, int adminClientId, int schedulingId, int custClientId)
        {
            _FileName = fileName;
            _SchedulingId = schedulingId;
            _AdminClientId = adminClientId;
            _CustClientId = custClientId;
            _SupportedSheetCollection = SchedulingTemplateLoader.GetSchedulingDataSheetCollection(_SchedulingId);
            LoadFileIntoDataTable();
            SchedulingDataSet.SchedulingImportsDataTable convertedTable = ConvertUploadedDataTable();
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
        private SchedulingDataSet.SchedulingImportsDataTable ConvertUploadedDataTable()
        {
            SchedulingDataSet.SchedulingImportsDataTable result = null;
            foreach (DataSheet supportedDataSheet in _SupportedSheetCollection)
            {
                result = AttemptToConvertFormat(supportedDataSheet);
                if (result != null && result.Rows.Count > 0)
                    return result;
            }
            return null;
        }

        private SchedulingDataSet.SchedulingImportsDataTable AttemptToConvertFormat(DataSheet supportedDataSheet)
        {
            SchedulingDataSet.SchedulingImportsDataTable result = new SchedulingDataSet.SchedulingImportsDataTable();
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

        private void PrepareConvertedTableForDatabaseUpdate(SchedulingDataSet.SchedulingImportsDataTable convertedTable)
        {
            foreach (SchedulingDataSet.SchedulingImportsRow row in convertedTable.Rows)
            {
                row.AdminClientID = _AdminClientId;
                row.CustClientID = _CustClientId;
                row.ImportDate = DateTime.Now;
            }
        }

        public void UploadDatabaseRecords(DataTable convertedTable)
        {
            //Delete all old records for that supplier/admin
            SchedulingImportsTableAdapter adapter = new SchedulingImportsTableAdapter();
            adapter.Connection = new SqlConnection(ExecutionHelper.GetConnectionString());
            //adapter.DeleteAll(_SchedulingId, _AdminClientId, _CustClientId);

            //Bulk insert the new records
            using (SqlBulkCopy s = new SqlBulkCopy(ExecutionHelper.GetConnectionString()))
            {
                s.DestinationTableName = "SchedulingImports";
                s.BatchSize = 5000;

                s.ColumnMappings.Add("SchedulingPlatform", "SchedulingPlatform");
                s.ColumnMappings.Add("AdminClientID", "AdminClientID");
                s.ColumnMappings.Add("CustClientID", "CustClientID");
                s.ColumnMappings.Add("TripNumber", "TripNumber");
                s.ColumnMappings.Add("TailNumber", "TailNumber");
                s.ColumnMappings.Add("ICAO", "ICAO");
                s.ColumnMappings.Add("FBO", "FBO");
                s.ColumnMappings.Add("Arrival", "Arrival");
                s.ColumnMappings.Add("ImportDate", "ImportDate");

                s.WriteToServer(convertedTable);
                s.Close();
            }
        }
        #endregion
    }
}
