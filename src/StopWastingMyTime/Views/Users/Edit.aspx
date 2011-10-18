<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<StopWastingMyTime.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit User
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.UserId) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.UserId) %>
                <%= Html.ValidationMessageFor(model => model.UserId) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Password) %>
            </div>
            <div class="editor-field">
                <%= Html.PasswordFor(model => model.Password) %>
                <%= Html.ValidationMessageFor(model => model.Password) %>
            </div>
            
            <div class="editor-label">
                Confirm Password
            </div>
            <div class="editor-field">
                <%= Html.Password("ConfirmPassword") %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Name) %>
                <%= Html.ValidationMessageFor(model => model.Name) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Active) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.Active) %>
                <%= Html.ValidationMessageFor(model => model.Active) %>
            </div>

            <div class="editor-label">
                <%= Html.LabelFor(model => model.Permissions) %>
            </div>
            <div class="editor-field">
                <ul>
                <% foreach (string permission in (IEnumerable<string>)ViewData["PermissionList"]) { %>
                    <li>
                    <input type="checkbox" name="permission" value="<%= Html.Encode(permission) %>"<%= Model.HasPermission(permission) ? " checked=\"checked\"" : "" %> />
                    <%= Html.Encode(permission) %>
                    </li>
                <% } %>
                </ul>
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

