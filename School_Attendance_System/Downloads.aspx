<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Downloads.aspx.cs" Inherits="School_Attendance_System.Downloads" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Download</title>
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/all.min.css" rel="stylesheet" />
    <style>
        .containers {
    position: fixed;
    width: 80vw;
    left: 10vw;
    right: 10vw;
    padding: 30px;
    justify-self: center;
    background: #fff;
    border-radius: 10px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    margin: 15% auto;
}
         .textbox{
     margin-top: 11px;
     margin-bottom: 11px;
 }
    </style>
</head>
<body>
    <form id="form1" runat="server">
       <div class="containers">
    <div>
        <h2 style="align-items: center; color: #152259; font-weight: bold;">Download Files</h2>
        <asp:GridView ID="gvFiles" runat="server" style="overflow-x:auto;" CssClass="table table-bordered" AutoGenerateColumns="False" OnRowCommand="gvFiles_RowCommand" Visible="true">
         <Columns>
           <asp:BoundField DataField="FileName" HeaderText="File Name" />
           <asp:TemplateField>
               <ItemTemplate>
                   <asp:LinkButton ID="lnkDownload" runat="server" CssClass="button" style="background-color: #4c8abd; text-decoration: none;" CommandName="Download"
                       CommandArgument='<%# Eval("FileName") + "|" + Eval("FileType") %>' Text="Download" />
               </ItemTemplate>
           </asp:TemplateField>
       </Columns>
   </asp:GridView>
    </div>
</div>
    </form>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery-3.7.1.min.js"></script>
    <script src="js/js.js"></script>
</body>
</html>
