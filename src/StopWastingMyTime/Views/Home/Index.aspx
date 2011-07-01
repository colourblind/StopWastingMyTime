﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Maintenance Report</h2>
<table>
    <thead>
        <tr>
            <% foreach (System.Data.DataColumn col in ((System.Data.DataTable)ViewData["MaintenanceReport"]).Columns) { %>
                <th><%= col.Caption %></th>
            <% } %>
        </tr>
    </thead>
    <tbody>
    <% foreach (System.Data.DataRow row in ((System.Data.DataTable)ViewData["MaintenanceReport"]).Rows) { %>
        <tr>
            <% foreach (var cell in row.ItemArray) {%>
                <td><%= cell.ToString() %></td>
            <% } %>
        </tr>
    <%} %>         
    </tbody>
</table>

<h2>Monthly Report</h2>
<table>
    <thead>
        <tr>
            <% foreach (System.Data.DataColumn col in ((System.Data.DataTable)ViewData["MonthlyReport"]).Columns) { %>
                <th><%= col.Caption %></th>
            <% } %>
        </tr>
    </thead>
    <tbody>
    <% foreach (System.Data.DataRow row in ((System.Data.DataTable)ViewData["MonthlyReport"]).Rows) { %>
        <tr>
            <% foreach (var cell in row.ItemArray) {%>
                <td><%= cell.ToString() %></td>
            <% } %>
        </tr>
    <%} %>         
    </tbody>
</table>

</asp:Content>
