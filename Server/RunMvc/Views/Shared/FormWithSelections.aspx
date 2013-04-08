<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.WithServices.Master" Inherits="System.Web.Mvc.ViewPage<NakedObjects.Web.Mvc.Models.FindViewModel>" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html"%>
<%@ Import Namespace="NakedObjects.Web.Mvc.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
     <% if (Model.ViewType == FindViewModel.ViewTypes.Dialog) {%>
        <%: Model.ContextAction.Name %>
     <%} else {%>
        <%: Html.ObjectTitle(Model.ContextObject)%>
     <%}%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.History()%>   
    <div class="<%: IdHelper.FindDialogName  %>">
        <%if (Model.ViewType == FindViewModel.ViewTypes.Dialog) {%> 
            <div class="<%: IdHelper.ActionDialogName %>" id="<%: Html.ObjectActionDialogId(Model.ContextObject, Model.ContextAction)%>">
                 <% Html.RenderPartial("DialogControl", Model); %>  
            </div>
        <%} else {%>
            <div class="<%: IdHelper.ObjectEditName  %>"  id="<%: Html.ObjectTypeAsCssId(Model.ContextObject)%>">
               <% Html.RenderPartial("ObjectEditControl", Model); %>   
            </div>
        <%}%>
    </div>
</asp:Content>
