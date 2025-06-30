        <%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="SchoolAttendanceSystem.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container container-fluid">
    <h2>Welcome, <asp:Label ID="lblHeadmistressName" runat="server" Text="Headmistress"></asp:Label></h2>

    <div class="row mt-4">
        <div class="col-md-3">
            <div class="card text-white bg-primary mb-3 text-center">
                <div class="card-header">Total Teachers</div>
                <div class="card-body">
                    <h5 class="card-title">
                        <asp:Label ID="lblTeacherCount" runat="server" Text="0"></asp:Label>
                    </h5>
                </div>
            </div>
        </div>

        <div class="col-md-3">
            <div class="card text-white bg-success mb-3 text-center">
                <div class="card-header">Total Students</div>
                <div class="card-body">
                    <h5 class="card-title">
                        <asp:Label ID="lblStudentCount" runat="server" Text="0"></asp:Label>
                    </h5>
                </div>
            </div>
        </div>

        <div class="col-md-3">
            <div class="card text-white bg-warning mb-3 text-center">
                <div class="card-header">Attendance</div>
                <div class="card-body">
                    <h5 class="card-title">
                        <asp:Label ID="lblTodayAttendance" runat="server" Text="0"></asp:Label>
                    </h5>
                </div>
            </div>
        </div>

        <div class="col-md-3">
            <div class="card text-white bg-danger mb-3 text-center">
                <div class="card-header">Notifications</div>
                <div class="card-body">
                    <h5 class="card-title">
                        <asp:Label ID="lblNotificationCount" runat="server" Text="0"></asp:Label>
                    </h5>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
