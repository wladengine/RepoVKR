using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepoVKR
{
    public class RepoMainList
    {
        public List<RepoMainListItem> lstGraduates { get; set; }
        public List<string> lstFilters { get; set; }
    }

    public class RepoMainListItem
    {
        public string Id { get; set; }
        public string FIO { get; set; }
        public string DirectionName { get; set; }
        public string VKRName { get; set; }
        public string ScienceDirector { get; set; }
        public int Priority { get; set; }
    }

    public class RepoSearchModel
    {
        public string PersonFIO { get; set; }
        public string VKRName { get; set; }
        public string ScienceDirector { get; set; }
        public string DirectionName { get; set; }
        public List<SelectListItem> DirectionNames { get; set; }
        public List<RepoMainListItem> lstGraduates { get; set; }
        public List<int> StudyLevelNameId { get; set; }
        public string SLName1 { get; set; }
        public string SLName2 { get; set; }
        public string SLName3 { get; set; }

    }
    public class RepoKeyWordModel
    {
        public string KeyWord { get; set; }
        public List<int> StudyLevelNameId { get; set; }
        public string SLName1 { get; set; }
        public string SLName2 { get; set; }
        public string SLName3 { get; set; }
        public List<RepoMainListItem> lstGraduates { get; set; }

    }
}