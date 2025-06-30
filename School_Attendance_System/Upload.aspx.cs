using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School_Attendance_System
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                string fileType = ddlFileType.SelectedValue;
                string fileName = Path.GetFileName(fileUpload.FileName);
                string fileExtension = Path.GetExtension(fileName).ToLower();

                // Validate file type
                if (!IsValidFileType(fileType, fileExtension))
                {
                    string script = "alert('Invalid file type for the selected category.');";
                    ClientScript.RegisterStartupScript(this.GetType(), "InvalidFileType", script, true);
                    return;
                }

                string folderPath = Server.MapPath($"~/Uploads/{fileType}/");

                // Ensure directory exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, fileName);

                // Check if file already exists
                if (File.Exists(filePath))
                {
                    string script = "alert('A file with the same name already exists. Please rename your file.');";
                    ClientScript.RegisterStartupScript(this.GetType(), "FileExists", script, true);
                    return;
                }

                try
                {
                    fileUpload.SaveAs(filePath);
                    // Show success message using JavaScript
                    string script = "alert('Upload successful!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "UploadSuccess", script, true);
                }
                catch (Exception ex)
                {
                    // Show failure message using JavaScript
                    string script = $"alert('Upload failed: {ex.Message}');";
                    ClientScript.RegisterStartupScript(this.GetType(), "UploadFailed", script, true);
                }
            }
            else
            {
                // Show message using JavaScript if no file selected
                string script = "alert('Please select a file to upload.');";
                ClientScript.RegisterStartupScript(this.GetType(), "FileNotSelected", script, true);
            }
        }

        private bool IsValidFileType(string fileType, string fileExtension)
        {
            // Define valid file extensions for each file type
            switch (fileType)
            {
                case "timetable":
                    return fileExtension == ".pdf" || fileExtension == ".xlsx";
                case "announcement":
                    return fileExtension == ".pdf" || fileExtension == ".docx";
                case "assignment":
                    return fileExtension == ".pdf" || fileExtension == ".docx";
                case "teacher_announcement":
                    return fileExtension == ".pdf" || fileExtension == ".docx";
                case "teacher_assignment":
                    return fileExtension == ".pdf" || fileExtension == ".docx";
                default:
                    return false;
            }
        }
    }
}
