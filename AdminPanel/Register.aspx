<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="AdminPanel_Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link href="~/Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/css/bootstrap-theme.min.css" rel="stylesheet" />
    <script src="~/Content/js/bootstrap.bundle.min.js"></script>
    <script src="~/Content/js/bootstrap.min.js"></script>
</head>
<body>
<form id="form" runat="server">
    <div class="container">

        <div class="row">
                    <div class="col-md-12 text-center">
                        <h3>Register To Address Book</h3><br /><br />
                    </div>
                </div>
        <div class="row">
                    <div class="col-md-12">
                        <b><asp:Label ID="lblRegisterMassage" runat="server" Text="" EnableViewState="false"></asp:Label></b><br /><br />
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
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
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
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
        <br />
        <div class="row">
                    <div class="col-md-3">
                        *Display Name
                    </div>
                    <div class="col-md-1">
                        :
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
        <br />
        <div class="row">
                    <div class="col-md-3">
                        *Mobile No
                    </div>
                    <div class="col-md-1">
                        :
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
        <br />
        <div class="row">
                    <div class="col-md-3">
                        *Email
                    </div>
                    <div class="col-md-1">
                        :
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
        <br />
        <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-1"></div>
                    <div class="col-md-8">
                        <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-success" Text="Register" OnClick="btnRegister_Click" />
                        <asp:HyperLink ID="hlRegister" runat="server" NavigateUrl="~/AdminPanel/LogIn.aspx" CssClass="btn btn-danger">Cancle</asp:HyperLink>
                    </div>
    </div>
    </div>
</form>
</body>
</html>
