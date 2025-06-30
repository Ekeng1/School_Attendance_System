<%@ Page Title="Manage Teachers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageTeachers.aspx.cs" Inherits="School_Attendance_System.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="d-flex justify-content-between mb-3">
                    <asp:Button ID="btnShowModal" runat="server" Text="Add Teacher" CssClass="button" BackColor="#4c8abd" OnClientClick="showModal(); return false;" />
                </div>

             <asp:GridView ID="gvTeachers" runat="server" CssClass="grid" AutoGenerateColumns="False" 
              OnRowEditing="gvTeachers_RowEditing" 
              OnRowCancelingEdit="gvTeachers_RowCancelingEdit" 
              OnRowUpdating="gvTeachers_RowUpdating" 
              OnRowDeleting="gvTeachers_RowDeleting" 
              DataKeyNames="teacherId">
    <Columns>
        <asp:BoundField DataField="teacherId" HeaderText="ID" ReadOnly="True" SortExpression="teacherId" />
        <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
        <asp:BoundField DataField="Section_id" HeaderText="Section" SortExpression="Section_id" />
        <asp:BoundField DataField="id" HeaderText="Class" SortExpression="id" />
        <asp:BoundField DataField="MobileNo" HeaderText="Mobile" SortExpression="MobileNo" />

        <asp:TemplateField HeaderText="Photo" SortExpression="Photo">
            <ItemTemplate>
                <asp:Image ID="imgPhoto" runat="server" Width="50" Height="50"
                           ImageUrl='<%# Eval("Photo") != DBNull.Value ? "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("Photo")) : "~/Images/placeholder.jpg" %>' />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:FileUpload ID="FileUploadPhoto" runat="server" />
                <asp:Image ID="imgEditPhoto" runat="server" Width="50" Height="50"
                           ImageUrl='<%# Eval("Photo") != DBNull.Value ? "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("Photo")) : "~/Images/placeholder.jpg" %>' />
            </EditItemTemplate>
        </asp:TemplateField>

<asp:TemplateField ShowHeader="False">
    <EditItemTemplate>
        <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update" CssClass="btn btn-success btn-sm" Text="✔ Save"></asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" CssClass="btn btn-secondary btn-sm" Text="✖ Cancel"></asp:LinkButton>
    </EditItemTemplate>
    <ItemTemplate>
        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-primary btn-sm" Text="Edit"></asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete" CssClass="btn btn-danger btn-sm" Text="Delete" OnClientClick="return confirm('Are you sure?');"></asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>

    </Columns>
</asp:GridView>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" id="addTeacherModal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add Teacher</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control mb-2" Placeholder="First Name"></asp:TextBox>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control mb-2" Placeholder="Last Name"></asp:TextBox>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-2" Placeholder="Email"></asp:TextBox>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control mb-2" Placeholder="Mobile Number"></asp:TextBox>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control mb-2" Placeholder="Password" TextMode="Password"></asp:TextBox>
                    <asp:FileUpload ID="FileUploadPhoto" runat="server" CssClass="form-control mb-2" />
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveTeacher" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSaveTeacher_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        function showModal() {
            var myModal = new bootstrap.Modal(document.getElementById('addTeacherModal'));
            myModal.show();
        }

        function closeModal() {
            var myModal = new bootstrap.Modal(document.getElementById('addTeacherModal'));
            myModal.hide();
        }
    </script>
</asp:Content>