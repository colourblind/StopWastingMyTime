<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<StopWastingMyTime.Models.TimeBlock>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    
    <div id="timesheet">
        <% Html.RenderPartial("TimesheetList"); %>
    </div>
    
    
    <script type="text/javascript" src="/Static/Javascript/Timesheet.js"></script>
</asp:Content>
