<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="School_Attendance_System.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
            <h2>Manage Teachers</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
            <asp:GridView ID="gvTeachers" runat="server" AutoGenerateColumns="False" 

                CssClass="grid"
                DataKeyNames="TeacherID" Width="1057px" >
                <Columns>
                    <asp:BoundField DataField="TeacherID" HeaderText="TeacherID" ReadOnly="True" />
                    <asp:BoundField DataField="Firstname" HeaderText="Firstname" />
                    <asp:BoundField DataField="Lastname" HeaderText="Lastname" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="MobileNo" HeaderText="Mobile Number" />
                    <asp:BoundField DataField="Address" HeaderText="Address" />
                    <asp:BoundField DataField="MaritalStatus" HeaderText="Marital Status" />
                    <asp:BoundField DataField="Photo" HeaderText="Photo" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="Gender" HeaderText="Gender" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ButtonType="Button" ValidationGroup="RowEditing" />
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                        </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnAdd" runat="server" Text="Add New Teacher" OnClick="btnAdd_Click" CssClass="btn btn-primary" />
        </div>

</asp:Content>
