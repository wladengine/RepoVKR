//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RepoVKR.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            this.Student = new HashSet<Student>();
        }
    
        public System.Guid Id { get; set; }
        public int PK1 { get; set; }
        public string ST { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string FIOEng { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Student { get; set; }
    }
}
