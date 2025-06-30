<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="School_Attendance_System.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2 id="title"><%: Title %>.</h2>
        <h3>Your contact page.</h3>
        <address>
            One Microsoft Way<br />
            Redmond, WA 98052-6399<br />
            <abbr title="Phone">P:</abbr>
            425.555.0100
        </address>

        <address>
            <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
            <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
        </address>
        <div>
            <div class="mb-3">
                <button type="button" class="btn btn-primary" style="background-color: #4c8abd;" data-bs-toggle="modal" data-bs-target="#addTeacherModal">
                    Add Teacher
                </button>
            </div>

            <asp:GridView ID="gvStudents" runat="server" CssClass="grid" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="StudentID" HeaderText="StudentID" SortExpression="StudentID" />
                    <asp:BoundField DataField="Firstname" HeaderText="Firstname" SortExpression="Firstname" />
                    <asp:BoundField DataField="Lastname" HeaderText="Lastname" SortExpression="Lastname" />
                    <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                    <asp:BoundField DataField="sectionid" HeaderText="sectionid" SortExpression="sectionid" />
                    <asp:BoundField DataField="Classid" HeaderText="Classid" SortExpression="Classid" />
                    <asp:BoundField DataField="Photo" HeaderText="Photo" SortExpression="Photo" />
                    <asp:BoundField DataField="MothersNo" HeaderText="MothersNo" SortExpression="MothersNo" />
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                            &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="" ShowDeleteButton="True" ButtonType="Button" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PsamsConnectionString %>" SelectCommand="SELECT [StudentID], [Firstname], [Lastname], [Gender], [sectionid], [Classid], [Photo], [MothersNo] FROM [Students]"></asp:SqlDataSource>

        </div>
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
                        <asp:Button ID="btnAddTeacher" runat="server" CssClass="btn btn-primary" Text="Add Teacher"/>
                    </div>
                </div>
            </div>
        </div>
        </div>
</asp:Content>
