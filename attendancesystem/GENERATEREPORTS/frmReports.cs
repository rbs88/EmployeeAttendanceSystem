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

namespace attendancesystem.GENERATEREPORTS
{
    public partial class frmReports : Form
    {

        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        DBConnection db = new DBConnection();
        public frmReports()
        {
            InitializeComponent();
            cn = new MySqlConnection(db.GetConnection());
            // LoadRecords();
            LoadNameRecord();
            //LoadMeetingAndEventsTitle();
            LoadDataToComboBox();
        }

        private void frmReports_Load(object sender, EventArgs e)
        {

        }

        private void btnDTR_Click(object sender, EventArgs e)
        {

        }

        private void metroTabPage1_Click(object sender, EventArgs e)
        {
           
        }

        public void LoadDataToComboBox()
        {
            cn.Open();
            cm = new MySqlCommand("SELECT TITLE FROM table_meetingattendance ORDER BY TITLE", cn);
            dr = cm.ExecuteReader();
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

            while (dr.Read())
            {
                collection.Add(dr["TITLE"].ToString());            
            }

            comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox2.AutoCompleteCustomSource = collection;

            cboPrint.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboPrint.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cboPrint.AutoCompleteCustomSource = collection;

            dr.Close();
            cn.Close();
        }

        public void LoadNameRecord()
        {
            cboFullName.Items.Clear();
            comboBox4.Items.Clear();
            cboFullName.Items.Add("ALL");
            comboBox4.Items.Add("ALL");
            comboBox4.Items.Add("BPSO");
            comboBox4.Items.Add("LUPON");
            comboBox4.Items.Add("STAFF");
            comboBox4.Items.Add("BCPC/VAWC");
            comboBox4.Items.Add("BADAC");
            comboBox4.Items.Add("TRAFFIC");
            comboBox4.Items.Add("MATERNAL DAYCARE TEACHERS");
            comboBox4.Items.Add("BNS");
            comboBox4.Items.Add("CCTV OPERATORS");
            comboBox4.Items.Add("ECO-AID");
            comboBox4.Items.Add("ENVIRONMENTAL");
            comboBox4.Items.Add("P.O");

            int i = 0;
            cn.Open();
            cm = new MySqlCommand("SELECT CONCAT(LASTNAME,', ',FIRSTNAME,' ',MIDDLENAME) as NAME FROM table_employee ORDER BY NAME", cn); ;
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                //cboSectionChief.Text.ToUpper();
                cboFullName.Items.Add(dr["NAME"].ToString().ToUpper());
                comboBox4.Items.Add(dr["NAME"].ToString().ToUpper());
            }
            dr.Close();
            cn.Close();
        }

