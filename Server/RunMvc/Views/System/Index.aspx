<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.WithServices.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html" %>


<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.History(null, true)%>   
    <h2><%: Html.SystemMessages() %></h2>
    <br/>
    <%: Html.UserMessages() %>
    <p>
        Please note that, by default, the Date Picker for date-input fields uses the UK date format (dd/mm/yy). To change this, see comments in the
        Views > Shared > Site.WithServices.Master
    </p>

    <% var warnings = global::NakedObjects.Core.Context.NakedObjectsContext.InitialisationWarnings; 
       if (warnings.Count() > 0) {
       %>
    <h3>The following warnings were raised during initialisation of Naked Objects: </h3>
    <ul>
    <% foreach (string warning in warnings)
       { %>
        <li><b><%: warning %></b></li>    
    <% } %>
    </ul>
    <% } %>

</asp:Content>
