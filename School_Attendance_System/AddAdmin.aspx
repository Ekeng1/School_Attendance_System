<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAdmin.aspx.cs" Inherits="School_Attendance_System.AddAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Admin</title>
        <link href="css/all.min.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
        font-family: Arial, sans-serif;
        background-color: #CCD6DD;
        margin: 0;
        padding: 0;
    }

    .container {
        width: 100%;
        max-width: 500px;
        margin: 50px auto;
        background: #ffffff;
        border-radius: 10px;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        padding: 30px;
    }

        .container h2 {
            text-align: center;
            margin-bottom: 20px;
            color: #333333;
        }

    .form-group {
        margin-bottom: 15px;
    }

        .form-group label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
            color: #555555;
        }

        .form-group input,
        .form-group select {
            width: 100%;
            padding: 10px;
            border: 1px solid #cccccc;
            border-radius: 5px;
            font-size: 14px;
            background-color: #f9f9f9;
        }

            .form-group input:focus,
            .form-group select:focus {
                border-color: #007bff;
                outline: none;
            }

        .form-group select {
            height: 40px;
        }

        .form-group button {
            width: 100%;
            padding: 12px;
            background-color: #007bff;
            color: #ffffff;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
            margin-top: 10px;
        }

            .form-group button:hover {
                background-color: #0056b3;
            }

    .form-footer {
        text-align: center;
        margin-top: 20px;
        font-size: 14px;
        color: #777777;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
         <div class="container">
     <h2>Add New Admin</h2>
         <div>
             <div class="form-group">
                 <label for="txtFirstName">First Name:</label>
                 <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server" />
             </div>

             <div class="form-group">
                 <label for="txtLastName">Last Name:</label>
                 <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server" />
             </div>

             <div class="form-group">
                 <label for="ddlGender">Gender:</label>
                 <asp:DropDownList ID="ddlGender" CssClass="form-control" runat="server">
                     <asp:ListItem Text="Select" Value="" />
                     <asp:ListItem Text="Male" Value="M" />
                     <asp:ListItem Text="Female" Value="F" />
                 </asp:DropDownList>
             </div>

             <div class="form-group">
                 <label for="txtMobileNo">Mobile Number:</label>
                 <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server" />
             </div>

             <div class="form-group">
                 <label for="txtEmail">Email:</label>
                 <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" />
             </div>
             <div class="form-group">
    <label for="ddlRole">Gender:</label>
    <asp:DropDownList ID="ddlRole" CssClass="form-control" runat="server">
        <asp:ListItem Text="Select" Value="" />
        <asp:ListItem Text="1" Value="1" />
        <asp:ListItem Text="2" Value="2" />
        <asp:ListItem Text="3" Value="3" />
    </asp:DropDownList>
</div>

             <div class="form-group">
                 <label for="txtPassword">Password:</label>
                 <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" runat="server" />
             </div>

             <div class="form-group">
                 <asp:Button ID="btnSubmit" BackColor="#007bff" CssClass="btn btn-primary" Text="Add Admin" runat="server" OnClick="btnSubmit_Click" />
             </div>

             <div class="form-footer">
                 © 2024 Admin Management System
    
             </div>
         </div>
     </div>
    </form>
</body>
</html>
