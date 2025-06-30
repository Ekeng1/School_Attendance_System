using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School_Attendance_System
{
    public partial class WebForm7 : System.Web.UI.Page
    {

        private string connectionString = "Data Source=DESKTOP-S83IEQ8;Initial Catalog=Psams;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTeachers();
            }
        }
        private void LoadTeachers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT id, Firstname, Lastname, DOB, Address, Email, MobileNo, 
                              Gender, MaritalStatus, Status, Hiredate, 
                              (SELECT SectionName FROM Sections WHERE SectionID = Sectionid) AS Section,
                              (SELECT ClassName FROM Classes WHERE ClassID = Classid) AS Class, Photo 
                              FROM Teachers";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvTeachers.DataSource = dt;
                    gvTeachers.DataBind();
                }
            }
        }

        protected void gvTeachers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int teacherID = Convert.ToInt32(gvTeachers.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvTeachers.Rows[e.RowIndex];

            string firstName = ((TextBox)row.FindControl("txtFirstName")).Text.Trim();
            string email = ((TextBox)row.FindControl("txtEmail")).Text.Trim();
            string mobileNo = ((TextBox)row.FindControl("txtMobileNo")).Text.Trim();
            string gender = ((DropDownList)row.FindControl("ddlGender")).SelectedValue;
            string password = ((TextBox)row.FindControl("txtPassword")).Text.Trim();

            string encryptedPassword = Encrypt(password);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Teachers SET Firstname = @Firstname, Email = @Email, 
                              MobileNo = @MobileNo, Gender = @Gender, Password = @Password 
                              WHERE id = @TeacherID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TeacherID", teacherID);
                    cmd.Parameters.AddWithValue("@Firstname", firstName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@MobileNo", mobileNo);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            gvTeachers.EditIndex = -1;
            LoadTeachers();
        }

        protected void gvTeachers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int teacherID = Convert.ToInt32(gvTeachers.DataKeys[e.RowIndex].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Teachers WHERE id = @TeacherID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TeacherID", teacherID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            LoadTeachers();
        }

        protected void btnAddTeacher_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string dob = txtDOB.Text.Trim();
            string email = txtEmail.Text.Trim();
            string mobileNo = txtMobileNo.Text.Trim();
            string address = txtAddress.Text.Trim();
            string password = txtPassword.Text.Trim();
            string gender = ddlGender.SelectedValue;
            string maritalStatus = ddlMaritalStatus.SelectedValue;
            string status = ddlStatus.SelectedValue;
            string hireDate = txtHireDate.Text.Trim();
            string section = txtSection.Text.Trim();
            string className = txtClass.Text.Trim();
            string photoPath = SavePhoto(fuPhoto);

            string encryptedPassword = Encrypt(password);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Teachers 
                            (Firstname, Lastname, DOB, Address, Email, MobileNo, Password, Gender, 
                             MaritalStatus, Status, Hiredate, Sectionid, Classid, Photo) 
                            VALUES (@Firstname, @Lastname, @DOB, @Address, @Email, @MobileNo, @Password, 
                                    @Gender, @MaritalStatus, @Status, @Hiredate, 
                                    (SELECT SectionID FROM Sections WHERE SectionName = @Section), 
                                    (SELECT ClassID FROM Classes WHERE ClassName = @Class), @Photo)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Firstname", firstName);
                    cmd.Parameters.AddWithValue("@Lastname", lastName);
                    cmd.Parameters.AddWithValue("@DOB", dob);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@MobileNo", mobileNo);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@MaritalStatus", maritalStatus);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Hiredate", hireDate);
                    cmd.Parameters.AddWithValue("@Section", section);
                    cmd.Parameters.AddWithValue("@Class", className);
                    cmd.Parameters.AddWithValue("@Photo", photoPath);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            LoadTeachers();
        }

        private string SavePhoto(FileUpload fileUpload)
        {
            if (fileUpload.HasFile)
            {
                string filePath = "~/Uploads/" + fileUpload.FileName;
                fileUpload.SaveAs(Server.MapPath(filePath));
                return filePath;
            }
            return "~/Uploads/default.png";
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
    }
}