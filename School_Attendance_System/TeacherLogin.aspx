<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherLogin.aspx.cs" Inherits="School_Attendance_System.TeacherLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TeacherLogin</title>
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/all.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="form">
                <div class="form-header">
                    <img src="#" alt="School XXX" />
                </div>
                <div class="form-body">
                    <asp:Label ID="lblUsername" runat="server" CssClass="label" Text="Username"></asp:Label>
                    <asp:TextBox ID="txtUsername" class="textbox" CssClass="textbox" placeholder="Enter Username" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblPassword" runat="server" CssClass="label" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtPassword" class="textbox" CssClass="textbox" TextMode="Password" placeholder="Enter Password" runat="server"></asp:TextBox>

                </div>

                <asp:Button ID="Submitbtn" CssClass="button" runat="server" Text="Login" Height="35px" OnClick="btnSubmit_Click" />


                <div class="lblmsg">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </form>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery-3.7.1.min.js"></script>
    <script src="js/js.js"></script>
</body>
</html>
