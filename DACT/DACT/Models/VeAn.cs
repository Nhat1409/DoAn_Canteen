namespace DACT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VeAn")]
    public partial class VeAn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VeAn()
        {
            ChiTietVAs = new HashSet<ChiTietVA>();
        }

        [Key]
        public int MaVe { get; set; }

        public int? MaTaiKhoan { get; set; }

        public DateTime? NgayMua { get; set; }

        public double? TongTien { get; set; }

        public bool? TrangThai { get; set; }

        public DateTime? NgayNhan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietVA> ChiTietVAs { get; set; }

        public virtual Taikhoan Taikhoan { get; set; }
    }
}
