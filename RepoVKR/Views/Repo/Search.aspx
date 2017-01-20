<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RepoVKR.RepoSearchModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script>
    function FormSortFIO()
    {
        $('#sort').val("fio");
        FormSubmit();
    }
    function FormSortFIOd()
    {
        $('#sort').val("fiod");
        FormSubmit();
    }
    function FormSubmit() {
        $('#fOpenCard').submit();
    }
    function dSlname1() {
        if ($('#SLName1').val() == "1") {
            $('#SLName1').val("0");
            $("#dSlname1").val("Бакалавры");
            document.getElementById("dSlname1").innerHTML = $('#dSlname1').text();
        }
        else {
            $('#SLName1').val("1");
            document.getElementById("dSlname1").innerHTML = $('#dSlname1').text().bold();
        }
    }
    function dSlname2() {
        if ($('#SLName2').val() == "1") {
            $('#SLName2').val("0");
            document.getElementById("dSlname2").innerHTML = $('#dSlname2').text();
        }
        else {
            $('#SLName2').val("1");
            document.getElementById("dSlname2").innerHTML = $('#dSlname2').text().bold();
        }
    }
    function dSlname3() {
        if ($('#SLName3').val() == "1") {
            $('#SLName3').val("0");
            document.getElementById("dSlname3").innerHTML = $('#dSlname3').text();
        }
        else {
            $('#SLName3').val("1");
            document.getElementById("dSlname3").innerHTML = $('#dSlname3').text().bold();
        }
    }
</script>
    <form id="fOpenCard" name ="fOpenCard" method="post" >
        <% string sort = Request.Params["sort"];
        %>
    <input type="hidden" id="sort" name="sort" value="<%=sort %>"/>
    
    <%=Html.HiddenFor(x=>x.SLName1) %>
    <%=Html.HiddenFor(x=>x.SLName2) %>
    <%=Html.HiddenFor(x=>x.SLName3) %>
    <div>
        Сортировать по: 
        <%if (sort == "fio") {%><b><%} %><a onclick="FormSortFIO()">ФИО ↓</a> 
        <%if (sort == "fiod") {%><b><%} else { %></b><%} %><a onclick="FormSortFIOd()">ФИО ↑</a>
        </b>
    </div>
    <br />
    <div  >
        <div class="searchspan">Уровень</div>
            <p class="searchspan searchspanshort" id ="dSlname1" onclick ="dSlname1()"> <%if (Model.SLName1 == "1") {%><b><%}%>Бакалавры </b></p>  
            <p class="searchspan searchspanshort" id ="dSlname2" onclick ="dSlname2()"> <%if (Model.SLName2 == "1") {%><b><%}%>Магистры </b></p> 
            <p class="searchspan searchspanshort" id ="dSlname3" onclick ="dSlname3()" > <%if (Model.SLName3 == "1") {%><b><%}%>Аспиранты </b></p> 
    </div>
        <br />  
        <br />  
        <br />  
    <div>
        <div class="searchspan ">ФИО студента</div> <%=Html.TextBoxFor(x=>x.PersonFIO) %>
    </div>
    <div>
        <div class="searchspan">Направление</div> 
        <%= Html.DropDownListFor(x => x.DirectionName, Model.DirectionNames, new SortedList<string, object>() { {"class","selectDirection"} })%>
    </div>
        <br />
    <div>
        <div class="searchspan">Научный руководитель</div> <%=Html.TextBoxFor(x=>x.ScienceDirector) %>
    </div>
    <div>
        <div class="searchspan">Тема ВКР</div> <%=Html.TextBoxFor(x=>x.VKRName)%>
    </div>
    
    <div>
        <input type="submit" value="  Поиск  " class="formsubmit"/>
    </div>
   </form>
<% 
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
    <% int cnt = Model.lstGraduates.Count;
       string scnt = "ей";
       if (cnt % 100 < 11 || cnt % 100 > 20)
       {
           if (cnt % 10 == 1) scnt = "ь";
           else if (cnt % 10 >1 && cnt%10 < 5) scnt = "и";
       } %>
    Найдено: <%=Model.lstGraduates.Count %> запис<%=scnt %>
    <table>
        <thead>
            <tr>
                <td>ФИО выпускника</td>
                <td>Направление подготовки (специальность)</td>
                <td>Наименование ВКР</td>
                <td>ФИО научного руководителя</td>
            </tr>
        </thead>
        <tbody>
           <% foreach (var x in lst) { %>
            <tr>
                <td> <a href="/Repo/GraduateBook/<%= x.Id %>"><%= x.FIO %></a></td>
                <td><%= x.DirectionName %></td>
                <td><%=x.VKRName %></td>
                <td><%=x.ScienceDirector %></td>
            </tr>
        <% } %>
        </tbody>
    </table>
   <ol> 
        
    </ol> 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
