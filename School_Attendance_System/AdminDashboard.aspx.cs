using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolAttendanceSystem
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

            lblTeacherCount.Text = GetCount("SELECT COUNT(*) FROM Teachers").ToString();
            lblStudentCount.Text = GetCount("SELECT COUNT(*) FROM Students").ToString();


            string today = DateTime.Now.ToString("yyyy-MM-dd");
            lblTodayAttendance.Text = GetCount("SELECT COUNT(*) FROM Attendance WHERE CONVERT(date, [Date]) = @Today AND Status='Present'",
                                                  new SqlParameter("@Today", today)).ToString();

            lblNotificationCount.Text = GetCount("SELECT COUNT(*) FROM Notifications").ToString();

            if (Session["UserName"] != null)
            {
                lblHeadmistressName.Text = Session["UserName"].ToString();
            }
            else
            {
                lblHeadmistressName.Text = "SchoolHead";
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