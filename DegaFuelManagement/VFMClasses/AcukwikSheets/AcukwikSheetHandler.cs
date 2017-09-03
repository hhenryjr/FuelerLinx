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
using VFMClasses.AcukwikSheets.AcukwikTableConverter;
using VFMClasses.DataSets;
using VFMClasses.DataSets.AcukwikDataSetTableAdapters;

namespace VFMClasses.AcukwikSheets
{
    public class AcukwikSheetHandler
    {
        #region Private Members
        private string _AcukwikName;
        private string _FileName;
        private DataSheetCollection _SuppoortedSheetCollection;
        private DataTable _UploadedSheetTable;
        #endregion

        #region Public Methods
        public bool Process(string fileName, string acukwikName)
        {
            _FileName = fileName;
            _AcukwikName = acukwikName;
            _SuppoortedSheetCollection = AcukwikTemplateLoader.GetAcukwikDataSheetCollection(_AcukwikName);
            LoadFileIntoDataTable();
            if (ConvertAcukwikTable(_AcukwikName)) return true;
            else return false;
        }
        #endregion

        #region Private Methods
        private bool ConvertAcukwikTable(string acukwikName)
        {
            switch (acukwikName.ToString())
            {
                case "Airports":
                    AirportsData airportData = new AirportsData();
                    return airportData.IsConvertSuccessful(_SuppoortedSheetCollection, _UploadedSheetTable);
                case "Countries":
                    CountriesData countryData = new CountriesData();
                    return countryData.IsConvertSuccessful(_SuppoortedSheetCollection, _UploadedSheetTable);
                case "FBOHandlerDetail":
                    FBOHandlerDetailData handlerData = new FBOHandlerDetailData();
                    return handlerData.IsConvertSuccessful(_SuppoortedSheetCollection, _UploadedSheetTable);
                case "SubdivisionStates":
                    SubdivisionStatesData statesData = new SubdivisionStatesData();
                    return statesData.IsConvertSuccessful(_SuppoortedSheetCollection, _UploadedSheetTable);
                case "SupplierDetail":
                    SupplierDetailData supplierData = new SupplierDetailData();
                    return supplierData.IsConvertSuccessful(_SuppoortedSheetCollection, _UploadedSheetTable);
                default:
                    return false;
            }
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
        #endregion
    }
}
