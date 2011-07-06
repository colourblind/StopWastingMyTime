<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<StopWastingMyTime.Models.Client>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Clients - Stop Wasting My Time
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Clients</h2>

    <table cellspacing="0">
        <tr>
            <th>Name</th>
            <th>Maintenance per Month</th>
            <th></th>
        </tr>
    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.MaintenancePerMonth) %>
            </td>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id = item.ClientId }) %> |
                <%= Html.ActionLink("Details", "Details", new { id = item.ClientId }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.ClientId }) %>
            </td>
        </tr>
    <% } %>
    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

