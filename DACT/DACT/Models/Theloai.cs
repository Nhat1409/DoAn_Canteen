namespace DACT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Theloai")]
    public partial class Theloai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Theloai()
        {
            MonAns = new HashSet<MonAn>();
        }

        [Key]
        public int MaTheLoai { get; set; }

        [Required]
        [StringLength(50)]
        public string TenTheLoai { get; set; }

        public bool? Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MonAn> MonAns { get; set; }
    }
}
