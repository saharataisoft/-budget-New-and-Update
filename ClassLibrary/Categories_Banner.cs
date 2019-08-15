using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetCBL
{
    public partial class Categories_Banner
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["BudgetWorldwide.ConnectionString"];
        public string CategoriesCode { get; set; }
        public string CategoriesName { get; set; }
        public Categories_Banner()
        {
        }

        public Categories_Banner(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }

        public List<Categories_Banner> SelectAll(string PrefixTable)
        {
            List<Categories_Banner> Categories_BannerList = null;
            try
            {
                string sSQL = "SELECT * FROM ##Categories_Banner";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Categories_BannerList = new List<Categories_Banner>();
                    Categories_Banner categories = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        categories = new Categories_Banner();
                        categories.CategoriesCode = ds.Tables[0].Rows[i]["CategoriesCode"].ToString();
                        categories.CategoriesName = ds.Tables[0].Rows[i]["CategoriesName"].ToString();
                        Categories_BannerList.Add(categories);
                    }
                }
            }
            catch (Exception ex) { }
            return Categories_BannerList;
        }
    }
}
