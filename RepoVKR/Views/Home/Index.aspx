<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page - My ASP.NET MVC Application
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
    <h3>We suggest the following:</h3>
    <ol class="round">
        <li class="one">
            <h5>Бакалавры</h5>
            <a href="../Repo/Index/1/">Learn more…</a>
        </li>

        <li class="two">
            <h5>Магистры</h5>
            <a href="../Repo/Index/2/">Learn more…</a>
        </li>

        <li class="three">
            <h5>Аспиранты</h5>
            <a href="../Repo/Index/3/">Learn more…</a>
        </li>
    </ol>
</asp:Content>
