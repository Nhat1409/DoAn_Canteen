namespace DACT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuyenTruyCap")]
    public partial class QuyenTruyCap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuyenTruyCap()
        {
            Taikhoans = new HashSet<Taikhoan>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaQuyenTruyCap { get; set; }

        [StringLength(15)]
        public string TenQuyenTruyCap { get; set; }

        [StringLength(50)]
        public string MoTa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Taikhoan> Taikhoans { get; set; }
    }
}
