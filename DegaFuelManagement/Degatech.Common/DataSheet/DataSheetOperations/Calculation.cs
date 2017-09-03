using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using Degatech.Common.DataSheet.DataSheetParsing;

namespace Degatech.Common.DataSheet.DataSheetOperations
{
    public class Calculation
    {
        #region Enums
        public enum CalculationType
        {
            None = 0,
            Addition = 1,
            Subtraction = 2,
            Multiplication = 3,
            Division = 4,
            Round = 5
        }

        public enum CalculationCondition
        {
            None = 0,
            GreaterThan100 = 1
        }

        public enum CalculateByTypes
        {
            None = 0,
            ByAmount = 1,
            ByOtherColumn = 2
        }
        #endregion

        #region Members
        private CalculationType _Type = CalculationType.None;
        private double _Amount = 0.0;
        private CalculationCondition _Condition = CalculationCondition.None;
        #endregion

        #region Properties
        public CalculationType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        public CalculationCondition Condition
        {
            get { return _Condition; }
            set { _Condition = value; }
        }

        public CalculateByTypes CalculateBy { get; set; }

        public DataSheetColumn Column { get; set; }
        #endregion

        #region Public Methods
        public double PerformCalculation(double columnValue, DataRow row, DataSheetSettings settings)
        {
            if (IsConditionMet(columnValue) && IsValidForCalculation(row))
            {
                PrepareAmountFromOtherColumn(row, settings);
                switch (_Type)
                {
                    case CalculationType.Addition:
                        columnValue = columnValue + _Amount;
                        break;
                    case CalculationType.Division:
                        columnValue = columnValue/_Amount;
                        break;
                    case CalculationType.Multiplication:
                        columnValue = columnValue*_Amount;
                        break;
                    case CalculationType.Subtraction:
                        columnValue = columnValue - _Amount;
                        break;
                    case CalculationType.Round:
                        columnValue = Math.Round(columnValue, int.Parse(Math.Ceiling(_Amount).ToString()));
                        break;
                }
            }
            return columnValue;
        }

        private void PrepareAmountFromOtherColumn(DataRow row, DataSheetSettings settings)
        {
            if (Column == null)
                return;
            _Amount = double.Parse(Column.FormattedColumnValue(row, settings));
        }
        #endregion

        #region Private Methods
        private bool IsConditionMet(double columnValue)
        {
            switch (_Condition)
            {
                    case CalculationCondition.GreaterThan100:
                    return (columnValue > 99.9);
            }
            return true;
        }

        private bool IsValidForCalculation(DataRow row)
        {
            if (Column == null)
                return true;
            return Column.IsValidRow(row);
        }
        #endregion
    }

    public class CalculationCollection : List<Calculation>
    {
        #region Public Methods
        public void Load(XmlNode calculationsNode)
        {
            Clear();
            foreach (XmlNode calculationNode in calculationsNode.ChildNodes)
            {
                Calculation calculation = new Calculation();
                calculation.Type = (Calculation.CalculationType)Enum.Parse(typeof(Calculation.CalculationType), calculationNode.Attributes["Type"].Value);

                if (calculationNode["Amount"] != null && Degatech.Utilities.Data.Comparers.StringComparer.IsNumeric(calculationNode["Amount"].InnerText))
                    calculation.Amount = double.Parse(calculationNode["Amount"].InnerText);
                if (calculationNode["Column"] != null)
                    calculation.Column = new DataSheetColumn(calculationNode["Column"]);
                if (calculationNode.Attributes["Condition"] != null)
                    calculation.Condition = (Calculation.CalculationCondition)Enum.Parse(typeof(Calculation.CalculationCondition), calculationNode.Attributes["Condition"].Value);
                Add(calculation);
            }
        }

        public double PerformCalculations(double columnValue, DataRow row, DataSheetSettings settings)
        {
            foreach (Calculation calculation in this)
                columnValue = calculation.PerformCalculation(columnValue, row, settings);
            return columnValue;
        }
        #endregion
    }
}
