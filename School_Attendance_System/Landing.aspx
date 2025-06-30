<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Landing.aspx.cs" Inherits="School_Attendance_System.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>School Attendance Management System</title>
    <link href="css/all.min.css" rel="stylesheet" />
    <link href="css/Styles.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap" rel="stylesheet"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="header" style="width: 100%;">
            <div class="brand">
                <img src="Images/RTking.jpg" />
            </div>
            <div class="nav">
                <a href="#">Home</a>
                <a href="#">Features</a>
                <a href="#">About Us</a>
                <a href="#">Contact Us</a>
                <a href="Login.aspx">Login</a>
                <div class="icon">
                    <a href="javascript:void(0);" onclick="myFunction()" aria-label="Toggle Navigation">&#9776;</a>
                </div>
            </div>
        </div>
        <div class="main">
            <div class="container-fluid text-center bg-light py-4">
                <h1 class="display-4">Welcome to the Attendance Management System</h1>
                <p class="lead">A Simple and efficient way to track student's attendance.</p>
                <a href="Login.aspx" class="btn btn-primary btn-lg">Get Started</a>
            </div>
            <div class="Container text-center mt-5">
                <h2>Why Choose our System?</h2>
                <div class="row mt-4">
                    <div class="col-md-4">
                        <div class="card">
                            <i class="fa fa-clock fa-3x text-primary"></i>
                            <h4>Real-time Attendance</h4>
                            <p>Track attendance instantly and generates reports.</p>
                        </div>
                    </div>
                    <div class="col-md-4 text-center hover-shadow">
                        <div class="card">
                            <i class="fa fa-mobile fa-3x text-primary"></i>
                            <h4>Mobile Access</h4>
                            <p>Mark Attendance using any device, anywhere.</p>
                        </div>
                    </div>
                    <div class="col-md-4 text-center">
                        <div class="card">
                            <i class="fa fa-chart-line fa-3x text-primary"></i>
                            <h4>Performance Insights</h4>
                            <p>Monitor attendance trends and student behavior</p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="container mt-5 text-center ">
                <h2 class="text-center">How It Works</h2>
                <div class="row mt-4">
                    <div class="col-md-4">
                        <div class="card" style="padding: 10px;">
                            <i class="fa fa-calendar-check fa-3x text-primary"></i>
                            <h4>Step 1</h4>
                            <p>Teacher logs in and selects the class.</p>
                        </div>
                    </div>
                    <div class="col-md-4 text-center">
                        <div class="card">
                            <i class="fa fa-edit fa-3x text-primary"></i>
                            <h4>Step 2</h4>
                            <p>Marks attendance for Students.</p>
                        </div>
                    </div>
                    <div class="col-md-4 text-center">
                        <div class="card">
                            <i class="fa fa-database fa-3x text-primary"></i>
                            <h4>Step 3</h4>
                            <p>Data is stored securely in the system.</p>
                        </div>
                    </div>
                    <div class="col-md-4 text-center">
                        <div class="card">
                            <i class="fa fa-chart-pie fa-3x text-primary"></i>
                            <h4>Step 4</h4>
                            <p>The School head generates reports.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <footer class="bg-dark text-white text-center py-3 mt-5">

            <div class="social-links">
                <a href="#" aria-label="Facebook"><i class="fa fa-facebook"></i></a>
                <a href="#" aria-label="Twitter"><i class="fa fa-twitter"></i></a>
                <a href="#" aria-label="LinkedIn"><i class="fa fa-linkedin"></i></a>
            </div>
            <div class="newsletter-form">
                <input type="email" placeholder="Enter your email" required>
                <button type="submit">Subscribe</button>
            </div>
            <p>&copy; 2025 Primay School Attendance System. All rights reserved.</p>
        </footer>

    </form>
    <script src="js/jquery-3.7.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/js.js"></script>
</body>
</html>
