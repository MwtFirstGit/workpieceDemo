namespace 工件界面Demo
{
    partial class SetTolForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtToptol = new DevExpress.XtraEditors.SpinEdit();
            this.txtBottol = new DevExpress.XtraEditors.SpinEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtToptol.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBottol.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(59, 40);
            this.labelControl1.LookAndFeel.SkinName = "Xmas 2008 Blue";
            this.labelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "上公差：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(59, 93);
            this.labelControl2.LookAndFeel.SkinName = "Xmas 2008 Blue";
            this.labelControl2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "下公差：";
            // 
            // txtToptol
            // 
            this.txtToptol.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtToptol.Location = new System.Drawing.Point(113, 39);
            this.txtToptol.Name = "txtToptol";
            this.txtToptol.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.txtToptol.Properties.LookAndFeel.SkinName = "Blue";
            this.txtToptol.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtToptol.Properties.MaxValue = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.txtToptol.Size = new System.Drawing.Size(177, 20);
            this.txtToptol.TabIndex = 2;
            // 
            // txtBottol
            // 
            this.txtBottol.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBottol.Location = new System.Drawing.Point(113, 92);
            this.txtBottol.Name = "txtBottol";
            this.txtBottol.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.txtBottol.Properties.LookAndFeel.SkinName = "Blue";
            this.txtBottol.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtBottol.Properties.MaxValue = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.txtBottol.Size = new System.Drawing.Size(177, 20);
            this.txtBottol.TabIndex = 3;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(61, 149);
            this.simpleButton1.LookAndFeel.SkinName = "Blue";
            this.simpleButton1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(103, 31);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(212, 149);
            this.simpleButton2.LookAndFeel.SkinName = "Blue";
            this.simpleButton2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(101, 31);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "取消";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // SetTolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 225);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.txtBottol);
            this.Controls.Add(this.txtToptol);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.LookAndFeel.SkinName = "Blue";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetTolForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "公差设置";
            this.Load += new System.EventHandler(this.SetTolForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtToptol.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBottol.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit txtToptol;
        private DevExpress.XtraEditors.SpinEdit txtBottol;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}