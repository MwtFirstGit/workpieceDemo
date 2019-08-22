using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Data;
using 工件界面Demo.Contract;
using 工件界面Demo.Public;

namespace 工件界面Demo.Biz
{
    class DataBiz : IDataService
    {
        private string Url = ConfigurationManager.ConnectionStrings["Url"].ConnectionString;

        private string token_type { get; set; }

        private string access_token { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataBiz()
        {
            GetToken();
        }

        private void GetToken()
        {
            HttpHelper httphelper = new HttpHelper();
            HttpItem httpitem = new HttpItem()
            {
                URL = string.Format("{0}/connect/token", Url),
                Postdata = "grant_type=client_credentials&client_id=xinyuclient&Client_Secret=xray_201908",
                Method = "Post",
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
            };
            HttpResult result = httphelper.GetHtml(httpitem);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (JsonConvert.DeserializeObject(result.Html) is JObject tokenobj)
                {
                    token_type = tokenobj["token_type"].ToString();
                    access_token = tokenobj["access_token"].ToString();
                }
            }
        }
        /// <summary>
        /// 根据id删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteInfobyId(string id)
        {
            HttpHelper httphelper = new HttpHelper();
            HttpItem httpitem = new HttpItem()
            {
                URL = string.Format("{0}/api/Codedata/{1}", Url, id),
                Method = "DELETE"
            };
            HttpResult result = httphelper.GetHtml(httpitem);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取所有二维码数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetCodeData()
        {
            HttpHelper httphelper = new HttpHelper();
            HttpItem httpitem = new HttpItem()
            {
                URL = string.Format("{0}/api/Codedata", Url),
                Method = "GET"
            };
            HttpResult result = httphelper.GetHtml(httpitem);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return FDataHelper.JArrToDT(result.Html);
            }
            return null;
        }

        /// <summary>
        /// 根据时间获取数据
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public DataTable GetDataInfobyDate(string time1, string time2)
        {
            HttpHelper httphelper = new HttpHelper();
            HttpItem httpitem = new HttpItem()
            {
                URL = string.Format("{0}/api/Codedata/bytime?time1={1}&time2={2}", Url,time1,time2),
                Method = "GET"
            };
            HttpResult result = httphelper.GetHtml(httpitem);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return FDataHelper.JArrToDT(result.Html);
            }
            return null;
        }

        /// <summary>
        /// 按照id查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetDataInfobyId(string id)
        {
            HttpHelper httphelper = new HttpHelper();
            HttpItem httpitem = new HttpItem()
            {
                URL = string.Format("{0}/api/Codedata/{1}", Url, id),
                Method = "GET"
            };
            HttpResult result = httphelper.GetHtml(httpitem);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return FDataHelper.JArrToDT(result.Html);
            }
            return null;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <returns></returns>
        public bool InsertCodeData(object data)
        {
            HttpHelper httphelper = new HttpHelper();
            HttpItem httpitem = new HttpItem()
            {
                URL = string.Format("{0}/api/Codedata", Url),
                Method = "POST",
                Postdata = JsonConvert.SerializeObject(data)
            };
            HttpResult result = httphelper.GetHtml(httpitem);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据id修改数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfobyId(string id,object data)
        {
            HttpHelper httphelper = new HttpHelper();
            HttpItem httpitem = new HttpItem()
            {
                URL = string.Format("{0}/api/Codedata/{1}", Url,id),
                Method = "PUT",
                Postdata = JsonConvert.SerializeObject(data)
                
            };
            HttpResult result = httphelper.GetHtml(httpitem);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
    }
}
