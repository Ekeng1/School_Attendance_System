<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Teacher.aspx.cs" Inherits="School_Attendance_System.WebForm7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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
                </ItemTemplate></asp:TemplateField>
                </Columns>
        </asp:GridView>
            <!-- Add Teacher Modal -->
            <div class="modal fade" id="addTeacherModal" tabindex="-1" aria-labelledby="addTeacherModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h2 class="modal-title" id="addTeacherModalLabel">Add Teacher</h2>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="txtTeacherID">Teacher ID</label>
                                <asp:TextBox ID="txtTeacherID" runat="server" CssClass="form-control" placeholder="Enter Teacher ID"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtFirstName">First Name</label>
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="Enter First Name"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtLastName">Last Name</label>
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Enter Last Name"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtDOB">Date of Birth</label>
                                <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtEmail">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtMobileNo">Mobile No</label>
                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="Enter Mobile No"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtSection">Section</label>
                                <asp:TextBox ID="txtSection" runat="server" CssClass="form-control" placeholder="Enter Department"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtClass">Class</label>
                                <asp:TextBox ID="txtClass" runat="server" CssClass="form-control" placeholder="Enter Class"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Password</label>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control mt-2" Placeholder="Password" TextMode="Password" />
                            </div>
                            <div class="form-group">
                                <label>Photo</label>
                                <asp:FileUpload ID="fuPhoto" runat="server" CssClass="form-control mt-2" />
                            </div>
                            <div class="form-group">
                                <label for="txtHireDate">Hire Date</label>
                                <asp:TextBox ID="txtHireDate" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtAddress">Address</label>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Address"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="ddlGender">Gender</label>
                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="ddlMaritalStatus">Marital Status</label>
                                <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="Single">Single</asp:ListItem>
                                    <asp:ListItem Value="Married">Married</asp:ListItem>
                                    <asp:ListItem Value="Divorced">Divorced</asp:ListItem>
                                    <asp:ListItem Value="Widowed">Widowed</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="ddlStatus">Status</label>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="Active">Active</asp:ListItem>
                                    <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="lblmsg" ForeColor="Tomato" runat="server"></asp:Label>
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                            <asp:Button ID="btnAddTeacher" runat="server" CssClass="btn btn-primary" Text="Add Teacher" OnClick="btnAddTeacher_Click" />
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
