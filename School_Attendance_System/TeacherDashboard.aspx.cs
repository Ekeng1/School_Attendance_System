using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School_Attendance_System
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["PsamsConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashboardData();
            }
        }
        private void LoadDashboardData()
        {

            lblStudentCount.Text = GetCount("SELECT COUNT(*) FROM Students").ToString();
            lblAttendanceCount.Text = GetCount("SELECT COUNT(*) FROM Attendance").ToString();


            if (Session["UserName"] != null)
            {
                lblHeadmistressName.Text = Session["UserName"].ToString();
            }
            else
            {
                lblHeadmistressName.Text = "Teacher";
            }
        }

        private int GetCount(string query, params SqlParameter[] parameters)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    count = (int)cmd.ExecuteScalar();
                }
            }
            return count;
        }
    }
}