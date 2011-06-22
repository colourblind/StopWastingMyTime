<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Login</h2>
    
    <% Html.BeginForm("Login", "Account", FormMethod.Post); %>

        <label for="username">Username</label>
        <%= Html.TextBox("username") %>
        
        <label for="password">Password</label>
        <%= Html.Password("password") %>

        <input type="submit" value="Login" />
        
    <% Html.EndForm(); %>
    
</asp:Content>
