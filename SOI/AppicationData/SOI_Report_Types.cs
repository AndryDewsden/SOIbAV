//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SOI.AppicationData
{
    using System;
    using System.Collections.Generic;
    
    public partial class SOI_Report_Types
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SOI_Report_Types()
        {
            this.SOI_Reports = new HashSet<SOI_Reports>();
        }
    
        public int id_report_type { get; set; }
        public string report_type_name { get; set; }
        public string report_type_discription { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOI_Reports> SOI_Reports { get; set; }
    }
}
