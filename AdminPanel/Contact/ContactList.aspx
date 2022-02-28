<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master" AutoEventWireup="true" CodeFile="ContactList.aspx.cs" Inherits="MultiUserAddressBook_AdminPanel_Contact_ContactList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <div class="container"> 
            <div class="row">
                <div class="col-md-6">
                    <h2 style="align-content:center ; color:lightcoral"">Contact List</h2>
                </div> 
                <div class="col-md-6" style="text-align:right">
                    <br />
                    <asp:HyperLink ID="hlAddContact" runat="server" NavigateUrl="~/AdminPanel/Contact/Add" CssClass="btn btn-success">Add Contact</asp:HyperLink>
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
                    <asp:GridView ID="gvContact" runat="server" CssClass="table table-hover" OnRowCommand="gvContact_RowCommand" AutoGenerateColumns="false" >
                        <Columns>
                           
                            <asp:BoundField DataField="UserID" HeaderText="UserID" />
                             <asp:BoundField DataField="ContactID" HeaderText="ContactID" />
                             <asp:BoundField DataField="CountryName" HeaderText="CountryName" />
                            <asp:BoundField DataField="StateName" HeaderText="StateName" />
                            <asp:BoundField DataField="CityName" HeaderText="CityName" />
                            <asp:BoundField DataField="ContactCategoryName" HeaderText="ContactCategoryName" />
                            <%--<asp:boundfield datafield="contactphotopath" headertext="photo" />--%>
                                <asp:TemplateField HeaderText="Phote">
                                    <ItemTemplate>
                                        <asp:Image ID="imgContactPhoto" runat="server" Height="50px" ImageUrl='<%#Eval("ContactPhotoPath") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:BoundField DataField="PhotoAttributes" HeaderText="PhotoAttributes" />
                            <asp:BoundField DataField="ContactName" HeaderText="ContactName" />
                            <asp:BoundField DataField="ContactNo" HeaderText="ContactNo" />
                            <%--<asp:BoundField DataField="WhatsAppNo" HeaderText="WhatsAppNo" />--%>
                            <asp:BoundField DataField="BirthDate" HeaderText="BirthDate" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <%--<asp:BoundField DataField="Age" HeaderText="Age" />--%>
                            <asp:BoundField DataField="Address" HeaderText="Address" />
                            <%--<asp:BoundField DataField="BloodGroup" HeaderText="BloodGroup" />--%>
                            <%--<asp:BoundField DataField="FacebookID" HeaderText="FacebookID" />--%>
                            <%--<asp:BoundField DataField="LinkedINID" HeaderText="LinkedINID" />--%>
                            <%--<<asp:BoundField DataField="CreationDate" HeaderText="CreationDate" />--%>
                            <%--<<asp:BoundField DataField="ModificationDate" HeaderText="ModificationDate" />--%>

                            <asp:TemplateField HeaderText="Edit Record">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlEdit" runat="server" NavigateUrl='<%# "~/AdminPanel/Contact/Edit/" + Eval("ContactID").ToString().Trim() %>'  CssClass="btn btn-warning">Edit</asp:HyperLink> 
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delete Record">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger" CommandName="DeleteRecord" CommandArgument='<%# Eval("ContactID").ToString() + "," + Eval("ContactPhotoPath").ToString() %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

