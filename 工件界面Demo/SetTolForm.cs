using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 工件界面Demo.Public;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace 工件界面Demo
{
    public partial class SetTolForm : XtraForm
    {
        /// <summary>
        /// 上工差
        /// </summary>
        public string TopTol { get; set; }
        /// <summary>
        /// 下工差
        /// </summary>
        public string BotTol { get; set; }
        public SetTolForm()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            txtBottol.Text = string.Empty;
            txtToptol.Text = string.Empty;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtToptol.Text == "0")
            {
                MsgBoxHelper.AlertMsgBox("上工差不能为0","提示");
                txtToptol.Focus();
                return;
            }
            if (txtBottol.Text == "0")
            {
                MsgBoxHelper.AlertMsgBox("下工差不能为0","提示");
                txtBottol.Focus();
                return;
            }
            TopTol = txtToptol.Text;
            BotTol = txtBottol.Text;
            FStreamHelper.WriteTxT(TopTol + "," + BotTol, "Tol.txt", false);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SetTolForm_Load(object sender, EventArgs e)
        {
            string tol = FStreamHelper.GetTxT("Tol.txt");
            if (!string.IsNullOrEmpty(tol))
            {
                tol = tol.Replace("----", "|");
                tol = tol.Split('|')[1];
                TopTol = tol.Split(',')[0];
                BotTol = tol.Split(',')[1];
                txtBottol.Text = BotTol;
                txtToptol.Text = TopTol;
            }
        }
    }
}
