<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<StopWastingMyTime.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>User - <%= Html.Encode(User.Identity.Name) %></h2>

    <% using (Html.BeginForm()) { %>
    <fieldset>
        <legend>Change Password</legend>
                
        <%= Html.ValidationSummary(false) %>

        <div class="editor-label">Password</div>
        <div class="editor-field"><%= Html.PasswordFor(x => x.Password)%></div>
        
        <div class="editor-label">Confirm Password</div>
        <div class="editor-field"><%= Html.Password("confirmPassword")%></div>
        
        <input type="submit" value="Change Password" />
    </fieldset>
    <% } %>
                

</asp:Content>

