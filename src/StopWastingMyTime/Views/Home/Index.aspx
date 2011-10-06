<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Monthly Report</h2>

<div class="filter">
    <% using (Html.BeginForm()) { %>
    <%= Html.TextBox("fromDate", (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yy"), new { @class = "date" })%> to
    <%= Html.TextBox("toDate", (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yy"), new { @class = "date" })%>
    <input type="submit" value="Update" />
    <% } %>
</div>

<table cellspacing="0">
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

<h2>Problem Report</h2>

<table cellspacing="0">
    <thead>
        <tr>
            <% foreach (System.Data.DataColumn col in ((System.Data.DataTable)ViewData["ProblemReport"]).Columns) { %>
                <th><%= col.Caption %></th>
            <% } %>
        </tr>
    </thead>
    <tbody>
    <% foreach (System.Data.DataRow row in ((System.Data.DataTable)ViewData["ProblemReport"]).Rows) { %>
        <tr class="overrun">
            <% foreach (var cell in row.ItemArray) {%>
                <td><%= cell.ToString() %></td>
            <% } %>
        </tr>
    <%} %>
    </tbody>
</table>

</asp:Content>
