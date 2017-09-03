using System;
using System.Xml;
using Degatech.Common.DataSheet.DataSheetParsing;

namespace VFMClasses.FuelSheets
{
    public class TemplateLoader
    {
        private string _AdminClientName;
        private string _SupplierName;

        #region Public Methods
        public DataSheetCollection GetDataSheetCollection(string clientName, string supplierName)
        {
            XmlDocument document = GetTemplateDocument(clientName, supplierName);
            DataSheetCollection collection = new DataSheetCollection();
            if (document == null)
                return collection;
            foreach (XmlNode node in document.SelectNodes("//FuelSheets/FuelSheet"))
            {
                DataSheet sheet = new DataSheet();
                sheet.LoadFromNode(node);
                collection.Add(sheet);
            }
            return collection;
        }

        public XmlDocument GetTemplateDocument(string clientName, string supplierName)
        {
            _SupplierName = supplierName.Replace(" ", "");
            _AdminClientName = clientName.Replace(" ", "");
            XmlDocument document = new XmlDocument();
            document.Load(GetTemplateFileName());
            return document;
        }
        #endregion

        #region Private Methods
        private string GetTemplateFileName()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "\\Templates\\FuelSheets\\" + _AdminClientName + "\\" + _SupplierName + ".xml";
        }
        #endregion

        #region Static Methods
        public static XmlDocument GetSupplierTemplateDocument(string clientName, string supplierName)
        {
            TemplateLoader loader = new TemplateLoader();
            return loader.GetTemplateDocument(clientName, supplierName);
        }

        public static DataSheetCollection GetSupplierDataSheetCollection(string clientName, string supplierName)
        {
            TemplateLoader loader = new TemplateLoader();
            return loader.GetDataSheetCollection(clientName, supplierName);
        }
        #endregion
    }
}