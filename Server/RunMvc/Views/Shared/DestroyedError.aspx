<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.WithServices.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="NakedObjects.Resources" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html" %>

<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">
    <%: MvcUi.DestroyedErrorTitle %>
</asp:Content>

<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.History()%>   
    <div class="error">
        <img alt="error" src="<%: Url.Content("~/Images/system-error.png") %>" />
        <h2>
            <%: MvcUi.DestroyedErrorText %>
        </h2>
    </div>
</asp:Content>
