using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using 工件界面Demo.Biz;
using 工件界面Demo.Contract;
using 工件界面Demo.Factory;
using 工件界面Demo.Public;

namespace 工件界面Demo
{
    public partial class IndexForm : DevExpress.XtraEditors.XtraForm
    {
        #region 测试数据
        double[] arr = { 1.0, 2.0, 0.6, 1.0, 1.0, 2.0, 0.9, 1.2, 1.5, 0.4, 1.4, 1.8, 1.2, 1.9, 0.7 };
        double[] localarr = { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };
        string code = "CS00001";
        string test = "test1";
        #endregion

        delegate void btngate();
        #region 公共变量
        /// <summary>
        /// 表示数据来源的实例对象
        /// </summary>
        IDataService dataservice = null;
        #endregion
        /// <summary>
        /// 时间定时器
        /// </summary>
        private static System.Timers.Timer _timer;
        /// <summary>
        /// 数据操作定时器
        /// </summary>
        private static System.Timers.Timer maintimer;
        /// <summary>
        /// 本地与服务器时间差（毫秒）
        /// </summary>
        private double _diffMillisecond = 0;
        static int inTimer = 0;
        /// <summary>
        /// 上公差
        /// </summary>
        public double topTol { get; set; } = 0;

        /// <summary>
        /// 下工差
        /// </summary>
        public double botTol { get; set; } = 0;

        /// <summary>
        /// 数据来源标识  值为1，表示数据操作再本地  值为2，表示数据与服务端交互
        /// </summary>
        public string index { get; set; } = "2";

        public DateTime startdate { get; set; }

        public DateTime enddate { get; set; }
        public IndexForm()
        {
            InitializeComponent();
        }

