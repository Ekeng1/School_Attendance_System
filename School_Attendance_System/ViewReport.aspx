<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewReport.aspx.cs" Inherits="School_Attendance_System.ViewReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report</title>
        <link href="css/all.min.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <style>
        /* General Layout */
body {
    font-family: Arial, sans-serif;
    background-color: #f7f7f7;
    margin: 0;
    padding: 0;
}

/* Container */
.container {
    width: 60%;
    margin: 50px auto;
    background-color: white;
    padding: 20px;
    box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
}

/* Header */
.header {
    text-align: center;
    margin-bottom: 30px;
}

.header h2 {
    color: #333;
}

/* Form Inputs */
.form-group {
    margin-bottom: 15px;
}

label {
    font-weight: bold;
    color: #333;
}

.input-field {
    width: 100%;
    padding: 10px;
    margin-top: 5px;
    border-radius: 5px;
    border: 1px solid #ddd;
}

/* Buttons */
.btn-primary {
    background-color: #4CAF50;
    color: white;
    padding: 10px 15px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    width: 100%;
}

.btn-primary:hover {
    background-color: #45a049;
}

/* Message */
.message {
    margin-bottom: 20px;
}

/* GridView */
.attendance-table {
    margin-top: 30px;
}

.table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 15px;
}

.table, .table th, .table td {
    border: 1px solid #ddd;
}

.table th, .table td {
    padding: 12px;
    text-align: center;
}

.table th {
    background-color: #f2f2f2;
}
 .input-field{
     margin-top: 11px;
     margin-bottom: 11px;
 }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Header -->
            <div class="header">
                <h2 style="align-items: center; color: #152259; margin: 5% auto;">Attendance Report</h2>
            </div>

            <!-- Notification Message -->
            <div class="message">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </div>

            <!-- Student ID input and Button -->
            <div class="form-group">
                <label for="txtStudentID">Enter Student ID:</label>
                <asp:TextBox ID="txtStudentID" runat="server" CssClass="input-field" placeholder="Student ID"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Button ID="btnViewReport" runat="server" Text="View Report" style="margin-top: 11px;" CssClass="btn btn-primary"  OnClick="btnViewReport_Click" />
            </div>

            <!-- Attendance Grid View -->
            <div class="attendance-table">
                <asp:GridView ID="gvReports" runat="server" CssClass="table" AutoGenerateColumns="False" 
                    EmptyDataText="No records found" ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
