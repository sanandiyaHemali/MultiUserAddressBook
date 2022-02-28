<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master" AutoEventWireup="true" CodeFile="CityList.aspx.cs" Inherits="MultiUserAddressBook_AdminPanel_City_CityList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <div class="container"> 
            <div class="row">
                <div class="col-md-6">
                    <h2 style="align-content:center ; color:lightcoral">City List</h2>
                </div> 
                <div class="col-md-6" style="text-align:right">
                    <br />
                    <asp:HyperLink ID="hlAddCity" runat="server" NavigateUrl="~/AdminPanel/City/Add" CssClass="btn btn-success">Add City</asp:HyperLink>
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
                    <asp:GridView ID="gvCity" runat="server" CssClass="table table-hover" OnRowCommand="gvCity_RowCommand" AutoGenerateColumns="false" >
                        <Columns>
                            <asp:BoundField DataField="UserID" HeaderText="UserID" />
                            <asp:BoundField DataField="CityID" HeaderText="CityID" />
                            <asp:BoundField DataField="StateName" HeaderText="StateName" />
                             <asp:BoundField DataField="CityName" HeaderText="CityName" />
                             <asp:BoundField DataField="STDCode" HeaderText="STDCode" />
                             <asp:BoundField DataField="PinCode" HeaderText="PinCode" />
                            <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" />
                            <asp:BoundField DataField="ModificationDate" HeaderText="ModificationDate" />

                            <asp:TemplateField HeaderText="Edit Record">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlEdit" runat="server" NavigateUrl='<%# "~/AdminPanel/City/Edit/" + Eval("CityID").ToString().Trim() %>'  CssClass="btn btn-warning">Edit</asp:HyperLink> 
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delete Record">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger" CommandName="DeleteRecord" CommandArgument='<%# Eval("CityID").ToString() %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div>
    </div>
</asp:Content>

