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
    class AirportsData
    {
        #region Public Methods
        public bool IsConvertSuccessful(DataSheetCollection supportedSheetCollection, DataTable uploadedSheetTable)
        {
            AcukwikDataSet.AcukwikAirportsDataTable convertedTable = ConvertTable(supportedSheetCollection, uploadedSheetTable);
            if (convertedTable == null) return false;
            UploadDatabaseRecords(convertedTable);
            if (convertedTable.Rows.Count == 0) return false;
            return true;
        }
        #endregion

        #region Private Methods
        private AcukwikDataSet.AcukwikAirportsDataTable ConvertTable(DataSheetCollection supportedSheetCollection, DataTable uploadedSheetTable)
        {
            AcukwikDataSet.AcukwikAirportsDataTable result = null;
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
            AcukwikAirportsTableAdapter adapter = new AcukwikAirportsTableAdapter();
            adapter.Connection = new SqlConnection(ExecutionHelper.GetConnectionString());
            adapter.DeleteData();

            //Bulk insert the new records
            using (SqlBulkCopy s = new SqlBulkCopy(ExecutionHelper.GetConnectionString()))
            {
                s.DestinationTableName = "AcukwikAirports";
                s.BatchSize = 5000;

                s.ColumnMappings.Add("Airport_ID", "Airport_ID");
                s.ColumnMappings.Add("ICAO", "ICAO");
                s.ColumnMappings.Add("IATA", "IATA");
                s.ColumnMappings.Add("FAA", "FAA");
                s.ColumnMappings.Add("FullAirportName", "FullAirportName");
                s.ColumnMappings.Add("AirportCity", "AirportCity");
                s.ColumnMappings.Add("State/Subdivision", "State/Subdivision");
                s.ColumnMappings.Add("Country", "Country");
                s.ColumnMappings.Add("AirportType", "AirportType");
                s.ColumnMappings.Add("Distance_From_City", "Distance_From_City");
                s.ColumnMappings.Add("Latitude", "Latitude");
                s.ColumnMappings.Add("Longitude", "Longitude");
                s.ColumnMappings.Add("Elevation", "Elevation");
                s.ColumnMappings.Add("Variation", "Variation");
                s.ColumnMappings.Add("IntlTimeZone", "IntlTimeZone");
                s.ColumnMappings.Add("DaylightSavingsYN", "DaylightSavingsYN");
                s.ColumnMappings.Add("FuelType", "FuelType");
                s.ColumnMappings.Add("AirportOfEntry", "AirportOfEntry");
                s.ColumnMappings.Add("Customs", "Customs");
                s.ColumnMappings.Add("HandlingMandatory", "HandlingMandatory");
                s.ColumnMappings.Add("SlotsRequired", "SlotsRequired");
                s.ColumnMappings.Add("Open24Hours", "Open24Hours");
                s.ColumnMappings.Add("ControlTowerHours", "ControlTowerHours");
                s.ColumnMappings.Add("ApproachList", "ApproachList");
                s.ColumnMappings.Add("PrimaryRunwayID", "PrimaryRunwayID");
                s.ColumnMappings.Add("RunwayLength", "RunwayLength");
                s.ColumnMappings.Add("RunwayWidth", "RunwayWidth");
                s.ColumnMappings.Add("Lighting", "Lighting");
                s.ColumnMappings.Add("AirportNameShort", "AirportNameShort");

                s.WriteToServer(convertedTable);
                s.Close();
            }
        }

        private AcukwikDataSet.AcukwikAirportsDataTable AttemptToConvertFormat(DataSheet supportedDataSheet, DataTable uploadedSheetTable)
        {
            AcukwikDataSet.AcukwikAirportsDataTable result = new AcukwikDataSet.AcukwikAirportsDataTable();
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
