using Degatech.Common.DataSheet.DataSheetParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace VFMClasses.AcukwikSheets
{
    class AcukwikTemplateLoader
    {
        private string _AcukwikName;

        #region Public Methods
        public DataSheetCollection GetDataSheetCollection(string acukwikName)
        {
            XmlDocument document = GetTemplateDocument(acukwikName);
            DataSheetCollection collection = new DataSheetCollection();
            if (document == null)
                return collection;
            foreach (XmlNode node in document.SelectNodes("//AcukwikSheets/AcukwikSheet"))
            {
                DataSheet sheet = new DataSheet();
                sheet.LoadFromNode(node);
                collection.Add(sheet);
            }
            return collection;
        }

        public XmlDocument GetTemplateDocument(string acukwikName)
        {
            _AcukwikName = acukwikName;
            XmlDocument document = new XmlDocument();
            document.Load(GetTemplateFileName());
            return document;
        }
        #endregion

        #region Private Methods
        private string GetTemplateFileName()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "\\Templates\\AcukwikSheets\\" + _AcukwikName + ".xml";
        }
        #endregion

        #region Static Methods
        public static XmlDocument GetAcukwikTemplateDocument(string acukwikName)
        {
            AcukwikTemplateLoader loader = new AcukwikTemplateLoader();
            return loader.GetTemplateDocument(acukwikName);
        }

        public static DataSheetCollection GetAcukwikDataSheetCollection(string acukwikName)
        {
            AcukwikTemplateLoader loader = new AcukwikTemplateLoader();
            return loader.GetDataSheetCollection(acukwikName);
        }
        #endregion
    }
}
