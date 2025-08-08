using MyApiaryAdmin.Helpers;
using MyApiaryAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApiaryAdmin
{
    public partial class UserControlSensorsMenu : UserControl
    {
        internal static List<SensorContext> Sensors;

        public UserControlSensorsMenu()
        {
            InitializeComponent();
        }

        internal async void load_sensors()
        {
            await Task.Run(() => getAllSensors());
        }

        private void getAllSensors()
        {
            List<SensorContext> responce = Request.GetAllSensors();
            if (responce != null)
            {
                Sensors = responce;
            }

            insert_data_in_dataGrid();
        }

        private void insert_data_in_dataGrid()
        {
            if (Sensors == null)
                return;

            if (bunifuCustomDataGridSensorsDataBase.Rows.Count != 0)
            {
                if (bunifuCustomDataGridSensorsDataBase.InvokeRequired)
                    bunifuCustomDataGridSensorsDataBase.Invoke((MethodInvoker)delegate
                    {
                        bunifuCustomDataGridSensorsDataBase.Rows.Clear();
                    });
                else
                    bunifuCustomDataGridSensorsDataBase.Rows.Clear();
            }

            if (bunifuCustomDataGridSensorsDataBase.InvokeRequired)
            {
                bunifuCustomDataGridSensorsDataBase.Invoke((MethodInvoker)delegate
                {
                    foreach (SensorContext sensor in Sensors)
                        bunifuCustomDataGridSensorsDataBase.Rows.Add(new object[] { sensor.Id.ToString(), sensor.Min_value.ToString(), sensor.Max_value.ToString(), sensor.Value.ToString(), sensor.Is_working.ToString(), sensor.Serial_number.ToString(), sensor.SensorTypeId.ToString(), sensor.BeehiveId.ToString(), sensor.BaseStationId.ToString() });

                });
            }
            else
            {
                foreach (SensorContext sensor in Sensors)
                    bunifuCustomDataGridSensorsDataBase.Rows.Add(new object[] { sensor.Id.ToString(), sensor.Min_value.ToString(), sensor.Max_value.ToString(), sensor.Value.ToString(), sensor.Is_working.ToString(), sensor.Serial_number.ToString(), sensor.SensorTypeId.ToString(), sensor.BeehiveId.ToString(), sensor.BaseStationId.ToString() });
            }
        }

        private void buttonAddSensor_Click(object sender, EventArgs e)
        {
            Form1AddSensor form1AddSensor = new Form1AddSensor();
            form1AddSensor.ShowDialog();
        }
    }
}
