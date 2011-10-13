<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<StopWastingMyTime.Models.Client>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
                
        <div class="display-label">Name</div>
        <div class="display-field"><%= Html.Encode(Model.Name) %></div>
        
        <div class="display-label">Jobs</div>
        <table cellspacing="0">
            <tr>
                <th>JobId</th>
                <th>Description</th>
                <th>Quoted Hours</th>
                <th>Total Hours</th>
            </tr>
        <% foreach (StopWastingMyTime.Models.Job j in Model.Jobs.OrderBy(o => o.JobId)) { %>
            <tr>
                <td><%= Html.ActionLink(j.JobId, "Details", "Jobs", new { id = j.JobId }, null) %></td>
                <td><%= Html.Encode(j.Description) %></td>
                <td><%= Html.Encode(j.QuotedHours) %></td>
                <td><%= Html.Encode(j.TotalHours) %></td>
            </tr>
        <% } %>
        </table>
        
    </fieldset>
    <p>
        <%= Html.ActionLink("Edit", "Edit", new { id = Model.ClientId }) %> |
        <%= Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

