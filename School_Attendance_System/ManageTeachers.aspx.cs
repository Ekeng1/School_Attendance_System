using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace School_Attendance_System
{
    public partial class WebForm4 : System.Web.UI.Page
    {

        string connectionString = "Data Source=DESKTOP-S83IEQ8;Initial Catalog=Psams;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTeachers();
            }
        }

        private void LoadTeachers(string searchQuery = "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT t.teacherId, 
                                t.Firstname + ' ' + t.Lastname AS FullName, 
                                t.Email, 
                                s.Section_id, 
                                c.id, 
                                t.MobileNo,
                                t.Photo
                        FROM Teachers t
                        LEFT JOIN Section s ON t.Sectionid = s.Section_id
                        LEFT JOIN Class c ON t.Classid = c.id
                        WHERE (@searchQuery = '' OR 
                               t.Firstname LIKE '%' + @searchQuery + '%' OR 
                               t.Lastname LIKE '%' + @searchQuery + '%' OR
                               t.Email LIKE '%' + @searchQuery + '%' OR
                               t.MobileNo LIKE '%' + @searchQuery + '%')";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@searchQuery", searchQuery);
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvTeachers.DataSource = dt;
                    gvTeachers.DataBind();
                }
            }
        }

        protected void gvTeachers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTeachers.EditIndex = e.NewEditIndex;
            LoadTeachers();
        }

        protected void gvTeachers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTeachers.EditIndex = -1;
            LoadTeachers();
        }

        protected void gvTeachers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvTeachers.Rows[e.RowIndex];
            string teacherId = gvTeachers.DataKeys[e.RowIndex].Value.ToString();
            string email = ((TextBox)row.Cells[2].Controls[0]).Text;
            string mobileNo = ((TextBox)row.Cells[5].Controls[0]).Text;

            // Handle password if it's being updated
            string password = string.Empty;
            TextBox txtPassword = row.FindControl("txtPassword") as TextBox;
            if (txtPassword != null && !string.IsNullOrEmpty(txtPassword.Text))
            {
                password = (txtPassword.Text);
            }

            byte[] photoData = null;
            FileUpload fuPhoto = (FileUpload)row.FindControl("FileUploadPhoto");

            if (fuPhoto != null && fuPhoto.HasFile)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fuPhoto.PostedFile.InputStream.CopyTo(ms);
                    photoData = ms.ToArray();
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Teachers SET 
                                Email = @Email, 
                                MobileNo = @MobileNo, 
                                " + (!string.IsNullOrEmpty(password) ? "Password = @Password," : "") + @"
                                Photo = @Photo 
                                WHERE teacherId = @TeacherId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@MobileNo", mobileNo);
                    if (!string.IsNullOrEmpty(password))
                    {
                        cmd.Parameters.AddWithValue("@Password", password);
                    }
                    cmd.Parameters.AddWithValue("@Photo", photoData ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TeacherId", teacherId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            gvTeachers.EditIndex = -1;
            LoadTeachers();
        }

        protected void gvTeachers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string teacherId = gvTeachers.DataKeys[e.RowIndex].Value.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Teachers WHERE teacherId = @TeacherId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            LoadTeachers();
        }

        protected void btnSaveTeacher_Click(object sender, EventArgs e)
        {
            byte[] photoData = null;

            if (FileUploadPhoto.HasFile)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    FileUploadPhoto.PostedFile.InputStream.CopyTo(ms);
                    photoData = ms.ToArray();
                }
            }

            string Password = (txtPassword.Text);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Teachers (Firstname, Lastname, Email, MobileNo, Password, Photo) 
                                VALUES (@FirstName, @LastName, @Email, @Mobile, @Password, @Photo)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@Photo", photoData ?? (object)DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            ClearFormFields();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal", "closeModal();", true);
            LoadTeachers();
        }

        //private string Encrypt(string clearText)
        //{
        //    string EncryptionKey = "MAKV2SPBNI99212";
        //    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(clearBytes, 0, clearBytes.Length);
        //                cs.Close();
        //            }
        //            clearText = Convert.ToBase64String(ms.ToArray());
        //        }
        //    }
        //    return clearText;
        //}


        //private string Decrypt(string cipherText)
        //{
        //    string EncryptionKey = "MAKV2SPBNI99212";
        //    byte[] cipherBytes = Convert.FromBase64String(cipherText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                cs.Close();
        //            }
        //            cipherText = Encoding.Unicode.GetString(ms.ToArray());
        //        }
        //    }
        //    return cipherText;
        //}

        private void ClearFormFields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtMobile.Text = "";
            txtPassword.Text = "";
        }
    }
}