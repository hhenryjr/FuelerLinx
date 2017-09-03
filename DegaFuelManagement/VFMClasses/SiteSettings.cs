using Degatech.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace VFMClasses
{
    public class SiteSettings : BaseClass
    {
        #region Properties
        public string MapSettings { get; set; } = String.Empty;

        public bool AllowTransactionDetailEditing
        {
            get
            {
                string allowTransactionDetailEditing =
                    WebConfigurationManager.AppSettings["AllowTransactionDetailEditing"];
                if (allowTransactionDetailEditing != null)
                    return bool.Parse(allowTransactionDetailEditing);
                return true;
            }
        }

        public string FuelerLinxServiceURL
        {
            get
            {
                string serviceURL = WebConfigurationManager.AppSettings["FuelerLinxServiceURL"];
                if (string.IsNullOrEmpty(serviceURL))
                    return "https://beta.fuelerlinx.com/Services/Transactions/DegaIntegration.asmx";
                return serviceURL;
            }
        }

        public bool ShowMarginVolumeTiers
        {
            get
            {
                string showMarginVolumeTiers =
                    WebConfigurationManager.AppSettings["ShowMarginVolumeTiers"];
                if (showMarginVolumeTiers != null)
                    return bool.Parse(showMarginVolumeTiers);
                return true;
            }
        }
        #endregion

        #region Constructors
        public SiteSettings()
        {
        }

        public SiteSettings(string map)
        {
            MapSettings = map;
            Load();
        }

        public SiteSettings(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MapSettings", MapSettings));
            using (SqlDataReader reader = Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_Select_SiteSettings_Map", parameters))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }

        public void LoadAll()
        {
            using (SqlDataReader reader = Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_Select_SiteSettings_All"))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }

        protected void LoadFromReader(SqlDataReader reader)
        {
            SetProperties(reader);
        }
        #endregion

        #region Static Methods
        public static SiteSettings GetMapSettings(string map)
        {
            return new SiteSettings(map);
        }

        public static SiteSettings GetAllSettings()
        {
            SiteSettings settings = new SiteSettings();
            settings.LoadAll();
            return settings;
        }
        #endregion
    }
}
