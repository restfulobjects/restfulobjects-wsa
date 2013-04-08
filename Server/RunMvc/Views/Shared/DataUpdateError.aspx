<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.WithServices.Master" Inherits="System.Web.Mvc.ViewPage<NakedObjects.Architecture.Persist.DataUpdateException>" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: NakedObjects.Resources.MvcUi.DataUpdateErrorTitle%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.History()%>   
    <div class="error">
        <img alt="error"src="<%: Url.Content("~/Images/system-error.png") %>" />
	    <h2><%: NakedObjects.Resources.MvcUi.DataUpdateErrorTitle%></h2>

	    <p><%: Model.Message %></p>
    </div>
</asp:Content>
