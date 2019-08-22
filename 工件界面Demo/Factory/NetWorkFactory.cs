using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 工件界面Demo.Biz;
using 工件界面Demo.Contract;

namespace 工件界面Demo.Factory
{
    /// <summary>
    /// 网络数据的情况下
    /// </summary>
    public class NetWorkFactory : AbstractFactory
    {
        /// <summary>
        /// 数据来源
        /// </summary>
        /// <returns></returns>
        public override IDataService CreateDataService()
        {
            return new DataBiz();
        }
    }
}
