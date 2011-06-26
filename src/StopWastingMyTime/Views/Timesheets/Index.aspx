<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    
    <div id="filter">
        <% Html.BeginForm("Index", "Timesheets", FormMethod.Get); %>
        <%= Html.TextBox("dateFrom", ViewData["dateFrom"]) %>
        <%= Html.TextBox("dateTo", ViewData["dateTo"]) %>
        <input type="submit" value="Filter" />
        <% Html.EndForm(); %>
    </div>
    
    <div id="timesheet">
        <% Html.RenderPartial("TimesheetList"); %>
    </div>
        
    <script type="text/javascript" src="/Static/Javascript/Timesheet.js"></script>
    <script type="text/javascript">
    
    $(document).ready(function() {
        $('#filter input[type=text]').datepicker({ showOn: 'focus', dateFormat: 'dd/mm/yy' });
    });
    
    </script>
    
</asp:Content>
