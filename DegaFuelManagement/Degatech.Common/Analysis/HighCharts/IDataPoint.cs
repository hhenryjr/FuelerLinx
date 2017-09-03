namespace Degatech.Common.Analysis.HighCharts
{
    public interface IDataPoint
    {
        #region Properties
        int Day { get; set; }
        int Week { get; set; }
        int Month { get; set; }
        int Year { get; set; }
        double Value { get; set; }
        string Label { get; set; }
        #endregion
    }
}