
namespace MyApiaryAdmin
{
    partial class FormAdminPanel
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdminPanel));
            this.label4 = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonAppClose = new System.Windows.Forms.Button();
            this.bunifuDragControlTop = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelSettings = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.bunifuFlatButtonMainSettingRomanCurtains = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bunifuFlatButtonMainSettingProtectiveCurtains = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bunifuFlatButtonSensors = new Bunifu.Framework.UI.BunifuFlatButton();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.userControlSensorsMenu1 = new MyApiaryAdmin.UserControlSensorsMenu();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
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
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(54)))));
            this.panelTop.Controls.Add(this.label4);
            this.panelTop.Controls.Add(this.pictureBox1);
            this.panelTop.Controls.Add(this.buttonAppClose);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1217, 26);
            this.panelTop.TabIndex = 9;
            this.panelTop.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTop_Paint);
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
            this.buttonAppClose.Location = new System.Drawing.Point(1191, 0);
            this.buttonAppClose.Name = "buttonAppClose";
            this.buttonAppClose.Size = new System.Drawing.Size(26, 26);
            this.buttonAppClose.TabIndex = 0;
            this.buttonAppClose.UseVisualStyleBackColor = false;
            this.buttonAppClose.Click += new System.EventHandler(this.buttonAppClose_Click);
            // 
            // bunifuDragControlTop
            // 
            this.bunifuDragControlTop.Fixed = true;
            this.bunifuDragControlTop.Horizontal = true;
            this.bunifuDragControlTop.TargetControl = this.panelTop;
            this.bunifuDragControlTop.Vertical = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(5)))), ((int)(((byte)(33)))));
            this.panel1.Controls.Add(this.labelSettings);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.bunifuFlatButtonMainSettingRomanCurtains);
            this.panel1.Controls.Add(this.bunifuFlatButtonMainSettingProtectiveCurtains);
            this.panel1.Controls.Add(this.bunifuFlatButtonSensors);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(262, 547);
            this.panel1.TabIndex = 10;
            // 
            // labelSettings
            // 
            this.labelSettings.AutoSize = true;
            this.labelSettings.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(123)))), ((int)(((byte)(140)))));
            this.labelSettings.Location = new System.Drawing.Point(23, 103);
            this.labelSettings.Name = "labelSettings";
            this.labelSettings.Size = new System.Drawing.Size(147, 23);
            this.labelSettings.TabIndex = 1;
            this.labelSettings.Text = "Налаштування";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::MyApiaryAdmin.Properties.Resources.beehello;
            this.pictureBox2.Location = new System.Drawing.Point(12, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(76, 94);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // bunifuFlatButtonMainSettingRomanCurtains
            // 
            this.bunifuFlatButtonMainSettingRomanCurtains.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(16)))), ((int)(((byte)(51)))));
            this.bunifuFlatButtonMainSettingRomanCurtains.BackColor = System.Drawing.Color.Transparent;
            this.bunifuFlatButtonMainSettingRomanCurtains.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bunifuFlatButtonMainSettingRomanCurtains.BorderRadius = 0;
            this.bunifuFlatButtonMainSettingRomanCurtains.ButtonText = "Юзери";
            this.bunifuFlatButtonMainSettingRomanCurtains.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuFlatButtonMainSettingRomanCurtains.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(5)))), ((int)(((byte)(33)))));
            this.bunifuFlatButtonMainSettingRomanCurtains.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuFlatButtonMainSettingRomanCurtains.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(72)))), ((int)(((byte)(93)))));
            this.bunifuFlatButtonMainSettingRomanCurtains.Iconcolor = System.Drawing.Color.Transparent;
            this.bunifuFlatButtonMainSettingRomanCurtains.Iconimage = ((System.Drawing.Image)(resources.GetObject("bunifuFlatButtonMainSettingRomanCurtains.Iconimage")));
            this.bunifuFlatButtonMainSettingRomanCurtains.Iconimage_right = null;
            this.bunifuFlatButtonMainSettingRomanCurtains.Iconimage_right_Selected = null;
            this.bunifuFlatButtonMainSettingRomanCurtains.Iconimage_Selected = null;
            this.bunifuFlatButtonMainSettingRomanCurtains.IconMarginLeft = 30;
            this.bunifuFlatButtonMainSettingRomanCurtains.IconMarginRight = 0;
            this.bunifuFlatButtonMainSettingRomanCurtains.IconRightVisible = false;
            this.bunifuFlatButtonMainSettingRomanCurtains.IconRightZoom = 0D;
            this.bunifuFlatButtonMainSettingRomanCurtains.IconVisible = true;
            this.bunifuFlatButtonMainSettingRomanCurtains.IconZoom = 55D;
            this.bunifuFlatButtonMainSettingRomanCurtains.IsTab = false;
            this.bunifuFlatButtonMainSettingRomanCurtains.Location = new System.Drawing.Point(1, 238);
            this.bunifuFlatButtonMainSettingRomanCurtains.Name = "bunifuFlatButtonMainSettingRomanCurtains";
            this.bunifuFlatButtonMainSettingRomanCurtains.Normalcolor = System.Drawing.Color.Transparent;
            this.bunifuFlatButtonMainSettingRomanCurtains.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(18)))), ((int)(((byte)(62)))));
            this.bunifuFlatButtonMainSettingRomanCurtains.OnHoverTextColor = System.Drawing.Color.Empty;
            this.bunifuFlatButtonMainSettingRomanCurtains.selected = false;
            this.bunifuFlatButtonMainSettingRomanCurtains.Size = new System.Drawing.Size(280, 41);
            this.bunifuFlatButtonMainSettingRomanCurtains.TabIndex = 9;
            this.bunifuFlatButtonMainSettingRomanCurtains.Text = "Юзери";
            this.bunifuFlatButtonMainSettingRomanCurtains.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bunifuFlatButtonMainSettingRomanCurtains.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(72)))), ((int)(((byte)(93)))));
            this.bunifuFlatButtonMainSettingRomanCurtains.TextFont = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // bunifuFlatButtonMainSettingProtectiveCurtains
            // 
            this.bunifuFlatButtonMainSettingProtectiveCurtains.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(16)))), ((int)(((byte)(51)))));
            this.bunifuFlatButtonMainSettingProtectiveCurtains.BackColor = System.Drawing.Color.Transparent;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.BorderRadius = 0;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.ButtonText = "Базові станції";
            this.bunifuFlatButtonMainSettingProtectiveCurtains.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(5)))), ((int)(((byte)(33)))));
            this.bunifuFlatButtonMainSettingProtectiveCurtains.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuFlatButtonMainSettingProtectiveCurtains.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(72)))), ((int)(((byte)(93)))));
            this.bunifuFlatButtonMainSettingProtectiveCurtains.Iconcolor = System.Drawing.Color.Transparent;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.Iconimage = ((System.Drawing.Image)(resources.GetObject("bunifuFlatButtonMainSettingProtectiveCurtains.Iconimage")));
            this.bunifuFlatButtonMainSettingProtectiveCurtains.Iconimage_right = null;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.Iconimage_right_Selected = null;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.Iconimage_Selected = null;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.IconMarginLeft = 30;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.IconMarginRight = 0;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.IconRightVisible = false;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.IconRightZoom = 0D;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.IconVisible = true;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.IconZoom = 55D;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.IsTab = false;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.Location = new System.Drawing.Point(1, 198);
            this.bunifuFlatButtonMainSettingProtectiveCurtains.Name = "bunifuFlatButtonMainSettingProtectiveCurtains";
            this.bunifuFlatButtonMainSettingProtectiveCurtains.Normalcolor = System.Drawing.Color.Transparent;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(18)))), ((int)(((byte)(62)))));
            this.bunifuFlatButtonMainSettingProtectiveCurtains.OnHoverTextColor = System.Drawing.Color.Empty;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.selected = false;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.Size = new System.Drawing.Size(280, 41);
            this.bunifuFlatButtonMainSettingProtectiveCurtains.TabIndex = 9;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.Text = "Базові станції";
            this.bunifuFlatButtonMainSettingProtectiveCurtains.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bunifuFlatButtonMainSettingProtectiveCurtains.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(72)))), ((int)(((byte)(93)))));
            this.bunifuFlatButtonMainSettingProtectiveCurtains.TextFont = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // bunifuFlatButtonSensors
            // 
            this.bunifuFlatButtonSensors.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(16)))), ((int)(((byte)(51)))));
            this.bunifuFlatButtonSensors.BackColor = System.Drawing.Color.Transparent;
            this.bunifuFlatButtonSensors.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bunifuFlatButtonSensors.BorderRadius = 0;
            this.bunifuFlatButtonSensors.ButtonText = "Датчики";
            this.bunifuFlatButtonSensors.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuFlatButtonSensors.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(5)))), ((int)(((byte)(33)))));
            this.bunifuFlatButtonSensors.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuFlatButtonSensors.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(72)))), ((int)(((byte)(93)))));
            this.bunifuFlatButtonSensors.Iconcolor = System.Drawing.Color.Transparent;
            this.bunifuFlatButtonSensors.Iconimage = ((System.Drawing.Image)(resources.GetObject("bunifuFlatButtonSensors.Iconimage")));
            this.bunifuFlatButtonSensors.Iconimage_right = null;
            this.bunifuFlatButtonSensors.Iconimage_right_Selected = null;
            this.bunifuFlatButtonSensors.Iconimage_Selected = null;
            this.bunifuFlatButtonSensors.IconMarginLeft = 30;
            this.bunifuFlatButtonSensors.IconMarginRight = 0;
            this.bunifuFlatButtonSensors.IconRightVisible = false;
            this.bunifuFlatButtonSensors.IconRightZoom = 0D;
            this.bunifuFlatButtonSensors.IconVisible = true;
            this.bunifuFlatButtonSensors.IconZoom = 55D;
            this.bunifuFlatButtonSensors.IsTab = false;
            this.bunifuFlatButtonSensors.Location = new System.Drawing.Point(1, 158);
            this.bunifuFlatButtonSensors.Name = "bunifuFlatButtonSensors";
            this.bunifuFlatButtonSensors.Normalcolor = System.Drawing.Color.Transparent;
            this.bunifuFlatButtonSensors.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(18)))), ((int)(((byte)(62)))));
            this.bunifuFlatButtonSensors.OnHoverTextColor = System.Drawing.Color.Empty;
            this.bunifuFlatButtonSensors.selected = false;
            this.bunifuFlatButtonSensors.Size = new System.Drawing.Size(280, 41);
            this.bunifuFlatButtonSensors.TabIndex = 9;
            this.bunifuFlatButtonSensors.Text = "Датчики";
            this.bunifuFlatButtonSensors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bunifuFlatButtonSensors.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(72)))), ((int)(((byte)(93)))));
            this.bunifuFlatButtonSensors.TextFont = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuFlatButtonSensors.Click += new System.EventHandler(this.bunifuFlatButtonSensors_Click);
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.userControlSensorsMenu1);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(262, 26);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(955, 547);
            this.panelContainer.TabIndex = 11;
            // 
            // userControlSensorsMenu1
            // 
            this.userControlSensorsMenu1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlSensorsMenu1.Location = new System.Drawing.Point(0, 0);
            this.userControlSensorsMenu1.Name = "userControlSensorsMenu1";
            this.userControlSensorsMenu1.Size = new System.Drawing.Size(955, 547);
            this.userControlSensorsMenu1.TabIndex = 0;
            // 
            // FormAdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(1217, 573);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAdminPanel";
            this.Text = "FormAdminPanel";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panelContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button buttonAppClose;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControlTop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelSettings;
        internal Bunifu.Framework.UI.BunifuFlatButton bunifuFlatButtonMainSettingRomanCurtains;
        internal Bunifu.Framework.UI.BunifuFlatButton bunifuFlatButtonMainSettingProtectiveCurtains;
        internal Bunifu.Framework.UI.BunifuFlatButton bunifuFlatButtonSensors;
        private System.Windows.Forms.Panel panelContainer;
        private UserControlSensorsMenu userControlSensorsMenu1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}