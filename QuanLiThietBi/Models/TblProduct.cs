using System;
using System.Collections.Generic;

namespace QuanLiThietBi.Models
{
    public partial class TblProduct
    {
        public TblProduct()
        {
            TblBorrowings = new HashSet<TblBorrowing>();
            TblComponents = new HashSet<TblComponent>();
            TblOrders = new HashSet<TblOrder>();
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public DateTime PurchaseDate { get; set; }
        public DateTime WarrantyEndDate { get; set; }
        public int Status { get; set; }
        public int LocationId { get; set; }

        public virtual TblCategory Category { get; set; } = null!;
        public virtual TblLocation Location { get; set; } = null!;
        public virtual ICollection<TblBorrowing> TblBorrowings { get; set; }
        public virtual ICollection<TblComponent> TblComponents { get; set; }
        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
