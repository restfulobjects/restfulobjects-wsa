<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.WithServices.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable>" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Html.ObjectTitle(Model)%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.History()%>   
    <div class="<%: IdHelper.StandaloneTableName %>" id="<%: Html.ObjectTypeAsCssId(Model) %>">
        <%: Html.Object(Model)%>
        <%: Html.UserMessages() %>
        <%: Html.Collection(Model)%>
    </div> 
</asp:Content>
