using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School_Attendance_System
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(txtUsername.Text == "" || txtPassword.Text == ""))
                {

                    SqlDataReader reader;
                    SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["PsamsConnectionString"].ConnectionString.ToString());

                    lblmsg.Text = "";
                    sqlConnection.Open();

                    string roleID = "";
                    string Firstname = "";

                    // Check in Admin (Schoolhead) table
                    SqlCommand cmdAdmin = new SqlCommand("SELECT Firstname, RoleID FROM Schoolhead WHERE Firstname=@firstname AND Password=@password", sqlConnection);
                    cmdAdmin.Parameters.AddWithValue("@firstname", txtUsername.Text);
                    cmdAdmin.Parameters.AddWithValue("@password", Encrypt(txtPassword.Text));
                    reader = cmdAdmin.ExecuteReader();

                    if (reader.Read())
                    {

                        roleID = reader["RoleID"].ToString();
                        Firstname = reader["Firstname"].ToString();
                        reader.Close();
                    }
                    else
                    {
                        reader.Close();

                        // Check in Teachers table
                        SqlCommand cmdTeacher = new SqlCommand("SELECT Firstname, RoleID FROM Teachers WHERE Firstname=@firstname AND Password=@password", sqlConnection);
                        cmdTeacher.Parameters.AddWithValue("@firstname", txtUsername.Text);
                        cmdTeacher.Parameters.AddWithValue("@password", (txtPassword.Text));
                        reader = cmdTeacher.ExecuteReader();

                        if (reader.Read())
                        {
                            roleID = reader["RoleID"].ToString();
                            Firstname = reader["Firstname"].ToString();
                            reader.Close();
                        }
                        else
                        {
                            reader.Close();

                            // Check in Parents table
                            SqlCommand cmdParent = new SqlCommand("SELECT Firstname, RoleID FROM Parents WHERE Firstname=@firstname AND Password=@password", sqlConnection);
                            cmdParent.Parameters.AddWithValue("@firstname", txtUsername.Text);
                            cmdParent.Parameters.AddWithValue("@password", (txtPassword.Text));
                            reader = cmdParent.ExecuteReader();

                            if (reader.Read())
                            {
                                roleID = reader["RoleID"].ToString();
                                Firstname = reader["Firstname"].ToString();
                                reader.Close();
                            }
                            else
                            {
                                reader.Close();
                                sqlConnection.Close();
                                lblmsg.ForeColor = System.Drawing.Color.Red;
                                lblmsg.Text = "Invalid Username or Password!";
                                return;
                            }
                        }
                    }


                    // Store user info in session
                    //Session["UserId"] = txtUsername.Text;
                    //Session["UserPhoto"] = "~/images/-";
                    Session["Firstname"] = Firstname;
                    Session["RoleID"] = roleID;

                    sqlConnection.Close();

                    // Show success message and redirect
                    string script = "<script>alert('Login Successful!'); window.location='";

                    switch (roleID)
                    {
                        case "1": // Admin
                            script += "AdminDashboard.aspx";
                            break;
                        case "2": // Teacher
                            script += "TeacherDashboard.aspx";
                            break;
                        case "3": // Parent
                            script += "ViewReport.aspx";
                            break;
                        default:
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Unauthorized access!";
                            return;
                    }

                    script += "';</script>";

                    // Execute JavaScript alert before redirection
                    ClientScript.RegisterStartupScript(this.GetType(), "loginSuccess", script, false);
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Form cannot be empty";
                }
            }
            catch (Exception ex)
{
    lblmsg.ForeColor = System.Drawing.Color.Red;
    lblmsg.Text = "Error: " + ex.Message; // Show actual error
}

        }
        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
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
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
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