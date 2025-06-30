<%@ Page Title="TeacherDashbord" Language="C#" MasterPageFile="~/Teachers.Master" AutoEventWireup="true" CodeBehind="TeacherDashboard.aspx.cs" Inherits="School_Attendance_System.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
         <h2>Welcome, <asp:Label ID="lblHeadmistressName" runat="server" Text="Teacher"></asp:Label></h2>
<div class="row mt-4">
    <div class="col-md-3">
        <div class="card text-white bg-primary mb-3 text-center">
            <div class="card-header">Total Students</div>
            <div class="card-body">
               <h5 class="card-title">
    <asp:Label ID="lblStudentCount" runat="server" Text="0"></asp:Label>
</h5>
            </div>
        </div>
    </div>

    <div class="col-md-3">
        <div class="card text-white bg-success mb-3 text-center">
            <div class="card-header">Total Present</div>
            <div class="card-body">
                <h5 class="card-title">
                    <asp:Label ID="lblAttendanceCount" runat="server" Text="0"></asp:Label>
                </h5>
            </div>
        </div>
    </div>

    <div class="col-md-3">
        <div class="card text-white bg-warning mb-3 text-center">
            <div class="card-header">Total Absent</div>
            <div class="card-body">
                <h5 class="card-title">
    <asp:Label ID="lblAttendance" runat="server" Text="0"></asp:Label>
</h5>
            </div>
        </div>
    </div>
    </div>
    </div>
</asp:Content>
