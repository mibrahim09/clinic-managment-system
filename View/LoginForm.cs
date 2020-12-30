using HospitalManagmentSystem.Controller;
using HospitalManagmentSystem.Models;
using HospitalManagmentSystem.View;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagmentSystem
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        void updateControls(bool enabled)
        {
            textBox1.Enabled = textBox2.Enabled = radioButton1.Enabled = radioButton2.Enabled = radioButton3.Enabled = enabled;
        }
        void unlockControls()
        {
            updateControls(true);
        }
        void lockControls()
        {
            updateControls(false);
        }
        UserRoles getUserType()
        {
            if (radioButton1.Checked) return UserRoles.SystemAdmin;
            if (radioButton2.Checked) return UserRoles.Doctor;
            if (radioButton3.Checked) return UserRoles.Accountant;
            return UserRoles.Unknown;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            lockControls();
            try
            {
                using (var conn = new MySqlConnection(Config.ConnectionString))
                {
                    using (var cmd = new MySqlCommand($"SELECT * from users where Username='{textBox1.Text}' and Password ='{textBox2.Text}' and Role='{(byte)getUserType()}'", conn))// idc much about sql injection here lol
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            var Type = getUserType();
                            if (Type == UserRoles.SystemAdmin)
                            {
                                var pAdmin = new AdminPanelForm();
                                this.Hide();
                                pAdmin.ShowDialog();
                            }
                            else if (Type == UserRoles.Doctor)
                            {
                                var pAdmin = new DoctorPanelForm();
                                this.Hide();
                                pAdmin.ShowDialog();
                            }
                            else if (Type == UserRoles.Accountant)
                            {
                                var pAdmin = new AccountantPanelForm();
                                this.Hide();
                                pAdmin.ShowDialog();
                            }
                            else
                                MessageBox.Show("Unknown user error!");

                        }
                        else
                            MessageBox.Show("Invalid login info!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                unlockControls();
            }
        }
    }
}
