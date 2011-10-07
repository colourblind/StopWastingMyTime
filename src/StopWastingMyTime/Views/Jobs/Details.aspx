<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<StopWastingMyTime.Models.Job>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>
    
    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">JobId</div>
        <div class="display-field"><%= Html.Encode(Model.JobId) %></div>
        
        <div class="display-label">Client</div>
        <div class="display-field"><%= Html.Encode(Model.Client.Name) %></div>
        
        <div class="display-label">Billable</div>
        <div class="display-field"><%= Html.Encode(Model.Billable) %></div>

        <div class="display-label">Quoted Hours</div>
        <div class="display-field"><%= Html.Encode(Model.QuotedHours) %></div>

        <div class="display-label">Description</div>
        <div class="display-field"><%= Html.Encode(Model.Description) %></div>

        <div class="display-label">Active</div>
        <div class="display-field"><%= Html.Encode(Model.IsActive) %></div>
        
        <div class="display-label">Timesheet Entries</div>
        <table cellspacing="0">
            <tr>
                <th>User</th>
                <th>Date</th>
                <th>Hours</th>
            </tr>
        <% foreach (StopWastingMyTime.Models.TimeBlock t in Model.TimeBlocks) { %>
            <tr>
                <td><%= Html.Encode(t.User.Name) %></td>
                <td><%= Html.Encode(t.Date) %></td>
                <td><%= Html.Encode(t.Time) %></td>
            </tr>
        <% } %>
        </table>
        
    </fieldset>
    <p>
        <%= Html.ActionLink("Edit", "Edit", new { id = Model.JobId }) %> |
        <%= Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>
