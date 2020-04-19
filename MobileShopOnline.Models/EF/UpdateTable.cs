namespace MobileShopOnline.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UpdateTable")]
    public partial class UpdateTable
    {
        [Key]
        public int UpdateId { get; set; }

        public int? MobileId { get; set; }

        public double? OldPrice { get; set; }

        public double? NewPrice { get; set; }

        public int? OldQuantity { get; set; }

        public int? NewQuantity { get; set; }

        [StringLength(20)]
        public string StatusUpdate { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public DateTime? TimeUpdate { get; set; }

        public virtual Account Account { get; set; }

        public virtual Mobile Mobile { get; set; }
    }
}
