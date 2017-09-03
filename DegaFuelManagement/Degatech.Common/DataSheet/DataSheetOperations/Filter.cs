using System;
using System.Collections.Generic;
using System.Xml;

namespace Degatech.Common.DataSheet.DataSheetOperations
{
    public class Filter
    {
        #region Enums
        public enum FilterType
        {
            None = 0,
            MaxLength = 1,
            MinLength = 2,
            NotEqual = 3
        }
        #endregion

        #region Members
        private FilterType _Type = FilterType.None;
        private string _Value = "";
        #endregion

        #region Properties
        public FilterType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        #endregion

        #region Public Methods
        public bool IsAllowed(string columnValue)
        {
            bool result = true;
            switch (_Type)
            {
                case FilterType.MaxLength:
                    int maxLength = int.Parse(Value);
                    if (columnValue.Length > maxLength)
                        result = false;
                    break;
                case FilterType.MinLength:
                    int minLength = int.Parse(Value);
                    if (columnValue.Length < minLength)
                        result = false;
                    break;
                case FilterType.NotEqual:
                    if (columnValue.ToLower().Trim() == Value.ToLower())
                        result = false;
                    break;
            }
            return result;
        }
        #endregion
    }

    public class FilterCollection : List<Filter>
    {
        #region Public Methods
        public void Load(XmlNode filerCollectionNode)
        {
            Clear();
            foreach (XmlNode filterNode in filerCollectionNode.ChildNodes)
            {
                Filter filter = new Filter();
                filter.Type = (Filter.FilterType)Enum.Parse(typeof(Filter.FilterType), filterNode.Attributes["Type"].Value);
                filter.Value = filterNode.InnerText;
                Add(filter);
            }
        }

        public bool IsAllowed(string columnValue)
        {
            bool result = true;
            List<Filter>.Enumerator e = this.GetEnumerator();
            while (e.MoveNext())
            {
                if (!e.Current.IsAllowed(columnValue))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        #endregion
    }
}
