<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<StopWastingMyTime.Models.Job>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.JobId) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.JobId) %>
                <%= Html.ValidationMessageFor(model => model.JobId) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Billable) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Billable) %>
                <%= Html.ValidationMessageFor(model => model.Billable) %>
            </div>
            
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

