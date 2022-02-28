<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master" AutoEventWireup="true" CodeFile="ContactCategoryAddEdit.aspx.cs" Inherits="MultiUserAddressBook_AdminPanel_ContactCategory_ContactCategoryAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container"> 
       <div class="row">
            <div class="col-md-12">
                <h2 style="align-content:center ; color:lightcoral"><asp:Label ID="lblHeader" runat="server" ></asp:Label></h2>
            </div> 
       </div>
        <br />
       <div class="row">
           <div class="col-md-12">
                <b><asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label></b>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                *Contact Category Name
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
               <asp:TextBox ID="txtContactCategoryName" runat="server" CssClass="form-control"></asp:TextBox>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                
           </div>
            <div class="col-md-1">
                
           </div>
            <div class="col-md-8">
               <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"  class="btn btn-success" />&nbsp;
                <asp:HyperLink ID="hlContactCategoryList" runat="server" NavigateUrl="~/AdminPanel/ContactCategory/List" CssClass="btn btn-danger">Cancel</asp:HyperLink>
           </div>
        </div>
    </div>
</asp:Content>

