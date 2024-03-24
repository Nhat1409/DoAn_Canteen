namespace DACT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("MonAn")]
    public partial class MonAn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MonAn()
        {
            ChiTietVAs = new HashSet<ChiTietVA>();
        }

        [Key]
        public int MaMonAn { get; set; }

        [StringLength(50)]
        public string TenMon { get; set; }

        public decimal? GiaMon { get; set; }

        [StringLength(100)]
        public string MoTa { get; set; }

        public int MaTheLoai { get; set; }

        public string Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietVA> ChiTietVAs { get; set; }

        public virtual Theloai Theloai { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
