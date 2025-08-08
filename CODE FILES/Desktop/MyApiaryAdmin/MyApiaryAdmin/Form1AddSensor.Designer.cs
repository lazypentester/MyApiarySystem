
namespace MyApiaryAdmin
{
    partial class Form1AddSensor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1AddSensor));
            this.panelTop = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonAppClose = new System.Windows.Forms.Button();
            this.bunifuDragControlTop = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.labelName = new System.Windows.Forms.Label();
            this.numericMinValue = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCreateNewSensor = new System.Windows.Forms.Button();
            this.numericMaxValue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Textbox_Serial_number = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxBeehive = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxBaseStation = new System.Windows.Forms.ComboBox();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.comboBoxSensorTypee = new System.Windows.Forms.ComboBox();
            this.comboBoxWorking = new System.Windows.Forms.ComboBox();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxValue)).BeginInit();
            this.SuspendLayout();
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
            this.panelTop.Size = new System.Drawing.Size(385, 26);
            this.panelTop.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(24, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "MyApiaryAdmin - Add Sensor";
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
            this.buttonAppClose.Location = new System.Drawing.Point(359, 0);
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
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.labelName.Location = new System.Drawing.Point(121, 50);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(131, 19);
            this.labelName.TabIndex = 10;
            this.labelName.Text = "Нова категорія";
            // 
            // numericMinValue
            // 
            this.numericMinValue.DecimalPlaces = 2;
            this.numericMinValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericMinValue.Location = new System.Drawing.Point(238, 126);
            this.numericMinValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericMinValue.Name = "numericMinValue";
            this.numericMinValue.Size = new System.Drawing.Size(53, 21);
            this.numericMinValue.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.label6.Location = new System.Drawing.Point(346, 312);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = "$";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.label2.Location = new System.Drawing.Point(58, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 18);
            this.label2.TabIndex = 14;
            this.label2.Text = "Мінімальне значення:";
            // 
            // buttonCreateNewSensor
            // 
            this.buttonCreateNewSensor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.buttonCreateNewSensor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCreateNewSensor.FlatAppearance.BorderSize = 0;
            this.buttonCreateNewSensor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreateNewSensor.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCreateNewSensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonCreateNewSensor.Location = new System.Drawing.Point(78, 439);
            this.buttonCreateNewSensor.Name = "buttonCreateNewSensor";
            this.buttonCreateNewSensor.Size = new System.Drawing.Size(226, 34);
            this.buttonCreateNewSensor.TabIndex = 18;
            this.buttonCreateNewSensor.Text = "Додати сенсор";
            this.buttonCreateNewSensor.UseVisualStyleBackColor = false;
            this.buttonCreateNewSensor.Click += new System.EventHandler(this.buttonCreateNewSensor_Click);
            // 
            // numericMaxValue
            // 
            this.numericMaxValue.DecimalPlaces = 2;
            this.numericMaxValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericMaxValue.Location = new System.Drawing.Point(258, 161);
            this.numericMaxValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericMaxValue.Name = "numericMaxValue";
            this.numericMaxValue.Size = new System.Drawing.Size(53, 21);
            this.numericMaxValue.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.label1.Location = new System.Drawing.Point(58, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "Максимальне значення:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.label7.Location = new System.Drawing.Point(58, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 18);
            this.label7.TabIndex = 21;
            this.label7.Text = "В роботі:";
            // 
            // Textbox_Serial_number
            // 
            this.Textbox_Serial_number.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Textbox_Serial_number.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.Textbox_Serial_number.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Textbox_Serial_number.HintForeColor = System.Drawing.Color.Empty;
            this.Textbox_Serial_number.HintText = "";
            this.Textbox_Serial_number.isPassword = false;
            this.Textbox_Serial_number.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.Textbox_Serial_number.LineIdleColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(104)))), ((int)(((byte)(153)))));
            this.Textbox_Serial_number.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.Textbox_Serial_number.LineThickness = 3;
            this.Textbox_Serial_number.Location = new System.Drawing.Point(179, 228);
            this.Textbox_Serial_number.Margin = new System.Windows.Forms.Padding(4);
            this.Textbox_Serial_number.Name = "Textbox_Serial_number";
            this.Textbox_Serial_number.Size = new System.Drawing.Size(148, 34);
            this.Textbox_Serial_number.TabIndex = 22;
            this.Textbox_Serial_number.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.label8.Location = new System.Drawing.Point(33, 244);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 18);
            this.label8.TabIndex = 23;
            this.label8.Text = "Серійний номер:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.label3.Location = new System.Drawing.Point(46, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 18);
            this.label3.TabIndex = 24;
            this.label3.Text = "Тип датчика:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.label5.Location = new System.Drawing.Point(46, 329);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 18);
            this.label5.TabIndex = 26;
            this.label5.Text = "Вулик:";
            // 
            // comboBoxBeehive
            // 
            this.comboBoxBeehive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBeehive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBeehive.FormattingEnabled = true;
            this.comboBoxBeehive.Location = new System.Drawing.Point(108, 328);
            this.comboBoxBeehive.Name = "comboBoxBeehive";
            this.comboBoxBeehive.Size = new System.Drawing.Size(204, 21);
            this.comboBoxBeehive.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.label9.Location = new System.Drawing.Point(33, 366);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(129, 18);
            this.label9.TabIndex = 28;
            this.label9.Text = "Базова станція:";
            // 
            // comboBoxBaseStation
            // 
            this.comboBoxBaseStation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBaseStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBaseStation.FormattingEnabled = true;
            this.comboBoxBaseStation.Location = new System.Drawing.Point(168, 366);
            this.comboBoxBaseStation.Name = "comboBoxBaseStation";
            this.comboBoxBaseStation.Size = new System.Drawing.Size(144, 21);
            this.comboBoxBaseStation.TabIndex = 27;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 3;
            this.bunifuElipse1.TargetControl = this.buttonCreateNewSensor;
            // 
            // comboBoxSensorTypee
            // 
            this.comboBoxSensorTypee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSensorTypee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSensorTypee.FormattingEnabled = true;
            this.comboBoxSensorTypee.Location = new System.Drawing.Point(156, 288);
            this.comboBoxSensorTypee.Name = "comboBoxSensorTypee";
            this.comboBoxSensorTypee.Size = new System.Drawing.Size(156, 21);
            this.comboBoxSensorTypee.TabIndex = 29;
            // 
            // comboBoxWorking
            // 
            this.comboBoxWorking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxWorking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWorking.FormattingEnabled = true;
            this.comboBoxWorking.Location = new System.Drawing.Point(142, 199);
            this.comboBoxWorking.Name = "comboBoxWorking";
            this.comboBoxWorking.Size = new System.Drawing.Size(106, 21);
            this.comboBoxWorking.TabIndex = 30;
            // 
            // Form1AddSensor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(385, 508);
            this.Controls.Add(this.comboBoxWorking);
            this.Controls.Add(this.comboBoxSensorTypee);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBoxBaseStation);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxBeehive);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Textbox_Serial_number);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericMaxValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCreateNewSensor);
            this.Controls.Add(this.numericMinValue);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1AddSensor";
            this.Text = "Form1AddSensor";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonAppClose;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControlTop;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.NumericUpDown numericMinValue;
        internal System.Windows.Forms.ComboBox comboBoxSensorType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCreateNewSensor;
        private System.Windows.Forms.NumericUpDown numericMaxValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private Bunifu.Framework.UI.BunifuMaterialTextbox Textbox_Serial_number;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.ComboBox comboBoxBeehive;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.ComboBox comboBoxBaseStation;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        internal System.Windows.Forms.ComboBox comboBoxSensorTypee;
        internal System.Windows.Forms.ComboBox comboBoxWorking;
    }
}