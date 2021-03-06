﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<StopWastingMyTime.Models.Job>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create Job 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create Job</h2>

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
                <%= Html.LabelFor(model => model.ClientId) %>
            </div>
            <div class="editor-field">
                <%= Html.DropDownListFor(model => model.ClientId, (SelectList)ViewData["ClientList"]) %>
                <%= Html.ValidationMessageFor(model => model.ClientId) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.QuotedHours) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.QuotedHours) %>
                <%= Html.ValidationMessageFor(model => model.QuotedHours) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Billable) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.Billable) %>
                <%= Html.ValidationMessageFor(model => model.Billable) %>
            </div>

            <div class="editor-label">
                <%= Html.LabelFor(model => model.Description) %>
            </div>
            <div class="editor-field">
                <%= Html.TextAreaFor(model => model.Description) %>
                <%= Html.ValidationMessageFor(model => model.Description)%>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.IsActive) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.IsActive) %>
                <%= Html.ValidationMessageFor(model => model.IsActive)%>
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

