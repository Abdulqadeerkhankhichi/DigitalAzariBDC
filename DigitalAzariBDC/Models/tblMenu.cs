//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DigitalAzariBDC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblMenu
    {
        public tblMenu()
        {
            this.tblAccessLevels = new HashSet<tblAccessLevel>();
        }
    
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public Nullable<bool> isParent { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string FontAwesome { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> EditBy { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string ElementId { get; set; }
    
        public virtual ICollection<tblAccessLevel> tblAccessLevels { get; set; }
    }
}
