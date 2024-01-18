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

namespace attendancesystem.EMPLOYEE
{
    public partial class frmAddNewEmployee : Form
    {
        MySqlConnection cn;
        MySqlCommand cm;
        //SqlDataReader dr;
        DBConnection db = new DBConnection();
        string _title = "Attendance Management System";
        frmEmployee f;
        public frmAddNewEmployee(frmEmployee f)
        {
            InitializeComponent();
            cn = new MySqlConnection(db.GetConnection());
            this.f = f;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeID.Text == string.Empty || txtFirstName.Text == string.Empty || txtLastName.Text == string.Empty)
                {
                    MessageBox.Show("Please fill in required fields");
                    return;
                }
             
                MemoryStream ms = new MemoryStream();
                pictureBox1.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arrImage = ms.GetBuffer();

                cn.Open();
                cm = new MySqlCommand("INSERT INTO table_employee(EMPLOYEEID,RFID,FIRSTNAME,MIDDLENAME,LASTNAME,BIRTHDAY,DEPARTMENT,DESIGNATION,STATUS,PATH,PICTURE,ADDRESS,MARITAL_STATUS,CONTACT_NO,GENDER,WORK_SCHEDULE)VALUES(@EMPLOYEEID,@RFID,@FIRSTNAME,@MIDDLENAME,@LASTNAME,@BIRTHDAY,@DEPARTMENT,@DESIGNATION,@STATUS,@PATH,@PICTURE,@ADDRESS,@MARITAL_STATUS,@CONTACT_NO,@GENDER,@WORK_SCHEDULE)", cn);
                cm.Parameters.AddWithValue("@EMPLOYEEID", txtEmployeeID.Text);
                cm.Parameters.AddWithValue("@RFID", txtRFID.Text);
                cm.Parameters.AddWithValue("@FIRSTNAME", txtFirstName.Text);
                cm.Parameters.AddWithValue("@MIDDLENAME", txtMiddleName.Text);
                cm.Parameters.AddWithValue("@LASTNAME", txtLastName.Text);
                cm.Parameters.AddWithValue("@BIRTHDAY", dtBirthday.Value);
                cm.Parameters.AddWithValue("@DEPARTMENT", cboDepartment.Text);
                cm.Parameters.AddWithValue("@DESIGNATION", txtDesignation.Text);
                cm.Parameters.AddWithValue("@STATUS", cboStatus.Text);
                cm.Parameters.AddWithValue("@PATH", txtPath.Text);
                cm.Parameters.AddWithValue("@ADDRESS", txtAddress.Text);
                cm.Parameters.AddWithValue("@MARITAL_STATUS", cboMaritalStatus.Text);
                cm.Parameters.AddWithValue("@CONTACT_NO", mtContactNo.Text);
                cm.Parameters.AddWithValue("@GENDER", cboGender.Text);
                cm.Parameters.AddWithValue("@WORK_SCHEDULE", cboWorkSched.Text);
                cm.Parameters.AddWithValue("@PICTURE", arrImage);

                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("New Employee has been successfully saved!", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pictureBox1.BackgroundImage = pictureBox1.InitialImage;
                f.LoadRecords();
                Clear();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear() 
        {
            txtEmployeeID.Clear();
            txtRFID.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtLastName.Clear();
            cboDepartment.Text = "";
            txtDesignation.Clear();
            cboStatus.ResetText();
            txtPath.Clear();
            txtAddress.Clear();
            cboGender.ResetText();
            cboMaritalStatus.ResetText();
            mtContactNo.Clear();
        }

        private void btnBrowseScanProfile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.InitialDirectory = "C:\\";
                open.Filter = "Image Files(*.pdf)|*.pdf";
                //open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.ico)|*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.ico";
                //open.Filter = "Word Documents|*.docx;*.doc|Excel Worksheets|*.xlsx;*.xls|PowerPoint Presentations|*.pptx;*.ppt" + "|Office Files|*.docx;*.xlsx;*.pptx;*.doc;*.xls;*.ppt";
               // open.Filter = "Office Files|*.docx;*.xlsx;*.pptx;*.doc;*.xls;*.ppt;*.pdf" + "|Word Documents|*.docx;*.doc|Excel Worksheets|*.xlsx;*.xls|PowerPoint Presentations|*.pptx;*.ppt";
                open.FilterIndex = 1;

                if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {                
                    if (open.CheckFileExists)
                    {
                        // string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 0));
                        // string paths = Application.StartupPath + @"Images";
                        // string paths = "//ADMIN-PC/Image";
                        // string paths = db.CentralizedPathsSave();
                        // string paths = "//SSVR/OFS$"; // Save to Server
                        string paths = "C:\\EFILES";
                        string CorrectFilename = System.IO.Path.GetFileName(open.FileName);
                        System.IO.File.Copy(open.FileName, paths + "\\EFILE\\" + CorrectFilename);
                        //pictureBox.Image = new Bitmap(open.FileName);
                        txtPath.Text = "\\" + "\\EFILE\\" + "\\" + CorrectFilename;
                        //  MessageBox.Show("Files ready to backup");
                    }
                    //  }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("File already exists.'Rename your file'", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBrowsePicture_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Image files (*.png) |*.png|(*.jpg)|*.jpg|(*.gif)|*.gif";
                openFileDialog1.ShowDialog();
                pictureBox1.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void cboStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("UPDATE THIS RECORD? CLICK YES TO CONFIRM", _title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    MemoryStream ms = new MemoryStream();
                    pictureBox1.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] arrImage = ms.GetBuffer();

                    //UPDATE RECORDS
                    cn.Open();
                    cm = new MySqlCommand("UPDATE table_employee SET EMPLOYEEID=@EMPLOYEEID, RFID=@RFID, FIRSTNAME=@FIRSTNAME, MIDDLENAME=@MIDDLENAME, LASTNAME=@LASTNAME, BIRTHDAY=@BIRTHDAY, DEPARTMENT=@DEPARTMENT, DESIGNATION=@DESIGNATION, STATUS=@STATUS, PATH=@PATH, ADDRESS=@ADDRESS, MARITAL_STATUS=@MARITAL_STATUS, CONTACT_NO=@CONTACT_NO, GENDER=@GENDER, PICTURE=@PICTURE, WORK_SCHEDULE=@WORK_SCHEDULE WHERE EMPID = @EMPID", cn);

                    cm.Parameters.AddWithValue("@EMPID", label21.Text);
                    cm.Parameters.AddWithValue("@EMPLOYEEID", txtEmployeeID.Text);
                    cm.Parameters.AddWithValue("@RFID", txtRFID.Text);
                    cm.Parameters.AddWithValue("@FIRSTNAME", txtFirstName.Text);
                    cm.Parameters.AddWithValue("@MIDDLENAME", txtMiddleName.Text);
                    cm.Parameters.AddWithValue("@LASTNAME", txtLastName.Text);
                    cm.Parameters.AddWithValue("@BIRTHDAY", dtBirthday.Value);
                    cm.Parameters.AddWithValue("@DEPARTMENT", cboDepartment.Text);
                    cm.Parameters.AddWithValue("@DESIGNATION", txtDesignation.Text);
                    cm.Parameters.AddWithValue("@STATUS", cboStatus.Text);
                    cm.Parameters.AddWithValue("@PATH", txtPath.Text);
                    cm.Parameters.AddWithValue("@ADDRESS", txtAddress.Text);
                    cm.Parameters.AddWithValue("@MARITAL_STATUS", cboMaritalStatus.Text);
                    cm.Parameters.AddWithValue("@CONTACT_NO", mtContactNo.Text);
                    cm.Parameters.AddWithValue("@GENDER", cboGender.Text);
                    cm.Parameters.AddWithValue("@WORK_SCHEDULE", cboWorkSched.Text);
                    cm.Parameters.AddWithValue("@PICTURE", arrImage);

                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("RECORD HAS SUCCESSFULLY UPDATED", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    //pictureBox1.BackgroundImage = pictureBox1.InitialImage;
                    f.LoadRecords();
                    f.AutoLoad1();
                  //Clear();                    
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("YOU WANT TO DELETE THIS RECORD? CLICK YES TO CONFIRM", _title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cn.Open();
                cm = new MySqlCommand("DELETE FROM table_employee WHERE EMPID = '" + label21.Text + "'", cn);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("RECORD HAS BEEN SUCCESSFULLY DELETED", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.Close();
                f.LoadRecords();
                f.AutoLoad1();
            }
        }
    }
}
