using MyApiaryAdmin.Helpers;
using MyApiaryAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApiaryAdmin
{
    public partial class Form1AddSensor : Form
    {

        BindingList<KeyValuePair<string, int>> pairWorking = new BindingList<KeyValuePair<string, int>>();
        BindingList<KeyValuePair<string, int>> pairSensorTypes = new BindingList<KeyValuePair<string, int>>();
        BindingList<KeyValuePair<string, int>> pairBeehives = new BindingList<KeyValuePair<string, int>>();
        BindingList<KeyValuePair<string, int>> BaseStations = new BindingList<KeyValuePair<string, int>>();

        public Form1AddSensor()
        {
            InitializeComponent();
            loadTypes();
        }

        private async void loadTypes()
        {
            await Task.Run(() => load_combobox_types());
        }

        private void load_combobox_types()
        {
            try
            {
                if (comboBoxWorking.InvokeRequired)
                {
                    comboBoxWorking.Invoke((MethodInvoker)delegate
                    {
                        comboBoxWorking.DisplayMember = "Key";
                        comboBoxWorking.ValueMember = "Value";
                        comboBoxWorking.DataSource = pairWorking;
                        pairWorking.Clear();
                        pairWorking.Add(new KeyValuePair<string, int>("Так", 1));
                        pairWorking.Add(new KeyValuePair<string, int>("Ні", 2));
                    });
                }
                else
                {
                    comboBoxWorking.DisplayMember = "Key";
                    comboBoxWorking.ValueMember = "Value";
                    comboBoxWorking.DataSource = pairWorking;
                    pairWorking.Clear();
                    pairWorking.Add(new KeyValuePair<string, int>("Так", 1));
                    pairWorking.Add(new KeyValuePair<string, int>("Ні", 2));
                }
               ////////////////////////
                if (comboBoxSensorTypee.InvokeRequired)
                {
                    comboBoxSensorTypee.Invoke((MethodInvoker)delegate
                    {
                        comboBoxSensorTypee.DisplayMember = "Key";
                        comboBoxSensorTypee.ValueMember = "Value";
                        comboBoxSensorTypee.DataSource = pairSensorTypes;
                        pairSensorTypes.Clear();
                        foreach(var item in Storage.Storage.sensorTypes)
                        {
                            pairSensorTypes.Add(new KeyValuePair<string, int>(item.Name, item.Id));
                        }
                    });
                }
                else
                {
                    comboBoxSensorTypee.DisplayMember = "Key";
                    comboBoxSensorTypee.ValueMember = "Value";
                    comboBoxSensorTypee.DataSource = pairSensorTypes;
                    pairSensorTypes.Clear();
                    foreach (var item in Storage.Storage.sensorTypes)
                    {
                        pairSensorTypes.Add(new KeyValuePair<string, int>(item.Name, item.Id));
                    }
                }
                ////////////////////////
                if (comboBoxBeehive.InvokeRequired)
                {
                    comboBoxBeehive.Invoke((MethodInvoker)delegate
                    {
                        comboBoxBeehive.DisplayMember = "Key";
                        comboBoxBeehive.ValueMember = "Value";
                        comboBoxBeehive.DataSource = pairBeehives;
                        pairBeehives.Clear();
                        foreach (var item in Storage.Storage.beehives)
                        {
                            pairBeehives.Add(new KeyValuePair<string, int>(item.Name, item.Id));
                        }
                    });
                }
                else
                {
                    comboBoxBeehive.DisplayMember = "Key";
                    comboBoxBeehive.ValueMember = "Value";
                    comboBoxBeehive.DataSource = pairBeehives;
                    pairBeehives.Clear();
                    foreach (var item in Storage.Storage.beehives)
                    {
                        pairBeehives.Add(new KeyValuePair<string, int>(item.Name, item.Id));
                    }
                }
                ////////////////////////
                if (comboBoxBaseStation.InvokeRequired)
                {
                    comboBoxBaseStation.Invoke((MethodInvoker)delegate
                    {
                        comboBoxBaseStation.DisplayMember = "Key";
                        comboBoxBaseStation.ValueMember = "Value";
                        comboBoxBaseStation.DataSource = BaseStations;
                        BaseStations.Clear();
                        foreach (var item in Storage.Storage.baseStations)
                        {
                            BaseStations.Add(new KeyValuePair<string, int>(item.Name, item.Id));
                        }
                    });
                }
                else
                {
                    comboBoxBaseStation.DisplayMember = "Key";
                    comboBoxBaseStation.ValueMember = "Value";
                    comboBoxBaseStation.DataSource = BaseStations;
                    BaseStations.Clear();
                    foreach (var item in Storage.Storage.baseStations)
                    {
                        BaseStations.Add(new KeyValuePair<string, int>(item.Name, item.Id));
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAppClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCreateNewSensor_Click(object sender, EventArgs e)
        {
            if (comboBoxBaseStation.SelectedValue == null)
            {
                MessageBox.Show("Заповніть будь-ласка всі поля.");
                return;
            }

            SensorContext sensor = new SensorContext();
            sensor.Min_value = (float)numericMinValue.Value;
            sensor.Max_value = (float)numericMaxValue.Value;
            sensor.Value = 0;
            if (Convert.ToInt32(comboBoxWorking.SelectedValue) == 1)
                sensor.Is_working = true;
            else
                sensor.Is_working = false;
            sensor.Serial_number = Textbox_Serial_number.Text;
            sensor.SensorTypeId = Convert.ToInt32(comboBoxSensorTypee.SelectedValue);
            sensor.BeehiveId = Convert.ToInt32(comboBoxBeehive.SelectedValue);
            sensor.BaseStationId = Convert.ToInt32(comboBoxBaseStation.SelectedValue);

            HttpStatusCode httpStatus = Request.CreateNewSensor(sensor);

            if (httpStatus == HttpStatusCode.OK)
            {
                MessageBox.Show("Новий датчик успішно створений");
                this.Close();
            } 
            else
            {
                switch (Convert.ToInt32(httpStatus))
                {
                    case 410:
                        MessageBox.Show("В цьому вулику вже є датчик такого типу, оберіть інший.");
                        break;
                }
            }
        }
    }
}
