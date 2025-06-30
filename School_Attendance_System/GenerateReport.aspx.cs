using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School_Attendance_System
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["PsamsConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    // Load sections into the dropdown
                    LoadSections();

                    // Hide the download and print buttons initially
                    btnDownload.Visible = false;
                    btnPrint.Visible = false;
                    gvReportPreview.Visible = false;
                    gvSummaryReport.Visible = false;
                }
            }

            // Load sections into the dropdown
            private void LoadSections()
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT Section_id, Section_name FROM Section";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    ddlSection.DataSource = reader;
                    ddlSection.DataTextField = "Section_name"; // Display text
                    ddlSection.DataValueField = "Section_id"; // Value field
                    ddlSection.DataBind();

                    // Add a default "Select Section" option
                    ddlSection.Items.Insert(0, new ListItem("Select Section", ""));
                }
            }

            // Load classes based on the selected section
            protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
            {
                string sectionId = ddlSection.SelectedValue;

                if (!string.IsNullOrEmpty(sectionId))
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "SELECT id, class_name FROM Class WHERE section_id = @section_id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@section_id", sectionId);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        ddlClass.DataSource = reader;
                        ddlClass.DataTextField = "class_name"; // Display text
                        ddlClass.DataValueField = "id"; // Value field
                        ddlClass.DataBind();

                        // Add a default "Select Class" option
                        ddlClass.Items.Insert(0, new ListItem("Select Class", ""));
                    }
                }
                else
                {
                    ddlClass.Items.Clear(); // Clear the class dropdown if no section is selected
                    ddlClass.Items.Insert(0, new ListItem("Select Class", ""));
                }

                // Clear the student dropdown
                ddlStudent.Items.Clear();
                ddlStudent.Items.Insert(0, new ListItem("Select Student", ""));
            }

            // Load students based on the selected class
            protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
            {
                string classId = ddlClass.SelectedValue;

                if (!string.IsNullOrEmpty(classId))
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "SELECT id, Firstname + ' ' + Lastname AS StudentName FROM Students WHERE Classid = @class_id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@class_id", classId);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        ddlStudent.DataSource = reader;
                        ddlStudent.DataTextField = "StudentName"; // Display text
                        ddlStudent.DataValueField = "id"; // Value field
                        ddlStudent.DataBind();

                        // Add a default "Select Student" option
                        ddlStudent.Items.Insert(0, new ListItem("Select Student", ""));
                    }
                }
                else
                {
                    ddlStudent.Items.Clear(); // Clear the student dropdown if no class is selected
                    ddlStudent.Items.Insert(0, new ListItem("Select Student", ""));
                }
            }
        protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear the GridView and buttons when a new student is selected
            gvReportPreview.Visible = false;
            gvSummaryReport.Visible = false;
            btnDownload.Visible = false;
            btnPrint.Visible = false;

            // Optionally, you can trigger the report generation automatically when a student is selected
            // btnGenerateReport_Click(sender, e);
        }

        // Generate Report Button Click Event
        protected void btnGenerateReport_Click(object sender, EventArgs e)
            {
                string studentId = ddlStudent.SelectedValue; // Get the selected student ID
                string classId = ddlClass.SelectedValue; // Get the selected class ID
                string period = ddlPeriod.SelectedValue; // Get the selected time period

                if (!string.IsNullOrEmpty(studentId))
                {
                    // Generate student-specific report
                    DataTable reportData = GetStudentAttendanceReport(studentId, period);

                    if (reportData.Rows.Count > 0)
                    {
                        // Bind the data to the GridView
                        gvReportPreview.DataSource = reportData;
                        gvReportPreview.DataBind();

                        // Show the GridView and buttons
                        gvReportPreview.Visible = true;
                        gvSummaryReport.Visible = false;
                        btnDownload.Visible = true;
                        btnPrint.Visible = true;

                        // Show success message using JavaScript alert
                        ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "alert('Student report generated successfully!');", true);
                    }
                    else
                    {
                        // Hide the GridView and buttons
                        gvReportPreview.Visible = false;
                        gvSummaryReport.Visible = false;
                        btnDownload.Visible = false;
                        btnPrint.Visible = false;

                        // Show error message using JavaScript alert
                        ScriptManager.RegisterStartupScript(this, GetType(), "showError", "alert('No attendance data found for the selected student and period.');", true);
                    }
                }
                else if (!string.IsNullOrEmpty(classId))
                {
                    // Generate class-wide summary report
                    DataTable summaryData = GetClassAttendanceSummary(classId, period);

                    if (summaryData.Rows.Count > 0)
                    {
                        // Bind the data to the summary GridView
                        gvSummaryReport.DataSource = summaryData;
                        gvSummaryReport.DataBind();

                        // Show the summary GridView and buttons
                        gvSummaryReport.Visible = true;
                        gvReportPreview.Visible = false;
                        btnDownload.Visible = true;
                        btnPrint.Visible = true;

                        // Show success message using JavaScript alert
                        ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "alert('Class summary report generated successfully!');", true);
                    }
                    else
                    {
                        // Hide the GridView and buttons
                        gvSummaryReport.Visible = false;
                        gvReportPreview.Visible = false;
                        btnDownload.Visible = false;
                        btnPrint.Visible = false;

                        // Show error message using JavaScript alert
                        ScriptManager.RegisterStartupScript(this, GetType(), "showError", "alert('No attendance data found for the selected class and period.');", true);
                    }
                }
                else
                {
                    // Show error message if no student or class is selected
                    ScriptManager.RegisterStartupScript(this, GetType(), "showError", "alert('Please select a student or class.');", true);
                }
            }

            // Fetch attendance data for the selected student and period
            private DataTable GetStudentAttendanceReport(string studentId, string period)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "";
                    switch (period)
                    {
                        case "weekly":
                            query = @"
                            SELECT CONVERT(VARCHAR, Date, 23) AS Date, Status 
                            FROM Attendance 
                            WHERE student_id = @studentId AND Date >= DATEADD(DAY, -7, GETDATE()) 
                            ORDER BY Date DESC";
                            break;
                        case "monthly":
                            query = @"
                            SELECT CONVERT(VARCHAR, Date, 23) AS Date, Status 
                            FROM Attendance 
                            WHERE student_id = @studentId AND Date >= DATEADD(MONTH, -1, GETDATE()) 
                            ORDER BY Date DESC";
                            break;
                        case "termly":
                            query = @"
                            SELECT CONVERT(VARCHAR, Date, 23) AS Date, Status 
                            FROM Attendance 
                            WHERE student_id = @studentId AND Date >= DATEADD(MONTH, -3, GETDATE()) 
                            ORDER BY Date DESC";
                            break;
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable reportData = new DataTable();
                    adapter.Fill(reportData);

                    return reportData;
                }
            }

            // Fetch class-wide attendance summary for the selected class and period
            private DataTable GetClassAttendanceSummary(string classId, string period)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "";
                    switch (period)
                    {
                        case "weekly":
                            query = @"
                            SELECT s.Firstname + ' ' + s.Lastname AS StudentName, 
                                   SUM(CASE WHEN a.Status = 'Present' THEN 1 ELSE 0 END) AS Present, 
                                   SUM(CASE WHEN a.Status = 'Absent' THEN 1 ELSE 0 END) AS Absent, 
                                   SUM(CASE WHEN a.Status = 'Late' THEN 1 ELSE 0 END) AS Late 
                            FROM Attendance a 
                            JOIN Students s ON a.student_id = s.id 
                            WHERE s.Classid = @classId AND a.Date >= DATEADD(DAY, -7, GETDATE()) 
                            GROUP BY s.Firstname, s.Lastname";
                            break;
                        case "monthly":
                            query = @"
                            SELECT s.Firstname + ' ' + s.Lastname AS StudentName, 
                                   SUM(CASE WHEN a.Status = 'Present' THEN 1 ELSE 0 END) AS Present, 
                                   SUM(CASE WHEN a.Status = 'Absent' THEN 1 ELSE 0 END) AS Absent, 
                                   SUM(CASE WHEN a.Status = 'Late' THEN 1 ELSE 0 END) AS Late 
                            FROM Attendance a 
                            JOIN Students s ON a.student_id = s.id 
                            WHERE s.Classid = @classId AND a.Date >= DATEADD(MONTH, -1, GETDATE()) 
                            GROUP BY s.Firstname, s.Lastname";
                            break;
                        case "termly":
                            query = @"
                            SELECT s.Firstname + ' ' + s.Lastname AS StudentName, 
                                   SUM(CASE WHEN a.Status = 'Present' THEN 1 ELSE 0 END) AS Present, 
                                   SUM(CASE WHEN a.Status = 'Absent' THEN 1 ELSE 0 END) AS Absent, 
                                   SUM(CASE WHEN a.Status = 'Late' THEN 1 ELSE 0 END) AS Late 
                            FROM Attendance a 
                            JOIN Students s ON a.student_id = s.id 
                            WHERE s.Classid = @classId AND a.Date >= DATEADD(MONTH, -3, GETDATE()) 
                            GROUP BY s.Firstname, s.Lastname";
                            break;
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@classId", classId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable summaryData = new DataTable();
                    adapter.Fill(summaryData);

                    return summaryData;
                }
            }

            // Download Report Button Click Event
            protected void btnDownload_Click(object sender, EventArgs e)
            {
                // Export the GridView data to PDF (you can use a library like iTextSharp or Rotativa)
                // For now, this is a placeholder for the download functionality
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "alert('Download functionality will be implemented here.');", true);
            }

            // Print Report Button Click Event
            protected void btnPrint_Click(object sender, EventArgs e)
            {
                // Print the GridView data
                ScriptManager.RegisterStartupScript(this, GetType(), "printReport", "window.print();", true);
            }
        }
    }