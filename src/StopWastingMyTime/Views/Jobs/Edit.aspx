﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<StopWastingMyTime.Models.Job>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ViewPage1
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ViewPage1</h2>

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
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

