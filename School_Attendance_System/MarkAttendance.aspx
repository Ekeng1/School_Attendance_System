<%@ Page Title="MarkAttendance" Language="C#" MasterPageFile="~/Teachers.Master" AutoEventWireup="true" CodeBehind="MarkAttendance.aspx.cs" Inherits="School_Attendance_System.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .container{
            overflow-x: scroll;
        }
    </style>
    <div class="container">
        <h2 style="align-items: center; color: #152259; font-weight: bold;">Mark Attendance</h2>
        
        <!-- Dropdown for Section Selection -->
        <asp:DropDownList ID="ddlSection" CssClass="textbox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
            <asp:ListItem Text="Select Section" Value="" />
        </asp:DropDownList>
        <br />

        <!-- Dropdown for Class Selection -->
        <asp:DropDownList ID="ddlClass" CssClass="textbox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
            <asp:ListItem Text="Select Class" Value="" />
        </asp:DropDownList>
        <br />

        <!-- Button to Fetch Students -->
        <asp:Button ID="btnFetchStudents" runat="server" CssClass="btn btn-primary" DataKeyNames="student_id" Text="Fetch Students" OnClick="btnFetchStudents_Click"/>
        
        <!-- GridView to Display Students and Attendance Options -->
      <asp:GridView ID="gvStudents" runat="server" CssClass="grid" style="overflow-x:auto;"
    AutoGenerateColumns="False" DataKeyNames="student_id">
    <Columns>
        <asp:BoundField DataField="student_id" HeaderText="ID" Visible="false" />
        
        <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
        
        <asp:TemplateField HeaderText="Attendance">
            <ItemTemplate>
                <asp:DropDownList ID="ddlAttendance" CssClass="textbox" runat="server">
                    <asp:ListItem Text="Present" Value="Present" Selected="True" />
                    <asp:ListItem Text="Absent" Value="Absent" />
                    <asp:ListItem Text="Late" Value="Late" />
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
        <br />

        <!-- Button to Save Attendance -->
        <asp:Button ID="btnSaveAttendance" runat="server" CssClass="btn btn-primary" Text="Save Attendance" OnClick="btnSaveAttendance_Click" />
        
        <!-- Button to Send SMS to Absent Students -->
        <asp:Button ID="btnSendSMS" runat="server" CssClass="btn btn-primary" Text="Send SMS to Absent Parents" OnClick="btnSendSMS_Click" />
        
        <!-- Label to show Missing Contacts Alerts -->
        <asp:Label ID="lblMissingContacts" runat="server"  ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
