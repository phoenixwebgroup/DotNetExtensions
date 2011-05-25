<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcActionFilers.Controllers.OtherProductViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	OtherProduct
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>OtherProduct</h2>

    <fieldset>
        <legend>Fields</legend>
        <p>
            Id:
            <%= Html.Encode(Model.Id) %>
        </p>
        <p>
            Name:
            <%= Html.Encode(Model.Name) %>
        </p>
    </fieldset>
    <p>
        <%=Html.ActionLink("Edit", "Edit", new { /* id=Model.PrimaryKey */ }) %> |
        <%=Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

