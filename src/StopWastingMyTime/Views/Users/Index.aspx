<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<StopWastingMyTime.Models.User>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Users
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Users</h2>

    <table cellspacing="0">
        <tr>
            <th>
                UserId
            </th>
            <th>
                Name
            </th>
            <th>
                Active
            </th>
            <th></th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.Encode(item.UserId) %>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.Active) %>
            </td>
            <td class="actions">
                <%= Html.ActionLink("Details", "Details", new { id = item.UserId }, new { @class = "details", title = "Details" })%>
                <%= Html.ActionLink("Edit", "Edit", new { id = item.UserId }, new { @class = "edit", title = "Edit" })%>
                <%= Html.ActionLink("Delete", "Delete", new { id = item.UserId }, new { @class = "delete", title = "Delete" })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

