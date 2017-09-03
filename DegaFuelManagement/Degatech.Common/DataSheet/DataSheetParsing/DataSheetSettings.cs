using System.Xml;

namespace Degatech.Common.DataSheet.DataSheetParsing
{
    public class DataSheetSettings : BaseClass
    {
        #region Properties
        public DataSheetSection DataSheetSection { get; set; } = new DataSheetSection();
        public DataSheetMultiRowsPerRecordOptions MultiRowsPerRecordOptions { get; set; } = new DataSheetMultiRowsPerRecordOptions();
        public DataSheetMultiRecordsPerRowOptions MultiRecordsPerRowOptions { get; set; } = new DataSheetMultiRecordsPerRowOptions();
        #endregion

        #region Public Methods
        public void LoadFromNode(XmlNode settingsNode)
        {
            SetProperties(settingsNode);
            if (settingsNode["Section"] != null)
                DataSheetSection.LoadFromNode(settingsNode["Section"]);
            if (settingsNode["MultipleRowsPerRecordOptions"] != null)
                MultiRowsPerRecordOptions.LoadFromNode(settingsNode["MultipleRowsPerRecordOptions"]);
            if (settingsNode["MultipleRecordsPerRowOptions"] != null)
                MultiRecordsPerRowOptions.LoadFromNode(settingsNode["MultipleRecordsPerRowOptions"]);
        }
        #endregion
    }
}