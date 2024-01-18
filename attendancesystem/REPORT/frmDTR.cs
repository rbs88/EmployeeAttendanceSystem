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
    public partial class frmDTR : Form
    {
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        MySqlDataAdapter da;
        DBConnection db = new DBConnection();
        GENERATEREPORTS.frmReports f;
        public frmDTR(GENERATEREPORTS.frmReports f)
        {
            InitializeComponent();
            cn = new MySqlConnection(db.GetConnection());
            this.f = f;
        }

        private void frmDTR_Load(object sender, EventArgs e)
        {
       
        }

        public void LoadReportBYDEPT()
        {
            try
            {
                ReportDataSource reportDS;

                this.reportViewer1.LocalReport.ReportPath = @"C:\Reports\ReportDTR.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();


                DataSet1 ds = new DataSet1();
                da = new MySqlDataAdapter();

                cn.Open();
                da.SelectCommand = new MySqlCommand("SELECT EMPLOYEEID,FULLNAME,DEPARTMENT,LOGDATEIN,TIME_IN,TIME_OUT,TIMEIN,TIMEOUT,WORK_SCHEDULE,SHIFT FROM table_attendance INNER JOIN table_employee ON table_attendance.EMPID=table_employee.EMPID WHERE(cast(LOGDATEIN as datetime) BETWEEN '" + f.metroDateTime4.Text + "' AND '" + f.metroDateTime3.Text + "') AND DEPARTMENT = '" + f.comboBox4.Text + "' ORDER BY FULLNAME,LOGDATEIN", cn);
                da.Fill(ds.Tables["dtDTR"]);
                cn.Close();

                ReportParameter p1 = new ReportParameter("pName", f.comboBox4.Text);
                ReportParameter p2 = new ReportParameter("pDateFrom", f.metroDateTime4.Text);
                ReportParameter p3 = new ReportParameter("pDateTo", f.metroDateTime3.Text);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p2 });
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p3 });

                reportDS = new ReportDataSource("DataSet1", ds.Tables["dtDTR"]);
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

        public void LoadReport1()
        {
            try
            {
                ReportDataSource reportDS;

                this.reportViewer1.LocalReport.ReportPath = @"C:\Reports\ReportDTR.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();


                DataSet1 ds = new DataSet1();
                da = new MySqlDataAdapter();

                cn.Open();
                da.SelectCommand = new MySqlCommand("SELECT EMPLOYEEID,FULLNAME,LOGDATEIN,TIME_IN,TIME_OUT,TIMEIN,TIMEOUT,WORK_SCHEDULE,SHIFT FROM table_attendance INNER JOIN table_employee ON table_attendance.EMPID=table_employee.EMPID WHERE(cast(LOGDATEIN as datetime) BETWEEN '" + f.metroDateTime4.Text + "' AND '" + f.metroDateTime3.Text + "') ORDER BY FULLNAME,LOGDATEIN", cn);             
                da.Fill(ds.Tables["dtDTR"]);
                cn.Close();

                ReportParameter p1 = new ReportParameter("pName", f.comboBox4.Text);
                ReportParameter p2 = new ReportParameter("pDateFrom", f.metroDateTime4.Text);
                ReportParameter p3 = new ReportParameter("pDateTo", f.metroDateTime3.Text);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p2 });
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p3 });

                reportDS = new ReportDataSource("DataSet1", ds.Tables["dtDTR"]);
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

        public void LoadReport2()
        {
            try
            {
                ReportDataSource reportDS;

                this.reportViewer1.LocalReport.ReportPath = @"C:\Reports\ReportDTR.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();


                DataSet1 ds = new DataSet1();
                da = new MySqlDataAdapter();

                cn.Open();
                da.SelectCommand = new MySqlCommand("SELECT EMPLOYEEID,FULLNAME,LOGDATEIN,TIME_IN,TIME_OUT,TIMEIN,TIMEOUT,WORK_SCHEDULE,SHIFT FROM table_attendance INNER JOIN table_employee ON table_attendance.EMPID=table_employee.EMPID WHERE(cast(LOGDATEIN as datetime) BETWEEN '" + f.metroDateTime4.Text + "' AND '" + f.metroDateTime3.Text + "') AND SHIFT = '" + f.comboBox3.Text + "' ORDER BY FULLNAME,LOGDATEIN", cn);
                da.Fill(ds.Tables["dtDTR"]);
                cn.Close();

                ReportParameter p1 = new ReportParameter("pName", f.comboBox4.Text);
                ReportParameter p2 = new ReportParameter("pDateFrom", f.metroDateTime4.Text);
                ReportParameter p3 = new ReportParameter("pDateTo", f.metroDateTime3.Text);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p2 });
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p3 });

                reportDS = new ReportDataSource("DataSet1", ds.Tables["dtDTR"]);
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

        public void LoadReport3()
        {
            try
            {
                ReportDataSource reportDS;

                this.reportViewer1.LocalReport.ReportPath = @"C:\Reports\ReportDTR1.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();


                DataSet1 ds = new DataSet1();
                da = new MySqlDataAdapter();

                cn.Open();
                da.SelectCommand = new MySqlCommand("SELECT EMPLOYEEID,FULLNAME,LOGDATEIN,TIME_IN,TIME_OUT,TIMEIN,TIMEOUT,WORK_SCHEDULE,SHIFT FROM table_attendance INNER JOIN table_employee ON table_attendance.EMPID=table_employee.EMPID WHERE(cast(LOGDATEIN as datetime) BETWEEN '" + f.metroDateTime4.Text + "' AND '" + f.metroDateTime3.Text + "') AND FULLNAME = '" + f.comboBox4.Text + "' AND SHIFT LIKE 'DAY SHIFT' ORDER BY FULLNAME,LOGDATEIN", cn);
                da.Fill(ds.Tables["dtDTR"]);
                cn.Close();

                ReportParameter p1 = new ReportParameter("pName", f.comboBox4.Text);
                ReportParameter p2 = new ReportParameter("pDateFrom", f.metroDateTime4.Text);
                ReportParameter p3 = new ReportParameter("pDateTo", f.metroDateTime3.Text);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p2 });
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p3 });

                reportDS = new ReportDataSource("DataSet1", ds.Tables["dtDTR"]);
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

        public void LoadReport4()
        {
            try
            {
                ReportDataSource reportDS;

                this.reportViewer1.LocalReport.ReportPath = @"C:\Reports\ReportDTR1.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();


                DataSet1 ds = new DataSet1();
                da = new MySqlDataAdapter();

                cn.Open();
                da.SelectCommand = new MySqlCommand("SELECT EMPLOYEEID,FULLNAME,LOGDATEIN,TIME_IN,TIME_OUT,TIMEIN,TIMEOUT,WORK_SCHEDULE,SHIFT FROM table_attendance INNER JOIN table_employee ON table_attendance.EMPID=table_employee.EMPID WHERE(cast(LOGDATEIN as datetime) BETWEEN '" + f.metroDateTime4.Text + "' AND '" + f.metroDateTime3.Text + "') AND FULLNAME = '" + f.comboBox4.Text + "' AND SHIFT LIKE 'NIGHT SHIFT' ORDER BY FULLNAME,LOGDATEIN", cn);
                da.Fill(ds.Tables["dtDTR"]);
                cn.Close();

                ReportParameter p1 = new ReportParameter("pName", f.comboBox4.Text);
                ReportParameter p2 = new ReportParameter("pDateFrom", f.metroDateTime4.Text);
                ReportParameter p3 = new ReportParameter("pDateTo", f.metroDateTime3.Text);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p2 });
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p3 });

                reportDS = new ReportDataSource("DataSet1", ds.Tables["dtDTR"]);
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

        public void LoadReport5()
        {
            try
            {
                ReportDataSource reportDS;

                this.reportViewer1.LocalReport.ReportPath = @"C:\Reports\ReportDTR1.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();


                DataSet1 ds = new DataSet1();
                da = new MySqlDataAdapter();

                cn.Open();
                da.SelectCommand = new MySqlCommand("SELECT EMPLOYEEID,FULLNAME,DEPARTMENT,LOGDATEIN,TIME_IN,TIME_OUT,TIMEIN,TIMEOUT,WORK_SCHEDULE,SHIFT FROM table_attendance INNER JOIN table_employee ON table_attendance.EMPID=table_employee.EMPID WHERE(cast(LOGDATEIN as datetime) BETWEEN '" + f.metroDateTime4.Text + "' AND '" + f.metroDateTime3.Text + "') AND FULLNAME = '" + f.comboBox4.Text + "' ORDER BY FULLNAME,LOGDATEIN", cn);
                da.Fill(ds.Tables["dtDTR"]);
                cn.Close();

                ReportParameter p1 = new ReportParameter("pName", f.comboBox4.Text);
                ReportParameter p2 = new ReportParameter("pDateFrom", f.metroDateTime4.Text);
                ReportParameter p3 = new ReportParameter("pDateTo", f.metroDateTime3.Text);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p2 });
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p3 });

                reportDS = new ReportDataSource("DataSet1", ds.Tables["dtDTR"]);
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
