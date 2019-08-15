using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetCBL
{
    public partial class Budget_ConfigSite
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public Budget_ConfigSite()
        {
        }
        public Budget_ConfigSite(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }
        public List<Budget_ConfigSite> SelectAll()
        {
            List<Budget_ConfigSite> configList = null;
            try
            {
                string sSQL = "SELECT * FROM ConfigSite";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    configList = new List<Budget_ConfigSite>();
                    Budget_ConfigSite config = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        config = new Budget_ConfigSite();
                        config.ConfigKey = ds.Tables[0].Rows[i]["ConfigKey"].ToString();
                        config.ConfigValue = ds.Tables[0].Rows[i]["ConfigValue"].ToString();
                        configList.Add(config);
                    }
                }
            }
            catch (Exception ex) { }
            return configList;
        }
        public Budget_ConfigSite SelectByConfigKey(string ConfigKey)
        {
            Budget_ConfigSite config = null;
            try
            {
                string sSQL = "SELECT * FROM ConfigSite WHERE ConfigKey=@ConfigKey";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@ConfigKey", SqlDbType.NVarChar);
                sqlParam[0].Value = ConfigKey;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    config = new Budget_ConfigSite();
                    config.ConfigKey = ds.Tables[0].Rows[0]["ConfigKey"].ToString();
                    config.ConfigValue = ds.Tables[0].Rows[0]["ConfigValue"].ToString();

                }
            }
            catch (Exception ex) { }
            return config;
        }
        public bool update()
        {
            bool bResult = false;
            try
            {
                string sSQL = "UPDATE ConfigSite SET ConfigValue=@ConfigValue  WHERE ConfigKey=@ConfigKey";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@ConfigKey", SqlDbType.NVarChar);
                sqlParam[0].Value = this.ConfigKey;
                sqlParam[1] = new SqlParameter("@ConfigValue", SqlDbType.NVarChar);
                sqlParam[1].Value = this.ConfigValue;
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
