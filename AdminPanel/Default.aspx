<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="AdminPanel_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container"> 
            <div class="row">
                <div class="col-md-12">
                    <h2 style="align-content:center ; color:lightcoral">User Details</h2>
                </div> 
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <br />
                    <b><asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label></b>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="gvUser" runat="server" CssClass="table table-hover" AutoGenerateColumns="false" >
                        <Columns>
                             <asp:BoundField DataField="UserID" HeaderText="UserID" />
                             <asp:BoundField DataField="UserName" HeaderText="UserName" />
                            <asp:BoundField DataField="Password" HeaderText="Password" />
                             <asp:BoundField DataField="DisplayName" HeaderText="DisplayName" />
                             <asp:BoundField DataField="MobileNo" HeaderText="MobileNo" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" />
                            <asp:BoundField DataField="ModificationDate" HeaderText="ModificationDate" />

                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
</asp:Content>

