namespace DACT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietVA")]
    public partial class ChiTietVA
    {
        [Key]
        public int MaChitiet { get; set; }

        public int? MaVe { get; set; }

        public int? MaMonAn { get; set; }

        public int? Soluong { get; set; }

        public double? TongTien { get; set; }

        public int? MaTaiKhoan { get; set; }

        public virtual MonAn MonAn { get; set; }

        public virtual VeAn VeAn { get; set; }
    }
}
