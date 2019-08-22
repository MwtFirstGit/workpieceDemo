using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 工件界面Demo.Entity
{
    public class DataEntity
    {
        /// <summary>
        /// 产品id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 产品二维码
        /// </summary>
        public string code { get; set; }
        public string checkresult { get; set; }

        /// <summary>
        /// 各个点的数据
        /// </summary>
        public string data { get; set; }

        /// <summary>
        /// 检测点的数量
        /// </summary>
        public int length { get; set; } = 0;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createtime{ get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updatetime { get; set; }
    }
}