        #region 事件
        /// <summary>
        /// 数据查询按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchDataBtn_Click(object sender, EventArgs e)
        {
            LooksForm looksForm = new LooksForm(index);
            looksForm.ShowDialog();
        }
        /// <summary>
        /// 设置工差按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetTolBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SetTolForm setTolForm = new SetTolForm();
                setTolForm.ShowDialog();
                if (setTolForm.DialogResult == DialogResult.OK)
                {
                    topTol = Convert.ToDouble(setTolForm.TopTol);
                    botTol = Convert.ToDouble(setTolForm.BotTol);
                    SetTolToForm();
                }
            }
            catch (Exception)
            {
                MsgBoxHelper.ErrorMsgBox("程序错误", "错误");
            }

        }
        /// <summary>
        /// 开启设置公差
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenSetTolBtn_Click(object sender, EventArgs e)
        {
            RightsForm frm = new RightsForm();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                SetTolBtn.Visible = true;
                startdate = DateTime.Now;
            }
            //MsgBoxHelper.AlertMsgBox("开始公差设置", "提示");
        }
        /// <summary>
        /// Load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IndexForm_Load(object sender, EventArgs e)
        {
            Init();
            //CompareDatas();
        }
        /// <summary>
        /// 数据操作定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void maintimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //DataTable dt = dataservice.GetCodeData();
        }
        /// <summary>
        /// 时间定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            btngate gate = new btngate(Fun);
            int value = Interlocked.Exchange(ref inTimer, 1);
            if (value == 0)
            {
                barStaticItem1.Caption = "系统时间:" + DateTime.Now.AddMilliseconds(this._diffMillisecond).ToString("yyyy-MM-dd HH:mm:ss");
                this.Invoke(gate);
                Interlocked.Exchange(ref inTimer, 0);
            }
        }

        private void Fun()
        {
            if (SetTolBtn.Visible == true)
            {
                if ((DateTime.Now - startdate).TotalSeconds > 5)
                {
                    SetTolBtn.Visible = false;
                }
            }
            CompareDatas();
        }
        #endregion


        #region 公共方法
        /// <summary>
        /// 初始化界面
        /// </summary>
        private void Init()
        {
            dataservice = AbstractFactory.ChooseFactory(index).CreateDataService();
            //时间定时器
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Interval = 1 * 1000;
            _timer.Enabled = true;
            _timer.AutoReset = true;
            //加载图片
            pictureEdit1.Image = Image.FromFile("test.png");
            //获取上下工差
            GetTol();
            //显示上下工差
            SetTolToForm();
            //数据操作定时器
            maintimer = new System.Timers.Timer(200);
            maintimer.Elapsed += new ElapsedEventHandler(maintimer_Elapsed);
            maintimer.Interval = 1 * 200;
            maintimer.Enabled = true;
            maintimer.AutoReset = true;
        }
        /// <summary>
        /// 显示上下工差在主界面
        /// </summary>
        private void SetTolToForm()
        {
            barStaticItem2.Caption = "当前上公差为:" + topTol;
            barStaticItem3.Caption = "当前下公差为:" + botTol;
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        private void CreateTolTxT()
        {
            if (!File.Exists("Tol.txt"))
                File.Create("Tol.txt").Close();
        }

        /// <summary>
        /// 获取本地保存的上下工差
        /// </summary>
        private void GetTol()
        {
            CreateTolTxT();
            string tol = FStreamHelper.GetTxT("Tol.txt");
            if (!string.IsNullOrEmpty(tol))
            {
                tol = tol.Replace("----", "|");
                tol = tol.Split('|')[1];
                topTol = Convert.ToDouble(tol.Split(',')[0]);
                botTol = Convert.ToDouble(tol.Split(',')[1]);
            }
        }
        #endregion

        #region 数据操作帮助

        /// <summary>
        /// 从plc中获取数据
        /// </summary>
        private void GetDataByPLC()
        {
            
        }
        /// <summary>
        /// 将从pcl获取的数据与工差进行对比
        /// </summary>
        private void CompareDatas()
        {
            if (arr.Length != localarr.Length)
            {
                return;
            }
            for (int i = 0;i<localarr.Length;i++)
            {
                double exh = CompareData(localarr[i], arr[i]);
                Dictionary<string, ProgressBarControl> controldic = ChooseControl(i);
                if (controldic != null)
                {
                    if (controldic.Count == 3)
                    {
                        if (exh == 100)
                        {
                            controldic["databar"].Position = 100;
                            controldic["databarB"].Position = 0;
                            controldic["databarT"].Position = 0;
                        }
                        else if (exh < 0)
                        {
                            controldic["databar"].Position = 0;
                            controldic["databarB"].Position = Convert.ToInt32((-1 * exh).ToString("0"));
                            controldic["databarT"].Position = 0;
                        }
                        else
                        {
                            controldic["databarB"].Position = 0;
                            controldic["databar"].Position = 0;
                            controldic["databarT"].Position = Convert.ToInt32(exh.ToString("0"));
                        }
                    }
                }
                else
                {
                    maintimer.Enabled = false;
                    MsgBoxHelper.AlertMsgBox("数据数量超出15个", "提示");
                    return;
                }
            }
        }

        /// <summary>
        /// 每一组数据单独对比
        /// </summary>
        private double CompareData(double localdata,double measuredata)
        {
            if ((localdata - botTol) > measuredata)
            {
                return -(localdata - botTol - measuredata) / (localdata - botTol) * 100;
            }
            else if(measuredata > (localdata + topTol))
            {
                return (measuredata - localdata- topTol) / (localdata + topTol) * 100;
            }
            return 100;
        }

        /// <summary>
        /// 选择控件
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private Dictionary<string, ProgressBarControl> ChooseControl(int position)
        {
            Dictionary<string, ProgressBarControl> control = new Dictionary<string, ProgressBarControl>();
            switch (position)
            {
                case 0:
                    control.Add("databar",databar1);
                    control.Add("databarT",databar1T);
                    control.Add("databarB",databar1B);
                    break;
                case 1:
                    control.Add("databar", databar2);
                    control.Add("databarT", databar2T);
                    control.Add("databarB", databar2B);
                    break;
                case 2:
                    control.Add("databar", databar3);
                    control.Add("databarT", databar3T);
                    control.Add("databarB", databar3B);
                    break;
                case 3:
                    control.Add("databar", databar4);
                    control.Add("databarT", databar4T);
                    control.Add("databarB", databar4B);
                    break;
                case 4:
                    control.Add("databar", databar5);
                    control.Add("databarT", databar5T);
                    control.Add("databarB", databar5B);
                    break;
                case 5:
                    control.Add("databar", databar6);
                    control.Add("databarT", databar6T);
                    control.Add("databarB", databar6B);
                    break;
                case 6:
                    control.Add("databar", databar7);
                    control.Add("databarT", databar7T);
                    control.Add("databarB", databar7B);
                    break;
                case 7:
                    control.Add("databar", databar8);
                    control.Add("databarT", databar8T);
                    control.Add("databarB", databar8B);
                    break;
                case 8:
                    control.Add("databar", databar9);
                    control.Add("databarT", databar9T);
                    control.Add("databarB", databar9B);
                    break;
                case 9:
                    control.Add("databar", databar10);
                    control.Add("databarT", databar10T);
                    control.Add("databarB", databar10B);
                    break;
                case 10:
                    control.Add("databar", databar11);
                    control.Add("databarT", databar11T);
                    control.Add("databarB", databar11B);
                    break;
                case 11:
                    control.Add("databar", databar12);
                    control.Add("databarT", databar12T);
                    control.Add("databarB", databar12B);
                    break;
                case 12:
                    control.Add("databar", databar13);
                    control.Add("databarT", databar13T);
                    control.Add("databarB", databar13B);
                    break;
                case 13:
                    control.Add("databar", databar14);
                    control.Add("databarT", databar14T);
                    control.Add("databarB", databar14B);
                    break;
                case 14:
                    control.Add("databar", databar15);
                    control.Add("databarT", databar15T);
                    control.Add("databarB", databar15B);
                    break;
                default:
                    control = null;
                    break;
            }
            return control;
        }

        #endregion

       
    }
}
