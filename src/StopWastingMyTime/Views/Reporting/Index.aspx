<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reporting - Stop Wasting My Time
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Reporting</h2>
    
    <% using (Html.BeginForm()) { %>

    <%= Html.TextBox("fromDate", (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yy")) %>
    <%= Html.TextBox("toDate", (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yy")) %>
    
    <p>
    	<input type="submit" value="Download" />
    </p>
    
    <% } %>

<script type="text/javascript">

$(document).ready(function() {
    $('input[name="fromDate"]').datepicker({ showOn: 'focus', dateFormat: 'dd/mm/yy' });
    $('input[name="toDate"]').datepicker({ showOn: 'focus', dateFormat: 'dd/mm/yy' });
});

</script>

</asp:Content>