<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="NakedObjects.Resources" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Models" %>

    <% if (Model is FindViewModel) {%>
        <%var fvm = (FindViewModel) Model; %>
        <%:Html.Object(Html.ObjectTitle(fvm.ContextObject).ToString(), IdHelper.ViewAction, fvm.ContextObject)%>
        <%:Html.ValidationSummary(true, MvcUi.EditError)%>
        <%:Html.UserMessages() %>
        <%
         using (Html.BeginForm(IdHelper.EditAction,
                                Html.TypeName(fvm.ContextObject).ToString(),
                                new {id = Html.GetObjectId(fvm.ContextObject).ToString()},
                                FormMethod.Post,
                                new {@class = IdHelper.EditName})) {%>
            <%: Html.PropertyListEdit(fvm.ContextObject, fvm.TargetObject, fvm.TargetAction, fvm.PropertyName, fvm.ActionResult)%>   
            <%}%>
     <%}else {%>     
        <%:Html.Object(Html.ObjectTitle(Model).ToString(), IdHelper.ViewAction, Model) %>
        <%:Html.ValidationSummary(true, MvcUi.EditError)%>
        <%:Html.UserMessages() %>
        <%
            using (Html.BeginForm(IdHelper.EditAction,
                                Html.TypeName(Model).ToString(),
                                new {id = Html.GetObjectId(Model).ToString()},
                                FormMethod.Post,
                                new {@class = IdHelper.EditName})) {%>
            <%:Html.PropertyListEdit(Model)%>     
            <%}%>
            
     <%}%> 
     <%: Html.CancelButton()%>  