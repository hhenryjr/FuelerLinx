//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VFM.EDM
{
    using System;
    using System.Collections.Generic;
    
    public partial class LoginPermission
    {
        public int Id { get; set; }
        public Nullable<PermissionType> Dashboard { get; set; }
        public Nullable<PermissionType> CompanyGrid { get; set; }
        public Nullable<PermissionType> CompanyDetail { get; set; }
        public Nullable<PermissionType> ContactGrid { get; set; }
        public Nullable<PermissionType> ContactDetail { get; set; }
        public Nullable<PermissionType> AirportGrid { get; set; }
        public Nullable<PermissionType> AirportDetail { get; set; }
        public Nullable<PermissionType> VendorGrid { get; set; }
        public Nullable<PermissionType> VendorDetail { get; set; }
        public Nullable<PermissionType> MarginMgr { get; set; }
        public Nullable<PermissionType> DropZone { get; set; }
        public Nullable<PermissionType> Transactions { get; set; }
        public Nullable<PermissionType> TaskScheduler { get; set; }
        public Nullable<PermissionType> Analysis { get; set; }
        public Nullable<bool> IsMainAdmin { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<bool> IsMarginEnabled { get; set; }
        public Nullable<bool> IsDistributionEnabled { get; set; }
    }
}
