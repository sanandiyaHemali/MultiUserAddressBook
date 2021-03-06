<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master" AutoEventWireup="true" CodeFile="CityAddEdit.aspx.cs" Inherits="MultiUserAddressBook_AdminPanel_City_CityAddEdit" %>

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
                *Select State
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control"></asp:DropDownList>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                *City Name
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
               <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control"></asp:TextBox>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                STD Code
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
               <asp:TextBox ID="txtSTDCode" runat="server" CssClass="form-control"></asp:TextBox>
           </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                PinCode
           </div>
            <div class="col-md-1">
                :
           </div>
            <div class="col-md-8">
               <asp:TextBox ID="txtPinCode" runat="server" CssClass="form-control"></asp:TextBox>
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
                <asp:HyperLink ID="hlCityList" runat="server" NavigateUrl="~/AdminPanel/City/List" CssClass="btn btn-danger">Cancel</asp:HyperLink>
           </div>
        </div>
    </div>
</asp:Content>

