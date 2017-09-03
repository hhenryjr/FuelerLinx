using System.Xml;

namespace Degatech.Common.DataSheet.DataSheetParsing
{
    public class DataSheetMultiRowsPerRecordOptions : BaseClass
    {
        #region Properties
        public bool MultipleRowsPerRecord { get; set; }
        public DataSheetColumnCollection Columns { get; set; } = new DataSheetColumnCollection();
        #endregion

        #region Public Methods
        public void LoadFromNode(XmlNode node)
        {
            Columns.Clear();
            SetProperties(node);
            Columns.Load(node);
        }
        #endregion
    }
}
