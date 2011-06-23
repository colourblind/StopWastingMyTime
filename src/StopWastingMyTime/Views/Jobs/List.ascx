<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<StopWastingMyTime.Models.Job>>" %>
[
<%= String.Join(",", Model.Select(x => String.Format("{{ \"value\": \"{0}\" }}", x.JobId)).ToArray()) %>
]
