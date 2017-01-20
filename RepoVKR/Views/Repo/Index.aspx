<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RepoVKR.RepoMainList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        Сортировать по: 
        <a href ="/Repo/Index/1?sort=fio">ФИО ↓</a> 
        <a href ="/Repo/Index/1?sort=fiod">ФИО ↑</a>  
    </div>
<% string sort = Request.Params["sort"];
   List<RepoVKR.RepoMainListItem> lst = Model.lstGraduates;
   if (sort == "fio"){
       lst = Model.lstGraduates.OrderBy(x=>x.FIO).ToList();
   }
   else if (sort == "fiod")
   {
       lst = Model.lstGraduates.OrderByDescending(x=>x.FIO).ToList();
   }
   %>
   <h2>Список ВКР</h2>
   <ol> 
        <% foreach (var x in lst) { %>
            <li><a href="/Repo/GraduateBook/<%= x.Id %>"><%= x.FIO %></a></li>
        <% } %>
    </ol> 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
