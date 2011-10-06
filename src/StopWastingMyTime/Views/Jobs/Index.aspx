<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<StopWastingMyTime.Models.Job>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Jobs
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Jobs</h2>
    
    <div class="dataFilters">
        <label>Client</label>
        <select id="clientFilter" onchange="return $(this).val() == 'ALL' ? dataSet.removeFilter('ClientName') : dataSet.addFilter('ClientName', $(this).val());">
            <option>ALL</option>
        </select>
        <label>Active</label>
        <select id="activeFilter" onchange="return $(this).val() == 'ALL' ? dataSet.removeFilter('IsActive') : dataSet.addFilter('IsActive', $(this).val());">
            <option>ALL</option>
        </select>
    </div>

    <ul class="pages">
        <li><a href="#" onclick="return dataSet.pageDown();">&lt;</a></li>
        <li><a href="#" onclick="return dataSet.pageUp();">&gt;</a></li>
    </ul>

    <table cellspacing="0" id="jobData">
        <tr>
            <th><a href="#" onclick="return dataSet.setSortProperty('JobId');">Job Id</a></th>
            <th><a href="#" onclick="return dataSet.setSortProperty('ClientName');">Client</a></th>
            <th><a href="#" onclick="return dataSet.setSortProperty('Description');">Description</a></th>
            <th><a href="#" onclick="return dataSet.setSortProperty('QuotedHours');">Quoted Hours</a></th>
            <th><a href="#" onclick="return dataSet.setSortProperty('IsBillable');">Billable</a></th>
            <th><a href="#" onclick="return dataSet.setSortProperty('IsActive');">Active</a></a></th>
            <th></th>
        </tr>
    </table>
    
    <p>
    <%= Html.ActionLink("Create New", "Create") %>
    </p>

<script id="dataTemplate" type="text/x-jquery-tmpl">
        <tr>
            <td>${JobId}</td>
            <td>${ClientName}</td>
            <td>${Description}</td>
            <td>${QuotedHours}</td>
            <td>${IsBillable}</th>
            <td>${IsActive}</th>
            <td>
                <a href="${EditLink}">Edit</a>&nbsp;|&nbsp;<a href="${DeleteLink}">Delete</a>
            </td>
        </tr>
</script>

<script type="text/javascript">
var data = <%= ViewData["JobsJson"] %>;
var dataSet = new DataContainer(data, 20);
dataSet.update = function() 
{
    $('#jobData tr').slice(1).remove();
    $('#dataTemplate').tmpl(dataSet.getData()).appendTo('#jobData');
    
    var clients = dataSet.getPropertyValues('ClientName');
    clients.push('ALL');
    var clientFilter = $('#clientFilter');
    var clientFilterOldVal = clientFilter.val();
    clientFilter.empty();
    for (var i = 0; i < clients.length; i ++)
        clientFilter.append('<option>' + clients[i] + '</option>');
    clientFilter.val(clientFilterOldVal);
    
    var active = dataSet.getPropertyValues('IsActive');
    active.push('ALL');
    var activeFilter = $('#activeFilter');
    var activeFilterOldVal = activeFilter.val();
    activeFilter.empty();
    for (var i = 0; i < active.length; i ++)
        activeFilter.append('<option>' + active[i] + '</option>');
    activeFilter.val(activeFilterOldVal);
    
    var pages = $('.pages');
    pages.find('li:not(:first):not(:last)').remove();
    for (var i = 0; i <= dataSet.maxPage(); i ++)
        pages.find('li:last').before('<li' + (dataSet.page == i ? ' class="selected"' : '') + '><a href="#" onclick="return dataSet.setPage(' + i + ');">' + (i + 1) + '</a></li>');
};
dataSet.setSortProperty('JobId');
dataSet.update();
</script>

</asp:Content>
