using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School_Attendance_System
{
    public partial class Downloads : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["PsamsConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFileGrid();
            }
        }

        private void BindFileGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FileName");
            dt.Columns.Add("FileType");

            string[] fileTypes = { "timetable", "announcement", "assignment", "teacher_announcement", "teacher_assignment" };

            foreach (var type in fileTypes)
            {
                string folderPath = Server.MapPath($"~/Uploads/{type}/");
                if (Directory.Exists(folderPath))
                {
                    string[] files = Directory.GetFiles(folderPath);
                    foreach (var file in files)
                    {
                        dt.Rows.Add(Path.GetFileName(file), type);
                    }
                }
            }

            gvFiles.DataSource = dt;
            gvFiles.DataBind();
        }

        protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                string[] args = e.CommandArgument.ToString().Split('|');
                string fileName = args[0];
                string fileType = args[1];
                string filePath = Server.MapPath($"~/Uploads/{fileType}/{fileName}");

                if (File.Exists(filePath))
                {
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                    Response.TransmitFile(filePath);
                    Response.End();
                }
                else
                {
                    string script = "alert('File not found.');";
                    ClientScript.RegisterStartupScript(this.GetType(), "FileNotFound", script, true);
                }
            }
        }

    }
}