<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reporting
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Timesheet by User</h2>
    
    <% using (Html.BeginForm("ByUser", "Reporting")) { %>

    <%= Html.TextBox("fromDate", (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yy"), new { @class = "date" }) %> to
    <%= Html.TextBox("toDate", (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yy"), new { @class = "date" }) %>
    <input type="submit" value="Download" />
    
    <% } %>

</asp:Content>
