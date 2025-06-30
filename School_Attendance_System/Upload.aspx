<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="School_Attendance_System.Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/all.min.css" rel="stylesheet" />
        <style>
        .container {
    position: fixed;
    width: 80vw;
    left: 10vw;
    right: 10vw;
    padding: 30px;
    justify-self: center;
    background: #fff;
    border-radius: 10px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    margin: 15% auto;
}
        .textbox{
            margin-top: 11px;
            margin-bottom: 11px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 style="align-items: center; color: #152259; font-weight: bold;">Upload Files</h2>
            <div class="">
                     <asp:DropDownList ID="ddlFileType" CssClass="textbox" runat="server">
                         <asp:ListItem Value="timetable">Timetable</asp:ListItem>
                         <asp:ListItem Value="announcement">Announcement</asp:ListItem>
                         <asp:ListItem Value="assignment">Assignment</asp:ListItem>
                         <asp:ListItem Value="teacher_announcement">Teacher Announcement</asp:ListItem>
                         <asp:ListItem Value="teacher_assignment">Teacher Assignment</asp:ListItem>
                     </asp:DropDownList><br />
                     <asp:FileUpload ID="fileUpload" CssClass="textbox" runat="server" /><br />
                     <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" style="background-color: #4c8abd; margin-top: 11px;" Text="Upload" OnClick="btnUpload_Click" /><br />
                     <asp:Label ID="lblmsg" runat="server" style="margin-top: 5px;" ForeColor="Red"></asp:Label>
                 </div>
        </div>
    </form>
</body>
</html>
