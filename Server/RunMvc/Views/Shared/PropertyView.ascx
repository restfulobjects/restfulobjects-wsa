<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NakedObjects.Web.Mvc.Models.PropertyViewModel>" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html" %>

<%: Html.PropertyListWith(Model.ContextObject, Model.PropertyName)%>