namespace Blogsayt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_yorum
    {
        public int id { get; set; }

        [Required]
        public string yorumicerik { get; set; }

        public int yorumkullaniciID { get; set; }

        public int yorummakaleID { get; set; }

        [Column(TypeName = "date")]
        public DateTime tarix { get; set; }

        public virtual tbl_makale tbl_makale { get; set; }

        public virtual tbl_kullanici tbl_kullanici { get; set; }
    }
}
