using System;
using System.Collections.Generic;

namespace QuanLiThietBi.Domain.Models
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblBorrowings = new HashSet<TblBorrowing>();
            TblOrders = new HashSet<TblOrder>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }
        public int Status { get; set; }

        public virtual ICollection<TblBorrowing> TblBorrowings { get; set; }
        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
