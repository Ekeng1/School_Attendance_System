<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="School_Attendance_System.AdminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AdminLogin</title>
    <link href="css/Styles.css" rel="stylesheet" />
    <link href="css/all.min.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="form">
                <div class="form-header">
                    <%--<img src="path/to/your/logo.png" alt="School XXX" class="logo" />--%>
                    <h5>Login to Your Account</h5>
                </div>
                <div class="form-body">
                    <div class="form-group">
                        <asp:Label ID="lblUsername" runat="server" CssClass="label" Text="Username"></asp:Label>
                        <asp:TextBox ID="txtUsername" CssClass="textbox" placeholder="Enter Username" runat="server"></asp:TextBox>
                    </div>
<div class="form-group">
    <asp:Label ID="lblPassword" runat="server" CssClass="label" Text="Password"></asp:Label>
        <asp:TextBox ID="txtPassword" CssClass="textbox password-input" TextMode="Password" placeholder="Enter Password" runat="server"></asp:TextBox>
    </div>
                    <asp:Button ID="Submitbtn" CssClass="button" runat="server" Text="Login" OnClick="btnSubmit_Click" />
                </div>
                <div class="lblmsg">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div class="privacy-notice">
            <p>By logging in, you agree to our <a href="privacy-policy.aspx">Privacy Policy</a>.</p>
        </div>
            </div>
        </div>
    </form>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery-3.7.1.min.js"></script>
    <script src="js/js.js"></script>
</body>
</html>