        private void btnClickHere_Click(object sender, EventArgs e)
        {
            if (cboFullName.Text == "ALL" && cboWorkSched.Text == "ALL")
            {
                LoadRecords1();              
            }

            else if (cboWorkSched.Text == "DAY SHIFT" && cboFullName.Text =="ALL")
            {
                LoadRecords2();
            }

            else if (cboWorkSched.Text == "NIGHT SHIFT" && cboFullName.Text == "ALL")
            {
                LoadRecords2();
            }
            else if (cboWorkSched.Text == "DAY SHIFT")
            {
                LoadRecords3();
            }
            else if (cboWorkSched.Text == "NIGHT SHIFT")
            {
                LoadRecords4();
            }
            else if (cboWorkSched.Text == "ALL")
            {
                LoadRecords5();
            }       
        }

        
        public void LoadRecords1()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new MySqlCommand("SELECT CONCAT(LASTNAME,', ',FIRSTNAME,' ',MIDDLENAME) as FULLNAME,EMPLOYEEID,LOGDATEIN,TIME_IN,TIME_OUT,TIMEIN,TIMEOUT,WORK_SCHEDULE,SHIFT FROM table_attendance INNER JOIN table_employee ON table_attendance.EMPID=table_employee.EMPID WHERE(cast(LOGDATEIN as datetime) BETWEEN '" + dtFrom.Text + "' AND '" + dtTo.Text + "') ORDER BY FULLNAME,LOGDATEIN", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["EMPLOYEEID"].ToString(),dr["FULLNAME"].ToString(), DateTime.Parse(dr["LOGDATEIN"].ToString()).ToShortDateString(), dr["TIME_IN"].ToString(), dr["TIME_OUT"].ToString(), dr["TIMEIN"].ToString(), dr["TIMEOUT"].ToString(), dr["SHIFT"].ToString());//, DateTime.Parse(dr["DCOVERED_FROM"].ToString()).ToShortDateString(), DateTime.Parse(dr["DCOVERED_TO"].ToString()).ToShortDateString(), dr["PURPOSE"].ToString(), dr["REMARKS"].ToString(), dr["STATUS"].ToString(), dr["P1"].ToString(), dr["P2"].ToString(), dr["P3"].ToString(), dr["P4"].ToString(), dr["P5"].ToString(), dr["P6"].ToString(), dr["P7"].ToString(), dr["P8"].ToString(), dr["P9"].ToString(), dr["P10"].ToString(), dr["P11"].ToString(), dr["P12"].ToString(), dr["P13"].ToString(), dr["P14"].ToString(), dr["P15"].ToString(), dr["sNO"].ToString(), dr["SECTION_CHIEF"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadRecords2()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new MySqlCommand("SELECT CONCAT(LASTNAME,', ',FIRSTNAME,' ',MIDDLENAME) as FULLNAME,EMPLOYEEID,LOGDATEIN,TIME_IN,TIME_OUT,TIMEIN,TIMEOUT,WORK_SCHEDULE,SHIFT FROM table_attendance INNER JOIN table_employee ON table_attendance.EMPID=table_employee.EMPID WHERE(cast(LOGDATEIN as datetime) BETWEEN '" + dtFrom.Text + "' AND '" + dtTo.Text + "') AND SHIFT = '"+ cboWorkSched.Text +"' ORDER BY FULLNAME,LOGDATEIN", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["EMPLOYEEID"].ToString(), dr["FULLNAME"].ToString(), DateTime.Parse(dr["LOGDATEIN"].ToString()).ToShortDateString(), dr["TIME_IN"].ToString(), dr["TIME_OUT"].ToString(), dr["TIMEIN"].ToString(), dr["TIMEOUT"].ToString(), dr["SHIFT"].ToString());//, DateTime.Parse(dr["DCOVERED_FROM"].ToString()).ToShortDateString(), DateTime.Parse(dr["DCOVERED_TO"].ToString()).ToShortDateString(), dr["PURPOSE"].ToString(), dr["REMARKS"].ToString(), dr["STATUS"].ToString(), dr["P1"].ToString(), dr["P2"].ToString(), dr["P3"].ToString(), dr["P4"].ToString(), dr["P5"].ToString(), dr["P6"].ToString(), dr["P7"].ToString(), dr["P8"].ToString(), dr["P9"].ToString(), dr["P10"].ToString(), dr["P11"].ToString(), dr["P12"].ToString(), dr["P13"].ToString(), dr["P14"].ToString(), dr["P15"].ToString(), dr["sNO"].ToString(), dr["SECTION_CHIEF"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadRecords3()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new MySqlCommand("SELECT CONCAT(LASTNAME,', ',FIRSTNAME,' ',MIDDLENAME) as NAME,EMPLOYEEID,LOGDATEIN,TIME_IN,TIME_OUT,TIMEIN,TIMEOUT,WORK_SCHEDULE,SHIFT FROM table_attendance INNER JOIN table_employee ON table_attendance.EMPID=table_employee.EMPID WHERE(cast(LOGDATEIN as datetime) BETWEEN '" + dtFrom.Text + "' AND '" + dtTo.Text + "') AND FULLNAME = '" + cboFullName.Text + "' AND SHIFT LIKE 'DAY SHIFT' ORDER BY FULLNAME,LOGDATEIN", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["EMPLOYEEID"].ToString(), dr["NAME"].ToString(), DateTime.Parse(dr["LOGDATEIN"].ToString()).ToShortDateString(), dr["TIME_IN"].ToString(), dr["TIME_OUT"].ToString(), dr["TIMEIN"].ToString(), dr["TIMEOUT"].ToString(), dr["SHIFT"].ToString());//, DateTime.Parse(dr["DCOVERED_FROM"].ToString()).ToShortDateString(), DateTime.Parse(dr["DCOVERED_TO"].ToString()).ToShortDateString(), dr["PURPOSE"].ToString(), dr["REMARKS"].ToString(), dr["STATUS"].ToString(), dr["P1"].ToString(), dr["P2"].ToString(), dr["P3"].ToString(), dr["P4"].ToString(), dr["P5"].ToString(), dr["P6"].ToString(), dr["P7"].ToString(), dr["P8"].ToString(), dr["P9"].ToString(), dr["P10"].ToString(), dr["P11"].ToString(), dr["P12"].ToString(), dr["P13"].ToString(), dr["P14"].ToString(), dr["P15"].ToString(), dr["sNO"].ToString(), dr["SECTION_CHIEF"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadRecords4()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new MySqlCommand("SELECT CONCAT(LASTNAME,', ',FIRSTNAME,' ',MIDDLENAME) as NAME,EMPLOYEEID,LOGDATEIN,TIME_IN,TIME_OUT,TIMEIN,TIMEOUT,WORK_SCHEDULE,SHIFT FROM table_attendance INNER JOIN table_employee ON table_attendance.EMPID=table_employee.EMPID WHERE(cast(LOGDATEIN as datetime) BETWEEN '" + dtFrom.Text + "' AND '" + dtTo.Text + "') AND FULLNAME = '" + cboFullName.Text + "' AND SHIFT LIKE 'NIGHT SHIFT' ORDER BY FULLNAME,LOGDATEIN", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["EMPLOYEEID"].ToString(), dr["NAME"].ToString(), DateTime.Parse(dr["LOGDATEIN"].ToString()).ToShortDateString(), dr["TIME_IN"].ToString(), dr["TIME_OUT"].ToString(), dr["TIMEIN"].ToString(), dr["TIMEOUT"].ToString(), dr["SHIFT"].ToString());//, DateTime.Parse(dr["DCOVERED_FROM"].ToString()).ToShortDateString(), DateTime.Parse(dr["DCOVERED_TO"].ToString()).ToShortDateString(), dr["PURPOSE"].ToString(), dr["REMARKS"].ToString(), dr["STATUS"].ToString(), dr["P1"].ToString(), dr["P2"].ToString(), dr["P3"].ToString(), dr["P4"].ToString(), dr["P5"].ToString(), dr["P6"].ToString(), dr["P7"].ToString(), dr["P8"].ToString(), dr["P9"].ToString(), dr["P10"].ToString(), dr["P11"].ToString(), dr["P12"].ToString(), dr["P13"].ToString(), dr["P14"].ToString(), dr["P15"].ToString(), dr["sNO"].ToString(), dr["SECTION_CHIEF"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadRecords5()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new MySqlCommand("SELECT CONCAT(LASTNAME,', ',FIRSTNAME,' ',MIDDLENAME) as NAME,EMPLOYEEID,LOGDATEIN,TIME_IN,TIME_OUT,TIMEIN,TIMEOUT,WORK_SCHEDULE,SHIFT FROM table_attendance INNER JOIN table_employee ON table_attendance.EMPID=table_employee.EMPID WHERE(cast(LOGDATEIN as datetime) BETWEEN '" + dtFrom.Text + "' AND '" + dtTo.Text + "') AND FULLNAME = '" + cboFullName.Text + "' ORDER BY FULLNAME,LOGDATEIN", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["EMPLOYEEID"].ToString(), dr["NAME"].ToString(), DateTime.Parse(dr["LOGDATEIN"].ToString()).ToShortDateString(), dr["TIME_IN"].ToString(), dr["TIME_OUT"].ToString(), dr["TIMEIN"].ToString(), dr["TIMEOUT"].ToString(), dr["SHIFT"].ToString());//, DateTime.Parse(dr["DCOVERED_FROM"].ToString()).ToShortDateString(), DateTime.Parse(dr["DCOVERED_TO"].ToString()).ToShortDateString(), dr["PURPOSE"].ToString(), dr["REMARKS"].ToString(), dr["STATUS"].ToString(), dr["P1"].ToString(), dr["P2"].ToString(), dr["P3"].ToString(), dr["P4"].ToString(), dr["P5"].ToString(), dr["P6"].ToString(), dr["P7"].ToString(), dr["P8"].ToString(), dr["P9"].ToString(), dr["P10"].ToString(), dr["P11"].ToString(), dr["P12"].ToString(), dr["P13"].ToString(), dr["P14"].ToString(), dr["P15"].ToString(), dr["sNO"].ToString(), dr["SECTION_CHIEF"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void metroTabControl1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new MySqlCommand("SELECT CONCAT(LASTNAME,', ',FIRSTNAME,' ',MIDDLENAME) as NAME,EMPLOYEEID,LOGDATEIN,TIME_IN,TIME_OUT,TIMEIN,TIMEOUT,WORK_SCHEDULE,SHIFT FROM table_attendance INNER JOIN table_employee ON table_attendance.EMPID=table_employee.EMPID WHERE LOGDATEIN = '"+DateTime.Now.ToShortDateString()+"' ORDER BY NAME", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["EMPLOYEEID"].ToString(), dr["NAME"].ToString(), DateTime.Parse(dr["LOGDATEIN"].ToString()).ToShortDateString(), dr["TIME_IN"].ToString(), dr["TIME_OUT"].ToString(), dr["TIMEIN"].ToString(), dr["TIMEOUT"].ToString(), dr["SHIFT"].ToString());//, DateTime.Parse(dr["DCOVERED_FROM"].ToString()).ToShortDateString(), DateTime.Parse(dr["DCOVERED_TO"].ToString()).ToShortDateString(), dr["PURPOSE"].ToString(), dr["REMARKS"].ToString(), dr["STATUS"].ToString(), dr["P1"].ToString(), dr["P2"].ToString(), dr["P3"].ToString(), dr["P4"].ToString(), dr["P5"].ToString(), dr["P6"].ToString(), dr["P7"].ToString(), dr["P8"].ToString(), dr["P9"].ToString(), dr["P10"].ToString(), dr["P11"].ToString(), dr["P12"].ToString(), dr["P13"].ToString(), dr["P14"].ToString(), dr["P15"].ToString(), dr["sNO"].ToString(), dr["SECTION_CHIEF"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text !=string.Empty)
            {
                LoadRecords6();
            }
            else
            {
                LoadRecords7();
            }            
        }

        public void LoadRecords6()
        {
            try
            {
                dataGridView2.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new MySqlCommand("SELECT CONCAT(LASTNAME,', ',FIRSTNAME,' ',MIDDLENAME) as NAME,FULLNAME,EMPLOYEEID,LOGDATE,TITLE,TIMEIN FROM table_meetingattendance INNER JOIN table_employee ON table_meetingattendance.EMPID=table_employee.EMPID WHERE LOGDATE LIKE '" + metroDateTime2.Text + "'  AND TITLE = '" + comboBox2.Text + "' ORDER BY TITLE,FULLNAME", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView2.Rows.Add(i, dr["EMPLOYEEID"].ToString(), dr["NAME"].ToString(), DateTime.Parse(dr["LOGDATE"].ToString()).ToShortDateString(), dr["TITLE"].ToString(), dr["TIMEIN"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadRecords7()
        {
            try
            {
                dataGridView2.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new MySqlCommand("SELECT CONCAT(LASTNAME,', ',FIRSTNAME,' ',MIDDLENAME) as NAME,FULLNAME,EMPLOYEEID,LOGDATE,TITLE,TIMEIN FROM table_meetingattendance INNER JOIN table_employee ON table_meetingattendance.EMPID=table_employee.EMPID WHERE LOGDATE LIKE '" + metroDateTime2.Text + "' ORDER BY TITLE,FULLNAME", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView2.Rows.Add(i, dr["EMPLOYEEID"].ToString(), dr["NAME"].ToString(), DateTime.Parse(dr["LOGDATE"].ToString()).ToShortDateString(), dr["TITLE"].ToString(), dr["TIMEIN"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
        private void btnPrintPreview1_Click(object sender, EventArgs e)
        {
            if (cboPrint.Text ==string.Empty)
            {
                return;
            }
            else if (cboPrint.Text == "ALL MEETING/EVENTS")
            {
                var frm = new REPORT.frmMeetingAttendanceReport(this);
                frm.LoadReportS();
                frm.ShowDialog();
            }
            else
            {
                var frm = new REPORT.frmMeetingAttendanceReport(this);
                frm.LoadReport();
                frm.ShowDialog();
            }                    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox4.Text == "ALL" && comboBox3.Text == "ALL")
            {
                var frm = new REPORT.frmDTR(this);
                frm.LoadReport1();
                frm.ShowDialog();
            }
            else if (comboBox3.Text == "BY DEPARTMENT")
            {
                var frm = new REPORT.frmDTR(this);
                frm.LoadReportBYDEPT();
                frm.ShowDialog();
            }

            else if (comboBox3.Text == "DAY SHIFT" && comboBox4.Text == "ALL")
            {
                var frm = new REPORT.frmDTR(this);
                frm.LoadReport2();
                frm.ShowDialog();
            }

            else if (comboBox3.Text == "NIGHT SHIFT" && comboBox4.Text == "ALL")
            {
                var frm = new REPORT.frmDTR(this);
                frm.LoadReport2();
                frm.ShowDialog();
            }
            else if (comboBox3.Text == "DAY SHIFT" && comboBox4.Text !="ALL")
            {
                var frm = new REPORT.frmDTR(this);
                frm.LoadReport3();
                frm.ShowDialog();
            }
            else if (comboBox3.Text == "NIGHT SHIFT" && comboBox4.Text != "ALL")
            {
                var frm = new REPORT.frmDTR(this);
                frm.LoadReport4();
                frm.ShowDialog();
            }
            else if (comboBox3.Text == "ALL" && comboBox4.Text != "ALL")
            {
                var frm = new REPORT.frmDTR(this);
                frm.LoadReport5();
                frm.ShowDialog();
            }
        }
    }
}
