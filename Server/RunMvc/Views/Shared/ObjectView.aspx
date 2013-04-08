<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.WithServices.Master" Inherits="System.Web.Mvc.ViewPage<System.Object>" %>
<%@ Import Namespace="NakedObjects.Resources" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%:  Html.ObjectTitle(Model)%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.History(Model)%>   
    <div class="<%: IdHelper.ObjectViewName %>" id="<%: Html.ObjectTypeAsCssId(Model) %>">
        <%: Html.Object(Model)%>
        <%: Html.UserMessages() %>

        <%  if (Html.ObjectIsNotPersistent(Model)) {%>
            <% using (Html.BeginForm(IdHelper.EditObjectAction,
                            Html.TypeName(Model).ToString(),
                            new {id = Html.GetObjectId(Model).ToString()},
                            FormMethod.Post,
                            new {@class = IdHelper.EditName})) {%>
            <%:Html.MenuOnTransient(Model)%>
            <%if (Html.ObjectHasVisibleFields(Model)) {%>
                <%: Html.PropertyList(Model)%>   
            <%}%>  
         <%}%>
        <%} else {%>
            <%: Html.Menu(Model)%>
            <%if (Html.ObjectHasVisibleFields(Model)) {%>
                <%: Html.PropertyList(Model)%>   
            <%}%>
        <%}%>
    </div>
</asp:Content>
