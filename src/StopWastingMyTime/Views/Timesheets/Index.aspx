<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<StopWastingMyTime.Models.TimeBlock>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    
    <div id="timesheet">
    <% foreach (var timeBlock in Model) { %>
        <div>
            <input type="text" class="date" readonly="readonly" />
            <input type="text" class="workPackage" readonly="readonly" />
            <input type="text" class="hours" readonly="readonly" />
            <a href="#" class="edit">Edit</a>
            <a href="#" class="cancel" style="display: none;">Cancel</a>
            <a href="#" class="delete">Delete</a>
        </div>
    <% } %>
    </div>
    
    
    <script type="text/javascript" src="/Static/Javascript/Timesheet.js"></script>
</asp:Content>
