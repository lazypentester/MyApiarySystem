
namespace MyApiaryAdmin
{
    partial class FormAuth
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAuth));
            this.label6 = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonAppClose = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonAuthLogPass = new System.Windows.Forms.Button();
            this.Textbox_Mail = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.Textbox_Password = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.bunifuDragControlMovePanel = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.linkLabelAuthToken = new System.Windows.Forms.LinkLabel();
            this.bunifuElipseButtonLogin = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuElipseWindowAuth = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label6.Location = new System.Drawing.Point(134, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 22);
            this.label6.TabIndex = 7;
            this.label6.Text = "Авторизація";
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(54)))));
            this.panelTop.Controls.Add(this.label4);
            this.panelTop.Controls.Add(this.pictureBox1);
            this.panelTop.Controls.Add(this.buttonAppClose);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(397, 26);
            this.panelTop.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(24, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "MyApiaryAdmin";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MyApiaryAdmin.Properties.Resources.beehello;
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(19, 19);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // buttonAppClose
            // 
            this.buttonAppClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(54)))));
            this.buttonAppClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAppClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonAppClose.FlatAppearance.BorderSize = 0;
            this.buttonAppClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(37)))), ((int)(((byte)(57)))));
            this.buttonAppClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAppClose.Image = ((System.Drawing.Image)(resources.GetObject("buttonAppClose.Image")));
            this.buttonAppClose.Location = new System.Drawing.Point(371, 0);
            this.buttonAppClose.Name = "buttonAppClose";
            this.buttonAppClose.Size = new System.Drawing.Size(26, 26);
            this.buttonAppClose.TabIndex = 0;
            this.buttonAppClose.UseVisualStyleBackColor = false;
            this.buttonAppClose.Click += new System.EventHandler(this.buttonAppClose_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::MyApiaryAdmin.Properties.Resources.beehello;
            this.pictureBox2.Location = new System.Drawing.Point(133, 78);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(118, 122);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label11.Location = new System.Drawing.Point(60, 292);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 21);
            this.label11.TabIndex = 10;
            this.label11.Text = "Пароль:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label10.Location = new System.Drawing.Point(60, 214);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 21);
            this.label10.TabIndex = 11;
            this.label10.Text = "Почта:";
            // 
            // buttonAuthLogPass
            // 
            this.buttonAuthLogPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.buttonAuthLogPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAuthLogPass.FlatAppearance.BorderSize = 0;
            this.buttonAuthLogPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAuthLogPass.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAuthLogPass.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonAuthLogPass.Location = new System.Drawing.Point(82, 381);
            this.buttonAuthLogPass.Name = "buttonAuthLogPass";
            this.buttonAuthLogPass.Size = new System.Drawing.Size(226, 34);
            this.buttonAuthLogPass.TabIndex = 12;
            this.buttonAuthLogPass.Text = "Увійти в систему";
            this.buttonAuthLogPass.UseVisualStyleBackColor = false;
            this.buttonAuthLogPass.Click += new System.EventHandler(this.buttonAuthLogPass_Click);
            // 
            // Textbox_Mail
            // 
            this.Textbox_Mail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Textbox_Mail.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.Textbox_Mail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Textbox_Mail.HintForeColor = System.Drawing.Color.Empty;
            this.Textbox_Mail.HintText = "";
            this.Textbox_Mail.isPassword = false;
            this.Textbox_Mail.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.Textbox_Mail.LineIdleColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(104)))), ((int)(((byte)(153)))));
            this.Textbox_Mail.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.Textbox_Mail.LineThickness = 3;
            this.Textbox_Mail.Location = new System.Drawing.Point(64, 239);
            this.Textbox_Mail.Margin = new System.Windows.Forms.Padding(4);
            this.Textbox_Mail.Name = "Textbox_Mail";
            this.Textbox_Mail.Size = new System.Drawing.Size(266, 34);
            this.Textbox_Mail.TabIndex = 13;
            this.Textbox_Mail.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // Textbox_Password
            // 
            this.Textbox_Password.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Textbox_Password.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.Textbox_Password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Textbox_Password.HintForeColor = System.Drawing.Color.Empty;
            this.Textbox_Password.HintText = "";
            this.Textbox_Password.isPassword = false;
            this.Textbox_Password.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.Textbox_Password.LineIdleColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(104)))), ((int)(((byte)(153)))));
            this.Textbox_Password.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.Textbox_Password.LineThickness = 3;
            this.Textbox_Password.Location = new System.Drawing.Point(64, 317);
            this.Textbox_Password.Margin = new System.Windows.Forms.Padding(4);
            this.Textbox_Password.Name = "Textbox_Password";
            this.Textbox_Password.Size = new System.Drawing.Size(266, 34);
            this.Textbox_Password.TabIndex = 14;
            this.Textbox_Password.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // bunifuDragControlMovePanel
            // 
            this.bunifuDragControlMovePanel.Fixed = true;
            this.bunifuDragControlMovePanel.Horizontal = true;
            this.bunifuDragControlMovePanel.TargetControl = this.panelTop;
            this.bunifuDragControlMovePanel.Vertical = true;
            // 
            // linkLabelAuthToken
            // 
            this.linkLabelAuthToken.AutoSize = true;
            this.linkLabelAuthToken.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabelAuthToken.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(104)))), ((int)(((byte)(153)))));
            this.linkLabelAuthToken.Location = new System.Drawing.Point(130, 428);
            this.linkLabelAuthToken.Name = "linkLabelAuthToken";
            this.linkLabelAuthToken.Size = new System.Drawing.Size(128, 18);
            this.linkLabelAuthToken.TabIndex = 15;
            this.linkLabelAuthToken.TabStop = true;
            this.linkLabelAuthToken.Text = "Увійти з токеном";
            // 
            // bunifuElipseButtonLogin
            // 
            this.bunifuElipseButtonLogin.ElipseRadius = 3;
            this.bunifuElipseButtonLogin.TargetControl = this.buttonAuthLogPass;
            // 
            // bunifuElipseWindowAuth
            // 
            this.bunifuElipseWindowAuth.ElipseRadius = 5;
            this.bunifuElipseWindowAuth.TargetControl = this;
            // 
            // FormAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(397, 501);
            this.Controls.Add(this.linkLabelAuthToken);
            this.Controls.Add(this.Textbox_Password);
            this.Controls.Add(this.Textbox_Mail);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.buttonAuthLogPass);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAuth";
            this.Text = "Form1";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonAppClose;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonAuthLogPass;
        private Bunifu.Framework.UI.BunifuMaterialTextbox Textbox_Mail;
        private Bunifu.Framework.UI.BunifuMaterialTextbox Textbox_Password;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControlMovePanel;
        private System.Windows.Forms.LinkLabel linkLabelAuthToken;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipseButtonLogin;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipseWindowAuth;
    }
}

