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
    public partial class FormAuth : Form
    {
        public FormAuth()
        {
            InitializeComponent();
        }

        private void buttonAppClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonAuthLogPass_Click(object sender, EventArgs e)
        {
            buttonAuthLogPass.Enabled = false;

            if(Textbox_Mail.Text.Equals("") || Textbox_Password.Text.Equals(""))
            {
                MessageBox.Show("Введіть логін та пароль.");
                buttonAuthLogPass.Enabled = true;
                return;
            }

            var user = new AuthLogin()
            {
                Email = Textbox_Mail.Text,
                Password = Textbox_Password.Text
            };

            bool auth = Request.AuthLogin(user);

            if (auth)
            {
                FormAdminPanel adminPanel = new FormAdminPanel();
                adminPanel.Show();
                this.Hide();
            }

            buttonAuthLogPass.Enabled = true;
        }
    }
}
