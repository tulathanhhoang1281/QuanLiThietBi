using System;
using System.Collections.Generic;

namespace QuanLiThietBi.Models
{
    public partial class TblRole
    {
        public TblRole()
        {
            TblEmployees = new HashSet<TblEmployee>();
        }

        public int RoleId { get; set; }
        public string NameRole { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<TblEmployee> TblEmployees { get; set; }
    }
}
