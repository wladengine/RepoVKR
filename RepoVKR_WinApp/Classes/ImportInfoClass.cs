using RepoVKR_WinApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoVKR_WinApp
{
    class ImportInfoClass
    {
        public Person Person { get; private set; }
        public Student Student { get; private set; }
        public GraduateBook GraduateBook { get; private set; }

        public string ScienceDirectorST { get; private set; }
        public string ScienceDirectorSurname { get; private set; }
        public string ScienceDirectorName { get; private set; }
        
        public string ReviewerST { get; private set; }
        public string ReviewerSurname { get; private set; }
        public string ReviewerName { get; private set; }
    }
}
