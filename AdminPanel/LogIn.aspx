<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogIn.aspx.cs" Inherits="AdminPanel_LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LogIn</title>
    <link href="~/Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/css/bootstrap-theme.min.css" rel="stylesheet" />
    <script src="~/Content/js/bootstrap.bundle.min.js"></script>
    <script src="~/Content/js/bootstrap.min.js"></script>
</head>
<body>
<form id="form1" runat="server">
    <div class="container">
        
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h3>Existing User Login To Address Book</h3><br /><br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <b><asp:Label ID="lblLogInMassage" runat="server" Text="" EnableViewState="false"></asp:Label></b><br /><br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        *User Name
                    </div>
                    <div class="col-md-1">
                        :
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtUName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-3">
                        *Password
                    </div>
                    <div class="col-md-1">
                        :
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtPass" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-1"></div>
                    <div class="col-md-8">
                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-success" Text="LogIn" OnClick="btnLogin_Click" />
                        <asp:HyperLink ID="hlRegister" runat="server" NavigateUrl="~/AdminPanel/Register.aspx" CssClass="btn btn-success">Register</asp:HyperLink>
                    </div>
                </div>           
                
    </div>
</form>
</body>
</html>
