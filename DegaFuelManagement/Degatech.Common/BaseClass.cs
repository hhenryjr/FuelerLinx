using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Degatech.Common
{
    public class BaseClass
    {
        #region Enum
        public enum LoginResults
        {
            Unknown = 0,
            InvalidUsernamePassword = 1,
            InactiveAccount = 2,
            Success = 3
        }

        public enum ClientTypes
        {
            NotSet = 0,
            Admin = 1,
            Customer = 2
        }

        public enum AdminStatuses
        {
            Pending = 0,
            FboNotified = 1,
            Confirmed = 2
        }

        public enum CustStatuses
        {            
            Pending = 0,        
            Reconciled = 1,
            Discrepancy = 2,
            Cancelled = 3
        }

        public enum EmailTypes
        {
            Registration = 0,
            NewClientActivated = 1,
            NewClientDeactivated = 2,
            FuelOrder = 3,
            PriceDistribution = 4
        }

        public enum AircraftSizes
        {
            NotSet = 0,
            LightJet = 1,
            MediumJet = 2,
            HeavyJet = 3,
            LightHelicopter = 4,
            WideBody = 5,
            SingleTurboProp = 6,
            VeryLightJet = 7,
            SingleEnginePiston = 8,
            MediumHelicopter = 9,
            HeavyHelicopter = 10,
            LightTwin = 11,
            HeavyTwin = 12,
            LightTurboProp = 13,
            MediumTurboprop = 14,
            HeavyTurboprop = 15,
            SuperHeavyJet = 16
        }
        #endregion

        #region Members
        #endregion

        #region Properties
        #endregion

        #region Public Methods
        public void SetProperties(SqlDataReader rdr)
        {
            Utilities.Data.PropertySetter.SetProperties(rdr, this);
        }

        public void SetProperties(DataRow row)
        {
            Utilities.Data.PropertySetter.SetProperties(row, this);
        }

        public void SetProperties(XmlNode node)
        {
            Utilities.Data.PropertySetter.SetProperties(node, this);
        }

        public void SetProperties(Dictionary<string, object> dictionary)
        {
            Utilities.Data.PropertySetter.SetProperties(dictionary, this);
        }

        public void Clone(object objectToCopy)
        {
            if (objectToCopy == null)
                return;
            if (objectToCopy.GetType() == typeof(Dictionary<string, object>))
                SetProperties((Dictionary<string, object>)objectToCopy);
            else
                Utilities.Data.PropertySetter.Clone(objectToCopy, this);
        }

        public List<SqlParameter> GetSQLParameters(System.Collections.ArrayList propertiesToOmit)
        {
            return Utilities.Data.PropertyGetter.GetSQLParameters(this, propertiesToOmit);
        }

        public List<SqlParameter> GetSQLParameters()
        {
            return GetSQLParameters(new System.Collections.ArrayList());
        }
        #endregion

        #region Private Methods
        private SqlParameter GetEnumSqlParameter(PropertyInfo propertyInfo)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@" + propertyInfo.Name;
            parameter.SqlDbType = SqlDbType.SmallInt;
            return parameter;
        }

        private SqlParameter GetSqlParameter(PropertyInfo propertyInfo)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@" + propertyInfo.Name;
            switch (propertyInfo.PropertyType.Name.ToLower())
            {
                case "int":
                    parameter.SqlDbType = SqlDbType.Int;
                    break;
                case "int32":
                    parameter.SqlDbType = SqlDbType.Int;
                    break;
                case "datetime":
                    if (IsDateNothing(DateTime.Parse(propertyInfo.GetValue(this, null).ToString())))
                        parameter = null;
                    else
                        parameter.SqlDbType = SqlDbType.DateTime;
                    break;
                case "decimal":
                    parameter.SqlDbType = SqlDbType.Decimal;
                    break;
                case "double":
                    parameter.SqlDbType = SqlDbType.Float;
                    break;
                case "bool":
                    parameter.SqlDbType = SqlDbType.Bit;
                    break;
                case "boolean":
                    parameter.SqlDbType = SqlDbType.Bit;
                    break;
                case "string":
                    parameter.SqlDbType = SqlDbType.NVarChar;
                    break;
                default:
                    parameter = null;
                    break;
            }
            return parameter;
        }

        public static bool IsDateNothing(DateTime DT)
        {
            if (DT == null || DT.ToShortDateString() == DateTime.MinValue.ToShortDateString() || DT <= DateTime.Parse("1/2/1753") || DT <= DateTime.Parse("1/2/1900"))
                return true;
            else
                return false;
        }

        public static DateTime DatabaseDateTimeMinValue()
        {
            return DateTime.Parse("1/1/1753");
        }

        #endregion
    }
}
