using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School_Attendance_System
{
    public partial class ViewReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleID"] == null)
            {
                Response.Redirect("Login.aspx"); // Redirect if not logged in
            }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            string roleID = Session["RoleID"].ToString();
            string studentID = txtStudentID.Text.Trim();

            if (string.IsNullOrEmpty(studentID))
            {
                lblMessage.Text = "Please enter a valid student ID.";
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["PsamsConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Date, Status FROM Attendance WHERE student_id = @StudentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        gvReports.DataSource = dt;
                        gvReports.DataBind();
                    }
                    else
                    {
                        lblMessage.Text = "No attendance records found for this student.";
                    }
                }
            }
        }
    }
}
