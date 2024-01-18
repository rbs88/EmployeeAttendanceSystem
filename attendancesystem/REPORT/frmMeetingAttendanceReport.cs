using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;

namespace attendancesystem.REPORT
{
    public partial class frmMeetingAttendanceReport : Form
    {
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        MySqlDataAdapter da;
        DBConnection db = new DBConnection();
        GENERATEREPORTS.frmReports f;
        public frmMeetingAttendanceReport(GENERATEREPORTS.frmReports f)
        {
            InitializeComponent();
            cn = new MySqlConnection(db.GetConnection());
            this.f = f;
        }

        private void frmMeetingAttendanceReport_Load(object sender, EventArgs e)
        {

           
        }

        public void LoadReport()
        {
            try
            {
                ReportDataSource reportDS;

                this.reportViewer1.LocalReport.ReportPath = @"C:\Reports\ReportMeetingAndEventsAttendance.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();


                DataSet1 ds = new DataSet1();
                da = new MySqlDataAdapter();

                cn.Open();
                da.SelectCommand = new MySqlCommand("SELECT EMPLOYEEID,FULLNAME,LOGDATE,TITLE,TIMEIN FROM table_meetingattendance INNER JOIN table_employee ON table_meetingattendance.EMPID=table_employee.EMPID WHERE LOGDATE LIKE '" + f.dtPrint.Text + "'  AND TITLE = '" + f.cboPrint.Text + "' ORDER BY TITLE,FULLNAME", cn);
                da.Fill(ds.Tables["dtMeetingEventsReport"]);
                cn.Close();

                ReportParameter p1 = new ReportParameter("pTitle",f.cboPrint.Text);
                //ReportParameter p2 = new ReportParameter("eDate", eDate);
                //ReportParameter p3 = new ReportParameter("Month", my);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });

                reportDS = new ReportDataSource("DataSet1", ds.Tables["dtMeetingEventsReport"]);
                reportViewer1.LocalReport.DataSources.Add(reportDS);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.PageWidth;
                this.reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(ex.InnerException.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(ex.InnerException.InnerException.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadReportS()
        {
            try
            {
                ReportDataSource reportDS;

                this.reportViewer1.LocalReport.ReportPath = @"C:\Reports\MeetingAndEvents.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();


                DataSet1 ds = new DataSet1();
                da = new MySqlDataAdapter();

                cn.Open();
                da.SelectCommand = new MySqlCommand("SELECT EMPLOYEEID,FULLNAME,LOGDATE,TITLE,TIMEIN FROM table_meetingattendance INNER JOIN table_employee ON table_meetingattendance.EMPID=table_employee.EMPID WHERE LOGDATE LIKE '" + f.dtPrint.Text + "' ORDER BY TITLE,FULLNAME", cn);
                da.Fill(ds.Tables["dtMeetingEventsReport"]);
                cn.Close();

                ReportParameter p1 = new ReportParameter("pTitle", f.cboPrint.Text);
                //ReportParameter p2 = new ReportParameter("eDate", eDate);
                ////ReportParameter p3 = new ReportParameter("Month", my);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });

                reportDS = new ReportDataSource("DataSet1", ds.Tables["dtMeetingEventsReport"]);
                reportViewer1.LocalReport.DataSources.Add(reportDS);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.PageWidth;
                this.reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(ex.InnerException.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(ex.InnerException.InnerException.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
