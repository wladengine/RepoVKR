//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RepoVKR_WinApp.Classes
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            this.GraduateBook = new HashSet<GraduateBook>();
        }
    
        public System.Guid Id { get; set; }
        public int PK1 { get; set; }
        public System.Guid PersonId { get; set; }
        public int FacultyPK1 { get; set; }
        public string FacultyName { get; set; }
        public string DirectionName { get; set; }
        public string ObrazProgramName { get; set; }
        public string WorkPlanNumber { get; set; }
        public string StudyLevelName { get; set; }
        public string StudyBasisName { get; set; }
        public string StudyNumber { get; set; }
        public string StudyFormName { get; set; }
    
        public virtual Person Person { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GraduateBook> GraduateBook { get; set; }
    }
}
