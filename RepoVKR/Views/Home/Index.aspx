<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
  
</asp:Content>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>Выпускные квалификационные работы</h1>
            </hgroup>
        </div>
    </section>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:1000px;">
        <tr style="vertical-align:top;">
            <td > 
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
            <b><a href="../Repo/Search/">Поиск по параметрам</a></b> <br />
            <b><a href="../Repo/Keyword/">Поиск по ключевому слову</a></b>

            </td>
            <td style="text-align:center;">
                <h3>Информация о дипломе СПбГУ</h3>
                <hr />
                Для получения информации отсканируйте QR-код в Вашем дипломе!
            </td>
        </tr>
    </table>
</asp:Content>
