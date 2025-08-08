
namespace MyApiaryAdmin
{
    partial class UserControlSensorsMenu
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bunifuCustomDataGridSensorsDataBase = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.ColumnSensorId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMinValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCurrentValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWorking = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTypeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBeehiveId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBaseStationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.buttonAddSensor = new System.Windows.Forms.Button();
            this.bunifuElipseButtonAdd = new Bunifu.Framework.UI.BunifuElipse(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuCustomDataGridSensorsDataBase)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuCustomDataGridSensorsDataBase
            // 
            this.bunifuCustomDataGridSensorsDataBase.AllowUserToAddRows = false;
            this.bunifuCustomDataGridSensorsDataBase.AllowUserToDeleteRows = false;
            this.bunifuCustomDataGridSensorsDataBase.AllowUserToResizeColumns = false;
            this.bunifuCustomDataGridSensorsDataBase.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bunifuCustomDataGridSensorsDataBase.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.bunifuCustomDataGridSensorsDataBase.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.bunifuCustomDataGridSensorsDataBase.BackgroundColor = System.Drawing.Color.DarkGray;
            this.bunifuCustomDataGridSensorsDataBase.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bunifuCustomDataGridSensorsDataBase.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.bunifuCustomDataGridSensorsDataBase.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bunifuCustomDataGridSensorsDataBase.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.bunifuCustomDataGridSensorsDataBase.ColumnHeadersHeight = 50;
            this.bunifuCustomDataGridSensorsDataBase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.bunifuCustomDataGridSensorsDataBase.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSensorId,
            this.ColumnMinValue,
            this.ColumnMaxValue,
            this.ColumnCurrentValue,
            this.ColumnWorking,
            this.ColumnSerialNumber,
            this.ColumnTypeId,
            this.ColumnBeehiveId,
            this.ColumnBaseStationId});
            this.bunifuCustomDataGridSensorsDataBase.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.bunifuCustomDataGridSensorsDataBase.DefaultCellStyle = dataGridViewCellStyle11;
            this.bunifuCustomDataGridSensorsDataBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bunifuCustomDataGridSensorsDataBase.DoubleBuffered = true;
            this.bunifuCustomDataGridSensorsDataBase.EnableHeadersVisualStyles = false;
            this.bunifuCustomDataGridSensorsDataBase.GridColor = System.Drawing.Color.DimGray;
            this.bunifuCustomDataGridSensorsDataBase.HeaderBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.bunifuCustomDataGridSensorsDataBase.HeaderForeColor = System.Drawing.Color.White;
            this.bunifuCustomDataGridSensorsDataBase.Location = new System.Drawing.Point(0, 77);
            this.bunifuCustomDataGridSensorsDataBase.Name = "bunifuCustomDataGridSensorsDataBase";
            this.bunifuCustomDataGridSensorsDataBase.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.bunifuCustomDataGridSensorsDataBase.RowHeadersVisible = false;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.bunifuCustomDataGridSensorsDataBase.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.bunifuCustomDataGridSensorsDataBase.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.bunifuCustomDataGridSensorsDataBase.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.bunifuCustomDataGridSensorsDataBase.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bunifuCustomDataGridSensorsDataBase.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.bunifuCustomDataGridSensorsDataBase.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Gray;
            this.bunifuCustomDataGridSensorsDataBase.RowTemplate.DividerHeight = 1;
            this.bunifuCustomDataGridSensorsDataBase.RowTemplate.Height = 25;
            this.bunifuCustomDataGridSensorsDataBase.RowTemplate.ReadOnly = true;
            this.bunifuCustomDataGridSensorsDataBase.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.bunifuCustomDataGridSensorsDataBase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.bunifuCustomDataGridSensorsDataBase.Size = new System.Drawing.Size(955, 470);
            this.bunifuCustomDataGridSensorsDataBase.TabIndex = 14;
            // 
            // ColumnSensorId
            // 
            this.ColumnSensorId.FillWeight = 21.87676F;
            this.ColumnSensorId.HeaderText = "Id";
            this.ColumnSensorId.Name = "ColumnSensorId";
            // 
            // ColumnMinValue
            // 
            this.ColumnMinValue.FillWeight = 86.13687F;
            this.ColumnMinValue.HeaderText = "MinV";
            this.ColumnMinValue.Name = "ColumnMinValue";
            // 
            // ColumnMaxValue
            // 
            this.ColumnMaxValue.FillWeight = 84.61188F;
            this.ColumnMaxValue.HeaderText = "MaxV";
            this.ColumnMaxValue.Name = "ColumnMaxValue";
            // 
            // ColumnCurrentValue
            // 
            this.ColumnCurrentValue.FillWeight = 82.91457F;
            this.ColumnCurrentValue.HeaderText = "Value";
            this.ColumnCurrentValue.Name = "ColumnCurrentValue";
            // 
            // ColumnWorking
            // 
            this.ColumnWorking.FillWeight = 110.1051F;
            this.ColumnWorking.HeaderText = "Working";
            this.ColumnWorking.Name = "ColumnWorking";
            // 
            // ColumnSerialNumber
            // 
            this.ColumnSerialNumber.FillWeight = 197.4676F;
            this.ColumnSerialNumber.HeaderText = "SerialNumber";
            this.ColumnSerialNumber.Name = "ColumnSerialNumber";
            // 
            // ColumnTypeId
            // 
            this.ColumnTypeId.FillWeight = 92.56827F;
            this.ColumnTypeId.HeaderText = "TypeId";
            this.ColumnTypeId.Name = "ColumnTypeId";
            // 
            // ColumnBeehiveId
            // 
            this.ColumnBeehiveId.FillWeight = 92.56827F;
            this.ColumnBeehiveId.HeaderText = "BeehiveId";
            this.ColumnBeehiveId.Name = "ColumnBeehiveId";
            // 
            // ColumnBaseStationId
            // 
            this.ColumnBaseStationId.FillWeight = 92.56827F;
            this.ColumnBaseStationId.HeaderText = "BaseStationId";
            this.ColumnBaseStationId.Name = "ColumnBaseStationId";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(5)))), ((int)(((byte)(33)))));
            this.panel4.Controls.Add(this.buttonAddSensor);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(955, 77);
            this.panel4.TabIndex = 13;
            // 
            // buttonAddSensor
            // 
            this.buttonAddSensor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(57)))));
            this.buttonAddSensor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAddSensor.FlatAppearance.BorderSize = 0;
            this.buttonAddSensor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddSensor.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddSensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonAddSensor.Location = new System.Drawing.Point(26, 17);
            this.buttonAddSensor.Name = "buttonAddSensor";
            this.buttonAddSensor.Size = new System.Drawing.Size(226, 40);
            this.buttonAddSensor.TabIndex = 15;
            this.buttonAddSensor.Text = "Додати датчик";
            this.buttonAddSensor.UseVisualStyleBackColor = false;
            this.buttonAddSensor.Click += new System.EventHandler(this.buttonAddSensor_Click);
            // 
            // bunifuElipseButtonAdd
            // 
            this.bunifuElipseButtonAdd.ElipseRadius = 3;
            this.bunifuElipseButtonAdd.TargetControl = this.buttonAddSensor;
            // 
            // UserControlSensorsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bunifuCustomDataGridSensorsDataBase);
            this.Controls.Add(this.panel4);
            this.Name = "UserControlSensorsMenu";
            this.Size = new System.Drawing.Size(955, 547);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuCustomDataGridSensorsDataBase)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel4;
        public Bunifu.Framework.UI.BunifuCustomDataGrid bunifuCustomDataGridSensorsDataBase;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSensorId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMinValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMaxValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCurrentValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWorking;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTypeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBeehiveId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBaseStationId;
        private System.Windows.Forms.Button buttonAddSensor;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipseButtonAdd;
    }
}
