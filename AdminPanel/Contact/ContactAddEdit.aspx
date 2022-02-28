<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master" AutoEventWireup="true" CodeFile="ContactAddEdit.aspx.cs" Inherits="MultiUserAddressBook_AdminPanel_Contact_ContactAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container"> 
       <div class="row">
            <div class="col-md-12">
                <h2 style="align-content:center ; color:lightcoral"><asp:Label ID="lblHeader" runat="server" ></asp:Label></h2>
            </div> 
       </div>
       <div class="row">
           <div class="col-md-12">
                <b><asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label></b>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                *Select Country
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                *Select State
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                *Select City
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                *Select Contact Category
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
                <asp:CheckBoxList ID="chkblContactCategory" runat="server"></asp:CheckBoxList>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                *Select Photo Path
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
                <asp:FileUpload ID="fuContactPhotoPath" runat="server"/>
                <asp:HiddenField ID="hfContactPhotoPath" runat="server"/>
                <asp:HiddenField runat="server" ID="hfAttribute"/>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                *Contact Name
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
               <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control"></asp:TextBox>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                *Contact No.
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
               <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control"></asp:TextBox>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                WhatsApp No.
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
               <asp:TextBox ID="txtWhatsAppNo" runat="server" CssClass="form-control"></asp:TextBox>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                *BirthDate
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
               <asp:TextBox ID="txtBirthDate" runat="server" CssClass="form-control"></asp:TextBox>
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
            <div class="col-md-3">
                Age
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
               <asp:TextBox ID="txtAge" runat="server" CssClass="form-control"></asp:TextBox>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                *Address
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
               <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                BloodGroup
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
               <asp:TextBox ID="txtBloodGroup" runat="server" CssClass="form-control"></asp:TextBox>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                FacebookID
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
               <asp:TextBox ID="txtFacebookID" runat="server" CssClass="form-control"></asp:TextBox>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                LinkedINID
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
               <asp:TextBox ID="txtLinkedINID" runat="server" CssClass="form-control"></asp:TextBox>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                
           </div>
            <div class="col-md-1">
                
           </div>
            <div class="col-md-8">
               <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" class="btn btn-success"/>&nbsp;
                <asp:HyperLink ID="hlContactList" runat="server" NavigateUrl="~/AdminPanel/Contact/List" CssClass="btn btn-danger">Cancel</asp:HyperLink>
           </div>
        </div>
    </div>
</asp:Content>

