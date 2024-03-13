using System;
using System.Collections.Generic;

namespace QuanLiThietBi.Models
{
    public partial class TblBorrowing
    {
        public int BorrowingId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Status { get; set; }

        public virtual TblProduct Product { get; set; } = null!;
        public virtual TblUser User { get; set; } = null!;
    }
}
