using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;

namespace School_Attendance_System
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        string connectionString = "Data Source=DESKTOP-S83IEQ8;Initial Catalog=Psams;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        string encryptionKey = "MAKV2SPBNI99212"; // Your encryption key
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
                LoadStudents();
                PopulateClassDropdown();
                PopulateSectionDropdown();
            }
        }

        private void BindGridView()
        {
            // 1. Define your SQL query
            string query = "SELECT [id], [StudentID], [Firstname], [Lastname], [Photo] FROM [Psams].[dbo].[Students]";

            // 2. Create a DataTable to store results
            DataTable dt = new DataTable();

            // 3. Get connection string from Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["PsamsConnectionString"].ConnectionString;

            // 4. Connect to the database and fetch data
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Open the connection
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Load data into DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    // Check if data was retrieved
                    if (dt.Rows.Count > 0)
                    {
                        gvStudents.DataSource = dt;
                        gvStudents.DataBind();
                    }
                    else
                    {
                        Response.Write("No records found."); // Optional: Display a message
                    }
                }
                catch (Exception ex)
                {
                    // Log errors (for debugging)
                    Response.Write("Error: " + ex.Message);
                }
            }
        }

        private void LoadStudents(string searchQuery = "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT s.id, s.StudentID, s.Firstname, s.Lastname, s.DOB, s.Gender, 
                               c.class_name, sec.Section_name, s.Address
                        FROM Students s
                        LEFT JOIN Class c ON s.Classid = c.id
                        LEFT JOIN Section sec ON s.sectionid = sec.Section_id
                        WHERE (@searchQuery = '' OR 
                              s.StudentID LIKE '%' + @searchQuery + '%' OR 
                              s.Firstname LIKE '%' + @searchQuery + '%' OR 
                              s.Lastname LIKE '%' + @searchQuery + '%' OR
                              s.Gender LIKE '%' + @searchQuery + '%')";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@searchQuery", searchQuery);
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvStudents.DataSource = dt;
                    gvStudents.DataBind();
                }
            }
        } 

        private void PopulateClassDropdown()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id, class_name FROM Class";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                ddlClass.DataSource = cmd.ExecuteReader();
                ddlClass.DataTextField = "class_name";
                ddlClass.DataValueField = "id";
                ddlClass.DataBind();
                ddlClass.Items.Insert(0, new ListItem("Select Class", ""));
            }
        }

        private void PopulateSectionDropdown()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Section_id, Section_name FROM Section";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                ddlSection.DataSource = cmd.ExecuteReader();
                ddlSection.DataTextField = "Section_name";
                ddlSection.DataValueField = "Section_id";
                ddlSection.DataBind();
                ddlSection.Items.Insert(0, new ListItem("Select Section", ""));
            }
        }
        protected void gvStudents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvStudents.EditIndex = -1;
            LoadStudents();
        }

        protected void gvStudents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string studentId = gvStudents.DataKeys[e.RowIndex].Value.ToString();

                // Get controls from the row
                string firstName = (gvStudents.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox).Text;
                string lastName = (gvStudents.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox).Text;

                // Handle photo upload if present
                byte[] photoData = null;
                FileUpload fuPhoto = gvStudents.Rows[e.RowIndex].FindControl("fuPhoto") as FileUpload;

                if (fuPhoto != null && fuPhoto.HasFile)
                {
                    using (BinaryReader br = new BinaryReader(fuPhoto.PostedFile.InputStream))
                    {
                        photoData = br.ReadBytes(fuPhoto.PostedFile.ContentLength);
                    }
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Students SET 
                           Firstname = @Firstname, 
                           Lastname = @Lastname, 
                           WHERE id = @id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Firstname", firstName);
                    cmd.Parameters.AddWithValue("@Lastname", lastName);
                    cmd.Parameters.AddWithValue("@id", studentId);
                    cmd.Parameters.Add("@Photo", SqlDbType.VarBinary).Value =
                        (object)photoData ?? DBNull.Value;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                gvStudents.EditIndex = -1;
                LoadStudents();
                lblMessage.Text = "Student updated successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error updating student: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void gvStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string studentId = gvStudents.DataKeys[e.RowIndex].Value.ToString();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // First delete parent records
                    string deleteParentQuery = "DELETE FROM Parents WHERE student_id=@id";
                    SqlCommand parentCmd = new SqlCommand(deleteParentQuery, conn);
                    parentCmd.Parameters.AddWithValue("@id", studentId);

                    // Then delete student
                    string deleteStudentQuery = "DELETE FROM Students WHERE id=@id";
                    SqlCommand studentCmd = new SqlCommand(deleteStudentQuery, conn);
                    studentCmd.Parameters.AddWithValue("@id", studentId);

                    conn.Open();
                    parentCmd.ExecuteNonQuery();
                    studentCmd.ExecuteNonQuery();
                }

                LoadStudents();
                lblMessage.Text = "Student deleted successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error deleting student: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void gvStudents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string studentId = gvStudents.DataKeys[e.NewEditIndex].Value.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT s.*, p.Firstname AS ParentFirstname, p.Lastname AS ParentLastname, 
                                        p.Phone, p.Email, p.Address AS ParentAddress, p.Relationship
                                 FROM Students s
                                 LEFT JOIN Parents p ON s.id = p.student_id
                                 WHERE s.id = @StudentID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtStudentID.Text = reader["StudentID"].ToString();
                        txtFirstname.Text = reader["Firstname"].ToString();
                        txtLastname.Text = reader["Lastname"].ToString();
                        txtDOB.Text = Convert.ToDateTime(reader["DOB"]).ToString("yyyy-MM-dd");
                        ddlGender.SelectedValue = reader["Gender"].ToString();
                        ddlClass.SelectedValue = reader["Classid"].ToString();
                        ddlSection.SelectedValue = reader["sectionid"].ToString();

                        // Parent information
                        txtParentFirstname.Text = reader["ParentFirstname"].ToString();
                        txtParentLastname.Text = reader["ParentLastname"].ToString();
                        txtParentPhone.Text = Decrypt(reader["Phone"].ToString());
                        txtParentEmail.Text = reader["Email"] != DBNull.Value ? Decrypt(reader["Email"].ToString()) : "";
                        txtParentAddress.Text = reader["ParentAddress"] != DBNull.Value ? Decrypt(reader["ParentAddress"].ToString()) : "";
                        ddlRelationship.SelectedValue = reader["Relationship"].ToString();
                    }
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "$('#addStudentModal').modal('show');", true);
        }

        protected void btnSaveStudent_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Save Student
                    string studentQuery = @"INSERT INTO Students 
                                          (StudentID, Firstname, Lastname, DOB, Gender, Classid, sectionid, Address)
                                          VALUES 
                                          (@StudentID, @Firstname, @Lastname, @DOB, @Gender, @Classid, @Sectionid, @Address);
                                          SELECT SCOPE_IDENTITY();";

                    SqlCommand studentCmd = new SqlCommand(studentQuery, conn, transaction);
                    studentCmd.Parameters.AddWithValue("@StudentID", GenerateStudentID());
                    studentCmd.Parameters.AddWithValue("@Firstname", txtFirstname.Text.Trim());
                    studentCmd.Parameters.AddWithValue("@Lastname", txtLastname.Text.Trim());
                    studentCmd.Parameters.AddWithValue("@DOB", DateTime.Parse(txtDOB.Text));
                    studentCmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                    studentCmd.Parameters.AddWithValue("@Classid", ddlClass.SelectedValue);
                    studentCmd.Parameters.AddWithValue("@Sectionid", ddlSection.SelectedValue);
                    studentCmd.Parameters.AddWithValue("@Address", Encrypt(txtParentAddress.Text.Trim()));

                    int newStudentId = Convert.ToInt32(studentCmd.ExecuteScalar());

                    // 2. Save Parent
                    string parentQuery = @"INSERT INTO Parents 
                                         (student_id, Firstname, Lastname, Phone, Email, Address, Relationship)
                                         VALUES 
                                         (@student_id, @Firstname, @Lastname, @Phone, @Email, @Address, @Relationship)";

                    SqlCommand parentCmd = new SqlCommand(parentQuery, conn, transaction);
                    parentCmd.Parameters.AddWithValue("@student_id", newStudentId);
                    parentCmd.Parameters.AddWithValue("@Firstname", txtParentFirstname.Text.Trim());
                    parentCmd.Parameters.AddWithValue("@Lastname", txtParentLastname.Text.Trim());
                    parentCmd.Parameters.AddWithValue("@Phone", Encrypt(txtParentPhone.Text.Trim()));
                    parentCmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(txtParentEmail.Text) ?
                        DBNull.Value : (object)Encrypt(txtParentEmail.Text.Trim()));
                    parentCmd.Parameters.AddWithValue("@Address", Encrypt(txtParentAddress.Text.Trim()));
                    parentCmd.Parameters.AddWithValue("@Relationship", ddlRelationship.SelectedValue);

                    parentCmd.ExecuteNonQuery();

                    transaction.Commit();
                    LoadStudents();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal", "$('#addStudentModal').modal('hide');", true);
                    lblMessage.Text = "Record saved successfully!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lblMessage.Text = "Error: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadStudents(txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Search error: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        private string GenerateStudentID()
        {
            // Implement your student ID generation logic here
            return "STD" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
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

        private string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return string.Empty;

            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
