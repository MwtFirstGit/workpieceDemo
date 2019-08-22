using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 工件界面Demo.Contract;
using 工件界面Demo.Factory;
using 工件界面Demo.Public;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace 工件界面Demo
{
    public partial class LooksForm : XtraForm
    {
        DataTable dtMain = new DataTable();
        /// <summary>
        /// 表示数据来源的实例对象
        /// </summary>
        IDataService dataservice = null;
        public LooksForm(string index)
        {
            dataservice = AbstractFactory.ChooseFactory(index).CreateDataService();
            InitializeComponent();
        }

        #region 事件
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dateEdit1.Text) && string.IsNullOrEmpty(txtPRD_ID.Text) && string.IsNullOrEmpty(txtPRD_NAME.Text))
            {
                MsgBoxHelper.AlertMsgBox("请至少选择一个条件查询", "提示");
                dateEdit1.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(dateEdit1.Text))
                SearchByDate(dateEdit1.Text,dateEdit1.Text);
            if (!string.IsNullOrEmpty(txtPRD_ID.Text))
                SearchById(txtPRD_ID.Text);
        }
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                dateEdit1.Enabled = true;
                txtPRD_ID.Enabled = false;
                txtPRD_NAME.Enabled = false;
            }
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked)
            {
                dateEdit1.Enabled = false;
                txtPRD_ID.Enabled = false;
                txtPRD_NAME.Enabled = true;
            }
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit3.Checked)
            {
                dateEdit1.Enabled = false;
                txtPRD_ID.Enabled = true;
                txtPRD_NAME.Enabled = false;
            }
        }
        #endregion

        /// <summary>
        /// 根据时间查询数据
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        private void SearchByDate(string time1,string time2)
        {
            dtMain = dataservice.GetDataInfobyDate(time1, time2);
            gridControl1.DataSource = dtMain;
        }

        /// <summary>
        /// 根据id查询数据
        /// </summary>
        /// <param name="id"></param>
        private void SearchById(string id)
        {
            dtMain = dataservice.GetDataInfobyId(id);
            gridControl1.DataSource = dtMain;
        }

        /// <summary>
        /// 拉取所有二维码数据
        /// </summary>
        private void SearchByCode()
        {
            dtMain = dataservice.GetCodeData();
            gridControl1.DataSource = dtMain;
        }
    }
}
