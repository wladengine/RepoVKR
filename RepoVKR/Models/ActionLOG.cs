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
    
    public partial class ActionLOG
    {
        public int OrdId { get; set; }
        public System.DateTime TIMESTAMP { get; set; }
        public System.Guid GraduateBookId { get; set; }
        public string ActionUser { get; set; }
        public string ActionName { get; set; }
    
        public virtual GraduateBook GraduateBook { get; set; }
    }
}
