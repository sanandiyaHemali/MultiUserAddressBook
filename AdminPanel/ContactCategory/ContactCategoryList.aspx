<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master" AutoEventWireup="true" CodeFile="ContactCategoryList.aspx.cs" Inherits="MultiUserAddressBook_AdminPanel_ContactCategory_ContactCategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <div class="container"> 
            <div class="row">
                <div class="col-md-6">
                    <h2 style="align-content:center ; color:lightcoral">ContactCategory List</h2>
                </div> 
                <div class="col-md-6" style="text-align:right">
                    <br />
                    <asp:HyperLink ID="hlAddContactCategory" runat="server" NavigateUrl="~/AdminPanel/ContactCategory/Add" CssClass="btn btn-success">Add ContactCategory</asp:HyperLink>
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
                    <asp:GridView ID="gvContactCategory" runat="server" CssClass="table table-hover" OnRowCommand="gvContactCategory_RowCommand" AutoGenerateColumns="false" >
                        <Columns>
                             <asp:BoundField DataField="UserID" HeaderText="UserID" />
                             <asp:BoundField DataField="ContactCategoryID" HeaderText="ContactCategoryID" />
                             <asp:BoundField DataField="ContactCategoryName" HeaderText="ContactCategoryName" />
                            <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" />
                            <asp:BoundField DataField="ModificationDate" HeaderText="ModificationDate" />

                            <asp:TemplateField HeaderText="Edit Record">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlEdit" runat="server" NavigateUrl='<%# "~/AdminPanel/ContactCategory/Edit/" + Eval("ContactCategoryID").ToString().Trim() %>'  CssClass="btn btn-warning">Edit</asp:HyperLink> 
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delete Record">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger" CommandName="DeleteRecord" CommandArgument='<%# Eval("ContactCategoryID").ToString() %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

