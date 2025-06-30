using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School_Attendance_System
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PsamsConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Teachers";
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvTeachers.DataSource = dt;
                    gvTeachers.DataBind();
                }
            }
        }

        protected void gvTeachers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTeachers.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void gvTeachers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvTeachers.Rows[e.RowIndex];
            string teacherID = gvTeachers.DataKeys[e.RowIndex].Value.ToString();
            string firstname = ((TextBox)row.FindControl("txtFirstname")).Text;
            string lastname = ((TextBox)row.FindControl("txtLastname")).Text;
            string dob = ((TextBox)row.FindControl("txtDOB")).Text;
            string email = ((TextBox)row.FindControl("txtEmail")).Text;
            string mobileNo = ((TextBox)row.FindControl("txtMobileNo")).Text;
            string password = ((TextBox)row.FindControl("txtPassword")).Text;
            string address = ((TextBox)row.FindControl("txtAddress")).Text;
            string maritalStatus = ((TextBox)row.FindControl("txtMaritalStatus")).Text;
            string photo = ((TextBox)row.FindControl("txtPhoto")).Text;
            string status = ((TextBox)row.FindControl("txtStatus")).Text;
            string department = ((TextBox)row.FindControl("txtDepartment")).Text;
            string hireDate = ((TextBox)row.FindControl("txtHireDate")).Text;
            string gender = ((DropDownList)row.FindControl("ddlGender")).SelectedValue;

            string hashedPassword = string.IsNullOrEmpty(password) ? null : HashPassword(password);

            UpdateTeacher(teacherID, firstname, lastname, dob, email, mobileNo, hashedPassword, address, maritalStatus, photo, status, department, hireDate, gender);

            gvTeachers.EditIndex = -1;
            BindGridView();
        }

        protected void gvTeachers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTeachers.EditIndex = -1;
            BindGridView();
        }

        protected void gvTeachers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string teacherID = gvTeachers.DataKeys[e.RowIndex].Value.ToString();
            DeleteTeacher(teacherID);
            BindGridView();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddTeacher.aspx");
        }

        private void UpdateTeacher(string teacherID, string firstname, string lastname, string dob, string email, string mobileNo, string password, string address, string maritalStatus, string photo, string status, string department, string hireDate, string gender)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PsamsConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Teachers SET Firstname = @Firstname, Lastname = @Lastname, DOB = @DOB, Email = @Email, MobileNo = @MobileNo, Address = @Address, MaritalStatus = @MaritalStatus, Photo = @Photo, Status = @Status, Department = @Department, Hiredate = @Hiredate, Gender = @Gender" +
                               (password != null ? ", Password = @Password" : "") +
                               " WHERE TeacherID = @TeacherID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@TeacherID", teacherID);
                    cmd.Parameters.AddWithValue("@Firstname", firstname);
                    cmd.Parameters.AddWithValue("@Lastname", lastname);
                    cmd.Parameters.AddWithValue("@DOB", string.IsNullOrEmpty(dob) ? (object)DBNull.Value : DateTime.Parse(dob));
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@MobileNo", mobileNo);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@MaritalStatus", maritalStatus);
                    cmd.Parameters.AddWithValue("@Photo", photo);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Department", department);
                    cmd.Parameters.AddWithValue("@Hiredate", string.IsNullOrEmpty(hireDate) ? (object)DBNull.Value : DateTime.Parse(hireDate));
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    if (password != null)
                    {
                        cmd.Parameters.AddWithValue("@Password", password);
                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        private void DeleteTeacher(string teacherID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PsamsConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Teachers WHERE TeacherID = @TeacherID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@TeacherID", teacherID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                

                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        protected void gvTeachers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}