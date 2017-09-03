using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Degatech.Common.DataSheet.DataSheetParsing
{
    public class DataSheet : BaseClass
    {
        #region Properties
        public string Name { get; set; } = string.Empty;
        public DataSheetColumnCollection ColumnCollection { get; set; } = new DataSheetColumnCollection();
        public DataSheetSettings Settings { get; set; } = new DataSheetSettings();
        public int FailedRowCount = 0;
        #endregion

        #region Public Methods
        public void LoadFromNode(XmlNode dataSheetNode)
        {
            SetProperties(dataSheetNode);
            ColumnCollection.Load(dataSheetNode);
            if (dataSheetNode["Settings"] != null)
                Settings.LoadFromNode(dataSheetNode["Settings"]);
        }

        public bool PopulateRowsFromData(DataTable tableToPopulate, DataTable tableToParse)
        {
            int maxFails = tableToParse.Rows.Count / 2;
            foreach (DataRow uploadedRow in tableToParse.Rows)
            {
                //First check to see if we started a new 'section' for sectioned sheets
                Settings.DataSheetSection.CheckForNewSection(uploadedRow, Settings);

                for (Settings.MultiRecordsPerRowOptions.CurrentGroup = 1;
                    Settings.MultiRecordsPerRowOptions.CurrentGroup <= Settings.MultiRecordsPerRowOptions.GroupCount;
                    Settings.MultiRecordsPerRowOptions.CurrentGroup++)
                {
                    if (!PopulateFromDataRow(tableToPopulate, uploadedRow, maxFails)) return false;
                }
            }
            return true;
        }

        public bool IsValidRow(DataRow row)
        {
            return ColumnCollection.IsValidRow(row, Settings);
        }

        public DataRow NewRow(DataTable formattedTable, DataRow uploadedSheetRow)
        {
            return ColumnCollection.NewRow(formattedTable, uploadedSheetRow, Settings);
        }

        public string GetAsXml()
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(DataSheet));
            using (StringWriter sww = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, this);
                return sww.ToString();
            }
        }
        #endregion

        #region Private Methods
        private bool PopulateFromDataRow(DataTable tableToPopulate, DataRow uploadedRow, int maxFails)
        {
            //Add a new row to our result if the row is valid
            if (IsValidRow(uploadedRow))
                tableToPopulate.Rows.Add(NewRow(tableToPopulate, uploadedRow));
            //Append to an existing row if we have multiple rows per record
            else if (Settings.MultiRowsPerRecordOptions.MultipleRowsPerRecord && tableToPopulate.Rows.Count > 0)
                Settings.MultiRowsPerRecordOptions.Columns.AppendToRow(tableToPopulate.Rows[tableToPopulate.Rows.Count - 1], uploadedRow, Settings);
            //Add to our failed row count if it was invalid for the first group of tries
            else if (Settings.MultiRecordsPerRowOptions.CurrentGroup == 1)
                FailedRowCount++;
            //Quit parsing the sheet if it's obviously not compatible with the current template
            if (FailedRowCount > maxFails && tableToPopulate.Rows.Count < 5)
                return false;
            return true;
        }
        #endregion
    }

    public class DataSheetCollection : List<DataSheet>
    {
        #region Public Methods
        public void LoadFromFile(XmlDocument document, string xPathForSheets)
        {
            if (document == null)
                return;
            LoadFromNodeList(document.SelectNodes(xPathForSheets));
        }

        public void LoadFromNodeList(XmlNodeList nodeList)
        {
            if (nodeList == null)
                return;
            foreach (XmlNode dataSheetNode in nodeList)
            {
                DataSheet sheet = new DataSheet();
                sheet.LoadFromNode(dataSheetNode);
                Add(sheet);
            }
        }
        #endregion
    }
}
