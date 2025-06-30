using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School_Attendance_System
{
    public partial class SiteMaster : MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
 if (!IsPostBack)
            {
                LoadSearchResults("", "");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSearchResults(txtSearch.Text.Trim(), ddlClass.SelectedValue);
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadSearchResults(txtSearch.Text.Trim(), ddlClass.SelectedValue);
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSearchResults(txtSearch.Text.Trim(), ddlClass.SelectedValue);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ddlClass.SelectedIndex = 0; // Reset to "Select Class"
            gvResults.DataSource = null;
            gvResults.DataBind();
            updResults.Update(); // Refresh the UpdatePanel
        }

        private void LoadSearchResults(string searchQuery, string classFilter)
        {
            ConnectionStringSettings connSettings = ConfigurationManager.ConnectionStrings["PsamsConnectionString"];

            if (connSettings == null)
            {
                throw new Exception("Database connection string 'PsamsConnectionString' is missing in Web.config.");
            }

            string connStr = connSettings.ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            SELECT ID, Firstname, Classid, 'Student' AS UserType 
            FROM Students 
            WHERE 
                (@Search = '' OR Firstname LIKE '%' + @Search + '%' OR ID LIKE '%' + @Search + '%' OR Classid LIKE '%' + @Search + '%')
                AND (@Class = '' OR Classid = @Class)
            UNION
            SELECT ID, Firstname, Classid, 'Teacher' AS UserType 
            FROM Teachers 
            WHERE 
                (@Search = '' OR Firstname LIKE '%' + @Search + '%' OR ID LIKE '%' + @Search + '%')
                AND (@Class = '' OR Classid = @Class)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Search", string.IsNullOrEmpty(searchQuery) ? "" : searchQuery);
                    cmd.Parameters.AddWithValue("@Class", string.IsNullOrEmpty(classFilter) ? "" : classFilter);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if(gvResults != null)
{
                        gvResults.DataSource = dt;
                        gvResults.DataBind();
                    }
else
                    {
                        // Log error or throw an exception if gvResults is null
                        Console.WriteLine("gvResults is null");
                    }

                }
            }

            // Ensure updResults exists before updating
            if (updResults != null)
            {
                updResults.Update();
            }
        }

    }
}
