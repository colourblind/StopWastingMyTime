<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Login</h2>
    
    <% Html.BeginForm("Login", "Account", FormMethod.Post); %>
    <fieldset>
    
        <div class="editor-label">
            <label for="username">Username</label>
        </div>
        <div class="editor-field">
            <%= Html.TextBox("username") %>
        </div>
        
        <div class="editor-label">
            <label for="password">Password</label>
        </div>
        <div class="editor-field">
            <%= Html.Password("password") %>
        </div>

        <p>
            <input type="submit" value="Login" />
        </p>
        
    </fieldset>    
    <% Html.EndForm(); %>
    
</asp:Content>
