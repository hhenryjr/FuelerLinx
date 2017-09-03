using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using Degatech.Common.DataSheet.DataSheetOperations;

namespace Degatech.Common.DataSheet.DataSheetParsing
{
    public class DataSheetColumn : BaseClass
    {
        #region Enums
        #endregion

        #region Members
        private string _Name = "";
        private int _Index = -1;
        private string _PreText = "";
        private string _PostText = "";
        private CalculationCollection _Calculations = new CalculationCollection();
        private FilterCollection _Filters = new FilterCollection();
        private string _StaticValue = "";
        private bool _IsNumeric;
        private string _DateParse = "";
        private StringOperationCollection _StringOperations = new StringOperationCollection();
        #endregion

        #region Properties
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        public string ColumnLetter { get; set; } = string.Empty;

        public string PreText
        {
            get { return _PreText; }
            set { _PreText = value; }
        }

        public string PostText
        {
            get { return _PostText; }
            set { _PostText = value; }
        }

        public CalculationCollection Calculations
        {
            get { return _Calculations; }
            set { _Calculations = value; }
        }

        public FilterCollection Filters
        {
            get { return _Filters; }
            set { _Filters = value; }
        }

        public string StaticValue
        {
            get { return _StaticValue; }
            set { _StaticValue = value; }
        }

        public bool IsNumeric
        {
            get { return _IsNumeric; }
            set { _IsNumeric = value; }
        }

        public string DateParse
        {
            get { return _DateParse; }
            set { _DateParse = value; }
        }

        public StringOperationCollection StringOperations
        {
            get { return _StringOperations; }
            set { _StringOperations = value; }
        }

        public ConditionalOperation ConditionalOperation { get; set; } = new ConditionalOperation();
        public bool IsJulianDate { get; set; }
        public bool RequiresWeightConversion { get; set; }
        public int Group { get; set; }
        #endregion

        #region Constructors
        public DataSheetColumn()
        {

        }

        public DataSheetColumn(XmlNode columnNode)
        {
            Load(columnNode);
        }
        #endregion

        #region Public Methods
        public void Load(XmlNode columnNode)
        {
            SetProperties(columnNode);
            if (Index < 0 && !string.IsNullOrEmpty(ColumnLetter))
                Index = Degatech.Utilities.IO.File.Convertors.Excel.ExcelColumnNameToNumber(ColumnLetter) - 1;
            else if (Index > -1 && string.IsNullOrEmpty(ColumnLetter))
                ColumnLetter = Degatech.Utilities.IO.File.Convertors.Excel.GetExcelColumnNameFromNumber(Index + 1);

            if (columnNode.Attributes["Name"] != null)
                _Name = columnNode.Attributes["Name"].Value;

            //Load all filters
            if (columnNode["Filters"] != null)
                _Filters.Load(columnNode["Filters"]);

            //Load all calculations
            if (columnNode["Calculations"] != null)
                _Calculations.Load(columnNode["Calculations"]);

            //Load all string operations
            if (columnNode["StringOperations"] != null)
                _StringOperations.Load(columnNode["StringOperations"]);

            //Load conditional operation
            if (columnNode["ConditionalOperation"] != null)
                ConditionalOperation.Load(columnNode["ConditionalOperation"]);
        }

        public bool IsValidRow(DataRow fuelSheetRow)
        {
            return _Filters.IsAllowed(PreFormattedColumnValue(fuelSheetRow));
        }

        public bool IsPartOfCurrentGroup(DataSheetSettings settings)
        {
            return (Group == 0 || Group == settings.MultiRecordsPerRowOptions.CurrentGroup);
        }

        public string PreFormattedColumnValue(DataRow dataSheetRow)
        {
            string result = _StaticValue;
            if (_Index > -1)
                result = dataSheetRow[_Index].ToString().Trim();
            return result;
        }

        public string FormattedColumnValue(DataRow dataSheetRow, DataSheetSettings settings)
        {
            string result = _StaticValue;
            if (_Index > -1)
                result = dataSheetRow[_Index].ToString().Trim();
            if (_StringOperations.Count > 0)
                result = _StringOperations.PerformOperations(result);
            if (IsNumeric && !Utilities.Data.Utilities.IsNumeric(result))
                result = "0";
            if (_Calculations.Count > 0)
                result = _Calculations.PerformCalculations(double.Parse(result), dataSheetRow, settings).ToString();
            if (ConditionalOperation.ConditionType != ConditionalOperation.ConditionTypes.None)
                result = ConditionalOperation.PerformOperation(result, dataSheetRow);
            result = HandleDateParsing(result);
            if (result != null)
                result = _PreText + result + _PostText;
            return result;
        }
        #endregion

        #region Private Methods

