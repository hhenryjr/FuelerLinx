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
    
    public partial class FuelOrderMessageAttachment
    {
        public int Id { get; set; }
        public Nullable<int> FuelOrderMessageID { get; set; }
        public byte[] AttachmentData { get; set; }
        public string ContentType { get; set; }
        public string AttachmentName { get; set; }
    
        public virtual FuelOrderMessage FuelOrderMessage { get; set; }
    }
}
