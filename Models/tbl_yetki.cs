namespace Blogsayt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_yetki
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_yetki()
        {
            tbl_kullanici = new HashSet<tbl_kullanici>();
        }

        [Key]
        public int yetkiid { get; set; }

        [Required]
        [StringLength(50)]
        public string yetkiadi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_kullanici> tbl_kullanici { get; set; }
    }
}
