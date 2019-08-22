using System;
using System.Configuration;
using System.Data;
using 工件界面Demo.Contract;
using 工件界面Demo.Entity;
using 工件界面Demo.Public;

namespace 工件界面Demo.Biz
{
    public class LocalDataBiz : IDataService
    {
        /// <summary>
        /// 数据保存路径
        /// </summary>
        private string DataFilePath = ConfigurationManager.ConnectionStrings["FilePath"].ConnectionString;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LocalDataBiz()
        {

        }

        /// <summary>
        /// 根据id删除本地数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteInfobyId(string id)
        {
            DataTable dt = FStreamHelper.ExcelToDataTable(DataFilePath);
            DataRow drt = dt.Select("id = '" + id + "'")[0];
            dt.Rows.Remove(drt);
            return FStreamHelper.ToExcel(dt, string.Empty,"sheet1", DataFilePath);
        }

        /// <summary>
        /// 获取所有二维码数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetCodeData()
        {
            return FStreamHelper.ExcelToDataTable(DataFilePath);
        }

        /// <summary>
        /// 根据时间获取数据
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public DataTable GetDataInfobyDate(string time1, string time2)
        {
            DataTable dt = FStreamHelper.ExcelToDataTable(DataFilePath);
            DataRow[] drs = dt.Select(string.Format("createtime = '{0}' and updatetime = '{1}'", time1, time2));
            if (drs != null)
                if (drs.Length > 0)
                    foreach (var item in drs)
                    {
                        dt.Rows.Remove(item);
                    }
            return dt;
        }

        /// <summary>
        /// 根据id获取本地数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetDataInfobyId(string id)
        {
            DataTable dt = FStreamHelper.ExcelToDataTable(DataFilePath);
            DataRow[] drs = dt.Select(string.Format("id = '{0}'", id));
            DataTable newdt = dt.Clone();
            if(drs != null)
                if (drs.Length > 0)
                    foreach (var item in drs)
                    {
                        newdt.ImportRow(item);
                    }
            return newdt;
        }

        /// <summary>
        /// 保存新数据到本地
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool InsertCodeData(object data)
        {
            DataTable dt = FStreamHelper.ExcelToDataTable(DataFilePath);
            DataTable newdt = FDataHelper.JArrToDT(data.ToString());
            if (newdt.Rows.Count > 0)
                foreach (var item in newdt.Rows)
                {
                    dt.Rows.Add(item);
                }
            return FStreamHelper.ToExcel(dt, string.Empty, "sheet1", DataFilePath);
        }

        /// <summary>
        /// 根据id更新本地数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateInfobyId(string id, object data)
        {
            DataTable dt = FStreamHelper.ExcelToDataTable(DataFilePath);
            DataRow[] drt = dt.Select("id = '" + id + "'");
            if (drt != null)
                if (drt.Length > 0)
                    foreach (var item in drt)
                    {
                        dt.Rows.Remove(item);
                    }
            DataTable newdt = FDataHelper.JArrToDT(data.ToString());
            foreach (var item in newdt.Rows)
            {
                dt.Rows.Add(item);
            }
            return FStreamHelper.ToExcel(dt, string.Empty, "sheet1", DataFilePath);
        }
    }
}
