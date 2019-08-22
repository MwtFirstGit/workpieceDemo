using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 工件界面Demo.Public
{
    public class MsgBoxHelper
    {

        #region 获取基本的消息框
        /// <summary>
        /// 获取基本的消息框
        /// </summary>
        /// <param name="msg">基本信息</param>
        /// <param name="Title">标题</param>
        /// <returns></returns>
        public static DialogResult MsgBox(string msg,string Title)
        {
            return XtraMessageBox.Show(GetLookAndFeel(),msg, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region 获取基本的错误消息框
        /// <summary>
        /// 获取基本的错误消息框
        /// </summary>
        /// <param name="msg">基本信息</param>
        /// <param name="Title">标题</param>
        /// <returns></returns>
        public static DialogResult ErrorMsgBox(string msg, string Title)
        {
            return XtraMessageBox.Show(GetLookAndFeel(),msg, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region 获取询问消息框
        /// <summary>
        /// 获取询问消息框
        /// </summary>
        /// <param name="msg">基本信息</param>
        /// <param name="Title">标题</param>
        /// <returns></returns>
        public static DialogResult AskMsgBox(string msg, string Title)
        {
            return XtraMessageBox.Show(GetLookAndFeel(),msg, Title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }
        #endregion

        #region 获取警告消息框
        /// <summary>
        /// 获取询问消息框
        /// </summary>
        /// <param name="msg">基本信息</param>
        /// <param name="Title">标题</param>
        /// <returns></returns>
        public static DialogResult AlertMsgBox(string msg, string Title)
        {
            return XtraMessageBox.Show(GetLookAndFeel(), msg, Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        #endregion

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <returns></returns>
        private static UserLookAndFeel GetLookAndFeel()
        {
            UserLookAndFeel userLookAndFeel = UserLookAndFeel.Default;
            userLookAndFeel.UseDefaultLookAndFeel = false;
            userLookAndFeel.SkinName = "Blue";
            return userLookAndFeel;
        }
    }
}
