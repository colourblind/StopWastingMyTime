<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<StopWastingMyTime.Models.Client>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Clients
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Clients</h2>

    <table cellspacing="0">
        <tr>
            <th>Name</th>
            <th></th>
        </tr>
    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td class="actions">
                <%= Html.ActionLink("Details", "Details", new { id = item.ClientId }, new { @class = "details", title = "Details" })%>
                <%= Html.ActionLink("Edit", "Edit", new { id = item.ClientId }, new { @class = "edit", title = "Edit" })%>
                <%= Html.ActionLink("Delete", "Delete", new { id = item.ClientId }, new { @class = "delete", title = "Delete" })%>
            </td>
        </tr>
    <% } %>
    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

