//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XLStoSQL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Finance_DB
    {
        public int ID { get; set; }
        public Nullable<double> Trans { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string Memo { get; set; }
        public string Account { get; set; }
        public Nullable<double> Debit { get; set; }
        public Nullable<double> Credit { get; set; }
    }
}