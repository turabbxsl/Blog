namespace Blogsayt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_makale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_makale()
        {
            tbl_yorum = new HashSet<tbl_yorum>();
            tbl_etiket = new HashSet<tbl_etiket>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string basliq { get; set; }

        [Required]
        public string icerik { get; set; }

        public int kullaniciid { get; set; }

        [Column(TypeName = "date")]
        public DateTime tarix { get; set; }

        public int kategoriyaid { get; set; }

        public virtual tbl_kategoriya tbl_kategoriya { get; set; }

        public virtual tbl_kullanici tbl_kullanici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_yorum> tbl_yorum { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_etiket> tbl_etiket { get; set; }
    }
}
