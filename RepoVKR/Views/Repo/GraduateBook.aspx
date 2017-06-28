<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RepoVKR.GraduateBookModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    GraduateBook
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Сведения о выпускнике</h2>

<table border="0" style="width:500px;">
    <tr>
        <td><b>Фамилия</b></td>
    </tr>
    <tr>
        <td><%= Model.Surname %></td>
    </tr>
    <tr>
        <td><b>Имя, отчество</b></td>
    </tr>
    <tr>
        <td><%= Model.Name %></td>
    </tr>
    <tr>
        <td><b>Название темы ВКР</b></td>
    </tr>
    <tr>
        <td><%= Model.VkrName %></td>
    </tr>
    <tr>
        <td><b>Название темы ВКР на английском языке</td>
    </tr>
    <tr>
        <td><%= Model.VkrNameEng %></td>
    </tr>
    <tr>
        <td><b>Научный руководитель</b></td>
    </tr>
    <tr>
        <td><%= Model.ScienceDirector %></td>
    </tr>
    <tr>
        <td><b>Рецензент</b></td>
    </tr>
    <tr>
        <td><%= Model.Reviever %></td>
    </tr>
    <tr>
        <td><b>Аннотация</b></td>
    </tr>
    <tr>
        <td><%= Model.Annotation %></td>
    </tr>
    <tr>
        <td><b>Аннотация (на англ. языке)</b></td>
    </tr>
    <tr>
        <td><%= Model.AnnotationEng %></td>
    </tr>
</table>

<h2>Приложенные файлы</h2>
    <table border="0">
    <% foreach (var f in Model.lstFiles) { %>
    <tr>
        <td><%= f.FileName %></td>
        <td><% string sSize = f.FileSize.ToString() + " bytes";
               if (f.FileSize > 1024 * 1024 * 1024)
                   sSize = Math.Round((double)f.FileSize / (1024d * 1024d * 1024d), 2).ToString() + " GB";
               else if (f.FileSize > 1024 * 1024)
                   sSize = Math.Round((double)f.FileSize / (1024d * 1024d), 2).ToString() + " MB";
               else if (f.FileSize > 1024)
                   sSize = Math.Round((double)f.FileSize / 1024d, 2).ToString() + " kB"; %><%= sSize %></td>
        <td><a href="/Repo/GetFile/<%= f.Id %>">Скачать</a></td>
    </tr>
    <% } %>
</table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
