<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.WithServices.Master" Inherits="System.Web.Mvc.ViewPage<System.Exception>" %>
<%@ Import Namespace="NakedObjects.Resources" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: MvcUi.DomainErrorTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.History()%> 
    <div class="error">
        <img alt="error" src="<%: Url.Content("~/Images/system-error.png") %>" />
	    <h2><%: MvcUi.DomainErrorText %></h2>

	    <p><%: Model.Message %></p>
    </div>
</asp:Content>
