using System;
using System.Collections.Generic;

namespace QuanLiThietBi.Domain.Models
{
    public partial class TblEmployee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int LocationId { get; set; }
        public int RoleId { get; set; }

        public virtual TblLocation Location { get; set; } = null!;
        public virtual TblRole Role { get; set; } = null!;
    }
}
