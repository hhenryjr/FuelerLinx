using System.Data;
using System.Xml;

namespace Degatech.Common.DataSheet.DataSheetParsing
{
    public class DataSheetSection : BaseClass
    {
        #region Properties
        public string Separator { get; set; } = string.Empty;
        public int SeparatorIndex { get; set; }
        public DataSheetColumnCollection SectionValues { get; set; } = new DataSheetColumnCollection();
        #endregion

        #region Public Methods
        public void LoadFromNode(XmlNode node)
        {
            SectionValues.Clear();
            SetProperties(node);
            if (node["SectionValues"] == null)
                return;
            SectionValues.Load(node["SectionValues"]);
        }

        public void CheckForNewSection(DataRow uploadedSheetRow, DataSheetSettings settings)
        {
            object separatorColumnValue = uploadedSheetRow[SeparatorIndex];
            if (separatorColumnValue == null || separatorColumnValue.ToString().IndexOf(Separator) == -1)
                return;
            PrepareSectionStaticValues(uploadedSheetRow, settings);
        }

        public void ApplySectionValues(DataTable formattedTable, DataRow newRow, DataSheetSettings settings)
        {
            foreach (DataSheetColumn column in SectionValues)
            {
                if (string.IsNullOrEmpty(column.StaticValue))
                    continue;
                newRow[column.Name] = newRow[column.Name] + column.StaticValue;
            }
        }
        #endregion

        #region Private Methods
        private void PrepareSectionStaticValues(DataRow uploadedSheetRow, DataSheetSettings settings)
        {

            foreach (DataSheetColumn column in SectionValues)
                column.StaticValue = column.FormattedColumnValue(uploadedSheetRow, settings);
        }
        #endregion
    }
}
