<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
  
</asp:Content>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>Репозиторий ВКР.</h1>
            </hgroup>
        </div>
    </section>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class ="leftmenu">
        <b>Выберите категорию:</b>
        <ul>
            <li> 
                <a href="../Repo/Index/1/"><b>Бакалавры</b></a>
            </li>
            <li>
                <a href="../Repo/Index/2/"><b>Магистры</b></a>
            </li>
            <li>
                <a href="../Repo/Index/3/"><b>Аспиранты</b></a>
            </li>
        </ul>

        <b><a href="../Repo/Search/">Поиск по параметрам</a></b>
    </div>
        
    <div>
        Приветствую тебя, дорогой гость.
    </div>
</asp:Content>
