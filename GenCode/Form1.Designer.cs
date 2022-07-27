namespace GenCode
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtModule = new System.Windows.Forms.TextBox();
            this.TxtNameSpace = new System.Windows.Forms.TextBox();
            this.txtThuMuc = new System.Windows.Forms.TextBox();
            this.txtRefix = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.TxtOutput = new System.Windows.Forms.TextBox();
            this.RdEFConfigs = new System.Windows.Forms.RadioButton();
            this.RdMapper = new System.Windows.Forms.RadioButton();
            this.RdQueries = new System.Windows.Forms.RadioButton();
            this.RdViewModels = new System.Windows.Forms.RadioButton();
            this.RdController = new System.Windows.Forms.RadioButton();
            this.TxtConnectString = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "NameSpace";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Thư mục";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(415, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Module";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tên bảng";
            // 
            // txtModule
            // 
            this.txtModule.Location = new System.Drawing.Point(496, 55);
            this.txtModule.Name = "txtModule";
            this.txtModule.Size = new System.Drawing.Size(258, 27);
            this.txtModule.TabIndex = 4;
            // 
            // TxtNameSpace
            // 
            this.TxtNameSpace.Location = new System.Drawing.Point(144, 55);
            this.TxtNameSpace.Name = "TxtNameSpace";
            this.TxtNameSpace.Size = new System.Drawing.Size(243, 27);
            this.TxtNameSpace.TabIndex = 5;
            // 
            // txtThuMuc
            // 
            this.txtThuMuc.Location = new System.Drawing.Point(144, 154);
            this.txtThuMuc.Name = "txtThuMuc";
            this.txtThuMuc.Size = new System.Drawing.Size(610, 27);
            this.txtThuMuc.TabIndex = 6;
            // 
            // txtRefix
            // 
            this.txtRefix.Location = new System.Drawing.Point(145, 107);
            this.txtRefix.Name = "txtRefix";
            this.txtRefix.Size = new System.Drawing.Size(238, 27);
            this.txtRefix.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(583, 297);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 36);
            this.button1.TabIndex = 8;
            this.button1.Text = "Đóng";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(403, 297);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(163, 36);
            this.button2.TabIndex = 9;
            this.button2.Text = "Thực hiện";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TxtOutput
            // 
            this.TxtOutput.Location = new System.Drawing.Point(42, 352);
            this.TxtOutput.Multiline = true;
            this.TxtOutput.Name = "TxtOutput";
            this.TxtOutput.Size = new System.Drawing.Size(712, 248);
            this.TxtOutput.TabIndex = 10;
            // 
            // RdEFConfigs
            // 
            this.RdEFConfigs.AutoSize = true;
            this.RdEFConfigs.Location = new System.Drawing.Point(52, 267);
            this.RdEFConfigs.Name = "RdEFConfigs";
            this.RdEFConfigs.Size = new System.Drawing.Size(95, 24);
            this.RdEFConfigs.TabIndex = 12;
            this.RdEFConfigs.TabStop = true;
            this.RdEFConfigs.Text = "EFConfigs";
            this.RdEFConfigs.UseVisualStyleBackColor = true;
            // 
            // RdMapper
            // 
            this.RdMapper.AutoSize = true;
            this.RdMapper.Location = new System.Drawing.Point(168, 267);
            this.RdMapper.Name = "RdMapper";
            this.RdMapper.Size = new System.Drawing.Size(82, 24);
            this.RdMapper.TabIndex = 13;
            this.RdMapper.TabStop = true;
            this.RdMapper.Text = "Mapper";
            this.RdMapper.UseVisualStyleBackColor = true;
            // 
            // RdQueries
            // 
            this.RdQueries.AutoSize = true;
            this.RdQueries.Location = new System.Drawing.Point(281, 267);
            this.RdQueries.Name = "RdQueries";
            this.RdQueries.Size = new System.Drawing.Size(80, 24);
            this.RdQueries.TabIndex = 14;
            this.RdQueries.TabStop = true;
            this.RdQueries.Text = "Queries";
            this.RdQueries.UseVisualStyleBackColor = true;
            // 
            // RdViewModels
            // 
            this.RdViewModels.AutoSize = true;
            this.RdViewModels.Location = new System.Drawing.Point(403, 267);
            this.RdViewModels.Name = "RdViewModels";
            this.RdViewModels.Size = new System.Drawing.Size(111, 24);
            this.RdViewModels.TabIndex = 15;
            this.RdViewModels.TabStop = true;
            this.RdViewModels.Text = "ViewModels";
            this.RdViewModels.UseVisualStyleBackColor = true;
            // 
            // RdController
            // 
            this.RdController.AutoSize = true;
            this.RdController.Location = new System.Drawing.Point(553, 267);
            this.RdController.Name = "RdController";
            this.RdController.Size = new System.Drawing.Size(96, 24);
            this.RdController.TabIndex = 17;
            this.RdController.TabStop = true;
            this.RdController.Text = "Controller";
            this.RdController.UseVisualStyleBackColor = true;
            // 
            // TxtConnectString
            // 
            this.TxtConnectString.Location = new System.Drawing.Point(145, 203);
            this.TxtConnectString.Name = "TxtConnectString";
            this.TxtConnectString.Size = new System.Drawing.Size(602, 27);
            this.TxtConnectString.TabIndex = 18;
            this.TxtConnectString.Text = "server=210.245.90.227;database=OnlineShop;uid=sonson;password=@Son@123456;Multipl" +
    "eActiveResultSets=True;TrustServerCertificate=True";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 203);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "ConnectString";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 623);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TxtConnectString);
            this.Controls.Add(this.RdController);
            this.Controls.Add(this.RdViewModels);
            this.Controls.Add(this.RdQueries);
            this.Controls.Add(this.RdMapper);
            this.Controls.Add(this.RdEFConfigs);
            this.Controls.Add(this.TxtOutput);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtRefix);
            this.Controls.Add(this.txtThuMuc);
            this.Controls.Add(this.TxtNameSpace);
            this.Controls.Add(this.txtModule);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtModule;
        private System.Windows.Forms.TextBox TxtNameSpace;
        private System.Windows.Forms.TextBox txtThuMuc;
        private System.Windows.Forms.TextBox txtRefix;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox TxtOutput;
        private System.Windows.Forms.RadioButton RdEFConfigs;
        private System.Windows.Forms.RadioButton RdMapper;
        private System.Windows.Forms.RadioButton RdQueries;
        private System.Windows.Forms.RadioButton RdViewModels;
        private System.Windows.Forms.RadioButton RdController;
        private System.Windows.Forms.TextBox TxtConnectString;
        private System.Windows.Forms.Label label5;
    }
}
