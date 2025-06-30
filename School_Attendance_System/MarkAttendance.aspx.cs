using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace School_Attendance_System
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        private string connectionString = "Data Source=DESKTOP-S83IEQ8;Initial Catalog=Psams;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSections(); // Load sections on initial page load
            }
        }

        // Load sections into the dropdown list
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
                ddlSection.Items.Insert(0, new ListItem("Select Section", "0"));
            }
        }
        private void BindStudentsGrid(string classId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Make sure this query returns student_id
                string query = @"SELECT s.student_id, 
                                s.FirstName + ' ' + s.LastName AS StudentName 
                         FROM Students s
                         WHERE s.class_id = @classId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@classId", classId);

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            gvStudents.DataSource = dt;
            gvStudents.DataBind();
        }
        // Load classes based on the selected section
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sectionId = Convert.ToInt32(ddlSection.SelectedValue);

            if (sectionId > 0) // Ensure a valid section is selected
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
                    ddlClass.Items.Insert(0, new ListItem("Select Class", "0"));
                }
            }
            else
            {
                ddlClass.Items.Clear(); // Clear the class dropdown if no section is selected
                ddlClass.Items.Insert(0, new ListItem("Select Class", "0"));
            }
        }

        // Load students based on the selected class
        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            int classId = Convert.ToInt32(ddlClass.SelectedValue);

            if (classId > 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Change "id" to "student_id" in your SELECT statement
                    string query = "SELECT id AS student_id, Firstname + ' ' + Lastname AS StudentName FROM Students WHERE Classid = @class_id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@class_id", classId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    gvStudents.DataSource = reader;
                    gvStudents.DataBind();
                }
            }
            else
            {
                gvStudents.DataSource = null;
                gvStudents.DataBind();
            }
        }
        protected void btnFetchStudents_Click(object sender, EventArgs e)
        {
            string classId = ddlClass.SelectedValue;

            if (!string.IsNullOrEmpty(classId)) // Ensure a valid class is selected
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT id, Firstname + ' ' + Lastname AS StudentName FROM Students WHERE Classid = @class_id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@class_id", classId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    gvStudents.DataSource = reader;
                    gvStudents.DataBind();

                    // Close the connection
                    conn.Close();
                }
            }
            else
            {
                // If no class is selected, clear the GridView
                gvStudents.DataSource = null;
                gvStudents.DataBind();
                lblMissingContacts.Text = "Please select a class to fetch students."; // Display a message
                lblMissingContacts.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Save attendance based on teacher's selection
        protected void btnSaveAttendance_Click(object sender, EventArgs e)
        {
            DateTime attendanceDate = DateTime.Today;
            string classId = ddlClass.SelectedValue;
            int recordsSaved = 0;

            if (!string.IsNullOrEmpty(classId))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    foreach (GridViewRow row in gvStudents.Rows)
                    {
                        string studentId = gvStudents.DataKeys[row.RowIndex].Value.ToString();
                        DropDownList ddl = (DropDownList)row.FindControl("ddlAttendance");
                        string status = ddl.SelectedValue;

                        // Check if attendance already exists
                        string checkQuery = @"SELECT COUNT(*) FROM Attendance 
                                    WHERE student_id = @student_id 
                                    AND Date = @Date";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                        checkCmd.Parameters.AddWithValue("@student_id", studentId);
                        checkCmd.Parameters.AddWithValue("@Date", attendanceDate);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count == 0)
                        {
                            string insertQuery = @"INSERT INTO Attendance 
                                        (class_id, Date, student_id, Status) 
                                        VALUES 
                                        (@class_id, @Date, @student_id, @Status)";
                            SqlCommand cmd = new SqlCommand(insertQuery, conn);
                            cmd.Parameters.AddWithValue("@class_id", classId);
                            cmd.Parameters.AddWithValue("@Date", attendanceDate.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@student_id", studentId);
                            cmd.Parameters.AddWithValue("@Status", status);

                            recordsSaved += cmd.ExecuteNonQuery();
                        }
                    }

                    lblMissingContacts.Text = $"Attendance saved successfully! {recordsSaved} records updated.";
                    lblMissingContacts.ForeColor = System.Drawing.Color.Green;
                }
            }
            else
            {
                lblMissingContacts.Text = "Please select a class to save attendance.";
                lblMissingContacts.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Send SMS to parents of absent students
        protected void btnSendSMS_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT s.Firstname + ' ' + s.Lastname AS StudentName, p.ParentNo 
                    FROM Attendance a 
                    JOIN Students s ON a.student_id = s.id 
                    JOIN Parents p ON s.id = p.student_id 
                    WHERE a.Date = @Today AND a.Status = 'Absent'";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Today", DateTime.Today);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string studentName = reader["StudentName"].ToString();
                        string parentNo = reader["ParentNo"].ToString();

                        if (!string.IsNullOrEmpty(parentNo))
                        {
                            SendSMS(parentNo, studentName);
                        }
                        else
                        {
                            lblMissingContacts.Text += $"Missing contact for {studentName}. "; // Alert for missing contacts
                            lblMissingContacts.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }

        private void SendSMS(string parentNo, string studentName)
            {
            try
            {
                // Load Twilio credentials from web.config
                string accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
                string authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
                string twilioPhoneNumber = ConfigurationManager.AppSettings["TwilioPhoneNumber"];

                // Initialize Twilio client
                TwilioClient.Init(accountSid, authToken);

                // Format the phone number (remove spaces, dashes, etc.)
                parentNo = parentNo.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

                // Ensure the number starts with a '+'
                if (!parentNo.StartsWith("+"))
                {
                    parentNo = "+237" + parentNo; // Adjust based on your country code
                }

                // Create and send the message
                var message = MessageResource.Create(
                    body: $"Your child {studentName} was absent today.",
                    from: new Twilio.Types.PhoneNumber(twilioPhoneNumber),
                    to: new Twilio.Types.PhoneNumber(parentNo)
                );

                // Log success (optional)
                System.Diagnostics.Debug.WriteLine($"SMS sent to {parentNo}. Twilio SID: {message.Sid}");
            }
            catch (Exception ex)
            {
                // Log errors (critical for debugging)
                System.Diagnostics.Debug.WriteLine($"Failed to send SMS: {ex.Message}");

                // Optionally show an error to the admin
                lblMissingContacts.Text = "Error sending SMS. Check logs.";
                lblMissingContacts.ForeColor = System.Drawing.Color.Red;
            }
            }
}
}
