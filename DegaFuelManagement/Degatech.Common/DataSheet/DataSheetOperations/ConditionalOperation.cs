using System;
using System.Data;
using System.Xml;

namespace Degatech.Common.DataSheet.DataSheetOperations
{
    public class ConditionalOperation : BaseClass
    {
        #region Enums
        public enum ConditionTypes
        {
            None = 0,
            If = 1
        }

        public enum ComparisonTypes
        {
            EqualTo = 0,
            GreaterThan = 1,
            LessThan = 2,
            GreaterThanOrEqualTo = 3,
            LessThanOrEqualTo = 4,
            NotEqualTo = 5
        }

        public enum ComparisonValueSource
        {
            NotSpecified = 0,
            Column = 1,
            StaticValue = 2
        }
        #endregion

        #region Properties
        public ConditionTypes ConditionType { get; set; }
        public ComparisonTypes ComparisonType { get; set; }
        public int AnalyzeIndex { get; set; } = -1;
        public string CompareWith { get; set; } = string.Empty;
        public string TrueValue { get; set; } = string.Empty;
        public int TrueValueIndex { get; set; } = -1;
        public string FalseValue { get; set; } = string.Empty;
        public int FalseValueIndex { get; set; } = -1;
        public ComparisonValueSource TrueValueSource { get; set; } = ComparisonValueSource.Column;
        public ComparisonValueSource FalseValueSource { get; set; } = ComparisonValueSource.Column;
        #endregion

        #region Public Methods
        public void Load(XmlNode conditionalOperationNode)
        {
            ComparisonType =
                (ConditionalOperation.ComparisonTypes)
                    Enum.Parse(typeof(ConditionalOperation.ComparisonTypes), conditionalOperationNode.Attributes["ComparisonType"].Value);
            ConditionType =
                (ConditionalOperation.ConditionTypes)
                    Enum.Parse(typeof(ConditionalOperation.ConditionTypes), conditionalOperationNode.Attributes["ConditionType"].Value);
            SetProperties(conditionalOperationNode);
        }

        public string PerformOperation(DataRow row)
        {
            switch (ConditionType)
            {
                case ConditionTypes.If:
                    string valueToAnalyze = row[AnalyzeIndex].ToString();
                    if (PerformComparison(valueToAnalyze, CompareWith))
                        return GetTrueValue(row);
                    return GetFalseValue(row);
            }
            return "";
        }

        public string PerformOperation(string result, DataRow row)
        {
            switch (ConditionType)
            {
                case ConditionTypes.If:
                    string valueToAnalyze = result;
                    if (PerformComparison(valueToAnalyze, CompareWith))
                        return result;
                    return GetFalseValue(row);
            }
            return "";
        }
        #endregion

        #region Private Methods
        private string GetTrueValue(DataRow row)
        {
            if (TrueValueIndex > -1 && (TrueValueSource == ComparisonValueSource.Column || TrueValueSource == ComparisonValueSource.Column))
                return row[TrueValueIndex].ToString();
            return TrueValue;
        }

        private string GetFalseValue(DataRow row)
        {
            if (FalseValueIndex > -1 && (FalseValueSource == ComparisonValueSource.Column || FalseValueSource == ComparisonValueSource.NotSpecified))
                return row[FalseValueIndex].ToString();
            return FalseValue;
        }

        private bool PerformComparison(string valueToAnalyze, string compareWith)
        {
            switch (ComparisonType)
            {
                case ComparisonTypes.EqualTo:
                    return (valueToAnalyze == compareWith);
                    case ComparisonTypes.NotEqualTo:
                    return (valueToAnalyze != compareWith);
                case ComparisonTypes.GreaterThan:
                    return (double.Parse(valueToAnalyze) > double.Parse(compareWith));
                case ComparisonTypes.GreaterThanOrEqualTo:
                    return (double.Parse(valueToAnalyze) >= double.Parse(compareWith));
                case ComparisonTypes.LessThan:
                    return (double.Parse(valueToAnalyze) < double.Parse(compareWith));
                case ComparisonTypes.LessThanOrEqualTo:
                    return (double.Parse(valueToAnalyze) <= double.Parse(compareWith));
            }
            return true;
        }
        #endregion
    }
}