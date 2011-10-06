<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Timesheet
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Timesheet</h2>
    
    <div class="filter">
        <% Html.BeginForm("Index", "Timesheets", FormMethod.Get); %>
        <%= Html.TextBox("date", null, new { @class = "date" }) %>
        <input type="submit" value="Change date" />
        <% Html.EndForm(); %>
    </div>
    
    <div id="timesheet">
        <% Html.RenderPartial("TimesheetList"); %>
    </div>
        
    <script type="text/javascript" src="/Static/Javascript/Timesheet.js"></script>
    
</asp:Content>
