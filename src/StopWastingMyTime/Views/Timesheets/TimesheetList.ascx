﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<StopWastingMyTime.Models.TimeBlock>>" %>
    <% foreach (var timeBlock in Model) { %>
        <div>
            <input type="hidden" class="timeBlockId" value="<%= Html.Encode(timeBlock.TimeBlockId) %>" />
            <input type="text" class="date" readonly="readonly" value="<%= Html.Encode(timeBlock.Date) %>" />
            <input type="text" class="workPackage" readonly="readonly" value="<%= Html.Encode(timeBlock.JobId) %>" />
            <input type="text" class="hours" readonly="readonly" value="<%= Html.Encode(timeBlock.Time) %>" />
            <a href="#" class="edit">Edit</a>
            <a href="#" class="save" style="display: none;">Save</a>
            <a href="#" class="cancel" style="display: none;">Cancel</a>
            <a href="#" class="delete">Delete</a>
        </div>
    <% } %>
        <div>
            <input type="hidden" class="timeBlockId" />
            <input type="text" class="date" />
            <input type="text" class="workPackage" />
            <input type="text" class="hours" />
            <a href="#" class="add">Add</a>
        </div>