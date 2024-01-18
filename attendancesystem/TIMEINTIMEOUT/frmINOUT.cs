using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace attendancesystem.TIMEINTIMEOUT
{
    public partial class frmINOUT : Form
    {
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        DBConnection db = new DBConnection();
        string _title = "Attendance Management System";

        public object Datetime { get; private set; }

        public frmINOUT()
        {
            InitializeComponent();
            cn = new MySqlConnection(db.GetConnection());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }

        private void frmINOUT_Load(object sender, EventArgs e)
        {
           //label1.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {          
             AutoLoadTap();        
        }
     
        public void AttendanceDayTime(string EMPID, string workSched, string date1, string date2) 
        {
            try
            {
                string _timein ="";
                string _timeout = "";
              

                TimeSpan DayTime = TimeSpan.Parse("9:00:00 AM");
                TimeSpan now = DateTime.Now.TimeOfDay;

                int cid = 0;
                cn.Open();
                cm = new MySqlCommand("SELECT ATTID,TIME_IN,TIME_OUT,SHIFT FROM table_attendance WHERE EMPID=@EMPID AND(LOGDATEIN BETWEEN @date1 and @date2)", cn);
                cm.Parameters.AddWithValue("@EMPID", EMPID);
                cm.Parameters.AddWithValue("@date1", date1);
                cm.Parameters.AddWithValue("@date2", date2);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    _timein  = dr["TIME_IN"].ToString();
                    _timeout = dr["TIME_OUT"].ToString();               
                    cid = int.Parse(dr["ATTID"].ToString());                                  
                }
                dr.Close();
                cn.Close();

                if (cid == 0 && _timein == "")
                {
                    cn.Open();
                    cm = new MySqlCommand("INSERT INTO table_attendance(EMPID,LOGDATEIN,TIME_IN,SHIFT)VALUES(@EMPID,@LOGDATEIN,@TIME_IN,@SHIFT)", cn);
                    cm.Parameters.AddWithValue("@EMPID", EMPID);
                    cm.Parameters.AddWithValue("@LOGDATEIN", date1);
                    cm.Parameters.AddWithValue("@TIME_IN", DateTime.Now.ToShortTimeString());
                    cm.Parameters.AddWithValue("@SHIFT", workSched);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.BringToFront();
                    lblMessage.Text = "IN";
                    label1.Visible = true;
                    label1.ForeColor = Color.Green;
                    label1.Text = "   SUCCESSFULLY TIME-IN.";
                    label11.Text = DateTime.Now.ToShortTimeString();
                    label11.ForeColor = Color.Green;

                }
                else if (_timeout =="" && now >= DayTime)
                {                  
                    cn.Open();
                    cm = new MySqlCommand("UPDATE table_attendance set TIME_OUT=@TIME_OUT  WHERE ATTID = @ATTID", cn);
                    cm.Parameters.AddWithValue("@TIME_OUT", DateTime.Now.ToShortTimeString());
                    cm.Parameters.AddWithValue("@ATTID", cid);            
                    cm.ExecuteNonQuery();
                    cn.Close();

                    lblMessage.ForeColor = Color.Red;
                    lblMessage.BringToFront();
                    lblMessage.Text = "OUT";
                    label1.Visible = true;
                    label1.ForeColor = Color.Red;
                    label1.Text = "   SUCCESSFULLY TIME-OUT.";
                    label11.Text = _timein;
                    label12.Text = DateTime.Now.ToShortTimeString();
                    label12.ForeColor = Color.Red;
                }

                else if (_timeout != "" && now >= DayTime)
                {
                    cn.Open();
                    cm = new MySqlCommand("UPDATE table_attendance set TIME_OUT=@TIME_OUT  WHERE ATTID = @ATTID", cn);
                    cm.Parameters.AddWithValue("@TIME_OUT", DateTime.Now.ToShortTimeString());
                    cm.Parameters.AddWithValue("@ATTID", cid);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    lblMessage.ForeColor = Color.Red;
                    lblMessage.BringToFront();
                    lblMessage.Text = "OUT";
                    label1.Visible = true;
                    label1.ForeColor = Color.Red;
                    label1.Text = "   SUCCESSFULLY TIME-OUT.";
                    label11.Text = _timein;
                    label12.Text = DateTime.Now.ToShortTimeString();
                    label12.ForeColor = Color.Red;
                }

                else if(_timeout == "" && now <= DayTime)
                {
                    label1.Visible = true;
                    label1.BringToFront();
                    label1.ForeColor = Color.Green;
                    label11.Text = _timein;
                    label11.ForeColor = Color.Green;
                    label1.Text = "    ALREADY TIME-IN";
                }
                //else
                //{
                //    label1.Visible = false;
                //    lblMessage1.BringToFront();
                //    lblMessage1.Text = "ALREADY TIMEIN/OUT FOR TODAY!";
                //}
              //   label11.Text = _timein;
              //  label12.Text = _timeout;
            }

            catch (Exception e)
            {

                cn.Close();
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            
        }

        public void AttendanceDayTime1(string EMPID, string workSched, string date1, string date2)
        {
            try
            {
                string _timein = "";
                string _timeout = "";
                string _inititalTimeIN = "12:00 AM";

                //TimeSpan DayTime = TimeSpan.Parse("9:00");
                //TimeSpan now = DateTime.Now.TimeOfDay;

                int cid = 0;
                cn.Open();
                cm = new MySqlCommand("SELECT ATTID,TIME_IN,TIME_OUT,SHIFT FROM table_attendance WHERE EMPID=@EMPID AND(LOGDATEIN BETWEEN @date1 and @date2)", cn);
                cm.Parameters.AddWithValue("@EMPID", EMPID);
                cm.Parameters.AddWithValue("@date1", date1);
                cm.Parameters.AddWithValue("@date2", date2);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    _timein = dr["TIME_IN"].ToString();
                    _timeout = dr["TIME_OUT"].ToString();
                    cid = int.Parse(dr["ATTID"].ToString());
                }
                dr.Close();
                cn.Close();

                if (cid == 0 && _timein == "")
                {
                    cn.Open();
                    cm = new MySqlCommand("INSERT INTO table_attendance(EMPID,LOGDATEIN,TIME_IN,TIME_OUT,SHIFT)VALUES(@EMPID,@LOGDATEIN,@TIME_IN,@TIME_OUT,@SHIFT)", cn);
                    cm.Parameters.AddWithValue("@EMPID", EMPID);
                    cm.Parameters.AddWithValue("@LOGDATEIN", date1);
                    cm.Parameters.AddWithValue("@TIME_IN", _inititalTimeIN);
                    cm.Parameters.AddWithValue("@TIME_OUT", DateTime.Now.ToShortTimeString());
                    cm.Parameters.AddWithValue("@SHIFT", workSched);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.BringToFront();
                    lblMessage.Text = "OUT";
                    label1.Visible = true;
                    label1.ForeColor = Color.Red;
                    label1.Text = "   SUCCESSFULLY TIME-OUT.";
                    label11.Text = _inititalTimeIN;
                    label12.Text = DateTime.Now.ToShortTimeString();
                }
                else if (_timein == "" && _timeout =="")
                {
                    cn.Open();
                    cm = new MySqlCommand("UPDATE table_attendance set TIME_OUT=@TIME_OUT,SHIFT=@SHIFT, TIME_IN=@TIME_IN WHERE ATTID = @ATTID", cn);
                    cm.Parameters.AddWithValue("@TIME_OUT", DateTime.Now.ToShortTimeString());
                    cm.Parameters.AddWithValue("@TIME_IN", _inititalTimeIN);
                    cm.Parameters.AddWithValue("@ATTID", cid);
                    cm.Parameters.AddWithValue("@SHIFT", workSched);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    lblMessage.ForeColor = Color.Red;
                    lblMessage.BringToFront();
                    lblMessage.Text = "OUT";
                    label1.Visible = true;
                    label1.ForeColor = Color.Red;
                    label11.Text = _inititalTimeIN;
                    label12.Text = DateTime.Now.ToShortTimeString();
                    label1.Text = "   SUCCESSFULLY TIME-OUT.";                 
                }
                else if (_timein != "" && _timeout == "")
                {
                    cn.Open();
                    cm = new MySqlCommand("UPDATE table_attendance set TIME_OUT=@TIME_OUT,SHIFT=@SHIFT WHERE ATTID = @ATTID", cn);
                    cm.Parameters.AddWithValue("@TIME_OUT", DateTime.Now.ToShortTimeString());
                  //  cm.Parameters.AddWithValue("@TIME_IN", _inititalTimeIN);
                    cm.Parameters.AddWithValue("@ATTID", cid);
                    cm.Parameters.AddWithValue("@SHIFT", workSched);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    lblMessage.ForeColor = Color.Red;
                    lblMessage.BringToFront();
                    lblMessage.Text = "OUT";
                    label1.Visible = true;
                    label1.ForeColor = Color.Red;
                    label11.Text = _timein;
                    label12.Text = DateTime.Now.ToShortTimeString();
                    label1.Text = "   SUCCESSFULLY TIME-OUT.";
                }
                else if (_timeout != "")
                {
                    cn.Open();
                    cm = new MySqlCommand("UPDATE table_attendance set TIME_OUT=@TIME_OUT  WHERE ATTID = @ATTID", cn);
                    cm.Parameters.AddWithValue("@TIME_OUT", DateTime.Now.ToShortTimeString());
                    cm.Parameters.AddWithValue("@ATTID", cid);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    lblMessage.ForeColor = Color.Red;
                    lblMessage.BringToFront();
                    lblMessage.Text = "OUT";
                    label1.Visible = true;
                    label1.ForeColor = Color.Red;
                    label11.Text = _timein;
                    label12.Text = DateTime.Now.ToShortTimeString();
                    label1.Text = "   SUCCESSFULLY TIME-OUT.";
                }             
            }

            catch (Exception e)
            {

                cn.Close();
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void AttendanceDayTime2(string EMPID, string workSched, string date1, string date2)
        {
            try
            {
                string _timein = "";
                string _timeout = "";
               // string _inititalTimeIN = "12:00 AM";

                //TimeSpan DayTime = TimeSpan.Parse("9:00");
                //TimeSpan now = DateTime.Now.TimeOfDay;

                int cid = 0;
                cn.Open();
                cm = new MySqlCommand("SELECT ATTID,TIME_IN,TIME_OUT,SHIFT FROM table_attendance WHERE EMPID=@EMPID AND(LOGDATEIN BETWEEN @date1 and @date2)", cn);
                cm.Parameters.AddWithValue("@EMPID", EMPID);
                cm.Parameters.AddWithValue("@date1", date1);
                cm.Parameters.AddWithValue("@date2", date2);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    _timein = dr["TIME_IN"].ToString();
                    _timeout = dr["TIME_OUT"].ToString();
                    cid = int.Parse(dr["ATTID"].ToString());
                }
                dr.Close();
                cn.Close();

                if (cid == 0 && _timein == "")
                {
                    cn.Open();
                    cm = new MySqlCommand("INSERT INTO table_attendance(EMPID,LOGDATEIN,TIME_IN,SHIFT)VALUES(@EMPID,@LOGDATEIN,@TIME_IN,@SHIFT)", cn);
                    cm.Parameters.AddWithValue("@EMPID", EMPID);
                    cm.Parameters.AddWithValue("@LOGDATEIN", date1);
                   // cm.Parameters.AddWithValue("@TIME_IN", _inititalTimeIN);
                    cm.Parameters.AddWithValue("@TIME_IN", DateTime.Now.ToShortTimeString());
                    cm.Parameters.AddWithValue("@SHIFT", workSched);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.BringToFront();
                    lblMessage.Text = "IN";
                    label1.Visible = true;
                    label1.ForeColor = Color.Green;
                    label1.Text = "   SUCCESSFULLY TIME-IN.";
                  //  label11.Text = _inititalTimeIN;
                    label11.Text = DateTime.Now.ToShortTimeString();
                }
                else if (_timein == "")
                {
                    cn.Open();
                    cm = new MySqlCommand("UPDATE table_attendance set TIME_IN=@TIME_IN,SHIFT=@SHIFT WHERE ATTID = @ATTID", cn);
                    cm.Parameters.AddWithValue("@TIME_IN", DateTime.Now.ToShortTimeString());
                    //  cm.Parameters.AddWithValue("@TIME_IN", _inititalTimeIN);
                    cm.Parameters.AddWithValue("@ATTID", cid);
                    cm.Parameters.AddWithValue("@SHIFT", workSched);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    lblMessage.ForeColor = Color.Green;
                    lblMessage.BringToFront();
                    lblMessage.Text = "IN";
                    label1.Visible = true;
                    label1.ForeColor = Color.Green;
                   // label11.Text = _timein;
                    label11.Text = DateTime.Now.ToShortTimeString();
                    label1.Text = "   SUCCESSFULLY TIME-IN.";
                }
                else if (_timein != "")
                {
                    cn.Open();
                    cm = new MySqlCommand("UPDATE table_attendance set TIME_OUT=@TIME_OUT,SHIFT=@SHIFT WHERE ATTID = @ATTID", cn);
                    cm.Parameters.AddWithValue("@TIME_OUT", DateTime.Now.ToShortTimeString());
                    //cm.Parameters.AddWithValue("@TIME_IN", _inititalTimeIN);
                    cm.Parameters.AddWithValue("@ATTID", cid);
                    cm.Parameters.AddWithValue("@SHIFT", workSched);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    lblMessage.ForeColor = Color.Red;
                    lblMessage.BringToFront();
                    lblMessage.Text = "OUT";
                    label1.Visible = true;
                    label1.ForeColor = Color.Red;
                    label11.Text = _timein;
                    label12.Text = DateTime.Now.ToShortTimeString();
                    label1.Text = "   SUCCESSFULLY TIME-OUT.";
                }
                else if (_timeout != "")
                {
                    cn.Open();
                    cm = new MySqlCommand("UPDATE table_attendance set TIME_OUT=@TIME_OUT WHERE ATTID = @ATTID", cn);
                    cm.Parameters.AddWithValue("@TIME_OUT", DateTime.Now.ToShortTimeString());
                    cm.Parameters.AddWithValue("@ATTID", cid);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    lblMessage.ForeColor = Color.Red;
                    lblMessage.BringToFront();
                    lblMessage.Text = "OUT";
                    label1.Visible = true;
                    label1.ForeColor = Color.Red;
                    label11.Text = _timein;
                    label12.Text = DateTime.Now.ToShortTimeString();
                    label1.Text = "   SUCCESSFULLY TIME-OUT.";
                }
            }

            catch (Exception e)
            {

                cn.Close();
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void AttendanceNightTime(string EMPID, string workSched, string date1, string date2)
        {
            try
            {
                string _timein = "";
                string _timeout = "";
                string _time_in = "";
                string _time_out = "";
                string _initialTimeout = "12:00 AM";
                //string _AmMidnightIn = "12:00 AM";
                //string _timeing = "";
                //string _timeoutg = "";
                int cid = 0;
                cn.Open();
                cm = new MySqlCommand("SELECT ATTID,TIME_IN,TIME_OUT,TIMEIN,TIMEOUT,SHIFT FROM table_attendance WHERE EMPID=@EMPID AND(LOGDATEIN BETWEEN @date1 and @date2)", cn);
                cm.Parameters.AddWithValue("@EMPID", EMPID);
                cm.Parameters.AddWithValue("@date1", date1);
                cm.Parameters.AddWithValue("@date2", date2);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    _timein = dr["TIMEIN"].ToString();
                    _timeout = dr["TIMEOUT"].ToString();
                    _time_in = dr["TIME_IN"].ToString();
                    _time_out = dr["TIME_OUT"].ToString();
                   //_timeing = dr["TIMEIN_G"].ToString();
                   //_timeoutg = dr["TIMEOUT_G"].ToString();
                   cid = int.Parse(dr["ATTID"].ToString());
                }
                dr.Close();
                cn.Close();

                if (cid == 0)
                {
                    cn.Open();
                    cm = new MySqlCommand("INSERT INTO table_attendance(EMPID,LOGDATEIN,TIMEIN,TIMEOUT,SHIFT)VALUES(@EMPID,@LOGDATEIN,@TIMEIN,@TIMEOUT,@SHIFT)", cn);
                    cm.Parameters.AddWithValue("@EMPID", EMPID);
                    cm.Parameters.AddWithValue("@LOGDATEIN", date1);
                    cm.Parameters.AddWithValue("@TIMEIN", DateTime.Now.ToShortTimeString());
                    cm.Parameters.AddWithValue("@TIMEOUT", _initialTimeout);
                    cm.Parameters.AddWithValue("@SHIFT", workSched);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.BringToFront();
                    lblMessage.Text = "IN";
                    label1.Visible = true;
                    label1.ForeColor = Color.Green;
                    label1.Text = "   SUCCESSFULLY TIME-IN.";
                    label18.Text = DateTime.Now.ToShortTimeString();
                    label19.Text = _initialTimeout;
                }

                else if (_timein == "")
                {
                    cn.Open();
                    cm = new MySqlCommand("UPDATE table_attendance set TIMEIN=@TIMEIN, TIMEOUT=@TIMEOUT, SHIFT=@SHIFT WHERE ATTID = @ATTID", cn);
                    cm.Parameters.AddWithValue("TIMEIN", DateTime.Now.ToShortTimeString());
                    cm.Parameters.AddWithValue("@TIMEOUT", _initialTimeout);
                    cm.Parameters.AddWithValue("@ATTID", cid);
                    cm.Parameters.AddWithValue("@SHIFT", workSched);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.BringToFront();
                    lblMessage.Text = "IN";
                    label1.Visible = true;
                    label1.ForeColor = Color.Green;
                    label18.Text = DateTime.Now.ToShortTimeString();
                    label19.Text = _initialTimeout;
                    label11.Text = _time_in;
                    label12.Text = _time_out;
                    label1.Text = "   SUCCESSFULLY TIME-IN.";
                }
                else if (_timeout != "")
                {
                    cn.Open();
                    cm = new MySqlCommand("UPDATE table_attendance set TIMEOUT=@TIMEOUT WHERE ATTID = @ATTID", cn);
                    cm.Parameters.AddWithValue("TIMEOUT", DateTime.Now.ToShortTimeString());
                    cm.Parameters.AddWithValue("@ATTID", cid);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.BringToFront();
                    lblMessage.Text = "OUT";
                    label1.Visible = true;
                    label1.ForeColor = Color.Red;
                    label18.Text = _timein;
                    label19.Text = DateTime.Now.ToShortTimeString();
                    label11.Text = _time_in;
                    label12.Text = _time_out;
                    label1.Text = "   SUCCESSFULLY TIME-OUT.";
                }
                else
                {
                    //lblMessage.ForeColor = Color.Red;
                    label1.Visible = true;
                    label1.BringToFront();
                    label1.ForeColor = Color.Red;
                    label1.Text = "   ALREADY TIMEIN/OUT FOR TODAY!";
                }
            }
            catch (Exception e)
            { 

                cn.Close();
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void MeetingAndEventsAttendance(string EMPID, string date1, string date2)
        {
            try
            {
                string _timein = "";              
                int cid = 0;
                cn.Open();
                cm = new MySqlCommand("SELECT id1,TIMEIN FROM table_meetingattendance WHERE EMPID=@EMPID AND(LOGDATE BETWEEN @date1 and @date2)", cn);
                cm.Parameters.AddWithValue("@EMPID", EMPID);
                cm.Parameters.AddWithValue("@date1", date1);
                cm.Parameters.AddWithValue("@date2", date2);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    _timein = dr["TIMEIN"].ToString();                  
                    cid = int.Parse(dr["id1"].ToString());
                }
                dr.Close();
                cn.Close();
              
                    cn.Open();
                    cm = new MySqlCommand("INSERT INTO table_meetingattendance(EMPID,LOGDATE,TITLE,TIMEIN)VALUES(@EMPID,@LOGDATE,@TITLE,@TIMEIN)", cn);
                    cm.Parameters.AddWithValue("@EMPID", EMPID);
                    cm.Parameters.AddWithValue("@LOGDATE", date1);
                    cm.Parameters.AddWithValue("@TIMEIN", DateTime.Now.ToShortTimeString());
                    cm.Parameters.AddWithValue("@TITLE", label9.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.BringToFront();
                    lblMessage.Text = "IN";
                    label1.Visible = true;
                    label1.ForeColor = Color.Green;
                    label1.Text = "   Your Attendance is successfully recorded.";
              
            }
            catch (Exception e)
            {

                cn.Close();
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void AutoLoadTap()
        {
            try
            {
                string workSched = "";
                if (textBox1.TextLength == 10)
                {
                    cn.Open();
                    cm = new MySqlCommand("SELECT * FROM table_employee WHERE EMPLOYEEID LIKE @id OR RFID LIKE @RFID", cn);
                    cm.Parameters.AddWithValue("@id", textBox1.Text);
                    cm.Parameters.AddWithValue("@RFID", textBox1.Text);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if(dr.HasRows)
                    {
                        //long len = dr.GetBytes(0, 0, null, 0, 0);
                        //byte[] array = new byte[System.Convert.ToInt32(len) + 1];
                        //dr.GetBytes(0, 0, array, 0, System.Convert.ToInt32(len));
                        lblMiddleName.BringToFront();
                        lblIDNO.Text = dr.GetValue(0).ToString();
                        lblID.Text = dr.GetValue(1).ToString();
                        lblLastName.Text = dr.GetValue(6).ToString();
                        lblFirstName.Text = dr.GetValue(4).ToString();
                        lblMiddleName.Text = dr.GetValue(5).ToString();
                        lblDepartment.Text = dr.GetValue(9).ToString();
                        lblDesignation.Text = dr.GetValue(10).ToString();
                        workSched = dr.GetValue(18).ToString();
                        label13.Text = dr.GetValue(18).ToString();

                        lblMessage.Text = "";
                        byte[] imgg = (byte[])(dr["PICTURE"]);

                        if (imgg == null)
                           pictureBox1.BackgroundImage = null;
                        else
                        {
                            MemoryStream mstream = new MemoryStream(imgg);
                            pictureBox1.BackgroundImage = System.Drawing.Image.FromStream(mstream);
                            //MemoryStream ms = new MemoryStream();
                            //pictureBox1.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            //byte[] arrImage = ms.GetBuffer();
                        }
                        dr.Close();
                        cn.Close();

                        if (label7.Text == "DAILY ATTENDANCE")
                        {
                            if (workSched == "DAY SHIFT")
                            {
                                label11.Text = "";
                                label12.Text = "";
                                label18.Text = "";
                                label19.Text = "";
                                AttendanceDayTime(lblIDNO.Text,workSched, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
                            }
                            else if(workSched == "NIGHT SHIFT")
                            {
                               
                                TimeSpan DayTime = TimeSpan.Parse("12:00");
                              
                                TimeSpan now = DateTime.Now.TimeOfDay;
                                if (now <= DayTime)
                                {
                                    TimeSpan DayTime1 = TimeSpan.Parse("03:00");
                                    if (now <= DayTime1)
                                    {
                                        label11.Text = "";
                                        label12.Text = "";
                                        label18.Text = "";
                                        label19.Text = "";
                                        AttendanceDayTime2(lblIDNO.Text, workSched, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
                                    }
                                    else
                                    {
                                        label11.Text = "";
                                        label12.Text = "";
                                        label18.Text = "";
                                        label19.Text = "";
                                        AttendanceDayTime1(lblIDNO.Text, workSched, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
                                    }

                                }
                                else
                                {
                                    label11.Text = "";
                                    label12.Text = "";
                                    label18.Text = "";
                                    label19.Text = "";
                                    AttendanceNightTime(lblIDNO.Text, workSched, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
                                }
                            }
                        }
                        else
                        {
                            MeetingAndEventsAttendance(lblIDNO.Text, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
                        }
                       
                    }
                    else
                    {
                        dr.Close();
                        cn.Close();
                        lblMessage.BringToFront();
                        label1.Visible = true;
                        label1.ForeColor = Color.Red;
                        label1.Text = "   NOT FOUND!";
                        lblMessage.Text = "";
                        pictureBox1.BackgroundImage = pictureBox1.InitialImage;
                        lblID.Text = "";
                        lblLastName.Text = "";
                        lblFirstName.Text = "";
                        lblMiddleName.Text = "";
                        lblDepartment.Text = "";
                        lblDesignation.Text = "";
                        label11.Text = "";
                        label12.Text = "";
                        label13.Text = "";
                        label18.Text = "";
                        label19.Text = "";
                        textBox1.Focus();
                        textBox1.SelectAll();
                    }

                    textBox1.Focus();
                    textBox1.SelectAll();
                }          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        private void frmINOUT_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = (this.Width - groupBox1.Width) / 2;
            groupBox1.Top = (this.Height - groupBox1.Height) / 2;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            AutoLoadTap();          
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
              
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
           // this.textBox1.SelectAll();
        }
    }
}
