//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UpayaWebApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class State
    {
        public State()
        {
            this.Districts = new HashSet<District>();
            this.Towns = new HashSet<Town>();
        }
    
        public short Id { get; set; }
        public string Name { get; set; }
        public string Abbr { get; set; }
        public string CountryId { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<Town> Towns { get; set; }
    }
}