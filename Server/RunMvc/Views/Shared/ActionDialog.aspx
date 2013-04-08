<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.WithServices.Master" Inherits="System.Web.Mvc.ViewPage<NakedObjects.Web.Mvc.Models.FindViewModel>" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.ContextAction.Name %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.History()%>   
    <div class="<%: IdHelper.ActionDialogName %>" id="<%: Html.ObjectActionDialogId(Model.ContextObject, Model.ContextAction) %>" >
        <% Html.RenderPartial("DialogControl", Model); %>
    </div>
</asp:Content>

