namespace Blogsayt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_kategoriya
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_kategoriya()
        {
            tbl_makale = new HashSet<tbl_makale>();
        }

        [Key]
        public int kategoriyaid { get; set; }

        [Required]
        [StringLength(50)]
        public string kategoriyaadi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_makale> tbl_makale { get; set; }
    }
}
