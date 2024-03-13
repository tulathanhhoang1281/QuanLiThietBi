using System;
using System.Collections.Generic;

namespace QuanLiThietBi.Domain.Models
{
    public partial class TblOrder
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }

        public virtual TblProduct Product { get; set; } = null!;
        public virtual TblUser User { get; set; } = null!;
    }
}
