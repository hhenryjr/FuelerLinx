using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Degatech.Common.Analysis.HighCharts
{
    [Serializable]
    public class SeriesResult
    {
        public string Name { get; set; }
        public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
        public List<DataValues> Data { get; set; }
        public List<DrillDownSeriesResult> DrillDownSeries { get; set; }

        public SeriesResult()
        {
            Name = string.Empty;
            Data = new List<DataValues>();
        }

        public void AddDataValueFromReportObject(IDataPoint dataPoint, double value, string label)
        {
            DataValues dataValues = new DataValues();
            dataValues.Day = dataPoint.Day;
            dataValues.Week = dataPoint.Week;
            dataValues.Month = dataPoint.Month;
            dataValues.Year = dataPoint.Year;
            dataValues.Value = value;
            dataValues.Label = label;
            Data.Add(dataValues);
        }

        public void AddDataValueFromReportObject(IDataPoint dataPoint)
        {
            AddDataValueFromReportObject(dataPoint, dataPoint.Value, dataPoint.Label);
        }

        public DrillDownSeriesResult GetDrilldownSeries(string id)
        {
            return DrillDownSeries.FirstOrDefault(result => result.id == id);
        }

        public object[][] GetDataAsObjectArray()
        {
            List<object[]> result = new List<object[]>();
            foreach (DataValues value in Data)
            {
                try
                {
                    var objectToAdd = value.GetAsObject();
                    if (objectToAdd.Value > 0)
                        result.Add(new object[] { objectToAdd.Label, objectToAdd.Value });
                    else
                        result.Add(new object[] { objectToAdd.Label, null });
                }
                catch (System.Exception exception)
                {
                    var test = exception;
                }
            }
            return result.ToArray();
        }

        public void FillInMissingDates(IReportFilter filter)
        {
            DateTime startDate = filter.StartDateFilter;
            while (startDate < filter.EndDateFilter)
            {
                DataValues values = GetDataValuesByDate(startDate);
                if (values == null)
                    Data.Add(new DataValues() { Day = 1, Month = startDate.Month, Year = startDate.Year });
                startDate = startDate.AddMonths(1);
            }
            Data = Data.OrderBy(x => x.Year).ThenBy(x => x.Month).ToList();
        }

        public DataValues GetDataValuesByDate(DateTime date)
        {
            return Data.FirstOrDefault(values => values.Month == date.Month && values.Year == date.Year);
        }

        #region Objects
        [Serializable]
        public class DataValues
        {
            #region Properties
            public int Month { get; set; }
            public int Year { get; set; }
            public int Week { get; set; }
            public int Day { get; set; }
            public double Value { get; set; }
            public string Label { get; set; }
            #endregion

            #region Public Methods
            public DataAsObject GetAsObject()
            {
                return new DataAsObject() { Label = GetPointX(), Value = Value };
            }

            public string GetPointX()
            {
                int day = (Day < 1) ? 1 : Day;
                if (string.IsNullOrEmpty(Label))
                    return new DateTime(Year, Month, day).ToString("MMM", CultureInfo.InvariantCulture) + " " + new DateTime(Year, Month, day).Year;
                return Label;
            }
            #endregion
        }

        public class DataAsObject
        {
            public object Label { get; set; }
            public double Value { get; set; }
        }
        #endregion
    }
}
