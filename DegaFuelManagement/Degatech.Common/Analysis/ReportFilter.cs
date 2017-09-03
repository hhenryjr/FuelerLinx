using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Degatech.Common.Analysis
{
    public interface IReportFilter
    {
        #region Properties
        DateTime StartDateFilter { get; set; }
        DateTime EndDateFilter { get; set; }
        #endregion

        #region Public Methods
        List<SqlParameter> GetSQLParameters();
        #endregion
    }
}