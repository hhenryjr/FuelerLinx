using System.Xml;

namespace Degatech.Common.DataSheet.DataSheetParsing
{
    public class DataSheetMultiRecordsPerRowOptions : BaseClass
    {
        #region Properties
        public int GroupCount { get; set; } = 1;
        public int CurrentGroup { get; set; } = 1;
        #endregion

        #region Public Methods
        public void LoadFromNode(XmlNode node)
        {
            SetProperties(node);
        }
        #endregion
    }
}
