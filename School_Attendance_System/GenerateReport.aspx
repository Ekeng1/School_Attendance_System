<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GenerateReport.aspx.cs" Inherits="School_Attendance_System.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">
    <h2>Generate Attendance Report</h2>
    
    <!-- Section Selection Dropdown -->
    <div class="form-group">
        <label for="ddlSection">Select Section:&nbsp;  </label>
        <asp:DropDownList ID="ddlSection" runat="server" CssClass="textbox" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
            <asp:ListItem Text="Select Section" Value="" />
        </asp:DropDownList>
    </div>
    
    <!-- Class Selection Dropdown -->
    <div class="form-group">
        <label for="ddlClass">Select Class:&nbsp;    </label>
        <asp:DropDownList ID="ddlClass" runat="server" CssClass="textbox" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
            <asp:ListItem Text="Select Class" Value="" />
        </asp:DropDownList>
    </div>
    
    <!-- Student Selection Dropdown -->
    <div class="form-group">
        <label for="ddlStudent">Select Student:&nbsp;    </label>
        <asp:DropDownList ID="ddlStudent" runat="server" CssClass="textbox" AutoPostBack="true" OnSelectedIndexChanged="ddlStudent_SelectedIndexChanged">
            <asp:ListItem Text="Select Student" Value="" />
        </asp:DropDownList>
    </div>
    
    <!-- Time Period Selection -->
    <div class="form-group">
        <label for="ddlPeriod">Select Time Period:&nbsp;</label>
        <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="textbox">
            <asp:ListItem Value="weekly">Weekly</asp:ListItem>
            <asp:ListItem Value="monthly">Monthly</asp:ListItem>
            <asp:ListItem Value="termly">Termly</asp:ListItem>
        </asp:DropDownList>
    </div>
    
    <!-- Generate Report Button -->
    <asp:Button ID="btnGenerateReport" runat="server" Text="Generate Report" CssClass="button" style="background-color: #4c8abd;" OnClick="btnGenerateReport_Click" />
    
    <!-- Error/No Data Message -->
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
    
    <!-- Report Preview Table -->
    <asp:GridView ID="gvReportPreview" runat="server" Visible="false" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Date" DataField="Date" />
            <asp:BoundField HeaderText="Status" DataField="Status" />
        </Columns>
    </asp:GridView>
    
    <!-- Summary Report Table -->
    <asp:GridView ID="gvSummaryReport" runat="server" Visible="false" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Student Name" DataField="StudentName" />
            <asp:BoundField HeaderText="Present" DataField="Present" />
            <asp:BoundField HeaderText="Absent" DataField="Absent" />
            <asp:BoundField HeaderText="Late" DataField="Late" />
        </Columns>
    </asp:GridView>
    
    <!-- Download & Print Buttons -->
    <asp:Button ID="btnDownload" runat="server" Text="Download Report (PDF)" OnClick="btnDownload_Click" CssClass="btn btn-success" Visible="false" />
    <asp:Button ID="btnPrint" runat="server" Text="Print Report" OnClick="btnPrint_Click" CssClass="btn btn-warning" Visible="false" />
</div>
</asp:Content>
