using System;
using System.Collections.Generic;
using System.Xml;

namespace Degatech.Common.DataSheet.DataSheetOperations
{
    public class StringOperation : BaseClass
    {
        #region Enums
        public enum OperationTypes
        {
            None = 0,
            Split = 1,
            Replace = 2,
            EndWith= 3
        }
        #endregion

        #region Private Members
        private OperationTypes _OperationType = OperationTypes.None;
        private SortedList<string, string> _Arguments = new SortedList<string, string>();
        private string _Value = "";
        #endregion

        #region Properties
        public OperationTypes OperationType
        {
            get { return _OperationType; }
            set { _OperationType = value; }
        }

        public SortedList<string, string> Arguments
        {
            get { return _Arguments; }
            set { _Arguments = value; }
        }

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public string SplitOn { get; set; } = string.Empty;
        public int Index { get; set; }
        public string Replace { get; set; } = string.Empty;
        public string ReplaceWith { get; set; } = string.Empty;

        #endregion

        #region Public Methods
        public StringOperation()
        {
            
        }

        public string PerformOperation(string StringToManipulate)
        {
            string result = StringToManipulate;
            switch (_OperationType)
            {
                case OperationTypes.Split:
                    char splitOn = SplitOn.ToCharArray()[0];
                    string[] tempSplit = result.Split(splitOn);
                    int indexOfSplit = Index;
                    if (tempSplit.Length > indexOfSplit)
                        result = tempSplit[indexOfSplit];
                    else
                        result = "";
                    break;
                case OperationTypes.Replace:
                    string toReplace = Replace;
                    string replaceWith = ReplaceWith;
                    result = result.Replace(toReplace, replaceWith);
                    break;
                    case OperationTypes.EndWith:
                    string endValue = Value;
                    if (!result.TrimEnd().EndsWith("."))
                        result += endValue;
                    break;
            }
            return result;
        }
        #endregion
    }

    public class StringOperationCollection : List<StringOperation>
    {
        #region Public Methods
        public void Load(XmlNode stringOperationsNode)
        {
            Clear();
            foreach (XmlNode operationNode in stringOperationsNode.ChildNodes)
            {
                StringOperation stringOperation = new StringOperation();
                stringOperation.SetProperties(operationNode);
                stringOperation.OperationType = (StringOperation.OperationTypes)Enum.Parse(typeof(StringOperation.OperationTypes), operationNode.Attributes["Type"].Value);
                if (!string.IsNullOrEmpty(operationNode.InnerText))
                    stringOperation.Value = operationNode.InnerText;
                foreach (XmlAttribute attribute in operationNode.Attributes)
                {
                    if (attribute.Name.ToLower() != "type")
                        Degatech.Utilities.Data.PropertySetter.SetPropertyValue(stringOperation, attribute.Name, attribute.Value);
                }
                Add(stringOperation);
            }
        }

        public string PerformOperations(string StringToManipulate)
        {
            string result = StringToManipulate;
            List<StringOperation>.Enumerator e = this.GetEnumerator();
            while (e.MoveNext())
            {
                result = e.Current.PerformOperation(result);
            }
            return result;
        }
        #endregion
    }
}