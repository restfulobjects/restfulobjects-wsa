<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NakedObjects.Web.Mvc.Models.FindViewModel>" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html" %>

    <%: Html.Object(Html.ObjectTitle(Model.ContextObject).ToString(), IdHelper.ViewAction, Model.ContextObject)%>
    <%: Html.ObjectActionName(Model.ContextAction.Name) %>
    <%: Html.ValidationSummary(true, NakedObjects.Resources.MvcUi.ActionError)%>
    <%: Html.UserMessages() %>
    <% using (Html.BeginForm(IdHelper.ActionAction + "/" + Model.ContextAction.Id,
                             Html.TypeName(Model.ContextObject).ToString(),
                             new { id = Html.GetObjectId(Model.ContextObject).ToString() },
                             FormMethod.Post, 
                             new { @class = IdHelper.DialogNameClass, enctype="multipart/form-data"})) {%>
        <%:Html.ParameterList(Model.ContextObject, Model.TargetObject, Model.ContextAction, Model.TargetAction, Model.PropertyName, Model.ActionResult)%>

        <% if (Html.ObjectIsNotPersistent(Model.ContextObject))  %>
            <%:  Html.PropertyListEditHidden(Model.ContextObject)%>
    <%}%>
    <%: Html.CancelButton()%>