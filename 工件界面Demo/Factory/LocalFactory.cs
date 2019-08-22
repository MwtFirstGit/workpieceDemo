using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 工件界面Demo.Biz;
using 工件界面Demo.Contract;

namespace 工件界面Demo.Factory
{
    /// <summary>
    /// 本地数据的情况下
    /// </summary>
    public class LocalFactory : AbstractFactory
    {
        /// <summary>
        /// 数据来源
        /// </summary>
        /// <returns></returns>
        public override IDataService CreateDataService()
        {
            return new LocalDataBiz();
        }
    }
}
