<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<StopWastingMyTime.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.Name %> - Stop Wasting My Time
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">UserId</div>
        <div class="display-field"><%= Html.Encode(Model.UserId) %></div>
        
        <div class="display-label">Name</div>
        <div class="display-field"><%= Html.Encode(Model.Name) %></div>
        
    </fieldset>
    <p>
        <%= Html.ActionLink("Edit", "Edit", new { id = Model.UserId }) %> |
        <%= Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

