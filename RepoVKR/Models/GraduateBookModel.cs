using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepoVKR
{
    public class GraduateBookModel
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string VkrName { get; set; }
        public string VkrNameEng { get; set; }

        public string Annotation { get; set; }
        public string AnnotationEng { get; set; }

        public string ScienceDirector { get; set; }
        public string Reviever { get; set; }

        public List<GraduateBookFileItem> lstFiles { get; set; }
    }

    public class GraduateBookFileItem
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
    }
}