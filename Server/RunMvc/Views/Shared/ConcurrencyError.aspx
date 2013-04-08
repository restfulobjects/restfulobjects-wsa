<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.WithServices.Master" Inherits="System.Web.Mvc.ViewPage<NakedObjects.Architecture.Persist.ConcurrencyException>" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: NakedObjects.Resources.MvcUi.ConcurrencyErrorTitle%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.History()%>   
    <div class="error">
        <img alt="error" src="<%: Url.Content("~/Images/concurrency-error.png") %>"/>
	    <h2><%: NakedObjects.Resources.MvcUi.ConcurrencyErrorTitle%></h2>

	    <p><%: Model.Message %></p>

        <% if (Model.SourceNakedObject != null) {%>
            <h3><%: NakedObjects.Resources.MvcUi.ConcurrencyErrorObject%>:</h3>
            <%: Html.Object(Html.ObjectTitle(Model.SourceNakedObject.Object).ToString(), IdHelper.ViewAction, Model.SourceNakedObject.Object)%>
        <%}%>
    </div>

</asp:Content>
