using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 工件界面Demo.Contract;

namespace 工件界面Demo.Factory
{
    public abstract class AbstractFactory
    {
        /// <summary>
        /// 1,表示数据操作在本地  2,表示数据操作在网络
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static AbstractFactory ChooseFactory(string index)
        {
            AbstractFactory factory = null;
            switch (index)
            {
                case "1":
                    factory = new LocalFactory();
                    break;
                case "2":
                    factory = new NetWorkFactory();
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
            }
            return factory;
        }

        /// <summary>
        /// 公共数据接口
        /// </summary>
        /// <returns></returns>
        public abstract IDataService CreateDataService();
    }
}