        private string HandleDateParsing(string result)
        {
            try
            {
                if (!string.IsNullOrEmpty(_DateParse))
                    if (string.IsNullOrEmpty(result))
                        result = Utilities.Data.PropertyGetter.DatabaseDateTimeMinValue().ToShortDateString();
                    else
                        result =
                            DateTime.Parse(result, new System.Globalization.CultureInfo(_DateParse, false))
                                .ToShortDateString();
                if (IsJulianDate)
                    if (string.IsNullOrEmpty(result))
                        result = Utilities.Data.PropertyGetter.DatabaseDateTimeMinValue().ToShortDateString();
                    else
                        result = Utilities.Data.UnitConvertors.DateConvertor.FromJulianDate(int.Parse(result)).ToShortDateString();
                return result;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }

    public class DataSheetColumnCollection : List<DataSheetColumn>
    {
        #region Members
        private DataRow _LastValidRow = null;
        #endregion

        #region Public Methods
        public void Load(XmlNode dataSheetNode)
        {
            this.Clear();
            XmlNodeList columnNodes = dataSheetNode.SelectNodes("Column");
            if ((columnNodes == null || columnNodes.Count == 0) && dataSheetNode["Columns"] != null)
                columnNodes = dataSheetNode.SelectNodes("Columns/Column");
            if (columnNodes == null)
                return;
            foreach (XmlNode columnNode in columnNodes)
            {
                var column = new DataSheetColumn();
                column.Load(columnNode);
                this.Add(column);
            }
        }

        public DataRow NewRow(DataTable formattedTable, DataRow uploadedSheetRow, DataSheetSettings settings)
        {
            DataRow row = formattedTable.NewRow();
            foreach (DataSheetColumn column in this)
                AddColumnValueToRow(row, column, uploadedSheetRow, settings);

            //Set any static section-values that need to apply to each row
            settings.DataSheetSection.ApplySectionValues(formattedTable, row, settings);

            return row;
        }

        public void AppendToRow(DataRow existingRow, DataRow uploadedSheetRow, DataSheetSettings settings)
        {
            if (!settings.MultiRowsPerRecordOptions.Columns.IsValidRow(uploadedSheetRow, settings))
                return;
            foreach (DataSheetColumn column in this)
                AddColumnValueToRow(existingRow, column, uploadedSheetRow, settings);
        }

        public bool IsValidRow(DataRow dataSheetRow, DataSheetSettings settings)
        {
            foreach (DataSheetColumn dataSheetColumn in this)
            {
                if (!dataSheetColumn.IsPartOfCurrentGroup(settings))
                    continue;
                if (!dataSheetColumn.IsValidRow(dataSheetRow))
                    return false;
            }
            return true;
        }

        public void SetPropertiesOfObject(object objectToSet, DataRow uploadedSheetRow, DataSheetSettings settings)
        {
            foreach (DataSheetColumn dataSheetColumn in this)
                Utilities.Data.PropertySetter.SetPropertyValue(objectToSet, dataSheetColumn.Name,
                                                dataSheetColumn.FormattedColumnValue(uploadedSheetRow, settings));
        }

        public int IndexOfColumnName(string columnName)
        {
            int result = 0;
            List<DataSheetColumn>.Enumerator e = this.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.Name.ToLower() == columnName.ToLower())
                {
                    result = e.Current.Index;
                    break;
                }
            }
            return result;
        }
        #endregion

        #region Private Methods
        private void AddColumnValueToRow(DataRow rowToPopulate, DataSheetColumn column, DataRow uploadedSheetRow, DataSheetSettings settings)
        {
            if (string.IsNullOrEmpty(column.Name))
                return;
            if (!column.IsPartOfCurrentGroup(settings))
                return;
            string columnValue = column.FormattedColumnValue(uploadedSheetRow, settings);
            if (columnValue == null)
                return;
            if (column.IsNumeric && rowToPopulate.Table.Columns[column.Name].DataType == typeof(System.Int32))
                rowToPopulate[column.Name] = (int)double.Parse(rowToPopulate[column.Name].ToString() == "" ? "0" : rowToPopulate[column.Name].ToString()) + (int)double.Parse(columnValue);
            else if (column.IsNumeric && !string.IsNullOrEmpty(rowToPopulate[column.Name].ToString()))
                rowToPopulate[column.Name] = double.Parse(rowToPopulate[column.Name].ToString()) + double.Parse(columnValue);
            else if (rowToPopulate.Table.Columns[column.Name].DataType == typeof(DateTime))
                rowToPopulate[column.Name] = rowToPopulate[column.Name].ToString() == "" ? columnValue : DateTime.Parse(rowToPopulate[column.Name].ToString()).ToShortDateString() + " " + columnValue;
            else
                rowToPopulate[column.Name] = rowToPopulate[column.Name] + column.FormattedColumnValue(uploadedSheetRow, settings);
        }
        #endregion
    }
}
