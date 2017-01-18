using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public string ObrazProgramName { get; set; }
    }
}