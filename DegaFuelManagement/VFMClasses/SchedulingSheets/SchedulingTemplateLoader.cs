using Degatech.Common.DataSheet.DataSheetParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace VFMClasses.ScheduleSheets
{
    public class SchedulingTemplateLoader
    {
        private int _AdminClientId;
        private string _ScheduleName;

        #region Public Methods
        public DataSheetCollection GetDataSheetCollection(int schedulingId)
        {
            XmlDocument document = GetTemplateDocument(schedulingId);
            DataSheetCollection collection = new DataSheetCollection();
            if (document == null)
                return collection;
            foreach (XmlNode node in document.SelectNodes("//SchedulingSheets/SchedulingSheet"))
            {
                DataSheet sheet = new DataSheet();
                sheet.LoadFromNode(node);
                collection.Add(sheet);
            }
            return collection;
        }

        public XmlDocument GetTemplateDocument(int schedulingId)
        {
            switch (schedulingId)
            {
                case 1:
                    _ScheduleName = "Bart";
                    break;
                case 2:
                    _ScheduleName = "FlexJet";
                    break;
                case 3:
                    _ScheduleName = "FlightOptions";
                    break;
                case 4:
                    _ScheduleName = "FOS";
                    break;
            }

            XmlDocument document = new XmlDocument();
            document.Load(GetTemplateFileName());
            return document;
        }
        #endregion

        #region Private Methods
        private string GetTemplateFileName()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "\\Templates\\SchedulingSheets\\" + _ScheduleName + ".xml";
        }
        #endregion

        #region Static Methods
        public static XmlDocument GetSchedulingTemplateDocument(int schedulingId)
        {
            SchedulingTemplateLoader loader = new SchedulingTemplateLoader();
            return loader.GetTemplateDocument(schedulingId);
        }

        public static DataSheetCollection GetSchedulingDataSheetCollection(int schedulingId)
        {
            SchedulingTemplateLoader loader = new SchedulingTemplateLoader();
            return loader.GetDataSheetCollection(schedulingId);
        }
        #endregion
    }
}
