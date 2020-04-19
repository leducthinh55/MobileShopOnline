namespace MobileShopOnline.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int OrderId { get; set; }

        public int? MobileId { get; set; }

        [StringLength(50)]
        public string MobileName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int? Quantity { get; set; }

        public int? TransactionId { get; set; }

        public DateTime? OrderTime { get; set; }

        public virtual Account Account { get; set; }

        public virtual Mobile Mobile { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}
