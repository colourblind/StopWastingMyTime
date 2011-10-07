<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<StopWastingMyTime.Models.TimeBlock>>" %>
        <div>
            <label>Job</label>
            <label>Hours</label>
            <label>Comments</label>
        </div>
    <% foreach (var timeBlock in Model) { %>
        <div>
            <input type="hidden" class="timeBlockId" value="<%= Html.Encode(timeBlock.TimeBlockId) %>" />
            <input type="text" class="workPackage" disabled="disabled" value="<%= Html.Encode(timeBlock.JobId) %>" />
            <input type="text" class="hours" disabled="disabled" value="<%= Html.Encode(timeBlock.Time) %>" />
            <input type="text" class="comment" disabled="disabled" value="<%= Html.Encode(timeBlock.Comment) %>" />
            <a href="#" class="edit">Edit</a>
            <a href="#" class="save" style="display: none;">Save</a> |
            <a href="#" class="cancel" style="display: none;">Cancel</a>
            <a href="#" class="delete">Delete</a>
        </div>
    <% } %>
        <div>
            <input type="hidden" class="timeBlockId" />
            <input type="text" class="workPackage" />
            <input type="text" class="hours" />
            <input type="text" class="comment" />
            <a href="#" class="add">Add</a>
        </div>
        