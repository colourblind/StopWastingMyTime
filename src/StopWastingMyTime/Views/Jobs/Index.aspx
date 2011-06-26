﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<StopWastingMyTime.Models.Job>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    
    <table>
        <tr>
            <th>Job Id</th>
            <th>Billable</th>
            <th></th>
        </tr>
    <% foreach (var job in Model) { %>
        <tr>
            <td><%= Html.Encode(job.JobId) %></td>
            <td><%= Html.Encode(job.Billable) %></th>
            <td><%= Html.ActionLink("Edit", "Edit", new { id = job.JobId }) %> <%= Html.ActionLink("Delete", "Delete", new { id = job.JobId }) %></td>
        </tr>
    <% } %>
    </table>
    
    <%= Html.ActionLink("Create", "Create") %>

</asp:Content>
