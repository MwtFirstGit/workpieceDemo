using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace 工件界面Demo.Contract
{
    public interface IDataService
    {
        /// <summary>
        /// 拉取所有二维码数据
        /// </summary>
        /// <returns></returns>
        DataTable GetCodeData();

        /// <summary>
        /// 插入新数据
        /// </summary>
        /// <returns></returns>
        bool InsertCodeData(object data);

        /// <summary>
        /// 按照id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DataTable GetDataInfobyId(string id);

        /// <summary>
        /// 根据id修改数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool UpdateInfobyId(string id,object data);

        /// <summary>
        /// 根据id删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteInfobyId(string id);

        /// <summary>
        /// 根据时间获取数据
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        DataTable GetDataInfobyDate(string time1, string time2);
    }
}
