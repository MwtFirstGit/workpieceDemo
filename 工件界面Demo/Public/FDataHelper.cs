using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace 工件界面Demo.Public
{
    public static class FDataHelper
    {
        /// <summary>
        /// JArray格式的字符串转DataTable
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable JArrToDT(string data)
        {
            DataTable dt = null;
            int m = 0;
            int columncount = CreateDtColumns(ref dt);
            if (JsonConvert.DeserializeObject(data) is JArray jarr)
            {
                #region 生成所有列
                foreach (var item in jarr)
                {
                    if (Convert.ToInt32(item["length"]) - m > 0)
                        CreateDtColumns(ref dt, Convert.ToInt32(item["length"]) - m, m);
                    m = Convert.ToInt32(item["length"]);
                }
                #endregion

                #region 真正的开始添加数据
                DataRow dr = null;
                foreach (var item in jarr)
                {
                    dr = dt.NewRow();
                    dr["id"] = Convert.ToString(item["id"]);
                    dr["code"] = Convert.ToString(item["code"]);
                    dr["checkresult"] = Convert.ToString(item["checkresult"]);
                    dr["createtime"] = item["createtime"].ToString();
                    dr["updatetime"] = item["updatetime"].ToString();
                    if (JsonConvert.DeserializeObject(item["data"].ToString()) is JArray jarrs)
                    {
                        for (int i = 0; i < jarrs.Count; i++)
                        {
                            dr["data" + (i + 1)] = Convert.ToDouble(jarrs[i]);
                        }
                    }
                    dt.Rows.Add(dr);
                }
                #endregion
            }
            return dt;
        }

        /// <summary>
        /// 新建DataTable
        /// </summary>
        /// <param name="dt"></param>
        public static int CreateDtColumns(ref DataTable dt)
        {
            dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("code");
            dt.Columns.Add("checkresult");
            dt.Columns.Add("createtime");
            dt.Columns.Add("updatetime");
            return dt.Columns.Count;
        }

        public static void CreateDtColumns(ref DataTable dt, int length, int m)
        {
            for (int i = 0; i < length; i++)
            {
                dt.Columns.Add("data" + (m + 1));
            }
        }
    }
}
