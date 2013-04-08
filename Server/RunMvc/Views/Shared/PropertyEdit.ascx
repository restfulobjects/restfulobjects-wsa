<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Models" %>

<%if (Model is PropertyViewModel) {%>
    <%  var pvm = Model as PropertyViewModel; %>
    <%: Html.PropertyListEditWith(pvm.ContextObject, pvm.PropertyName)%>
<%} else {%>
    <%  var fvm = Model as FindViewModel; %>
    <% if (fvm.ContextAction == null) {%>
        <%:Html.PropertyListEditWith(fvm.ContextObject, fvm.TargetObject, fvm.TargetAction, fvm.PropertyName, fvm.ActionResult)%>
    <% } else { %>
        <%: Html.ParameterListWith(fvm.ContextObject, fvm.TargetObject, fvm.ContextAction, fvm.TargetAction, fvm.PropertyName, fvm.ActionResult)%>
    <% }%>
<%}%>