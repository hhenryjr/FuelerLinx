using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Degatech.Common;

public class EmailDispatches : BaseClass
{
    #region Enum

    public enum Purpose
    {
        RequestForRelease = 0,
        FuelReleaseConfirmation = 1,
        VendorRequestForRelease = 2
    }

    #endregion

    #region Properties
    public int AdminClientID { get; set; }
    public string Content { get; set; }

    #endregion
}
