using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace attendancesystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            var frm = new EMPLOYEE.frmEmployee();
            frm.TopLevel = false;       
            panel2.Controls.Add(frm);         
            frm.BringToFront();        
            frm.Show();
        }

        private void btnTimeInTimeOut_Click(object sender, EventArgs e)
        {
            var frm = new TIMEINTIMEOUT.frmINOUT();
            frm.label7.Text = "DAILY ATTENDANCE";
            frm.ShowDialog();
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            var frm = new GENERATEREPORTS.frmReports();
            //frm.TopLevel = false;
            //panel2.Controls.Add(frm);
            //frm.BringToFront();
            frm.ShowDialog();
        }

        private void btnMeetingAttendance_Click(object sender, EventArgs e)
        {
            //var frm = new TIMEINTIMEOUT.frmINOUT();
            //frm.label7.Text = "EMPLOYEE MEETING/EVENTS ATTENDANCE";
            //frm.label7.ForeColor = Color.DarkOliveGreen;
            //frm.label7.BackColor = Color.Beige;
            //frm.ShowDialog();

            var frm = new EventsTitle.EventsTitle();
            frm.ShowDialog();
        }
    }
}
