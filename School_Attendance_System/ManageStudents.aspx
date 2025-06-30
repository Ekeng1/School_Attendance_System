<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageStudents.aspx.cs" Inherits="School_Attendance_System.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container">
                <!-- Message Label for displaying status/errors -->
                <div class="row mb-3">
                    <div class="col-12">
                        <asp:Label ID="lblMessage" runat="server" CssClass="alert" Visible="false"></asp:Label>
                    </div>
                </div>

                <div class="d-flex justify-content-between mb-3">
                    <asp:Button ID="btnShowModal" runat="server" Text="Add Student" CssClass="btn btn-primary" OnClientClick="showModal(); return false;" />
                    <div class="input-group" style="width: 300px;">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search students..."></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="Search"
                            CssClass="btn btn-outline-secondary" OnClick="btnSearch_Click" />
                    </div>
                </div>

                <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False" CssClass="grid"
                    OnRowEditing="gvStudents_RowEditing"
                    OnRowCancelingEdit="gvStudents_RowCancelingEdit"
                    OnRowUpdating="gvStudents_RowUpdating"
                    OnRowDeleting="gvStudents_RowDeleting"
                    DataKeyNames="id" EmptyDataText="No students found.">
                    <Columns>
                        <asp:BoundField DataField="StudentID" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="Firstname" HeaderText="First Name" />
                        <asp:BoundField DataField="Lastname" HeaderText="Last Name" />
                        <asp:BoundField DataField="DOB" HeaderText="Date of Birth" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="Gender" HeaderText="Gender" />
                        <asp:BoundField DataField="Gender" HeaderText="Gender" />
                        <asp:BoundField DataField="class_name" HeaderText="Class" />
                        <asp:BoundField DataField="Section_name" HeaderText="Section" />
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="150px">
                            <EditItemTemplate>
                                <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update" CssClass="btn btn-success btn-sm" Text="✔ Save"></asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" CssClass="btn btn-secondary btn-sm" Text="✖ Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-primary btn-sm" Text="Edit"></asp:LinkButton>
                                &nbsp;
                               <asp:LinkButton
                                   runat="server"
                                   CommandName="Delete"
                                   Text="Delete"
                                   OnClientClick="return confirmDelete();"
                                   CssClass="btn btn-danger btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Add/Edit Student Modal -->
            <div class="modal fade" id="addStudentModal" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header text-white"  style="background-color:  #4c8abd" >
                            <h5 class="modal-title" id="modalTitle" runat="server">Add Student</h5>
                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <!-- Student Information -->
                            <h4 class="mb-3">Student Information</h4>
                            <div class="row g-3">
                                <div class="col-md-6">
                                    <asp:Label AssociatedControlID="txtStudentID" Text="Student ID" CssClass="form-label" runat="server" />
                                    <asp:TextBox ID="txtStudentID" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label AssociatedControlID="txtFirstname" Text="First Name" CssClass="form-label" runat="server" />
                                    <asp:TextBox ID="txtFirstname" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label AssociatedControlID="txtLastname" Text="Last Name" CssClass="form-label" runat="server" />
                                    <asp:TextBox ID="txtLastname" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label AssociatedControlID="txtDOB" Text="Date of Birth" CssClass="form-label" runat="server" />
                                    <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" TextMode="Date" required="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label AssociatedControlID="ddlGender" Text="Gender" CssClass="form-label" runat="server" />
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-select" required="true">
                                        <asp:ListItem Value="">Select Gender</asp:ListItem>
                                        <asp:ListItem Value="Male">Male</asp:ListItem>
                                        <asp:ListItem Value="Female">Female</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label AssociatedControlID="ddlClass" Text="Class" CssClass="form-label" runat="server" />
                                    <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-select" required="true"></asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label AssociatedControlID="ddlSection" Text="Section" CssClass="form-label" runat="server" />
                                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-select" required="true"></asp:DropDownList>
                                </div>
                                <div class="col-12">
                                    <asp:Label AssociatedControlID="fileUpload" Text="Student Photo" CssClass="form-label" runat="server" />
                                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
                                </div>
                            </div>

                            <!-- Parent Information -->
                            <h4 class="mt-4 mb-3">Parent/Guardian Information</h4>
                            <div class="row g-3">
                                <div class="col-md-6">
                                    <asp:Label AssociatedControlID="txtParentFirstname" Text="First Name" CssClass="form-label" runat="server" />
                                    <asp:TextBox ID="txtParentFirstname" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label AssociatedControlID="txtParentLastname" Text="Last Name" CssClass="form-label" runat="server" />
                                    <asp:TextBox ID="txtParentLastname" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label AssociatedControlID="txtParentPhone" Text="Phone Number" CssClass="form-label" runat="server" />
                                    <asp:TextBox ID="txtParentPhone" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label AssociatedControlID="txtParentEmail" Text="Email" CssClass="form-label" runat="server" />
                                    <asp:TextBox ID="txtParentEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                </div>
                                <div class="col-12">
                                    <asp:Label AssociatedControlID="txtParentAddress" Text="Address" CssClass="form-label" runat="server" />
                                    <asp:TextBox ID="txtParentAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label AssociatedControlID="ddlRelationship" Text="Relationship" CssClass="form-label" runat="server" />
                                    <asp:DropDownList ID="ddlRelationship" runat="server" CssClass="form-select" required="true">
                                        <asp:ListItem Value="">Select Relationship</asp:ListItem>
                                        <asp:ListItem Value="Father">Father</asp:ListItem>
                                        <asp:ListItem Value="Mother">Mother</asp:ListItem>
                                        <asp:ListItem Value="Guardian">Guardian</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSaveStudent" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSaveStudent_Click" OnClientClick="return validateForm();" />
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function showModal() {
            var myModal = new bootstrap.Modal(document.getElementById('addStudentModal'));
            myModal.show();
        }

        function validateForm() {
            var isValid = true;
            var requiredFields = [
                '<%= txtFirstname.ClientID %>',
                '<%= txtLastname.ClientID %>',
                '<%= txtDOB.ClientID %>',
                '<%= ddlGender.ClientID %>',
                '<%= ddlClass.ClientID %>',
                '<%= ddlSection.ClientID %>',
                '<%= txtParentFirstname.ClientID %>',
                '<%= txtParentLastname.ClientID %>',
                '<%= txtParentPhone.ClientID %>',
                '<%= ddlRelationship.ClientID %>'
            ];

            for (var i = 0; i < requiredFields.length; i++) {
                var field = document.getElementById(requiredFields[i]);
                if (!field.value.trim()) {
                    alert('Please fill in all required fields.');
                    field.focus();
                    isValid = false;
                    break;
                }
            }

            // Validate phone number format
            if (isValid) {
                var phone = document.getElementById('<%= txtParentPhone.ClientID %>').value.trim();
                if (!/^[\d\+\-\(\) ]+$/.test(phone)) {
                    alert('Please enter a valid phone number.');
                    document.getElementById('<%= txtParentPhone.ClientID %>').focus();
                    isValid = false;
                }
            }

            return isValid;
        }
        function confirmDelete() {
            return confirm('Are you sure you want to delete this student and all associated parent records?');
        }
    </script>
</asp:Content>
