using System;
using Degatech.Utilities.Collection;

namespace Degatech.Common.Analysis.HighCharts
{
    [Serializable]
    public class DrillDownSeriesResult
    {
        #region Properties
        public string name { get; set; }
        public string id { get; set; }
        public SerializableDictionary<string, int> data { get; set; }
        #endregion
    }
}