namespace Blogsayt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbl-kullanici")]
    public partial class tbl_kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_kullanici()
        {
            tbl_makale = new HashSet<tbl_makale>();
            tbl_yorum = new HashSet<tbl_yorum>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(10)]
        public string kullaniciadi { get; set; }

        [Required]
        [StringLength(10)]
        [DataType(DataType.Password)]
        public string sifre { get; set; }

        [StringLength(50)]
        [DisplayName("Adiniz")]
        public string isim { get; set; }

        [StringLength(50)]
        [DisplayName("Soyadiniz")]
        public string soyisim { get; set; }


        [StringLength(50)]
        [DisplayName("Email Adresi")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$",ErrorMessage ="Email Adresini Duzgun Girin !")]
        public string email { get; set; }


        public int yetkiid { get; set; }

        [Column(TypeName = "date")]
        public DateTime kayittarixi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_makale> tbl_makale { get; set; }

        public virtual tbl_yetki tbl_yetki { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_yorum> tbl_yorum { get; set; }
    }
}
