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
using System.Timers;
using System.Windows.Forms;

namespace MyApiaryAdmin
{
    public partial class FormAdminPanel : Form
    {
        private UserControl activeControl;
        private UserControlSensorsMenu control_sensors;
        private System.Timers.Timer aTimer;

        public FormAdminPanel()
        {
            InitializeComponent();
            addUserCintrollers();
            GetSensorDepend();
        }


        private void addUserCintrollers()
        {
            this.panelContainer.Controls.Add(control_sensors = new UserControlSensorsMenu()); control_sensors.Hide();
        }

        private void OpenChildControl(UserControl childControl, object sender)
        {
            if (activeControl != null)
                activeControl.Hide();
            try
            {
                activeControl = childControl ?? throw new NullReferenceException();
                childControl.Dock = DockStyle.Fill;
                this.panelContainer.Tag = childControl;
                childControl.BringToFront();
                childControl.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAppClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuFlatButtonSensors_Click(object sender, EventArgs e)
        {
            OpenChildControl(control_sensors, sender);
            start_timer_update_sensors();

        }

        private void start_timer_update_sensors()
        {
            if (aTimer != null)
                return;

            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(updateSensors);
            aTimer.Interval = 10000;
            aTimer.Enabled = true;
            control_sensors.load_sensors();
        }

        private void updateSensors(object source, ElapsedEventArgs e)
        {
            Request.updateSensorsAuto(UserControlSensorsMenu.Sensors);
            control_sensors.load_sensors();
        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }

        public void GetSensorDepend()
        {
            Storage.Storage.sensorTypes = Request.GetSensorTypes();
            Storage.Storage.beehives = Request.GetBeehives();
            Storage.Storage.baseStations = Request.GetBaseStations();
        }
    }
}
