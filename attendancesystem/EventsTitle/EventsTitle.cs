using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace attendancesystem.EventsTitle
{
    public partial class EventsTitle : Form
    {
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        DBConnection db = new DBConnection();
        string _title = "Attendance Management System";
        public EventsTitle()
        {
            InitializeComponent();
            cn = new MySqlConnection(db.GetConnection());
            LoadNameRecord();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text !=string.Empty)
            {
                var frm = new TIMEINTIMEOUT.frmINOUT();
                frm.label7.Text = "EMPLOYEE MEETING/EVENTS ATTENDANCE";
                frm.label7.ForeColor = Color.DarkOliveGreen;
                frm.label7.BackColor = Color.Beige;
                frm.label9.Text = comboBox1.Text.ToString();
                frm.groupBox2.Visible= false;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Provide Meeting/Events Title");
            }          
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == string.Empty)
                {
                    MessageBox.Show("Please Provide Meeting/Events Title");
                    return;
                }       
                cn.Open();
                cm = new MySqlCommand("INSERT INTO table_meetingeventstitle(TITLE)VALUES(@TITLE)", cn);
                cm.Parameters.AddWithValue("@TITLE", comboBox1.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("New title has been successfully saved!", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNameRecord();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadNameRecord()
        {
            //cboFullName.Items.Clear();
            //comboBox4.Items.Clear();
            //cboFullName.Items.Add("ALL");
            //comboBox4.Items.Add("ALL");
            int i = 0;
            cn.Open();
            cm = new MySqlCommand("SELECT TITLE FROM table_meetingeventstitle ORDER BY TITLE", cn); ;
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                //cboSectionChief.Text.ToUpper();
              //  cboFullName.Items.Add(dr["NAME"].ToString().ToUpper());
                comboBox1.Items.Add(dr["TITLE"].ToString().ToUpper());
            }
            dr.Close();
            cn.Close();
        }
    }
}
