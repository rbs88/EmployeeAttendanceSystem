using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace attendancesystem.EMPLOYEE
{
    public partial class frmEmployee : Form
    {
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        MySqlDataAdapter da;
        DBConnection db = new DBConnection();
        string _title = "EMPLOYEE INFORMATION AND ATTENDANCE MANAGEMENT SYSTEM";
        public frmEmployee()
        {
            InitializeComponent();
            cn = new MySqlConnection(db.GetConnection());
            LoadRecords();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = new EMPLOYEE.frmAddNewEmployee(this);
            frm.btnUpdate.Enabled = false;
            frm.btnDelete.Enabled = false;
            frm.cboWorkSched.Text = "DAY SHIFT";
            frm.cboStatus.Text = "ACTIVE";
            frm.ShowDialog();
        }

        public void LoadRecords()
        {

            cn.Open();
            cm = new MySqlCommand("SELECT *, timestampdiff(YEAR, BIRTHDAY, CURDATE()) as AGE FROM table_employee WHERE EMPLOYEEID LIKE '%" + metroTextBox1.Text + "%' OR FIRSTNAME LIKE '%" + metroTextBox1.Text + "%' OR LASTNAME LIKE '%" + metroTextBox1.Text + "%' ORDER BY LASTNAME", cn);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cm;
                var dt = new DataTable();
                sda.Fill(dt);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dt;
                dataGridView1.DataSource = bSource;
                sda.Update(dt);
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cn.Close();
            }
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            LoadRecords();
            AutoLoad1();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns[0].Visible = false;
           // dataGridView1.Columns[1].Visible = false;
           //  dataGridView1.Columns[2].Visible = false;
            //dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false; 
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
          //  dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
           //dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[15].Visible = false;
            dataGridView1.Columns[16].Visible = false;
            // dataGridView1.Columns[18].Visible = false;
             dataGridView1.Columns[19].Visible = false;

            DataGridViewColumn column = dataGridView1.Columns[1];
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn columnn = dataGridView1.Columns[2];
            columnn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn column1 = dataGridView1.Columns[3];
            column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewColumn column2 = dataGridView1.Columns[4];
            column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewColumn column3 = dataGridView1.Columns[5];
            column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewColumn column4 = dataGridView1.Columns[9];
            column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewColumn column5 = dataGridView1.Columns[11];
            column5.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewColumn column6 = dataGridView1.Columns[18];
            column6.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        public void LoadImage()
        {
            var frm = new EMPLOYEE.frmPDFViewer();
            if (dataGridView1.Rows.Count != 0)
            {
                if (dataGridView1.CurrentRow.Cells[12].Value.ToString() != string.Empty)
                {
                    try
                    {
                        cn.Open();
                        da = new MySqlDataAdapter("Select PATH from table_employee where EMPID = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", cn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        // cn.Close();                  
                        // string paths = db.CentralizedPathsOpen();
                        // string paths = "//ADMIN-PC/Image";  
                        string paths = "C:\\EFILES";
                        //string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 0));               
                        var newFileName = (paths + dt.Rows[0]["PATH"].ToString());
                        frm.axAcroPDF1.src = newFileName;
                        //Process.Start(new ProcessStartInfo(newFileName) { UseShellExecute = true });
                        cn.Close();
                        frm.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        cn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No uploaded pdf file/s");                
                    return;                                        
                }
               // View Image to Picturebox
            }
            else
            {
                return;
            }
        }

       
        private void btnOpen_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //AutoLoad1();
            if (dataGridView1.CurrentRow.Cells[12].Value.ToString() != string.Empty)
            {
                button1.Text = "OPEN PDF FILE/S";
                button1.ForeColor = Color.White;
                button1.BackColor = Color.Green;
            }
            else
            {
                button1.Text = "NO UPLOADED PDF FILE/S";
                button1.ForeColor = Color.White;
                button1.BackColor = Color.Red;
            }
        }

        public void AutoLoad()
        {

            var f = new EMPLOYEE.frmAddNewEmployee(this);
            f.btnSave.Enabled = false;
            try
            {        
                    cn.Open();
                    cm = new MySqlCommand("SELECT *,timestampdiff(YEAR, BIRTHDAY, CURDATE()) as AGE FROM table_employee WHERE EMPID LIKE @id", cn);
                    cm.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                    //long len = dr.GetBytes(0, 0, null, 0, 0);
                    //byte[] array = new byte[System.Convert.ToInt32(len) + 1];
                    //dr.GetBytes(0, 0, array, 0, System.Convert.ToInt32(len));
                    f.label21.Text = dr.GetString(0).ToString();
                    f.txtEmployeeID.Text = dr.GetValue(1).ToString();
                    f.txtRFID.Text = dr.GetValue(2).ToString();
                    f.txtFirstName.Text = dr.GetValue(4).ToString();
                    f.txtMiddleName.Text = dr.GetValue(5).ToString();
                    f.txtLastName.Text = dr.GetValue(6).ToString();
                    f.dtBirthday.Value = DateTime.Parse(dr.GetValue(7).ToString());
                    f.txtAge.Text = dr.GetValue(8).ToString();
                    f.cboDepartment.Text = dr.GetValue(9).ToString();
                    f.txtDesignation.Text = dr.GetValue(10).ToString();
                    f.cboStatus.Text = dr.GetValue(11).ToString();
                    f.txtPath.Text = dr.GetValue(12).ToString();
                    f.txtAddress.Text = dr.GetValue(14).ToString();
                    f.cboMaritalStatus.Text = dr.GetValue(15).ToString();
                    f.mtContactNo.Text = dr.GetValue(16).ToString();
                    f.cboGender.Text = dr.GetValue(17).ToString();
                    f.cboWorkSched.Text = dr.GetValue(18).ToString();
                    f.txtAge.Text = dr.GetValue(19).ToString();
                                      

                    byte[] imgg = (byte[])(dr["PICTURE"]);

                        if (imgg == null)
                            f.pictureBox1.BackgroundImage = null;
                        else
                        {
                            MemoryStream mstream = new MemoryStream(imgg);
                            f.pictureBox1.BackgroundImage = System.Drawing.Image.FromStream(mstream);
                            //MemoryStream ms = new MemoryStream();
                            //pictureBox1.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            //byte[] arrImage = ms.GetBuffer();
                        }
                    }
                    dr.Close();
                    cn.Close();
                    f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cn.Close();
            }         
        }

        public void AutoLoad1()
        {
            if (dataGridView1.Rows.Count != 0)
            {
                try
                {
                    cn.Open();
                    cm = new MySqlCommand("SELECT * FROM table_employee WHERE EMPID LIKE @id", cn);
                    cm.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        //long len = dr.GetBytes(0, 0, null, 0, 0);
                        //byte[] array = new byte[System.Convert.ToInt32(len) + 1];
                        //dr.GetBytes(0, 0, array, 0, System.Convert.ToInt32(len));

                        lblFirstName.Text = dr.GetValue(4).ToString();
                        lblMiddleName.Text = dr.GetValue(5).ToString();
                        lblLastName.Text = dr.GetValue(6).ToString();
                        //f.dtBirthday.Value = DateTime.Parse(dr.GetValue(6).ToString());
                        //f.txtAge.Text = dr.GetValue(7).ToString();
                        lblDepartment.Text = dr.GetValue(9).ToString();
                        lblDesignation.Text = dr.GetValue(10).ToString();
                        lblStatus.Text = dr.GetValue(11).ToString();

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
                    }
                    dr.Close();
                    cn.Close();
                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cn.Close();
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            AutoLoad();         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "OPEN PDF FILE/S")
            {
                LoadImage();
            }
            else
            {
                return;
            }          
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            AutoLoad1();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            AutoLoad1();
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoLoad1();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AutoLoad1();
        }
    }
}
